using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using System.Threading;
using System.Net;
using System.IO;

namespace PowerPOS
{
    public partial class UcProduct : DevExpress.XtraEditors.XtraUserControl
    {
        public bool _FIRST_LOAD;
        DataTable _TABLE_PRODUCT;
        private int _ROW_INDEX = -1;
        private int _QTY = 0;
        private double _TOTAL = 0, _VAL = 0;
        string id;
        public static string barcode;
        string _STREAM_IMAGE_URL;

        public UcProduct()
        {
            InitializeComponent();
        }

        private void SetActive(CheckEdit check)
        {
            check.Properties.Appearance.ForeColor = System.Drawing.Color.GreenYellow;
            //button.Checked = true;
        }

        private void CheckList_CheckedChanged(object sender, EventArgs e)
        {
            //if (((CheckEdit)sender).Checked)
            //{
            //    SetDefault(cbNoPrice);
            //    SetDefault(cbNoStock);
            //}
            //SetActiveCheck((CheckEdit)sender);
        }

        private void SetDefaultButton(CheckButton button)
        {
            button.Appearance.ForeColor = System.Drawing.Color.DarkGray;
            button.Checked = false;
        }

        private void UcProduct_Load(object sender, EventArgs e)
        {
            _FIRST_LOAD = true;
            LoadData();
            txtSearch.Focus();
        }

        public void LoadData()
        {
            if (Param.MemberType == "Shop")
            {
                grbPrice.Enabled = false;
            }

            DataTable dt = Util.DBQuery(string.Format(@"SELECT DISTINCT Name FROM Category ORDER BY Priority, Name"));
            cbbCategory.Properties.Items.Clear();
            cbbCategory.Properties.Items.Add("หมวดหมู่สินค้าทั้งหมด");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbbCategory.Properties.Items.Add(dt.Rows[i]["Name"].ToString());
            }
            cbbCategory.SelectedIndex = 0;

            dt = Util.DBQuery(string.Format(@"SELECT DISTINCT Name FROM Brand ORDER BY Priority, Name"));
            cbbBrand.Properties.Items.Clear();
            cbbBrand.Properties.Items.Add("ยี่ห้อสินค้าทั้งหมด");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbbBrand.Properties.Items.Add(dt.Rows[i]["Name"].ToString());
            }
            cbbBrand.SelectedIndex = 0;

