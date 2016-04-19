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
using Newtonsoft.Json;

namespace PowerPOS
{
    public partial class UcConfig : DevExpress.XtraEditors.XtraUserControl
    {

        bool _FIRST_LOAD = true;

        public UcConfig()
        {
            InitializeComponent();
        }

        private void GetPrinter()
        {
            var index = 0;
            var i = 0;
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cbxPrinter.Properties.Items.Add(printer);
                if (Param.DevicePrinter == printer) index = i;
                i++;
            }
            cbxPrinter.SelectedIndex = index;
        }

        private void UcConfig_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            //if (Param.SystemConfig.Bill == null)
            //{
            //    var json = JsonConvert.SerializeObject(Param.SystemConfig);
            //    json = json.Substring(0, json.Length - 1) + ", \"Bill\":{\"PrintCount\":2,\"PrintType\":\"Y\",\"PrintLogo\":\"Y\",\"HeaderName\":\"ใบส่งของ\",\"FooterText\":\"Line Official ID : @RemaxThailand\",\"Logo\":\"" + Param.LogoUrl + "\"}";
            //    Param.SystemConfig = JsonConvert.DeserializeObject(json.ToString());
            //    bwUpdateConfig.RunWorkerAsync();
            //    //var json = "{\"Bill\":{\"PrintCount\":1,\"PrintType\":\"Y\",\"PrintLogo\":\"Y\",\"HeaderName\":\"ใบส่งของ\",\"Line Official ID : @RemaxThailand\":\"H\",\"Printer\":\"\"}";

            //    //Param.systemConfig = Tuple.Create(Param.systemConfig, Bill);
            //    //Param.systemConfig = Tuple.Create(Param.systemConfig.Item1, Param.systemConfig.Item2);

            //}

            nudPrintCount.Value = Convert.ToDecimal(Param.PrintCount);
            rdbPrint.Checked = "" + Param.PrintType == "Y";
            rdbNotPrint.Checked = "" + Param.PrintType == "N";
            rdbAlert.Checked = "" + Param.PrintType == "A";
            rdbLogoPrint.Checked = "" + Param.PrintLogo == "Y";
            rdbLogoNotPrint.Checked = "" + Param.PrintLogo == "N";
            rdbSize76.Checked = "" + Param.PaperSize == "76";
            rdbSize80.Checked = "" + Param.PaperSize == "80";
            txtBillHeader.Text = "" + Param.HeaderName;
            txtBillFooter.Text = "" + Param.FooterText;

            //ptbLogo.ImageLocation = File.Exists(Param.LogoPath) ? Param.LogoPath : Param.Logo;

            if (cbxPrinter.Text == "")
            {
                GetPrinter();
            }
            lblShopName.Text = Param.ShopName;
            lblDeviceName.Text = Param.ComputerName;
            _FIRST_LOAD = false;

            CheckLogoUrl();

            if (Param.MemberType == "Shop")
            {
                groupControl2.Enabled = false;
                groupControl3.Enabled = false;
            }
            //GetPrinter();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSaveBill.Enabled = false;
            gbxBill.Enabled = false;

            if (bwUpdateConfig.IsBusy)
            {
                bwUpdateConfig.CancelAsync();
            }
            while (bwUpdateConfig.IsBusy)
            {
            }

