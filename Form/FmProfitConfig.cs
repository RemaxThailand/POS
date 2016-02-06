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
    public partial class FmProfitConfig : DevExpress.XtraEditors.XtraForm
    {
        public FmProfitConfig()
        {
            InitializeComponent();
        }

        private void FmProfitConfig_Load(object sender, EventArgs e)
        {
            DataTable dt = Util.DBQuery(string.Format(@"SELECT DISTINCT name FROM  Category WHERE Shop = '{0}' ORDER BY Name", Param.ShopId));
            cbCategory.Properties.Items.Clear();
            cbCategory.Properties.Items.Add("หมวดหมู่");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbCategory.Properties.Items.Add(dt.Rows[i]["name"].ToString());
                if (Param.CategoryName != null && Param.CategoryName == dt.Rows[i]["name"].ToString())
                    cbCategory.SelectedIndex = i + 1;
            }
            if (Param.CategoryName == null)
                cbCategory.SelectedIndex = 0;
            Param.CategoryName = null;
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            nudPrice.Enabled = cbCategory.SelectedIndex != 0;
            nudPrice1.Enabled = cbCategory.SelectedIndex != 0;
            nudPrice2.Enabled = cbCategory.SelectedIndex != 0;
            nudPrice3.Enabled = cbCategory.SelectedIndex != 0;
            nudPrice4.Enabled = cbCategory.SelectedIndex != 0;

            if (cbCategory.SelectedIndex == 0)
            {
                nudPrice.Value = 0;
                nudPrice1.Value = 0;
                nudPrice2.Value = 0;
                nudPrice3.Value = 0;
                nudPrice4.Value = 0;
            }
            else
            {
                DataTable dt = Util.DBQuery(@"SELECT IFNULL(price,0) price, IFNULL(price1,0) price1,  IFNULL(price2,0) price2, IFNULL(price3,0) price3, IFNULL(price4,0) price4 FROM Category c LEFT JOIN CategoryProfit p ON c.Category = p.Category WHERE LOWER(name) = '" + cbCategory.SelectedItem.ToString().ToLower() + "'");
                nudPrice.Value = int.Parse(dt.Rows[0]["price"].ToString());
                nudPrice1.Value = int.Parse(dt.Rows[0]["price1"].ToString());
                nudPrice2.Value = int.Parse(dt.Rows[0]["price2"].ToString());
                nudPrice3.Value = int.Parse(dt.Rows[0]["price3"].ToString());
                nudPrice4.Value = int.Parse(dt.Rows[0]["price4"].ToString());
                //nudPrice_ValueChanged(sender, e);
            }
            btnSave.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Util.DBExecute(string.Format(@"INSERT OR REPLACE INTO CategoryProfit (Category, price, price1, price2,price3,price4, Sync) VALUES ((SELECT Category FROM Category WHERE LOWER(name) = '{3}'), {0}, {1}, {2}, {3}, {4}, 1)",
               nudPrice.Value, nudPrice1.Value, nudPrice2.Value, nudPrice3.Value, nudPrice4.Value, cbCategory.SelectedItem.ToString().ToLower()));
            btnSave.Enabled = false;
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }
    }
}