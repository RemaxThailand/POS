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

namespace PowerPOS
{
    public partial class FmEmployee : DevExpress.XtraEditors.XtraForm
    {
        public FmEmployee()
        {
            InitializeComponent();
            loadData();
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            employeeGroupGridview.AddNewRow();
        }

        private void employeeGroupGridview_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadEmployeeData();
        }

        private void loadData()
        {
            var dt = Util.SqlCeQuery("SELECT * FROM EmployeeType WHERE shop = '" + Param.ShopId + "' ORDER BY orderLevel*1, addDate");
            employeeGroupGridControl.DataSource = dt;
            btnDeleteEmployeeType.Enabled = dt.Rows.Count > 0;
        }

        private void loadEmployeeData()
        {
            DataRow dr = employeeGroupGridview.GetDataRow(employeeGroupGridview.GetSelectedRows()[0]);
            var dt = Util.SqlCeQuery("SELECT *, 'passtmp' passtmp FROM Employee WHERE shop = '" + Param.ShopId + "' AND employeeType = '"+ dr["id"] + "' ORDER BY addDate");
            employeeGridControl.DataSource = dt;
            btnDelete.Enabled = dt.Rows.Count > 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            employeeGridView.AddNewRow();
        }

        private void employeeGroupGridview_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (employeeGroupGridview.GetSelectedRows()[0] >= 0 && e.Value.ToString().Trim() != "")
            {
                DataRow dr = employeeGroupGridview.GetDataRow(employeeGroupGridview.GetSelectedRows()[0]);
                Util.SqlCeExecute("UPDATE EmployeeType SET name = '" + e.Value + "', updateDate = GETDATE() WHERE id = '" + dr["id"] + "'");
            }
            else
            {
                var dt = Util.SqlCeQuery("SELECT MAX(id*1)+1 maxId FROM EmployeeType WHERE shop = '" + Param.ShopId + "'");
                Util.SqlCeExecute("INSERT EmployeeType (shop, id, name, orderLevel, active, addDate) VALUES ('" + Param.ShopId + "', " + dt.Rows[0]["maxId"].ToString() + ", '" + e.Value + "', 99, 1, GETDATE())");
                loadData();
            }
        }

        private void employeeGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var data = e.Value;
            var name = e.Column.FieldName;
            bool pass = true;
            if (name == "status")
            {
                data = (bool)e.Value == true ? 1 : 0;
            }
            else if (name == "passtmp")
            {
                data = Util.MD5String(e.Value.ToString());
                name = "password";
            }
            else if (name == "username")
            {
                DataTable dt = Util.SqlCeQuery("SELECT COUNT(*) cnt FROM Employee WHERE username = '" + e.Value.ToString() + "'");
                pass = dt.Rows[0]["cnt"].ToString() == "0";
                if (!pass)
                {
                    MessageBox.Show("ไม่สามารถเพิ่มข้อมูลได้\nเนื่องจากมีชื่อผู้ใช้ "+ e.Value.ToString() + " ในระบบแล้วค่ะ", "มีข้อผิดพลาดเกิดขึ้น", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            if (pass)
            {
                if (employeeGridView.GetSelectedRows()[0] >= 0)
                {
                    DataRow dr = employeeGridView.GetDataRow(employeeGridView.GetSelectedRows()[0]);
                    Util.SqlCeExecute("UPDATE Employee SET " + name + " = '" + data + "', updateDate = GETDATE() WHERE employeeId = '" + dr["employeeId"] + "'");
                }
                else
                {
                    DataTable dt = Util.SqlCeQuery("SELECT MAX(employeeId*1)+1 maxId FROM Employee WHERE shop = '" + Param.ShopId + "'");
                    DataRow dr = employeeGroupGridview.GetDataRow(employeeGroupGridview.GetSelectedRows()[0]);
                    var key = (dt.Rows.Count == 0) ? "1" : dt.Rows[0]["maxId"].ToString();
                    Util.SqlCeExecute("INSERT Employee (shop, employeeId, employeeType, status, addDate, loginCount, " + name + ") VALUES ('"
                        + Param.ShopId + "', " + key + ", '" + dr["id"] + "', 1, GETDATE(), 0, '" + data + "')");

                    loadEmployeeData();
                    employeeGridView.FocusedRowHandle = employeeGridView.RowCount - 1;
                }
            }
        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            DataRow dr = employeeGroupGridview.GetDataRow(employeeGroupGridview.GetSelectedRows()[0]);
            if (MessageBox.Show("คุณแน่ใจหรือไม่ ?\nที่จะลบข้อมูล " + dr["name"].ToString() + " ออกจากระบบ", "ยืนยันการทำงาน",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Util.SqlCeExecute("DELETE FROM EmployeeType WHERE id = '" + dr["id"] + "'");
                employeeGroupGridview.DeleteSelectedRows();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataRow dr = employeeGridView.GetDataRow(employeeGridView.GetSelectedRows()[0]);
            if (MessageBox.Show("คุณแน่ใจหรือไม่ ?\nที่จะลบข้อมูล " + dr["firstname"].ToString() + " " + dr["lastname"].ToString() + " ออกจากระบบ", "ยืนยันการทำงาน",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Util.SqlCeExecute("DELETE FROM Employee WHERE employeeId = '" + dr["employeeId"] + "'");
                employeeGridView.DeleteSelectedRows();
            }
        }

        private void btnScreen_Click(object sender, EventArgs e)
        {
            FmScreenMapping fm = new FmScreenMapping();
            fm.ShowDialog(this);
        }
    }
}