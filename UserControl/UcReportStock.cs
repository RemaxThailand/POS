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
using System.Threading;
using System.Globalization;
using DevExpress.XtraGrid.Views.Base;
using System.Drawing.Text;

namespace PowerPOS
{
    public partial class UcReportStock : DevExpress.XtraEditors.XtraUserControl
    {
        DataTable _TABLE_REPORT;
        public static string sellNo;

        public UcReportStock()
        {
            InitializeComponent();
        }

        private void UcReportStock_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            LoadData();
        }

        public void LoadData()
        {
            DataTable dt, dtQty;
            DataRow row;
            int i, a;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            try
            {
                ptbShop.Image = new Bitmap(Properties.Resources.Claim);


                _TABLE_REPORT = Util.DBQuery(string.Format(@"
                 SELECT strftime('%d-%m-%Y %H:%M:%S', h.SellDate) SellDate, h.SellNo, c.Firstname, c.Lastname, h.TotalPrice
               FROM SellHeader h
                LEFT JOIN Customer c
                ON h.Customer = c.Customer 
                WHERE h.SellDate LIKE '{0}%'
                  AND h.Customer = 'CL0001'
                  AND (h.Comment <> 'คืนสินค้า' OR h.Comment IS Null)
                ORDER BY SellDate DESC
             ", dtpDate.Value.ToString("yyyy-MM-dd"), Param.UserId));

                reportGridView.OptionsBehavior.AutoPopulateColumns = false;
                reportGridControl.MainView = reportGridView;
                var sumPrice = 0.0;
                var sumProfit = 0.0;
                dt = new DataTable();
                for (i = 0; i < ((ColumnView)reportGridControl.MainView).Columns.Count; i++)
                {
                    dt.Columns.Add(reportGridView.Columns[i].FieldName);
                }
                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");

                for (a = 0; a < _TABLE_REPORT.Rows.Count; a++)
                {
                    row = dt.NewRow();
                    row[0] = (a + 1) * 1;
                    row[1] = Convert.ToDateTime(_TABLE_REPORT.Rows[a]["SellDate"].ToString()).ToLocalTime().ToString("dd-MM-yyyy HH:mm:ss");
                    row[2] = _TABLE_REPORT.Rows[a]["SellNo"].ToString();
                    row[3] = "สินค้าไม่มีในสต็อค";
                    row[4] = int.Parse(_TABLE_REPORT.Rows[a]["TotalPrice"].ToString()).ToString("#,##0");
                    dt.Rows.Add(row);
                    sumPrice += double.Parse(_TABLE_REPORT.Rows[a]["TotalPrice"].ToString());
                }

                reportGridControl.DataSource = dt;

                lblListCount.Text = reportGridView.RowCount.ToString() + " รายการ";
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");


                dtQty = Util.DBQuery(string.Format(@"SELECT IFNULL(SUM(d.Quantity),0) QTY FROM SellHeader h
                            LEFT JOIN SellDetail d
                            ON h.SellNo = d.SellNo 
                              WHERE h.SellDate LIKE '{0}%'
                                              AND h.Customer = 'CL0001'
                                              AND (h.Comment <> 'คืนสินค้า' OR h.Comment IS Null)
                                            ORDER BY SellDate DESC
                         ", dtpDate.Value.ToString("yyyy-MM-dd"), Param.UserId));

                lblProductCount.Text = dtQty.Rows[0]["QTY"].ToString() + " ชิ้น";

                DrawImage(sumPrice, sumProfit);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

      
        private void DrawImage(double sumPrice, double sumProfit)
        {
            ptbShop.Visible = true;
            ptbShop.Image = new Bitmap(Properties.Resources.Claim);

            using (Graphics g = Graphics.FromImage(ptbShop.Image))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");

                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.TextRenderingHint = TextRenderingHint.AntiAlias;

                Font stringFont = new Font("DilleniaUPC", 50, FontStyle.Bold);
                SolidBrush drawBrush = new SolidBrush(ColorTranslator.FromHtml("#ffffff"));

                string measureString = Param.ShopName;
                SizeF stringSize = g.MeasureString(measureString, stringFont);
                g.DrawString(measureString, stringFont, drawBrush, (ptbShop.Image.Width - stringSize.Width) / 2, 17);

                stringFont = new Font("DilleniaUPC", 60, FontStyle.Bold);
                drawBrush = new SolidBrush(ColorTranslator.FromHtml("#263e74"));
                measureString = "วันที่ " + dtpDate.Value.ToString("dd MMMM yyyy");
                stringSize = g.MeasureString(measureString, stringFont);
                g.DrawString(measureString, stringFont, drawBrush, (ptbShop.Image.Width - stringSize.Width) / 2, 200);


                stringFont = new Font("DilleniaUPC", 80, FontStyle.Bold);
                drawBrush = new SolidBrush(ColorTranslator.FromHtml("#f40c43"));
                measureString = sumPrice.ToString("#,##0");
                stringSize = g.MeasureString(measureString, stringFont);
                g.DrawString(measureString, stringFont, drawBrush, (ptbShop.Image.Width - stringSize.Width - 50), 300);

            }
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void miDetail_Click(object sender, EventArgs e)
        {
            if (reportGridView.RowCount > 0)
            {
                Param.SellNo = reportGridView.GetRowCellDisplayText(reportGridView.FocusedRowHandle, reportGridView.Columns["SaleNo"]);
                FmSaleDetial frm = new FmSaleDetial();
                frm.Show();
            }
            else
            {
                MessageBox.Show("กรุณาเลือกรายการที่ต้องการดูรายละเอียดการขาย", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void reportGridControl_DoubleClick(object sender, EventArgs e)
        {
            miDetail_Click((sender), e);
        }
    }
}
