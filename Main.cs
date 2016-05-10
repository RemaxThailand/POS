using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerPOS
{
    public partial class Main : Form
    {
        #region Parameter
        enum Screen { Sale, ReceiveProduct, Product, Customer, User, Brand, Category, Color, Report, ShopInfo, Config, Claim, Return, Stock, Statistic, Credit};
        XtraUserControl _USER_CONTROL;
        UcSale _UC_SALE;
        UcStock _UC_STOCK;
        UcProduct _UC_PRODUCT;
        UcCustomer _UC_CUSTOMER;
        UcReceiveProduct _UC_RECEIVE_PRODUCT;
        UcReport _UC_REPORT;
        UcConfig _UC_CONFIG;
        UcClaim _UC_CLAIM;
        UcStatistic _UC_STATISTIC;
        UcCredit _UC_CREDIT;
        UcReportProduct _UC_REPORT_PORDUCT;
        #endregion

        public Main()
        {
            InitializeComponent();
            Param.UserId = "0000";
            Param.UserCode = "1234";
            Util.ConnectSQLiteDatabase();
            Util.GetDiviceId();
            this.Opacity = 0;
            this.ShowInTaskbar = false;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            try
            {
                if(args[1].ToString() == "RemaxThailand")
                {
                    Param.ShopId = args[2].ToString();
                    Param.Token = args[3].ToString();
                    Param.DatabaseName = args[4].ToString();
                    Param.DatabasePassword = args[5].ToString();
                    Param.InitialFinished = false;
                    InitialCloudData();
                    Param.Main = this;
                }
                else
                {
                    Process process = new Process();
                    process.StartInfo.FileName = "Updater.exe";
                    process.Start();
                    this.Dispose();
                }
            }
            catch
            {
                Process process = new Process();
                process.StartInfo.FileName = "Updater.exe";
                process.Start();
                this.Dispose();
            }

            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "DevExpress Dark Style";

            ////Product
            //string data = Util.GetApiData("/product/pos", 
            //    string.Format("shop={0}", Param.ShopId));

            //dynamic json = JsonConvert.DeserializeObject(data);
            //Console.WriteLine(json.success);

            //for (int i = 0; i < json.result.Count; i++)
            //{
            //    Console.WriteLine("Product  " + json.result[i].name);
            //}

            ////Category
            //string category = Util.GetApiData("/category/info",
            //  string.Format("shop={0}", Param.ShopId));

            //dynamic jsonCategory = JsonConvert.DeserializeObject(category);
            //Console.WriteLine(jsonCategory.success);

            //for (int i = 0; i < jsonCategory.result.Count; i++)
            //{
            //    Console.WriteLine("Category  " + jsonCategory.result[i].name);
            //}

            ////Barnd
            //string brand = Util.GetApiData("/brand/info",
            //  string.Format("shop={0}", Param.ShopId));

            //dynamic jsonBarnd = JsonConvert.DeserializeObject(brand);
            //Console.WriteLine(jsonBarnd.success);

            //for (int i = 0; i < jsonBarnd.result.Count; i++)
            //{
            //    Console.WriteLine("Barnd  " + jsonBarnd.result[i].name);
            //}

            ////Barcode
            //string barcode = Util.GetApiData("/product/barcodePos",
            //  string.Format("shop=5BB7C6B3-F6D0-4926-B14F-C580DD148612"));

            //dynamic jsonBarcode = JsonConvert.DeserializeObject(barcode);
            //Console.WriteLine(jsonBarcode.success);

            //for (int i = 0; i < jsonBarcode.result.Count; i++)
            //{
            //    Console.WriteLine("Barcode[2]  " + jsonBarcode.result[i].barcode);
            //}


            //Claim
            //string claim = Util.GetApiData("/claim/info",
            //  string.Format("shop={0}&id=''&barcode=''", Param.ShopId));

            //dynamic jsonClaim = JsonConvert.DeserializeObject(claim);
            //Console.WriteLine(jsonClaim.success);

            //for (int i = 0; i < jsonClaim.result.Count; i++)
            //{
            //    Console.WriteLine("Claim  " + jsonClaim.result[i].claimNo);
            //}
            //AddPanel(Screen.Sale);
        }

        private void InitialCloudData()
        {
            this.Opacity = 0;
            this.ShowInTaskbar = false;
            Param.MainPanel = pnlMain;

            FmInitialData fm = new FmInitialData();
            var result = fm.ShowDialog(this);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.Text = string.Format("Power POS - ร้าน {0} ({1})", Param.ShopName, Param.ComputerName);
                this.Opacity = 100;
                this.ShowInTaskbar = true;
                Param.InitialFinished = true;

                AddPanel(Param.Screen.Sale);
                Param.SelectedScreen = (int)Screen.Sale;
            }
            else
            {
                this.Dispose();
            }
        }

        #region Menu Click Event
        private void navExit_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            if (MessageBox.Show("คุณต้องการ ออกจากระบบใช่หรือไม่", "ยืนยันการออกจากระบบ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Dispose();
            }
        }

        private void tileNavPane1_TileClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            if (e.Element.Name == "navSale")
            {
                AddPanel(Param.Screen.Sale);
            }
            else if (e.Element.Name == "navStock")
            {
                AddPanel(Param.Screen.Stock);
            }
            else if (e.Element.Name == "navProduct")
            {
                AddPanel(Param.Screen.Product);
            }
            else if (e.Element.Name == "navCustomerData")
            {
                AddPanel(Param.Screen.Customer);
            }
            else if (e.Element.Name == "navReceived")
            {
                AddPanel(Param.Screen.ReceiveProduct);
            }
            else if (e.Element.Name == "navReportDaily")
            {
                AddPanel(Param.Screen.Report);
            }
            else if (e.Element.Name == "navConfig")
            {
                AddPanel(Param.Screen.Config);
            }
            else if (e.Element.Name == "navClaim")
            {
                AddPanel(Param.Screen.Claim);
            }
            else if (e.Element.Name == "navStatistic")
            {
                AddPanel(Param.Screen.Statistic);
            }
            else if (e.Element.Name == "navCredit")
            {
                AddPanel(Param.Screen.Credit);
            }
            else if (e.Element.Name == "navRefresh")
            {
                InitialCloudData();
            }
            else if (e.Element.Name == "navReportProduct")
            {
                AddPanel(Param.Screen.ReportProduct);
            }
            
        }
        #endregion


        #region Internal Method
        public void AddPanel(Param.Screen screen)
        {
            switch (screen)
            {
                case Param.Screen.Sale:
                    if (_UC_SALE == null) _UC_SALE = new UcSale();
                    _USER_CONTROL = _UC_SALE;
                    _UC_SALE.LoadData();
                    break;
                case Param.Screen.ReceiveProduct:
                    if (_UC_RECEIVE_PRODUCT == null) _UC_RECEIVE_PRODUCT = new UcReceiveProduct();
                    _USER_CONTROL = _UC_RECEIVE_PRODUCT;
                    _UC_RECEIVE_PRODUCT._FIRST_LOAD = true;
                    _UC_RECEIVE_PRODUCT.LoadData();
                    _UC_RECEIVE_PRODUCT.SearchData();
                    break;
                case Param.Screen.Stock:
                    if (_UC_STOCK == null) _UC_STOCK = new UcStock();
                    _USER_CONTROL = _UC_STOCK;
                    _UC_STOCK.LoadData();
                    break;
                case Param.Screen.Product:
                    if (_UC_PRODUCT == null) _UC_PRODUCT = new UcProduct();
                    _USER_CONTROL = _UC_PRODUCT;
                    _UC_PRODUCT._FIRST_LOAD = true;
                    _UC_PRODUCT.LoadData();
                    break;
                case Param.Screen.Customer:
                    if (_UC_CUSTOMER == null) _UC_CUSTOMER = new UcCustomer();
                    _USER_CONTROL = _UC_CUSTOMER;
                    _UC_CUSTOMER.LoadData();
                    break;
                case Param.Screen.Report:
                    if (_UC_REPORT == null) _UC_REPORT = new UcReport();
                    _USER_CONTROL = _UC_REPORT;
                    _UC_REPORT.LoadData();
                    break;
                case Param.Screen.Config:
                    if (_UC_CONFIG == null) _UC_CONFIG = new UcConfig();
                    _USER_CONTROL = _UC_CONFIG;
                    _UC_CONFIG.LoadData();
                    break;
                case Param.Screen.Claim:
                    if (_UC_CLAIM == null) _UC_CLAIM = new UcClaim();
                    _USER_CONTROL = _UC_CLAIM;
                    break;
                case Param.Screen.Statistic:
                    if (_UC_STATISTIC == null) _UC_STATISTIC = new UcStatistic();
                    _USER_CONTROL = _UC_STATISTIC;
                    break;
                case Param.Screen.Credit:
                    if (_UC_CREDIT == null) _UC_CREDIT = new UcCredit();
                    _USER_CONTROL = _UC_CREDIT;
                    _UC_CREDIT.LoadData();
                    break;
                case Param.Screen.ReportProduct:
                    if (_UC_REPORT_PORDUCT == null) _UC_REPORT_PORDUCT = new UcReportProduct();
                    _USER_CONTROL = _UC_REPORT_PORDUCT;
                    _UC_REPORT_PORDUCT.LoadData();
                    break;
            }
            if (!pnlMain.Contains(_USER_CONTROL))
            {
                pnlMain.Controls.Clear();
                _USER_CONTROL.Dock = DockStyle.Fill;
                pnlMain.Controls.Add(_USER_CONTROL);
            }
        }
        #endregion

        private void tmSync_Tick(object sender, EventArgs e)
        {
            if (!bwSync.IsBusy && Param.InitialFinished)
            {
                bwSync.RunWorkerAsync();
                statusProgressPanel.Visible = true;
                statusProgressPanel.Caption = "กำลัง Sync ข้อมูลเข้าระบบ Cloud";
                //lblStatus.Visible = true;
                //lblStatus.Text = "กำลัง Sync ข้อมูลเข้าระบบ Cloud";
            }
        }

        private void bwSync_DoWork(object sender, DoWorkEventArgs e)
        {
            Util.SyncData();
        }

        private void bwSync_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            statusProgressPanel.Visible = false;
            statusProgressPanel.Caption = "";

            //lblStatus.Visible = false;
            //lblStatus.Text = "";
        }
    }
}
