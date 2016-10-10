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
    public partial class FmScreenMapping : DevExpress.XtraEditors.XtraForm
    {
        public string employeeTypeName = "";
        public string employeeType = "2";
        DataTable _TABLE_SCREEN, _TABLE_SUBSCREEN, _TABLE_PERMISSION;
        DataRow row;


        public FmScreenMapping()
        {
            InitializeComponent();
        }

        private void FmScreenMapping_Load(object sender, EventArgs e)
        {
            this.Text = "กำหนดสิทธิ์การใช้งานหน้าจอในระบบ - " + employeeTypeName;
            clMainScreen.OptionsColumn.AllowEdit = Util.CanAccessScreenDetail("E30");
            clSubScreen.OptionsColumn.AllowEdit = Util.CanAccessScreenDetail("E30");
            clPermission.OptionsColumn.AllowEdit = Util.CanAccessScreenDetail("E30");
            loadData();
        }

        private void loadData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("canView", typeof(bool));
            dt.Columns.Add("name", typeof(string));
            

            _TABLE_SCREEN = Util.DBQuery(string.Format(@"
                    SELECT DISTINCT  s.id, s.name, CASE WHEN m.permission IS NULL THEN 'False' ELSE 'True' END canView
                    FROM SystemScreen s
                    LEFT JOIN EmployeeScreenMapping m
                    ON s.system = m.system
                    AND s.id = m.screen
                    AND m.permission = 'V0'
                    AND m.employeeType = {0}
                    WHERE s.system = 'POS'
                    AND s.parent = ''
                    AND s.active = 'True'
                    ORDER BY s.orderLevel
            ", employeeType));

            if (_TABLE_SCREEN.Rows.Count > 0)
            {
                for (int a = 0; a < _TABLE_SCREEN.Rows.Count; a++)
                {
                    row = dt.NewRow();
                    row[0] = _TABLE_SCREEN.Rows[a]["id"].ToString();
                    row[1] = _TABLE_SCREEN.Rows[a]["canView"].ToString();
                    row[2] = _TABLE_SCREEN.Rows[a]["name"].ToString();
                    dt.Rows.Add(row);
                }

                screenGridControl.DataSource = dt;

            }
            else
            {
                screenGridControl.DataSource = null;
            }

         

            //var dt = Util.DBQuery(string.Format(@"
            //    SELECT DISTINCT  s.id, s.name, CASE WHEN m.permission IS NULL THEN Cast(0 as bit) ELSE Cast(1 as bit) END canView
            //    FROM SystemScreen s
            //     LEFT JOIN EmployeeScreenMapping m
            //      ON s.system = m.system
            //      AND s.id = m.screen
            //      AND m.permission = 'V0'
            //      AND m.employeeType = {0}
            //    WHERE s.system = 'POS'
            //     AND s.parent = ''
            //     AND s.active = 'True'
            //    ORDER BY s.orderLevel
            //", employeeType));
            //screenGridControl.DataSource = dt;
        }

        private void loadSubScreenData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("canView", typeof(bool));
            dt.Columns.Add("name", typeof(string));

            DataRow dr = mainScreenGridview.GetDataRow(mainScreenGridview.GetSelectedRows()[0]);

            _TABLE_SUBSCREEN = Util.DBQuery(string.Format(@"
                SELECT DISTINCT s.id, s.name, CASE WHEN m.permission IS NULL THEN 'False' ELSE 'True' END canView
                FROM SystemScreen s
	                LEFT JOIN EmployeeScreenMapping m
		                ON s.system = m.system
		                AND s.id = m.screen
		                AND m.permission = 'V0'
		                AND m.employeeType = {1}
                WHERE s.system = 'POS'
	                AND s.parent = '{0}'
	                AND s.active = 'True'
                ORDER BY s.orderLevel
            ", dr["id"].ToString(), employeeType));

            if (_TABLE_SUBSCREEN.Rows.Count > 0)
            {
                for (int a = 0; a < _TABLE_SUBSCREEN.Rows.Count; a++)
                {
                    row = dt.NewRow();
                    row[0] = _TABLE_SUBSCREEN.Rows[a]["id"].ToString();
                    row[1] = _TABLE_SUBSCREEN.Rows[a]["canView"].ToString();
                    row[2] = _TABLE_SUBSCREEN.Rows[a]["name"].ToString();
                    dt.Rows.Add(row);
                }

                subScreenGridControl.DataSource = dt;

            }
            else
            {
                subScreenGridControl.DataSource = null;
            }
            //var dt = Util.DBQuery(string.Format(@"
            //    SELECT DISTINCT s.id, s.name, CASE WHEN m.permission IS NULL THEN 0 ELSE 1 END canView
            //    FROM SystemScreen s
	           //     LEFT JOIN EmployeeScreenMapping m
		          //      ON s.system = m.system
		          //      AND s.id = m.screen
		          //      AND m.permission = 'V0'
		          //      AND m.employeeType = {1}
            //    WHERE s.system = 'POS'
	           //     AND s.parent = '{0}'
	           //     AND s.active = 'True'
            //    ORDER BY s.orderLevel
            //", dr["id"].ToString(), employeeType));
            //subScreenGridControl.DataSource = dt;
            if(_TABLE_SUBSCREEN.Rows.Count == 0) loadPermissionData();
        }

        private void loadPermissionData()
        {
            DataRow dr = _TABLE_SUBSCREEN.Rows.Count == 0 ?
                mainScreenGridview.GetDataRow(mainScreenGridview.GetSelectedRows()[0]) :
                subScreenGridView.GetDataRow(subScreenGridView.GetSelectedRows()[0]);

            DataTable dt = new DataTable();
            dt.Columns.Add("screen", typeof(string));
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("description", typeof(string));
            dt.Columns.Add("orderLevel", typeof(string));
            dt.Columns.Add("canDo", typeof(bool));

            //DataRow dr = ((DataTable)subScreenGridControl.DataSource).Rows.Count == 0 ?
            //    mainScreenGridview.GetDataRow(mainScreenGridview.GetSelectedRows()[0]) :
            //    subScreenGridView.GetDataRow(subScreenGridView.GetSelectedRows()[0]);
            _TABLE_PERMISSION = Util.DBQuery(string.Format(@"
                SELECT p.screen screen, p.id id, p.description description, 0 orderLevel, CASE WHEN m.permission IS NULL THEN 'False' ELSE 'True' END canDo
                FROM SystemScreenPermission p
	                LEFT JOIN EmployeeScreenMapping m
		                ON p.system = m.system
		                AND p.id = m.permission
		                AND p.screen = m.screen
		                AND m.employeeType = {1}
                WHERE p.system = 'POS'
	                AND p.id LIKE 'V%'
	                AND p.screen = '{0}'
	                AND p.id <> 'V0'
                UNION ALL
                SELECT p.screen, p.id, p.description description, 1 orderLevel, CASE WHEN m.permission IS NULL THEN 'False' ELSE 'True' END canDo
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
                SELECT p.screen, p.id, p.description description, 2 orderLevel,CASE WHEN m.permission IS NULL THEN 'False' ELSE 'True' END canDo
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
                SELECT p.screen, p.id, p.description description, 3 orderLevel,CASE WHEN m.permission IS NULL THEN 'False' ELSE 'True' END canDo
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

            if (_TABLE_PERMISSION.Rows.Count > 0)
            {
                for (int a = 0; a < _TABLE_PERMISSION.Rows.Count; a++)
                {
                    row = dt.NewRow();
                    row[0] = _TABLE_PERMISSION.Rows[a]["screen"].ToString();
                    row[1] = _TABLE_PERMISSION.Rows[a]["id"].ToString();
                    row[2] = _TABLE_PERMISSION.Rows[a]["description"].ToString();
                    row[3] = _TABLE_PERMISSION.Rows[a]["orderLevel"].ToString();
                    row[4] = _TABLE_PERMISSION.Rows[a]["canDo"].ToString();
                    dt.Rows.Add(row);
                }

                employeeControl.DataSource = dt;

            }
            else
            {
                employeeControl.DataSource = null;
            }

            //var dt = Util.DBQuery(string.Format(@"
            //    SELECT p.screen, p.id, p.description description, 0 orderLevel, CASE WHEN m.permission IS NULL THEN 0 ELSE 1 END canDo
            //    FROM SystemScreenPermission p
	           //     LEFT JOIN EmployeeScreenMapping m
		          //      ON p.system = m.system
		          //      AND p.id = m.permission
		          //      AND p.screen = m.screen
		          //      AND m.employeeType = {1}
            //    WHERE p.system = 'POS'
	           //     AND p.id LIKE 'V%'
	           //     AND p.screen = '{0}'
	           //     AND p.id <> 'V0'
            //    UNION ALL
            //    SELECT p.screen, p.id, p.description description, 1 orderLevel, CASE WHEN m.permission IS NULL THEN 0 ELSE 1 END canDo
            //    FROM SystemScreenPermission p
	           //     LEFT JOIN EmployeeScreenMapping m
		          //      ON p.system = m.system
		          //      AND p.id = m.permission
		          //      AND p.screen = m.screen
		          //      AND m.employeeType = {1}
            //    WHERE p.system = 'POS'
	           //     AND p.id LIKE 'A%'
	           //     AND p.screen = '{0}'
            //    UNION ALL
            //    SELECT p.screen, p.id, p.description description, 2 orderLevel,CASE WHEN m.permission IS NULL THEN 0 ELSE 1 END canDo
            //    FROM SystemScreenPermission p
	           //     LEFT JOIN EmployeeScreenMapping m
		          //      ON p.system = m.system
		          //      AND p.id = m.permission
		          //      AND p.screen = m.screen
		          //      AND m.employeeType = {1}
            //    WHERE p.system = 'POS'
	           //     AND p.id LIKE 'E%'
	           //     AND p.screen = '{0}'
            //    UNION ALL	
            //    SELECT p.screen, p.id, p.description description, 3 orderLevel,CASE WHEN m.permission IS NULL THEN 0 ELSE 1 END canDo
            //    FROM SystemScreenPermission p
	           //     LEFT JOIN EmployeeScreenMapping m
		          //      ON p.system = m.system
		          //      AND p.id = m.permission
		          //      AND p.screen = m.screen
		          //      AND m.employeeType = {1}
            //    WHERE p.system = 'POS'
	           //     AND p.id LIKE 'D%'
	           //     AND p.screen = '{0}'
            //    ORDER BY orderLevel, id
            //", dr["id"].ToString(), employeeType));
            //employeeControl.DataSource = dt;
        }

        private void mainScreenGridview_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:ss:mm");
            var data = e.Value;
            var name = e.Column.FieldName;
            if(name == "canView")
            {
                DataRow dr = mainScreenGridview.GetDataRow(mainScreenGridview.GetSelectedRows()[0]);
                if ((bool)data)
                {
                    Util.DBExecute("INSERT INTO EmployeeScreenMapping (system, screen, permission, shop, employeeType, addDate, addBy, Sync) VALUES (" +
                        "'POS', '" + dr["id"] + "', 'V0', '" + Param.ShopId + "', '"+ employeeType + "',  '" + dtime + "', '" + Param.UserId + "', 1)");
                }
                else
                {
                    Util.DBExecute("DELETE FROM EmployeeScreenMapping WHERE system = 'POS' AND screen = '" + dr["id"] + "' AND permission = 'V0' AND employeeType = '" + employeeType + "'");

                    try
                    {
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                        dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/employee/DeleteScreen",
                        string.Format("shop={0}&system=POS&screen={1}&permission={2}&employeetype={3}",
                                Param.ApiShopId, dr["id"], "V0", employeeType)));
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
                }
            }
        }

        private void mainScreenGridview_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadSubScreenData();
        }

        private void subScreenGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:ss:mm");
            var data = e.Value;
            var name = e.Column.FieldName;
            if (name == "canView")
            {
                DataRow dr = subScreenGridView.GetDataRow(subScreenGridView.GetSelectedRows()[0]);
                if ((bool)data)
                {
                    Util.DBExecute("INSERT INTO EmployeeScreenMapping (system, screen, permission, shop, employeeType, addDate, addBy, Sync) VALUES (" +
                        "'POS', '" + dr["id"] + "', 'V0', '" + Param.ShopId + "', '" + employeeType + "', '" + dtime + "', '" + Param.UserId + "', 1)");
                }
                else
                {
                    Util.DBExecute("DELETE FROM EmployeeScreenMapping WHERE system = 'POS' AND screen = '" + dr["id"] + "' AND permission = 'V0' AND employeeType = '" + employeeType + "'");

                    try
                    {
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                        dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/employee/DeleteScreen",
                        string.Format("shop={0}&system=POS&screen={1}&permission={2}&employeetype={3}",
                                Param.ApiShopId, dr["id"], "V0", employeeType)));
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
                }

               
            }
        }

        private void subScreenGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadPermissionData();
        }

        private void employeeGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:ss:mm");
            var data = e.Value;
            var name = e.Column.FieldName;
            DataTable dt;
            int i = 0;

            if (name == "canDo")
            {
                DataRow dr = employeeGridView.GetDataRow(employeeGridView.GetSelectedRows()[0]);
                if ((bool)data)
                {
                    Util.DBExecute("INSERT INTO EmployeeScreenMapping (system, screen, permission, shop, employeeType, addDate, addBy, Sync) VALUES (" +
                        "'POS', '" + dr["screen"] + "', '" + dr["id"] + "', '" + Param.ShopId + "', '" + employeeType + "', '" + dtime + "', '" + Param.UserId + "', 1)");
                }
                else
                {
                    Util.DBExecute("DELETE FROM EmployeeScreenMapping WHERE system = 'POS' AND screen = '" + dr["screen"] + "' AND permission = '" + dr["id"] + "' AND employeeType = '" + employeeType + "'");

                    try
                    {
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                       
                        dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/employee/DeleteScreen",
                        string.Format("shop={0}&system=POS&screen={1}&permission={2}&employeetype={3}",
                                Param.ApiShopId, dr["screen"], dr["id"], employeeType)));
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
                }
            }
        }
    }
}