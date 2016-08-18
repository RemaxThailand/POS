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
        bool _CASH;

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
        private void CheckedDiscount(object sender, EventArgs e)
        {
            txtDiscountBath.Enabled = rdbBath.Checked;
            txtDiscountPer.Enabled = rdbPercent.Checked;

            //if (rdbBath.Checked)
            //{
                
            //}
            //else if (rdbPercent.Checked)
            //{ }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _CASH = false;
                if (txtCash.Text != "")
                {
                    DataTable dt = Util.DBQuery(string.Format(@"SELECT IFNULL(SUBSTR(MAX(creditNo), 1,6)||SUBSTR('0000'||(SUBSTR(MAX(creditNo), 7, 4)+1), -4, 4), SUBSTR(STRFTIME('%Y%m{0}D'), 3)||'0001') creditNo
                    FROM CreditCustomer
                    WHERE SUBSTR(creditNo, 1, 4) = SUBSTR(STRFTIME('%Y%m'), 3, 4)
                    AND SUBSTR(creditNo, 5, 1) = '{0}'", Param.DevicePrefix));
                    var CreditNo = dt.Rows[0]["creditNo"].ToString();

                    if (txtCash.Text == "0")
                    {
                        if (Param.SelectCustomerId == "000000")
                        {
                            MessageBox.Show("กรุณาตรวจสอบจำนวนเงินที่รับ\nหรือข้อมูลลูกค้าให้ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if (MessageBox.Show("ข้อมูลการขายนี้ จะถูกบันทึกลงข้อมูลลูกหนี้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                Util.DBExecute(string.Format(@"INSERT INTO CreditCustomer (shop, creditNo, sellNo, sync)  SELECT '{2}','{0}', '{1}', 1", CreditNo, _SELL_NO, Param.ShopId));
                                _CASH = true;
                            }
                        }
                    }
                    else
                    {
                        _CASH = true;
                    }


                    if (_CASH)
                    {
                        dt = Util.DBQuery(string.Format(@"SELECT b.Barcode, b.SellNo, b.SellPrice Price FROM Barcode b WHERE b.sellBy = '{0}'", Param.DeviceID));

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Util.DBExecute(string.Format(@"UPDATE Barcode
                        SET SellBy = '{0}', SellDate = STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), SellNo = '{1}', Sync = 1, SellPrice = {2}, Customer = '{3}'
                        WHERE Barcode = '{4}'",
                                Param.UserId, _SELL_NO, dt.Rows[i]["Price"].ToString(), Param.SelectCustomerId,
                                dt.Rows[i]["Barcode"].ToString(), dt.Rows[i]["SellNo"].ToString()));

                            //Util.DBExecute(string.Format(@"INSERT INTO SellDetail (SellNo, Product, Barcode, SellPrice, Cost, Sync)
                            //SELECT '{0}', Product, Barcode, SellPrice, Cost, 1 FROM Barcode WHERE SellNo = '{0}' AND Barcode = '{1}'", _SELL_NO, dt.Rows[i]["Barcode"].ToString()));
                        }

                        DataTable dtF = Util.DBQuery(string.Format(@"SELECT SUM(SellPrice) SellPrice, SUM(Cost) Cost, SUM(OperationCost) OP, (SUM(SellPrice) - SUM(Cost) ) Profit FROM (
                            SELECT IFNULL(SUM(SellPrice),0) SellPrice, IFNULL(SUM(Cost),0) Cost, IFNULL(SUM(OperationCost),0) OperationCost FROM Barcode WHERE SellNo = '{0}') aa", _SELL_NO));

                        Util.DBExecute(string.Format(@"INSERT INTO SellHeader (SellNo, Profit, TotalPrice, Customer, CustomerSex, CustomerAge, SellDate, SellBy)
                            SELECT '{0}', '{6}','{5}', '{1}', '{2}', {3}, STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), '{4}'",
                                _SELL_NO, Param.SelectCustomerId, Param.SelectCustomerSex, Param.SelectCustomerAge, Param.UserId, _TOTAL, dtF.Rows[0]["Profit"].ToString()));


                        Util.DBExecute(string.Format(@"INSERT INTO SellDetail (SellNo, Product, SellPrice, Cost, Quantity, Sync)
                            SELECT  '{0}' sellNo, Product, SUM(SellPrice) TotalPrice, SUM(Cost) PriceCost, COUNT(*) Amount, 1 FROM Barcode WHERE SellNo = '{0}' GROUP BY Product,   Product", _SELL_NO));

                        DataTable dtS = Util.DBQuery(string.Format(@"SELECT * FROM SellTemp"));
                        for (int i = 0; i < dtS.Rows.Count; i++)
                        {
                            DataTable dtP = Util.DBQuery(string.Format(@"SELECT * FROM Product WHERE product = '{0}'", dtS.Rows[i]["Product"].ToString()));
                            int Amount = Convert.ToInt32(dtP.Rows[0]["quantity"].ToString()) - Convert.ToInt32(dtS.Rows[i]["Amount"].ToString());

                            Util.DBExecute(string.Format(@"UPDATE Product SET Quantity = '{0}', Sync = 1 WHERE Product = '{1}' AND shop = '{2}'", Amount.ToString(), dtS.Rows[i]["Product"].ToString(), Param.ShopId));
                        }

                        //Util.DBExecute(string.Format(@"INSERT INTO SellHeader (SellNo, Profit, TotalPrice, Customer, CustomerSex, CustomerAge, SellDate, SellBy)
                        //    SELECT '{0}', (SELECT SUM(SellPrice-Cost-OperationCost) FROM Barcode WHERE SellNo = '{0}'),
                        //    (SELECT SUM(SellPrice) FROM Barcode WHERE SellNo = '{0}'), '{1}', '{2}', {3}, STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), '{4}'",
                        //        _SELL_NO, Param.SelectCustomerId, Param.SelectCustomerSex, Param.SelectCustomerAge, Param.UserId));

                        Util.DBExecute(string.Format(@"UPDATE ChangePrice SET SellNo = '{0}', Sync = 1 WHERE SellNo = '{1}'", _SELL_NO, Param.DeviceID));


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

                            Util.DBExecute(string.Format(@"DELETE FROM SellTemp"));

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
                }
                else
                {
                    MessageBox.Show("กรุณาตรวจสอบจำนวนเงินที่รับมาอีกครั้ง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
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