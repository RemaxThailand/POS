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
using DevExpress.XtraNavBar;
using System.Drawing.Text;

namespace PowerPOS
{
    public partial class UcCredit : DevExpress.XtraEditors.XtraUserControl
    {
        DataTable _TABLE_CREDIT;
        int _TOTAL;
        public static string sellNo, price;

        public UcCredit()
        {
            InitializeComponent();
        }

        private void UcCredit_Load(object sender, EventArgs e)
        {
            btnPaid.Enabled = Util.CanAccessScreenDetail("E01");

            LoadData();
        }

        public void LoadData()
        {

            DataTable dt;
            DataRow row;
            int i, a;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            sellNo = "";
            _TOTAL = 0;
            _TABLE_CREDIT = Util.DBQuery(string.Format(@"SELECT c.firstname||' '||c.lastname name, sh.sellNo, strftime('%d-%m-%Y %H:%M:%S', sh.SellDate) sellDate, sh.totalPrice, strftime('%d-%m-%Y %H:%M:%S', cc.dueDate) dueDate
                    FROM CreditCustomer cc
                LEFT JOIN SellHeader sh
                ON cc.sellNo = sh.sellNo
                LEFT JOIN Customer c
                ON c.customer = sh.customer
                WHERE (sh.sellNo LIKE '%{3}%' OR c.firstname LIKE '%{3}%' OR c.lastname LIKE '%{3}%')
                AND SUBSTR(sh.SellDate,1,10) BETWEEN '{1}' AND '{2}' 
                AND sh.Customer NOT IN ('000001','000002')
                AND(sh.Comment <> 'คืนสินค้า' OR sh.Comment IS Null)
                AND sh.sellNo NOT LIKE '%CL%'
                AND {0}", (cbPaid.Checked) ? "sh.payType = 1 AND cc.paidPrice IS NOT NULL" : "sh.payType = 0 AND (cc.paidPrice IS NULL OR cc.paidPrice = 0)",
                Convert.ToDateTime(dtpStartDate.Value).ToString("yyyy-MM-dd"), Convert.ToDateTime(dtpEndDate.Value).ToString("yyyy-MM-dd"),
                txtSearch.Text.Trim()
            ));

            creditGridview.OptionsBehavior.AutoPopulateColumns = false;
            creditGridControl.MainView = creditGridview;
            //btnPaid.Enabled = false;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");

            dt = new DataTable();
            for (i = 0; i < ((ColumnView)creditGridControl.MainView).Columns.Count; i++)
            {
                dt.Columns.Add(creditGridview.Columns[i].FieldName);
            }

            for (a = 0; a < _TABLE_CREDIT.Rows.Count; a++)
            {
                //int warranty = int.Parse(_TABLE_CREDIT.Rows[a]["Warranty"].ToString());
                row = dt.NewRow();
                row[0] = (a + 1) * 1;
                row[1] = _TABLE_CREDIT.Rows[a]["name"].ToString();
                row[2] = _TABLE_CREDIT.Rows[a]["sellNo"].ToString();
                row[3] = Convert.ToDateTime(_TABLE_CREDIT.Rows[a]["sellDate"].ToString()).ToLocalTime().ToString("dd-MM-yyyy HH:mm:ss");
                //_TABLE_CREDIT.Rows[a]["SellDate"].ToString();
                //Convert.ToDateTime(_TABLE_CREDIT.Rows[a]["sellDate"].ToString()).ToLocalTime().ToString("dd-MM-yyyy HH:mm:ss");
                row[4] = Convert.ToInt32(_TABLE_CREDIT.Rows[a]["totalPrice"]).ToString("#,##0");
                row[5] = _TABLE_CREDIT.Rows[a]["dueDate"].ToString();
                _TOTAL += int.Parse(_TABLE_CREDIT.Rows[a]["totalPrice"].ToString());

                dt.Rows.Add(row);

                //btnPaid.Enabled = true;
            }

            creditGridControl.DataSource = dt;

            lblListCount.Text = creditGridview.RowCount.ToString("#,##0") + " รายการ";
            lblProductCount.Text = _TOTAL.ToString("#,##0") + " บาท";

        }

        public void LoadDataPaid()
        {

            DataTable dt;
            DataRow row;
            int i, a;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            sellNo = "";
            _TOTAL = 0;
            _TABLE_CREDIT = Util.DBQuery(string.Format(@"SELECT c.firstname||' '||c.lastname name, sh.sellNo, sh.sellDate, sh.totalPrice
                    FROM CreditCustomer cc
                LEFT JOIN SellHeader sh
                ON cc.sellNo = sh.sellNo
                LEFT JOIN Customer c
                ON c.customer = sh.customer
                WHERE (sh.sellNo LIKE '%{1}%' OR c.firstname LIKE '%{1}%' OR c.lastname LIKE '%{1}%')
                AND SUBSTR(sh.SellDate,1,10) LIKE '%{0}%' 
                AND sh.Customer NOT IN ('000001','000002')
                AND(sh.Comment <> 'คืนสินค้า' OR sh.Comment IS Null)
                AND h.sellNo NOT LIKE '%CL%'
                AND payType = 1", Convert.ToDateTime(dtpDate.Value).ToString("yyyy-MM-dd"),  txtSearch.Text.Trim()
            ));

            paidGridView.OptionsBehavior.AutoPopulateColumns = false;
            paidGridControl.MainView = paidGridView;
            btnPaid.Enabled = false;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");

            dt = new DataTable();
            for (i = 0; i < ((ColumnView)paidGridControl.MainView).Columns.Count; i++)
            {
                dt.Columns.Add(paidGridView.Columns[i].FieldName);
            }

            for (a = 0; a < _TABLE_CREDIT.Rows.Count; a++)
            {
                //int warranty = int.Parse(_TABLE_CREDIT.Rows[a]["Warranty"].ToString());
                row = dt.NewRow();
                row[0] = (a + 1) * 1;
                row[1] = _TABLE_CREDIT.Rows[a]["name"].ToString();
                row[2] = _TABLE_CREDIT.Rows[a]["sellNo"].ToString();
                row[3] = _TABLE_CREDIT.Rows[a]["sellDate"].ToString();
                    //Convert.ToDateTime(_TABLE_CREDIT.Rows[a]["sellDate"].ToString()).ToLocalTime().ToString("dd-MM-yyyy HH:mm:ss");
                row[4] = Convert.ToInt32(_TABLE_CREDIT.Rows[a]["totalPrice"]).ToString("#,##0");
                _TOTAL += int.Parse(_TABLE_CREDIT.Rows[a]["totalPrice"].ToString());

                dt.Rows.Add(row);

                btnPaid.Enabled = true;
            }

            paidGridControl.DataSource = dt;

            DrawImage(_TOTAL);

            lblListCount.Text = paidGridView.RowCount.ToString("#,##0") + " รายการ";
            lblProductCount.Text = _TOTAL.ToString("#,##0") + " บาท";

        }

