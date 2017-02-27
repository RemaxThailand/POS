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
using Newtonsoft.Json;

namespace PowerPOS
{
    public partial class FmEditAddress : DevExpress.XtraEditors.XtraForm
    {
        DataTable _TABLE_ADDRESS;
        dynamic _JSON_ADDRESS;
        public string _claimNo, province, district;
        dynamic _JSON_PROVINCE;
        dynamic _JSON_DISTRICT;

        public FmEditAddress()
        {
            InitializeComponent();
        }

        private void FmEditAddress_Load(object sender, EventArgs e)
        {
            LoadData(sender, e);
            bwLoadProvince.RunWorkerAsync();

        }

        public void LoadData(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            DataTable dtProvince = Util.DBQuery(string.Format(@"SELECT id, name FROM Province ORDER BY Name"));
            cbbProvince.Items.Clear();
            cbbProvince.DataSource = dtProvince;
            cbbProvince.DisplayMember = dtProvince.Columns["name"].ToString();
            cbbProvince.ValueMember = dtProvince.Columns["id"].ToString();

            DataTable dtDistrict = Util.DBQuery(string.Format(@"SELECT * FROM District ORDER BY province, id"));
            cbbDistrict.DataSource = dtDistrict;
            cbbDistrict.DisplayMember = dtDistrict.Columns["name"].ToString();
            cbbDistrict.ValueMember = dtDistrict.Columns["id"].ToString();


            string _shop = Param.ApiShopId;

            _TABLE_ADDRESS = new DataTable();
            _JSON_ADDRESS = JsonConvert.DeserializeObject(Util.ApiProcess("/claim/info", "shop=&id=" + _claimNo + "&barcode=&claimdate_from=&claimdate_to=&status=&firstname=&lineid=&tel="));

            if (_JSON_ADDRESS.success.Value)
            {
                for (int a = 0; a < _JSON_ADDRESS.result[0].Count; a++)
                {
                    txtName.Text = _JSON_ADDRESS.result[0][a].firstname.ToString();
                    txtLastname.Text = _JSON_ADDRESS.result[0][a].lastname.ToString();
                    txtNickname.Text = _JSON_ADDRESS.result[0][a].nickname.ToString();
                    txtAddress.Text = _JSON_ADDRESS.result[0][a].address.ToString();
                    txtAddress2.Text = _JSON_ADDRESS.result[0][a].address2.ToString();
                    txtSubDistrict.Text = _JSON_ADDRESS.result[0][a].subDistrict.ToString();
                    //row[16] = _JSON_ADDRESS.result[0][a].district.ToString();
                    //row[17] = _JSON_ADDRESS.result[0][a].province.ToString();
                    txtZipCode.Text = _JSON_ADDRESS.result[0][a].zipcode.ToString();
                    txtMobile.Text = _JSON_ADDRESS.result[0][a].tel.ToString();
                    txtEmail.Text = _JSON_ADDRESS.result[0][a].email.ToString();
                    cbbProvince.Text = _JSON_ADDRESS.result[0][a].province.ToString();
                    comboProvince.Text = _JSON_ADDRESS.result[0][a].province.ToString();
                    //cbbProvince_SelectedIndexChanged(sender, e);
                    cbbDistrict.Text = _JSON_ADDRESS.result[0][a].district.ToString();
                    //cbbDistrict_SelectedIndexChanged(sender, e);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณต้องการบันทึกข้อมูล ใช่หรือไม่", "ยืนยันการบันทึกข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataTable dt;
                int i = 0;

                try
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                    i = 0;
                    //for (i = 0; i < dt.Rows.Count; i++)
                    //{
                   
                    string value = txtName.Text + "," + txtLastname.Text + "," + txtNickname.Text + "," + txtMobile.Text + "," + txtEmail.Text + "," + txtAddress.Text + "," + txtAddress2.Text + "," + txtSubDistrict.Text + "," + cbbDistrict.Text + "," + cbbProvince.Text + "," + txtZipCode.Text;

                    dynamic json = JsonConvert.DeserializeObject(Util.GetApiData("/claim/update",
                    string.Format("shop={0}&id={1}&column={2}&value={3}", Param.ApiShopId, _claimNo, "firstname,lastname,nickname,tel,email,address,address2,subDistrict,district,province,zipcode", value)
                    ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                MessageBox.Show("บันทีกข้อมูลที่อยู่ลูกค้าเรียบร้อยแล้ว");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
                Console.WriteLine("Error is : " + ex);

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
                //MessageBox.Show("Error is : " + ex);
                Console.WriteLine("Error is : " + ex);

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
                Console.WriteLine("Error is : " + ex);

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
                    txtZipCode.Text = _JSON_DISTRICT.result[comboDistrict.SelectedIndex].zipcode.ToString();
                    //splashScreenManager.CloseWaitForm();
                    //FIRSTLOAD = false;

                }


                else
                {
                    MessageBox.Show("กรุณาทำการปิดและเปิดหน้าต่างนี้ไหมอีกครั้ง");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error is : " + ex);
                Console.WriteLine("Error is : " + ex);
            }
        }

        private void comboProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (!_FIRSTLOAD)
            //{
                comboDistrict.Properties.Items.Clear();
                //splashScreenManager.ShowWaitForm();
                bwLoadDistrict.RunWorkerAsync();
            //}
        }

        private void comboDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (!_FIRSTLOAD)
            //{
                txtZipCode.Text = _JSON_DISTRICT.result[comboDistrict.SelectedIndex].zipcode.ToString();
            //}
        }

        private void cbbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtDistrict = Util.DBQuery(string.Format(@"SELECT * FROM District WHERE province = '{0}' ORDER BY province, id", cbbProvince.SelectedValue));
                cbbDistrict.DataSource = dtDistrict;
                cbbDistrict.DisplayMember = dtDistrict.Columns["name"].ToString();
                cbbDistrict.ValueMember = dtDistrict.Columns["id"].ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void cbbDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtDistrict = Util.DBQuery(string.Format(@"SELECT zipCode FROM District WHERE id = '{0}' AND province = '{1}'", cbbDistrict.SelectedValue, cbbProvince.SelectedValue));
                txtZipCode.Text = dtDistrict.Rows[0]["zipCode"].ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}