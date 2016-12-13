using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Globalization;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;

namespace PowerPOS
{
    public partial class FmClaimAdd : DevExpress.XtraEditors.XtraForm
    {
        dynamic _JSON_BARCODE;
        dynamic _JSON_CLAIMADD;
        dynamic _JSON_BARCODEEXIST;
        dynamic _JSON_PROVINCE;
        dynamic _JSON_DISTRICT;

        DataTable _TABLE_BARCODE;
        DataRow row;
        bool _SUCCESS;
        bool _FIRSTLOAD;

        public FmClaimAdd()
        {
            InitializeComponent();
        }

        private void textBarcode_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            lblProductCode.Text = "-";
            lblBarcode.Text = "-";
            lblProductName.Text = "-";
            lblSellDate.Text = "-";
            lblWarranty.Text = "-";
            lblLastShop.Text = "-";
            lblSellPrice.Text = "-";
            textDescription.Text = null;
            textDescription.Enabled = false;
            btnAdd.Enabled = false;
            btnRemove.Enabled = false;

            lblWarranty.BackColor = default(Color);
            lblWarranty.Font = new Font(lblWarranty.Font, FontStyle.Regular);
            lblSellDate.BackColor = default(Color);
            lblSellDate.Font = new Font(lblSellDate.Font, FontStyle.Regular);

            splashScreenManager.ShowWaitForm();
            bwBarcode.RunWorkerAsync();
        }

        private void FmClaimAdd_Load(object sender, EventArgs e)
        {
            _TABLE_BARCODE = new DataTable();
            addClaimGridControl.MainView = addClaimGridView;
            for (int i = 0; i < ((ColumnView)addClaimGridControl.MainView).Columns.Count; i++)
            {
                _TABLE_BARCODE.Columns.Add(addClaimGridView.Columns[i].FieldName);
            }
            _FIRSTLOAD = true;
            splashScreenManager.ShowWaitForm();
            bwLoadProvince.RunWorkerAsync();
        }

