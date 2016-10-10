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
    public partial class FmSelectPrinter : DevExpress.XtraEditors.XtraForm
    {
        public FmSelectPrinter()
        {
            InitializeComponent();
        }

        
        private void GetPrinter()
        {
            var index = 0;
            var i = 0;
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cbxPrinter.Properties.Items.Add(printer);
                if (Param.DevicePrinter == printer) index = i;
                i++;
            }
            cbxPrinter.SelectedIndex = index;
        }

        private void FmSelectPrinter_Load(object sender, EventArgs e)
        {
            if (cbxPrinter.Text == "")
            {
                GetPrinter();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("คุณต้องการเลือกเครื่องพิมพ์นี้ ใช่หรือไม่ ?", "ยืนยันการเลือกเครื่องพิมพ์", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
                Param.Printer = Param.DevicePrinter;
                Param.DevicePrinter = cbxPrinter.SelectedItem.ToString();
                this.DialogResult = DialogResult.OK;
            //}
            
        }
    }
}