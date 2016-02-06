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
                AND {0}", (cbPaid.Checked) ? "sh.payType = 1 AND cc.paidPrice IS NOT NULL" : "sh.payType = 0 AND (cc.paidPrice IS NULL OR cc.paidPrice = 0)",
                Convert.ToDateTime(dtpStartDate.Value).ToString("yyyy-MM-dd"), Convert.ToDateTime(dtpEndDate.Value).ToString("yyyy-MM-dd"),
                txtSearch.Text.Trim()
            ));

            creditGridview.OptionsBehavior.AutoPopulateColumns = false;
            creditGridControl.MainView = creditGridview;

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
            }

            creditGridControl.DataSource = dt;

            lblListCount.Text = creditGridview.RowCount.ToString() + " รายการ";
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

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }
    }
}
