﻿using DevExpress.XtraBars.Navigation;
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
        enum Screen { Sale, ReceiveProduct, Product, Customer, User, Brand, Category, Color, Report, ShopInfo, Config, Claim, Return, Stock, ReportStock, Credit, claimShop, claimReceived};
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
        UcReportStock _UC_REPORT_STOCK;
        UcCredit _UC_CREDIT;
        UcReportProduct _UC_REPORT_PORDUCT;
        UcAddDataClaim _UC_DATA_CLAIM;
        UcClaimShop _UC_CLAIM_SHOP;
        UcClaimReceived _UC_CLAIM_RECEIVED;

        #endregion

        public Main()
        {
            InitializeComponent();
            Param.UserId = "0000";
            Param.UserCode = "1234";

            Param.EmployeeId = null;
            Param.EmployeeType = null;

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
                //MessageBox.Show("Shop" + args[2].ToString());
                if (args[1].ToString() == "RemaxThailand")
                {
                    Param.ShopId = args[2].ToString();
                    Param.Token = args[3].ToString();
                    Param.DatabaseName = args[4].ToString();
                    Param.DatabasePassword = args[5].ToString();
                    Param.LicenseKey = args[6].ToString();
                    Param.ApiShopId = args[7].ToString();
                    Param.InitialFinished = false;
                    Param.Main = this;
                    InitialCloudData();
                    InitialEmployeeScreen();
                }
                else
                {
                    Process process = new Process();
                    process.StartInfo.FileName = "Updater.exe";
                    process.Start();
                    this.Dispose();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "มีข้อผิดพลาดเกิดขึ้น", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Param.ShopId = "00000000";
                Util.WriteErrorLog(ex.Message);
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

        private void InitialEmployeeScreen()
        {
            var dt = Util.DBQuery(string.Format(@"
                SELECT s.id, s.parent, 
                    CASE WHEN s.active = 'True' THEN 1 ELSE 0 END active,
	                CASE WHEN m.screen IS NULL THEN 0 ELSE 1 END canView
                FROM SystemScreen s
	                LEFT JOIN EmployeeScreenMapping m
		                ON s.system = m.system
		                AND s.id = m.screen
		                AND m.employeeType = '{0}'
		                AND m.permission = 'V0'
                WHERE s.system = 'POS'
            ", Param.EmployeeType));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string parent = dt.Rows[i]["parent"].ToString();
                string screen = dt.Rows[i]["id"].ToString();
                bool hasParent = parent != "";
                bool canView = dt.Rows[i]["canView"].ToString() == "1" && dt.Rows[i]["active"].ToString() == "1";

                if (!canView)
                {
                    foreach (TileNavItem item in this.tileNavPane1.DefaultCategory.Items)
                    {
                        if (hasParent)
                        {
                            if (item.Name == parent)
                            {
                                foreach (TileNavSubItem subitem in item.SubItems)
                                {
                                    if (subitem.Name == screen)
                                    {
                                        Console.WriteLine("remove item {0}", screen);
                                        item.SubItems.Remove(subitem);
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        else if (item.Name == screen)
                        {
                            Console.WriteLine("remove item {0}", screen);
                            this.tileNavPane1.DefaultCategory.Items.Remove(item);
                            break;
                        }
                    }
                }
            }
        }

        private void InitialCloudData()
        {
            this.Opacity = 0;
            this.ShowInTaskbar = false;
            Param.MainPanel = pnlMain;

            bool exit = false;

            FmInitialData fm = new FmInitialData();
            var result = fm.ShowDialog(this);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                if (Param.EmployeeId == null)
                {
                    FmLogin fmLogin = new FmLogin();
                    bool success = false;
                    while (!success && !exit)
                    {
                        if (fmLogin.ShowDialog(this) == DialogResult.OK)
                        {
                            success = true;
                        }
                        else
                        {
                            exit = true;
                        }
                    }
                }

                if (!exit)
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
                
                //this.Opacity = 0;
                //this.ShowInTaskbar = false;

                //bool exit = false;
                //FmLogin fmLogin = new FmLogin();
                //bool success = false;
                //while (!success && !exit)
                //{
                //    if (fmLogin.ShowDialog(this) == DialogResult.OK)
                //    {
                //        success = true;
                //    }
                //    else
                //    {
                //        exit = true;
                //    }
                //}

                //if (!exit)
                //{
                //    this.Opacity = 100;
                //    this.ShowInTaskbar = true;
                //    AddPanel(Param.Screen.Sale);
                //    Param.SelectedScreen = (int)Screen.Sale;
                //}
                //else
                //{
                //    this.Dispose();
                //}
                this.Dispose();
            }
        }

        private void tileNavPane1_TileClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            Util.LoadScreenPermissionDetail(e.Element.Name);

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
            else if (e.Element.Name == "navClaimData")
            {
                AddPanel(Param.Screen.Claim);
            }
            else if (e.Element.Name == "navReportStock")
            {
                AddPanel(Param.Screen.ReportStock);
            }
            else if (e.Element.Name == "navCredit")
            {
                AddPanel(Param.Screen.Credit);
            }
            else if (e.Element.Name == "navRefresh")
            {
                //InitializeComponent();
                //Param.UserId = "0000";
                //Param.UserCode = "1234";

                //Param.EmployeeId = null;
                //Param.EmployeeType = null;

                //Util.ConnectSQLiteDatabase();
                //Util.GetDiviceId();
                //this.Opacity = 0;
                //this.ShowInTaskbar = false;

                //Main_Load(sender,(e));
                InitialCloudData();
            }
            else if (e.Element.Name == "navReportProduct")
            {
                AddPanel(Param.Screen.ReportProduct);
            }
            else if (e.Element.Name == "navReport")
            {
                AddPanel(Param.Screen.Report);
            }
            else if (e.Element.Name == "navClaimDataShop")
            {
                if (Param.ShopId == "66666666")
                {
                    AddPanel(Param.Screen.claimShop);
                }
            }
            else if (e.Element.Name == "navClaimReceivedShop")
            {
                if (Param.ShopId == "66666666")
                {
                    AddPanel(Param.Screen.claimReceived);
                }
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
                    if (Param.shopClaim == true)
                    {
                        if (_UC_CLAIM == null) _UC_CLAIM = new UcClaim();
                        _USER_CONTROL = _UC_CLAIM;
                    }
                    else
                    {
                        if (_UC_DATA_CLAIM == null) _UC_DATA_CLAIM = new UcAddDataClaim();
                        _USER_CONTROL = _UC_DATA_CLAIM;
                    }
                    break;
                //case Param.Screen.Claim:
                //    if (_UC_DATA_CLAIM == null) _UC_DATA_CLAIM = new UcAddDataClaim();
                //    _USER_CONTROL = _UC_DATA_CLAIM;
                //    break;
                case Param.Screen.ReportStock:
                    if (_UC_REPORT_STOCK == null) _UC_REPORT_STOCK = new UcReportStock();
                    _USER_CONTROL = _UC_REPORT_STOCK;
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
                case Param.Screen.claimShop:
                    if (_UC_CLAIM_SHOP == null) _UC_CLAIM_SHOP = new UcClaimShop();
                    _USER_CONTROL = _UC_CLAIM_SHOP;
                    _UC_CLAIM_SHOP.LoadData();
                    break;
                case Param.Screen.claimReceived:
                    if (_UC_CLAIM_RECEIVED == null) _UC_CLAIM_RECEIVED = new UcClaimReceived();
                    _USER_CONTROL = _UC_CLAIM_RECEIVED;
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
