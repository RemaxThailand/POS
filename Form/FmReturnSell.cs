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
using System.Globalization;

namespace PowerPOS
{
    public partial class FmReturnSell : DevExpress.XtraEditors.XtraForm
    {
        public static string Pid;
        public static string PName;
        public static string sellN;
        public static string sellP;
        public static string costProduct;
        public static string costPrice;
        public static string customer;
        public static string sellDate;
        DataTable dt, _TABLE_RETURN;

        public FmReturnSell()
        {
            InitializeComponent();
        }

        private void FmReturnSell_Load(object sender, EventArgs e)
        {
            try
            {
                _TABLE_RETURN = Util.DBQuery(string.Format(@"SELECT sd.SellNo, sh.SellDate , c.firstname|| ' ' || c.lastname Customer, sd.Quantity, sd.SellPrice , sd.SellPrice/sd.Quantity Price, 
                    sd.Cost, sd.Cost/sd.Quantity PriceCost, sd.Product, p.Name
                    FROM SellDetail sd
                    LEFT JOIN SellHeader sh
                    ON sd.SellNo = sh.SellNo
                    LEFT JOIN Customer c
                    ON c.Customer = sh.Customer
                    LEFT JOIN Product p
                    ON p.Product = sd.Product
                    WHERE sd.Product = '{0}' 
                    AND sd.Quantity <> 0 
                    AND sh.SellDate BETWEEN '{2}' AND '{3}'
                    GROUP BY sd.Product, sd.SellNo ORDER BY sd.SellNo DESC", Param.product, Param.ShopParent,
                    DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")));

                lblID.Text = _TABLE_RETURN.Rows[0]["product"].ToString();
                lblName.Text = _TABLE_RETURN.Rows[0]["name"].ToString();

                saleGridView.OptionsBehavior.AutoPopulateColumns = false;
                saleGridControl.MainView = saleGridView;

                DataRow row;
                dt = new DataTable();
                for (int i = 0; i < ((ColumnView)saleGridControl.MainView).Columns.Count; i++)
                {
                    dt.Columns.Add(saleGridView.Columns[i].FieldName);
                }

                if (_TABLE_RETURN.Rows.Count > 0)
                {
                    for (int a = 0; a < _TABLE_RETURN.Rows.Count; a++)
                    {
                        //string customer = _TABLE_RETURN.Rows[a]["firstname"].ToString() + _TABLE_RETURN.Rows[a]["lastname"].ToString();
                        row = dt.NewRow();
                        row[0] = (a + 1) * 1;
                        row[1] = _TABLE_RETURN.Rows[a]["SellNo"].ToString();
                        row[2] = Convert.ToDateTime(_TABLE_RETURN.Rows[a]["SellDate"]).ToLocalTime().ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH")); 
                        row[3] = _TABLE_RETURN.Rows[a]["Customer"].ToString();
                        row[4] = Convert.ToInt32(_TABLE_RETURN.Rows[a]["Price"].ToString()).ToString("#,##0");
                        row[5] = Convert.ToInt32(_TABLE_RETURN.Rows[a]["Quantity"].ToString()).ToString("#,##0");
                        row[6] = Convert.ToInt32(_TABLE_RETURN.Rows[a]["SellPrice"].ToString()).ToString("#,##0");
                        row[7] = Convert.ToInt32(_TABLE_RETURN.Rows[a]["Cost"].ToString()).ToString("#,##0");
                        row[8] = Convert.ToInt32(_TABLE_RETURN.Rows[a]["PriceCost"].ToString()).ToString("#,##0");
                        dt.Rows.Add(row);
                    }
                    saleGridControl.DataSource = dt;
                  }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void saleGridControl_Click(object sender, EventArgs e)
        {
            if (saleGridView.RowCount > 0)
            {
                Pid = lblID.Text;
                sellN = saleGridView.GetRowCellDisplayText(saleGridView.FocusedRowHandle, saleGridView.Columns["sellNo"]).ToString();

                dt = Util.DBQuery(string.Format(@"SELECT sd.SellNo, sh.SellDate , c.firstname|| ' ' || c.lastname Customer, sd.Quantity, sd.SellPrice , sd.SellPrice/sd.Quantity Price, 
                    sd.Cost, sd.Cost/sd.Quantity PriceCost, sd.Product, p.Name
                    FROM SellDetail sd
                    LEFT JOIN SellHeader sh
                    ON sd.SellNo = sh.SellNo
                    LEFT JOIN Customer c
                    ON c.Customer = sh.Customer
                    LEFT JOIN Product p
                    ON p.Product = sd.Product
                    WHERE sd.Product = '{0}' 
                    AND sd.Quantity <> 0 
                    AND sd.SellNo = '{1}'
                    GROUP BY sd.Product, sd.SellNo ORDER BY sd.SellNo DESC", Pid, sellN));

                sellP = dt.Rows[0]["Price"].ToString();
                costProduct = dt.Rows[0]["Cost"].ToString();
                costPrice = dt.Rows[0]["PriceCost"].ToString();
                customer = dt.Rows[0]["Customer"].ToString();
                sellDate = dt.Rows[0]["SellDate"].ToString();
                PName = dt.Rows[0]["Name"].ToString();

                //this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("กรุณาเลือกรายการที่ต้องการ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}