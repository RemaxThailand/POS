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
    public partial class FmChangePrice : DevExpress.XtraEditors.XtraForm
    {
        public FmChangePrice()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //เช็คข้อมูลก่อนว่า เลขที่การขาย, รหัสสินค้า เคยเปลี่ยนราคารึยัง ถ้าเคยเปลี่ยนแล้ววให้ Update ถ้ายังให้ Insert//
                //เช็คข้อมูลว่า เปลี่ยนราคาแล้วต่ำกว่าทุนหรือไม่ ถ้าต่ำกว่าต้องมีแจ้งเตือน ว่าต้องการขายราคานี้หรือไม่ , ถ้าไม้ต่ำกว่าก็สามารถขายได้เลย แค่แสดง//
                // Insert ข้อมูลการเปลี่ยนราคาลงในตาราง ChangePrice //
                //Column SellNo, Price, Product, ChangeBy, ChangeDate

                DataTable dt = Util.DBQuery(string.Format(@"SELECT * FROM Employee WHERE code = '{0}' AND shop = '{1}'", txtCode.Text, Param.ShopId));
                if (dt.Rows.Count > 0)
                {
                    Param.UserCode = dt.Rows[0]["code"].ToString();


                    dt = Util.DBQuery(string.Format(@"SELECT * FROM Employee WHERE code = '{0}'", txtCode.Text));

                    if (dt.Rows.Count > 0)
                    {
                        //DataTable dt = Util.DBQuery(string.Format(@"SELECT product, cost FROM Barcode WHERE sellBy = '{0}' AND product = '{1}'
                        //    UNION ALL SELECT product, cost FROM Product WHERE product = '{1}' AND cost <> 0", Param.CpuId, UcSale.product));

                        //if (Convert.ToInt32(UcSale.price) >= Convert.ToInt32(dt.Rows[0]["cost"].ToString()))
                        //{
                        dt = Util.DBQuery(string.Format(@"SELECT * FROM ChangePrice WHERE sellNo = '{0}' AND product = '{1}'", Param.CpuId, UcSale.product));
                        if (dt.Rows.Count > 0)
                        {
                            Util.DBExecute(string.Format(@"UPDATE ChangePrice SET price = '{0}' WHERE sellNO = '{1}' AND product = '{2}'", UcSale.price, Param.CpuId, UcSale.product));
                        }
                        else
                        {
                            Util.DBExecute(string.Format(@"INSERT INTO ChangePrice (shop, sellNo, product, price, changeBy, changeDate)
                            SELECT '{0}','{1}','{2}','{3}','{4}', STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW')",
                            Param.ShopId, Param.CpuId, UcSale.product, UcSale.price, Param.UserCode));
                        }

                        this.DialogResult = DialogResult.OK;
                        this.Dispose();
                        //}
                        //else
                        //{
                        //    if(MessageBox.Show("ราคาขายต่ำกว่าราคาต้นทุน คุณต้องการขายหรือไม่", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //    {
                        //        dt = Util.DBQuery(string.Format(@"SELECT * FROM ChangePrice WHERE sellNo = '{0}' AND product = '{1}'", Param.CpuId, UcSale.product));
                        //        if (dt.Rows.Count > 0)
                        //        {
                        //            Util.DBExecute(string.Format(@"UPDATE ChangePrice SET price = '{0}' WHERE sellNO = '{1}' AND product = '{2}'", UcSale.price, Param.CpuId, UcSale.product));
                        //        }
                        //        else
                        //        {
                        //            Util.DBExecute(string.Format(@"INSERT INTO ChangePrice (shop, sellNo, product, price, changeBy, changeDate)
                        //        SELECT '{0}','{1}','{2}','{3}','{4}', STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW')",
                        //            Param.ShopId, Param.CpuId, UcSale.product, UcSale.price, Param.UserId));
                        //        }

                        //        this.DialogResult = DialogResult.OK;
                        //        this.Dispose();
                        //    }
                    }
                }
                else
                {
                    MessageBox.Show("ข้อมูลไม่ถูกต้อง กรุณากรอกข้อมูลอีกครั้ง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCode.Text = "";
                    txtCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void txtCode_Enter(object sender, EventArgs e)
        {
            Util.SetKeyboardLayout(Util.GetInputLanguageByName("US"));
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
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
    }
}