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
    public partial class FmMessage : DevExpress.XtraEditors.XtraForm
    {
        public FmMessage()
        {
            InitializeComponent();
        }

        private void FmMessage_Load(object sender, EventArgs e)
        {
            //lblHeader.Text = "";
            //lblMessage.Text = "";

            lblCode.Text = DateTime.Now.ToString("HHmmss");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCode.Text == lblCode.Text)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                MessageBox.Show("กรุณาตรวจสอบรหัสที่กรอกอีกครั้ง!!");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtCode.Text.Length == 6)
            {
                if (txtCode.Text == lblCode.Text)
                {
                    btnOk.Enabled = true;
                }
            }
        }
    }
}