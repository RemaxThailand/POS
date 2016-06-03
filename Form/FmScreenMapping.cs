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
    public partial class FmScreenMapping : DevExpress.XtraEditors.XtraForm
    {
        public string employeeTypeName = "";
        public string employeeType = "1";

        public FmScreenMapping()
        {
            InitializeComponent();
            this.Text = "กำหนดสิทธิ์การใช้งานหน้าจอในระบบ - " + employeeTypeName;
            loadData();
        }

        private void loadData()
        {
            var dt = Util.SqlCeQuery(string.Format(@"
                SELECT s.id, s.name, CONVERT(BIT, CASE WHEN m.permission IS NULL THEN 0 ELSE 1 END) canView
                FROM SystemScreen s
	                LEFT JOIN EmployeeScreenMapping m
		                ON s.system = m.system
		                AND s.id = m.screen
		                AND m.permission = 'V0'
		                AND m.employeeType = {0}
                WHERE s.system = 'POS'
	                AND s.parent IS NULL
                ORDER BY s.orderLevel
            ", employeeType));
            screenGridControl.DataSource = dt;
        }

        private void loadSubScreenData()
        {
            DataRow dr = mainScreenGridview.GetDataRow(mainScreenGridview.GetSelectedRows()[0]);
            var dt = Util.SqlCeQuery(string.Format(@"
                SELECT s.id, s.name, CONVERT(BIT, CASE WHEN m.permission IS NULL THEN 0 ELSE 1 END) canView
                FROM SystemScreen s
	                LEFT JOIN EmployeeScreenMapping m
		                ON s.system = m.system
		                AND s.id = m.screen
		                AND m.permission = 'V0'
		                AND m.employeeType = {1}
                WHERE s.system = 'POS'
	                AND s.parent = '{0}'
                ORDER BY s.orderLevel
            ", dr["id"].ToString(), employeeType));
            subScreenGridControl.DataSource = dt;
            if(dt.Rows.Count == 0) loadPermissionData();
        }

        private void loadPermissionData()
        {
            DataRow dr = ((DataTable)subScreenGridControl.DataSource).Rows.Count == 0 ?
                mainScreenGridview.GetDataRow(mainScreenGridview.GetSelectedRows()[0]) :
                subScreenGridView.GetDataRow(subScreenGridView.GetSelectedRows()[0]);
            var dt = Util.SqlCeQuery(string.Format(@"
                SELECT p.screen, p.id, p.description, 1 orderLevel, CONVERT(BIT, CASE WHEN m.permission IS NULL THEN 0 ELSE 1 END) canDo
                FROM SystemScreenPermission p
	                LEFT JOIN EmployeeScreenMapping m
		                ON p.system = m.system
		                AND p.id = m.permission
		                AND p.screen = m.screen
		                AND m.employeeType = {1}
                WHERE p.system = 'POS'
	                AND p.id LIKE 'A%'
	                AND p.screen = '{0}'
                UNION ALL	
                SELECT p.screen, p.id, p.description, 2 orderLevel, CONVERT(BIT, CASE WHEN m.permission IS NULL THEN 0 ELSE 1 END) canDo
                FROM SystemScreenPermission p
	                LEFT JOIN EmployeeScreenMapping m
		                ON p.system = m.system
		                AND p.id = m.permission
		                AND p.screen = m.screen
		                AND m.employeeType = {1}
                WHERE p.system = 'POS'
	                AND p.id LIKE 'E%'
	                AND p.screen = '{0}'
                UNION ALL	
                SELECT p.screen, p.id, p.description, 3 orderLevel, CONVERT(BIT, CASE WHEN m.permission IS NULL THEN 0 ELSE 1 END) canDo
                FROM SystemScreenPermission p
	                LEFT JOIN EmployeeScreenMapping m
		                ON p.system = m.system
		                AND p.id = m.permission
		                AND p.screen = m.screen
		                AND m.employeeType = {1}
                WHERE p.system = 'POS'
	                AND p.id LIKE 'D%'
	                AND p.screen = '{0}'
                ORDER BY orderLevel, id
            ", dr["id"].ToString(), employeeType));
            employeeControl.DataSource = dt;
        }

        private void mainScreenGridview_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var data = e.Value;
            var name = e.Column.FieldName;
            if(name == "canView")
            {
                DataRow dr = mainScreenGridview.GetDataRow(mainScreenGridview.GetSelectedRows()[0]);
                if ((bool)data)
                {
                    Util.SqlCeExecute("INSERT INTO EmployeeScreenMapping (system, screen, permission, shop, employeeType, addDate) VALUES (" +
                        "'POS', '" + dr["id"] + "', 'V0', '" + Param.ShopId + "', '"+ employeeType + "', GETDATE())");
                }
                else
                {
                    Util.SqlCeExecute("DELETE FROM EmployeeScreenMapping WHERE system = 'POS' AND screen = '" + dr["id"] + "' AND permission = 'V0' AND employeeType = '" + employeeType + "'");
                }
            }
        }

        private void mainScreenGridview_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadSubScreenData();
        }

        private void subScreenGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var data = e.Value;
            var name = e.Column.FieldName;
            if (name == "canView")
            {
                DataRow dr = subScreenGridView.GetDataRow(subScreenGridView.GetSelectedRows()[0]);
                if ((bool)data)
                {
                    Util.SqlCeExecute("INSERT INTO EmployeeScreenMapping (system, screen, permission, shop, employeeType, addDate) VALUES (" +
                        "'POS', '" + dr["id"] + "', 'V0', '" + Param.ShopId + "', '" + employeeType + "', GETDATE())");
                }
                else
                {
                    Util.SqlCeExecute("DELETE FROM EmployeeScreenMapping WHERE system = 'POS' AND screen = '" + dr["id"] + "' AND permission = 'V0' AND employeeType = '" + employeeType + "'");
                }
            }
        }

        private void subScreenGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadPermissionData();
        }

        private void employeeGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var data = e.Value;
            var name = e.Column.FieldName;
            if (name == "canDo")
            {
                DataRow dr = employeeGridView.GetDataRow(employeeGridView.GetSelectedRows()[0]);
                if ((bool)data)
                {
                    Util.SqlCeExecute("INSERT INTO EmployeeScreenMapping (system, screen, permission, shop, employeeType, addDate) VALUES (" +
                        "'POS', '" + dr["screen"] + "', '" + dr["id"] + "', '" + Param.ShopId + "', '" + employeeType + "', GETDATE())");
                }
                else
                {
                    Util.SqlCeExecute("DELETE FROM EmployeeScreenMapping WHERE system = 'POS' AND screen = '" + dr["screen"] + "' AND permission = '" + dr["id"] + "' AND employeeType = '" + employeeType + "'");
                }
            }
        }
    }
}