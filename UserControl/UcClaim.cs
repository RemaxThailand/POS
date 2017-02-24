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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.BandedGrid;
using Newtonsoft.Json;
using System.Globalization;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using System.Net;
using System.Drawing.Printing;
using System.Configuration;
using System.Threading;

namespace PowerPOS
{
    public partial class UcClaim : DevExpress.XtraEditors.XtraUserControl
    {

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
        string[] image;

        public UcClaim()
        {
            InitializeComponent();
        }

        private void textBarcodeSwap_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                lblProductCodeSw.Text = "-";
                lblBarcodeSw.Text = "-";
                lblProductNameSw.Text = "-";
                lblSellDateSw.Text = "-";
                lblLastShopSw.Text = "-";
                lblWarrantySw.Text = "-";
                lblSellPriceSw.Text = "-";
                splashScreenManager.ShowWaitForm();
                bwBarcodeSwap.RunWorkerAsync();
            }
            catch
            { }
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

        private void UcClaim_Load(object sender, EventArgs e)
        {
            //if (Param.ApiShopId != "636C1CCE-5626-4AE0-B6D9-2A909BD37CF6")
            //{
            //    panelControl1.Visible = false;
            //    panelControl3.Visible = false;
            //    label1.Visible = true;
            //}

            //if (Param.MemberType == "Shop" || Param.MemberType == "" || Param.MemberType == null)
            //{
            //    panelControl1.Visible = false;
            //    panelControl3.Visible = false;
            //    label1.Visible = true;
            //}


            splashScreenManager.ShowWaitForm();
            _GRID_VIEW = (GridView)claimGridControl.MainView;
            Param.DataSet = new DataSet();
            bwLoadClaim.RunWorkerAsync();
            loadDefault();
            lblClaimNo.BackColor = this.BackColor;
            lblBarcode.BackColor = this.BackColor;
        }