        private void DrawImage(double sumPrice)
        {
            ptbPaid.Visible = true;
            ptbPaid.Image = new Bitmap(Properties.Resources.Credit);

            using (Graphics g = Graphics.FromImage(ptbPaid.Image))
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
                g.DrawString(measureString, stringFont, drawBrush, (ptbPaid.Image.Width - stringSize.Width) / 2, 12);

                stringFont = new Font("DilleniaUPC", 60, FontStyle.Bold);
                drawBrush = new SolidBrush(ColorTranslator.FromHtml("#263e74"));
                measureString = "วันที่ " + dtpDate.Value.ToString("dd MMMM yyyy");
                stringSize = g.MeasureString(measureString, stringFont);
                g.DrawString(measureString, stringFont, drawBrush, (ptbPaid.Image.Width - stringSize.Width) / 2, 190);


                stringFont = new Font("DilleniaUPC", 80, FontStyle.Bold);
                drawBrush = new SolidBrush(ColorTranslator.FromHtml("#f40c43"));
                measureString = sumPrice.ToString("#,##0");
                stringSize = g.MeasureString(measureString, stringFont);
                g.DrawString(measureString, stringFont, drawBrush, (ptbPaid.Image.Width - stringSize.Width - 50), 300);
            }
        }

        private void cbPaid_CheckedChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnPaid_Click(object sender, EventArgs e)
        {
            sellNo = creditGridview.GetRowCellDisplayText(creditGridview.FocusedRowHandle, creditGridview.Columns["sellno"]).ToString();
            price = creditGridview.GetRowCellDisplayText(creditGridview.FocusedRowHandle, creditGridview.Columns["sellPrice"]).ToString();
            if (sellNo != "")
            {
                //sellNo = creditGridview.GetRowCellDisplayText(creditGridview.FocusedRowHandle, creditGridview.Columns["sellno"]).ToString();
                //price = creditGridview.GetRowCellDisplayText(creditGridview.FocusedRowHandle, creditGridview.Columns["sellPrice"]).ToString();
                FmPaidCredit dialog = new FmPaidCredit();

                var result = dialog.ShowDialog(this);
                if (result != DialogResult.OK)
                {

                }

                LoadData();
            }
        }

        private void creditGridControl_DoubleClick(object sender, EventArgs e)
        {
            miDetail_Click((sender), e);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            miPrintReceipt_Click((sender), e);
        }

        private void miDetail_Click(object sender, EventArgs e)
        {
            if (creditGridview.RowCount > 0)
            {
                Param.SellNo = creditGridview.GetRowCellDisplayText(creditGridview.FocusedRowHandle, creditGridview.Columns["sellno"]);
                FmSaleDetial frm = new FmSaleDetial();
                frm.Show();
            }
            else
            {
                MessageBox.Show("กรุณาเลือกรายการที่ต้องการดูรายละเอียดการขาย", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void navBarControl1_MouseDown(object sender, MouseEventArgs e)
        {
            NavBarHitInfo hInfo = ((NavBarControl)sender).CalcHitInfo(e.Location);
            if (hInfo.InGroupCaption && !hInfo.InGroupButton)
                hInfo.Group.Expanded = !hInfo.Group.Expanded;
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            LoadDataPaid();
        }

        private void navBarControl1_ActiveGroupChanged(object sender, NavBarGroupEventArgs e)
        {
            if (navBarControl1.ActiveGroup.Caption == "ข้อมูลลูกหนี้")
            {
                LoadData();
                creditGridControl.Visible = true;
                paidGridControl.Visible = false;
            }
            else
            {
                LoadDataPaid();
                creditGridControl.Visible = false;
                paidGridControl.Visible = true;
            }
        }

        private void btnPrintA4_Click(object sender, EventArgs e)
        {
            Param.Paper = Param.PaperSize;
            FmSelectPrinter fm = new FmSelectPrinter();
            if (fm.ShowDialog(this) == DialogResult.OK)
            {
                Param.PaperSize = "A4";
                miPrintReceipt_Click(sender, (e));
                Param.DevicePrinter = Param.Printer;
                Param.PaperSize = Param.Paper;
            }
        }

        private void miPrintReceipt_Click(object sender, EventArgs e)
        {
            if (creditGridview.RowCount > 0)
            {
                sellNo = creditGridview.GetRowCellDisplayText(creditGridview.FocusedRowHandle, creditGridview.Columns["sellno"]);
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
    }
}
