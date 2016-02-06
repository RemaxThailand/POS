using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;

namespace PowerPOS
{
    public partial class FmSelectCustomer : DevExpress.XtraEditors.XtraForm
    {
        DataTable _TABLE_CUSTOMER;
        public static string Firstname;
        public static string Lastname;
        public static string Nickname;
        public static string tel;

        public FmSelectCustomer()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt;
            DataRow row;
            int i, a;
            _TABLE_CUSTOMER = Util.DBQuery(string.Format(@"SELECT Customer, Firstname, Lastname, IFNULL(Nickname, '') Nickname, IFNULL(CardNo, '') CardNo, IFNULL(CitizenID, '') CitizenID, 
                    IFNULL(Mobile, '') Mobile, IFNULL(Sex, '') Sex, IFNULL(Birthday, '') Birthday, SellPrice
                    FROM Customer
                    WHERE Firstname LIKE '%{0}%'
                    OR Lastname LIKE '%{0}%'
                    OR Nickname LIKE '%{0}%'
                    OR CardNo LIKE '%{0}%'
                    OR CitizenID LIKE '%{0}%'
                    OR Mobile LIKE '%{0}%'
                    LIMIT 10", txtSearch.Text.Trim()));

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
                row[1] = _TABLE_CUSTOMER.Rows[a]["Customer"].ToString().Split(' ')[0];
                row[2] = _TABLE_CUSTOMER.Rows[a]["Firstname"].ToString().Split(' ')[0];
                row[3] = _TABLE_CUSTOMER.Rows[a]["Lastname"].ToString().Split(' ')[0];
                row[4] = _TABLE_CUSTOMER.Rows[a]["Nickname"].ToString();
                row[5] = _TABLE_CUSTOMER.Rows[a]["CardNo"].ToString();
                row[6] = mobile;
                row[7] = citizen;
                row[8] = _TABLE_CUSTOMER.Rows[a]["SellPrice"].ToString();
                row[9] = _TABLE_CUSTOMER.Rows[a]["Birthday"].ToString();
                row[10] = _TABLE_CUSTOMER.Rows[a]["Sex"].ToString();
                dt.Rows.Add(row);
            }

            customerGridControl.DataSource = dt;
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && txtSearch.Text.Trim() != "")
            {
                btnSearch_Click(sender, e);
            }
        }

        private void customerGridControl_DoubleClick(object sender, EventArgs e)
        {
            if (customerGridView.RowCount > 0)
            {
                try
                {
                    var name = (customerGridView.GetRowCellDisplayText(customerGridView.FocusedRowHandle, customerGridView.Columns["ID"]).ToString() == "000000") ? "ลูกค้าทั่วไป" :
                        customerGridView.GetRowCellDisplayText(customerGridView.FocusedRowHandle, customerGridView.Columns["Firstname"]).ToString() +
                        ((customerGridView.GetRowCellDisplayText(customerGridView.FocusedRowHandle, customerGridView.Columns["Nickname"]).ToString() != "") ? " (" + customerGridView.GetRowCellDisplayText(customerGridView.FocusedRowHandle, customerGridView.Columns["Nickname"]).ToString() + ")" : "");
                    Param.SelectCustomerId = customerGridView.GetRowCellDisplayText(customerGridView.FocusedRowHandle, customerGridView.Columns["ID"]).ToString();
                    Param.SelectCustomerName = name;

                    Param.SelectCustomerAge = customerGridView.GetRowCellDisplayText(customerGridView.FocusedRowHandle, customerGridView.Columns["Birthday"]).ToString() == "" ? 0 : (int)DateTime.Today.Subtract(Convert.ToDateTime(customerGridView.GetRowCellDisplayText(customerGridView.FocusedRowHandle, customerGridView.Columns["Birthday"]).ToString()).Date).TotalDays / 365;
                    //Param.SelectCustomerAge = Param.SelectCustomerAge == DateTime.Today.Year ? 0 : Param.SelectCustomerAge;
                    Param.SelectCustomerSex = customerGridView.GetRowCellDisplayText(customerGridView.FocusedRowHandle, customerGridView.Columns["Sex"]).ToString();
                    Param.SelectCustomerSellPrice = int.Parse(customerGridView.GetRowCellDisplayText(customerGridView.FocusedRowHandle, customerGridView.Columns["SellPrice"]).ToString());

                    Firstname = customerGridView.GetRowCellDisplayText(customerGridView.FocusedRowHandle, customerGridView.Columns["Firstname"]).ToString();
                    Lastname = customerGridView.GetRowCellDisplayText(customerGridView.FocusedRowHandle, customerGridView.Columns["Lastname"]).ToString();
                    Nickname = customerGridView.GetRowCellDisplayText(customerGridView.FocusedRowHandle, customerGridView.Columns["Nickname"]).ToString();
                    tel = customerGridView.GetRowCellDisplayText(customerGridView.FocusedRowHandle, customerGridView.Columns["Mobile"]).ToString();

                    this.DialogResult = DialogResult.OK;
                    this.Dispose();
                }
                catch
                {

                }
            }
        }
    }
}