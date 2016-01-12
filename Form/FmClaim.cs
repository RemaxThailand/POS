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
    public partial class FmClaim : DevExpress.XtraEditors.XtraForm
    {
        public FmClaim()
        {
            InitializeComponent();
        }

        private void checkRadio(object sender, EventArgs e)
        {
            txtBarcode.Enabled = rdbSwap.Checked;
            txtCash.Enabled = rdbCash.Checked;
            gbxCustomer.Enabled = rdbHq.Checked;
            if (rdbSwap.Checked) { txtBarcode.Focus(); pnPrice.Visible = true; }
            else if (rdbCash.Checked) { txtCash.Focus(); pnPrice.Visible = true; }
            else if (rdbHq.Checked) { txtMobile.Focus(); pnPrice.Visible = false; }

            gbxCustomer.Visible = rdbHq.Checked;
            this.Height = (gbxCustomer.Visible) ? 535 : 355;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณแน่ใจหรือไม่ ที่จะทำการเคลมสินค้านี้ ?", "ยืนยันข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataTable dt = Util.DBQuery(string.Format(@"SELECT IFNULL(SUBSTR(MAX(ClaimNo), 1,6)||SUBSTR('0000'||(SUBSTR(MAX(ClaimNo), 7, 4)+1), -4, 4), SUBSTR(STRFTIME('%Y%m{0}C'), 3)||'0001') ClaimNo
                            FROM Claim
                            WHERE SUBSTR(ClaimNo, 1, 4) = SUBSTR(STRFTIME('%Y%m'), 3, 4)
                            AND SUBSTR(ClaimNo, 5, 1) = '{0}'", Param.DevicePrefix));
                var ClaimNo = dt.Rows[0]["ClaimNo"].ToString();
                if (rtbDetail.Text != "")
                {
                    if (rdbSwap.Checked)
                    {
                        //ClaimType = 1 , เพิ่มข้อมูลใส่คอลัมท์ BarcodeClaim, Description, Price
                        if (txtBarcode.Text != "")
                        {
                            //Util.DBExecute(string.Format(@"INSERT INTO Claim (ClaimNo, ClaimType, ClaimDate, Product, Barcode, BarcodeClaim, Price, Description, ClaimBy, Sync)
                            //                        SELECT '{0}', '1', STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', 1 ",
                            //ClaimNo, UcClaim.Product, UcClaim.barcode, txtBarcode.Text, UcClaim.sellprice, rtbDetail.Text, Param.UserId));

                            //Util.DBExecute(string.Format(@"UPDATE Barcode SET Comment = 'เคลมสินค้า(เปลี่ยนสินค้า)' ,Sync = 1 WHERE Barcode = '{0}'", UcClaim.barcode));
                            //Util.DBExecute(string.Format(@"UPDATE Barcode SET Comment = 'เปลี่ยนสินค้า[{1}]',SellPrice = '{4}', SellDate = '{2}', SellBy = '{3}' ,Sync = 1 WHERE Barcode = '{0}'", txtBarcode.Text, UcClaim.barcode, UcClaim.SellDate, Param.UserId, UcClaim.sellprice));

                            //MessageBox.Show("บันทึกข้อมูลเรียบร้อยแล้ว", "แจ้งการบันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //txtClear();
                        }
                        else
                        {
                            MessageBox.Show("กรุณากรอกบาร์โค้ดที่ต้องการเปลี่ยน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else if (rdbCash.Checked)
                    {
                        ////ClaimType = 2 , เพิ่มข้อมูลใส่คอลัมท์ Price, Description
                        //if (Convert.ToDouble(txtCash.Text) <= Convert.ToInt32(lblSellPrice.Text))
                        //{
                        //    Util.DBExecute(string.Format(@"INSERT INTO Claim (ClaimNo, ClaimType, ClaimDate, Product, Barcode, Price, PriceClaim, Description, ClaimBy, Sync)
                        //                                SELECT '{0}', '2', STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', 1",
                        //        ClaimNo, UcClaim.Product, UcClaim.barcode, UcClaim.sellprice, txtCash.Text, rtbDetail.Text, Param.UserId));

                        //    Util.DBExecute(string.Format(@"UPDATE Barcode SET Comment = 'เคลมสินค้า(คืนเงิน)' ,Sync = 1 WHERE Barcode = '{0}'", UcClaim.barcode));

                        //    MessageBox.Show("บันทึกข้อมูลเรียบร้อยแล้ว", "แจ้งการบันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    txtClear();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("กรุณาตรวจสอบจำนวนเงินอีกครั้ง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //}
                    }
                    else if (rdbHq.Checked)
                    {
                        ////ClaimType = 3 , เพิ่มข้อมูลใส่คอลัมท์ Firstname, Lastname, Nickname, Tel, Email, Description
                        //if (txtMobile.Text != "")
                        //{
                        //    Util.DBExecute(string.Format(@"INSERT INTO Claim (ClaimNo, ClaimType, ClaimDate, Product, Barcode, Price, Description, Firstname, Lastname, Nickname, Tel, Email, ClaimBy, Sync)
                        //                                SELECT '{0}', '3', STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', 1",
                        //        ClaimNo, UcClaim.Product, UcClaim.barcode, UcClaim.sellprice, rtbDetail.Text, txtName.Text, txtLastname.Text, txtNickname.Text, txtMobile.Text, txtEmail.Text, Param.UserId));

                        //    Util.DBExecute(string.Format(@"UPDATE Barcode SET Comment = 'เคลมสินค้า(ส่งคืนสำนักงาน)' ,Sync = 1 WHERE Barcode = '{0}'", UcClaim.barcode));

                        //    MessageBox.Show("บันทึกข้อมูลเรียบร้อยแล้ว", "แจ้งการบันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    txtClear();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("กรุณากรอกข้อมูลลูกค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //}
                    }
                }
                else
                {
                    MessageBox.Show("กรุณากรอกรายละเอียดการเคลม", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtClear()
        {
            txtBarcode.Text = "";
            txtCash.Text = "";
            rtbDetail.Text = "";
            txtName.Text = "";
            txtLastname.Text = "";
            txtNickname.Text = "";
            txtMobile.Text = "";
            txtEmail.Text = "";
        }
    }
}