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
    public partial class FmCashReceive : DevExpress.XtraEditors.XtraForm
    {
        public int _TOTAL = 0;
        public string _SELL_NO;

        public FmCashReceive()
        {
            InitializeComponent();
        }

        private void FmCashReceive_Load(object sender, EventArgs e)
        {
            txtCash.Focus();
        }

        private void txtCash_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                lblChange.Text = (int.Parse(txtCash.Text) - _TOTAL).ToString("#,##0");
            }
            catch
            {
                lblChange.Text = _TOTAL.ToString("#,##0");
            }
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt;
                dt = Util.DBQuery(string.Format(@"SELECT b.Barcode, b.SellNo, p.Price{0} Price
                    FROM Barcode b
                        LEFT JOIN Product p
                        ON b.product = p.product
                    WHERE p.Shop = '{1}' AND b.sellBy = '{2}'", Param.SelectCustomerSellPrice == 0 ? "" : "" + Param.SelectCustomerSellPrice, Param.ShopId, Param.CpuId));



                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Util.DBExecute(string.Format(@"UPDATE Barcode
                        SET SellBy = '{0}', SellDate = STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), SellNo = '{1}', Sync = 1, SellPrice = {2}, Customer = '{3}'
                        WHERE Barcode = '{4}' AND SellNo = '{5}'",
                        Param.UserId, _SELL_NO, dt.Rows[i]["Price"].ToString(), Param.SelectCustomerId,
                        dt.Rows[i]["Barcode"].ToString(), dt.Rows[i]["SellNo"].ToString()));

                    Util.DBExecute(string.Format(@"INSERT INTO SellDetail (SellNo, Product, Barcode, SellPrice, Cost, Sync)
                    SELECT '{0}', Product, Barcode, SellPrice, Cost, 1 FROM Barcode WHERE SellNo = '{0}' AND Barcode = '{1}'", _SELL_NO, dt.Rows[i]["Barcode"].ToString()));

                }

                Util.DBExecute(string.Format(@"INSERT INTO SellHeader (SellNo, Profit, TotalPrice, Customer, CustomerSex, CustomerAge, SellDate, SellBy)
                    SELECT '{0}', (SELECT SUM(SellPrice-Cost-OperationCost) FROM Barcode WHERE SellNo = '{0}'),
                    (SELECT SUM(SellPrice) FROM Barcode WHERE SellNo = '{0}'), '{1}', '{2}', {3}, STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), '{4}'",
                        _SELL_NO, Param.SelectCustomerId, Param.SelectCustomerSex, Param.SelectCustomerAge, Param.UserId));

                var cash = int.Parse(txtCash.Text);
                if ((cash >= double.Parse(lblPrice.Text)) || cash == 0)
                {
                    if (cash != 0)
                    {
                        Util.DBExecute(string.Format(@"UPDATE SellHeader SET Cash = {0}, PayType = 1, Paid = 1, Sync = 1 WHERE SellNo = '{1}'", cash, _SELL_NO));
                    }
                    else
                    {
                        Util.DBExecute(string.Format(@"UPDATE SellHeader SET Cash = 0, PayType = 0, Paid = 0, Sync = 1 WHERE SellNo = '{0}'", _SELL_NO));
                    }
                    //Util.PrintReceipt(_SELL_NO);
                    Param.SelectCustomerId = "000000";
                    Param.SelectCustomerName = "ลูกค้าทั่วไป";
                    Param.SelectCustomerSex = "";
                    Param.SelectCustomerAge = 0;
                    Param.SelectCustomerSellPrice = 0;

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;


                    if (Param.PrintType == "Y")
                    {
                        var cnt = int.Parse(Param.PrintCount.ToString());
                        for (int i = 1; i <= cnt; i++)
                            Util.PrintReceipt(_SELL_NO);
                    }
                    else if (Param.PrintType == "A")
                    {
                        if (MessageBox.Show("คุณต้องการพิมพ์ใบเสร็จรับเงินหรือไม่ ?", "การพิมพ์", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            Util.PrintReceipt(_SELL_NO);
                    }
                }
                else
                {
                    MessageBox.Show("กรุณาตรวจสอบจำนวนเงินที่รับมาอีกครั้ง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { }
        }

        private void txtCash_Enter(object sender, EventArgs e)
        {
            Util.SetKeyboardLayout(Util.GetInputLanguageByName("US"));
            //btnSave_Click(sender, (e));
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}