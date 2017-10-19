using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using Newtonsoft.Json;
using System.Globalization;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing.Printing;

namespace PowerPOS
{
    public partial class UcAddDataClaim : DevExpress.XtraEditors.XtraUserControl
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

        GridView _GRID_VIEW;
        dynamic _JSON_CLAIM;
        dynamic _JSON_CLAIM_UPDATE;
        dynamic _JSON_WARRANTY;
        dynamic _JSON_BARCODESWAP;
        Boolean DOSWAP;
        Boolean CLAIM_UPDATE;
        DataTable _TABLE_CLAIM;
        DataTable _TABLE_WARRANTY;
        DataRow _CLAIM_SELECTED;
        string _status;
        DateTime _receiveDate;
        DateTime _sentDate;
        string _claimNo;
        string _CLAIM_NO;
        string[] image;

        public UcAddDataClaim()
        {
            InitializeComponent();
        }

        private void textBarcodeSwap_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void btnBarcodeWarr_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                splashScreenManager.ShowWaitForm();
                bwWarrantyInfo.RunWorkerAsync();
            }
            catch
            { }
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

        private void UcAddDataClaim_Load(object sender, EventArgs e)
        {
            if (Param.MemberType == "Shop" || Param.MemberType == "" || Param.MemberType == null)
            {
                panelControl1.Visible = false;
                panelControl3.Visible = false;
                label1.Visible = true;
            }

            //if (Param.ApiShopId != "636C1CCE-5626-4AE0-B6D9-2A909BD37CF6")
            //{
            //    panelControl1.Visible = false;
            //    panelControl3.Visible = false;
            //    label1.Visible = true;
            //}


            //splashScreenManager.ShowWaitForm();
            _GRID_VIEW = (GridView)claimGridControl.MainView;
            Param.DataSet = new DataSet();
            bwLoadClaim.RunWorkerAsync();
            //loadDefault();
            //lblClaimNo.BackColor = this.BackColor;
            lblBarcode.BackColor = this.BackColor;

            _TABLE_BARCODE = new DataTable();
            addClaimGridControl.MainView = addClaimGridView;
            for (int i = 0; i < ((ColumnView)addClaimGridControl.MainView).Columns.Count; i++)
            {
                _TABLE_BARCODE.Columns.Add(addClaimGridView.Columns[i].FieldName);
            }
            _FIRSTLOAD = true;
            splashScreenManager.ShowWaitForm();
            bwLoadProvince.RunWorkerAsync();
            btnQuery_Click(sender, e);
        }



        private void bwBarcode_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
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
                //MessageBox.Show("Error is : " + ex);
                Console.WriteLine("Error is: " + ex);
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
                    lblSellDate.Text = _JSON_BARCODE.result.sellDate.ToString("dd/MM/yyyy");
                    lblLastShop.Text = _JSON_BARCODE.result.shop.ToString();
                    lblWarranty.Text = _JSON_BARCODE.result.warranty.ToString();
                    lblSellPrice.Text = _JSON_BARCODE.result.sellPrice.ToString();
                    lblProduct.Text = _JSON_BARCODE.result.product.ToString();
                    lblSellNo.Text = _JSON_BARCODE.result.sellNo.ToString();

                    string dd = _JSON_BARCODE.result.sellDate.ToString("MM/dd/yyyy");
                    DateTime sellDate = Convert.ToDateTime(dd);
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

                if (textCustomerName.Text != "" && textCustomerLastName.Text != "" && textAddress.Text != "" && txtMobile.Text != "")
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
                    row[17] = txtMobile.Text;
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
                        txtMobile.Text = "";
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
                //MessageBox.Show("Error is: " + ex);
                Console.WriteLine("Error is: " + ex);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            addClaimGridView.DeleteRow(addClaimGridView.FocusedRowHandle);
        }

        private void textDescription_EditValueChanged(object sender, EventArgs e)
        {
            if (textDetail.Text.Length > 0)
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

                //try
                //{
                //    if (_JSON_PROVINCE.result.Count > 0)
                //    {
                //        for (int i = 0; i < _JSON_PROVINCE.result.Count; i++)
                //        {
                //            comboProvince.Properties.Items.Add(_JSON_PROVINCE.result[i].name);
                //        }


                //        comboProvince.SelectedIndex = 0;
                //        bwLoadDistrict.RunWorkerAsync();
                //    }
                //    else
                //    {
                //        MessageBox.Show("กรุณาทำการปิดและเปิดหน้าต่างนี้ไหมอีกครั้ง");
                //    }


                //}
                //catch (Exception ex)
                //{
                //    //MessageBox.Show("Error is : " + ex);
                //    Console.WriteLine("Error is: " + ex);
                //}
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
                    string returntype;
                    if (rdbRS.Checked == true)
                    {
                        returntype = "RS";
                    }
                    else if (rdbRC.Checked == true)
                    {
                        returntype = "RC";
                    }
                    else
                    {
                        returntype = "RS";
                    }

                    _JSON_CLAIMADD = JsonConvert.DeserializeObject(Util.ApiProcess("/claim/addPos", "shop=" + Param.ApiShopId + "&from=" + "A" + "&barcode=" + addClaimGridView.GetRowCellValue(i, "barcode").ToString() + "&product=" + addClaimGridView.GetRowCellValue(i, "product").ToString() + "&description=" + addClaimGridView.GetRowCellValue(i, "description").ToString() + "&firstname=" + addClaimGridView.GetRowCellValue(i, "firstname").ToString() + "&lastname=" + addClaimGridView.GetRowCellValue(i, "lastname").ToString() + "&nickname=" + addClaimGridView.GetRowCellValue(i, "nickname").ToString() + "&address=" + addClaimGridView.GetRowCellValue(i, "address").ToString() + "&address2=" + addClaimGridView.GetRowCellValue(i, "address2").ToString() + "&province=" + addClaimGridView.GetRowCellValue(i, "province").ToString() + "&district=" + addClaimGridView.GetRowCellValue(i, "district").ToString() + "&subDistrict=" + addClaimGridView.GetRowCellValue(i, "subDistrict").ToString() + "&zipcode=" + addClaimGridView.GetRowCellValue(i, "zipcode").ToString() + "&tel=" + addClaimGridView.GetRowCellValue(i, "tel").ToString() + "&email=" + "-" + "&images=" + "-" + "&lastShop=" + addClaimGridView.GetRowCellValue(i, "lastShop").ToString() + "&sellNo=" + addClaimGridView.GetRowCellValue(i, "sellNo").ToString() + "&sellPrice=" + addClaimGridView.GetRowCellValue(i, "sellPrice").ToString() + "&usernameClaim=" + "claim" + "&customerLineId=" + addClaimGridView.GetRowCellValue(i, "lineId").ToString() + "&claimType=" + "-" + "&sellDate=" + sellDate+ "&returnType=" + returntype));

                    _SUCCESS = true;
                }


            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error is: " + ex);
                Console.WriteLine("Error is: " + ex);
            }
        }

        private void bwClaimAdd_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_SUCCESS)
            {
                splashScreenManager.CloseWaitForm();

                textCustomerName.EditValue = "";
                textCustomerLastName.EditValue = "";
                textCustomerNickname.EditValue = "";
                txtMobile.EditValue = "";
                txtLineID.EditValue = "";
                textDescription.EditValue = "";
                textAddress.EditValue = "";
                textAddress2.EditValue = "";
                textSubDistrict.EditValue = "";

                //splashScreenManager.ShowWaitForm();
                comboProvince.SelectedIndex = 0;
                bwLoadProvince.RunWorkerAsync();
                checkSameDesc.Checked = false;
                addClaimGridControl.DataSource = null;
                addClaimGridControl.RefreshDataSource();

                textBarcode.Text = "Barcode...";
                lblBarcode.Text = "-";
                lblProductCode.Text = "-";
                lblProductName.Text = "-";
                lblSellDate.Text = "-";
                lblWarranty.Text = "-";
                btnAdd.Enabled = false;
                btnRemove.Enabled = false;
                _TABLE_BARCODE.Clear();
                rdbRS.Checked = true;
                //this.Close();
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
                //MessageBox.Show("Error is : " + ex);
                Console.WriteLine("Error is: " + ex);
            }
        }

        private void bwLoadProvince_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                comboProvince.Properties.Items.Clear();
                if (_JSON_PROVINCE.result.Count > 0)
                {
                    for (int i = 0; i < _JSON_PROVINCE.result.Count; i++)
                    {
                        comboProvince.Properties.Items.Add(_JSON_PROVINCE.result[i].name);
                    }

                    comboProvince.SelectedIndex = 0;
                    bwLoadDistrict.RunWorkerAsync();
                    _FIRSTLOAD = false;

                }
                else
                {
                    MessageBox.Show("กรุณาทำการปิดและเปิดหน้าต่างนี้ไหมอีกครั้ง");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error is : " + ex);
                Console.WriteLine("Error is: " + ex);
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
                //MessageBox.Show("Error is : " + ex);
                Console.WriteLine("Error is: " + ex);
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

                    comboDistrict.SelectedIndex = comboDistrict.SelectedIndex + 1;
                    textZipcode.Text = _JSON_DISTRICT.result[comboDistrict.SelectedIndex].zipcode.ToString();
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
                //MessageBox.Show("Error is : " + ex);
                Console.WriteLine("Error is: " + ex);
            }
        }

        private void comboProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!_FIRSTLOAD)
                {
                    comboDistrict.Properties.Items.Clear();
                    splashScreenManager.ShowWaitForm();
                    bwLoadDistrict.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error is: " + ex);
            }
        }

        private void comboDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!_FIRSTLOAD)
                {
                    textZipcode.Text = _JSON_DISTRICT.result[comboDistrict.SelectedIndex].zipcode.ToString();
                    splashScreenManager.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error is: " + ex);
            }
        }

        private void bwLoadClaim_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                

                DateTime dateFrom = Convert.ToDateTime(claimDateFrom.Text);
                DateTime dateTo = Convert.ToDateTime(claimDateTo.Text);
                //string _dateFrom = dateFrom.ToString("MM/dd/yyyy", new CultureInfo("en-US"));
                string _dateFrom = dateFrom.ToString("MM/dd/yyyy", new CultureInfo("en-US"));
                string _dateTo = dateTo.ToString("MM/dd/yyyy", new CultureInfo("en-US"));
                string _shop = Param.ApiShopId;
                if (_shop == "POWERDDH-8888-8888-B620-48D3B6489999" || _shop == "9D7B3665-D502-4E6C-8C08-891C9E6C96A8" || _shop == "636C1CCE-5626-4AE0-B6D9-2A909BD37CF6" || _shop == "B2ED5F81-0FB5-4ACF-B0A1-385A806E0C2B")
                {
                    _shop = "";
                }
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                _TABLE_CLAIM = new DataTable();
                _JSON_CLAIM = JsonConvert.DeserializeObject(Util.ApiProcess("/claim/info", "shop=" + _shop + "&id=" + textNo.Text + "&barcode=" + barcode.Text + "&claimdate_from=" + _dateFrom + "&claimdate_to=" + _dateTo + "&status=" + "" + "&firstname=" + textFirstname.Text + "&lineid=" + textLineID.Text + "&tel=" + textTel.Text));

                if (_JSON_CLAIM.success.Value)
                {

                    DataRow row;
                    claimGridControl.MainView = claimInfoGridView;
                    for (int i = 0; i < ((ColumnView)claimGridControl.MainView).Columns.Count; i++)
                    {
                        _TABLE_CLAIM.Columns.Add(claimInfoGridView.Columns[i].FieldName);
                    }

                    for (int a = 0; a < _JSON_CLAIM.result[0].Count; a++)
                    {
                        string statusStr;
                        string status = _JSON_CLAIM.result[0][a].status.ToString();
                        switch (status)
                        {
                            case "CI":
                                statusStr = "รอตรวจสอบข้อมูล";
                                break;
                            case "AP":
                                statusStr = "อยู่ในเงื่อนไขการเคลม";
                                break;
                            case "RJ":
                                statusStr = "ไม่รับเคลม";
                                break;
                            case "RP":
                                statusStr = "ได้รับสินค้าเคลมแล้ว";
                                break;
                            case "WS":
                                statusStr = "รอส่ง";
                                break;
                            case "SH":
                                statusStr = "จัดส่งสินค้าเคลมให้ลูกค้าแล้ว";
                                break;
                            default:
                                statusStr = "กรุณาแจ้งผู้พัฒนาโปรแกรม";
                                break;
                        }
                        Param.shopClaimName = _JSON_CLAIM.result[0][a].shopCode.ToString();

                        row = _TABLE_CLAIM.NewRow();
                        row[0] = _JSON_CLAIM.result[0][a].shopName.ToString();
                        row[1] = _JSON_CLAIM.result[0][a].claimNo.ToString();
                        row[2] = _JSON_CLAIM.result[0][a].claimDate.ToString("dd/MM/yyyy");
                        row[3] = _JSON_CLAIM.result[0][a].firstname.ToString();
                        row[4] = _JSON_CLAIM.result[0][a].productName.ToString();
                        row[5] = statusStr;
                        row[6] = _JSON_CLAIM.result[0][a].status.ToString();

                        row[7] = _JSON_CLAIM.result[0][a].description.ToString();
                        row[8] = _JSON_CLAIM.result[0][a].images.ToString();
                        row[9] = _JSON_CLAIM.result[0][a].tel.ToString();
                        row[10] = _JSON_CLAIM.result[0][a].trackNo.ToString();

                        row[11] = _JSON_CLAIM.result[0][a].lastname.ToString();
                        row[12] = _JSON_CLAIM.result[0][a].nickname.ToString();
                        row[13] = _JSON_CLAIM.result[0][a].address.ToString();
                        row[14] = _JSON_CLAIM.result[0][a].address2.ToString();
                        row[15] = _JSON_CLAIM.result[0][a].subDistrict.ToString();
                        row[16] = _JSON_CLAIM.result[0][a].district.ToString();
                        row[17] = _JSON_CLAIM.result[0][a].province.ToString();
                        row[18] = _JSON_CLAIM.result[0][a].zipcode.ToString();

                        row[19] = _JSON_CLAIM.result[0][a].customerLineId.ToString();

                        row[20] = _JSON_CLAIM.result[0][a].remark.ToString();
                        row[21] = _JSON_CLAIM.result[0][a].receiveDate.ToString();
                        row[22] = _JSON_CLAIM.result[0][a].sentDate.ToString();
                        row[23] = _JSON_CLAIM.result[0][a].lastShop.ToString();
                        row[24] = _JSON_CLAIM.result[0][a].barcode.ToString();
                        row[25] = _JSON_CLAIM.result[0][a].barcodeClaim.ToString();

                        row[26] = _JSON_CLAIM.result[0][a].sellDate.ToString("dd/MM/yyyy");
                        row[27] = _JSON_CLAIM.result[0][a].price.ToString();
                        row[28] = _JSON_CLAIM.result[0][a].sku.ToString();
                        row[29] = _JSON_CLAIM.result[0][a].returnType.ToString();
                        _TABLE_CLAIM.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error is: " + ex);
                Console.WriteLine("Error is: " + ex);
            }
        }

        private void bwLoadClaim_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                splashScreenManager.CloseWaitForm();
                claimGridControl.DataSource = _TABLE_CLAIM;
                //lblClaimQty.Text = _JSON_CLAIM.result[1][0].qtyClaim.ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error is: " + ex);
                Console.WriteLine("Error is: " + ex);
            }
        }

        private void comboRefresh()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            customerAddress.EditValue = "";
            textDescription.EditValue = "";
            textCustomerLine.EditValue = "";
            lblSellDate.Text = "-";
            lblSellPrice.Text = "-";
            //sentDatePicker.Text = DateTime.Now.ToShortDateString();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {

            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            try
            {
                //DateTime dateFrom = claimDateFrom.Value.Day;
                //DateTime dateTo = claimDateTo.Value.Day;
                if (claimDateFrom.Value.Date > claimDateTo.Value.Date)
                {
                    comboRefresh();
                    claimGridControl.DataSource = null;
                    MessageBox.Show("กรุณาตรวจสอบเงื่อนไขวันที่");
                }
                else
                {
                    comboRefresh();
                    claimGridControl.DataSource = null;
                    if (textNo.Text != null && textNo.Text != "")
                    {
                        //statusRadioGroup.EditValue = "";
                        claimDateFrom.Text = "01/01/1900";
                        claimDateTo.Text = "01/01/9998";
                    }
                    else if (barcode.Text != null && barcode.Text != "")
                    {
                        //statusRadioGroup.EditValue = "";
                        claimDateFrom.Text = "01/01/1900";
                        claimDateTo.Text = "01/01/9998";
                    }
                    else if (textFirstname.Text != null && textFirstname.Text != "")
                    {
                        //statusRadioGroup.EditValue = "";
                        claimDateFrom.Text = "01/01/1900";
                        claimDateTo.Text = "01/01/9998";
                    }
                    else if (textLineID.Text != null && textLineID.Text != "")
                    {
                        //statusRadioGroup.EditValue = "";
                        claimDateFrom.Text = "01/01/1900";
                        claimDateTo.Text = "01/01/9998";
                    }
                    else if (textTel.Text != null && textTel.Text != "")
                    {
                        //statusRadioGroup.EditValue = "";
                        claimDateFrom.Text = "01/01/1900";
                        claimDateTo.Text = "01/01/9998";
                    }
                    splashScreenManager.ShowWaitForm();
                    bwLoadClaim.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error is: " + ex);
                Console.WriteLine("Error is: " + ex);

            }
        }

        private void textBarcodeWarr_Leave(object sender, EventArgs e)
        {
            if (textBarcodeWarr.Text.Length == 0)
            {
                textBarcodeWarr.Text = "Barcode...";
            }
        }

        private void textBarcodeWarr_Enter(object sender, EventArgs e)
        {
            if (textBarcodeWarr.Text == "Barcode...")
            {
                textBarcodeWarr.Text = "";
            }
        }

        private void bwWarrantyInfo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _TABLE_WARRANTY = new DataTable();
                _JSON_WARRANTY = JsonConvert.DeserializeObject(Util.ApiProcess("/warranty/info", "shop=" + Param.ApiShopId + "&barcode=" + textBarcodeWarr.Text));
                if (_JSON_WARRANTY.success.Value)
                {
                    DataRow row;
                    for (int i = 0; i < warrantyGridControl.Rows.Count; i++)
                    {
                        _TABLE_WARRANTY.Columns.Add(warrantyGridControl.Rows[i].Properties.FieldName);
                    }

                    row = _TABLE_WARRANTY.NewRow();
                    row[0] = _JSON_WARRANTY.result.productName.ToString();
                    row[1] = _JSON_WARRANTY.result.sellDate.ToString("dd/MM/yyyy");
                    row[2] = _JSON_WARRANTY.result.expireDate.ToString("dd/MM/yyyy");
                    _TABLE_WARRANTY.Rows.Add(row);
                }
            }
            catch { }
        }

        private void bwWarrantyInfo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                splashScreenManager.CloseWaitForm();
            }
            catch { }
            warrantyGridControl.DataSource = _TABLE_WARRANTY;
        }

        private void warrantyGridControl_RecordCellStyle(object sender, DevExpress.XtraVerticalGrid.Events.GetCustomRowCellStyleEventArgs e)
        {
            try
            {
                if (_JSON_WARRANTY != null)
                {
                    GridView gView = sender as GridView;
                    if (e.Row.Properties.FieldName == "expireDate")
                    {
                        DateTime expireDate = Convert.ToDateTime(_JSON_WARRANTY.result.expireDate.ToString());
                        DateTime today = DateTime.Now;
                        if (expireDate > today)
                        {
                            e.Appearance.BackColor = Color.CornflowerBlue;
                        }
                        else
                        {
                            e.Appearance.BackColor = Color.DarkRed;
                        }
                    }
                }
            }
            catch { }
        }

        private void textBarcodeWarr_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Return))
                {
                    splashScreenManager.ShowWaitForm();
                    bwWarrantyInfo.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error is: " + ex);
            }
        }

        private void textNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Return))
                {
                    btnQuery_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error is: " + ex);
            }
        }

        private void barcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Return))
                {
                    btnQuery_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error is: " + ex);
            }
        }

        private void textFirstname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Return))
                {
                    btnQuery_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error is: " + ex);
            }
        }

        private void btnAddress_Click(object sender, EventArgs e)
        {
            DateTime dateFrom = Convert.ToDateTime(claimDateFrom.Value);
            DateTime dateTo = Convert.ToDateTime(claimDateTo.Value);
            //string _dateFrom = dateFrom.ToString("MM/dd/yyyy", new CultureInfo("en-US"));
            string _dateFrom = dateFrom.ToString("MM/dd/yyyy", new CultureInfo("en-US"));
            string _dateTo = dateTo.ToString("MM/dd/yyyy", new CultureInfo("en-US"));

            FmEditAddress dialog = new FmEditAddress();
            dialog._claimNo = _claimNo;

            var result = dialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {

            }

        }

        private void claimInfoGridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                //DateTime d,dd;
                // string s;

                comboRefresh();
                //bwLoadImage.RunWorkerAsync();
                _claimNo = claimInfoGridView.GetRowCellDisplayText(claimInfoGridView.FocusedRowHandle, claimInfoGridView.Columns["claimNo"]).ToString();
                _CLAIM_SELECTED = _TABLE_CLAIM.AsEnumerable().Where(r => ((string)r["claimNo"]).Equals(_claimNo)).First();

                //lblClaimNo.Text = _CLAIM_SELECTED[1].ToString();
                lblCBarcode.Text = _CLAIM_SELECTED[24].ToString();

                textDetail.Text = _CLAIM_SELECTED[7].ToString();
                customerAddress.Text = _CLAIM_SELECTED[3].ToString() + " " + _CLAIM_SELECTED[11].ToString() + " " + _CLAIM_SELECTED[12].ToString() + Environment.NewLine + "ที่อยู่ " + _CLAIM_SELECTED[13].ToString() + " " + _CLAIM_SELECTED[14].ToString() + " แขวง/ตำบล" + _CLAIM_SELECTED[15].ToString() + " เขต/อำเภอ" + _CLAIM_SELECTED[16].ToString() + " จังหวัด" + _CLAIM_SELECTED[17].ToString() + " " + _CLAIM_SELECTED[18].ToString() + Environment.NewLine + "โทร " + _CLAIM_SELECTED[9].ToString();
                textCustomerLine.Text = _CLAIM_SELECTED[19].ToString();
                lblLastShop.Text = _CLAIM_SELECTED[23].ToString();
                lblCSellDate.Text = _CLAIM_SELECTED[26].ToString();
                lblSellPrice.Text = _CLAIM_SELECTED[27].ToString();
                lblCProductCode.Text = _CLAIM_SELECTED[28].ToString();
                lblCProductName.Text = _CLAIM_SELECTED[4].ToString();
                if (_CLAIM_SELECTED[29].ToString() == "RC")
                {
                    lblreturn.Text = "ส่งตามที่อยู่ลูกค้า";
                }
                else
                {
                    lblreturn.Text = "ส่งกลับร้าน";
                }

                //if (_CLAIM_SELECTED[25].ToString() != "" && _CLAIM_SELECTED[25].ToString() != null)
                //{
                //    textBarcodeSwap.Text = _CLAIM_SELECTED[25].ToString();
                //    checkChangeProduct.Checked = true;
                //    bwBarcodeSwap.RunWorkerAsync();
                //}

                /*if (_CLAIM_SELECTED[10].ToString() != "" &&  _CLAIM_SELECTED[22].ToString() != "" && _CLAIM_SELECTED[22].ToString() != null && _CLAIM_SELECTED[6].ToString() == "SH")
                {
                    comboBoxEdit1.SelectedIndex = 4;
                    textTrackNo.Show();
                    textTrackNo.Text = _CLAIM_SELECTED[10].ToString();
                    sentDatePicker.Show();
                    sentDatePicker.Text = _CLAIM_SELECTED[22].ToString();
                }
                else if (_CLAIM_SELECTED[20].ToString() != "" && _CLAIM_SELECTED[20].ToString() != "Remark" && _CLAIM_SELECTED[20].ToString() != null && _CLAIM_SELECTED[6].ToString() == "RJ")
                {
                    comboBoxEdit1.SelectedIndex = 1;
                    textRemark.Show();
                    textRemark.Text = _CLAIM_SELECTED[20].ToString();
                }
                else if (_CLAIM_SELECTED[21].ToString() != "" && _CLAIM_SELECTED[21].ToString() != null && _CLAIM_SELECTED[6].ToString() == "RP")
                {
                    comboBoxEdit1.SelectedIndex = 2;
                    receiveDatePicker.Show();
                    receiveDatePicker.Text = _CLAIM_SELECTED[21].ToString();
                }
                else if ( _CLAIM_SELECTED[6].ToString() == "WS")
                {
                    comboBoxEdit1.SelectedIndex = 3;
                    receiveDatePicker.Show();
                    receiveDatePicker.Text = _CLAIM_SELECTED[21].ToString();
                    /*sentDatePicker.Show();
                    sentDatePicker.Text = _CLAIM_SELECTED[22].ToString();
                }
                else if (_CLAIM_SELECTED[6].ToString() == "AP")
                {
                    comboBoxEdit1.SelectedIndex = 0;
                }
                else
                {
                    receiveDatePicker.Hide();
                    sentDatePicker.Hide();
                    textRemark.Hide();
                    textTrackNo.Hide();
                }*/

                //if (_CLAIM_SELECTED[6].ToString() == "SH")
                //{
                //    comboBoxEdit1.SelectedIndex = 4;
                //    textTrackNo.Show();
                //    textTrackNo.Text = _CLAIM_SELECTED[10].ToString();
                //    sentDatePicker.Show();
                //    //d = DateTime.Parse(_CLAIM_SELECTED[22].ToString());
                //    sentDatePicker.Text = _CLAIM_SELECTED[22].ToString();// d.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("en-US"));
                //}
                //else if (_CLAIM_SELECTED[6].ToString() == "RJ")
                //{
                //    comboBoxEdit1.SelectedIndex = 1;
                //    textRemark.Show();
                //    textRemark.Text = _CLAIM_SELECTED[20].ToString();
                //}
                //else if (_CLAIM_SELECTED[6].ToString() == "RP")
                //{
                //    comboBoxEdit1.SelectedIndex = 2;
                //    receiveDatePicker.Show();
                //    receiveDatePicker.Text = _CLAIM_SELECTED[21].ToString();
                //}
                //else if (_CLAIM_SELECTED[6].ToString() == "WS")
                //{
                //    comboBoxEdit1.SelectedIndex = 3;
                //    receiveDatePicker.Show();
                //    receiveDatePicker.Text = _CLAIM_SELECTED[21].ToString();
                //    /*sentDatePicker.Show();
                //    sentDatePicker.Text = _CLAIM_SELECTED[22].ToString();*/
                //}
                //else if (_CLAIM_SELECTED[6].ToString() == "AP")
                //{
                //    comboBoxEdit1.SelectedIndex = 0;
                //}
                //else
                //{
                //    receiveDatePicker.Hide();
                //    sentDatePicker.Hide();
                //    textRemark.Show();
                //    textTrackNo.Hide();
                //}

                //if (comboBoxEdit1.SelectedIndex == 4)
                //{
                //    textTrackNo.Show();
                //    sentDatePicker.Show();

                //    receiveDatePicker.Hide();
                //    textRemark.Show();
                //}
                //else if (comboBoxEdit1.SelectedIndex == 1)
                //{
                //    textRemark.Show();

                //    receiveDatePicker.Hide();
                //    sentDatePicker.Hide();
                //    textTrackNo.Hide();
                //}
                //else if (comboBoxEdit1.SelectedIndex == 2)
                //{
                //    receiveDatePicker.Show();

                //    sentDatePicker.Hide();
                //    textRemark.Show();
                //    textTrackNo.Hide();
                //}
                //else if (comboBoxEdit1.SelectedIndex == 3)
                //{
                //    receiveDatePicker.Show();

                //    sentDatePicker.Hide();
                //    textRemark.Show();
                //    textTrackNo.Hide();
                //}
                //else
                //{
                //    receiveDatePicker.Hide();
                //    sentDatePicker.Hide();
                //    textRemark.Show();
                //    textTrackNo.Hide();
                //}
            }
            catch
            {
            }
        }

        private void claimInfoGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView gView = sender as GridView;
            if (e.Column.FieldName == "statusStr")
            {
                string status = gView.GetRowCellDisplayText(e.RowHandle, gView.Columns["status"]);
                switch (status)
                {
                    case "CI":
                        e.Appearance.BackColor = Color.Crimson;
                        e.Appearance.ForeColor = Color.White;
                        break;
                    case "AP":
                        e.Appearance.BackColor = Color.CornflowerBlue;
                        e.Appearance.ForeColor = Color.White;
                        break;
                    case "RJ":
                        e.Appearance.BackColor = Color.DarkRed;
                        e.Appearance.ForeColor = Color.White;
                        break;
                    case "RP":
                        e.Appearance.BackColor = Color.CornflowerBlue;
                        e.Appearance.ForeColor = Color.White;
                        break;
                    case "WS":
                        e.Appearance.BackColor = Color.CornflowerBlue;
                        e.Appearance.ForeColor = Color.White;
                        break;
                    case "SH":
                        e.Appearance.BackColor = Color.ForestGreen;
                        e.Appearance.ForeColor = Color.White;
                        break;
                    default:
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;
                        break;
                }
            }
        }

        private void btnPrintClaim_Click(object sender, EventArgs e)
        {
            if (_CLAIM_NO != null)
            {
                Util.GetAddress(_CLAIM_NO, false);
                btnPrintClaim.Enabled = false;

                PaperSize paperSize = new PaperSize();
                paperSize.RawKind = (int)PaperKind.A4;

                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.PaperSize = paperSize;
                pd.PrintController = new System.Drawing.Printing.StandardPrintController();
                //pd.PrinterSettings.PrinterName = ConfigurationManager.AppSettings["PrinterName"];
                pd.PrinterSettings.PrinterName = Param.DevicePrinter;


                pd.PrintPage += (_, g) =>
                {
                    Util.PrintClaim(g, _CLAIM_NO);
                };
                pd.Print();

                btnPrintClaim.Enabled = true;
            }
        }

        private void claimGridControl_MouseClick(object sender, MouseEventArgs e)
        {
            _CLAIM_NO = claimInfoGridView.GetRowCellDisplayText(claimInfoGridView.FocusedRowHandle, claimInfoGridView.Columns["claimNo"]);
        }
    }
}
