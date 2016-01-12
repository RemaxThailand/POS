﻿ using System;
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
        private int _QTY = 0;
        private int _RECEIVED = 0;
        public bool _FIRST_LOAD;
        private bool _SELECT_ORDER = false;
        DataTable _TABLE_RECEIVED, dt;
        private int _ROW_INDEX = -1;
        int row = -1;
        string _STREAM_IMAGE_URL;

        public static string productNo;
        public static string OrderNo;
        public static string ProductName;

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
            //if (Param.SystemConfig.SellPrice != null)
            //{
            dt = Util.DBQuery(@"SELECT DISTINCT OrderNo FROM Barcode WHERE ReceivedDate IS NULL ORDER BY OrderNo");
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
                    cbbOrderNo.Properties.Items.Add(dt.Rows[i]["OrderNo"].ToString());
                }
            }
            cbbOrderNo.SelectedIndex = 0;

            dtab = Util.DBQuery(@"SELECT DISTINCT OrderNo FROM Barcode WHERE OrderNo NOT IN (SELECT OrderNo  FROM Barcode WHERE ReceivedDate IS NULL ) AND OrderNo <> ''  ORDER BY OrderNo");
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
            //}
            //else
            //{
            //dt = Util.DBQuery(@"SELECT DISTINCT OrderNo FROM Barcode WHERE (ReceivedDate IS NULL OR (OperationCost = 0 OR OperationCost = '')) AND Ship <> 2 ORDER BY OrderNo");
            //cbbOrderNo.Properties.Items.Clear();
            //cbbOrderNo.Properties.Items.Add("เลขที่ใบสั่งซื้อ");
            //if (dt.Rows.Count == 0)
            //{
            //    cbbOrderNo.Enabled = false;
            //}
            //else
            //{
            //    cbbOrderNo.Enabled = true;
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        cbbOrderNo.Properties.Items.Add(dt.Rows[i]["OrderNo"].ToString());
            //    }
            //}
            //cbbOrderNo.SelectedIndex = 0;

            //dtab = Util.DBQuery(@"SELECT DISTINCT OrderNo FROM Barcode WHERE OrderNo NOT IN (SELECT OrderNo  FROM Barcode WHERE ReceivedDate IS NULL OR (OperationCost = 0 OR OperationCost = '') AND Ship <> 2) AND OrderNo <> ''  ORDER BY OrderNo");
            //cbbOrder.Properties.Items.Clear();
            //cbbOrder.Properties.Items.Add("เลขที่ใบสั่งซื้อ");
            //if (dtab.Rows.Count == 0)
            //{
            //    cbbOrder.Enabled = false;
            //}
            //else
            //{
            //    cbbOrder.Enabled = true;
            //    for (int i = 0; i < dtab.Rows.Count; i++)
            //    {
            //        cbbOrder.Properties.Items.Add(dtab.Rows[i]["OrderNo"].ToString());
            //    }
            //}
            //cbbOrder.SelectedIndex = 0;
            //}

            lblStatus.Text = "";
            _FIRST_LOAD = false;
        }

        public void SearchData()
        {
            try
            {
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
                    _QTY = 0;
                    _RECEIVED = 0;
                    int i, a;
                    _TABLE_RECEIVED = Util.DBQuery(string.Format(@"
                    SELECT DISTINCT p.sku,b.Product, p.Name, COUNT(*) ProductCount, IFNULL(r.ReceivedCount, 0) ReceivedCount, c.name Category
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
                    WHERE (b.ReceivedDate IS NULL OR b.ReceivedBy = '{2}')
                        AND b.OrderNo = '{1}'
                    GROUP BY b.Product
                    ORDER BY c.name,p.Name
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
                        progressBarControl1.Increment(_RECEIVED);
                    }
                }

                dt = new DataTable();
                dt = Util.DBQuery(string.Format(@"SELECT COUNT(ReceivedDate) Received FROM Barcode WHERE ReceivedDate IS NOT NULL AND OrderNo = '{0}'
                        UNION ALL SELECT COUNT(ReceivedDate) Received FROM Barcode WHERE ReceivedDate IS NULL AND OrderNo = '{0}'", cbbOrderNo.SelectedItem.ToString()));

                lblListCount.Text = receivedGridView.RowCount.ToString() + " " + "รายการ";
                lblReceived.Text = dt.Rows[0]["Received"].ToString();
                lblNoReceived.Text = dt.Rows[1]["Received"].ToString();
                int val = int.Parse(dt.Rows[0]["Received"].ToString()) + int.Parse(dt.Rows[1]["Received"].ToString());
                lblProductCount.Text = val.ToString();
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
                    SELECT DISTINCT b.Product, p.Name, COUNT(*) ProductCount, IFNULL(r.ReceivedCount, 0) ReceivedCount, c.name Category
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
                    WHERE (b.ReceivedDate IS NULL OR b.ReceivedBy = '{2}')
                        AND b.OrderNo = '{1}'
                    GROUP BY b.Product
                    ORDER BY p.Name
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

            }
            txtBarcode.Focus();
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                DataTable dt = Util.DBQuery(string.Format(@"SELECT OrderNo, p.product, IFNULL(ReceivedDate, '') ReceivedDate , p.Image
                    FROM Barcode b LEFT JOIN Product p ON b.product = p.product
                    WHERE Barcode = '{0}'", txtBarcode.Text));

                lblStatus.Visible = true;

                if (dt.Rows.Count == 0)
                {
                    lblStatus.ForeColor = Color.Red;
                    lblStatus.Text = "ไม่พบข้อมูลสินค้าชิ้นนี้ในระบบ";
                    //_SKU = "0";
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
                        Param.ProductId = receivedGridView.GetRowCellDisplayText(receivedGridView.FocusedRowHandle, receivedGridView.Columns["Product"]);
                        ptbProduct.Image = null;

                        var filename = @"Resource/Images/Product/" + Param.ProductId + ".jpg";
                        DataTable dt = Util.DBQuery(string.Format("SELECT Image FROM Product WHERE product = '{0}'", Param.ProductId));

                        _STREAM_IMAGE_URL = "http://src.powerdd.com/img/product/" + "88888888" + "/" + receivedGridView.GetRowCellDisplayText(receivedGridView.FocusedRowHandle, receivedGridView.Columns["Sku"]) + "/" + dt.Rows[0]["Image"].ToString().Split(',')[0];

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
                        //MessageBox.Show(ex.ToString());
                        //pnlPrice.Visible = false;
                    }
                }

            }
        }

        private void receivedGridControl_DoubleClick(object sender, EventArgs e)
        {
            _ROW_INDEX = Convert.ToInt32(receivedGridView.GetRowCellDisplayText(receivedGridView.FocusedRowHandle, receivedGridView.Columns["No"]));
            try
            {
                if (_ROW_INDEX < receivedGridView.RowCount)
                {
                    row = _ROW_INDEX;
                }
                if (row != -1)
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
            { }
        }

    }
}