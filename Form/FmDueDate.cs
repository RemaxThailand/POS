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
    public partial class FmDueDate : DevExpress.XtraEditors.XtraForm
    {
        public FmDueDate()
        {
            InitializeComponent();
        }

        private void FmDueDate_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            for (int i = 0; i <= 30; i++)
            {
                cbbCredit.Properties.Items.Add(i + " วัน");
            }
            cbbCredit.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Param.Due = cbbCredit.SelectedIndex;

            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }
    }
}