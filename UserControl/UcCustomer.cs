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

namespace PowerPOS
{
    public partial class UcCustomer : DevExpress.XtraEditors.XtraUserControl
    {

        DataTable _TABLE_CUSTOMER;

        public UcCustomer()
        {
            InitializeComponent();
        }

        private void UcCustomer_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            DataTable dt;
            DataRow row;
            int i, a;
            _TABLE_CUSTOMER = Util.DBQuery(string.Format(@"SELECT * FROM Customer
                WHERE Firstname LIKE '%{0}%'
                OR Lastname LIKE '%{0}%'
                OR Nickname LIKE '%{0}%'
                OR CitizenID LIKE '%{0}%'
                OR CardNo LIKE '%{0}%'
                OR Mobile LIKE '%{0}%'
                OR ShopName LIKE '%{0}%'
                ",
                txtSearch.Text.Trim()));


            customerGridView.OptionsBehavior.AutoPopulateColumns = false;
            customerGridControl.MainView = customerGridView;

            dt = new DataTable();
            for (i = 0; i < ((ColumnView)customerGridControl.MainView).Columns.Count; i++)
            {
                dt.Columns.Add(customerGridView.Columns[i].FieldName);
            }

            for (a = 0; a < _TABLE_CUSTOMER.Rows.Count; a++)
            {
                var mobile = (_TABLE_CUSTOMER.Rows[a]["Mobile"].ToString().Length == 10) ? _TABLE_CUSTOMER.Rows[a]["Mobile"].ToString().Substring(0, 3) + "-" +
                    _TABLE_CUSTOMER.Rows[a]["Mobile"].ToString().Substring(3, 4) + "-" +
                    _TABLE_CUSTOMER.Rows[a]["Mobile"].ToString().Substring(7, 3)
                    : _TABLE_CUSTOMER.Rows[a]["Mobile"].ToString();

                var citizen = (_TABLE_CUSTOMER.Rows[a]["CitizenID"].ToString().Length == 13) ? _TABLE_CUSTOMER.Rows[a]["CitizenID"].ToString().Substring(0, 1) + "-" +
                    _TABLE_CUSTOMER.Rows[a]["CitizenID"].ToString().Substring(1, 4) + "-" +
                    _TABLE_CUSTOMER.Rows[a]["CitizenID"].ToString().Substring(5, 5) + "-" +
                    _TABLE_CUSTOMER.Rows[a]["CitizenID"].ToString().Substring(10, 2) + "-" +
                    _TABLE_CUSTOMER.Rows[a]["CitizenID"].ToString().Substring(12, 1)
                    : _TABLE_CUSTOMER.Rows[a]["CitizenID"].ToString();

                row = dt.NewRow();
                row[0] = (a + 1) * 1;
                row[1] = _TABLE_CUSTOMER.Rows[a]["Customer"].ToString();
                row[2] = _TABLE_CUSTOMER.Rows[a]["firstname"].ToString().Split(' ')[0];
                row[3] = _TABLE_CUSTOMER.Rows[a]["lastname"].ToString().Split(' ')[0];
                row[4] = _TABLE_CUSTOMER.Rows[a]["Nickname"].ToString();
                row[5] = _TABLE_CUSTOMER.Rows[a]["ShopName"].ToString();
                row[6] = _TABLE_CUSTOMER.Rows[a]["SellPrice"].ToString() == "0" ? "ปลีก" : "ส่ง " + _TABLE_CUSTOMER.Rows[a]["SellPrice"].ToString();
                row[7] = _TABLE_CUSTOMER.Rows[a]["Credit"].ToString() == "0" ? "-" : _TABLE_CUSTOMER.Rows[a]["Credit"].ToString() + " วัน";
                row[8] = _TABLE_CUSTOMER.Rows[a]["CardNo"].ToString();
                row[9] = mobile;
                row[10] = citizen;
                dt.Rows.Add(row);
            }

            customerGridControl.DataSource = dt;

            //lblRecords.Text = dt.Rows.Count.ToString("#,##0");
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            FmAddCustomer ul = new FmAddCustomer();
            var result = ul.ShowDialog(this);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            LoadData();
        }

        private void customerGridControl_DoubleClick(object sender, EventArgs e)
        {
            if (customerGridView.RowCount > 0)
            {

                FmAddCustomer ul = new FmAddCustomer();
                ul.LoadCustomerData(sender, e, "Mobile", customerGridView.GetRowCellDisplayText(customerGridView.FocusedRowHandle, customerGridView.Columns["Mobile"]).ToString().Replace("-", ""));
                var result = ul.ShowDialog(this);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
    }
}
