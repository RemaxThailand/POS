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
            _TABLE_CREDIT = Util.DBQuery(string.Format(@"SELECT c.firstname||' '||c.lastname name, sh.sellNo, sh.sellDate, sh.totalPrice
                    FROM CreditCustomer cc
                LEFT JOIN SellHeader sh
                ON cc.sellNo = sh.sellNo
                LEFT JOIN Customer c
                ON c.customer = sh.customer
                WHERE (sh.sellNo LIKE '%{3}%' OR c.firstname LIKE '%{3}%' OR c.lastname LIKE '%{3}%')
                AND SUBSTR(sh.SellDate,1,10) BETWEEN '{1}' AND '{2}' 
                AND sh.Customer NOT IN ('000001','000002')
                AND(sh.Comment <> 'คืนสินค้า' OR sh.Comment IS Null)
                AND {0}", (cbPaid.Checked) ? "sh.payType = 1 AND cc.paidPrice IS NOT NULL" : "sh.payType = 0 AND (cc.paidPrice IS NULL OR cc.paidPrice = 0)",
                Convert.ToDateTime(dtpStartDate.Value).ToString("yyyy-MM-dd"), Convert.ToDateTime(dtpEndDate.Value).ToString("yyyy-MM-dd"),
                txtSearch.Text.Trim()
            ));

            creditGridview.OptionsBehavior.AutoPopulateColumns = false;
            creditGridControl.MainView = creditGridview;
            btnPaid.Enabled = false;
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
                row[4] = Convert.ToInt32(_TABLE_CREDIT.Rows[a]["totalPrice"]).ToString("#,##0");
                _TOTAL += int.Parse(_TABLE_CREDIT.Rows[a]["totalPrice"].ToString());

                dt.Rows.Add(row);

                btnPaid.Enabled = true;
            }

            creditGridControl.DataSource = dt;

            lblListCount.Text = creditGridview.RowCount.ToString("#,##0") + " รายการ";
            lblProductCount.Text = _TOTAL.ToString("#,##0") + " บาท";

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
