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
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using System.IO;
using System.Threading;

namespace PowerPOS
{
    public partial class UcStock : DevExpress.XtraEditors.XtraUserControl
    {
        private bool _FIRST_LOAD;
        private int _QTY = 0;
        private int _RECEIVED = 0;
        DataTable _TABLE_STOCK;
        public static int printType = 0;
        public static string productNo;
        string _STREAM_IMAGE_URL;
        //public static string ProductName;
        //private ProductEntity productEntity;

        public UcStock()
        {
            InitializeComponent();
        }

        private void UcStock_Load(object sender, EventArgs e)
        {
            _FIRST_LOAD = true;
            LoadData();
            txtBarcode.Select();
            
            //cbbPrintType.SelectedIndex = 0;
        }

        public void LoadData()
        {
            _FIRST_LOAD = false;
            SearchData();
        }

        private void SearchData()
        {
            DataTable dt;
            DataRow row;
            int i, a;
            if (!_FIRST_LOAD)
            {
                _QTY = 0;
                _RECEIVED = 0;
                _TABLE_STOCK = Util.DBQuery(string.Format(@"SELECT DISTINCT p.sku,p.Image,b.Product, p.Name, COUNT(*) ProductCount, IFNULL(r.Stock, 0) Stock, bn.Name Brand, c.Name Category
                    FROM Barcode b
                        LEFT JOIN Product p
                            ON b.Product = p.Product
                        LEFT JOIN ( 
                                SELECT DISTINCT Product, COUNT(*) Stock
                                FROM Barcode 
                                WHERE ReceivedDate IS NOT NULL 
                                AND (SellBy  = '' OR SellBy  IS NULL)
                                AND inStock = 1 
                                GROUP BY Product
                        ) r
                            ON b.Product = r.Product
                        LEFT JOIN Category c
                            ON p.Category = c.Category
                            AND p.Shop = c.Shop
                        LEFT JOIN Brand bn
                            ON p.Brand = bn.Brand
                            AND p.Shop = bn.Shop
                    WHERE (b.ReceivedDate NOT NULL OR b.ReceivedBy = '{0}') AND (SellBy  = '' OR SellBy  IS NULL)
                    GROUP BY b.Product                   
                UNION ALL
                SELECT p.sku, p.image, p.product, p.name, p.quantity ProductCount, ic.quantity stock, b.name brand, c.name category
                FROM Product p
                    LEFT JOIN InventoryCount ic
                    ON p.product = ic.product
                    LEFT JOIN Brand b
                    ON b.brand = p.brand 
                    LEFT JOIN Category c
                    ON c.category = p.category
                WHERE p.Quantity <> 0
                ", Param.UserId
                ));

                stockGridView.OptionsBehavior.AutoPopulateColumns = false;
                stockGridControl.MainView = stockGridView;

                dt = new DataTable();
                for (i = 0; i < ((ColumnView)stockGridControl.MainView).Columns.Count; i++)
                {
                    dt.Columns.Add(stockGridView.Columns[i].FieldName);
                }

                for (a = 0; a < _TABLE_STOCK.Rows.Count; a++)
                {
                    var progress = float.Parse(_TABLE_STOCK.Rows[a]["Stock"].ToString()) / float.Parse(_TABLE_STOCK.Rows[a]["ProductCount"].ToString()) * 100.0f;
                    row = dt.NewRow();
                    row[0] = (a + 1) * 1;
                    row[1] = _TABLE_STOCK.Rows[a]["product"].ToString();
                    row[2] = _TABLE_STOCK.Rows[a]["Name"].ToString();
                    row[3] = _TABLE_STOCK.Rows[a]["Category"].ToString();
                    row[4] = Convert.ToInt32(_TABLE_STOCK.Rows[a]["ProductCount"]).ToString("#,##0");
                    row[5] = Convert.ToInt32(_TABLE_STOCK.Rows[a]["Stock"]).ToString("#,##0");
                    row[6] = (int)progress;
                    row[7] = _TABLE_STOCK.Rows[a]["Sku"].ToString();
                    dt.Rows.Add(row);
                    _QTY += int.Parse(_TABLE_STOCK.Rows[a]["ProductCount"].ToString());
                    _RECEIVED += int.Parse(_TABLE_STOCK.Rows[a]["Stock"].ToString());
                }

                if (_RECEIVED != 0)
                {
                    progressBarControl1.Properties.PercentView = true;
                    progressBarControl1.Visible = true;
                    progressBarControl1.Properties.Maximum = _QTY;
                    progressBarControl1.EditValue = _RECEIVED;
                }

                RepositoryItemProgressBar ritem = new RepositoryItemProgressBar();
                ritem.Minimum = 0;
                ritem.Maximum = 100;
                ritem.ShowTitle = true;
                stockGridControl.RepositoryItems.Add(ritem);
                stockGridView.Columns["Progress"].ColumnEdit = ritem;

                stockGridControl.DataSource = dt;
                int val = _QTY - _RECEIVED;
                lblListCount.Text = stockGridView.RowCount.ToString() + " รายการ";
                lblProductCount.Text = _QTY.ToString() + " ชิ้น";
                lblReceived.Text = _RECEIVED.ToString() + " ชิ้น";
                lblNoReceived.Text = val.ToString();
            }
            txtBarcode.Select();
        }

