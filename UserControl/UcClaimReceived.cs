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
using DevExpress.XtraEditors.Repository;
using System.IO;
using System.Threading;
using System.Media;
using System.Globalization;
using Newtonsoft.Json;


namespace PowerPOS
{
    public partial class UcClaimReceived : DevExpress.XtraEditors.XtraUserControl
    {
        private int _QTY = 0, _RECEIVED = 0, _NORECEIVED = 0;
        private int _TOTAL = 0, _TOTALRECEIVED = 0, _NOSR = 0;
        public bool _FIRST_LOAD;
        private bool _SELECT_ORDER = false;
        DataTable _TABLE_RECEIVED, dt;
        private int _ROW_INDEX = -1;
        int row = -1;
        string _STREAM_IMAGE_URL, _SKU, _PRODUCT;
        UcClaimShop _UC_CLAIM_SHOP;

        public static string productNo;

       

        public static string OrderNo;



        public static string ProductName;

        string order;



        public UcClaimReceived()
        {
            InitializeComponent();
        }


        private void UcClaimReceived_Load(object sender, EventArgs e)
        {
            _FIRST_LOAD = true;
            LoadData();
        }

        public void SearchData()
        {
            try
            {
                _QTY = 0;
                _RECEIVED = 0;
                _TOTAL = 0;
                _TOTALRECEIVED = 0;
                DataRow row;
                if (cbbOrderNo.SelectedIndex == 0)
                {
                    //table1.BeginUpdate();
                    //tableModel1.Rows.Clear();
                    //table1.EndUpdate();
                    receivedGridControl.DataSource = null;

                    gbOrderNo.Height = 100;
                }
                else if (!_FIRST_LOAD)
                {
                    int i, a;
                    string[] _SHOP = cbbOrderNo.SelectedItem.ToString().Split('/');
                    _TABLE_RECEIVED = Util.DBQuery(string.Format(@"
                    SELECT DISTINCT ss.shopName, p.sku,b.Product, p.Name, IFNULL(co.Count, 0) ProductCount, IFNULL(r.ReceivedCount, 0) ReceivedCount, IFNULL(b.cost * co.Count,0) Price, IFNULL(b.cost * r.ReceivedCount,0) PriceReceived,  c.name Category
                    FROM BarcodeClaim  b
                        LEFT JOIN Product p
                            ON b.Product = p.Product
                            AND p.Shop = '{0}'
                        LEFT JOIN ( 
                                SELECT DISTINCT Product, COUNT(*) ReceivedCount 
                                FROM BarcodeClaim   
                                WHERE ReceivedDate IS NOT NULL 
                                AND OrderNo =  '{1}' 
                                GROUP BY Product
                        ) r
                            ON b.Product = r.Product
                        LEFT JOIN Category c
                            ON p.category = c.category
                        LEFT JOIN ( 
                                SELECT DISTINCT bb.Product, COUNT(*) Count 
                                FROM BarcodeClaim   bb
                                WHERE bb.OrderNo =  '{1}' 
                                AND bb.shop = '{3}'
                                GROUP BY bb.Product
                        ) co
                        ON p.product = co.product
                        LEFT JOIN Shop ss
                        ON ss.shop = b.shop
                    WHERE b.OrderNo = '{1}'
                    AND b.shop = '{3}'
                    GROUP BY b.Product
                    ", Param.ShopId, _SHOP[0].ToString(), Param.UserId, _SHOP[1].ToString()));

                    receivedGridView.OptionsBehavior.AutoPopulateColumns = false;
                    receivedGridControl.MainView = receivedGridView;

                    dt = new DataTable();
                    for (i = 0; i < ((ColumnView)receivedGridControl.MainView).Columns.Count; i++)
                    {
                        dt.Columns.Add(receivedGridView.Columns[i].FieldName);
                    }

                    for (a = 0; a < _TABLE_RECEIVED.Rows.Count; a++)
                    {
                        var progress = float.Parse(_TABLE_RECEIVED.Rows[a]["ReceivedCount"].ToString()) / float.Parse(_TABLE_RECEIVED.Rows[a]["ProductCount"].ToString()) * 100.0f;
                        row = dt.NewRow();
                        row[0] = (a + 1) * 1;
                        row[1] = _TABLE_RECEIVED.Rows[a]["product"].ToString();
                        row[2] = _TABLE_RECEIVED.Rows[a]["Name"].ToString();
                        row[3] = _TABLE_RECEIVED.Rows[a]["Category"].ToString();
                        row[4] = Convert.ToInt32(_TABLE_RECEIVED.Rows[a]["ProductCount"]).ToString("#,##0");
                        row[5] = Convert.ToInt32(_TABLE_RECEIVED.Rows[a]["ReceivedCount"]).ToString("#,##0");
                        row[6] = (int)progress;
                        row[7] = _TABLE_RECEIVED.Rows[a]["Sku"].ToString();
                        row[8] = _TABLE_RECEIVED.Rows[a]["Price"].ToString();
                        row[9] = _TABLE_RECEIVED.Rows[a]["PriceReceived"].ToString();
                        dt.Rows.Add(row);
                        _QTY += int.Parse(_TABLE_RECEIVED.Rows[a]["ProductCount"].ToString());
                        _RECEIVED += int.Parse(_TABLE_RECEIVED.Rows[a]["ReceivedCount"].ToString());
                        _TOTAL += int.Parse(_TABLE_RECEIVED.Rows[a]["Price"].ToString());
                        _TOTALRECEIVED += int.Parse(_TABLE_RECEIVED.Rows[a]["PriceReceived"].ToString());
                    }

                    lblShopName.Text = _TABLE_RECEIVED.Rows[0]["shopName"].ToString();

                    RepositoryItemProgressBar ritem = new RepositoryItemProgressBar();
                    ritem.Minimum = 0;
                    ritem.Maximum = 100;
                    ritem.ShowTitle = true;
                    receivedGridControl.RepositoryItems.Add(ritem);
                    receivedGridView.Columns["Progress"].ColumnEdit = ritem;

                    receivedGridControl.DataSource = dt;
                    ptbProduct.Visible = false;

                    //if (_QTY == 0 || _RECEIVED == 0)
                    //{
                    //    gbOrderNo.Height = 53;
                    //    progressBarControl1.Visible = false;
                    //    gbCost.Visible = false;
                    //}
                    //else if (_RECEIVED != 0)
                    //{
                    //    gbCost.Visible = true;
                    //    gbOrderNo.Height = 79;
                    //    progressBarControl1.Visible = true;
                    //    progressBarControl1.Properties.Maximum = _QTY;
                    //    progressBarControl1.EditValue = _RECEIVED;
                    //}
                }

                lblListCount.Text = receivedGridView.RowCount.ToString("#,##0") + " รายการ";
                //if (receivedGridView.RowCount > 0)
                //{
                _NORECEIVED = _QTY - _RECEIVED;
                lblReceived.Text = _RECEIVED.ToString("#,##0") + " ชิ้น";
                lblNoReceived.Text = _NORECEIVED.ToString("#,##0") + " ชิ้น";
                lblProductCount.Text = _QTY.ToString("#,##0") + " ชิ้น";

                _NOSR = _TOTAL - _TOTALRECEIVED;
                lblTotal.Text = _TOTAL.ToString("#,##0") + " บาท";
                lblSRecieved.Text = _TOTALRECEIVED.ToString("#,##0") + " บาท";
                lblSNoReceived.Text = _NOSR.ToString("#,##0") + " บาท";

                //}
                txtBarcode.Focus();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                {
                    Param.BarcodeNo = txtBarcode.Text;

                    DataTable dt = Util.DBQuery(string.Format(@"SELECT b.shop, OrderNo, p.product, IFNULL(ReceivedDate, '') ReceivedDate , p.Image
                    FROM BarcodeClaim b LEFT JOIN Product p ON b.product = p.product
                    WHERE b.Barcode = '{0}'", txtBarcode.Text));

                    //lblStatus.Visible = true;
                    OrderNo = cbbOrderNo.SelectedItem.ToString();
                    if (dt.Rows.Count == 0)
                    {
                        SoundPlayer simpleSound = new SoundPlayer(@"Resources/Sound/ohno.wav");
                        simpleSound.Play();

                        if (txtBarcode.Text == "")
                        {
                            MessageBox.Show("กรุณากรอกบาร์โค้ด ที่ต้องการทำการรับสินค้าเข้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบข้อมูลสินค้าชิ้นนี้ในระบบ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        Param.ProductId = dt.Rows[0]["product"].ToString();
                        _PRODUCT = Param.ProductId;
                        string[] _SHOP = cbbOrderNo.SelectedItem.ToString().Split('/');
                        if (_SHOP[0].ToString() != dt.Rows[0]["OrderNo"].ToString())
                        {
                            cbbOrderNo.EditValue = dt.Rows[0]["OrderNo"].ToString()+"/"+ dt.Rows[0]["shop"].ToString();
                        }

                        if (dt.Rows[0]["ReceivedDate"].ToString() != "")
                        {
                            SoundPlayer simpleSound = new SoundPlayer(@"Resources/Sound/ah.wav");
                            simpleSound.Play();

                            MessageBox.Show("เคยรับสินค้าชิ้นนี้เข้าระบบแล้ว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            SearchData();

                            ptbProduct.Visible = true;
                            ptbProduct.Image = null;

                            int rowHandle = receivedGridView.LocateByValue("ID", _PRODUCT);
                            if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                                receivedGridView.FocusedRowHandle = rowHandle;

                            var filename = @"Resource/Images/Product/" + _PRODUCT + ".jpg";
                            _STREAM_IMAGE_URL = Param.ImagePath + "/" + receivedGridView.GetRowCellDisplayText(receivedGridView.FocusedRowHandle, receivedGridView.Columns["Sku"]) + "/" + dt.Rows[0]["Image"].ToString().Split(',')[0];

                            if (!File.Exists(filename))
                            {
                                if (dt.Rows[0]["Image"].ToString() != "")
                                {
                                    DownloadImage(_STREAM_IMAGE_URL, @"Resource/Images/Product/", _PRODUCT + ".jpg");
                                }
                            }
                            else
                            {
                                try { ptbProduct.Image = Image.FromFile(filename); }
                                catch
                                {
                                    if (dt.Rows[0]["Image"].ToString() != "")
                                    {
                                        DownloadImage(_STREAM_IMAGE_URL, @"Resource/Images/Product/", _PRODUCT + ".jpg");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Util.DBExecute(string.Format(@"UPDATE BarcodeClaim SET ReceivedDate = STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), ReceivedBy = '{1}', sync = 1
                            WHERE Barcode = '{0}'", txtBarcode.Text, Param.UserId));
                            _PRODUCT = Param.ProductId;
                            SearchData();

                            SoundPlayer simpleSound = new SoundPlayer(@"Resources/Sound/fastpop.wav");
                            simpleSound.Play();

                            lblStatus.ForeColor = Color.Green;
                            lblStatus.Text = "รับสินค้าเข้าระบบเรียบร้อยแล้ว";

                            ptbProduct.Visible = true;
                            ptbProduct.Image = null;

                            int rowHandle = receivedGridView.LocateByValue("ID", _PRODUCT);
                            if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                                receivedGridView.FocusedRowHandle = rowHandle;

                            var filename = @"Resource/Images/Product/" + _PRODUCT + ".jpg";
                            _STREAM_IMAGE_URL = Param.ImagePath + "/" + receivedGridView.GetRowCellDisplayText(receivedGridView.FocusedRowHandle, receivedGridView.Columns["Sku"]) + "/" + dt.Rows[0]["Image"].ToString().Split(',')[0];

                            if (!File.Exists(filename))
                            {
                                if (dt.Rows[0]["Image"].ToString() != "")
                                {
                                    DownloadImage(_STREAM_IMAGE_URL, @"Resource/Images/Product/", _PRODUCT + ".jpg");
                                }
                            }
                            else
                            {
                                try { ptbProduct.Image = Image.FromFile(filename); }
                                catch
                                {
                                    if (dt.Rows[0]["Image"].ToString() != "")
                                    {
                                        DownloadImage(_STREAM_IMAGE_URL, @"Resource/Images/Product/", _PRODUCT + ".jpg");
                                    }
                                }
                            }
                        }
                    }

                    if (lblNoReceived.Text == "0 ชิ้น")
                    {
                        if (cbbOrderNo.SelectedIndex != 0)
                        {
                            SoundPlayer simpleSound = new SoundPlayer(@"Resources/Sound/yahoo.wav");
                            simpleSound.Play();
                        }
                    }
                    txtBarcode.Text = "";
                    txtBarcode.Focus();
                    _UC_CLAIM_SHOP = new UcClaimShop();
                    _UC_CLAIM_SHOP.LoadData();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void cbbOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ptbProduct.Visible = false;
            if (cbbOrderNo.SelectedIndex != 0 && _SELECT_ORDER == false)
            {
                _SELECT_ORDER = true;
                cbbOrderNo.SelectedItem = "เลขที่ใบสั่งซื้อ";
                _FIRST_LOAD = false;
                _SELECT_ORDER = false;

            }
            SearchDataOrder();
        }

        public void LoadData()
        {
            DataTable dt, dtab;
            dt = Util.DBQuery(@"SELECT b.shop, orderNo, COUNT(b.Barcode) qty FROM BarcodeClaim b 
                        WHERE ReceivedDate IS NULL 
                        GROUP BY b.orderNo, b.shop
                        HAVING qty  <> 0 
                        ORDER BY orderNo DESC");

            cbbOrderNo.Properties.Items.Clear();
            cbbOrderNo.Properties.Items.Add("เลขที่เคลม");
            if (dt.Rows.Count == 0)
            {
                cbbOrderNo.Enabled = false;
            }
            else
            {
                cbbOrderNo.Enabled = true;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cbbOrderNo.Properties.Items.Add(dt.Rows[i]["orderNo"].ToString()+"/"+ dt.Rows[i]["shop"].ToString());
                }
            }
            cbbOrderNo.SelectedIndex = 0;

            dtab = Util.DBQuery(@"SELECT orderNo, COUNT(b.Barcode) qty FROM BarcodeClaim b 
                        WHERE ReceivedDate IS NOT NULL 
                        GROUP BY b.orderNo , b.shop
                        HAVING qty  <> 0
                        ORDER BY orderNo DESC");

            cbbOrder.Properties.Items.Clear();
            cbbOrder.Properties.Items.Add("เลขที่เคลม");
            if (dtab.Rows.Count == 0)
            {
                cbbOrder.Enabled = false;
            }
            else
            {
                cbbOrder.Enabled = true;
                for (int i = 0; i < dtab.Rows.Count; i++)
                {
                    cbbOrder.Properties.Items.Add(dtab.Rows[i]["OrderNo"].ToString());
                }
            }
            cbbOrder.SelectedIndex = 0;

            _FIRST_LOAD = false;
        }

        private void SearchDataOrder()
        {
            DataRow row;
            _QTY = 0;
            _RECEIVED = 0;
            _TOTAL = 0;
            _TOTALRECEIVED = 0;
            if (cbbOrder.SelectedIndex == 0)
            {
                receivedGridControl.DataSource = null;
                gbOrder.Height = 69;
                //progressBar1.Visible = false;
                //gbCost.Visible = false;
            }
            else if (!_FIRST_LOAD)
            {
                //_QTY = 0;
                //_RECEIVED = 0;
                int i, a;
                _TABLE_RECEIVED = Util.DBQuery(string.Format(@"
                    SELECT DISTINCT p.sku,b.Product, p.Name, IFNULL(co.Count, 0) ProductCount, IFNULL(r.ReceivedCount, 0) ReceivedCount, IFNULL(b.cost * co.Count,0) Price, IFNULL(b.cost * r.ReceivedCount,0) PriceReceived,  c.name Category
                    FROM BarcodeClaim b
                        LEFT JOIN Product p
                            ON b.Product = p.Product
                            AND p.Shop = '{0}'
                        LEFT JOIN ( 
                                SELECT DISTINCT Product, COUNT(*) ReceivedCount 
                                FROM BarcodeClaim 
                                WHERE ReceivedDate IS NOT NULL 
                                AND OrderNo =  '{1}' 
                                GROUP BY Product
                        ) r
                            ON b.Product = r.Product
                        LEFT JOIN Category c
                            ON p.category = c.category
                        LEFT JOIN ( 
                                SELECT DISTINCT bb.Product, COUNT(*) Count 
                                FROM BarcodeClaim bb
                                WHERE bb.OrderNo =  '{1}' 
                                GROUP BY bb.Product
                        ) co
                        ON p.product = co.product
                    WHERE b.OrderNo = '{1}'
                    GROUP BY b.Product
                 ", Param.ShopId, cbbOrder.SelectedItem.ToString(), Param.UserId));

                receivedGridView.OptionsBehavior.AutoPopulateColumns = false;
                receivedGridControl.MainView = receivedGridView;

                dt = new DataTable();
                for (i = 0; i < ((ColumnView)receivedGridControl.MainView).Columns.Count; i++)
                {
                    dt.Columns.Add(receivedGridView.Columns[i].FieldName);
                }

                for (a = 0; a < _TABLE_RECEIVED.Rows.Count; a++)
                {
                    var progress = float.Parse(_TABLE_RECEIVED.Rows[a]["ReceivedCount"].ToString()) / float.Parse(_TABLE_RECEIVED.Rows[a]["ProductCount"].ToString()) * 100.0f;
                    row = dt.NewRow();
                    row[0] = (a + 1) * 1;
                    row[1] = _TABLE_RECEIVED.Rows[a]["product"].ToString();
                    row[2] = _TABLE_RECEIVED.Rows[a]["Name"].ToString();
                    row[3] = _TABLE_RECEIVED.Rows[a]["Category"].ToString();
                    row[4] = Convert.ToInt32(_TABLE_RECEIVED.Rows[a]["ProductCount"]).ToString("#,##0");
                    row[5] = Convert.ToInt32(_TABLE_RECEIVED.Rows[a]["ReceivedCount"]).ToString("#,##0");
                    row[6] = (int)progress;
                    row[7] = _TABLE_RECEIVED.Rows[a]["Sku"].ToString();
                    row[8] = _TABLE_RECEIVED.Rows[a]["Price"].ToString();
                    row[9] = _TABLE_RECEIVED.Rows[a]["PriceReceived"].ToString();
                    dt.Rows.Add(row);
                    _QTY += int.Parse(_TABLE_RECEIVED.Rows[a]["ProductCount"].ToString());
                    _RECEIVED += int.Parse(_TABLE_RECEIVED.Rows[a]["ReceivedCount"].ToString());
                    _TOTAL += int.Parse(_TABLE_RECEIVED.Rows[a]["Price"].ToString());
                    _TOTALRECEIVED += int.Parse(_TABLE_RECEIVED.Rows[a]["PriceReceived"].ToString());
                }

                RepositoryItemProgressBar ritem = new RepositoryItemProgressBar();
                ritem.Minimum = 0;
                ritem.Maximum = 100;
                ritem.ShowTitle = true;
                receivedGridControl.RepositoryItems.Add(ritem);
                receivedGridView.Columns["Progress"].ColumnEdit = ritem;

                receivedGridControl.DataSource = dt;

                //if (_QTY == 0 || _RECEIVED == 0)
                //{
                //    gbOrderNo.Height = 53;
                //    progressBarControl1.Visible = false;
                //    gbCost.Visible = false;
                //}
                //else if (_RECEIVED != 0)
                //{
                //    gbCost.Visible = true;
                //    gbOrderNo.Height = 79;
                //    progressBarControl1.Visible = true;
                //    progressBarControl1.Properties.Maximum = _QTY;
                //    progressBarControl1.EditValue = _RECEIVED;
                //}

                lblListCount.Text = receivedGridView.RowCount.ToString("#,##0") + " รายการ";
            }

            _NORECEIVED = _QTY - _RECEIVED;
            lblReceived.Text = _RECEIVED.ToString("#,##0") + " ชิ้น";
            lblNoReceived.Text = _NORECEIVED.ToString("#,##0") + " ชิ้น";
            lblProductCount.Text = _QTY.ToString("#,##0") + " ชิ้น";

            _NOSR = _TOTAL - _TOTALRECEIVED;
            lblTotal.Text = _TOTAL.ToString("#,##0") + " บาท";
            lblSRecieved.Text = _TOTALRECEIVED.ToString("#,##0") + " บาท";
            lblSNoReceived.Text = _NOSR.ToString("#,##0") + " บาท";
            //}
            txtBarcode.Focus();
        }

        private void cbbOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ptbProduct.Visible = false;
            if (cbbOrder.SelectedIndex != 0 && _SELECT_ORDER == false)
            {
                _SELECT_ORDER = true;
                cbbOrder.SelectedItem = "เลขที่เคลม";
                _FIRST_LOAD = false;
                _SELECT_ORDER = false;
            }
            SearchData();
        }

        private void receivedGridControl_Click(object sender, EventArgs e)
        {
            if (!_FIRST_LOAD)
            {
                if (receivedGridView.RowCount > 0)
                {
                    try
                    {
                        ptbProduct.Visible = true;
                        Param.ProductId = receivedGridView.GetRowCellDisplayText(receivedGridView.FocusedRowHandle, receivedGridView.Columns["ID"]);
                        ptbProduct.Image = null;

                        var filename = @"Resource/Images/Product/" + Param.ProductId + ".jpg";
                        DataTable dt = Util.DBQuery(string.Format("SELECT Image FROM Product WHERE product = '{0}'", Param.ProductId));

                        _STREAM_IMAGE_URL = Param.ImagePath + receivedGridView.GetRowCellDisplayText(receivedGridView.FocusedRowHandle, receivedGridView.Columns["Sku"]) + "/" + dt.Rows[0]["Image"].ToString().Split(',')[0];

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
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        //pnlPrice.Visible = false;
                    }
                }
            }
        }

        private void DownloadImage(string url, string savePath, string fileName)
        {
            ptbProduct.ImageLocation = url;
            DownloadImage d = new DownloadImage();
            Thread thread = new Thread(() => d.Download(url, savePath, fileName));
            thread.Start();
        }

        private void receivedGridControl_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //if (receivedGridView.RowCount > 0)
                //{
                //    productNo = receivedGridView.GetRowCellDisplayText(receivedGridView.FocusedRowHandle, receivedGridView.Columns["ID"]);
                //    if (cbbOrder.SelectedIndex == 0) { OrderNo = cbbOrderNo.SelectedItem.ToString(); } else { OrderNo = cbbOrder.SelectedItem.ToString(); }
                //    ProductName = receivedGridView.GetRowCellDisplayText(receivedGridView.FocusedRowHandle, receivedGridView.Columns["Name"]);
                //    FmOrderDetail frm = new FmOrderDetail();
                //    frm.Show();
                //}
                //else
                //{
                //    MessageBox.Show("กรุณาเลือกรายการที่ต้องการดูรายละเอียดการขาย", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
