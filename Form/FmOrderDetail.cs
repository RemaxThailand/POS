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
using DevExpress.XtraEditors.Repository;

namespace PowerPOS
{
    public partial class FmOrderDetail : DevExpress.XtraEditors.XtraForm
    {
        private bool _FIRST_LOAD;
        DataTable dt, _TABLE_ORDER;

        public FmOrderDetail()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FmOrderDetail_Load(object sender, EventArgs e)
        {
            _FIRST_LOAD = true;
            LoadData();
        }
        private void LoadData()
        {
            lblOrderNo.Text = UcReceiveProduct.OrderNo;
            lblProduct.Text = UcReceiveProduct.productNo;
            lblProductName.Text = UcReceiveProduct.ProductName;

            _FIRST_LOAD = false;
            SearchData();
        }

        private void SearchData()
        {
            DataRow row;
            if (!_FIRST_LOAD)
            {
                _TABLE_ORDER = Util.DBQuery(string.Format(@"
                    SELECT Barcode,ReceivedDate
                    FROM Barcode 
                    WHERE OrderNo = '{0}'
                        AND Product = '{1}'
                ", UcReceiveProduct.OrderNo, UcReceiveProduct.productNo));

                orderGridView.OptionsBehavior.AutoPopulateColumns = false;
                orderGridControl.MainView = orderGridView;

                dt = new DataTable();
                dt.Columns.Add("Image", typeof(Image));
                dt.Columns.Add("No", typeof(int));
                dt.Columns.Add("Barcode", typeof(string));

                for (int a = 0; a < _TABLE_ORDER.Rows.Count; a++)
                {
                    
                    if (_TABLE_ORDER.Rows[a]["ReceivedDate"].ToString() == "")
                    {
                        var image = PowerPOS.Properties.Resources.boscheduler_16x16;
                        row = dt.NewRow();
                        row[0] = image;
                        row[1] = (a + 1) * 1;
                        row[2] = _TABLE_ORDER.Rows[a]["Barcode"].ToString();
                        dt.Rows.Add(row);
                    }
                    else
                    {
                        var image = global::PowerPOS.Properties.Resources.apply_16x16;
                        row = dt.NewRow();
                        row[0] = image;
                        row[1] = (a + 1) * 1;
                        row[2] = _TABLE_ORDER.Rows[a]["Barcode"].ToString();
                        dt.Rows.Add(row);
                    }
                }

                orderGridView.Columns["Image"].ColumnEdit = new RepositoryItemPictureEdit();
                orderGridControl.DataSource = dt;
            }
        }
    }
}