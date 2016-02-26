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
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Grid;

namespace PowerPOS
{
    public partial class UcStatistic : DevExpress.XtraEditors.XtraUserControl
    {
        DataTable _TABLE_REPORT;
        public UcStatistic()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {

        }

        private void UcStatistic_Load(object sender, EventArgs e)
        {
        }
    }
}