        private void bwBarcode_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _JSON_BARCODEEXIST = JsonConvert.DeserializeObject(Util.ApiProcess("/claim/barcodeExist", "shop=" + Param.ShopId + "&barcode=" + textBarcode.Text));
                if (_JSON_BARCODEEXIST.result[0].exist == true)
                {
                    MessageBox.Show("มีข้อมูลบาร์โค้ด " + textBarcode.Text + " นี้ในระบบแล้ว");

                }
                else
                {
                    _JSON_BARCODE = JsonConvert.DeserializeObject(Util.ApiProcess("/warranty/info", "shop=" + Param.ShopId + "&barcode=" + textBarcode.Text));
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error is : " + ex);
            }
        }

        private void bwBarcode_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (_JSON_BARCODEEXIST.result[0].exist == true)
                {
                    splashScreenManager.CloseWaitForm();
                    _JSON_BARCODE.Clear();
                }

                if (_JSON_BARCODE.success.Value)
                {
                    lblProductCode.Text = _JSON_BARCODE.result.sku.ToString();
                    lblBarcode.Text = _JSON_BARCODE.result.barcode.ToString();
                    lblProductName.Text = _JSON_BARCODE.result.productName.ToString();
                    lblSellDate.Text = _JSON_BARCODE.result.sellDate.ToString("dd/MM/yyyy").Substring(0, 10);
                    lblLastShop.Text = _JSON_BARCODE.result.shop.ToString();
                    lblWarranty.Text = _JSON_BARCODE.result.warranty.ToString();
                    lblSellPrice.Text = _JSON_BARCODE.result.sellPrice.ToString();
                    lblProduct.Text = _JSON_BARCODE.result.product.ToString();
                    lblSellNo.Text = _JSON_BARCODE.result.sellNo.ToString();

                    DateTime sellDate = Convert.ToDateTime(lblSellDate.Text);
                    DateTime today = DateTime.Now;
                    int wDay = Convert.ToInt32(lblWarranty.Text);
                    sellDate = sellDate.AddDays(wDay);
                    if (sellDate < today)
                    {
                        if (wDay == 0)
                        {
                            lblWarranty.BackColor = Color.Red;
                            lblWarranty.Font = new Font(lblWarranty.Font, FontStyle.Bold);
                            textDescription.Enabled = false;
                            btnAdd.Enabled = false;
                        }
                        else
                        {
                            lblSellDate.BackColor = Color.Red;
                            lblSellDate.Font = new Font(lblSellDate.Font, FontStyle.Bold);
                            textDescription.Enabled = false;
                            btnAdd.Enabled = false;
                        }
                    }
                    else
                    {
                        textDescription.Enabled = true;
                        btnRemove.Enabled = true;
                    }

                }
                else
                {
                    lblProductCode.Text = "-";
                    lblBarcode.Text = "-";
                    lblProductName.Text = "-";
                    lblSellDate.Text = "-";
                    lblLastShop.Text = "-";
                    lblWarranty.Text = "-";
                    lblSellPrice.Text = "-";
                    textDescription.Text = null;
                    textDescription.Enabled = false;
                    btnAdd.Enabled = false;
                    btnRemove.Enabled = false;
                    lblWarranty.BackColor = default(Color);
                    lblWarranty.Font = new Font(lblWarranty.Font, FontStyle.Regular);
                    lblSellDate.BackColor = default(Color);
                    lblSellDate.Font = new Font(lblSellDate.Font, FontStyle.Regular);
                }
                splashScreenManager.CloseWaitForm();
                _JSON_BARCODE.Clear();
            }
            catch { }
        }

        private void textBarcode_Enter(object sender, EventArgs e)
        {
            if (textBarcode.Text == "Barcode...")
            {
                textBarcode.Text = "";
            }
        }

        private void textBarcode_Leave(object sender, EventArgs e)
        {
            if (textBarcode.Text.Length == 0)
            {
                textBarcode.Text = "Barcode...";
            }
        }

        private void textBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Return))
            {
                lblProductCode.Text = "-";
                lblBarcode.Text = "-";
                lblProductName.Text = "-";
                lblSellDate.Text = "-";
                lblWarranty.Text = "-";
                lblSellPrice.Text = "-";
                lblLastShop.Text = "-";
                textDescription.Text = null;
                textDescription.Enabled = false;
                btnAdd.Enabled = false;
                btnRemove.Enabled = false;
                lblWarranty.BackColor = default(Color);
                lblWarranty.Font = new Font(lblWarranty.Font, FontStyle.Regular);
                lblSellDate.BackColor = default(Color);
                lblSellDate.Font = new Font(lblSellDate.Font, FontStyle.Regular);
                splashScreenManager.ShowWaitForm();
                bwBarcode.RunWorkerAsync();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                if (textCustomerName.Text != "" && textCustomerLastName.Text != "" && textAddress.Text != "" && textTel.Text != "")
                {
                    row = _TABLE_BARCODE.NewRow();
                    row[0] = textCustomerName.Text;
                    row[1] = lblBarcode.Text;
                    row[2] = lblProductName.Text;
                    row[3] = textDescription.Text;

                    row[4] = lblProduct.Text;
                    row[5] = lblLastShop.Text;
                    row[6] = lblSellPrice.Text;
                    row[7] = lblSellDate.Text;
                    row[8] = lblSellNo.Text;

                    row[9] = textCustomerLastName.Text;
                    row[10] = textCustomerNickname.Text;
                    row[11] = textAddress.Text;
                    row[12] = textAddress2.Text;
                    row[13] = comboProvince.Text;
                    row[14] = comboDistrict.Text;
                    row[15] = textSubDistrict.Text;
                    row[16] = textZipcode.Text;
                    row[17] = textTel.Text;
                    row[18] = textLineID.Text;

                    if (((ColumnView)addClaimGridControl.MainView).RowCount == 0)
                    {
                        _TABLE_BARCODE.Rows.Add(row);
                        addClaimGridControl.DataSource = _TABLE_BARCODE;
                        addClaimGridControl.RefreshDataSource();
                    }
                    else
                    {

                        DataRow[] isValidate = _TABLE_BARCODE.Select("barcode = " + lblBarcode.Text + "");
                        if (isValidate.Length != 0)
                        {
                            MessageBox.Show("บาร์โค้ด " + lblBarcode.Text + " ซ้ำ!");
                        }
                        else
                        {
                            _TABLE_BARCODE.Rows.Add(row);
                            addClaimGridControl.DataSource = _TABLE_BARCODE;
                            addClaimGridControl.RefreshDataSource();
                        }
                    }

                    if (!checkSameDesc.Checked)
                    {
                        textCustomerName.Text = "";
                        textCustomerLastName.Text = "";
                        textCustomerNickname.Text = "";
                        textAddress.Text = "";
                        textAddress2.Text = "";
                        textDescription.Text = "";
                        textSubDistrict.Text = "";
                        textTel.Text = "";
                        textLineID.Text = "";
                        textBarcode.Text = "";

                        lblProductCode.Text = "-";
                        lblBarcode.Text = "-";
                        lblProductName.Text = "-";
                        lblSellDate.Text = "-";
                        lblWarranty.Text = "-";
                        lblSellPrice.Text = "-";
                        lblLastShop.Text = "-";
                        textDescription.Text = null;
                        textDescription.Enabled = false;
                        btnAdd.Enabled = false;
                        lblWarranty.BackColor = default(Color);
                        lblWarranty.Font = new Font(lblWarranty.Font, FontStyle.Regular);
                        lblSellDate.BackColor = default(Color);
                        lblSellDate.Font = new Font(lblSellDate.Font, FontStyle.Regular);

                    }

                }
                else
                {
                    MessageBox.Show("กรุณาระบุข้อมูล * ให้ครบถ้วน");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error is: " + ex);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            addClaimGridView.DeleteRow(addClaimGridView.FocusedRowHandle);

        }

        private void textDescription_EditValueChanged(object sender, EventArgs e)
        {
            if (textDescription.Text.Length > 0)
            {
                btnAdd.Enabled = true;
            }
            else
            {
                btnAdd.Enabled = false;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //if(textCustomerName.Text != "")
            //{
            if (addClaimGridView.DataRowCount > 0)
            {
                splashScreenManager.ShowWaitForm();
                bwClaimAdd.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("กรุณาเพิ่มข้อมูล");
            }
            //}
            /*else
            {
                MessageBox.Show("กรุณาระบุชื่อลูกค้า");
            }*/

        }

        private void bwClaimAdd_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _SUCCESS = false;

                for (int i = 0; i < addClaimGridView.DataRowCount; i++)
                {

                    DateTime date = Convert.ToDateTime(addClaimGridView.GetRowCellValue(i, "sellDate").ToString());
                    string sellDate = date.ToString("MM/dd/yyyy");
                    _JSON_CLAIMADD = JsonConvert.DeserializeObject(Util.ApiProcess("/claim/add", "shop=" + Param.ApiShopId + "&from=" + "A" + "&barcode=" + addClaimGridView.GetRowCellValue(i, "barcode").ToString() + "&product=" + addClaimGridView.GetRowCellValue(i, "product").ToString() + "&description=" + addClaimGridView.GetRowCellValue(i, "description").ToString() + "&firstname=" + addClaimGridView.GetRowCellValue(i, "firstname").ToString() + "&lastname=" + addClaimGridView.GetRowCellValue(i, "lastname").ToString() + "&nickname=" + addClaimGridView.GetRowCellValue(i, "nickname").ToString() + "&address=" + addClaimGridView.GetRowCellValue(i, "address").ToString() + "&address2=" + addClaimGridView.GetRowCellValue(i, "address2").ToString() + "&province=" + addClaimGridView.GetRowCellValue(i, "province").ToString() + "&district=" + addClaimGridView.GetRowCellValue(i, "district").ToString() + "&subDistrict=" + addClaimGridView.GetRowCellValue(i, "subDistrict").ToString() + "&zipcode=" + addClaimGridView.GetRowCellValue(i, "zipcode").ToString() + "&tel=" + addClaimGridView.GetRowCellValue(i, "tel").ToString() + "&email=" + "-" + "&images=" + "-" + "&lastShop=" + addClaimGridView.GetRowCellValue(i, "lastShop").ToString() + "&sellNo=" + addClaimGridView.GetRowCellValue(i, "sellNo").ToString() + "&sellPrice=" + addClaimGridView.GetRowCellValue(i, "sellPrice").ToString() + "&usernameClaim=" + "claim" + "&customerLineId=" + addClaimGridView.GetRowCellValue(i, "lineId").ToString() + "&claimType=" + "-" + "&sellDate=" + sellDate));

                    _SUCCESS = true;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error is: " + ex);
            }
        }

        private void bwClaimAdd_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_SUCCESS)
            {
                splashScreenManager.CloseWaitForm();

                this.Close();
            }
        }

        private void textDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Return))
            {
                btnAdd_Click(sender, e);
            }
        }

        private void bwLoadProvince_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _JSON_PROVINCE = JsonConvert.DeserializeObject(Util.ApiProcess("/province/list", "&language=th"));

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error is : " + ex);
            }
        }

        private void bwLoadProvince_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (_JSON_PROVINCE.result.Count > 0)
                {
                    for (int i = 0; i < _JSON_PROVINCE.result.Count; i++)
                    {
                        comboProvince.Properties.Items.Add(_JSON_PROVINCE.result[i].name);
                    }


                    comboProvince.SelectedIndex = 0;
                    bwLoadDistrict.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("กรุณาทำการปิดและเปิดหน้าต่างนี้ไหมอีกครั้ง");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error is : " + ex);
            }
        }

        private void bwLoadDistrict_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var selectProvince = comboProvince.SelectedIndex + 1;
                _JSON_DISTRICT = JsonConvert.DeserializeObject(Util.ApiProcess("/province/district", "&province=" + selectProvince.ToString() + "&language=th"));

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error is : " + ex);
            }
        }

        private void bwLoadDistrict_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (_JSON_DISTRICT.result.Count > 0)
                {
                    for (int i = 0; i < _JSON_DISTRICT.result.Count; i++)
                    {
                        comboDistrict.Properties.Items.Add(_JSON_DISTRICT.result[i].name);

                    }

                    comboDistrict.SelectedIndex = 0;
                    textZipcode.Text = _JSON_DISTRICT.result[comboDistrict.SelectedIndex + 1].zipcode.ToString();
                    splashScreenManager.CloseWaitForm();
                    _FIRSTLOAD = false;

                }


                else
                {
                    MessageBox.Show("กรุณาทำการปิดและเปิดหน้าต่างนี้ไหมอีกครั้ง");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error is : " + ex);
            }
        }

        private void comboProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_FIRSTLOAD)
            {
                comboDistrict.Properties.Items.Clear();
                splashScreenManager.ShowWaitForm();
                bwLoadDistrict.RunWorkerAsync();
            }
        }

        private void comboDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_FIRSTLOAD)
            {
                textZipcode.Text = _JSON_DISTRICT.result[comboDistrict.SelectedIndex].zipcode.ToString();
            }
        }
    }
}