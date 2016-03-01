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

namespace PowerPOS
{
    public partial class UcReceiveProduct : DevExpress.XtraEditors.XtraUserControl
    {
        private int _QTY = 0, _RECEIVED = 0, _NORECEIVED = 0;
        private int _TOTAL = 0, _TOTALRECEIVED = 0, _NOSR = 0;
        public bool _FIRST_LOAD;
        private bool _SELECT_ORDER = false;
        DataTable _TABLE_RECEIVED, dt;
        private int _ROW_INDEX = -1;
        int row = -1;
        string _STREAM_IMAGE_URL, _SKU;

        public static string productNo;
        public static string OrderNo;
        public static string ProductName;

        string order;

        public UcReceiveProduct()
        {
            InitializeComponent();
        }

        private void UcReceiveProduct_Load(object sender, EventArgs e)
        {
            _FIRST_LOAD = true;
            LoadData();
        }

        public void LoadData()
        {
            DataTable dt, dtab;
                dt = Util.DBQuery(@"SELECT orderNo, SUM(No) NN, SUM(Re) RR FROM (SELECT orderNo, SUM(N) No, SUM(R) Re FROM (SELECT orderNo, COUNT(b.Barcode) N , 0 R FROM Barcode b  GROUP BY b.orderNo
                                    UNION ALL SELECT orderNo, 0 N, COUNT(b.Barcode) R FROM Barcode b WHERE ReceivedDate IS NOT NULL GROUP BY b.orderNo) aa
                                    GROUP BY orderNo HAVING SUM(N) <> SUM(R) 
                                    UNION ALL
                                    SELECT orderNo, SUM(Quantity ), SUM(ReceivedQuantity)  FROM PurchaseOrder GROUP BY orderNo HAVING  SUM(Quantity ) <> SUM(ReceivedQuantity)
                                    ) bb
                                    GROUP BY orderNo
                                    ORDER BY orderNo DESC");
                cbbOrderNo.Properties.Items.Clear();
                cbbOrderNo.Properties.Items.Add("เลขที่ใบสั่งซื้อ");
                if (dt.Rows.Count == 0)
                {
                    cbbOrderNo.Enabled = false;
                }
                else
                {
                    cbbOrderNo.Enabled = true;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cbbOrderNo.Properties.Items.Add(dt.Rows[i]["orderNo"].ToString());
                    }
                }
                cbbOrderNo.SelectedIndex = 0;