            Param.DevicePrinter = cbxPrinter.SelectedItem.ToString();
            bwUpdateConfig.RunWorkerAsync();
        }

        private void bwUpdateConfig_DoWork(object sender, DoWorkEventArgs e)
        {
            string values = Param.DevicePrinter + "," + Param.PrintType +"," + Param.PrintLogo + "," + Param.Logo + "," + Param.HeaderName + "," + Param.FooterText +  "," + Param.PrintCount + "," + Param.PaperSize;
            string app = Util.GetApiData("/shop-application/updatePos",
               string.Format("shop={0}&licenseKey={1}&column={2}&value={3}", Param.ApiShopId, Param.LicenseKey, "devicePrinter,printType,printLogo,logo,headerName,footerText,printCount,paperSize", values));

            dynamic jsonApp = JsonConvert.DeserializeObject(app);
            Console.WriteLine(jsonApp.success);

            Properties.Settings.Default.DevicePrinter = Param.DevicePrinter;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }

        private void bwUpdateConfig_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnSaveBill.Enabled = false;
            gbxBill.Enabled = true;
        }

        private void bwUploadLogo_DoWork(object sender, DoWorkEventArgs e)
        {
            //CloudBlobClient blobClient = Param.AzureStorageAccount.CreateCloudBlobClient();
            //CloudBlobContainer container = blobClient.GetContainerReference("images");
            //try
            //{
            //    CloudBlockBlob blockBlob = container.GetBlockBlobReference("POS/Logo/" + Param.ShopId + Param.LogoPath.Replace("Resource/Images", ""));
            //    using (var fileStream = File.OpenRead(openFileDialog1.FileName))
            //    {
            //        blockBlob.UploadFromStream(fileStream);
            //        Param.SystemConfig.Bill.Logo = _LOGO_URL;
            //        btnSaveBill_Click(sender, e);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error upload image {0}", ex.Message);
            //}
        }

        private void bwUploadLogo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            gbxBill.Enabled = true;
            CheckLogoUrl();
        }

        private void bwDownloadLogo_DoWork(object sender, DoWorkEventArgs e)
        {
            //if (!Directory.Exists("Resource/Images")) Directory.CreateDirectory("Resource/Images");
            //if (File.Exists(Param.LogoPath)) File.Delete(Param.LogoPath);
            //using (var client = new WebClient())
            //{
            //    client.DownloadFileAsync(new Uri(Param.LogoUrl), Param.LogoPath);
            //    Param.SystemConfig.Bill.Logo = Param.LogoUrl;
            //    btnSaveBill_Click(sender, e);
            //}
        }

        private void bwDownloadLogo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            gbxBill.Enabled = true;
            CheckLogoUrl();
        }

        private void CheckLogoUrl()
        {
            //mniResetLogo.Visible = Param.SystemConfig.Bill.Logo != Param.LogoUrl;
        }

        private void rdbAlert_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbAlert.Checked && !_FIRST_LOAD)
            {
                Param.PrintType = "A";
                btnSaveBill.Enabled = true;
            }
        }

        private void rdbPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbPrint.Checked && !_FIRST_LOAD)
            {
                Param.PrintType = "Y";
                btnSaveBill.Enabled = true;
            }
        }

        private void rdbNotPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbNotPrint.Checked && !_FIRST_LOAD)
            {
                Param.PrintType = "N";
                btnSaveBill.Enabled = true;
            }
        }

        private void nudPrintCount_ValueChanged(object sender, EventArgs e)
        {
            if (!_FIRST_LOAD)
            {
                Param.PrintCount = nudPrintCount.Value.ToString();
                btnSaveBill.Enabled = true;
            }
        }

        private void rdbLogoPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbLogoPrint.Checked && !_FIRST_LOAD)
            {
                Param.PrintLogo = "Y";
                btnSaveBill.Enabled = true;
            }
        }

        private void rdbLogoNotPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbLogoNotPrint.Checked && !_FIRST_LOAD)
            {
                Param.PrintLogo = "N";
                btnSaveBill.Enabled = true;
            }
        }

        private void txtBillHeader_TextChanged(object sender, EventArgs e)
        {
            if (!_FIRST_LOAD)
            {
                Param.HeaderName = txtBillHeader.Text;
                btnSaveBill.Enabled = true;
            }
        }

        private void txtBillFooter_TextChanged(object sender, EventArgs e)
        {
            if (!_FIRST_LOAD)
            {
                Param.FooterText = txtBillFooter.Text;
                btnSaveBill.Enabled = true;
            }
        }

        private void cbxPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_FIRST_LOAD)
            {
                Param.DevicePrinter = cbxPrinter.SelectedItem.ToString();
                btnSaveBill.Enabled = true;
            }
        }

        private void lblVersion_MouseClick(object sender, MouseEventArgs e)
        {
            FmPosDetail frm = new FmPosDetail();
            var result = frm.ShowDialog(this);
        }

        private void lblVersion_Click(object sender, EventArgs e)
        {
            FmPosDetail frm = new FmPosDetail();
            var result = frm.ShowDialog(this);
        }

        private void rdbSize76_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbSize76.Checked && !_FIRST_LOAD)
            {
                Param.PaperSize = "76";
                btnSaveBill.Enabled = true;
            }
        }

        private void rdbSize80_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbSize80.Checked && !_FIRST_LOAD)
            {
                Param.PaperSize = "80";
                btnSaveBill.Enabled = true;
            }
        }
    }
}
