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
    public partial class FmCancelProduct : DevExpress.XtraEditors.XtraForm
    {
        public FmCancelProduct()
        {
            InitializeComponent();
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                lblStatus.Visible = false;

                Param.BarcodeNo = txtBarcode.Text;

                DataTable dt = Util.DBQuery(string.Format(@"SELECT b.OrderNo, p.Product 
                                    FROM Barcode b 
                                    LEFT JOIN Product p 
                                    ON b.product = p.Product 
                                    WHERE b.Barcode = '{0}'", txtBarcode.Text));

                if (dt.Rows.Count == 0)
                {
                    dt = Util.DBQuery(string.Format(@"SELECT Product, Barcode FROM Product WHERE SKU = '{0}'", txtBarcode.Text));
                    Console.WriteLine(txtBarcode.Text + " " + Param.BarcodeNo + " " + dt.Rows.Count.ToString());

                    if (dt.Rows.Count == 0)
                    {
                        //dt = Util.DBQuery(string.Format(@"SELECT Barcode FROM Product WHERE Barcode LIKE '%{0}%' OR Name LIKE '%{0}%'", txtBarcode.Text));
                        //if (dt.Rows.Count == 0)
                        //{
                        //    lblStatus.Text = "ไม่พบข้อมูลสินค้าชิ้นนี้ในระบบ";
                        //    lblStatus.ForeColor = Color.Red;
                        //}
                        //else
                        //{
                        //    Param.status = "Cancel";
                        //    FmSelectProduct frm = new FmSelectProduct();
                        //    var result = frm.ShowDialog(this);
                        //    if (result == System.Windows.Forms.DialogResult.OK)
                        //    {
                        //        Param.UcSale.LoadData();
                        //        txtBarcode.Text = "";
                        //    }

                        //}
                        MessageBox.Show("กรุณากรอกบาร์โค้ด ที่ต้องการยกเลิกการขาย", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        Param.status = "Cancel";
                        FmProductQty frm = new FmProductQty();
                        Param.product = dt.Rows[0]["Product"].ToString();
                        var result = frm.ShowDialog(this);
                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            Param.UcSale.LoadData();
                        }
                    }
                }
                else
                {
                    lblStatus.Visible = false;
                    dt = Util.DBQuery(string.Format(@"SELECT p.Name, p.Product , IFNULL(p.Price, 0) Price, IFNULL(p.Price1, 0) Price1, IFNULL(p.Price2, 0) Price2, ReceivedDate, ReceivedBy, SellDate, SellBy 
                                    FROM Barcode b 
                                    LEFT JOIN Product p 
                                    ON b.Product = p.Product 
                                    WHERE b.Barcode = '{0}' AND b.Shop = '{1}' AND SellBy <> '' ", txtBarcode.Text, Param.ShopId));

                    if (dt.Rows.Count == 1)
                    {
                        Util.DBExecute(string.Format(@"UPDATE Barcode SET SellPrice = '0',SellBy = '', Sync = 1 WHERE SellBy = '{0}' AND Barcode = '{1}'", Param.DeviceID, txtBarcode.Text));
                        Param.UcSale.LoadData();
                        txtBarcode.Text = "";
                    }
                    else
                    {
                        txtBarcode.Text = "";
                        lblStatus.Visible = true;
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FmCancelProduct_Load(object sender, EventArgs e)
        {
            lblStatus.Visible = false;
        }
    }
}