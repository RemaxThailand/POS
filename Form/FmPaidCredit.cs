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
    public partial class FmPaidCredit : DevExpress.XtraEditors.XtraForm
    {
        public FmPaidCredit()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var cash = int.Parse(txtCash.Text);
                if ((cash >= double.Parse(lblTotal.Text)))
                {
                    Util.DBExecute(string.Format(@"UPDATE CreditCustomer SET paidPrice = '{0}', paidBy = '{1}', paidDate = (SELECT STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW')), Sync = 1 WHERE SellNo = '{2}'", cash, Param.UserId, lblSellNo.Text));
                    Util.DBExecute(string.Format(@"UPDATE SellHeader SET payType = 1, Sync = 1 WHERE SellNo = '{0}'", lblSellNo.Text));

                    this.DialogResult = DialogResult.OK;
                    this.Dispose();

                }
                else
                {
                    MessageBox.Show("กรุณาตรวจสอบจำนวนเงินที่รับมาอีกครั้ง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { }
        }

        private void FmPaidCredit_Load(object sender, EventArgs e)
        {
            txtCash.Focus();
            LoadData();
        }

        public void LoadData()
        {
            txtCash.Focus();
            lblSellNo.Text = UcCredit.sellNo;
            lblTotal.Text = UcCredit.price;
        }

        private void txtCash_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Return))
            {
                if (txtCash.Text != "")
                {
                    btnSave_Click(sender, (e));
                }
                else
                {
                    MessageBox.Show("กรุณากรอกจำนวนเงิน");
                }
            }
        }

        private void txtCash_Enter(object sender, EventArgs e)
        {
            Util.SetKeyboardLayout(Util.GetInputLanguageByName("US"));
        }

        private void txtCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtCash_KeyUp(object sender, KeyEventArgs e)
        {
            
            try
            {
                lblChange.Text = (int.Parse(txtCash.Text) - int.Parse(lblTotal.Text)).ToString("#,##0");
            }
            catch
            {
                lblChange.Text = int.Parse(lblTotal.Text).ToString("#,##0");
            }
        }
    }
}