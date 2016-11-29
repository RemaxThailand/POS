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
using System.Net;

namespace PowerPOS
{
    public partial class FmClaimImg : DevExpress.XtraEditors.XtraForm
    {

        public string imgUrl;


        public FmClaimImg()
        {
            InitializeComponent();
        }

        private void FmClaimImg_Load(object sender, EventArgs e)
        {
            bwLoadImage.RunWorkerAsync();
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

        private async void bwLoadImage_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                pictureEdit1.Image = await DownloadImage(imgUrl);
                
            }
            catch
            {

            }
        }
    }
}