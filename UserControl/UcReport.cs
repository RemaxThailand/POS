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
using System.Drawing.Text;
using System.Drawing.Imaging;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace PowerPOS
{
    public partial class UcReport : DevExpress.XtraEditors.XtraUserControl
    {
        DataTable _TABLE_REPORT;
        public static string sellNo;

        public UcReport()
        {
            InitializeComponent();
        }

        private void UcReport_Load(object sender, EventArgs e)
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
            try {
                if (Param.MemberType != "Shop" && Param.MemberType != "Event" && Param.MemberType != "Agent")
                {
                    pictureEdit1.Visible = true;
                    ptbShop.Visible = false;
                    pictureEdit1.Image = new Bitmap(Properties.Resources.daily);
                }
                else
                {
                    pictureEdit1.Visible = false;
                    ptbShop.Visible = true;
                    pictureEdit1.Image = new Bitmap(Properties.Resources.dailyShop);
                }
                    //_TABLE_REPORT = Util.DBQuery(string.Format(@"
                    //     SELECT h.SellDate, h.SellNo, c.Firstname, c.Lastname, c.Mobile, b.sellPrice  - b.cost Profit, b.sellPrice TotalPrice, h.Paid
                    //     FROM SellHeader h
                    //     LEFT JOIN Customer c
                    //     ON h.Customer = c.Customer
                    //    LEFT JOIN  (SELECT Product, SUM(cost) cost, SUM(sellPrice) sellPrice ,SellNo FROM Barcode WHERE SellBy = '{1}' GROUP BY SellNo) b 
                    //     ON h.sellNo = b.sellNo
                    //     WHERE h.SellDate LIKE '{0}%'
                    //       AND h.Customer NOT IN ('000001','000002')
                    //       AND b.sellPrice IS NOT NULL   
                    //     ORDER BY h.SellDate DESC
                    // ", dtpDate.Value.ToString("yyyy-MM-dd"), Param.UserId ));

                 _TABLE_REPORT = Util.DBQuery(string.Format(@"
                 SELECT strftime('%d-%m-%Y %H:%M:%S', h.SellDate) SellDate, h.SellNo, c.Firstname, c.Lastname, c.Mobile, h.Profit, h.TotalPrice, h.Paid, h.payType
               FROM SellHeader h
                LEFT JOIN Customer c
                ON h.Customer = c.Customer 
                WHERE h.SellDate LIKE '{0}%'
                  AND h.Customer NOT IN ('000001','000002')
                  AND(h.Comment <> 'คืนสินค้า' OR h.Comment IS Null)
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
                    var mobile = (_TABLE_REPORT.Rows[a]["Mobile"].ToString().Length == 10) ? _TABLE_REPORT.Rows[a]["Mobile"].ToString().Substring(0, 3) + "-" +
                        _TABLE_REPORT.Rows[a]["Mobile"].ToString().Substring(3, 4) + "-" +
                        _TABLE_REPORT.Rows[a]["Mobile"].ToString().Substring(7, 3)
                        : _TABLE_REPORT.Rows[a]["Mobile"].ToString();

                    //DateTime date = Convert.ToDateTime(_TABLE_REPORT.Rows[a]["SellDate"].ToString());
                    //string datetime = date.ToString("MM/dd/yyyy");
                    row = dt.NewRow();
                    row[0] = (a + 1) * 1;
                    row[1] = _TABLE_REPORT.Rows[a]["SellDate"].ToString();
                    row[2] = _TABLE_REPORT.Rows[a]["SellNo"].ToString();
                    row[3] = _TABLE_REPORT.Rows[a]["Firstname"].ToString() + " " + _TABLE_REPORT.Rows[a]["Lastname"].ToString();
                    row[4] = mobile == "" ? "-" : mobile;
                    row[5] = int.Parse(_TABLE_REPORT.Rows[a]["TotalPrice"].ToString()).ToString("#,##0");
                    if (_TABLE_REPORT.Rows[a]["payType"].ToString() == "1")
                    {
                        row[6] = "ชำระเงินแล้ว";
                    }
                    else
                    {
                        row[6] = "ยังไม่ชำระเงิน";
                    }
                    dt.Rows.Add(row);
                    sumPrice += double.Parse(_TABLE_REPORT.Rows[a]["TotalPrice"].ToString());
                    sumProfit += double.Parse(_TABLE_REPORT.Rows[a]["Profit"].ToString());
                }

                reportGridControl.DataSource = dt;

                lblListCount.Text = reportGridView.RowCount.ToString("#,##0") + " รายการ";
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");


                dtQty = Util.DBQuery(string.Format(@"SELECT IFNULL(SUM(d.Quantity),0) QTY FROM SellHeader h
                            LEFT JOIN SellDetail d
                            ON h.SellNo = d.SellNo 
                            WHERE h.SellDate LIKE '{0}%'
                              AND h.Customer NOT IN ('000001','000002')
                              AND(h.Comment <> 'คืนสินค้า' OR h.Comment IS Null)
                            ORDER BY SellDate DESC
                         ", dtpDate.Value.ToString("yyyy-MM-dd"), Param.UserId));
                lblProductCount.Text = int.Parse(dtQty.Rows[0]["QTY"].ToString()).ToString("#,##0") + " ชิ้น";

                try
                {
                    dt = Util.DBQuery(string.Format(@"SELECT SUM(h.totalPrice)  Total,COUNT(DISTINCT DATE(h.SellDate)) CountDate,SUM(h.totalPrice)/COUNT(DISTINCT DATE(h.SellDate)) AVG
                        FROM SellHeader h
                        --LEFT JOIN (SELECT Product, SUM(cost) cost, SUM(sellPrice) sellPrice ,SellNo FROM Barcode WHERE SellBy = '{2}' GROUP BY Product,SellNo) b 
                        --ON h.sellNo = b.sellNo
                        WHERE SUBSTR(h.SellDate,1,10) BETWEEN '{0}' AND '{1}'
                    ", Convert.ToDateTime(dtpDate.Value.AddDays(-30)).ToString("yyyy-MM-dd"), Convert.ToDateTime(dtpDate.Value.AddDays(1)).ToString("yyyy-MM-dd"), Param.UserId));
                            var avg30 = double.Parse(dt.Rows[0]["AVG"].ToString());

                    dt = Util.DBQuery(string.Format(@"
                    SELECT IFNULL(SUM(b.sellPrice),0) TotalPrice FROM SellHeader h 
                          LEFT JOIN (SELECT Product, SUM(cost) cost, SUM(sellPrice) sellPrice ,SellNo FROM Barcode WHERE SellBy = '{14}' GROUP BY Product,SellNo) b 
                          ON h.sellNo = b.sellNo WHERE SUBSTR(h.SellDate, 1, 10) = '{0}'
                    UNION ALL SELECT IFNULL(SUM(b.sellPrice),0) TotalPrice FROM SellHeader h 
                          LEFT JOIN (SELECT Product, SUM(cost) cost, SUM(sellPrice) sellPrice ,SellNo FROM Barcode WHERE SellBy = '{14}' GROUP BY Product,SellNo) b 
                          ON h.sellNo = b.sellNo WHERE SUBSTR(h.SellDate, 1, 10) = '{1}'
                    UNION ALL SELECT IFNULL(SUM(b.sellPrice),0) TotalPrice FROM SellHeader h 
                          LEFT JOIN (SELECT Product, SUM(cost) cost, SUM(sellPrice) sellPrice ,SellNo FROM Barcode WHERE SellBy = '{14}' GROUP BY Product,SellNo) b 
                          ON h.sellNo = b.sellNo WHERE SUBSTR(h.SellDate, 1, 10) = '{2}'
                    UNION ALL SELECT IFNULL(SUM(b.sellPrice),0) TotalPrice FROM SellHeader h 
                          LEFT JOIN (SELECT Product, SUM(cost) cost, SUM(sellPrice) sellPrice ,SellNo FROM Barcode WHERE SellBy = '{14}' GROUP BY Product,SellNo) b 
                          ON h.sellNo = b.sellNo WHERE SUBSTR(h.SellDate, 1, 10) = '{3}'
                    UNION ALL SELECT IFNULL(SUM(b.sellPrice),0) TotalPrice FROM SellHeader h 
                          LEFT JOIN (SELECT Product, SUM(cost) cost, SUM(sellPrice) sellPrice ,SellNo FROM Barcode WHERE SellBy = '{14}' GROUP BY Product,SellNo) b 
                          ON h.sellNo = b.sellNo WHERE SUBSTR(h.SellDate, 1, 10) = '{4}'
                    UNION ALL SELECT IFNULL(SUM(b.sellPrice),0) TotalPrice FROM SellHeader h 
                          LEFT JOIN (SELECT Product, SUM(cost) cost, SUM(sellPrice) sellPrice ,SellNo FROM Barcode WHERE SellBy = '{14}' GROUP BY Product,SellNo) b 
                          ON h.sellNo = b.sellNo WHERE SUBSTR(h.SellDate, 1, 10) = '{5}'
                    UNION ALL SELECT IFNULL(SUM(b.sellPrice),0) TotalPrice FROM SellHeader h 
                          LEFT JOIN (SELECT Product, SUM(cost) cost, SUM(sellPrice) sellPrice ,SellNo FROM Barcode WHERE SellBy = '{14}' GROUP BY Product,SellNo) b 
                          ON h.sellNo = b.sellNo WHERE SUBSTR(h.SellDate, 1, 10) = '{6}'
                    UNION ALL SELECT IFNULL(SUM(b.sellPrice),0) TotalPrice FROM SellHeader h 
                          LEFT JOIN (SELECT Product, SUM(cost) cost, SUM(sellPrice) sellPrice ,SellNo FROM Barcode WHERE SellBy = '{14}' GROUP BY Product,SellNo) b 
                          ON h.sellNo = b.sellNo WHERE SUBSTR(h.SellDate, 1, 10) = '{7}'
                    UNION ALL SELECT IFNULL(SUM(b.sellPrice),0) TotalPrice FROM SellHeader h 
                          LEFT JOIN (SELECT Product, SUM(cost) cost, SUM(sellPrice) sellPrice ,SellNo FROM Barcode WHERE SellBy = '{14}' GROUP BY Product,SellNo) b 
                          ON h.sellNo = b.sellNo WHERE SUBSTR(h.SellDate, 1, 10) = '{8}'
                    UNION ALL SELECT IFNULL(SUM(b.sellPrice),0) TotalPrice FROM SellHeader h 
                          LEFT JOIN (SELECT Product, SUM(cost) cost, SUM(sellPrice) sellPrice ,SellNo FROM Barcode WHERE SellBy = '{14}' GROUP BY Product,SellNo) b 
                          ON h.sellNo = b.sellNo WHERE SUBSTR(h.SellDate, 1, 10) = '{9}'
                    UNION ALL SELECT IFNULL(SUM(b.sellPrice),0) TotalPrice FROM SellHeader h 
                          LEFT JOIN (SELECT Product, SUM(cost) cost, SUM(sellPrice) sellPrice ,SellNo FROM Barcode WHERE SellBy = '{14}' GROUP BY Product,SellNo) b 
                          ON h.sellNo = b.sellNo WHERE SUBSTR(h.SellDate, 1, 10) = '{10}'
                    UNION ALL SELECT IFNULL(SUM(b.sellPrice),0) TotalPrice FROM SellHeader h 
                          LEFT JOIN (SELECT Product, SUM(cost) cost, SUM(sellPrice) sellPrice ,SellNo FROM Barcode WHERE SellBy = '{14}' GROUP BY Product,SellNo) b 
                          ON h.sellNo = b.sellNo WHERE SUBSTR(h.SellDate, 1, 10) = '{11}'
                    UNION ALL SELECT IFNULL(SUM(b.sellPrice),0) TotalPrice FROM SellHeader h 
                          LEFT JOIN (SELECT Product, SUM(cost) cost, SUM(sellPrice) sellPrice ,SellNo FROM Barcode WHERE SellBy = '{14}' GROUP BY Product,SellNo) b 
                          ON h.sellNo = b.sellNo WHERE SUBSTR(h.SellDate, 1, 10) = '{12}'
                    UNION ALL SELECT IFNULL(SUM(b.sellPrice),0) TotalPrice FROM SellHeader h 
                          LEFT JOIN (SELECT Product, SUM(cost) cost, SUM(sellPrice) sellPrice ,SellNo FROM Barcode WHERE SellBy = '{14}' GROUP BY Product,SellNo) b 
                          ON h.sellNo = b.sellNo WHERE SUBSTR(h.SellDate, 1, 10) = '{13}'
                ", dtpDate.Value.AddDays(-13).ToString("yyyy-MM-dd"), dtpDate.Value.AddDays(-12).ToString("yyyy-MM-dd"), dtpDate.Value.AddDays(-11).ToString("yyyy-MM-dd")
                     , dtpDate.Value.AddDays(-10).ToString("yyyy-MM-dd"), dtpDate.Value.AddDays(-9).ToString("yyyy-MM-dd"), dtpDate.Value.AddDays(-8).ToString("yyyy-MM-dd")
                     , dtpDate.Value.AddDays(-7).ToString("yyyy-MM-dd"), dtpDate.Value.AddDays(-6).ToString("yyyy-MM-dd"), dtpDate.Value.AddDays(-5).ToString("yyyy-MM-dd")
                     , dtpDate.Value.AddDays(-4).ToString("yyyy-MM-dd"), dtpDate.Value.AddDays(-3).ToString("yyyy-MM-dd"), dtpDate.Value.AddDays(-2).ToString("yyyy-MM-dd")
                     , dtpDate.Value.AddDays(-1).ToString("yyyy-MM-dd"), dtpDate.Value.ToString("yyyy-MM-dd"), Param.UserId));
                    i = 0;
                    double max = 0;
                    List<double> chart = new List<double>();
                    for (i = 0; i < dt.Rows.Count; i++)
                    {
                        chart.Add(double.Parse(dt.Rows[i]["TotalPrice"].ToString()));
                        if (chart[i] > max) max = chart[i];
                    }
                    DrawImage(sumPrice, sumProfit, avg30, max, chart);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void DrawImage(double sumPrice, double sumProfit, double avgPrice, double max, List<double> chart)
        {
            if (Param.MemberType != "Shop" && Param.MemberType != "Event" && Param.MemberType != "Agent")
            {
                pictureEdit1.Visible = true;
                ptbShop.Visible = false;
                pictureEdit1.Image = new Bitmap(Properties.Resources.daily);

                using (Graphics g = Graphics.FromImage(pictureEdit1.Image))
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
                    g.DrawString(measureString, stringFont, drawBrush, (pictureEdit1.Image.Width - stringSize.Width) / 2, 17);

                    stringFont = new Font("DilleniaUPC", 60, FontStyle.Bold);
                    drawBrush = new SolidBrush(ColorTranslator.FromHtml("#263e74"));
                    measureString = "วันที่ " + dtpDate.Value.ToString("dd MMMM yyyy");
                    stringSize = g.MeasureString(measureString, stringFont);
                    g.DrawString(measureString, stringFont, drawBrush, (pictureEdit1.Image.Width - stringSize.Width) / 2, 230);


                    stringFont = new Font("DilleniaUPC", 80, FontStyle.Bold);
                    drawBrush = new SolidBrush(ColorTranslator.FromHtml("#f40c43"));
                    measureString = sumPrice.ToString("#,##0");
                    stringSize = g.MeasureString(measureString, stringFont);
                    g.DrawString(measureString, stringFont, drawBrush, (pictureEdit1.Image.Width - stringSize.Width - 50), 334);

                    measureString = sumProfit.ToString("#,##0");
                    stringSize = g.MeasureString(measureString, stringFont);
                    g.DrawString(measureString, stringFont, drawBrush, (pictureEdit1.Image.Width - stringSize.Width - 50), 428);

                    drawBrush = new SolidBrush(ColorTranslator.FromHtml("#fa3711"));
                    measureString = avgPrice.ToString("#,##0");
                    stringSize = g.MeasureString(measureString, stringFont);
                    g.DrawString(measureString, stringFont, drawBrush, (pictureEdit1.Image.Width - stringSize.Width) / 2, 618);

                    var startX = 48;
                    var startY = 780;
                    var width = 40;
                    var gab = 5;
                    var maxHeight = 250;
                    var whiteBrush = new SolidBrush(ColorTranslator.FromHtml("#ffa1d6"));
                    for (int i = 0; i < chart.Count; i++)
                    {
                        drawBrush = new SolidBrush(ColorTranslator.FromHtml(i != chart.Count - 1 ? "#af18b1" : ((chart[i] > avgPrice) ? "#207a2b" : "#d31a1a")));
                        var h = (int)(chart[i] * maxHeight / max);
                        g.FillRectangle(drawBrush, new Rectangle(startX + ((width + gab) * i), startY + maxHeight - h, width, h));
                        g.DrawRectangle(new Pen(whiteBrush), new Rectangle(startX + ((width + gab) * i), startY + maxHeight - h, width, h));
                    }
                    if (avgPrice > 0)
                    {
                        var y = (int)(avgPrice * maxHeight / max);
                        g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#ffffff")), 1.0f), startX, startY + maxHeight - y, 672, startY + maxHeight - y);
                    }
                    g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#ffffff")), 1.0f), startX, startY + maxHeight, 672, startY + maxHeight);

                }
            }
            else
            {
                pictureEdit1.Visible = false;
                ptbShop.Visible = true;
                ptbShop.Image = new Bitmap(Properties.Resources.dailyShop);

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
                    g.DrawString(measureString, stringFont, drawBrush, (pictureEdit1.Image.Width - stringSize.Width) / 2, 17);

                    stringFont = new Font("DilleniaUPC", 60, FontStyle.Bold);
                    drawBrush = new SolidBrush(ColorTranslator.FromHtml("#263e74"));
                    measureString = "วันที่ " + dtpDate.Value.ToString("dd MMMM yyyy");
                    stringSize = g.MeasureString(measureString, stringFont);
                    g.DrawString(measureString, stringFont, drawBrush, (pictureEdit1.Image.Width - stringSize.Width) / 2, 230);


                    stringFont = new Font("DilleniaUPC", 80, FontStyle.Bold);
                    drawBrush = new SolidBrush(ColorTranslator.FromHtml("#f40c43"));
                    measureString = sumPrice.ToString("#,##0");
                    stringSize = g.MeasureString(measureString, stringFont);
                    g.DrawString(measureString, stringFont, drawBrush, (pictureEdit1.Image.Width - stringSize.Width - 50), 334);
                }
            }
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

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void miPrintReceipt_Click(object sender, EventArgs e)
        {
            if (reportGridView.RowCount > 0)
            {
                //sellNo = reportGridView.GetRowCellDisplayText(reportGridView.FocusedRowHandle, reportGridView.Columns["SaleNo"]);
                if (Param.PrintType == "Y")
                {
                    var cnt = int.Parse(Param.PrintCount.ToString());
                    for (int i = 1; i <= cnt; i++)
                        Util.PrintReceipt(sellNo);
                }
                else if (Param.PrintType == "A")
                {
                    if (MessageBox.Show("คุณต้องการพิมพ์ใบเสร็จรับเงินหรือไม่ ?", "การพิมพ์", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        Util.PrintReceipt(sellNo);
                }
            }
            else
            {
                MessageBox.Show("กรุณาเลือกรายการที่ต้องการพิมพ์ใบเสร็จรับเงิน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void reportGridControl_MouseClick(object sender, MouseEventArgs e)
        {
            sellNo = reportGridView.GetRowCellDisplayText(reportGridView.FocusedRowHandle, reportGridView.Columns["SaleNo"]);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            miPrintReceipt_Click(sender, (e));
        }

        private void reportGridView_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string status = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Status"]);
                if (status == "ยังไม่ชำระเงิน")
                {
                    e.Appearance.BackColor = Color.Salmon;
                    //e.Appearance.BackColor2 = Color.SeaShell;
                }
            }
        }

    }
}