                dtab = Util.DBQuery(@"SELECT orderNo, SUM(No) NN, SUM(Re) RR FROM (SELECT orderNo, SUM(N) No, SUM(R) Re FROM (SELECT orderNo, COUNT(b.Barcode) N , 0 R FROM Barcode b GROUP BY b.orderNo
                                    UNION ALL SELECT orderNo, 0 N, COUNT(b.Barcode) R FROM Barcode b WHERE ReceivedDate IS NOT NULL GROUP BY b.orderNo) aa
                                    GROUP BY orderNo HAVING SUM(N) = SUM(R) 
                                    UNION ALL
                                    SELECT orderNo, SUM(Quantity ), SUM(ReceivedQuantity)  FROM PurchaseOrder GROUP BY orderNo HAVING  SUM(Quantity ) = SUM(ReceivedQuantity)
                                    ) bb
                                    GROUP BY orderNo
                                    ORDER BY orderNo DESC");
                cbbOrder.Properties.Items.Clear();
                cbbOrder.Properties.Items.Add("เลขที่ใบสั่งซื้อ");
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

            lblStatus.Text = "";
            _FIRST_LOAD = false;
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

                    gbOrderNo.Height = 53;
                    progressBarControl1.Visible = false;
                    gbCost.Visible = false;
                }
                else if (!_FIRST_LOAD)
                {

                    int i, a;
                    _TABLE_RECEIVED = Util.DBQuery(string.Format(@"
                    SELECT DISTINCT p.sku,b.Product, p.Name, IFNULL(co.Count, 0) ProductCount, IFNULL(r.ReceivedCount, 0) ReceivedCount, IFNULL(b.cost * co.Count,0) Price, IFNULL(b.cost * r.ReceivedCount,0) PriceReceived,  c.name Category
                    FROM Barcode b
                        LEFT JOIN Product p
                            ON b.Product = p.Product
                            AND p.Shop = '{0}'
                        LEFT JOIN ( 
                                SELECT DISTINCT Product, COUNT(*) ReceivedCount 
                                FROM Barcode 
                                WHERE ReceivedDate IS NOT NULL 
                                AND ReceivedBy = '{2}' 
                                AND OrderNo =  '{1}' 
                                GROUP BY Product
                        ) r
                            ON b.Product = r.Product
                        LEFT JOIN Category c
                            ON p.category = c.category
                        LEFT JOIN ( 
                                SELECT DISTINCT bb.Product, COUNT(*) Count 
                                FROM Barcode bb
                                WHERE bb.OrderNo =  '{1}' 
                                GROUP BY bb.Product
                        ) co
                        ON p.product = co.product
                    WHERE (b.ReceivedDate IS NULL OR b.ReceivedBy = '{2}')
                        AND b.OrderNo = '{1}'
                    GROUP BY b.Product
                    UNION ALL
                    SELECT DISTINCT p.sku, po.product, p.Name, po.Quantity, po.ReceivedQuantity, IFNULL(po.priceCost * po.Quantity ,0) Price, IFNULL(po.priceCost * po.ReceivedQuantity,0) PriceReceived, c.name category
                    FROM PurchaseOrder po
                        LEFT JOIN Product p
                        ON po.Product = p.product
                    LEFT JOIN Category c
                    ON p.Category = c.category
                    WHERE po.OrderNo = '{1}'
                ", Param.ShopId, cbbOrderNo.SelectedItem.ToString(), Param.UserId));

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
                    ptbProduct.Visible = false;

                    if (_QTY == 0 || _RECEIVED == 0)
                    {
                        gbOrderNo.Height = 53;
                        progressBarControl1.Visible = false;
                        gbCost.Visible = false;
                    }
                    else if (_RECEIVED != 0)
                    {
                        gbCost.Visible = true;
                        gbOrderNo.Height = 79;
                        progressBarControl1.Visible = true;
                        progressBarControl1.Properties.Maximum = _QTY;
                        progressBarControl1.EditValue = _RECEIVED;
                    }
                }

                lblListCount.Text = receivedGridView.RowCount.ToString("#,##0") + " รายการ";
                if (receivedGridView.RowCount > 0)
                {
                    _NORECEIVED = _QTY - _RECEIVED;
                    lblReceived.Text = _RECEIVED.ToString("#,##0") + " ชิ้น";
                    lblNoReceived.Text = _NORECEIVED.ToString("#,##0") + " ชิ้น";
                    lblProductCount.Text = _QTY.ToString("#,##0") + " ชิ้น";

                    _NOSR = _TOTAL - _TOTALRECEIVED;
                    lblTotal.Text = _TOTAL.ToString("#,##0") + " บาท";
                    lblSRecieved.Text = _TOTALRECEIVED.ToString("#,##0") + " บาท";
                    lblSNoReceived.Text = _NOSR.ToString("#,##0") + " บาท";

                }
                txtBarcode.Focus();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private void SearchDataOrder()
        {
            DataRow row;
            if (cbbOrder.SelectedIndex == 0)
            {
                gbOrder.Height = 69;
                //progressBar1.Visible = false;
                gbCost.Visible = false;
            }
            else if (!_FIRST_LOAD)
            {
                _QTY = 0;
                _RECEIVED = 0;
                int i, a;
                _TABLE_RECEIVED = Util.DBQuery(string.Format(@"
                    SELECT DISTINCT p.sku,b.Product, p.Name, IFNULL(co.Count, 0) ProductCount, IFNULL(r.ReceivedCount, 0) ReceivedCount, c.name Category
                    FROM Barcode b
                        LEFT JOIN Product p
                            ON b.Product = p.Product 
                            AND p.Shop = '{0}'
                        LEFT JOIN ( 
                                SELECT DISTINCT Product, COUNT(*) ReceivedCount 
                                FROM Barcode 
                                WHERE ReceivedDate IS NOT NULL 
                                AND ReceivedBy = '{2}' 
                                AND OrderNo =  '{1}' 
                                GROUP BY Product
                        ) r
                            ON b.Product = r.Product
                        LEFT JOIN Category c
                            ON p.category = c.category
                         LEFT JOIN ( 
                                SELECT DISTINCT bb.Product, COUNT(*) Count 
                                FROM Barcode bb
                                WHERE bb.OrderNo =  '{1}' 
                                GROUP BY bb.Product
                        ) co
                            ON p.product = co.product
                    WHERE (b.ReceivedDate IS NULL OR b.ReceivedBy = '{2}')
                        AND b.OrderNo = '{1}'
                    GROUP BY b.Product
                       UNION ALL
                    SELECT DISTINCT p.sku, po.product, p.Name, po.Quantity, po.ReceivedQuantity , c.name category
                    FROM PurchaseOrder po
                        LEFT JOIN Product p
                        ON po.Product = p.product
                    LEFT JOIN Category c
                    ON p.Category = c.category
                    WHERE po.OrderNo = '{1}'
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
                    dt.Rows.Add(row);
                    _QTY += int.Parse(_TABLE_RECEIVED.Rows[a]["ProductCount"].ToString());
                    _RECEIVED += int.Parse(_TABLE_RECEIVED.Rows[a]["ReceivedCount"].ToString());

                }

                RepositoryItemProgressBar ritem = new RepositoryItemProgressBar();
                ritem.Minimum = 0;
                ritem.Maximum = 100;
                ritem.ShowTitle = true;
                receivedGridControl.RepositoryItems.Add(ritem);
                receivedGridView.Columns["Progress"].ColumnEdit = ritem;

                receivedGridControl.DataSource = dt;

                if (_QTY == 0 || _RECEIVED == 0)
                {
                    gbOrderNo.Height = 53;
                    progressBarControl1.Visible = false;
                    gbCost.Visible = false;
                }
                else if (_RECEIVED != 0)
                {
                    gbCost.Visible = true;
                    gbOrderNo.Height = 79;
                    progressBarControl1.Visible = true;
                    progressBarControl1.Properties.Maximum = _QTY;
                    progressBarControl1.EditValue = _RECEIVED;
                }

                lblListCount.Text = receivedGridView.RowCount.ToString("#,##0") + " รายการ";
                if (receivedGridView.RowCount > 0)
                {
                    int _NORECEIVED = _QTY - _RECEIVED;
                    lblReceived.Text = _RECEIVED.ToString("#,##0") + " ชิ้น";
                    lblNoReceived.Text = _NORECEIVED.ToString("#,##0") + " ชิ้น";
                    lblProductCount.Text = _QTY.ToString("#,##0") + " ชิ้น";
                }

            }
            txtBarcode.Focus();
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                Param.BarcodeNo = txtBarcode.Text;

                DataTable dt = Util.DBQuery(string.Format(@"SELECT OrderNo, p.product, IFNULL(ReceivedDate, '') ReceivedDate , p.Image
                    FROM Barcode b LEFT JOIN Product p ON b.product = p.product
                    WHERE b.Barcode = '{0}'", txtBarcode.Text));

                lblStatus.Visible = true;
                OrderNo = cbbOrderNo.SelectedItem.ToString();
                if (dt.Rows.Count == 0)
                {
                    dt = Util.DBQuery(string.Format(@"SELECT Barcode FROM Product WHERE Barcode LIKE '%{0}%' OR Name LIKE '%{0}%'", txtBarcode.Text));
                    Console.WriteLine(txtBarcode.Text + "" + Param.BarcodeNo + "" + dt.Rows.Count.ToString());

                    if (dt.Rows.Count == 0)
                    {
                        dt = Util.DBQuery(string.Format(@"SELECT Product, Barcode FROM Product WHERE SKU LIKE '%{0}%'", txtBarcode.Text));
                        if (dt.Rows.Count == 0)
                        {
                            lblStatus.ForeColor = Color.Red;
                            lblStatus.Text = "ไม่พบข้อมูลสินค้าชิ้นนี้ในระบบ";
                        }
                        else
                        {
                            Param.status = "Received";
                            FmProductQty frm = new FmProductQty();
                            Param.product = dt.Rows[0]["Product"].ToString();
                            var result = frm.ShowDialog(this);
                            if (result == System.Windows.Forms.DialogResult.OK)
                            {
                                SearchData();
                            }
                        }
                    }
                    else
                    {
                        Param.status = "Received";
                        //OrderNo = cbbOrderNo.SelectedItem.ToString();
                        FmSelectProduct frm = new FmSelectProduct();
                        var result = frm.ShowDialog(this);
                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            SearchData();
                        }
                    }
                }
                else
                {
                    Param.ProductId = dt.Rows[0]["product"].ToString();

                    if (cbbOrderNo.SelectedItem.ToString() != dt.Rows[0]["OrderNo"].ToString())
                    {
                        cbbOrderNo.EditValue = dt.Rows[0]["OrderNo"].ToString();
                    }

                    if (dt.Rows[0]["ReceivedDate"].ToString() != "")
                    {
                        lblStatus.ForeColor = Color.Red;
                        lblStatus.Text = "เคยรับสินค้าชิ้นนี้เข้าระบบแล้ว";
                        SearchData();
                    }
                    else
                    {
                        Util.DBExecute(string.Format(@"UPDATE Barcode SET ReceivedDate = STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), ReceivedBy = '{1}', Sync = 1
                            WHERE Barcode = '{0}'", txtBarcode.Text, Param.UserId));
                        SearchData();

                        lblStatus.ForeColor = Color.Green;
                        lblStatus.Text = "รับสินค้าเข้าระบบเรียบร้อยแล้ว";

                        ptbProduct.Visible = true;
                        ptbProduct.Image = null;

                        var filename = @"Resource/Images/Product/" + Param.ProductId + ".jpg";
                        _STREAM_IMAGE_URL = Param.ImagePath + "/" + receivedGridView.GetRowCellDisplayText(receivedGridView.FocusedRowHandle, receivedGridView.Columns["Sku"]) + "/" + dt.Rows[0]["Image"].ToString().Split(',')[0];

                        if (!File.Exists(filename))
                        {
                            if (dt.Rows[0]["Image"].ToString() != "")
                            {
                                DownloadImage(_STREAM_IMAGE_URL, @"Resource/Images/Product/", Param.ProductId + ".jpg");
                            }
                        }
                        else
                        {
                            try { ptbProduct.Image = Image.FromFile(filename); }
                            catch
                            {
                                if (dt.Rows[0]["Image"].ToString() != "")
                                {
                                    DownloadImage(_STREAM_IMAGE_URL, @"Resource/Images/Product/", Param.ProductId + ".jpg");
                                }
                            }
                        }

                    }

                }
                txtBarcode.Text = "";
                txtBarcode.Focus();
            }
        }

        private void DownloadImage(string url, string savePath, string fileName)
        {
            ptbProduct.ImageLocation = url;
            DownloadImage d = new DownloadImage();
            Thread thread = new Thread(() => d.Download(url, savePath, fileName));
            thread.Start();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dt = Util.DBQuery(@"SELECT CAST(IFNULL(SUM(OperationCost),0) AS DOUBLE) OperationCost, COUNT(*) ProductCount
                FROM Barcode 
                WHERE ReceivedDate IS NOT NULL
                AND SellDate IS NULL
                AND OrderNo = '" + cbbOrderNo.SelectedItem.ToString() + "'");
            var count = int.Parse(dt.Rows[0]["ProductCount"].ToString()) * 1.0;
            var currentCost = double.Parse(dt.Rows[0]["OperationCost"].ToString()) * 1.0;
            var cost = (((int)nudCost.Value * 1.0 + currentCost) / count).ToString("#,##0.00");

            if (MessageBox.Show("เฉลี่ยค่าดำเนินการ = " + cost + " บาท/ชิ้น", "ยืนยันข้อมูล", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                == DialogResult.OK)
            {
               Util.DBExecute(@"UPDATE Barcode SET 
                    OperationCost = " + cost + @", ReceivedBy = '" + Param.UserId + @"', Sync = 1 WHERE ReceivedDate IS NOT NULL AND SellBy = '' AND OrderNo = '" + cbbOrderNo.SelectedItem.ToString() + "'");
                MessageBox.Show("รับสินค้าหมายเลขคำสั่งซื้อเลขที่ " + cbbOrderNo.SelectedItem.ToString() + "\n เสร็จเรียบร้อบแล้ว", "สถานะการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Information);

                receivedGridControl.DataSource = null;
                LoadData();
                SearchData();
            }

        }

        private void cbbOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ptbProduct.Visible = false;
            if (cbbOrder.SelectedIndex != 0 && _SELECT_ORDER == false)
            {
                _SELECT_ORDER = true;
                cbbOrder.SelectedItem = "เลขที่ใบสั่งซื้อ";
                _FIRST_LOAD = false;
                _SELECT_ORDER = false;
            }
            SearchData();
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

        private void receivedGridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
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

        private void btnPrint_Click(object sender, EventArgs e)
        {

            if (cbbOrderNo.SelectedIndex != 0)
            {
                order = cbbOrderNo.SelectedItem.ToString();
                Util.PrintOrder(order);
            }
            else
            {
                MessageBox.Show("กรุณาเลือกเลขที่ใบสั่งซื้อ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void receivedGridControl_DoubleClick(object sender, EventArgs e)
        {
            //_ROW_INDEX = Convert.ToInt32(receivedGridView.GetRowCellDisplayText(receivedGridView.FocusedRowHandle, receivedGridView.Columns["No"]));
            try
            {
                if (receivedGridView.RowCount > 0)
                //{
                //    row = _ROW_INDEX;
                //}
                //if (row != -1)
                {
                    productNo = receivedGridView.GetRowCellDisplayText(receivedGridView.FocusedRowHandle, receivedGridView.Columns["ID"]);
                    if (cbbOrder.SelectedIndex == 0) { OrderNo = cbbOrderNo.SelectedItem.ToString(); } else { OrderNo = cbbOrder.SelectedItem.ToString(); }
                    ProductName = receivedGridView.GetRowCellDisplayText(receivedGridView.FocusedRowHandle, receivedGridView.Columns["Name"]);
                    FmOrderDetail frm = new FmOrderDetail();
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("กรุณาเลือกรายการที่ต้องการดูรายละเอียดการขาย", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