        private void DownloadImage(string url, string savePath, string fileName)
        {
            ptbProduct.ImageLocation = url;
            DownloadImage d = new DownloadImage();
            Thread thread = new Thread(() => d.Download(url, savePath, fileName));
            thread.Start();
        }

        private void stockGridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (!_FIRST_LOAD)
            {
                if (stockGridView.RowCount > 0)
                {
                    try
                    {
                        Param.ProductId = stockGridView.GetRowCellDisplayText(stockGridView.FocusedRowHandle, stockGridView.Columns["Product"]);
                        ptbProduct.Image = null;

                        var filename = @"Resource/Images/Product/" + Param.ProductId + ".jpg";
                        DataTable dt = Util.DBQuery(string.Format("SELECT Image FROM Product WHERE product = '{0}'", Param.ProductId));

                        _STREAM_IMAGE_URL = Param.ImagePath + "/" + stockGridView.GetRowCellDisplayText(stockGridView.FocusedRowHandle, stockGridView.Columns["Sku"]) + "/" + dt.Rows[0]["Image"].ToString().Split(',')[0];

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

        private void btnNewCount_Click(object sender, EventArgs e)
        {
            if (stockGridView.RowCount > 0)
            {
                if (MessageBox.Show("คุณแน่ใจหรือไม่ ที่จะเริ่มนับสต็อกสินค้าใหม่ ?", "ยืนยันการนับสต็อกสินค้า", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Util.DBExecute(string.Format(@"UPDATE Barcode SET inStock = 0 ,Sync = 1 WHERE inStock = 1 AND (SellDate IS NULL OR SellDate = '') "));
                    SearchData();
                    progressBarControl1.EditValue = 0;
                    //lblStatus.Text = "";
                }
            }
        }


        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                {
                    Param.BarcodeNo = txtBarcode.Text;

                    DataTable dt = Util.DBQuery(string.Format(@"SELECT p.product, p.Image, IFNULL(SellDate, '') SellDate, b.inStock 
                    FROM Barcode b 
                        LEFT JOIN Product p 
                        ON b.product = p.product
                    WHERE b.Barcode = '{0}'  AND p.Shop = '{1}' AND b.SellDate IS NULL", 
                     txtBarcode.Text, Param.ShopId));

                    //lblStatus.Visible = true;


                    if (dt.Rows.Count == 0)
                    {
                        dt = Util.DBQuery(string.Format(@"SELECT Barcode FROM Product WHERE Barcode LIKE '%{0}%' OR Name LIKE '%{0}%'", txtBarcode.Text));
                        if (dt.Rows.Count == 0)
                        {
                            dt = Util.DBQuery(string.Format(@"SELECT Product, Barcode FROM Product WHERE SKU LIKE '%{0}%'", txtBarcode.Text));
                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("ไม่พบข้อมูลสินค้าชิ้นนี้ในระบบ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                Param.status = "Stock";
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
                            Param.status = "Stock";
                            FmSelectProduct frm = new FmSelectProduct();
                            var result = frm.ShowDialog(this);
                            if (result == System.Windows.Forms.DialogResult.OK)
                            {
                                LoadData();
                                txtBarcode.Text = "";
                            }

                        }
                    }
                    else
                    {
                        Param.ProductId = dt.Rows[0]["product"].ToString();

                        if (dt.Rows[0]["inStock"].ToString() != "False")
                        {
                            lblStatus.ForeColor = Color.Red;
                            //lblStatus.Text = "เคยตรวจสอบสินค้าชิ้นนี้แล้ว";
                            MessageBox.Show("เคยตรวจสอบสินค้าชิ้นนี้แล้ว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            SearchData();
                        }
                        else
                        {
                            Util.DBExecute(string.Format(@"UPDATE Barcode SET inStock = 1, Sync = 1 WHERE Barcode = '{0}'", txtBarcode.Text));
                            SearchData();

                            lblStatus.ForeColor = Color.Green;
                            lblStatus.Text = "ตรวจสอบสินค้าเรียบร้อยแล้ว";
                        }

                    }
                    txtBarcode.Text = "";
                    txtBarcode.Focus();
                }
            }
            catch { }
        }
    }
}
