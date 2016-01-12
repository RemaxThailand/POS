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
using DevExpress.XtraGrid.Views.Base;
using System.Threading;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;

namespace PowerPOS
{
    public partial class UcClaim : DevExpress.XtraEditors.XtraUserControl
    {
        DataTable _TABLE_CLAIM;
        string _STREAM_IMAGE_URL;

        public UcClaim()
        {
            InitializeComponent();
        }

        private void UcClaim_Load(object sender, EventArgs e)
        {
            txtBarcode.Focus();
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    int i, a;
            //    DataRow row;
            //    DataTable dt;
            //    lblStatus.Visible = false;

            //    if (e.KeyCode == Keys.Return)
            //    {
            //        string barcode = Util.GetApiData("/claim/barcodeClaim",
            //        string.Format("barcode={0}", txtBarcode.Text));

            //        dynamic jsonBarcode = JsonConvert.DeserializeObject(barcode);
            //        Console.WriteLine(jsonBarcode.success);

            //        if (jsonBarcode.success.Value)
            //        {
            //            if (jsonBarcode.result == "")
            //            {
            //                lblStatus.Text = "สินค้าชิ้นนี้ยังไม่ได้ทำการขาย";
            //                lblStatus.ForeColor = Color.Red;
            //                lblWarranty.Visible = false;
            //                lblStatus.Visible = true;
            //                txtBarcode.Enabled = true;
            //                ptbProduct.Image = null;
            //                btnClaim.Visible = false;
            //            }
            //            else
            //            {
            //                btnClaim.Visible = true;
            //                lblStatus.Visible = false;
            //                txtBarcode.Enabled = true;

            //                claimGridView.OptionsBehavior.AutoPopulateColumns = false;
            //                claimGridControl.MainView = claimGridView;

            //                dt = new DataTable();
            //                for (i = 0; i < ((ColumnView)claimGridControl.MainView).Columns.Count; i++)
            //                {
            //                    dt.Columns.Add(claimGridView.Columns[i].FieldName);
            //                }

            //                if (_TABLE_CLAIM.Rows.Count > 0)
            //                {
            //                    for (a = 0; a < _TABLE_CLAIM.Rows.Count; a++)
            //                    {
            //                        string customer = _TABLE_CLAIM.Rows[a]["firstname"].ToString() + _TABLE_CLAIM.Rows[a]["lastname"].ToString();
            //                        row = dt.NewRow();
            //                        row[0] = (a + 1) * 1;
            //                        row[1] = Convert.ToDateTime(_TABLE_CLAIM.Rows[a]["SellDate"]).ToLocalTime().ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH")); ;
            //                        row[2] = customer;
            //                        row[3] = _TABLE_CLAIM.Rows[a]["Name"].ToString();
            //                        row[4] = Convert.ToInt32(_TABLE_CLAIM.Rows[a]["Price"].ToString()).ToString("#,##0");
            //                        row[5] = "shop";
            //                        dt.Rows.Add(row);
            //                    }

            //                    claimGridControl.DataSource = dt;

            //                    var remain = (DateTime.Now - Convert.ToDateTime(_TABLE_CLAIM.Rows[0]["SellDate"])).TotalDays;

            //                    btnClaim.Visible = remain > 0;

            //                    int day = Convert.ToInt32(remain);
            //                    if (day == 0)
            //                    {
            //                        lblWarranty.Text = "สินค้าชิ้นนี้ขายไปแล้วในวันนี้";
            //                    }
            //                    else
            //                    {
            //                        lblWarranty.Text = "สินค้าชิ้นนี้" + ((day > 0) ? " ขายไปแล้ว " + day.ToString("#,###") + " วัน" : " ขายไปแล้ว " + (day * -1).ToString("#,###") + " วัน");
            //                    }
            //                    lblWarranty.Visible = true;


            //                    ptbProduct.Image = null;


            //                    var filename = @"Resource/Images/Product/" + _TABLE_CLAIM.Rows[0]["product"].ToString() + ".jpg";
            //                    dt = Util.DBQuery(string.Format("SELECT Image FROM Product WHERE product = '{0}'", _TABLE_CLAIM.Rows[0]["product"].ToString()));

            //                    _STREAM_IMAGE_URL = "http://src.powerdd.com/img/product/" + "88888888" + "/" + _TABLE_CLAIM.Rows[0]["sku"].ToString() + "/" + dt.Rows[0]["Image"].ToString().Split(',')[0];

            //                    if (!File.Exists(filename))
            //                    {
            //                        if (dt.Rows.Count > 0 && dt.Rows[0]["Image"].ToString() != "")
            //                        {
            //                            DownloadImage(_STREAM_IMAGE_URL, @"Resource/Images/Product/", Param.ProductId + ".jpg");
            //                        }
            //                    }
            //                    else
            //                    {
            //                        try { ptbProduct.Image = Image.FromFile(filename); }
            //                        catch
            //                        {
            //                            if (dt.Rows.Count > 0 && dt.Rows[0]["Image"].ToString() != "")
            //                            {
            //                                DownloadImage(_STREAM_IMAGE_URL, @"Resource/Images/Product/", Param.ProductId + ".jpg");
            //                            }
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    claimGridControl.DataSource = null;
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}
        }

        private void DownloadImage(string url, string savePath, string fileName)
        {
            ptbProduct.ImageLocation = url;
            DownloadImage d = new DownloadImage();
            Thread thread = new Thread(() => d.Download(url, savePath, fileName));
            thread.Start();
        }
    }
}
