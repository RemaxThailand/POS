﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Net;
using System.Management;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Threading;
using System.Globalization;

namespace PowerPOS
{
    public partial class FmInitialData : DevExpress.XtraEditors.XtraForm
    {
        //const int _PROGRESS_ALL = 11;
        //public static int _PROGRESS_STEP = 0;

        public FmInitialData()
        {
            InitializeComponent();
            Param.lblStatus = lblStatus;
        }

        private void FmInitialData_Load(object sender, EventArgs e)
        {
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "DevExpress Dark Style";


            pgbStatus.Value = 0;
            //_PROGRESS_STEP = 0;

            if (!File.Exists(Param.LoadingImageLocal))
            {
                pictureBox1.ImageLocation = Param.LoadingImageUrl;
                DownloadLoadingImage();
            }
            else
            {
                try
                {
                    pictureBox1.Image = Image.FromFile(Param.LoadingImageLocal);
                }
                catch
                {
                    pictureBox1.ImageLocation = Param.LoadingImageUrl;
                    DownloadLoadingImage();
                }
            }

            progressPanel1.Description = "กำลังตรวจสอบสิทธิ์การใช้งานระบบ";
            bwCheckLicense.RunWorkerAsync();
        }

        private async void DownloadLoadingImage()
        {
            if (!Directory.Exists("Resource/Images")) Directory.CreateDirectory("Resource/Images");
            if (File.Exists(Param.LoadingImageLocal)) File.Delete(Param.LoadingImageLocal);
            using (var client = new WebClient())
            {
                await client.DownloadFileTaskAsync(new Uri(Param.LoadingImageUrl), Param.LoadingImageLocal);
            }
        }

        private void bwCheckLicense_DoWork(object sender, DoWorkEventArgs e)
        {
            Util.GetApiConfig();
            if (Param.ApiChecked)
            {
                if (Util.CanConnectInternet())
                {
                    dynamic jsonObject = Util.LoadAppConfig();
                   
                    Console.WriteLine(Param.jsonObject);
                    if (!Param.jsonObject)
                    {
                        Param.ApiChecked = false;
                    }
                }
            }

            while (!Param.ApiChecked)
            {
                FmLicense fm = new FmLicense();
                var result = fm.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    break;
                }
            }
        }