            _FIRST_LOAD = false;
            SearchData();
        }

        private void SearchData()
        {
            DataTable dt;
            DataRow row ;
            int i,a;
            _QTY = 0;
            _TOTAL = 0;
            _VAL = 0;
            //dt = Util.DBQuery(string.Format("SELECT * FROM shopConfig WHERE shop = '{0}'", Param.ShopId));
            try
            {
                if (!_FIRST_LOAD)
                {
                    //if (Param.ShopType == "shop")
                    //{
                    //    _TABLE_PRODUCT = Util.DBQuery(string.Format(@"SELECT DISTINCT p.product, p.Name, IFNULL(cnt.ProductCount, 0) Qty, c.Name Category, bb.name Brand,  IFNULL(p.Warranty, 0)  Warranty, IFNULL(cnt.Cost,0) Cost,  
                    //    IFNULL(p.Price, 0) Price, IFNULL(p.Price1, 0)  Price1,IFNULL(p.Price2, 0)  Price2, IFNULL(p.Price3, 0) Price3, IFNULL(p.Price4, 0)  Price4,IFNULL(p.Price5, 0)  Price5,
                    //    p.Image, p.sku, IFNULL(p.webPrice, 0) webPrice, IFNULL(p.webPrice1, 0)  webPrice1,IFNULL(p.webPrice2, 0)  webPrice2, IFNULL(p.webPrice3, 0) webPrice3, 
                    //    IFNULL(p.webPrice4, 0)  webPrice4,IFNULL(p.webPrice5, 0)  webPrice5
                    //    FROM Barcode b
                    //          LEFT JOIN Product p
                    //              ON b.Product = p.product
                    //              AND p.Shop = '{0}'
                    //          LEFT JOIN Category c
                    //              ON p.Category = c.Category
                    //              AND p.Shop = c.Shop
                    //          LEFT JOIN Brand bb
                    //              ON p.Brand = bb.Brand
                    //          LEFT JOIN (
                    //              SELECT Product, AVG(Cost+OperationCost) Cost, COUNT(*) ProductCount FROM Barcode WHERE ReceivedDate IS NOT NULL AND (SellNo = '' OR SellNo IS NULL) GROUP BY Product
                    //          ) cnt
                    //    ON p.Product = cnt.Product
                    //    WHERE (p.product LIKE '%{1}%' OR p.Name LIKE '%{1}%') {2} {3} {4} {5}
                    //      ORDER BY p.Category, p.Name", Param.ShopId, txtSearch.Text.Trim(),
                    //            (cbbCategory.SelectedIndex != 0) ? "AND c.Name = '" + cbbCategory.SelectedItem.ToString() + "'" : "",
                    //            (cbbBrand.SelectedIndex != 0) ? "AND b.Name = '" + cbbBrand.SelectedItem.ToString() + "'" : "",
                    //            (cbNoPrice.Checked) ? "AND p.Price = 0" : "",
                    //            (cbNoStock.Checked) ? "AND IFNULL(cnt.ProductCount, 0) = 0" : ""
                    //        ));
                    //}
                    //else
                    //{

                    //      _TABLE_PRODUCT = Util.DBQuery(string.Format(@"SELECT product, name, category, brand, image, sku,warranty,IFNULL((SUM(cost)+SUM(OperationCost))/SUM(qty),0) cost, price, price1, price2, price3, price4, price5, webprice, webprice1, webprice2, webprice3, webprice4, webprice5, qty FROM (
                    //      SELECT DISTINCT p.product, p.Name, c.Name Category, bb.name Brand, p.Image, p.sku,  IFNULL(p.Warranty, 0)  Warranty, IFNULL(cnt.Cost,0) Cost,  IFNULL(cnt.OperationCost,0) OperationCost, 
                    //                          IFNULL(p.Price, 0) Price, IFNULL(p.Price1, 0)  Price1,IFNULL(p.Price2, 0)  Price2, IFNULL(p.Price3, 0) Price3, IFNULL(p.Price4, 0)  Price4, IFNULL(p.Price5, 0)  Price5,
                    //                          IFNULL(p.webPrice, 0) webPrice, IFNULL(p.webPrice1, 0)  webPrice1,IFNULL(p.webPrice2, 0)  webPrice2, IFNULL(p.webPrice3, 0) webPrice3, IFNULL(p.webPrice4, 0)  webPrice4, IFNULL(p.webPrice5, 0)  webPrice5, IFNULL(cnt.ProductCount,0) Qty
                    //                          FROM Barcode b
                    //                              LEFT JOIN Product p
                    //                                  ON b.Product = p.Product
                    //                              LEFT JOIN Category c
                    //                                  ON p.Category = c.Category
                    //                              LEFT JOIN Brand bb
                    //                                  ON p.brand = bb.brand
                    //                              LEFT JOIN (
                    //                                  SELECT Product, SUM(Cost) Cost, SUM(OperationCost) OperationCost, COUNT(*) ProductCount FROM Barcode WHERE ReceivedDate IS NOT NULL AND  sellPrice = 0 GROUP BY Product
                    //                              ) cnt
                    //                    ON p.product = cnt.Product
                    //      UNION ALL
                    //      SELECT DISTINCT p.product, p.Name, c.Name Category, b.Name Brand,p.Image, p.sku,  IFNULL(p.Warranty, 0)  Warranty,   IFNULL(b.PriceCost * cntP.Quantity ,0) Cost, 0 OperationCost,
                    //                          IFNULL(p.Price, 0) Price, IFNULL(p.Price1, 0)  Price1,IFNULL(p.Price2, 0)  Price2, IFNULL(p.Price3, 0)  Price3,IFNULL(p.Price4, 0)  Price4, IFNULL(p.Price5, 0)  Price5,
                    //                          IFNULL(p.WebPrice,0) WebPrice, IFNULL(p.WebPrice1,0) WebPrice1, IFNULL(p.WebPrice2,0) WebPrice2, IFNULL(p.WebPrice3,0) WebPrice3, IFNULL(p.WebPrice4,0) WebPrice4, IFNULL(p.WebPrice5,0) WebPrice5, IFNULL(cntP.Quantity,0) ProductCount                    
                    //                          FROM PurchaseOrder b
                    //                              LEFT JOIN Product  p
                    //                                  ON b.Product = p.product
                    //                              LEFT JOIN Category c
                    //                                  ON p.Category = c.Category 
                    //                              LEFT JOIN Brand b
                    //                                  ON p.Brand = b.Brand 
                    //                              LEFT JOIN 
                    //                              (
                    //                                  SELECT Product, Quantity, Name, Cost FROM Product
                    //                              ) cntP
                    //                              ON cntP.Product = p.product
                    //      )

                    //WHERE (product LIKE '%{1}%' OR Name LIKE '%{1}%') {2} {3} {4} {5}
                    //      GROUP BY product 
                    //      ORDER BY Category, Name", Param.ShopId, txtSearch.Text.Trim(),
                    //         (cbbCategory.SelectedIndex != 0) ? "AND category = '" + cbbCategory.SelectedItem.ToString() + "'" : "",
                    //         (cbbBrand.SelectedIndex != 0) ? "AND brand = '" + cbbBrand.SelectedItem.ToString() + "'" : "",
                    //         (cbNoPrice.Checked) ? "AND (Price = 0 OR Price = '' OR Price = null)" : "",
                    //         (cbNoStock.Checked) ? "AND IFNULL(Qty, 0) = 0" : ""
                    //     ));


                    _TABLE_PRODUCT = Util.DBQuery(string.Format(@"SELECT DISTINCT p.product, p.Name, c.Name Category, bb.name Brand, p.Image, p.sku,  IFNULL(p.Warranty, 0)  Warranty, IFNULL(cnt.Cost, 0) Cost,  IFNULL(cnt.OperationCost, 0) OperationCost, 
                                        IFNULL(p.Price, 0) Price, IFNULL(p.Price1, 0)  Price1,IFNULL(p.Price2, 0)  Price2, IFNULL(p.Price3, 0) Price3, IFNULL(p.Price4, 0)  Price4, IFNULL(p.Price5, 0)  Price5,
                                        IFNULL(p.webPrice, 0) webPrice, IFNULL(p.webPrice1, 0)  webPrice1,IFNULL(p.webPrice2, 0)  webPrice2, IFNULL(p.webPrice3, 0) webPrice3, IFNULL(p.webPrice4, 0)  webPrice4, IFNULL(p.webPrice5, 0)  webPrice5, IFNULL(cnt.ProductCount, 0) Qty
                                         FROM Barcode b
                                            LEFT JOIN Product p
                                                ON b.Product = p.Product
                                            LEFT JOIN Category c
                                                ON p.Category = c.Category
                                            LEFT JOIN Brand bb
                                                ON p.brand = bb.brand
                                            LEFT JOIN (
                                                SELECT Product, Cost, OperationCost, COUNT(*) ProductCount FROM Barcode WHERE ReceivedDate IS NOT NULL AND  sellNo = '' GROUP BY Product
                                            ) cnt
                                        ON p.product = cnt.Product
                    WHERE (p.product LIKE '%{1}%' OR p.Name LIKE '%{1}%') {2} {3} {4} {5}
                          GROUP BY p.product 
                          ORDER BY p.Category, p.Name", Param.ShopId, txtSearch.Text.Trim(),
                             (cbbCategory.SelectedIndex != 0) ? "AND c.Name  = '" + cbbCategory.SelectedItem.ToString() + "'" : "",
                             (cbbBrand.SelectedIndex != 0) ? "AND b.Name = '" + cbbBrand.SelectedItem.ToString() + "'" : "",
                             (cbNoPrice.Checked) ? "AND (p.Price = 0 OR p.Price = '' OR p.Price = null)" : "",
                             (cbNoStock.Checked) ? "AND IFNULL(cnt.ProductCount, 0) = 0" : ""
                    ));
                    //}
                    productGridview.OptionsBehavior.AutoPopulateColumns = false;
                    productGridControl.MainView = productGridview;

                    dt = new DataTable();
                    for (i = 0; i < ((ColumnView)productGridControl.MainView).Columns.Count; i++)
                    {
                        dt.Columns.Add(productGridview.Columns[i].FieldName);
                    }

                    for (a = 0; a < _TABLE_PRODUCT.Rows.Count; a++)
                    {
                        int warranty = int.Parse(_TABLE_PRODUCT.Rows[a]["Warranty"].ToString());
                        row = dt.NewRow();
                        row[0] = (a + 1) * 1;
                        row[1] = _TABLE_PRODUCT.Rows[a]["product"].ToString();
                        row[2] = _TABLE_PRODUCT.Rows[a]["Sku"].ToString();
                        row[3] = _TABLE_PRODUCT.Rows[a]["Name"].ToString();
                        row[4] = Convert.ToInt32(_TABLE_PRODUCT.Rows[a]["Qty"]).ToString("#,##0");
                        row[5] = _TABLE_PRODUCT.Rows[a]["Category"].ToString();
                        row[6] = _TABLE_PRODUCT.Rows[a]["Brand"].ToString();
                        row[7] = (warranty == 365) ? "1 ปี" : ((warranty == 0) ? "-" : ((warranty % 30 == 0) ? warranty / 30 + " เดือน" : warranty + " วัน"));
                        row[8] = Convert.ToDouble(_TABLE_PRODUCT.Rows[a]["cost"]).ToString("#,##0");
                        row[9] = Convert.ToDouble(_TABLE_PRODUCT.Rows[a]["Price"]).ToString("#,##0");
                        row[10] = Convert.ToDouble(_TABLE_PRODUCT.Rows[a]["Price1"]).ToString("#,##0");
                        row[11] = Convert.ToDouble(_TABLE_PRODUCT.Rows[a]["Price2"]).ToString("#,##0");
                        row[12] = Convert.ToDouble(_TABLE_PRODUCT.Rows[a]["Price3"]).ToString("#,##0");
                        row[13] = Convert.ToDouble(_TABLE_PRODUCT.Rows[a]["Price4"]).ToString("#,##0");
                        row[14] = Convert.ToDouble(_TABLE_PRODUCT.Rows[a]["Price5"]).ToString("#,##0");
                        row[15] = _TABLE_PRODUCT.Rows[a]["Image"].ToString();                        
                        row[16] = Convert.ToDouble(_TABLE_PRODUCT.Rows[a]["webPrice"]).ToString("#,##0");
                        row[17] = Convert.ToDouble(_TABLE_PRODUCT.Rows[a]["webPrice1"]).ToString("#,##0");
                        row[18] = Convert.ToDouble(_TABLE_PRODUCT.Rows[a]["webPrice2"]).ToString("#,##0");
                        row[19] = Convert.ToDouble(_TABLE_PRODUCT.Rows[a]["webPrice3"]).ToString("#,##0");
                        row[20] = Convert.ToDouble(_TABLE_PRODUCT.Rows[a]["webPrice4"]).ToString("#,##0");
                        row[21] = Convert.ToDouble(_TABLE_PRODUCT.Rows[a]["webPrice5"]).ToString("#,##0");
                        dt.Rows.Add(row);
                        double cost = double.Parse(_TABLE_PRODUCT.Rows[a]["cost"].ToString()) * int.Parse(_TABLE_PRODUCT.Rows[a]["Qty"].ToString());
                        //Console.WriteLine(cost.ToString());
                        _TOTAL += double.Parse(_TABLE_PRODUCT.Rows[a]["cost"].ToString());
                        _QTY += int.Parse(_TABLE_PRODUCT.Rows[a]["Qty"].ToString());
                        _VAL += cost;
                    }

                    productGridControl.DataSource = dt;

                    //var val = _QTY * _TOTAL;
                    lblListCount.Text = productGridview.RowCount.ToString("#,##0") + " รายการ";
                    lblTotal.Text = _VAL.ToString("#,##0.00") + " บาท";
                    lblProductCount.Text = _QTY.ToString("#,##0") + " ชิ้น";

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            //table1.EndUpdate();

            //lblRecords.Text = dt.Rows.Count.ToString("#,##0");
            //}
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (!_FIRST_LOAD)
            {
                if (productGridview.RowCount > 0)
                {
                    try
                    {
                        _ROW_INDEX = Convert.ToInt32(productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["No"]));
                        pnlPrice.Visible = true;
                        ptbProduct.Image = null;
                        //ptbProduct.Image = null;
                        Param.ProductId = productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["product"]);
                        Param.CategoryName = productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["category"]);
                        lblCategory.Text = Param.CategoryName;

                        var filename = @"Resource/Images/Product/" + Param.ProductId + ".jpg";
                        DataTable dt = Util.DBQuery(string.Format("SELECT Image FROM Product WHERE product = '{0}'", Param.ProductId));

                        _STREAM_IMAGE_URL = Param.ImagePath + "/" + productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["Sku"]) + "/" + productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["Image"]).Split(',')[0];

                        if (!File.Exists(filename))
                        {
                            if (dt.Rows.Count > 0 && dt.Rows[0]["Image"].ToString() != "")
                            {
                                DownloadImage(_STREAM_IMAGE_URL, @"Resource/Images/Product/", Param.ProductId + ".jpg");
                            }
                        }
                        else
                        {
                            try { ptbProduct.Image = Image.FromFile(filename); }
                            catch
                            {
                                if (dt.Rows.Count > 0 && dt.Rows[0]["Image"].ToString() != "")
                                {
                                    DownloadImage(_STREAM_IMAGE_URL, @"Resource/Images/Product/", Param.ProductId + ".jpg");
                                }
                            }
                        }

                        lblCost.Text = Convert.ToDouble(productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["cost"])).ToString("#,##0.00");
                        txtPrice.Text = productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["webPrice"]).ToString();
                        txtPrice1.Text = productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["webPrice1"]).ToString();
                        txtPrice2.Text = productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["webPrice2"]).ToString();
                        txtPrice3.Text = productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["webPrice3"]).ToString();
                        txtPrice4.Text = productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["webPrice4"]).ToString();
                        nudPrice.Value = Convert.ToDecimal(productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["price"]));
                        nudPrice1.Value = Convert.ToDecimal(productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["price1"]));
                        nudPrice2.Value = Convert.ToDecimal(productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["price2"]));
                        nudPrice3.Value = Convert.ToDecimal(productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["price3"]));
                        nudPrice4.Value = Convert.ToDecimal(productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["price4"]));
                        nudPrice_ValueChanged(sender, e);
                        nudPrice1_ValueChanged(sender, e);
                        nudPrice2_ValueChanged(sender, e);
                        nudPrice3_ValueChanged(sender, e);
                        nudPrice4_ValueChanged(sender, e);
                        btnSave.Enabled = false;
                        lblCategory.Text = productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["category"]);
                        id = productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["product"]);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        pnlPrice.Visible = false;
                    }
                }
            }

            //if (productGridview.RowCount > 0)
            //{
            //    try
            //    {
            //        _ROW_INDEX = Convert.ToInt32(productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["No"]));
            //        pnlPrice.Visible = true;
            //        ptbProduct.Image = null;
            //        Param.ProductId = productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["รหัสสินค้า"]);
            //        Param.CategoryName = productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["ประเภท"]);
            //        lblCategory.Text = Param.CategoryName;

            //        //var filename = @"Resource/Images/Product/" + Param.ProductId + ".jpg";
            //        //DataTable dt = Util.DBQuery(string.Format("SELECT CoverImage FROM Product WHERE ID = '{0}' AND Shop = {1}", Param.ProductId, Param.ShopParent));


            //        //if (!File.Exists(filename))
            //        //{
            //        //    if (dt.Rows.Count > 0 && dt.Rows[0]["CoverImage"].ToString() != "")
            //        //    {
            //        //        DownloadImage(dt.Rows[0]["CoverImage"].ToString(), @"Resource/Images/Product/", Param.ProductId + ".jpg");
            //        //    }
            //        //}
            //        //else
            //        //{
            //        //    try { ptbProduct.Image = Image.FromFile(filename); }
            //        //    catch
            //        //    {
            //        //        if (dt.Rows.Count > 0 && dt.Rows[0]["CoverImage"].ToString() != "")
            //        //        {
            //        //            DownloadImage(dt.Rows[0]["CoverImage"].ToString(), @"Resource/Images/Product/", Param.ProductId + ".jpg");
            //        //        }
            //        //    }
            //        //}


            //        lblCost.Text = double.Parse(productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["ต้นทุน"])).ToString("#,##0.00");
            //        txtPrice.Text = int.Parse(productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["ราคาปลีก"])).ToString("#,##0");
            //        txtPrice1.Text = int.Parse(productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["ราคาส่ง"])).ToString("#,##0");
            //        txtPrice2.Text = int.Parse(productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["ราคาส่ง2"])).ToString("#,##0");
            //        nudPrice.Value = int.Parse(productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["ราคาปลีก"]));
            //        nudPrice1.Value = int.Parse(productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["ราคาส่ง"]));
            //        nudPrice2.Value = int.Parse(productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["ราคาส่ง2"]));
            //        nudPrice_ValueChanged(sender, e);
            //        nudPrice1_ValueChanged(sender, e);
            //        nudPrice2_ValueChanged(sender, e);
            //        btnSave.Enabled = false;
            //        lblCategory.Text = productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["ประเภท"]);
            //        id = productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["รหัสสินค้า"]);

            //    }
            //    catch
            //    {
            //        pnlPrice.Visible = false;
            //    }
            //}
        }

        private void nudPrice_ValueChanged(object sender, EventArgs e)
        {
            var percent = (((double)nudPrice.Value * 1.00 / double.Parse(lblCost.Text.Replace(",", "")) * 100) - 100);
            txtPercent.Text = ((double)nudPrice.Value == 0 || lblCost.Text == "0.00") ? "∞" : percent.ToString("#,##0.00");
            nudPrice.ForeColor = percent < 0 ? Color.Red : Color.Gray;
            btnSave.Enabled = true;

           
        }

        private void nudPrice1_ValueChanged(object sender, EventArgs e)
        {
            var percent = (((double)nudPrice1.Value * 1.00 / double.Parse(lblCost.Text.Replace(",", "")) * 100) - 100);
            txtPercent1.Text = ((double)nudPrice1.Value == 0 || lblCost.Text == "0.00") ? "∞" : percent.ToString("#,##0.00");
            nudPrice1.ForeColor = percent < 0 ? Color.Red : Color.Gray;
            btnSave.Enabled = true;
        }

        private void nudPrice2_ValueChanged(object sender, EventArgs e)
        {
            var percent = (((double)nudPrice2.Value * 1.00 / double.Parse(lblCost.Text.Replace(",", "")) * 100) - 100);
            txtPercent2.Text = ((double)nudPrice2.Value == 0 || lblCost.Text == "0.00") ? "∞" : percent.ToString("#,##0.00");
            nudPrice2.ForeColor = percent < 0 ? Color.Red : Color.Gray;
            btnSave.Enabled = true;
        }

        private void nudPrice3_ValueChanged(object sender, EventArgs e)
        {
            var percent = (((double)nudPrice3.Value * 1.00 / double.Parse(lblCost.Text.Replace(",", "")) * 100) - 100);
            txtPercent3.Text = ((double)nudPrice3.Value == 0 || lblCost.Text == "0.00") ? "∞" : percent.ToString("#,##0.00");
            nudPrice3.ForeColor = percent < 0 ? Color.Red : Color.Gray;
            btnSave.Enabled = true;
        }

        private void nudPrice4_ValueChanged(object sender, EventArgs e)
        {
            var percent = (((double)nudPrice4.Value * 1.00 / double.Parse(lblCost.Text.Replace(",", "")) * 100) - 100);
            txtPercent4.Text = ((double)nudPrice4.Value == 0 || lblCost.Text == "0.00") ? "∞" : percent.ToString("#,##0.00");
            nudPrice4.ForeColor = percent < 0 ? Color.Red : Color.Gray;
            btnSave.Enabled = true;
        }

        private void btnUseWebPrice_Click(object sender, EventArgs e)
        {
            nudPrice.Value = int.Parse(txtPrice.Text.Replace(",", ""));
            nudPrice1.Value = int.Parse(txtPrice1.Text.Replace(",", ""));
            nudPrice2.Value = int.Parse(txtPrice2.Text.Replace(",", ""));
            nudPrice3.Value = int.Parse(txtPrice3.Text.Replace(",", ""));
            nudPrice4.Value = int.Parse(txtPrice4.Text.Replace(",", ""));
        }

        private void btnUsePercentPrice_Click(object sender, EventArgs e)
        {
            DataTable dt = Util.DBQuery(@"SELECT IFNULL(Price,0) Price, IFNULL(Price1,0) Price1, IFNULL(Price2,0) Price2 FROM Category c LEFT JOIN CategoryProfit p ON c.Category  = p.Category  WHERE LOWER(name) = '" + lblCategory.Text.ToLower() + "'");
            if (double.Parse(dt.Rows[0]["Price"].ToString()) == 0)
            {
                btnConfig_Click(sender, e);
            }
            else
            {
                nudPrice.Value = (int)Math.Ceiling((100 + double.Parse(dt.Rows[0]["Price"].ToString())) * double.Parse(lblCost.Text.Replace(",", "")) / 100);
                nudPrice1.Value = (int)Math.Ceiling((100 + double.Parse(dt.Rows[0]["Price1"].ToString())) * double.Parse(lblCost.Text.Replace(",", "")) / 100);
                nudPrice2.Value = (int)Math.Ceiling((100 + double.Parse(dt.Rows[0]["Price2"].ToString())) * double.Parse(lblCost.Text.Replace(",", "")) / 100);
                nudPrice3.Value = (int)Math.Ceiling((100 + double.Parse(dt.Rows[0]["Price3"].ToString())) * double.Parse(lblCost.Text.Replace(",", "")) / 100);
                nudPrice4.Value = (int)Math.Ceiling((100 + double.Parse(dt.Rows[0]["Price4"].ToString())) * double.Parse(lblCost.Text.Replace(",", "")) / 100);
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            Param.CategoryName = lblCategory.Text;
            FmProfitConfig ul = new FmProfitConfig();
            var result = ul.ShowDialog(this);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                DataTable dt;
                dt = Util.DBQuery(string.Format(@"SELECT Barcode FROM Barcode WHERE Barcode = '" + txtSearch.Text.Trim() + "'"));
                if (dt.Rows.Count > 0)
                {
                    barcode = dt.Rows[0]["Barcode"].ToString();
                    //FmBarcode frm = new FmBarcode();
                    //frm.Show();
                    txtSearch.Text = "";
                }
                else
                {
                    SearchData();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณแน่ใจหรือไม่ ที่จะกำหนดสินค้านี้ ?", "ยืนยันข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Util.DBExecute(string.Format(@"UPDATE Product SET Cost = '" + lblCost.Text + "' , Price = '{2}', Price1 = '{3}', Price2 = '{4}',Price3 = '{5}',Price4 = '{6}',Sync = 1 WHERE product = '{0}' AND shop = '{1}'", id, Param.ShopId, nudPrice.Value.ToString(), nudPrice1.Value.ToString(), nudPrice2.Value.ToString(), nudPrice3.Value.ToString(), nudPrice4.Value.ToString()));
                SearchData();
            }
        }

        private void productGridview_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (!_FIRST_LOAD)
            {
                if (productGridview.RowCount > 0)
                {
                    try
                    {
                        _ROW_INDEX = Convert.ToInt32(productGridview.GetDataRow(e.FocusedRowHandle)["No"].ToString());
                        pnlPrice.Visible = true;
                        ptbProduct.Image = null;
                        //ptbProduct.Image = null;
                        Param.ProductId = productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["product"]);
                        Param.CategoryName = productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["category"]);
                        lblCategory.Text = Param.CategoryName;

                        var filename = @"Resource/Images/Product/" + Param.ProductId + ".jpg";
                        DataTable dt = Util.DBQuery(string.Format("SELECT Image FROM Product WHERE product = '{0}'", Param.ProductId));

                        _STREAM_IMAGE_URL = Param.ImagePath + "/" + productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["Sku"]) + "/" + productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["Image"]).Split(',')[0];

                        if (!File.Exists(filename))
                        {
                            if (dt.Rows.Count > 0 && dt.Rows[0]["Image"].ToString() != "")
                            {
                                DownloadImage(_STREAM_IMAGE_URL, @"Resource/Images/Product/", Param.ProductId + ".jpg");
                            }
                        }
                        else
                        {
                            try { ptbProduct.Image = Image.FromFile(filename); }
                            catch
                            {
                                if (dt.Rows.Count > 0 && dt.Rows[0]["Image"].ToString() != "")
                                {
                                    DownloadImage(_STREAM_IMAGE_URL, @"Resource/Images/Product/", Param.ProductId + ".jpg");
                                }
                            }
                        }

                        lblCost.Text = Convert.ToDouble(productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["cost"])).ToString("#,##0.00");
                        txtPrice.Text = productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["webPrice"]).ToString();
                        txtPrice1.Text = productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["webPrice1"]).ToString();
                        txtPrice2.Text = productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["webPrice2"]).ToString();
                        txtPrice3.Text = productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["webPrice3"]).ToString();
                        txtPrice4.Text = productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["webPrice4"]).ToString();
                        nudPrice.Value = Convert.ToDecimal(productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["price"]));
                        nudPrice1.Value = Convert.ToDecimal(productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["price1"]));
                        nudPrice2.Value = Convert.ToDecimal(productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["price2"]));
                        nudPrice3.Value = Convert.ToDecimal(productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["price3"]));
                        nudPrice4.Value = Convert.ToDecimal(productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["price4"]));
                        nudPrice_ValueChanged(sender, e);
                        nudPrice1_ValueChanged(sender, e);
                        nudPrice2_ValueChanged(sender, e);
                        nudPrice3_ValueChanged(sender, e);
                        nudPrice4_ValueChanged(sender, e);
                        btnSave.Enabled = false;
                        lblCategory.Text = productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["category"]);
                        id = productGridview.GetRowCellDisplayText(productGridview.FocusedRowHandle, productGridview.Columns["product"]);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        pnlPrice.Visible = false;
                    }
                }
            }
        }

        private void DownloadImage(string url, string savePath, string fileName)
        {
            ptbProduct.ImageLocation = url;
            //ptbProduct.Image = Image.FromFile(url);
            DownloadImage d = new DownloadImage();
            Thread thread = new Thread(() => d.Download(url, savePath, fileName));
            thread.Start();
        }
    }

  
}