        private void loadDefault()
        {
            statusRadioGroup.EditValue = "CI";
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
                if (_shop == "POWERDDH-8888-8888-B620-48D3B6489999" || _shop == "9D7B3665-D502-4E6C-8C08-891C9E6C96A8" || _shop == "636C1CCE-5626-4AE0-B6D9-2A909BD37CF6")
                {
                    _shop = "";
                }
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                _TABLE_CLAIM = new DataTable();
                _JSON_CLAIM = JsonConvert.DeserializeObject(Util.ApiProcess("/claim/info", "shop=" + _shop + "&id=" + textNo.Text + "&barcode=" + barcode.Text + "&claimdate_from=" + _dateFrom + "&claimdate_to=" + _dateTo + "&status=" + statusRadioGroup.Text + "&firstname=" + textFirstname.Text + "&lineid=" + textLineID.Text + "&tel=" + textTel.Text));

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

                        _TABLE_CLAIM.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error is: " + ex);
            }
        }

        private void bwLoadClaim_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                splashScreenManager.CloseWaitForm();
                claimGridControl.DataSource = _TABLE_CLAIM;
                lblClaimQty.Text = _JSON_CLAIM.result[1][0].qtyClaim.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error is: " + ex);
            }
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
                        statusRadioGroup.EditValue = "";
                        claimDateFrom.Text = "01/01/1900";
                        claimDateTo.Text = "01/01/9998";
                    }
                    else if (barcode.Text != null && barcode.Text != "")
                    {
                        statusRadioGroup.EditValue = "";
                        claimDateFrom.Text = "01/01/1900";
                        claimDateTo.Text = "01/01/9998";
                    }
                    else if (textFirstname.Text != null && textFirstname.Text != "")
                    {
                        statusRadioGroup.EditValue = "";
                        claimDateFrom.Text = "01/01/1900";
                        claimDateTo.Text = "01/01/9998";
                    }
                    else if (textLineID.Text != null && textLineID.Text != "")
                    {
                        statusRadioGroup.EditValue = "";
                        claimDateFrom.Text = "01/01/1900";
                        claimDateTo.Text = "01/01/9998";
                    }
                    else if (textTel.Text != null && textTel.Text != "")
                    {
                        statusRadioGroup.EditValue = "";
                        claimDateFrom.Text = "01/01/1900";
                        claimDateTo.Text = "01/01/9998";
                    }
                    splashScreenManager.ShowWaitForm();
                    bwLoadClaim.RunWorkerAsync();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error is: " + ex);
            }
        }

        private async void bwLoadImage_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                tileGroup1.Items.Clear();
                image = _CLAIM_SELECTED[8].ToString().Split('|');
                int idx = 0;
                for (int i = 0; i < image.Length; i++)
                {
                    if (image[i] != "" && image[i] != null)
                    {
                        try
                        {
                            TileItem itmImg = new TileItem();
                            TileItemElement tileItemElement1 = new TileItemElement();
                            tileItemElement1.Text = "กำลังโหลดรูปภาพ...";
                            itmImg.Elements.Add(tileItemElement1);
                            itmImg.BackgroundImageScaleMode = TileItemImageScaleMode.Stretch;
                            itmImg.Id = idx;
                            itmImg.ItemSize = TileItemSize.Medium;
                            itmImg.Name = image[i];
                            itmImg.AppearanceItem.Normal.ForeColor = Color.DarkGray;
                            tileGroup1.Items.Add(itmImg);

                            tileGroup1.Items[idx].BackgroundImage = await DownloadImage(image[i]);
                            itmImg.Elements.Remove(tileItemElement1);
                        }
                        catch
                        {
                        }

                        idx++;
                    }
                    else
                    {
                        try
                        {
                            TileItem itmImg = new TileItem();
                            TileItemElement tileItemElement1 = new TileItemElement();

                            //tileGroup1.Items[0].BackgroundImage = await DownloadImage("https://img.powerdd.com/product/default.jpg");
                            itmImg.Elements.Remove(tileItemElement1);
                        }
                        catch
                        {
                        }

                    }
                }
            }
            catch { }
        }

        private async Task<Image> DownloadImage(string url)
        {
            try
            {
                WebRequest requestPic = WebRequest.Create(url);
                WebResponse responsePic = await requestPic.GetResponseAsync();
                return Image.FromStream(responsePic.GetResponseStream());
            }
            catch
            {
                return null;
            }
        }

        private void tileControl1_ItemClick(object sender, TileItemEventArgs e)
        {
            FmClaimImg frm = new FmClaimImg();
            frm.imgUrl = e.Item.Name;
            frm.Text = _CLAIM_SELECTED[1].ToString() + "-" + _CLAIM_SELECTED[4].ToString();
            frm.ShowDialog();
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iStatus = comboBoxEdit1.SelectedIndex;
            switch (iStatus)
            {
                case 0:
                    _status = "AP";
                    break;
                case 1:
                    _status = "RJ";
                    break;
                case 2:
                    _status = "RP";
                    break;
                case 3:
                    _status = "WS";
                    break;
                case 4:
                    _status = "SH";
                    break;
            }
            if (comboBoxEdit1.SelectedIndex == 4)
            {
                textTrackNo.Show();
                sentDatePicker.Show();

                receiveDatePicker.Hide();
                textRemark.Show();
                textRemark.Text = "Remark";
            }
            else if (comboBoxEdit1.SelectedIndex == 1)
            {
                textRemark.Show();

                receiveDatePicker.Hide();
                sentDatePicker.Hide();
                textTrackNo.Hide();
                textTrackNo.Text = "Track No.";
            }
            else if (comboBoxEdit1.SelectedIndex == 2)
            {
                receiveDatePicker.Show();

                sentDatePicker.Hide();
                textRemark.Show();
                textTrackNo.Hide();
                textRemark.Text = "Remark";
                textTrackNo.Text = "Track No.";
            }
            else
            {
                receiveDatePicker.Hide();
                sentDatePicker.Hide();
                textRemark.Show();
                textTrackNo.Hide();
                textRemark.Text = "Remark";
                textTrackNo.Text = "Track No.";
            }
        }

        private void comboRefresh()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            comboBoxEdit1.Text = "กรุณาเลือกสถานะการเคลม...";
            comboBoxEdit1.EditValue = "กรุณาเลือกสถานะการเคลม...";

            customerAddress.EditValue = "";
            textDescription.EditValue = "";
            textCustomerLine.EditValue = "";
            textBarcodeSwap.EditValue = "Barcode...";
            tileGroup1.Items.Clear();
            checkChangeProduct.Checked = false;
            lblProductCodeSw.Text = "-";
            lblBarcodeSw.Text = "-";
            lblProductNameSw.Text = "-";
            lblSellDateSw.Text = "-";
            lblLastShopSw.Text = "-";
            lblWarrantySw.Text = "-";
            lblBarcode.Text = "-";
            lblClaimNo.Text = "-";
            lblSellPriceSw.Text = "-";
            lblSellDate.Text = "-";
            lblSellPrice.Text = "-";
            //sentDatePicker.Text = DateTime.Now.ToShortDateString();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            if (receiveDatePicker.Visible)
            {
                _receiveDate = Convert.ToDateTime(receiveDatePicker.Text);
            }
            else { _receiveDate = Convert.ToDateTime("01/01/1900"); }
            if (sentDatePicker.Visible)
            {
                _sentDate = Convert.ToDateTime(sentDatePicker.Value);
            }
            else { _sentDate = Convert.ToDateTime("01/01/1900"); }
            splashScreenManager.ShowWaitForm();
            bwUpdateClaim.RunWorkerAsync();
        }

        private void bwUpdateClaim_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            try
            {
                DOSWAP = false;
                CLAIM_UPDATE = false;
                if (checkChangeProduct.Checked)
                {
                    if (lblBarcode.Text == lblBarcodeSw.Text)
                    {
                        MessageBox.Show("Barcode สินค้าเคลม และสินค้าเปลี่ยนเคลม ซ้ำกัน");
                        CLAIM_UPDATE = false;
                    }
                    else
                    {
                        if (lblBarcodeSw.Text != "-" && lblBarcodeSw.Text != "")
                        {
                            string remark = (textRemark.Text == "Remark") ? "" : textRemark.Text;
                            string barcodeClaim = (lblBarcodeSw.Text != "-" && lblBarcodeSw.Text != "") ? lblBarcodeSw.Text : "";
                            _JSON_CLAIM_UPDATE = JsonConvert.DeserializeObject(Util.ApiProcess("/claim/update", "shop=" + Param.ApiShopId + "&id=" + _claimNo + "&column=" + "status,trackNo,receiveDate,sentDate,remark,barcodeClaim" + "&value=" + _status + "," + textTrackNo.Text + "," + _receiveDate.ToString("MM/dd/yyyy") + "," + _sentDate.ToString("MM/dd/yyyy") + "," + remark + "," + barcodeClaim));
                            DOSWAP = true;
                        }
                        else
                        {
                            MessageBox.Show("การเปลี่ยนสินค้ามีปัญหา กรุณาติดต่อผู้พัฒนาโปรแกรม");

                        }
                    }
                }
                else
                {
                    string remark = (textRemark.Text == "Remark") ? "" : textRemark.Text;
                    //string barcodeClaim = (lblBarcodeSw.Text != "-" && lblBarcodeSw.Text != "") ? lblBarcodeSw.Text : "";
                    //_JSON_CLAIM_UPDATE = JsonConvert.DeserializeObject(Util.ApiProcess("/claim/update", "shop=" + Param.ShopId + "&id=" + _claimNo + "&column=" + "status,trackNo,receiveDate,sentDate,remark" + "&value=" + _status + "," + textTrackNo.Text + "," + _receiveDate.ToString("MM/dd/yyyy") + "," + _sentDate.ToString("MM/dd/yyyy") + "," + remark));
                    string barcodeClaim = (lblBarcodeSw.Text != "-" && lblBarcodeSw.Text != "") ? lblBarcodeSw.Text : "";
                    _JSON_CLAIM_UPDATE = JsonConvert.DeserializeObject(Util.ApiProcess("/claim/update", "shop=" + Param.ApiShopId + "&id=" + _claimNo + "&column=" + "status,trackNo,receiveDate,sentDate,remark" + "&value=" + _status + "," + textTrackNo.Text + "," + _receiveDate.ToString("MM/dd/yyyy") + "," + _sentDate.ToString("MM/dd/yyyy") + "," + remark));

                    CLAIM_UPDATE = true;
                }
            }
            catch { }
        }

        private void bwUpdateClaim_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                splashScreenManager.CloseWaitForm();
            }
            catch { }
            if (CLAIM_UPDATE)
            {
                if (_JSON_CLAIM_UPDATE.success.Value)
                {
                    string statusStr;
                    string status = _status;
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

                    DataRow row = _TABLE_CLAIM.AsEnumerable().Where(r => ((string)r["claimNo"]).Equals(_claimNo)).First();
                    row[5] = statusStr;
                    row[6] = _status;
                    row[20] = (textRemark.Text == "Remark") ? "" : textRemark.Text;
                    claimGridControl.Refresh();

                    if (checkChangeProduct.Checked)
                    {
                        if (DOSWAP)
                        {
                            MessageBox.Show("เปลี่ยนสินค้าเคลมเรียบร้อยแล้ว");
                        }
                        else
                        {
                            MessageBox.Show("การเปลี่ยนสินค้ามีปัญหา กรุณาแจ้งผู้พัฒนาโปรแกรม");
                        }
                    }
                }
            }
            DOSWAP = false;
            CLAIM_UPDATE = false;
        }

        private void textTrackNo_Leave(object sender, EventArgs e)
        {
            if (textTrackNo.Text.Length == 0)
            {
                textTrackNo.Text = "Track No.";
            }
        }

        private void textTrackNo_Enter(object sender, EventArgs e)
        {
            if (textTrackNo.Text == "Track No.")
            {
                textTrackNo.Text = "";
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
                    row[1] = _JSON_WARRANTY.result.sellDate.ToString().Substring(0, 10);
                    row[2] = _JSON_WARRANTY.result.expireDate.ToString().Substring(0, 10);
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
            if ((e.KeyCode == Keys.Return))
            {
                splashScreenManager.ShowWaitForm();
                bwWarrantyInfo.RunWorkerAsync();
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

                lblClaimNo.Text = _CLAIM_SELECTED[1].ToString();
                lblBarcode.Text = _CLAIM_SELECTED[24].ToString();

                textDescription.Text = _CLAIM_SELECTED[7].ToString();
                customerAddress.Text = _CLAIM_SELECTED[3].ToString() + " " + _CLAIM_SELECTED[11].ToString() + " " + _CLAIM_SELECTED[12].ToString() + Environment.NewLine + "ที่อยู่ " + _CLAIM_SELECTED[13].ToString() + " " + _CLAIM_SELECTED[14].ToString() + " แขวง/ตำบล" + _CLAIM_SELECTED[15].ToString() + " เขต/อำเภอ" + _CLAIM_SELECTED[16].ToString() + " จังหวัด" + _CLAIM_SELECTED[17].ToString() + " " + _CLAIM_SELECTED[18].ToString() + Environment.NewLine + "โทร " + _CLAIM_SELECTED[9].ToString();
                textCustomerLine.Text = _CLAIM_SELECTED[19].ToString();
                lblLastShop.Text = _CLAIM_SELECTED[23].ToString();
                lblSellDate.Text = _CLAIM_SELECTED[26].ToString();
                lblSellPrice.Text = _CLAIM_SELECTED[27].ToString();

                if (_CLAIM_SELECTED[25].ToString() != "" && _CLAIM_SELECTED[25].ToString() != null)
                {
                    textBarcodeSwap.Text = _CLAIM_SELECTED[25].ToString();
                    checkChangeProduct.Checked = true;
                    bwBarcodeSwap.RunWorkerAsync();
                }

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

                if (_CLAIM_SELECTED[6].ToString() == "SH")
                {
                    comboBoxEdit1.SelectedIndex = 4;
                    textTrackNo.Show();
                    textTrackNo.Text = _CLAIM_SELECTED[10].ToString();
                    sentDatePicker.Show();
                    //d = DateTime.Parse(_CLAIM_SELECTED[22].ToString());
                    sentDatePicker.Text = _CLAIM_SELECTED[22].ToString();// d.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("en-US"));
                }
                else if (_CLAIM_SELECTED[6].ToString() == "RJ")
                {
                    comboBoxEdit1.SelectedIndex = 1;
                    textRemark.Show();
                    textRemark.Text = _CLAIM_SELECTED[20].ToString();
                }
                else if (_CLAIM_SELECTED[6].ToString() == "RP")
                {
                    comboBoxEdit1.SelectedIndex = 2;
                    receiveDatePicker.Show();
                    receiveDatePicker.Text = _CLAIM_SELECTED[21].ToString();
                }
                else if (_CLAIM_SELECTED[6].ToString() == "WS")
                {
                    comboBoxEdit1.SelectedIndex = 3;
                    receiveDatePicker.Show();
                    receiveDatePicker.Text = _CLAIM_SELECTED[21].ToString();
                    /*sentDatePicker.Show();
                    sentDatePicker.Text = _CLAIM_SELECTED[22].ToString();*/
                }
                else if (_CLAIM_SELECTED[6].ToString() == "AP")
                {
                    comboBoxEdit1.SelectedIndex = 0;
                }
                else
                {
                    receiveDatePicker.Hide();
                    sentDatePicker.Hide();
                    textRemark.Show();
                    textTrackNo.Hide();
                }

                if (comboBoxEdit1.SelectedIndex == 4)
                {
                    textTrackNo.Show();
                    sentDatePicker.Show();

                    receiveDatePicker.Hide();
                    textRemark.Show();
                }
                else if (comboBoxEdit1.SelectedIndex == 1)
                {
                    textRemark.Show();

                    receiveDatePicker.Hide();
                    sentDatePicker.Hide();
                    textTrackNo.Hide();
                }
                else if (comboBoxEdit1.SelectedIndex == 2)
                {
                    receiveDatePicker.Show();

                    sentDatePicker.Hide();
                    textRemark.Show();
                    textTrackNo.Hide();
                }
                else if (comboBoxEdit1.SelectedIndex == 3)
                {
                    receiveDatePicker.Show();

                    sentDatePicker.Hide();
                    textRemark.Show();
                    textTrackNo.Hide();
                }
                else
                {
                    receiveDatePicker.Hide();
                    sentDatePicker.Hide();
                    textRemark.Show();
                    textTrackNo.Hide();
                }
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

        private void textRemark_Leave(object sender, EventArgs e)
        {
            if (textRemark.Text.Length == 0)
            {
                textRemark.Text = "Remark";
            }
        }

        private void textRemark_Enter(object sender, EventArgs e)
        {
            if (textRemark.Text == "Remark")
            {
                textRemark.Text = "";
            }
        }

        private void textBarcodeSwap_Leave(object sender, EventArgs e)
        {
            if (textBarcodeSwap.Text.Length == 0)
            {
                textBarcodeSwap.Text = "Barcode...";
            }
        }

        private void textBarcodeSwap_Enter(object sender, EventArgs e)
        {
            if (textBarcodeSwap.Text == "Barcode...")
            {
                textBarcodeSwap.Text = "";
            }
        }

        private void textBarcodeSwap_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Return))
            {
                lblProductCodeSw.Text = "-";
                lblBarcodeSw.Text = "-";
                lblProductNameSw.Text = "-";
                lblSellDateSw.Text = "-";
                lblLastShopSw.Text = "-";
                lblWarrantySw.Text = "-";
                lblSellPriceSw.Text = "-";
                splashScreenManager.ShowWaitForm();
                bwBarcodeSwap.RunWorkerAsync();
            }
        }

        private void bwBarcodeSwap_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _JSON_BARCODESWAP = JsonConvert.DeserializeObject(Util.ApiProcess("/warranty/info", "shop=" + Param.ApiShopId + "&barcode=" + textBarcodeSwap.Text));

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error is : " + ex);
            }
        }

        private void bwBarcodeSwap_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (_JSON_BARCODESWAP.success.Value)
                {
                    lblProductCodeSw.Text = _JSON_BARCODESWAP.result.sku.ToString();
                    lblBarcodeSw.Text = _JSON_BARCODESWAP.result.barcode.ToString();
                    lblProductNameSw.Text = _JSON_BARCODESWAP.result.productName.ToString();
                    lblSellDateSw.Text = _JSON_BARCODESWAP.result.sellDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    lblLastShopSw.Text = _JSON_BARCODESWAP.result.shop.ToString();
                    lblWarrantySw.Text = _JSON_BARCODESWAP.result.warranty.ToString();
                    lblSellPriceSw.Text = _JSON_BARCODESWAP.result.sellPrice.ToString();

                }
                else
                {
                    lblProductCodeSw.Text = "-";
                    lblBarcodeSw.Text = "-";
                    lblProductNameSw.Text = "-";
                    lblSellDateSw.Text = "-";
                    lblLastShopSw.Text = "-";
                    lblWarrantySw.Text = "-";
                    lblSellPriceSw.Text = "-";
                }
                splashScreenManager.CloseWaitForm();
                _JSON_BARCODESWAP.Clear();
            }
            catch { }
        }

        private void checkChangeProduct_CheckedChanged(object sender, EventArgs e)
        {
            if (checkChangeProduct.Checked)
            {
                lblBarcodeSw.BackColor = Color.Red;
                lblBarcodeSw.Font = new Font(lblBarcodeSw.Font, FontStyle.Bold);
                comboBoxEdit1.SelectedIndex = 3;

            }
            else
            {
                lblBarcodeSw.BackColor = default(Color);
                lblBarcodeSw.Font = new Font(lblBarcodeSw.Font, FontStyle.Regular);
                comboBoxEdit1.Text = "กรุณาเลือกสถานะการเคลม...";
                comboBoxEdit1.EditValue = "กรุณาเลือกสถานะการเคลม...";
            }
        }

        private void textNo_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Return))
            {
                btnQuery_Click(sender, e);
            }
        }

        private void barcode_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Return))
            {
                btnQuery_Click(sender, e);
            }
        }

        private void textFirstname_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Return))
            {
                btnQuery_Click(sender, e);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FmClaimAdd frm = new FmClaimAdd();
            frm.ShowDialog();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            bwLoadImage.RunWorkerAsync();
        }

        private void btnPrintAddr_Click(object sender, EventArgs e)
        {
            if (lblClaimNo.Text.Length > 1)
            {
                Util.GetAddress(lblClaimNo.Text, false);
                btnPrintAddr.Enabled = false;

                PaperSize paperSize = new PaperSize();
                paperSize.RawKind = (int)PaperKind.A4;

                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.PaperSize = paperSize;
                pd.PrintController = new System.Drawing.Printing.StandardPrintController();
                //pd.PrinterSettings.PrinterName = ConfigurationManager.AppSettings["PrinterName"];
                pd.PrinterSettings.PrinterName = Param.DevicePrinter;

                pd.PrintPage += (_, g) =>
                {
                    Util.PrintAddress(g, lblClaimNo.Text);
                };
                pd.Print();


                btnPrintAddr.Enabled = true;
            }
        }

        private void btnPrintClaim_Click(object sender, EventArgs e)
        {
            if (lblClaimNo.Text.Length > 1)
            {
                Util.GetAddress(lblClaimNo.Text, false);
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
                    Util.PrintClaim(g, lblClaimNo.Text);
                };
                pd.Print();


                btnPrintClaim.Enabled = true;
            }
        }

        private void btnPrintReturn_Click(object sender, EventArgs e)
        {
            if (lblClaimNo.Text.Length > 1 && lblProductCodeSw.Text.Length > 1)
            {
                Util.GetAddress(lblClaimNo.Text, true);
                btnPrintReturn.Enabled = false;

                PaperSize paperSize = new PaperSize();
                paperSize.RawKind = (int)PaperKind.A4;

                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.PaperSize = paperSize;
                pd.PrintController = new System.Drawing.Printing.StandardPrintController();
                //pd.PrinterSettings.PrinterName = ConfigurationManager.AppSettings["PrinterName"];
                pd.PrinterSettings.PrinterName = Param.DevicePrinter;

                pd.PrintPage += (_, g) =>
                {
                    Util.PrintReturn(g, lblClaimNo.Text);
                };
                pd.Print();


                btnPrintReturn.Enabled = true;
            }
            else
            {
                MessageBox.Show("ยังไม่ได้ทำการเปลี่ยนสินค้าเคลม");
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
    }
}