        private void bwCheckLicense_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!Param.ApiChecked)
            {
                this.Dispose();
            }
            else
            {
                progressPanel1.Description = "กำลัง Sync ข้อมูลเข้าระบบ Cloud";
                bwSyncData.RunWorkerAsync();
            }
        }

        private void bwSyncData_DoWork(object sender, DoWorkEventArgs e)
        {
            Util.SyncData();
        }

        private void bwSyncData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressPanel1.Description = "กำลังโหลดข้อมูลรายละเอียดของร้านค้า";
            bwLoadShopInfo.RunWorkerAsync();
        }

        private void bwLoadShopInfo_DoWork(object sender, DoWorkEventArgs e)
        {
            Util.LoadShopInfo();
        }

        private void bwLoadShopInfo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressPanel1.Description = "กำลังโหลดข้อมูลการตั้งค่าระบบ";
            bwLoadShopConfig.RunWorkerAsync();
        }

        private void bwLoadShopConfig_DoWork(object sender, DoWorkEventArgs e)
        {
            var i = 0;
            var d = 0;

            string config = Util.GetApiData("/shop-config/pos",
               string.Format("shop={0}", Param.ApiShopId));

            dynamic jsonConfig = JsonConvert.DeserializeObject(config);
            Console.WriteLine(jsonConfig.success);

            if (jsonConfig.success.Value)
            {
                Util.DBExecute(@"CREATE TABLE IF NOT EXISTS ShopConfig (
                shop NVARCHAR(8) NOT NULL ,
                configKey NVARCHAR(10) NOT NULL ,
                configValue NVARCHAR(20) NOT NULL ,
                PRIMARY KEY (shop,configKey))");

                const string command = @"INSERT OR REPLACE INTO ShopConfig (shop, configKey, configValue) ";
                var sb = new StringBuilder(command);

                for (i = 0; i < jsonConfig.result.Count; i++)
                {
                    if (d != 0) sb.Append(" UNION ALL ");
                    sb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}'",
                       jsonConfig.result[i].shop, jsonConfig.result[i].configKey, jsonConfig.result[i].configValue));
                    d++;
                }
                Util.DBExecute(sb.ToString());
            }
        }

        private void bwLoadShopConfig_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressPanel1.Description = "กำลังโหลดข้อมูลบาร์โค้ดสินค้า";
            bwLoadBarcode.RunWorkerAsync();
        }

        private void bwLoadShopApplication_DoWork(object sender, DoWorkEventArgs e)
        {
            var i = 0;
            var d = 0;

            string config = Util.GetApiData("/shop-config/pos",
               string.Format("shop={0}", Param.ApiShopId));

            dynamic jsonConfig = JsonConvert.DeserializeObject(config);
            Console.WriteLine(jsonConfig.success);

          
        }

        private void bwLoadBarcode_DoWork(object sender, DoWorkEventArgs e)
        {
            var startDate = DateTime.Now;
            var i = 0;
            var d = 0;

            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS  Barcode (
                shop NVARCHAR(10) NOT NULL,
                barcode NVARCHAR(16) NOT NULL,
                orderNo NVARCHAR(15) NOT NULL,
                product NVARCHAR(10),
                cost FLOAT DEFAULT 0,
                operationCost FLOAT DEFAULT 0,
                sellPrice FLOAT DEFAULT 0,
                receivedDate NVARCHAR(50),
                receivedBy NVARCHAR(5) DEFAULT 0,
                sellNo NVARCHAR(10),
                sellDate NVARCHAR(50),
                sellBy NVARCHAR(5),
                customer NVARCHAR(10),
                comment NVARCHAR(256),
                inStock BIT DEFAULT 0,
                sync BIT DEFAULT 0,
                PRIMARY KEY (shop, barcode ))");

            string barcode = Util.GetApiData("/product/barcodePos",
              string.Format("shop={0}", Param.ApiShopId));

            dynamic jsonBarcode = JsonConvert.DeserializeObject(barcode);
            Console.WriteLine(jsonBarcode.success);

            if (jsonBarcode.success.Value)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                StringBuilder sb = new StringBuilder(@"INSERT OR REPLACE INTO Barcode (barcode, orderNo, product, cost, operationCost, 
                    sellPrice, receivedDate, receivedBy, sellNo, sellDate, sellBy, customer, comment, inStock, shop) ");

                for (i = 0; i < jsonBarcode.result.Count; i++)
                {

                    //foreach(int d in jsonBarcode.result)
                    //{
                    //Console.WriteLine("Barcode[2]  " + jsonBarcode.result[i].barcode);
                    if (d != 0) sb.Append(" UNION ALL ");
                    sb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}', '{3}', '{4}', {5}, {6}, '{7}', '{8}', {9}, '{10}', '{11}', '{12}', '{13}', '{14}'",
                        jsonBarcode.result[i].barcode, jsonBarcode.result[i].orderNo, jsonBarcode.result[i].product, jsonBarcode.result[i].cost == null ? 0 : jsonBarcode.result[i].cost, jsonBarcode.result[i].operationCost == null ? 0 : jsonBarcode.result[i].operationCost, jsonBarcode.result[i].sellPrice == null ? 0 : jsonBarcode.result[i].sellPrice,
                        jsonBarcode.result[i].receivedDate.ToString() == "" ? "NULL" : "'" + jsonBarcode.result[i].receivedDate.ToString("yyyy-MM-dd HH:mm:ss") + "'", jsonBarcode.result[i].receivedBy, jsonBarcode.result[i].sellNo == null ? "" : jsonBarcode.result[i].sellNo,
                        jsonBarcode.result[i].sellDate.ToString() == "1/1/2000 12:00:00 AM" ? "NULL" : "'" + jsonBarcode.result[i].sellDate.ToString("yyyy-MM-dd HH:mm:ss") + "'", jsonBarcode.result[i].sellBy == null ? "" : jsonBarcode.result[i].sellBy, jsonBarcode.result[i].customer == "" ? "" : jsonBarcode.result[i].customer,
                        jsonBarcode.result[i].comment == null ? "" : jsonBarcode.result[i].comment, jsonBarcode.result[i].inStock == false ? 0 : 1 ,Param.ShopId));
                    //i++;
                    d++;


                    //Console.WriteLine(d);

                    if (d % 500 == 0)
                    {
                        d = 0;
                        Util.DBExecute(sb.ToString());
                        sb = new StringBuilder(@"INSERT OR REPLACE INTO Barcode (barcode, orderNo, product, cost, operationCost, 
                    sellPrice, receivedDate, receivedBy, sellNo, sellDate, sellBy, customer, comment, inStock, shop) ");
                    }

                }
                Util.DBExecute(sb.ToString());

                Console.WriteLine("Load shop barcode = {0} seconds", (DateTime.Now - startDate).TotalSeconds);
            }
            else
            {
                Console.WriteLine(jsonBarcode.errorMessage);
            }

            LoadCategory(Param.ShopId);
            LoadBrand(Param.ShopId);
        }

        private void LoadCategory(string shop)
        {
            var i = 0;
            var d = 0;

            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS Category (
                shop NVARCHAR(8) NOT NULL ,
                category NVARCHAR(8) NOT NULL,
                name NVARCHAR(100) NOT NULL,
                canDelete BIT DEFAULT 0,
                active BIT DEFAULT 1,
                priority INT DEFAULT 0,
                sync BIT DEFAULT 0,
                PRIMARY KEY (shop, category))");
            //Util.DBExecute(string.Format(@"DELETE FROM Category WHERE shop = '{0}'", Param.ShopId));

            string category = Util.GetApiData("/category/categoryPos",
            string.Format("shop={0}", Param.ApiShopId));

            dynamic jsonCategory = JsonConvert.DeserializeObject(category);
            Console.WriteLine(jsonCategory.success);

            if (jsonCategory.success.Value)
            {
                const string command = @"INSERT OR REPLACE INTO Category (shop, category, name, active, priority) ";
                var sb = new StringBuilder(command);

                for (i = 0; i < jsonCategory.result.Count; i++)
                {
                    if (d != 0) sb.Append(" UNION ALL ");
                    sb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}', {3}, {4}",
                        jsonCategory.result[i].shop, jsonCategory.result[i].category, jsonCategory.result[i].name, jsonCategory.result[i].active == true ? 1 : 0, jsonCategory.result[i].priority));

                    if (d % 500 == 0)
                    {
                        d = 0;
                        Util.DBExecute(sb.ToString());
                        sb = new StringBuilder(command);
                    }
                }
                if (d != 0)
                    Util.DBExecute(sb.ToString());
            }


            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS CategoryProfit (
                    shop NVARCHAR(8) NOT NULL ,
                    category NVARCHAR(8) NOT NULL ,
                    price MONEY DEFAULT 0,
                    price1 MONEY DEFAULT 0,
                    price2 MONEY DEFAULT 0,
                    price3 MONEY DEFAULT 0, 
                    price4 MONEY DEFAULT 0, 
                    price5 MONEY DEFAULT 0,
                    sync BIT DEFAULT 0,
                    PRIMARY KEY (category))");

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            string categoryProfit = Util.GetApiData("/category/profit",
            string.Format("shop={0}", Param.ApiShopId));

            dynamic jsonCategoryProfit = JsonConvert.DeserializeObject(categoryProfit);
            Console.WriteLine(jsonCategoryProfit.success);

            if (jsonCategoryProfit.success.Value)
            {
                const string comm = @"INSERT OR REPLACE INTO CategoryProfit (shop, category, price, price1, price2, price3, price4, price5) ";
                var sbb = new StringBuilder(comm);

                i = 0;
                d = 0;
                for (i = 0; i < jsonCategoryProfit.result.Count; i++)
                {
                    if (d != 0) sbb.Append(" UNION ALL ");
                    sbb.Append(string.Format(@" SELECT '{0}', '{1}', {2}, {3}, {4}, {5}, {6}, {7}",
                        jsonCategoryProfit.result[i].shop, jsonCategoryProfit.result[i].category, jsonCategoryProfit.result[i].price, jsonCategoryProfit.result[i].price1, jsonCategoryProfit.result[i].price2, jsonCategoryProfit.result[i].price3, jsonCategoryProfit.result[i].price4, jsonCategoryProfit.result[i].price5));
                    d++;
                    if (d % 500 == 0)
                    {
                        d = 0;
                        Util.DBExecute(sbb.ToString());
                        sbb = new StringBuilder(comm);
                    }
                }
                Util.DBExecute(sbb.ToString());
            }
        }

        private void LoadBrand(string shop)
        {
            var i = 0;
            var d = 0;

            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS Brand (
                shop NVARCHAR(8) NOT NULL ,
                brand NVARCHAR(8) NOT NULL,
                name NVARCHAR(100) NOT NULL,
                canDelete BIT DEFAULT 0,
                active BIT DEFAULT 1,
                priority INT DEFAULT 0,
                sync BIT DEFAULT 0,
                PRIMARY KEY (shop, brand ))");
            //Util.DBExecute(string.Format(@"DELETE FROM Brand WHERE shop = '{0}'", Param.ShopId));

            string brand = Util.GetApiData("/brand/brandPos",
            string.Format("shop={0}", Param.ApiShopId));

            dynamic jsonBrand = JsonConvert.DeserializeObject(brand);
            Console.WriteLine(jsonBrand.success);
            if (jsonBrand.success.Value)
            {
                const string command = @"INSERT OR REPLACE INTO Brand (shop, brand, name, active, priority) ";
                var sb = new StringBuilder(command);
                i = 0;


                for (i = 0; i < jsonBrand.result.Count; i++)
                {
                    if (d != 0) sb.Append(" UNION ALL ");
                    sb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}', {3}, {4}",
                        jsonBrand.result[i].shop, jsonBrand.result[i].brand, jsonBrand.result[i].name, jsonBrand.result[i].active == true ? 1 : 0, jsonBrand.result[i].priority));

                    d++;
                    if (d % 500 == 0)
                    {
                        d = 0;
                        Util.DBExecute(sb.ToString());
                        sb = new StringBuilder(command);
                    }
                }
                if (d != 0)
                    Util.DBExecute(sb.ToString());
            }
        }

        private void bwLoadBarcode_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressPanel1.Description = "กำลังดาวน์โหลดข้อมูลสินค้า";
            bwLoadProduct.RunWorkerAsync();
        }

        private void bwLoadProduct_DoWork(object sender, DoWorkEventArgs e)
        {
            var startDate = DateTime.Now;
            InsertProduct(Param.ShopId);
            Console.WriteLine("Load parent product = {0} seconds", (DateTime.Now - startDate).TotalSeconds);
        }

        private void InsertProduct(string shop)
        {
            var i = 0;
            var d = 0;

            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS Product (
                shop NVARCHAR(8) NOT NULL ,
                product NVARCHAR(10) NOT NULL ,
                sku NVARCHAR(20),
                name NVARCHAR(100), 
                image NVARCHAR(256),
                price FLOAT DEFAULT 0,
                price1 FLOAT DEFAULT 0,
                price2 FLOAT DEFAULT 0,
                price3 FLOAT DEFAULT 0,
                price4 FLOAT DEFAULT 0,
                price5 FLOAT DEFAULT 0,
                warranty INT DEFAULT 0,
                webPrice FLOAT DEFAULT 0,
                webPrice1 FLOAT DEFAULT 0,
                webPrice2 FLOAT DEFAULT 0,
                webPrice3 FLOAT DEFAULT 0,
                webPrice4 FLOAT DEFAULT 0,
                webPrice5 FLOAT DEFAULT 0,
                webWarranty INT DEFAULT 0,
                isPromotion BIT DEFAULT 0,
                pricePromotion FLOAT DEFAULT 0,
                cost FLOAT DEFAULT 0,
                category NVARCHAR(8),
                brand NVARCHAR(8),
                barcode NVARCHAR(50),
                quantity INT DEFAULT 0,
                sync BIT DEFAULT 0,
                PRIMARY KEY (shop, product))
            ");

            string inProduct = Util.GetApiData("/product/insertPos",
            string.Format("shop={0}", Param.ApiShopId));

            dynamic jsonInProduct = JsonConvert.DeserializeObject(inProduct);
            Console.WriteLine(jsonInProduct.success);

            string product = Util.GetApiData("/product/pos",
            string.Format("shop={0}", Param.ApiShopId));


            dynamic jsonProduct = JsonConvert.DeserializeObject(product);
            Console.WriteLine(jsonProduct.success);

            if (jsonProduct.success.Value)
            {

                const string command = @"INSERT OR REPLACE INTO Product (shop, product, sku, name, image, Price, Price1, Price2, Price3, Price4, warranty, 
                                            webPrice, webPrice1, webPrice2, webPrice3, webPrice4, webPrice5, webWarranty, isPromotion, pricePromotion, cost, category, brand, barcode, quantity) ";
                var sb = new StringBuilder(command);

                for (i = 0; i < jsonProduct.result.Count; i++)
                {
                    if (d != 0) sb.Append(" UNION ALL ");
                    sb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}', '{3}', '{4}', {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, '{23}', {24}",
                       jsonProduct.result[i].shop, jsonProduct.result[i].product, jsonProduct.result[i].sku, jsonProduct.result[i].name,
                       jsonProduct.result[i].image == null ? "" : jsonProduct.result[i].image, jsonProduct.result[i].price, jsonProduct.result[i].price1, jsonProduct.result[i].price2, jsonProduct.result[i].price3, jsonProduct.result[i].price4, 
                       jsonProduct.result[i].webWarranty, jsonProduct.result[i].webPrice, jsonProduct.result[i].webPrice1, jsonProduct.result[i].webPrice2,
                       jsonProduct.result[i].webPrice3, jsonProduct.result[i].webPrice4, jsonProduct.result[i].webPrice5, jsonProduct.result[i].webWarranty,
                       jsonProduct.result[i].isPromotion == null ? 0 : jsonProduct.result[i].isPromotion == true ? 1 : 0, jsonProduct.result[i].pricePromotion == null ? 0 : jsonProduct.result[i].pricePromotion, jsonProduct.result[i].cost,
                       jsonProduct.result[i].category == null ? "" : jsonProduct.result[i].category, jsonProduct.result[i].brand == null ? "" : jsonProduct.result[i].brand, jsonProduct.result[i].barcode == null ? "" : jsonProduct.result[i].barcode, 
                       jsonProduct.result[i].Quantity == null ? "" : jsonProduct.result[i].Quantity));
                    d++;
                    if (d % 30 == 0)
                    {
                        d = 0;
                        Console.WriteLine(sb.ToString());
                        Util.DBExecute(sb.ToString());
                        sb = new StringBuilder(command);
                    }
                }
                Util.DBExecute(sb.ToString());
            }
        }

        private void bwLoadProduct_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressPanel1.Description = "กำลังเตรียมข้อมูลสินค้าเพื่อขาย";
            bwInitialShopProduct.RunWorkerAsync();
        }

        private void bwInitialShopProduct_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            DataTable dt;

            //_TABLE_CONFIG = Util.DBQuery("SELECT COUNT(*) cnt FROM ShopConfig");
            //if (_TABLE_CONFIG.Rows[0]["cnt"].ToString() != "0")
            //if(Param.ShopType == "shop")
            //{
            //    dt = Util.DBQuery(string.Format(@"SELECT '{0}', p.product, p.name, p.sku, p.image, p.category, p.brand, p.price, p.price1, p.price2, p.price3, p.price4, p.price5, p.warranty,
            //            p.webPrice, p.webPrice1, p.webPrice2, p.webPrice3, p.webPrice4, p.webPrice5, p.webWarranty, p.cost
            //            FROM (SELECT DISTINCT Product FROM Barcode) b
            //            LEFT JOIN Product p
            //            ON b.Product = p.Product
            //            AND p.Shop = '{0}'
            //            ", Param.ShopId, Param.ShopParent));

            //    while (i < dt.Rows.Count)
            //    {
            //        Util.DBExecute(string.Format(@"INSERT OR REPLACE INTO Product (shop, product, name, sku, image, category, brand, price, price1, price2, price3, price4, price5, warranty, webPrice, webPrice1, webPrice2, webPrice3, webPrice4, webPrice5, webWarranty, cost) 
            //        VALUES ('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}',{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21})"
            //        , Param.ShopId, dt.Rows[i]["product"].ToString(), dt.Rows[i]["name"].ToString(), dt.Rows[i]["sku"].ToString(), dt.Rows[i]["image"].ToString(), dt.Rows[i]["category"].ToString(), dt.Rows[i]["brand"].ToString(), dt.Rows[i]["price"].ToString(), dt.Rows[i]["price1"].ToString()
            //        , dt.Rows[i]["price2"].ToString(), dt.Rows[i]["price3"].ToString(), dt.Rows[i]["price4"].ToString(), dt.Rows[i]["price5"].ToString(), dt.Rows[i]["warranty"].ToString(), dt.Rows[i]["webPrice"].ToString(), dt.Rows[i]["webPrice1"].ToString()
            //        , dt.Rows[i]["webPrice2"].ToString(), dt.Rows[i]["webPrice3"].ToString(), dt.Rows[i]["webPrice4"].ToString(), dt.Rows[i]["webPrice5"].ToString(), dt.Rows[i]["webWarranty"].ToString(), dt.Rows[i]["cost"].ToString()));
            //        i++;
            //    }
            //}
            //else
            //{
            //    //Util.DBExecute(string.Format(@"INSERT OR REPLACE INTO Product (shop, product, name, image, category, brand, price, price1, price2, price3, price4, price5, warranty,
            //    //    webPrice, webPrice1, webPrice2, webPrice3, webPrice4, webPrice5, webWarranty, cost) 
            //    //    SELECT '{0}', p.product, p.name, p.image, p.category, p.brand, p.price, p.price1, p.price2, p.price3, p.price4, p.price5, p.warranty,
            //    //    p.webPrice, p.webPrice1, p.webPrice2, p.webPrice3, p.webPrice4, p.webPrice5, ps.Warranty WebWarranty, ps.Cost
            //    //    FROM (SELECT DISTINCT Product FROM Barcode) b
            //    //    LEFT JOIN Product p
            //    //    ON b.Product = p.ID
            //    //    AND p.Shop = '{1}'
            //    //    LEFT JOIN Product ps
            //    //    ON b.Product = ps.ID
            //    //    AND ps.Shop = '{0}'", Param.ShopId, Param.ShopParent));
            //    //i++;

            //}


            dt = Util.DBQuery("SELECT * FROM ShopConfig");
            i = 0;
            if (Param.ShopType == "shop")
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["configKey"].ToString() == "price")
                    {
                        Util.DBExecute(string.Format(@"UPDATE Product SET Price = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price,0) <> {1}", Param.ShopId, dt.Rows[i]["configValue"].ToString()));
                    }
                    else if (dt.Rows[i]["configKey"].ToString() == "price1")
                    {
                        Util.DBExecute(string.Format(@"UPDATE Product SET Price1 = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price1,0) <> {1}", Param.ShopId, dt.Rows[i]["configValue"].ToString()));
                    }

                    else if (dt.Rows[i]["configKey"].ToString() == "price2")
                    {
                        Util.DBExecute(string.Format(@"UPDATE Product SET Price2 = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price2,0) <> {1}", Param.ShopId, dt.Rows[i]["configValue"].ToString()));
                    }

                    else if (dt.Rows[i]["configKey"].ToString() == "price3")
                    {
                        Util.DBExecute(string.Format(@"UPDATE Product SET Price3 = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price3,0) <> {1}", Param.ShopId, dt.Rows[i]["configValue"].ToString()));
                    }

                    else if (dt.Rows[i]["configKey"].ToString() == "price4")
                    {
                        Util.DBExecute(string.Format(@"UPDATE Product SET Price4 = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price4,0) <> {1}", Param.ShopId, dt.Rows[i]["configValue"].ToString()));
                    }

                    else if (dt.Rows[i]["configKey"].ToString() == "price5")
                    {
                        Util.DBExecute(string.Format(@"UPDATE Product SET Price5 = {1}, Sync = 1 WHERE Shop = '{0}' AND IFNULL(Price5,0) <> {1}", Param.ShopId, dt.Rows[i]["configValue"].ToString()));
                    }
                }

                Util.DBExecute(string.Format(@"UPDATE Product SET Warranty = IFNULL(WebWarranty,0), Sync = 1 WHERE Shop = '{0}' AND IFNULL(WebWarranty,0) <> IFNULL(Warranty,'')", Param.ShopId));

            }
            else if (Param.ShopType == "event")
            {
                //Util.DBExecute(string.Format(@"UPDATE Product SET Sync = 1 WHERE Shop = '{0}'", Param.ShopId));
                Util.DBExecute(string.Format(@"UPDATE Product SET Price = {1},Price1 = {1},Price2 = {1},Price3 = {1},Price4 = {1},Price5 = {1},Sync = 1 WHERE category = '1' AND Shop = '{0}' AND IFNULL(Price,0) <> {1}", Param.ShopId, 99));
                Util.DBExecute(string.Format(@"UPDATE Product SET Price = {1},Price1 = {1},Price2 = {1},Price3 = {1},Price4 = {1},Price5 = {1},Sync = 1 WHERE product IN ('1124','1125','1127','1128') AND Shop = '{0}' AND IFNULL(Price,0) <> {1}", Param.ShopId, 59));

            }
            else
            {
                //Util.DBExecute(string.Format(@"UPDATE Product SET Sync = 1 WHERE Shop = '{0}'", Param.ShopId));
            }

            dt = Util.DBQuery(string.Format(@"SELECT product, name, image, price, price1, price2, price3, price4, price5, warranty, IFNULL(cost,0) Cost, category, brand 
                FROM Product
                WHERE Shop = '{0}'
                AND Sync = 1", Param.ShopId));
            Console.WriteLine("Update product total = {0} records", dt.Rows.Count);
            //Util.DBExecute(string.Format(@"UPDATE Product SET Sync = 0 WHERE Shop = '{0}'", Param.ShopId));
        }

        private void bwInitialShopProduct_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressPanel1.Description = "กำลังดาวน์โหลดข้อมูลลูกค้า";
            bwLoadCustomer.RunWorkerAsync();
        }

        private void bwLoadCustomer_DoWork(object sender, DoWorkEventArgs e)
        {
            var startDate = DateTime.Now;
            var i = 0;
            var d = 0;

            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS Customer (
                    shop NVARCHAR(8) NOT NULL,
                    customer NVARCHAR(8)NOT NULL,
                    member NVARCHAR(10),
                    name NVARCHAR(100),
                    firstname NVARCHAR(50),
                    lastname NVARCHAR(50),
                    nickname NVARCHAR(50),  
                    contactName NVARCHAR(50),  
                    image NVARCHAR(256),  
                    citizenID NVARCHAR(20),
                    birthday NVARCHAR(20),
                    sex NVARCHAR(10),
                    cardNo NVARCHAR(20),
                    mobile NVARCHAR(20),
                    email NVARCHAR(50),
                    shopName NVARCHAR(50),
                    address NVARCHAR(100),
                    address2 NVARCHAR(100),
                    subDistrict NVARCHAR(100),
                    district NVARCHAR(10),
                    province NVARCHAR(10),
                    zipCode NVARCHAR(10),
                    shopSameAddress BIT DEFAULT 1,
                    shopAddress NVARCHAR(100),
                    shopAddress2 NVARCHAR(100),
                    shopSubDistrict NVARCHAR(100),
                    shopDistrict NVARCHAR(10),
                    shopProvince NVARCHAR(10),
                    shopZipCode NVARCHAR(10),
                    sellPrice INT DEFAULT 0,
                    discountPercent INT DEFAULT 0,
                    credit INT DEFAULT 0,
                    comment NVARCHAR(100),
                    addDate NVARCHAR(50),
                    addBy NVARCHAR(10),
                    updateDate NVARCHAR(50),
                    updateBy NVARCHAR(10),
                    sync BIT DEFAULT 0,
                    PRIMARY KEY (shop, customer))");

            string customer = Util.GetApiData("/customer/customerInfo",
              string.Format("shop={0}", Param.ApiShopId));

            dynamic jsonCustomer = JsonConvert.DeserializeObject(customer);
            Console.WriteLine(jsonCustomer.success);

            if (jsonCustomer.success.Value)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                const string command = @"INSERT OR REPLACE INTO Customer (shop, customer, member, name, firstname, lastname, nickname, contactName, citizenID, birthday, 
                sex, cardNo, mobile, email, shopName, address, address2, subDistrict, district, province, zipCode, 
                shopSameAddress, shopAddress, shopAddress2, shopSubDistrict, shopDistrict, shopProvince, shopZipCode, 
                sellPrice, discountPercent, credit, comment, addDate) ";
                var sb = new StringBuilder(command);

                for (i = 0; i < jsonCustomer.result.Count; i++)
                {
                    if (d != 0) sb.Append(" UNION ALL ");
                    sb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', {9}, '{10}', '{11}',
                    '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}', '{22}', '{23}', '{24}',
                    '{25}', '{26}', '{27}', '{28}', '{29}', '{30}', '{31}', {32}",
                         jsonCustomer.result[i].shop, jsonCustomer.result[i].customer, jsonCustomer.result[i].member, jsonCustomer.result[i].name, jsonCustomer.result[i].firstname == null ? "" : jsonCustomer.result[i].firstname, jsonCustomer.result[i].lastname == null ? "" : jsonCustomer.result[i].lastname, jsonCustomer.result[i].nickname == null ? "" : jsonCustomer.result[i].nickname,
                        jsonCustomer.result[i].contactName == null ? "" : jsonCustomer.result[i].contactName, jsonCustomer.result[i].citizenId == null ? "" : jsonCustomer.result[i].citizenId, jsonCustomer.result[i].birthday == null ? "NULL" : "'" + jsonCustomer.result[i].birthday.ToString("yyyy-MM-dd HH:mm:ss") + "'", jsonCustomer.result[i].sex == null ? "" : jsonCustomer.result[i].sex,
                        jsonCustomer.result[i].cardNo == null ? "" : jsonCustomer.result[i].cardNo, jsonCustomer.result[i].mobile == null ? "" : jsonCustomer.result[i].mobile, jsonCustomer.result[i].email == null ? "" : jsonCustomer.result[i].email, jsonCustomer.result[i].shopName == null ? "" : jsonCustomer.result[i].shopName, jsonCustomer.result[i].address == null ? "" : jsonCustomer.result[i].address,
                        jsonCustomer.result[i].address2 == null ? "" : jsonCustomer.result[i].address2, jsonCustomer.result[i].subDistrict == null ? "" : jsonCustomer.result[i].subDistrict, jsonCustomer.result[i].district == null ? "" : jsonCustomer.result[i].district, jsonCustomer.result[i].province == null ? "" : jsonCustomer.result[i].province, jsonCustomer.result[i].zipCode == null ? "" : jsonCustomer.result[i].zipCode,
                        jsonCustomer.result[i].shopSameAddress == false ? 0 : 1, jsonCustomer.result[i].shopAddress == null ? "" : jsonCustomer.result[i].shopAddress, jsonCustomer.result[i].shopAddress2 == null ? "" : jsonCustomer.result[i].shopAddress2, jsonCustomer.result[i].shopSubDistrict == null ? "" : jsonCustomer.result[i].shopSubDistrict, jsonCustomer.result[i].shopDistrict == null ? "" : jsonCustomer.result[i].shopDistrict, 
                        jsonCustomer.result[i].shopProvince == null ? "" : jsonCustomer.result[i].shopProvince, jsonCustomer.result[i].shopZipcode == null ? "" : jsonCustomer.result[i].shopZipcode, jsonCustomer.result[i].sellPrice, jsonCustomer.result[i].discountPercent, jsonCustomer.result[i].credit, jsonCustomer.result[i].comment, jsonCustomer.result[i].addDate == null ? "NULL" : "'" + jsonCustomer.result[i].addDate.ToString("yyyy-MM-dd HH:mm:ss") + "'"));
                    d++;

                    if (d % 500 == 0)
                    {
                        d = 0;
                        Util.DBExecute(sb.ToString());
                        sb = new StringBuilder(command);
                    }
                }
                Util.DBExecute(sb.ToString());

                Console.WriteLine("Load shop Customer = {0} seconds", (DateTime.Now - startDate).TotalSeconds);
            }
            if (d == 0)
            {
                const string comm = @"INSERT OR REPLACE INTO Customer (shop, customer, Firstname, Lastname, AddDate, AddBy, Sync)";
                var sbd = new StringBuilder(comm);
                sbd.Append(string.Format(@"SELECT '{0}', '000000', 'ลูกค้า', 'ทั่วไป', STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), '0000', 1 
                                          UNION ALL SELECT '{0}', '000001', 'สำนักงานใหญ่', '', STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), '0000', 1 
                                          UNION ALL SELECT '{0}', '000002', 'ลูกค้า', 'เคลม', STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), '0000', 1", Param.ShopId));
                Util.DBExecute(sbd.ToString());
            }
        }

        private void bwLoadCustomer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressPanel1.Description = "กำลังเตรียมข้อมูลสินค้าเพื่อขาย";
            bwLoadSale.RunWorkerAsync();
        }

        private void bwLoadSale_DoWork(object sender, DoWorkEventArgs e)
        {
            var startDate = DateTime.Now;
            int i, d;

            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS SellHeader (
                sellNo NVARCHAR(10) PRIMARY KEY NOT NULL,
                customer NVARCHAR(8) DEFAULT 000000,
                customerSex NVARCHAR(10),
                customerAge INT,
                credit INT DEFAULT 0,
                payType NVARCHAR(5) DEFAULT 0,
                cash FLOAT DEFAULT 0,
                discountPercent FLOAT DEFAULT 0,
                discountCash FLOAT DEFAULT 0,
                paid BIT DEFAULT 0,
                profit FLOAT DEFAULT 0,
                totalPrice FLOAT DEFAULT 0,
                pointReceived INT DEFAULT 0,
                pointUse INT DEFAULT 0,
                comment NVARCHAR(256),
                sellDate NVARCHAR(20),
                sellBy NVARCHAR(5),
                sync BIT DEFAULT 0)");

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            string SellHeader = Util.GetApiData("/sale/saleInfo",
            string.Format("shop={0}", Param.ApiShopId));

            dynamic jsonSellHeader = JsonConvert.DeserializeObject(SellHeader);
            Console.WriteLine(jsonSellHeader.success);

            if (jsonSellHeader.success.Value)
            {
                const string comm = @"INSERT OR REPLACE INTO SellHeader (sellNo, customer, customerSex, customerAge, credit, payType, cash, discountPercent, 
                discountCash, paid, profit, totalPrice, pointReceived, pointUse, comment, sellDate, sellBy) ";
                var sbb = new StringBuilder(comm);

                i = 0;
                d = 0;
                for (i = 0; i < jsonSellHeader.result.Count; i++)
                {
                    if (d != 0) sbb.Append(" UNION ALL ");
                    sbb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', {15}, '{16}'",
                        jsonSellHeader.result[i].sellNo, jsonSellHeader.result[i].customer, jsonSellHeader.result[i].customerSex, jsonSellHeader.result[i].customerAge,
                        jsonSellHeader.result[i].credit, jsonSellHeader.result[i].payType, jsonSellHeader.result[i].cash, jsonSellHeader.result[i].discountPercent,
                        jsonSellHeader.result[i].discountCash, jsonSellHeader.result[i].paid == true ? 1 : 0, jsonSellHeader.result[i].profit, jsonSellHeader.result[i].totalPrice,
                        jsonSellHeader.result[i].pointReceived, jsonSellHeader.result[i].pointUse, jsonSellHeader.result[i].comment,
                        jsonSellHeader.result[i].sellDate == null ? "NULL" : "'" + jsonSellHeader.result[i].sellDate.ToString("yyyy-MM-dd HH:mm:ss") + "'", jsonSellHeader.result[i].sellBy));
                    d++;
                    if (d % 500 == 0)
                    {
                        d = 0;
                        Util.DBExecute(sbb.ToString());
                        sbb = new StringBuilder(comm);
                    }
                }
                Util.DBExecute(sbb.ToString());
            }

            Console.WriteLine("Load shop SellHeader = {0} seconds", (DateTime.Now - startDate).TotalSeconds);

            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS SellDetail (
                sellNo NVARCHAR(10) NOT NULL,
                product NVARCHAR(8) NOT NULL,
                barcode NVARCHAR(20) NOT NULL,
                sellPrice FLOAT NOT NULL,
                cost FLOAT DEFAULT 0,
                comment NVARCHAR(256),
                sync BIT DEFAULT 0,
                PRIMARY KEY (sellNo, product, barcode))");
            //quantity INT NOT NULL,

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            string SellDetail = Util.GetApiData("/sale/saleDetailInfo",
            string.Format("shop={0}", Param.ApiShopId));

            dynamic jsonSellDetail = JsonConvert.DeserializeObject(SellDetail);
            Console.WriteLine(jsonSellDetail.success);

            if (jsonSellDetail.success.Value)
            {
                const string comm = @"INSERT OR REPLACE INTO SellDetail (sellNo, product, barcode, sellPrice, cost, comment) ";
                var sbb = new StringBuilder(comm);

                i = 0;
                d = 0;
                for (i = 0; i < jsonSellDetail.result.Count; i++)
                {
                    if (d != 0) sbb.Append(" UNION ALL ");
                    sbb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'",
                        jsonSellDetail.result[i].sellNo, jsonSellDetail.result[i].product, jsonSellDetail.result[i].barcode,
                        jsonSellDetail.result[i].sellPrice, jsonSellDetail.result[i].cost, jsonSellDetail.result[i].comment));
                    d++;
                    if (d % 500 == 0)
                    {
                        d = 0;
                        Util.DBExecute(sbb.ToString());
                        sbb = new StringBuilder(comm);
                    }
                }
                Util.DBExecute(sbb.ToString());


                //Console.WriteLine("Load shop Customer = {0} seconds", (DateTime.Now - startDate).TotalSeconds);
            }
        }

        private void bwLoadSale_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressPanel1.Description = "กำลังดาวน์โหลดข้อมูลการคืน";
            bwLoadReturnProduct.RunWorkerAsync();
        }

        private void bwLoadReturnProduct_DoWork(object sender, DoWorkEventArgs e)
        {
            int i, d;
            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS ReturnProduct (
                returnNo NVARCHAR(10) NOT NULL,    
                sellNo NVARCHAR(10) NOT NULL,
                returnDate NVARCHAR(30),
                product NVARCHAR(10) NOT NULL,
                barcode NVARCHAR(20) NOT NULL,
                sellPrice DOUBLE NOT NULL,
                returnBy NVARCHAR(10),
                sync BOOL DEFAULT 0,
                PRIMARY KEY (sellNo, barcode))");

            string Return = Util.GetApiData("/return/returnInfo",
            string.Format("shop={0}", Param.ApiShopId));

            dynamic jsonReturn = JsonConvert.DeserializeObject(Return);
            Console.WriteLine(jsonReturn.success);

            if (jsonReturn.success.Value)
            {
                const string comm = @"INSERT OR REPLACE INTO ReturnProduct (returnNo, sellNo, returnDate, product, barcode, sellPrice, returnBy) ";
                var sbb = new StringBuilder(comm);

                i = 0;
                d = 0;
                for (i = 0; i < jsonReturn.result.Count; i++)
                {
                    if (d != 0) sbb.Append(" UNION ALL ");
                    sbb.Append(string.Format(@" SELECT '{0}', '{1}', {2}, '{3}', '{4}', '{5}', '{6}'",
                        jsonReturn.result[i].returnNo, jsonReturn.result[i].sellNo, jsonReturn.result[i].returnDate == null ? "NULL" : "'" + jsonReturn.result[i].returnDate.ToString("yyyy-MM-dd HH:mm:ss") + "'",
                        jsonReturn.result[i].product, jsonReturn.result[i].barcode, jsonReturn.result[i].sellPrice, jsonReturn.result[i].returnBy));
                    d++;
                    if (d % 500 == 0)
                    {
                        d = 0;
                        Util.DBExecute(sbb.ToString());
                        sbb = new StringBuilder(comm);
                    }
                }
                Util.DBExecute(sbb.ToString());
            }
        }

        private void bwLoadReturnProduct_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressPanel1.Description = "กำลังดาวน์โหลดข้อมูลเคลมสินค้า";
            bwLoadClaim.RunWorkerAsync();
        }

        private void bwLoadClaim_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS Claim (
                claimNo NVARCHAR(10) NOT NULL,
                claimType NVARCHAR(5) NOT NULL,
                claimDate NVARCHAR(30) NOT NULL,
                product NVARCHAR(10) NOT NULL,
                barcode NVARCHAR(20) NOT NULL,
                barcodeClaim NVARCHAR(20) ,
                description NVARCHAR(256) NOT NULL,
                price DOUBLE NOT NULL,
                priceClaim DOUBLE ,
                firstname NVARCHAR(50),
                lastname NVARCHAR(50),
                nickname NVARCHAR(50),
                tel NVARCHAR(20),
                email NVARCHAR(50),
                claimBy NVARCHAR(10),
                sync BOOL DEFAULT 0,
                PRIMARY KEY (claimNo, barcode))");


            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //var azureTable = Param.AzureTableClient.GetTableReference("Claim");
            //azureTable.CreateIfNotExists();
            //var query = new TableQuery<ClaimEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, Param.ShopId));
            //const string command = @"INSERT OR REPLACE INTO Claim ( ClaimNo, Barcode, ClaimType, BarcodeClaim, ClaimDate, Description, Price, PriceClaim, Product, Firstname, Lastname, Nickname, Tel, Email, ClaimBy) ";
            //var sb = new StringBuilder(command);

            //foreach (ClaimEntity d in azureTable.ExecuteQuery(query))
            //{
            //    if (i != 0) sb.Append(" UNION ALL ");
            //    sb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}'",
            //    d.RowKey, d.Barcode, d.ClaimType, d.BarcodeClaim, d.ClaimDate.ToString("yyyy-MM-dd HH:mm:ss"), d.Description, d.Price, d.PriceClaim, d.Product, d.Firstname, d.Lastname, d.Nickname, d.Tel, d.Email, d.ClaimBy));
            //    i++;
            //    if (i % 500 == 0)
            //    {
            //        i = 0;
            //        Util.DBExecute(sb.ToString());
            //        sb = new StringBuilder(command);
            //    }
            //}
            //Util.DBExecute(sb.ToString());
        }

        private void bwLoadClaim_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressPanel1.Description = "กำลังดาวน์โหลดข้อมูลจังหวัด";
            bwLoadProvince.RunWorkerAsync();
        }

        private void bwLoadProvince_DoWork(object sender, DoWorkEventArgs e)
        {
            var i = 0;
            var d = 0;
            string command;
            var sb = new StringBuilder();
            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS Province (
                id NVARCHAR(5) PRIMARY KEY NOT NULL,
                name NVARCHAR(50))");

            Util.DBExecute(@"CREATE TABLE IF NOT EXISTS District (
                province NVARCHAR(5) NOT NULL ,
                id NVARCHAR(5) NOT NULL ,
                name NVARCHAR(50),
                zipCode NVARCHAR(10), 
                PRIMARY KEY (province, id))");

            //DataTable dt = Util.DBQuery("SELECT COUNT(*) cnt FROM Province");
            //if (dt.Rows[0]["cnt"].ToString() == "0")
            //{
                string province = Util.GetApiData("/province/list",
                string.Format("language=Th"));

                dynamic jsonProvince = JsonConvert.DeserializeObject(province);
                Console.WriteLine(jsonProvince.success);

                command = @"INSERT OR REPLACE INTO Province (id, name) ";
                sb = new StringBuilder(command);

                if (jsonProvince.success.Value)
                {
                    for (i = 0; i < jsonProvince.result.Count; i++)
                    {
                        if (d != 0) sb.Append(" UNION ALL ");
                        sb.Append(string.Format(@" SELECT '{0}', '{1}'",
                           jsonProvince.result[i].id, jsonProvince.result[i].name));

                        d++;
                        if (d % 500 == 0)
                        {
                            d = 0;
                            Util.DBExecute(sb.ToString());
                            sb = new StringBuilder(command);
                        }
                    }
                    Util.DBExecute(sb.ToString());
                }
            //}


            // District
            string district = Util.GetApiData("/province/districtPos",
            string.Format("language=Th"));

            dynamic jsonDistrict = JsonConvert.DeserializeObject(district);
            Console.WriteLine(jsonDistrict.success);

            command = @"INSERT OR REPLACE  INTO District (province, id, name, zipcode) ";
            sb = new StringBuilder(command);
            i = 0;
            d = 0;
            if (jsonDistrict.success.Value)
            {
                for (i = 0; i < jsonDistrict.result.Count; i++)
                {
                    if (d != 0) sb.Append(" UNION ALL ");
                    sb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}', '{3}'",
                        jsonDistrict.result[i].province, jsonDistrict.result[i].id, jsonDistrict.result[i].name, jsonDistrict.result[i].zipcode));
                    d++;
                    if (d % 500 == 0)
                    {
                        d = 0;
                        Util.DBExecute(sb.ToString());
                        sb = new StringBuilder(command);
                    }
                }
                Util.DBExecute(sb.ToString());
            }
        }

        private void bwLoadProvince_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressPanel1.Description = "ดาวน์โหลดข้อมูลเสร็จเรียบร้อยแล้ว";
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

        }

       
    }
}