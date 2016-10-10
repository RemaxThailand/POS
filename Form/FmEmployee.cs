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
using System.Threading;
using System.Globalization;
using Newtonsoft.Json;

namespace PowerPOS
{
    public partial class FmEmployee : DevExpress.XtraEditors.XtraForm
    {
        public FmEmployee()
        {
            InitializeComponent();
            loadData();
        }

        private void FmEmployee_Load(object sender, EventArgs e)
        {
            btnAddEmployeeType.Enabled = Util.CanAccessScreenDetail("A10");
            btnDeleteEmployeeType.Enabled = Util.CanAccessScreenDetail("D10");
            clName.OptionsColumn.AllowEdit = Util.CanAccessScreenDetail("E10");
            btnAdd.Enabled = Util.CanAccessScreenDetail("A20");
            btnDelete.Enabled = Util.CanAccessScreenDetail("D20");
            clFirstname.OptionsColumn.AllowEdit = Util.CanAccessScreenDetail("E20");
            clLastname.OptionsColumn.AllowEdit = Util.CanAccessScreenDetail("E20");
            clNickname.OptionsColumn.AllowEdit = Util.CanAccessScreenDetail("E20");
            clMobile.OptionsColumn.AllowEdit = Util.CanAccessScreenDetail("E20");
            clUsername.OptionsColumn.AllowEdit = Util.CanAccessScreenDetail("E20");
            clPassword.OptionsColumn.AllowEdit = Util.CanAccessScreenDetail("E20");
            btnScreen.Visible = Util.CanAccessScreenDetail("V30");
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
            var dt = Util.DBQuery("SELECT * FROM EmployeeType WHERE shop = '" + Param.ShopId + "' ORDER BY orderLevel*1, addDate");
            employeeGroupGridControl.DataSource = dt;
            btnDeleteEmployeeType.Enabled = dt.Rows.Count > 0 && Util.CanAccessScreenDetail("D10");
        }

        private void loadEmployeeData()
        {
            DataRow dr = employeeGroupGridview.GetDataRow(employeeGroupGridview.GetSelectedRows()[0]);
            var dt = Util.DBQuery("SELECT *, 'passtmp' passtmp FROM Employee WHERE shop = '" + Param.ShopId + "' AND employeeType = '"+ dr["id"] + "' ORDER BY addDate");
            employeeGridControl.DataSource = dt;
            btnDelete.Enabled = dt.Rows.Count > 0 && Util.CanAccessScreenDetail("D20");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            employeeGridView.AddNewRow();
        }

        private void employeeGroupGridview_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:ss:mm");

            if (employeeGroupGridview.GetSelectedRows()[0] >= 0 && e.Value.ToString().Trim() != "")
            {
                DataRow dr = employeeGroupGridview.GetDataRow(employeeGroupGridview.GetSelectedRows()[0]);
                Util.DBExecute("UPDATE EmployeeType SET name = '" + e.Value + "', updateDate = '" + dtime + "', updateBy = '" + Param.UserId + "', Sync = 1 WHERE id = '" + dr["id"] + "'");
            }
            else
            {
                var dt = Util.DBQuery("SELECT MAX(id*1)+1 maxId FROM EmployeeType WHERE shop = '" + Param.ShopId + "'");
                Util.DBExecute("INSERT INTO EmployeeType (shop, id, name, orderLevel, active, addDate, addBy, sync) VALUES ('" + Param.ShopId + "', " + dt.Rows[0]["maxId"].ToString() + ", '" + e.Value + "', 99, 1, '" + dtime + "', '" + Param.UserId + "', 1)");
                loadData();
            }
        }

        private void employeeGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:ss:mm");
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
                DataTable dt = Util.DBQuery("SELECT COUNT(*) cnt FROM Employee WHERE username = '" + e.Value.ToString() + "'");
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
                    Util.DBExecute("UPDATE Employee SET " + name + " = '" + data + "', updateDate = '" + dtime +"', updateBy = '" + Param.UserId + "', Sync = 1 WHERE employeeId = '" + dr["employeeId"] + "'");
                }
                else
                {
                    DataTable dt = Util.DBQuery("SELECT MAX(employeeId*1)+1 maxId FROM Employee WHERE shop = '" + Param.ShopId + "'");
                    DataRow dr = employeeGroupGridview.GetDataRow(employeeGroupGridview.GetSelectedRows()[0]);
                    var key = (dt.Rows.Count == 0) ? "1" : dt.Rows[0]["maxId"].ToString();
                    Util.DBExecute("INSERT INTO Employee (shop, employeeId, employeeType, status, addDate, addBy , loginCount, " + name + ", Sync) VALUES ('"
                        + Param.ShopId + "', " + key + ", '" + dr["id"] + "', 1, '" + dtime + "', '" + Param.UserId + "', 0, '" + data + "', 1)");

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
                Util.DBExecute("DELETE FROM EmployeeType WHERE id = '" + dr["id"] + "'");
                //employeeGroupGridview.DeleteSelectedRows();

                try
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/employee/DeleteType",
                    string.Format("shop={0}&id={1}", Param.ApiShopId, dr["id"])));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }

                }
                catch (Exception ex)
                {
                    Util.WriteErrorLog(ex.Message);
                    Util.WriteErrorLog(ex.StackTrace);
                }

                employeeGroupGridview.DeleteSelectedRows();

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataRow dr = employeeGridView.GetDataRow(employeeGridView.GetSelectedRows()[0]);
            if (MessageBox.Show("คุณแน่ใจหรือไม่ ?\nที่จะลบข้อมูล " + dr["firstname"].ToString() + " " + dr["lastname"].ToString() + " ออกจากระบบ", "ยืนยันการทำงาน",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Util.DBExecute("DELETE FROM Employee WHERE employeeId = '" + dr["employeeId"] + "'");
                //employeeGridView.DeleteSelectedRows();

                try
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/employee/Delete",
                    string.Format("shop={0}&employeeId={1}", Param.ApiShopId, dr["employeeId"])));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }

                }
                catch (Exception ex)
                {
                    Util.WriteErrorLog(ex.Message);
                    Util.WriteErrorLog(ex.StackTrace);
                }

                employeeGridView.DeleteSelectedRows();

            }
        }

        private void btnScreen_Click(object sender, EventArgs e)
        {
            DataRow dr = employeeGroupGridview.GetDataRow(employeeGroupGridview.GetSelectedRows()[0]);
            FmScreenMapping fm = new FmScreenMapping();
            fm.employeeType = dr["id"].ToString();
            fm.employeeTypeName = dr["name"].ToString();
            fm.ShowDialog(this);
        }
    }
}