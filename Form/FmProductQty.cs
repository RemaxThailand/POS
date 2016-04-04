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
using System.Threading;
using System.Globalization;

namespace PowerPOS
{
    public partial class FmProductQty : DevExpress.XtraEditors.XtraForm
    {
        int _QTY, _COUNT;

        public FmProductQty()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Param.amount = txtAmount.Text;
                if (Param.status == "Received")
                {
                    DataTable dt = Util.DBQuery(string.Format(@"SELECT Product, Quantity FROM PurchaseOrder WHERE product = '{0}' AND OrderNo = '{1}'", Param.product, UcReceiveProduct.OrderNo));
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("ไม่พบข้อมูลสินค้าชิ้นนี้ในระบบ", "แจ้งเตือน");
                    }
                    else
                    {
                        if (txtAmount.Text.Trim() == "")
                        {
                            txtAmount.Focus();
                        }
                        else
                        {
                            if (Convert.ToInt32(dt.Rows[0]["Quantity"].ToString()) >= Convert.ToInt32(txtAmount.Text))
                            {
                                if (MessageBox.Show("คุณแน่ใจหรือไม่ ที่จะทำการรับสินค้านี้ ?", "ยืนยันข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                                    dt = Util.DBQuery(string.Format(@"SELECT Quantity, ReceivedQuantity FROM PurchaseOrder WHERE Product = '{0}' AND OrderNo = '{1}'", Param.product, UcReceiveProduct.OrderNo));

                                    int Received = Convert.ToInt32(dt.Rows[0]["ReceivedQuantity"].ToString()) + Convert.ToInt32(txtAmount.Text);

                                    if (Convert.ToInt32(dt.Rows[0]["Quantity"].ToString()) >= Received)
                                    {
                                        Util.DBExecute(string.Format(@"UPDATE PurchaseOrder SET ReceivedQuantity = '{0}', ReceivedDate = '{1}', ReceivedBy = '{2}', Sync = 1 WHERE product = '{3}' AND OrderNo = '{4}'",
                                            Received, DateTime.Now.ToString("yyyy-MM-dd"), Param.UserId, Param.product, UcReceiveProduct.OrderNo));
                                        dt = Util.DBQuery(string.Format(@"SELECT Product,IFNULL(Quantity,0) Quantity FROM Product WHERE Product = '{0}'", Param.product));

                                        int Amount = Convert.ToInt32(dt.Rows[0]["Quantity"].ToString()) + Convert.ToInt32(txtAmount.Text);

                                        dt = Util.DBQuery(string.Format(@"SELECT PriceCost FROM PurchaseOrder WHERE Product = '{0}'", Param.product));

                                        Util.DBExecute(string.Format(@"UPDATE Product SET Quantity = '{0}', Cost = {2}, Sync = 1 WHERE Product = '{1}' ", Amount.ToString(), Param.product, dt.Rows[0]["PriceCost"].ToString()));

                                        this.DialogResult = DialogResult.OK;
                                        this.Dispose();
                                    }
                                    else
                                    {
                                        MessageBox.Show("กรุณาตรวจสอบจำนวนที่ต้องการรับอีกครั้ง", "แจ้งเตือน");
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("กรุณาตรวจสอบจำนวนที่รับอีกครั้ง", "แจ้งเตือน");
                            }
                        }
                    }
                }
                else if (Param.status == "Sell")
                {
                    DataTable dt = Util.DBQuery(string.Format(@"SELECT Product, Quantity, Name FROM Product WHERE Product = '{0}' AND shop = '{1}' GROUP BY Product", Param.product, Param.ShopId));

                    if (dt.Rows[0]["Quantity"].ToString() == "0")
                    {
                        MessageBox.Show("ไม่พบข้อมูลสินค้าชิ้นนี้ในระบบ", "แจ้งเตือน");
                    }
                    else
                    {
                        if (txtAmount.Text.Trim() == "")
                        {
                            txtAmount.Focus();
                        }
                        else
                        {
                            if (Convert.ToInt32(dt.Rows[0]["Quantity"].ToString()) >= Convert.ToInt32(txtAmount.Text))
                            {
                                if (MessageBox.Show("คุณแน่ใจหรือไม่ ที่จะยืนยันการขายนี้ ?", "ยืนยันข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    dt = Util.DBQuery(string.Format(@"SELECT Product, Name, Quantity, Price{2}, Cost FROM Product WHERE Product = '{0}' AND shop = '{1}'", Param.product, Param.ShopId, Param.SelectCustomerSellPrice == 0 ? "" : "" + Param.SelectCustomerSellPrice));

                                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                                    Util.DBExecute(string.Format(@"INSERT INTO SellTemp (Product, ProductName, Price, Amount, TotalPrice, PriceCost) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                                        Param.product, dt.Rows[0]["Name"].ToString(), dt.Rows[0][3].ToString(), txtAmount.Text.Trim(), Convert.ToInt32(dt.Rows[0][3].ToString()) * Convert.ToInt32(txtAmount.Text), dt.Rows[0]["Cost"].ToString()));

                                    dt = Util.DBQuery(@"SELECT product, productName, price, amount FROM sellTemp");
                                    if (dt.Rows.Count > 0)
                                    {

                                        Util.DBExecute(string.Format(@"UPDATE sellTemp  SET
                                            Price = (SELECT p.Price{0} FROM Product p
                                            WHERE sellTemp.product = p.ID
                                            AND p.shop = '{1}'),
                                            TotalPrice = (SELECT p.Price{0} FROM Product p
                                            WHERE sellTemp.product = p.ID
                                            AND p.shop = '{1}') * sellTemp.Amount,
                                            PriceCost = (SELECT p.Cost FROM Product p
                                            WHERE sellTemp.product = p.ID
                                            AND p.shop = '{1}') * sellTemp.Amount", Param.SelectCustomerSellPrice == 0 ? "" : "" + Param.SelectCustomerSellPrice, Param.ShopId));
                                    }

                                    this.DialogResult = DialogResult.OK;
                                    this.Dispose();
                                }
                            }
                            else
                            {
                                MessageBox.Show("กรุณาตรวจสอบจำนวนที่ขายอีกครั้ง", "แจ้งเตือน");
                            }
                        }
                    }
                }
                else if (Param.status == "Return")
                {
                    DataTable dt = Util.DBQuery(string.Format(@"SELECT Product, SUM(Quantity) Quantity FROM SellDetail WHERE Product = '{0}' GROUP BY Product", Param.product));

                    if (dt.Rows[0]["Quantity"].ToString() == "0")
                    {
                        MessageBox.Show("ไม่พบข้อมูลสินค้าชิ้นนี้ในระบบ", "แจ้งเตือน");
                    }
                    else
                    {
                        if (txtAmount.Text.Trim() == "")
                        {
                            txtAmount.Focus();
                        }
                        else
                        {
                            if (Convert.ToInt32(dt.Rows[0]["Quantity"].ToString()) >= Convert.ToInt32(txtAmount.Text))
                            {
                                //Param.amount = txtAmount.Text;

                                this.DialogResult = DialogResult.OK;
                                this.Dispose();
                            }
                            else
                            {
                                MessageBox.Show("กรุณาตรวจสอบจำนวนที่คืนอีกครั้ง", "แจ้งเตือน");
                            }
                        }
                    }
                }
                else if (Param.status == "Stock")
                {
                    _QTY = 0;
                    _COUNT = 0;
                    DataTable dt = Util.DBQuery(string.Format(@"SELECT Product, Quantity FROM Product WHERE product = '{0}' AND Quantity <> 0 ", Param.product));
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("ไม่พบข้อมูลสินค้าชิ้นนี้ในระบบ", "แจ้งเตือน");
                    }
                    else
                    {
                        if (txtAmount.Text.Trim() == "")
                        {
                            txtAmount.Focus();
                        }
                        else
                        {
                            _QTY = Convert.ToInt32(dt.Rows[0]["Quantity"].ToString());
                            if (_QTY >= Convert.ToInt32(txtAmount.Text))
                            {
                                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                                dt = Util.DBQuery(string.Format(@"SELECT quantity FROM InventoryCount WHERE Product = '{0}'", Param.product));

                                if (dt.Rows.Count > 0)
                                {
                                    _COUNT = Convert.ToInt32(dt.Rows[0]["quantity"].ToString()) + Convert.ToInt32(txtAmount.Text);
                                }
                                else
                                {
                                    _COUNT = Convert.ToInt32(txtAmount.Text);
                                }

                                if (_QTY >= _COUNT)
                                {
                                    dt = Util.DBQuery(string.Format(@"SELECT product FROM InventoryCount WHERE Product = '{0}'", Param.product));

                                    if (dt.Rows.Count > 0)
                                    {
                                        Util.DBExecute(string.Format(@"UPDATE InventoryCount SET quantity = '{1}', Sync = 1 WHERE product = '{0}'", Param.product, _COUNT));
                                    }
                                    else
                                    {
                                        Util.DBExecute(string.Format(@"INSERT INTO InventoryCount (shop, product, quantity, sync)
                                             SELECT '{0}','{1}','{2}', 1 ", Param.ShopId, Param.product, _COUNT));
                                    }

                                    this.DialogResult = DialogResult.OK;
                                    this.Dispose();
                                }
                                else
                                {
                                    MessageBox.Show("กรุณาตรวจสอบจำนวนที่นับสินค้าอีกครั้ง", "แจ้งเตือน");
                                }
                            }
                            else
                            {
                                MessageBox.Show("กรุณาตรวจสอบจำนวนที่นับสินค้าอีกครั้ง", "แจ้งเตือน");
                            }
                        }
                    }
                }
                else if (Param.status == "Cancel")
                {
                    DataTable dt = Util.DBQuery(string.Format(@"SELECT Product, Amount, Price FROM SellTemp WHERE product = '{0}' ", Param.product, Param.ShopId));

                    if (dt.Rows[0]["Amount"].ToString() == "0")
                    {
                        MessageBox.Show("ไม่พบข้อมูลสินค้าชิ้นนี้ในรายการขาย", "แจ้งเตือน");
                    }
                    else
                    {
                        if (txtAmount.Text.Trim() == "")
                        {
                            txtAmount.Focus();
                        }
                        else
                        {
                            if (Convert.ToInt32(dt.Rows[0]["Amount"].ToString()) >= Convert.ToInt32(txtAmount.Text))
                            {
                                int qty = Convert.ToInt32(dt.Rows[0]["Amount"].ToString()) - Convert.ToInt32(txtAmount.Text);
                                int price = Convert.ToInt32(dt.Rows[0]["Price"].ToString()) * Convert.ToInt32(txtAmount.Text);

                                if (qty > 0)
                                {
                                    Util.DBExecute(string.Format(@"UPDATE SellTemp SET Amount = '{0}', TotalPrice = '{2}' WHERE product = '{1}'", qty, Param.product, price));
                                }
                                else
                                {
                                    Util.DBExecute(string.Format(@"DELETE FROM SellTemp WHERE Product = '{0}'", Param.product));
                                }

                                this.DialogResult = DialogResult.OK;
                                this.Dispose();
                            }
                            else
                            {
                                MessageBox.Show("กรุณาตรวจสอบจำนวนที่ยกเลิกอีกครั้ง", "แจ้งเตือน");
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private void txtAmount_Enter(object sender, EventArgs e)
        {
            Util.SetKeyboardLayout(Util.GetInputLanguageByName("US"));
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    btnSave_Click(sender, (e));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void FmProductQty_Load(object sender, EventArgs e)
        {
            if (Param.status == "Return")
            {
                FmReturnSell frm = new FmReturnSell();
                var result = frm.ShowDialog(this);
                if (result == System.Windows.Forms.DialogResult.OK)
                {

                }
            }
        }
    }
}