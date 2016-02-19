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
using System.Globalization;
using System.IO;
using System.Threading;

namespace PowerPOS
{
    public partial class UcSale : DevExpress.XtraEditors.XtraUserControl
    {
        DataTable _TABLE_SALE,_TABLE_RETURN;
        private string barcode;
        string _STREAM_IMAGE_URL;
        bool  _CHANGE = false;
        public static string product, price;


        public UcSale()
        {
            InitializeComponent();
        }

        private void SetActiveButton(CheckButton button)
        {
            button.Appearance.ForeColor = System.Drawing.Color.Green;
            //button.Checked = true;
        }

        private void SetDefaultButton(CheckButton button)
        {
            button.Appearance.ForeColor = System.Drawing.Color.DarkGray;
            button.Checked = false;
        }

        private void btnAge_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckButton)sender).Checked)
            {
                SetDefaultButton(btnAge1);
                SetDefaultButton(btnAge2);
                SetDefaultButton(btnAge3);
                SetDefaultButton(btnAge4);
                SetDefaultButton(btnAge5);
                SetDefaultButton(btnAge6);
            }
            SetActiveButton((CheckButton)sender);
        }

        private void btnGender_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckButton)sender).Checked)
            {
                
                SetDefaultButton(btnMale);
                SetDefaultButton(btnFemale);
            }
            SetActiveButton((CheckButton)sender);
        }

        private void UcSale_Load(object sender, EventArgs e)
        {
            Param.SelectCustomerId = "000000";
            Param.SelectCustomerName = "ลูกค้าทั่วไป";
            Param.SelectCustomerSex = "";
            Param.SelectCustomerAge = 0;
            Param.SelectCustomerSellPrice = 0;
            Param.UcSale = this;
            LoadData();
            txtBarcode.Focus();
            //SelectCustomer(sender, e);
        }

        public void LoadData()
        {
            DataTable dt;
            DataRow row;
            int i, a;
            int _QTY = 0;

            dt = Util.DBQuery(string.Format(@"SELECT * FROM Barcode b
                        WHERE NOT EXISTS (SELECT *
                          FROM ChangePrice cp
                          WHERE b.product = cp.product
                          AND cp.SellNo = '{0}')
                        AND b.SellBy = '{0}'", Param.DeviceID));
            if (dt.Rows.Count > 0)
            {
                Util.DBExecute(string.Format(@"UPDATE Barcode SET SellPrice = (SELECT p.Price{3} FROM Product p
                            WHERE Barcode.product = p.product AND p.shop = '{2}'), Sync = 1 WHERE SellBy = '{0}'", Param.DeviceID, barcode, Param.ShopId, Param.SelectCustomerSellPrice == 0 ? "" : "" + Param.SelectCustomerSellPrice));
            }

            dt = Util.DBQuery(string.Format(@"SELECT product, productName, price, amount FROM sellTemp st
                    WHERE NOT EXISTS (SELECT *
                      FROM ChangePrice cp
                      WHERE st.product = cp.product
                      AND cp.SellNo = '{0}')", Param.DeviceID));
            if (dt.Rows.Count > 0)
            {

                Util.DBExecute(string.Format(@"UPDATE sellTemp  SET
                    Price = (SELECT p.Price{0} FROM Product p
                    WHERE sellTemp.product = p.product),
                    TotalPrice =  (SELECT p.Price{0} FROM Product p
                    WHERE sellTemp.product = p.product) * sellTemp.Amount,
                    PriceCost = (SELECT p.Cost FROM Product p
                    WHERE sellTemp.product = p.Product) * sellTemp.Amount",
                    Param.SelectCustomerSellPrice == 0 ? "" : "" + Param.SelectCustomerSellPrice, price));
            }

            _TABLE_SALE = Util.DBQuery(string.Format(@"SELECT product, name, Price, SUM (ProductCount) ProductCount , Price *  SUM (ProductCount) TotalPrice FROM 
                        (SELECT p.product, p.Name, p.Price{2} PriceA, IFNULL(cp.priceChange, p.Price{2}) Price, ProductCount
                        FROM (SELECT Product, COUNT(*) ProductCount, SellPrice FROM Barcode WHERE SellBy = '{0}' GROUP BY Product) b 
                            LEFT JOIN Product p 
                            ON b.Product = p.product
                        LEFT JOIN ChangePrice cp
                        ON cp.product = p.product
                        AND cp.SellNo = '{0}'
                    UNION ALL
                    SELECT product, productName, price,  price priceA, amount FROM sellTemp)
                    GROUP BY product", Param.DeviceID, Param.ShopId, Param.SelectCustomerSellPrice == 0 ? "" : "" + Param.SelectCustomerSellPrice));

            var sumPrice = 0;

            productGridView.OptionsBehavior.AutoPopulateColumns = false;
            productGridControl.MainView = productGridView;

            dt = new DataTable();
            for (i = 0; i < ((ColumnView)productGridControl.MainView).Columns.Count; i++)
            {
                dt.Columns.Add(productGridView.Columns[i].FieldName);
            }

            if (_TABLE_SALE.Rows.Count > 0)
            {
                for (a = 0; a < _TABLE_SALE.Rows.Count; a++)
                {
                    //var total = int.Parse(_TABLE_SALE.Rows[a]["ProductCount"].ToString()) * int.Parse(_TABLE_SALE.Rows[a]["Price"].ToString());
                    var total =  int.Parse(_TABLE_SALE.Rows[a]["TotalPrice"].ToString());

                    row = dt.NewRow();
                    row[0] = (a + 1) * 1;
                    row[1] = _TABLE_SALE.Rows[a]["product"].ToString();
                    row[2] = _TABLE_SALE.Rows[a]["Name"].ToString();
                    row[3] = Convert.ToInt32(_TABLE_SALE.Rows[a]["Price"].ToString()).ToString("#,##0");
                    row[4] = Convert.ToInt32(_TABLE_SALE.Rows[a]["ProductCount"]).ToString("#,##0");
                    row[5] = Convert.ToInt32(_TABLE_SALE.Rows[a]["TotalPrice"].ToString()).ToString("#,##0");
                    dt.Rows.Add(row);
                    _QTY += int.Parse(_TABLE_SALE.Rows[a]["ProductCount"].ToString());
                    sumPrice += total;
                }

                productGridControl.DataSource = dt;
                lblPrice.Text = sumPrice.ToString("#,##0");

            }
            else
            {
                productGridControl.DataSource = null;
            }

            productGridControl.DataSource = dt;
            lblPrice.Text = sumPrice.ToString("#,##0");
            lblListCount.Text = productGridView.RowCount.ToString() + " รายการ";
            lblProductCount.Text = _QTY.ToString() + " ชิ้น";

            btnCancelSale.Enabled = sumPrice > 0;
            btnCancelProduct.Enabled = sumPrice > 0;
            btnConfirm.Enabled = sumPrice > 0;
        }

        private void SelectCustomer(object sender, EventArgs e)
        {
            lblCustomerName.Text = Param.SelectCustomerName;
            if (Param.SelectCustomerSex != "")
            {
                if (Param.SelectCustomerSex == "M")
                {
                    SetActiveButton(btnMale);
                }
                else
                {
                    SetActiveButton(btnFemale);
                }              
                btnMale.Enabled = false;
                btnFemale.Enabled = false;
            }
            else
            {
                SetDefaultButton(btnMale);
                SetDefaultButton(btnFemale);
                btnMale.Enabled = true;
                btnFemale.Enabled = true;
            }

            if (Param.SelectCustomerAge != 0)
            {
                if (Param.SelectCustomerAge > 60)
                    SetActiveButton(btnAge6);
                else if (Param.SelectCustomerAge > 40)
                    SetActiveButton(btnAge5);
                else if (Param.SelectCustomerAge > 25)
                    SetActiveButton(btnAge4);
                else if (Param.SelectCustomerAge > 18)
                    SetActiveButton(btnAge3);
                else if (Param.SelectCustomerAge > 12)
                    SetActiveButton(btnAge2);
                else
                    SetActiveButton(btnAge1);
                btnAge1.Enabled = false;
                btnAge2.Enabled = false;
                btnAge3.Enabled = false;
                btnAge4.Enabled = false;
                btnAge5.Enabled = false;
                btnAge6.Enabled = false;
            }
            else
            {
                SetDefaultButton(btnAge1);
                SetDefaultButton(btnAge2);
                SetDefaultButton(btnAge3);
                SetDefaultButton(btnAge4);
                SetDefaultButton(btnAge5);
                SetDefaultButton(btnAge6);
                btnAge1.Enabled = true;
                btnAge2.Enabled = true;
                btnAge3.Enabled = true;
                btnAge4.Enabled = true;
                btnAge5.Enabled = true;
                btnAge6.Enabled = true;
            }
            LoadData();
        }

        private void btnCancelSale_Click(object sender, EventArgs e)
        {
            if (productGridView.RowCount > 0)
            {
                var confirm = MessageBox.Show("คุณแน่ใจหรือไม่\nที่จะยกเลิกรายการขายนี้", "ยืนยันการทำงาน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
                if (confirm)
                {
                    //DataTable dt = Util.DBQuery(@"SELECT Product FROM SellTemp");
                    //if (dt.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dt.Rows.Count; i++)
                    //    {
                    //        Util.DBExecute(string.Format(@"UPDATE Product SET Quantity = (SELECT st.Amount FROM SellTemp st WHERE st.Product = Product.Product ) + Quantity, Sync = 1  WHERE shop = '{0}' AND Product = '{1}'", Param.ShopId, dt.Rows[i][0].ToString()));
                    //    }
                    //}
                    Util.DBExecute(string.Format(@"UPDATE Barcode SET SellBy = '', SellPrice = '0',Sync = 1 WHERE SellBy = '{0}'", Param.DeviceID));
                    Util.DBExecute(string.Format(@"DELETE FROM ChangePrice WHERE SellNo = '{0}'", Param.DeviceID));
                    Util.DBExecute(string.Format(@"DELETE FROM SellTemp"));

                    lblStatus.Text = "";
                    LoadData();
                }
            }
        }

        private void btnCancelProduct_Click(object sender, EventArgs e)
        {
            if (productGridView.RowCount > 0)
            {
                FmCancelProduct frm = new FmCancelProduct();
                frm.Show();
                lblStatus.Visible = false;
            }
            else
            {
                MessageBox.Show("ไม่มีสินค้าในรายการขาย", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (productGridView.RowCount > 0)
            {
                if (MessageBox.Show("คูณต้องการ ยืนยันการขายนี้หรือไม่ ?", "ยืนยันข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataTable dt = Util.DBQuery(string.Format(@"SELECT IFNULL(SUBSTR(MAX(SellNo), 1, 5)||SUBSTR('0000'||(SUBSTR(MAX(SellNo), 6, 4)+1), -4, 4), SUBSTR(STRFTIME('%Y%m{0}'), 3)||'0001') SellNo
                    FROM SellHeader
                    WHERE SUBSTR(SellNo, 1, 4) = SUBSTR(STRFTIME('%Y%m'), 3, 4)
                    AND SUBSTR(SellNo, 5, 1) = '{0}'", Param.DevicePrefix));
                    var SellNo = dt.Rows[0]["SellNo"].ToString();

                    FmCashReceive dialog = new FmCashReceive();
                    dialog.lblPrice.Text = lblPrice.Text;
                    dialog.lblChange.Text = "0";
                    dialog._TOTAL = int.Parse(lblPrice.Text.Replace(",", ""));
                    dialog._SELL_NO = SellNo;
                    var result = dialog.ShowDialog(this);
                    if (result != DialogResult.OK)
                    {
                        //Util.DBExecute(string.Format(@"UPDATE SellHeader SET Cash = 0, PayType = 0, Paid = 0, Sync = 1 WHERE SellNo = '{0}'", SellNo));
                    }

                    Param.SelectCustomerId = "000000";
                    Param.SelectCustomerName = "ลูกค้าทั่วไป";
                    Param.SelectCustomerSex = "";
                    Param.SelectCustomerAge = 0;
                    Param.SelectCustomerSellPrice = 0;
                    lblStatus.Text = "";

                    //if (Param.PrintType == "Y")
                    //{
                    //    var cnt = int.Parse(Param.PrintCount.ToString());
                    //    for (int i = 1; i <= cnt; i++)
                    //        Util.PrintReceipt(SellNo);
                    //}
                    //else if (Param.PrintType == "A")
                    //{
                    //    if (MessageBox.Show("คุณต้องการพิมพ์ใบเสร็จรับเงินหรือไม่ ?", "การพิมพ์", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //        Util.PrintReceipt(SellNo);
                    //}

                    lblCustomerName.Text = "ลูกค้าทั่วไป";
                    Param.SelectCustomerSex = "";
                    Param.SelectCustomerAge = 0;
                    Param.SelectCustomerSellPrice = 0;
                    //SelectCustomer(sender, (e));
                    LoadData();
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Param.SelectCustomerId = "000000";
            Param.SelectCustomerName = "ลูกค้าทั่วไป";
            Param.SelectCustomerSex = "";
            Param.SelectCustomerAge = 0;
            Param.SelectCustomerSellPrice = 0;
            FmSelectCustomer ul = new FmSelectCustomer();
            var result = ul.ShowDialog(this);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                SelectCustomer(sender, e);
            }
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                productGridControl.Visible = true;
                returnGridControl.Visible = false;
                lblStatus.Visible = false;
                Param.BarcodeNo = txtBarcode.Text;

                if (e.KeyCode == Keys.Return)
                {
                    barcode = txtBarcode.Text;


                    DataTable dt = Util.DBQuery(string.Format(@"SELECT p.Name, p.Product, IFNULL(p.Price, 0) Price, IFNULL(p.Price1, 0) Price1, IFNULL(p.Price2, 0) Price2, ReceivedDate, ReceivedBy, SellDate, SellBy, Comment 
                    FROM Barcode b 
                        LEFT JOIN Product p 
                        ON b.Product = p.Product 
                    WHERE b.Barcode = '{0}'", txtBarcode.Text, Param.ShopId));
                    //lblStatus.Visible = true;

                    if (dt.Rows.Count == 0)
                    {
                        dt = Util.DBQuery(string.Format(@"SELECT Barcode FROM Product WHERE Barcode LIKE '%{0}%' OR Name LIKE '%{0}%'", txtBarcode.Text));
                        Console.WriteLine(txtBarcode.Text + "" + Param.BarcodeNo + "" + dt.Rows.Count.ToString());
                        if (dt.Rows.Count == 0)
                        {
                            dt = Util.DBQuery(string.Format(@"SELECT Product, Barcode FROM Product WHERE SKU LIKE '%{0}%'", txtBarcode.Text));
                            if (dt.Rows.Count == 0)
                            {
                                lblStatus.Visible = true;
                                lblStatus.Text = "ไม่พบข้อมูลสินค้าชิ้นนี้ในระบบ";
                                lblStatus.ForeColor = Color.Red;
                            }
                            else
                            {
                                Param.status = "Sell";
                                FmProductQty frm = new FmProductQty();
                                Param.product = dt.Rows[0]["Product"].ToString();
                                var result = frm.ShowDialog(this);
                                if (result == System.Windows.Forms.DialogResult.OK)
                                {
                                    LoadData();
                                }
                            }
                        }
                        else
                        {
                            Param.status = "Sell";
                            FmSelectProduct frm = new FmSelectProduct();
                            var result = frm.ShowDialog(this);
                            if (result == System.Windows.Forms.DialogResult.OK)
                            {
                                LoadData();
                            }
                        }
                    }
                    else if (dt.Rows[0]["ReceivedDate"].ToString() == "")
                    {
                        lblStatus.Visible = true;
                        lblStatus.Text = "สินค้าชิ้นนี้ยังไม่ได้รับเข้าระบบ";
                        lblStatus.ForeColor = Color.Red;
                        var confirm = MessageBox.Show("สินค้าชิ้นนี้ยังไม่ได้รับเข้าระบบ\nคุณต้องการไปที่หน้าจอรับสินค้าเข้าระบบหรือไม่ ?", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                        if (confirm)
                        {
                            Param.Main.AddPanel(Param.Screen.ReceiveProduct);
                            Param.SelectedScreen = (int)Param.Screen.ReceiveProduct;
                        }
                    }
                    else if (dt.Rows[0]["ReceivedBy"].ToString() == "")
                    {
                        lblStatus.Visible = true;
                        lblStatus.Text = "สินค้าชิ้นนี้ยังไม่ได้กำหนดต้นทุน";
                        lblStatus.ForeColor = Color.Red;
                        var confirm = MessageBox.Show("สินค้าชิ้นนี้ยังไม่ได้กำหนดต้นทุน\nคุณต้องการไปที่หน้าจอรับสินค้าเข้าระบบหรือไม่ ?", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                        if (confirm)
                        {
                            Param.Main.AddPanel(Param.Screen.ReceiveProduct);
                            Param.SelectedScreen = (int)Param.Screen.ReceiveProduct;
                        }
                    }
                    else if (dt.Rows[0]["SellDate"].ToString() != "")
                    {
                        lblStatus.Visible = true;
                        lblStatus.Text = "สินค้าชิ้นนี้ได้ขายออกจากระบบไปแล้ว";
                        lblStatus.ForeColor = Color.Red;
                    }
                    else if (dt.Rows[0]["SellBy"].ToString() == Param.DeviceID)
                    {
                        lblStatus.Visible = true;
                        lblStatus.Text = "มีสินค้าชิ้นนี้ในรายการขายแล้ว";
                        lblStatus.ForeColor = Color.Red;
                    }
                    else if (int.Parse(dt.Rows[0]["Price"].ToString()) == 0 || int.Parse(dt.Rows[0]["Price1"].ToString()) == 0)
                    {
                        lblStatus.Visible = true;
                        lblStatus.Text = "สินค้าชิ้นนี้ยังไม่ได้กำหนดราคาขาย";
                        lblStatus.ForeColor = Color.Red;
                        var confirm = MessageBox.Show("สินค้าชิ้นนี้ยังไม่ได้กำหนดราคาขาย\nคุณต้องการไปที่หน้าจอข้อมูลสินค้าหรือไม่ ?", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                        if (confirm)
                        {
                            Param.Main.AddPanel(Param.Screen.Product);
                            Param.SelectedScreen = (int)Param.Screen.Product;
                        }
                    }
                    else
                    {
                        //Util.DBExecute(string.Format(@"UPDATE Barcode SET SellBy = '{0}', Sync = 1 WHERE Barcode = '{1}'", Param.CpuId, txtBarcode.Text));
                        Util.DBExecute(string.Format(@"UPDATE Barcode SET SellPrice = (SELECT p.Price{3} FROM Product p WHERE Barcode.product = p.product AND p.shop = '{2}'),
                            SellBy = '{0}', Sync = 1 WHERE Barcode = '{1}'", Param.DeviceID, txtBarcode.Text, Param.ShopId, Param.SelectCustomerSellPrice == 0 ? "" : "" + Param.SelectCustomerSellPrice));
                        LoadData();
                        txtBarcode.Focus();
                        lblStatus.Text = "เพิ่มสินค้าในรายการขายแล้ว";
                        lblStatus.ForeColor = Color.Green;
                    }
                    lblStatus.Visible = false;
                    txtBarcode.Text = "";
                }
                else if (e.KeyCode == Keys.F1)
                {
                    btnConfirm_Click(sender, (e));
                }
                else if (e.KeyCode == Keys.F11)
                {
                    btnCancelProduct_Click(sender, (e));
                }
                else if (e.KeyCode == Keys.F12)
                {
                    btnCancelSale_Click(sender, (e));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            DataTable dt;

            DataTable RId = Util.DBQuery(string.Format(@"SELECT IFNULL(SUBSTR(MAX(barcode), 1,6)||SUBSTR('0000'||(SUBSTR(MAX(barcode), 7, 4)+1), -4, 4), SUBSTR(STRFTIME('%Y%m{0}R'), 3)||'0001') Return
                                            FROM ReturnProduct
                                            WHERE SUBSTR(barcode, 1, 4) = SUBSTR(STRFTIME('%Y%m'), 3, 4)
                                            AND SUBSTR(barcode, 5, 1) = '{0}'", Param.DevicePrefix));
            var Return = RId.Rows[0]["Return"].ToString();

            dt = Util.DBQuery(string.Format(@"SELECT b.OrderNo, p.product, p.Image, IFNULL(b.ReceivedDate, '') ReceivedDate 
                    FROM Barcode b LEFT JOIN Product p ON b.product = p.product WHERE b.Barcode = '{0}'", txtBarcodeReturn.Text));
            if (dt.Rows.Count == 0)
            {
                if (MessageBox.Show("คุณแน่ใจหรือไม่ ที่จะยืนยันการรับคืนสินค้านี้ ?", "ยืนยันข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string Bar = Return + "-" + Param.product;
                    Util.DBExecute(string.Format(@"INSERT INTO ReturnProduct (ReturnNo, SellNo, ReturnDate, Product, Barcode, SellPrice, Quantity, ReturnBy, Sync)
                                             SELECT '{0}', '{1}', STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), '{2}', '{3}', '{4}', '{5}', '{6}', 1 ",
                       Return, FmReturnSell.sellN, FmReturnSell.Pid, Bar, FmReturnSell.sellP, Param.amount, Param.UserId));

                    DataTable price = Util.DBQuery(string.Format(@"SELECT SellPrice, Cost, Quantity FROM SellDetail WHERE SellNo = '{0}' AND Product = '{1}'", FmReturnSell.sellN, FmReturnSell.Pid));

                    var sellPrice = Convert.ToInt32(price.Rows[0]["SellPrice"].ToString()) - (Convert.ToInt32(Param.amount) * Convert.ToInt32(FmReturnSell.sellP));
                    var amount = Convert.ToInt32(price.Rows[0]["Quantity"].ToString()) - Convert.ToInt32(Param.amount);
                    var costT = Convert.ToInt32(price.Rows[0]["Cost"].ToString()) - (Convert.ToInt32(Param.amount) * Convert.ToInt32(FmReturnSell.costPrice));

                    Util.DBExecute(string.Format(@"UPDATE SellDetail SET SellPrice = '{2}', Cost = '{4}', Quantity = '{3}',Sync = 1 WHERE SellNo = '{0}' AND Product = '{1}'",
                    FmReturnSell.sellN, FmReturnSell.Pid, sellPrice, amount, costT));

                    Util.DBExecute(string.Format(@"UPDATE SellHeader SET Profit = (SELECT SUM(SellPrice-Cost) FROM SellDetail WHERE SellNo = '{0}')
                                            , TotalPrice = (SELECT SUM(SellPrice) FROM SellDetail WHERE SellNo = '{0}') ,Sync = 1 WHERE SellNo = '{0}'", FmReturnSell.sellN));

                    DataTable dtap = Util.DBQuery(string.Format(@"SELECT * FROM SellHeader WHERE SellNo = '{0}'", FmReturnSell.sellN));

                    if (dtap.Rows[0]["TotalPrice"].ToString() == "0" || dtap.Rows[0]["TotalPrice"].ToString() == "")
                    {
                        Util.DBExecute(string.Format(@"UPDATE SellHeader SET Comment = 'คืนสินค้า' ,Sync = 1 WHERE SellNo = '{0}'", FmReturnSell.sellN));
                        Util.DBExecute(string.Format(@"UPDATE SellDetail SET Comment = 'คืนสินค้า' ,Sync = 1 WHERE SellNo = '{0}'", FmReturnSell.sellN));

                    }

                    dtap = Util.DBQuery(string.Format(@"SELECT SellNo, Product, Quantity FROM SellDetail WHERE SellNo = '{0}'", FmReturnSell.sellN));
                    for (int i = 0; i < dtap.Rows.Count; i++)
                    {
                        if (dtap.Rows[i]["Quantity"].ToString() == "0" || dtap.Rows[i]["Quantity"].ToString() == "")
                        {
                            Util.DBExecute(string.Format(@"UPDATE SellDetail SET Comment = 'คืนสินค้า' ,Sync = 1 WHERE SellNo = '{0}' AND product = '{1}' ", FmReturnSell.sellN, dtap.Rows[i]["Product"].ToString()));

                        }
                    }


                    DataTable QTY = Util.DBQuery(string.Format(@"SELECT Quantity FROM Product WHERE product = '{0}' AND shop = '{1}'", FmReturnSell.Pid, Param.ShopId));

                    int qty = Convert.ToInt32(QTY.Rows[0]["Quantity"].ToString()) + Convert.ToInt32(Param.amount);

                    Util.DBExecute(string.Format(@"UPDATE Product SET Quantity = '{0}',Sync = 1 WHERE id = '{1}' AND shop = '{2}'", qty, FmReturnSell.Pid, Param.ShopId));
                }
            }
            else
            {
                dt = Util.DBQuery(string.Format(@"SELECT STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW') ReturnDate, b.Barcode, b.SellNo, p.product, b.SellPrice
                                            FROM Barcode b
                                                LEFT JOIN Product p
                                                ON b.product = p.product
                                            WHERE p.Shop = '{0}' AND b.Barcode = '{1}'", Param.ShopId, txtBarcodeReturn.Text));

                Util.DBExecute(string.Format(@"INSERT INTO ReturnProduct (ReturnNo, SellNo, ReturnDate, Product, Barcode, SellPrice, ReturnBy, Quantity, Sync)
                    VALUES ('{3}','{0}', '{6}', '{4}', '{1}', '{5}', '{2}',1, 1 ) ",dt.Rows[0]["SellNo"].ToString(), dt.Rows[0]["Barcode"].ToString(), Param.UserId, Return, dt.Rows[0]["product"].ToString(), 
                    dt.Rows[0]["SellPrice"].ToString(), dt.Rows[0]["ReturnDate"].ToString()));

                Util.DBExecute(string.Format(@"UPDATE Barcode SET SellBy = '',SellNo = '',Customer = '',SellPrice = '',SellDate = null ,
                    Sync = 1 WHERE Barcode = '{0}'", txtBarcodeReturn.Text));

                Util.DBExecute(string.Format(@"UPDATE SellHeader SET Profit = (SELECT SUM(SellPrice-Cost-OperationCost) FROM Barcode WHERE SellNo = '{0}')
                        , TotalPrice = (SELECT SUM(SellPrice) FROM Barcode WHERE SellNo = '{0}') ,Sync = 1 WHERE SellNo = '{0}'", dt.Rows[0]["SellNo"].ToString()));

                Util.DBExecute(string.Format(@"UPDATE SellDetail SET SellPrice =  IFNULL((SELECT SUM(SellPrice) FROM Barcode WHERE SellNo = '{0}' AND Product = '{1}'),0)  ,
                            Cost = IFNULL((SELECT SUM(Cost) FROM Barcode WHERE SellNo = '{0}' AND Product = '{1}'),0), Quantity = IFNULL((SELECT COUNT(*) 
                            FROM Barcode WHERE SellNo = '{0}' AND Product = '{1}'),0),
                            Sync = 1 WHERE SellNo = '{0}' AND Product = '{1}'", dt.Rows[0]["SellNo"].ToString(), dt.Rows[0]["product"].ToString()));

                DataTable dtap = Util.DBQuery(string.Format(@"SELECT * FROM SellHeader WHERE SellNo = '{0}'", dt.Rows[0]["SellNo"].ToString()));

                if (dtap.Rows[0]["TotalPrice"].ToString() == "0" || dtap.Rows[0]["TotalPrice"].ToString() == "")
                {
                    Util.DBExecute(string.Format(@"UPDATE SellHeader SET Comment = 'คืนสินค้า' ,Sync = 1 WHERE SellNo = '{0}'", dt.Rows[0]["SellNo"].ToString()));
                    Util.DBExecute(string.Format(@"UPDATE SellDetail SET Comment = 'คืนสินค้า' ,Sync = 1 WHERE SellNo = '{0}'", dt.Rows[0]["SellNo"].ToString()));
                }
            }

            //txtBarcode.Focus();
            lblStatusReturn.Visible = true;
            lblStatusReturn.Text = "รับคืนสินค้าในชิ้นนี้แล้ว";
            returnGridControl.DataSource = null;
            txtBarcodeReturn.Text = "";
            lblWarranty.Visible = false;
            ptbProduct.Image = null;
            btnReturn.Visible = false;
            lblStatusReturn.ForeColor = Color.Green;

        }

        private void txtBarcodeReturn_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                returnGridControl.Visible = true;
                productGridControl.Visible = false;
                int i,a;
                DataRow row;
                DataTable dt;
                lblStatusReturn.Visible = false;

                if (e.KeyCode == Keys.Return)
                {
                    Param.BarcodeNo = txtBarcodeReturn.Text;

                    dt = Util.DBQuery(string.Format(@"SELECT b.OrderNo, p.Product, p.Image, IFNULL(b.ReceivedDate, '') ReceivedDate
                    FROM Barcode b LEFT JOIN Product p ON b.product = p.Product WHERE b.Barcode = '{0}'", txtBarcodeReturn.Text));
                    if (dt.Rows.Count == 0)
                    {
                        dt = Util.DBQuery(string.Format(@"SELECT Barcode FROM Product WHERE Barcode LIKE '%{0}%'", txtBarcodeReturn.Text));
                        Console.WriteLine(txtBarcode.Text + "" + Param.BarcodeNo + "" + dt.Rows.Count.ToString());
                        if (dt.Rows.Count == 0)
                        {
                            
                            dt = Util.DBQuery(string.Format(@"SELECT Product, Barcode FROM Product WHERE SKU LIKE '%{0}%'", txtBarcodeReturn.Text));
                            if (dt.Rows.Count == 0)
                            {
                                lblStatus.Text = "ไม่พบข้อมูลสินค้าชิ้นนี้ในระบบ";
                                lblStatus.ForeColor = Color.Red;
                            }
                            else
                            {
                                int qty = 0;
                                Param.status = "Return";
                                FmProductQty frm = new FmProductQty();
                                Param.product = dt.Rows[0]["Product"].ToString();
                                var result = frm.ShowDialog(this);
                                if (result == System.Windows.Forms.DialogResult.OK)
                                {
                                    lblStatus.Visible = false;
                                    txtBarcode.Enabled = true;

                                    returnGridView.OptionsBehavior.AutoPopulateColumns = false;
                                    returnGridControl.MainView = returnGridView;

                                    dt = new DataTable();
                                    for (i = 0; i < ((ColumnView)returnGridControl.MainView).Columns.Count; i++)
                                    {
                                        dt.Columns.Add(returnGridView.Columns[i].FieldName);
                                    }

                                    if (Param.amount != "")
                                    {
                                        for (i = 0; i < 1; i++)
                                        {
                                            string customer = FmReturnSell.customer;
                                            string SellDate = FmReturnSell.sellDate;
                                            string Pname = FmReturnSell.PName;
                                            int Amount = int.Parse(Param.amount);
                                            string SellP = (int.Parse(FmReturnSell.sellP) * int.Parse(Param.amount)).ToString();

                                            dt = new DataTable();
                                            for (i = 0; i < ((ColumnView)returnGridControl.MainView).Columns.Count; i++)
                                            {
                                                dt.Columns.Add(returnGridView.Columns[i].FieldName);
                                            }

                                            row = dt.NewRow();
                                            row[0] = (i + 1) * 1;
                                            row[1] = SellDate;
                                            row[2] = customer;
                                            row[3] = Pname;
                                            row[4] = Amount;
                                            row[5] = SellP;
                                            dt.Rows.Add(row);
                                            qty += Amount;
                                            returnGridControl.DataSource = dt;

                                        }
                                    }

                                    var remain = (DateTime.Now - Convert.ToDateTime(FmReturnSell.sellDate)).TotalDays;

                                    btnReturn.Visible = remain > 0;

                                    int day = Convert.ToInt32(remain);
                                    if (day == 0)
                                    {
                                        lblWarranty.Text = "สินค้าชิ้นนี้ขายไปแล้วในวันนี้";
                                    }
                                    else
                                    {
                                        lblWarranty.Text = "สินค้าชิ้นนี้" + ((day > 0) ? " ขายไปแล้ว " + day.ToString("#,###") + " วัน" : " ขายไปแล้ว " + (day * -1).ToString("#,###") + " วัน");
                                    }
                                    lblWarranty.Visible = true;

                                    lblListCount.Text = returnGridView.RowCount.ToString() + " รายการ";
                                    lblProductCount.Text = qty.ToString() + " ชิ้น";

                                    var filename = @"Resource/Images/Product/" + Param.product + ".jpg";
                                    dt = Util.DBQuery(string.Format("SELECT Sku, Image FROM Product WHERE product = '{0}'", Param.product));

                                    _STREAM_IMAGE_URL = Param.ImagePath + "/" + dt.Rows[0]["Sku"].ToString() + "/" + dt.Rows[0]["Image"].ToString().Split(',')[0];

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

                                }
                            }
                        }
                        else
                        {
                            Param.status = "Return";
                            FmSelectProduct frm = new FmSelectProduct();
                            var result = frm.ShowDialog(this);
                            if (result == System.Windows.Forms.DialogResult.OK)
                            {
                                lblStatus.Visible = false;
                                txtBarcode.Enabled = true;

                                returnGridView.OptionsBehavior.AutoPopulateColumns = false;
                                returnGridControl.MainView = returnGridView;

                                dt = new DataTable();
                                for (i = 0; i < ((ColumnView)returnGridControl.MainView).Columns.Count; i++)
                                {
                                    dt.Columns.Add(returnGridView.Columns[i].FieldName);
                                }

                                if (Param.amount != "")
                                {
                                    for (i = 0; i < 1; i++)
                                    {
                                        string customer = FmReturnSell.customer;
                                        string SellDate = FmReturnSell.sellDate;
                                        string Pname = FmReturnSell.PName;
                                        int Amount = int.Parse(Param.amount);
                                        string SellP = (int.Parse(FmReturnSell.sellP) * int.Parse(Param.amount)).ToString();

                                        dt = new DataTable();
                                        for (i = 0; i < ((ColumnView)returnGridControl.MainView).Columns.Count; i++)
                                        {
                                            dt.Columns.Add(returnGridView.Columns[i].FieldName);
                                        }

                                        row = dt.NewRow();
                                        row[0] = (i + 1) * 1;
                                        row[1] = SellDate;
                                        row[2] = customer;
                                        row[3] = Pname;
                                        row[4] = Amount;
                                        row[5] = SellP;
                                        dt.Rows.Add(row);

                                        returnGridControl.DataSource = dt;

                                    }
                                }

                                var remain = (DateTime.Now - Convert.ToDateTime(FmReturnSell.sellDate)).TotalDays;

                                btnReturn.Visible = remain > 0;

                                int day = Convert.ToInt32(remain);
                                if (day == 0)
                                {
                                    lblWarranty.Text = "สินค้าชิ้นนี้ขายไปแล้วในวันนี้";
                                }
                                else
                                {
                                    lblWarranty.Text = "สินค้าชิ้นนี้" + ((day > 0) ? " ขายไปแล้ว " + day.ToString("#,###") + " วัน" : " ขายไปแล้ว " + (day * -1).ToString("#,###") + " วัน");
                                }
                                lblWarranty.Visible = true;

                                var filename = @"Resource/Images/Product/" + Param.product + ".jpg";
                                dt = Util.DBQuery(string.Format("SELECT Sku, Image FROM Product WHERE product = '{0}'", Param.product));

                                _STREAM_IMAGE_URL = Param.ImagePath + "/" + dt.Rows[0]["Sku"].ToString() + "/" + dt.Rows[0]["Image"].ToString().Split(',')[0];

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
                            }
                        }


                        //lblStatus.ForeColor = Color.Pink;
                        //lblStatus.Text = "พบข้อมูลสินค้าชิ้นนี้ในระบบ";
                        //_SKU = "0";
                    }
                    else
                    {
                        _TABLE_RETURN = Util.DBQuery(string.Format(@"SELECT p.Name, p.Product, IFNULL(p.Price, 0) Price, IFNULL(p.Price1, 0) Price1, IFNULL(p.Price2, 0) Price2, 
                    b.ReceivedDate, b.ReceivedBy, b.SellDate, b.SellBy,b.Comment , sh.SellNo, c.customer, c.firstname , c.lastname, p.sku, 1 Amount
                    FROM Barcode b 
                        LEFT JOIN Product p 
                        ON b.Product = p.Product 
                         LEFT JOIN SellHeader sh
                        ON b.SellNo = sh.SellNo
                        LEFT JOIN Customer c
                        ON sh.Customer = c.Customer
                    WHERE b.Barcode = '{0}' AND (b.SellDate IS NOT NULL OR b.SellDate = '')", txtBarcodeReturn.Text));
                        lblStatusReturn.Visible = true;


                        if (_TABLE_RETURN.Rows.Count == 0)
                        {
                            lblStatusReturn.Text = "สินค้าชิ้นนี้ยังไม่ได้ทำการขาย";
                            lblStatusReturn.ForeColor = Color.Red;
                            lblWarranty.Visible = false;
                            lblStatusReturn.Visible = true;
                            txtBarcodeReturn.Enabled = true;
                            ptbProduct.Image = null;
                            btnReturn.Visible = false;
                        }
                        else
                        {
                            btnReturn.Visible = true;
                            lblStatusReturn.Visible = false;
                            txtBarcodeReturn.Enabled = true;

                            returnGridView.OptionsBehavior.AutoPopulateColumns = false;
                            returnGridControl.MainView = returnGridView;

                            dt = new DataTable();
                            for (i = 0; i < ((ColumnView)returnGridControl.MainView).Columns.Count; i++)
                            {
                                dt.Columns.Add(returnGridView.Columns[i].FieldName);
                            }

                            if (_TABLE_RETURN.Rows.Count > 0)
                            {
                                for (a = 0; a < _TABLE_RETURN.Rows.Count; a++)
                                {
                                    string customer = _TABLE_RETURN.Rows[a]["firstname"].ToString() + _TABLE_RETURN.Rows[a]["lastname"].ToString();
                                    row = dt.NewRow();
                                    row[0] = (a + 1) * 1;
                                    row[1] = Convert.ToDateTime(_TABLE_RETURN.Rows[a]["SellDate"]).ToLocalTime().ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH")); ;
                                    row[2] = customer;
                                    row[3] = _TABLE_RETURN.Rows[a]["Name"].ToString();
                                    row[4] = _TABLE_RETURN.Rows[a]["Amount"].ToString();
                                    row[5] = Convert.ToInt32(_TABLE_RETURN.Rows[a]["Price"].ToString()).ToString("#,##0");
                                    dt.Rows.Add(row);
                                }

                                returnGridControl.DataSource = dt;

                                var remain = (DateTime.Now - Convert.ToDateTime(_TABLE_RETURN.Rows[0]["SellDate"])).TotalDays;

                                btnReturn.Visible = remain > 0;

                                int day = Convert.ToInt32(remain);
                                if (day == 0)
                                {
                                    lblWarranty.Text = "สินค้าชิ้นนี้ขายไปแล้วในวันนี้";
                                }
                                else
                                {
                                    lblWarranty.Text = "สินค้าชิ้นนี้" + ((day > 0) ? " ขายไปแล้ว " + day.ToString("#,###") + " วัน" : " ขายไปแล้ว " + (day * -1).ToString("#,###") + " วัน");
                                }
                                lblWarranty.Visible = true;


                                ptbProduct.Image = null;


                                var filename = @"Resource/Images/Product/" + _TABLE_RETURN.Rows[0]["product"].ToString() + ".jpg";
                                dt = Util.DBQuery(string.Format("SELECT Image FROM Product WHERE product = '{0}'", _TABLE_RETURN.Rows[0]["product"].ToString()));

                                _STREAM_IMAGE_URL = Param.ImagePath + "/" + _TABLE_RETURN.Rows[0]["sku"].ToString() + "/" + dt.Rows[0]["Image"].ToString().Split(',')[0];

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
                            }
                            else
                            {
                                returnGridControl.DataSource = null;
                            }
                        }
                    }
                }
            }
            catch
            { }
        }

        private void txtBarcode_Enter(object sender, EventArgs e)
        {
            Util.SetKeyboardLayout(Util.GetInputLanguageByName("US"));
        }

        private void productGridControl_EditorKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                _CHANGE = false;
                if (e.KeyCode == Keys.Return)
                {
                    FmChangePrice frm = new FmChangePrice();
                    product = productGridView.GetRowCellDisplayText(productGridView.FocusedRowHandle, productGridView.Columns["Product"]).ToString();
                    price = productGridView.GetRowCellDisplayText(productGridView.FocusedRowHandle, productGridView.Columns["Price"]).ToString();
                    var result = frm.ShowDialog(this);
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        _CHANGE = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private void productGridView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (_CHANGE)
            {
                DataTable dt = Util.DBQuery(string.Format(@"SELECT product, cost FROM Barcode WHERE sellBy = '{0}' AND product = '{1}'
                        UNION ALL SELECT product, cost FROM Product WHERE product = '{1}' AND cost <> 0", Param.DeviceID, UcSale.product));

                if (Convert.ToInt32(productGridView.GetRowCellDisplayText(productGridView.FocusedRowHandle, productGridView.Columns["Price"]).ToString()) >= Convert.ToInt32(dt.Rows[0]["cost"].ToString()))
                {
                    Util.DBExecute(string.Format(@"UPDATE ChangePrice SET priceChange = '{1}'WHERE Product = '{0}' AND SellNo = '{2}'",
                   productGridView.GetRowCellDisplayText(productGridView.FocusedRowHandle, productGridView.Columns["Product"]).ToString()
                   , productGridView.GetRowCellDisplayText(productGridView.FocusedRowHandle, productGridView.Columns["Price"]).ToString(), Param.DeviceID));

                    // อัพเดต ในตาราง ChangePrice
                    //Column PriceChange โดยดูจาก SellNo และ Product
                    Util.DBExecute(string.Format(@"UPDATE Barcode SET SellPrice = '{3}', Sync = 1 WHERE Product = '{1}' AND SellBy = '{0}'",
                        Param.DeviceID, productGridView.GetRowCellDisplayText(productGridView.FocusedRowHandle, productGridView.Columns["Product"]).ToString(),
                        Param.ShopId, productGridView.GetRowCellDisplayText(productGridView.FocusedRowHandle, productGridView.Columns["Price"]).ToString()));

                    price = productGridView.GetRowCellDisplayText(productGridView.FocusedRowHandle, productGridView.Columns["Price"]).ToString();
                }
                else
                {
                    if (MessageBox.Show("ราคาขายต่ำกว่าราคาต้นทุน คุณต้องการขายหรือไม่", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Util.DBExecute(string.Format(@"UPDATE ChangePrice SET priceChange = '{1}'WHERE Product = '{0}' AND SellNo = '{2}'",
                       productGridView.GetRowCellDisplayText(productGridView.FocusedRowHandle, productGridView.Columns["Product"]).ToString()
                       , productGridView.GetRowCellDisplayText(productGridView.FocusedRowHandle, productGridView.Columns["Price"]).ToString(), Param.DeviceID));

                        // อัพเดต ในตาราง ChangePrice
                        //Column PriceChange โดยดูจาก SellNo และ Product
                        Util.DBExecute(string.Format(@"UPDATE Barcode SET SellPrice = '{3}', Sync = 1 WHERE Product = '{1}' AND SellBy = '{0}'",
                            Param.DeviceID, productGridView.GetRowCellDisplayText(productGridView.FocusedRowHandle, productGridView.Columns["Product"]).ToString(),
                            Param.ShopId, productGridView.GetRowCellDisplayText(productGridView.FocusedRowHandle, productGridView.Columns["Price"]).ToString()));

                        price = productGridView.GetRowCellDisplayText(productGridView.FocusedRowHandle, productGridView.Columns["Price"]).ToString();
                    }
                    else
                    {
                        Util.DBExecute(string.Format(@"UPDATE ChangePrice SET priceChange = '{1}'WHERE Product = '{0}' AND SellNo = '{2}'",
                            productGridView.GetRowCellDisplayText(productGridView.FocusedRowHandle, productGridView.Columns["Product"]).ToString()
                            , price, Param.DeviceID));
                    }
                }

                dt = Util.DBQuery(@"SELECT product, productName, price, amount FROM sellTemp");
                if (dt.Rows.Count > 0)
                {

                    Util.DBExecute(string.Format(@"UPDATE sellTemp  SET
                    Price = '{1}',
                    TotalPrice = '{1}' * sellTemp.Amount,
                    PriceCost = (SELECT p.Cost FROM Product p
                    WHERE sellTemp.product = p.Product) * sellTemp.Amount
                    WHERE selltemp.product = '{2}'",
                        Param.SelectCustomerSellPrice == 0 ? "" : "" + Param.SelectCustomerSellPrice, price, 
                        productGridView.GetRowCellDisplayText(productGridView.FocusedRowHandle, productGridView.Columns["Product"]).ToString()));
                }
                LoadData();

            }

        }

        private void DownloadImage(string url, string savePath, string fileName)
        {
            ptbProduct.ImageLocation = url;
            DownloadImage d = new DownloadImage();
            Thread thread = new Thread(() => d.Download(url, savePath, fileName));
            thread.Start();
        }


    }
}
