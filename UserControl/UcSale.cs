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
            _TABLE_SALE = Util.DBQuery(string.Format(@"SELECT p.product, p.Name, p.Price{2} Price, ProductCount
                        FROM (SELECT Product, COUNT(*) ProductCount FROM Barcode WHERE SellBy = '{0}' GROUP BY Product) b 
                            LEFT JOIN Product p 
                            ON b.Product = p.product
                        WHERE Shop = '{1}'", Param.CpuId, Param.ShopId, Param.SelectCustomerSellPrice == 0 ? "" : "" + Param.SelectCustomerSellPrice));

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
                    var total = int.Parse(_TABLE_SALE.Rows[a]["ProductCount"].ToString()) * int.Parse(_TABLE_SALE.Rows[a]["Price"].ToString());
                    row = dt.NewRow();
                    row[0] = (a + 1) * 1;
                    row[1] = _TABLE_SALE.Rows[a]["product"].ToString();
                    row[2] = _TABLE_SALE.Rows[a]["Name"].ToString();
                    row[3] = Convert.ToInt32(_TABLE_SALE.Rows[a]["Price"].ToString()).ToString("#,##0");
                    row[4] = Convert.ToInt32(_TABLE_SALE.Rows[a]["ProductCount"]).ToString("#,##0");
                    row[5] = (int)total;
                    dt.Rows.Add(row);
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

            btnCancelSale.Enabled = sumPrice > 0;
            btnCancelProduct.Enabled = sumPrice > 0;
            btnConfirm.Enabled = sumPrice > 0;
    }

        private void SelectCustomer(object sender, EventArgs e)
        {
        //    lblCustomerName.Text = Param.SelectCustomerName;
        //    if (Param.SelectCustomerSex != "")
        //    {
        //        if (Param.SelectCustomerSex == "M")
        //            btnMale_Click(sender, e);
        //        else
        //            btnFemale_Click(sender, e);
        //        btnMale.Enabled = false;
        //        btnFemale.Enabled = false;
        //    }
        //    else
        //    {
        //        SetDefaultButton(btnMale);
        //        SetDefaultButton(btnFemale);
        //        btnMale.Enabled = true;
        //        btnFemale.Enabled = true;
        //    }

        //    if (Param.SelectCustomerAge != 0)
        //    {
        //        if (Param.SelectCustomerAge > 60)
        //            btnAge6_Click(sender, e);
        //        else if (Param.SelectCustomerAge > 40)
        //            btnAge5_Click(sender, e);
        //        else if (Param.SelectCustomerAge > 25)
        //            btnAge4_Click(sender, e);
        //        else if (Param.SelectCustomerAge > 18)
        //            btnAge3_Click(sender, e);
        //        else if (Param.SelectCustomerAge > 12)
        //            btnAge2_Click(sender, e);
        //        else
        //            btnAge1_Click(sender, e);
        //        btnAge1.Enabled = false;
        //        btnAge2.Enabled = false;
        //        btnAge3.Enabled = false;
        //        btnAge4.Enabled = false;
        //        btnAge5.Enabled = false;
        //        btnAge6.Enabled = false;
        //    }
        //    else
        //    {
        //        SetDefaultButton(btnAge1);
        //        SetDefaultButton(btnAge2);
        //        SetDefaultButton(btnAge3);
        //        SetDefaultButton(btnAge4);
        //        SetDefaultButton(btnAge5);
        //        SetDefaultButton(btnAge6);
        //        btnAge1.Enabled = true;
        //        btnAge2.Enabled = true;
        //        btnAge3.Enabled = true;
        //        btnAge4.Enabled = true;
        //        btnAge5.Enabled = true;
        //        btnAge6.Enabled = true;
        //    }
        //    LoadData();
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    lblStatus.Visible = false;

            //    if (e. == Keys.Return)
            //    {
            //        DataTable dt = Util.DBQuery(string.Format(@"SELECT p.Name, p.ID, IFNULL(p.Price, 0) Price, IFNULL(p.Price1, 0) Price1, IFNULL(p.Price2, 0) Price2, ReceivedDate, ReceivedBy, SellDate, SellBy, Comment 
            //        FROM Barcode b 
            //            LEFT JOIN Product p 
            //            ON b.Product = p.ID 
            //        WHERE Barcode = '{0}'", txtBarcode.Text, Param.ShopId));
            //        lblStatus.Visible = true;

            //        string[] claim = dt.Rows[0]["Comment"].ToString().Split('(');
            //        string[] change = dt.Rows[0]["Comment"].ToString().Split('[');


            //        if (dt.Rows.Count == 0)
            //        {
            //            lblStatus.Text = "ไม่พบข้อมูลสินค้าชิ้นนี้ในระบบ";
            //            lblStatus.ForeColor = Color.Red;
            //        }
            //        else if (dt.Rows[0]["ReceivedDate"].ToString() == "")
            //        {
            //            lblStatus.Text = "สินค้าชิ้นนี้ยังไม่ได้รับเข้าระบบ";
            //            lblStatus.ForeColor = Color.Red;
            //            var confirm = MessageBox.Show("สินค้าชิ้นนี้ยังไม่ได้รับเข้าระบบ\nคุณต้องการไปที่หน้าจอรับสินค้าเข้าระบบหรือไม่ ?", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            //            if (confirm)
            //            {
            //                Util.ShowScreen(Param.Screen.ReceiveProduct);
            //                Param.SelectedScreen = (int)Param.Screen.ReceiveProduct;
            //            }
            //        }
            //        else if (dt.Rows[0]["ReceivedBy"].ToString() == "")
            //        {
            //            lblStatus.Text = "สินค้าชิ้นนี้ยังไม่ได้กำหนดต้นทุน";
            //            lblStatus.ForeColor = Color.Red;
            //            var confirm = MessageBox.Show("สินค้าชิ้นนี้ยังไม่ได้กำหนดต้นทุน\nคุณต้องการไปที่หน้าจอรับสินค้าเข้าระบบหรือไม่ ?", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            //            if (confirm)
            //            {
            //                Util.ShowScreen(Param.Screen.ReceiveProduct);
            //                Param.SelectedScreen = (int)Param.Screen.ReceiveProduct;
            //            }
            //        }
            //        else if (claim[0].ToString() == "เคลมสินค้า" || change[0].ToString() == "เปลี่ยนสินค้า")
            //        {
            //            lblStatus.Text = "สินค้าชิ้นนี้ได้ทำการเคลมแล้ว";
            //            lblStatus.ForeColor = Color.Red;
            //        }
            //        else if (dt.Rows[0]["SellDate"].ToString() != "")
            //        {
            //            lblStatus.Text = "สินค้าชิ้นนี้ได้ขายออกจากระบบไปแล้ว";
            //            lblStatus.ForeColor = Color.Red;
            //        }
            //        else if (dt.Rows[0]["SellBy"].ToString() == Param.CpuId)
            //        {
            //            lblStatus.Text = "มีสินค้าชิ้นนี้ในรายการขายแล้ว";
            //            lblStatus.ForeColor = Color.Red;
            //        }
            //        else if (int.Parse(dt.Rows[0]["Price"].ToString()) == 0 || int.Parse(dt.Rows[0]["Price1"].ToString()) == 0)
            //        {
            //            lblStatus.Text = "สินค้าชิ้นนี้ยังไม่ได้กำหนดราคาขาย";
            //            lblStatus.ForeColor = Color.Red;
            //            var confirm = MessageBox.Show("สินค้าชิ้นนี้ยังไม่ได้กำหนดราคาขาย\nคุณต้องการไปที่หน้าจอข้อมูลสินค้าหรือไม่ ?", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            //            if (confirm)
            //            {
            //                Util.ShowScreen(Param.Screen.Product);
            //                Param.SelectedScreen = (int)Param.Screen.Product;
            //            }
            //        }
            //        else
            //        {
            //            Util.DBExecute(string.Format(@"UPDATE Barcode SET SellBy = '{0}', Sync = 1 WHERE Barcode = '{1}'", Param.CpuId, txtBarcode.Text));
            //            LoadData();
            //            txtBarcode.Focus();
            //            lblStatus.Text = "เพิ่มสินค้าในรายการขายแล้ว";
            //            lblStatus.ForeColor = Color.Green;
            //        }

            //        txtBarcode.Text = "";
            //    }
            //}
            //catch { }
        }

        private void btnCancelSale_Click(object sender, EventArgs e)
        {
            if (productGridView.RowCount > 0)
            {
                var confirm = MessageBox.Show("คุณแน่ใจหรือไม่\nที่จะยกเลิกรายการขายนี้", "ยืนยันการทำงาน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
                if (confirm)
                {
                    Util.DBExecute(string.Format(@"UPDATE Barcode SET SellBy = '', Sync = 1 WHERE SellBy = '{0}'", Param.CpuId));
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
            //if (result == System.Windows.Forms.DialogResult.OK)
            //{
            //    SelectCustomer(sender, e);
            //}
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                productGridControl.Visible = true;
                returnGridControl.Visible = false;
                lblStatus.Visible = false;

                if (e.KeyCode == Keys.Return)
                {
                    barcode = txtBarcode.Text;
                    DataTable dt = Util.DBQuery(string.Format(@"SELECT p.Name, p.Product, IFNULL(p.Price, 0) Price, IFNULL(p.Price1, 0) Price1, IFNULL(p.Price2, 0) Price2, ReceivedDate, ReceivedBy, SellDate, SellBy, Comment 
                    FROM Barcode b 
                        LEFT JOIN Product p 
                        ON b.Product = p.Product 
                    WHERE Barcode = '{0}'", txtBarcode.Text, Param.ShopId));
                    lblStatus.Visible = true;

                    if (dt.Rows.Count == 0)
                    {
                        dt = Util.DBQuery(string.Format(@"SELECT Barcode FROM Product WHERE Barcode LIKE '%{0}%'", txtBarcode.Text));
                        Console.WriteLine(txtBarcode.Text + "" + Param.BarcodeNo + "" + dt.Rows.Count.ToString());
                        if (dt.Rows.Count == 0)
                        {
                            lblStatus.Text = "ไม่พบข้อมูลสินค้าชิ้นนี้ในระบบ";
                            lblStatus.ForeColor = Color.Red;
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
                        lblStatus.Text = "สินค้าชิ้นนี้ได้ขายออกจากระบบไปแล้ว";
                        lblStatus.ForeColor = Color.Red;
                    }
                    else if (dt.Rows[0]["SellBy"].ToString() == Param.CpuId)
                    {
                        lblStatus.Text = "มีสินค้าชิ้นนี้ในรายการขายแล้ว";
                        lblStatus.ForeColor = Color.Red;
                    }
                    else if (int.Parse(dt.Rows[0]["Price"].ToString()) == 0 || int.Parse(dt.Rows[0]["Price1"].ToString()) == 0)
                    {
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
                        Util.DBExecute(string.Format(@"UPDATE Barcode SET SellBy = '{0}', Sync = 1 WHERE Barcode = '{1}'", Param.CpuId, txtBarcode.Text));
                        LoadData();
                        txtBarcode.Focus();
                        lblStatus.Text = "เพิ่มสินค้าในรายการขายแล้ว";
                        lblStatus.ForeColor = Color.Green;
                    }

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
            { }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            DataTable dt;
            dt = Util.DBQuery(string.Format(@"SELECT IFNULL(SUBSTR(MAX(returnNo), 1,6)||SUBSTR('0000'||(SUBSTR(MAX(returnNo), 7, 4)+1), -4, 4), SUBSTR(STRFTIME('%Y%m{0}R'), 3)||'0001') returnNo
                    FROM ReturnProduct
                    WHERE SUBSTR(returnNo, 1, 4) = SUBSTR(STRFTIME('%Y%m'), 3, 4)
                    AND SUBSTR(returnNo, 5, 1) = '{0}'", Param.DevicePrefix));
            var returnNo = dt.Rows[0]["returnNo"].ToString();

            dt = Util.DBQuery(string.Format(@"SELECT b.Barcode, b.SellNo, p.product, b.SellPrice
                                            FROM Barcode b
                                                LEFT JOIN Product p
                                                ON b.product = p.product
                                            WHERE p.Shop = '{0}' AND Barcode = '{1}'", Param.ShopId, txtBarcodeReturn.Text));

            Util.DBExecute(string.Format(@"INSERT INTO ReturnProduct (ReturnNo, SellNo, ReturnDate, Product, Barcode, SellPrice, ReturnBy, Sync)
                    SELECT '{3}','{0}', STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), Product, Barcode, SellPrice, '{2}', 1 FROM Barcode WHERE SellNo = '{0}'AND Barcode = '{1}' GROUP BY Product",
                    dt.Rows[0]["SellNo"].ToString(), dt.Rows[0]["Barcode"].ToString(), Param.UserId, returnNo));

            Util.DBExecute(string.Format(@"UPDATE Barcode SET SellBy = '',SellNo = '',Customer = '',SellPrice = '',SellDate = null ,
                    Sync = 1 WHERE Barcode = '{0}'", txtBarcodeReturn.Text));

            //Util.DBExecute(string.Format(@"UPDATE SellHeader SET Profit = (SELECT SUM(SellPrice-Cost-OperationCost) FROM Barcode WHERE SellNo = '{0}')
            //        , TotalPrice = (SELECT SUM(SellPrice) FROM Barcode WHERE SellNo = '{0}') ,Sync = 1 WHERE SellNo = '{0}'", dt.Rows[0]["SellNo"].ToString()));

            //Util.DBExecute(string.Format(@"UPDATE SellDetail SET SellPrice =  IFNULL((SELECT SUM(SellPrice) FROM Barcode WHERE SellNo = '{0}' AND Product = '{1}'),0)  ,
            //            Cost = IFNULL((SELECT SUM(Cost) FROM Barcode WHERE SellNo = '{0}' AND Product = '{1}'),0), Quantity = IFNULL((SELECT COUNT(*) 
            //            FROM Barcode WHERE SellNo = '{0}' AND Product = '{1}'),0),
            //            Sync = 1 WHERE SellNo = '{0}' AND Product = '{1}'", dt.Rows[0]["SellNo"].ToString(), dt.Rows[0]["product"].ToString()));

            //DataTable dtap = Util.DBQuery(string.Format(@"SELECT * FROM SellHeader WHERE SellNo = '{0}'", dt.Rows[0]["SellNo"].ToString()));

            //if (dtap.Rows[0]["TotalPrice"].ToString() == "0" || dtap.Rows[0]["TotalPrice"].ToString() == "")
            //{
            //    Util.DBExecute(string.Format(@"UPDATE SellHeader SET Comment = 'คืนสินค้า' ,Sync = 1 WHERE SellNo = '{0}'", dt.Rows[0]["SellNo"].ToString()));
            //    Util.DBExecute(string.Format(@"UPDATE SellDetail SET Comment = 'คืนสินค้า' ,Sync = 1 WHERE SellNo = '{0}'", dt.Rows[0]["SellNo"].ToString()));
            //}

            //txtBarcode.Focus();
            lblStatusReturn.Visible = true;
            lblStatusReturn.Text = "รับคืนสินค้าในชิ้นนี้แล้ว (" + dt.Rows[0]["Barcode"].ToString() + ")";
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
                    _TABLE_RETURN = Util.DBQuery(string.Format(@"SELECT p.Name, p.Product, IFNULL(p.Price, 0) Price, IFNULL(p.Price1, 0) Price1, IFNULL(p.Price2, 0) Price2, 
                    b.ReceivedDate, b.ReceivedBy, b.SellDate, b.SellBy,b.Comment , sh.SellNo, c.customer, c.firstname , c.lastname, p.sku
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
                                row[4] = Convert.ToInt32(_TABLE_RETURN.Rows[a]["Price"].ToString()).ToString("#,##0");
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
            catch
            { }
        }

        private void txtBarcode_Enter(object sender, EventArgs e)
        {
            Util.SetKeyboardLayout(Util.GetInputLanguageByName("US"));
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
