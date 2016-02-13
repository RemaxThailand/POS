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
    public partial class FmLicense : DevExpress.XtraEditors.XtraForm
    {
        public FmLicense()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Param.LicenseKey = txtLicenseKey.Text.Trim();
            Properties.Settings.Default.LicenseKey = Param.LicenseKey;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();

            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            txtLicenseKey.Enabled = false;
            dynamic jsonObject = Util.LoadAppConfig();
            if (!Param.jsonObject)
            {
                MessageBox.Show("ข้อมูลไม่ถูกต้อง กรุณาตรวจสอบ License ของคุณอีกครั้ง", "แจ้งเตือน" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                txtLicenseKey.Enabled = true;
                lblDeviceID.Visible = true;

            }
            else
            {
                lblDeviceID.Visible = false;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void FmLicense_Load(object sender, EventArgs e)
        {
            txtLicenseKey.Text = Properties.Settings.Default.LicenseKey;
            lblDeviceID.Text = Param.DeviceID;
            Util.GetApiData("/shop-application/license/request", string.Format("deviceId={0}&deviceName={1}", Param.DeviceID, Param.ComputerName));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}