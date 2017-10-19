namespace PowerPOS
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.TileItemElement tileItemElement88 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement90 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement77 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement73 = new DevExpress.XtraEditors.TileItemElement();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            DevExpress.XtraEditors.TileItemElement tileItemElement74 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement75 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement76 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement78 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement79 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement80 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement83 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement81 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement82 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement87 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement84 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement85 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement86 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement89 = new DevExpress.XtraEditors.TileItemElement();
            this.tileNavPane1 = new DevExpress.XtraBars.Navigation.TileNavPane();
            this.navCredit = new DevExpress.XtraBars.Navigation.TileNavItem();
            this.navRefresh = new DevExpress.XtraBars.Navigation.TileNavItem();
            this.navButton2 = new DevExpress.XtraBars.Navigation.NavButton();
            this.navExit = new DevExpress.XtraBars.Navigation.NavButton();
            this.navData = new DevExpress.XtraBars.Navigation.TileNavItem();
            this.navSale = new DevExpress.XtraBars.Navigation.TileNavSubItem();
            this.navReceived = new DevExpress.XtraBars.Navigation.TileNavSubItem();
            this.navProduct = new DevExpress.XtraBars.Navigation.TileNavSubItem();
            this.navStock = new DevExpress.XtraBars.Navigation.TileNavSubItem();
            this.navCustomer = new DevExpress.XtraBars.Navigation.TileNavItem();
            this.navCustomerData = new DevExpress.XtraBars.Navigation.TileNavSubItem();
            this.navClaim = new DevExpress.XtraBars.Navigation.TileNavItem();
            this.navClaimData = new DevExpress.XtraBars.Navigation.TileNavSubItem();
            this.navClaimReceivedShop = new DevExpress.XtraBars.Navigation.TileNavSubItem();
            this.navClaimDataShop = new DevExpress.XtraBars.Navigation.TileNavSubItem();
            this.navReport = new DevExpress.XtraBars.Navigation.TileNavItem();
            this.navReportDaily = new DevExpress.XtraBars.Navigation.TileNavSubItem();
            this.navReportProduct = new DevExpress.XtraBars.Navigation.TileNavSubItem();
            this.navReportStock = new DevExpress.XtraBars.Navigation.TileNavSubItem();
            this.navConfig = new DevExpress.XtraBars.Navigation.TileNavItem();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.bwSync = new System.ComponentModel.BackgroundWorker();
            this.tmSync = new System.Windows.Forms.Timer(this.components);
            this.statusProgressPanel = new DevExpress.XtraWaitForm.ProgressPanel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tileNavPane1
            // 
            this.tileNavPane1.Appearance.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.tileNavPane1.Appearance.Options.UseFont = true;
            this.tileNavPane1.AppearanceHovered.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.tileNavPane1.AppearanceHovered.Options.UseFont = true;
            this.tileNavPane1.AppearanceSelected.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.tileNavPane1.AppearanceSelected.Options.UseFont = true;
            this.tileNavPane1.ButtonPadding = new System.Windows.Forms.Padding(12);
            this.tileNavPane1.Buttons.Add(this.navButton2);
            this.tileNavPane1.Buttons.Add(this.navExit);
            this.tileNavPane1.ContinuousNavigation = true;
            // 
            // tileNavCategory1
            // 
            // 
            // navCredit
            // 
            this.navCredit.Caption = "ลูกหนี้";
            this.navCredit.Name = "navCredit";
            this.navCredit.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            this.navCredit.OwnerCollection = this.tileNavPane1.DefaultCategory.Items;
            // 
            // 
            // 
            this.navCredit.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.navCredit.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navCredit.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement88.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement88.Image")));
            tileItemElement88.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement88.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement88.Text = "ลูกหนี้";
            this.navCredit.Tile.Elements.Add(tileItemElement88);
            this.navCredit.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navCredit.Tile.Name = "tileNavItem";
            this.navCredit.Tile.ShowDropDownButton = DevExpress.Utils.DefaultBoolean.False;
            this.navCredit.Tile.ShowItemShadow = DevExpress.Utils.DefaultBoolean.True;
            // 
            // navRefresh
            // 
            this.navRefresh.Caption = "ปรับปรุงข้อมูล";
            this.navRefresh.Name = "navRefresh";
            this.navRefresh.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            this.navRefresh.OwnerCollection = this.tileNavPane1.DefaultCategory.Items;
            // 
            // 
            // 
            this.navRefresh.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navRefresh.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navRefresh.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement90.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement90.Image")));
            tileItemElement90.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement90.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement90.Text = "ปรับปรุงข้อมูล";
            this.navRefresh.Tile.Elements.Add(tileItemElement90);
            this.navRefresh.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navRefresh.Tile.Name = "tileBarItem1";
            this.navRefresh.Tile.ShowDropDownButton = DevExpress.Utils.DefaultBoolean.False;
            this.navRefresh.Tile.ShowItemShadow = DevExpress.Utils.DefaultBoolean.True;
            this.tileNavPane1.DefaultCategory.Items.AddRange(new DevExpress.XtraBars.Navigation.TileNavItem[] {
            this.navData,
            this.navCustomer,
            this.navClaim,
            this.navReport,
            this.navCredit,
            this.navConfig,
            this.navRefresh});
            this.tileNavPane1.DefaultCategory.Name = "tileNavCategory1";
            this.tileNavPane1.DefaultCategory.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            this.tileNavPane1.DefaultCategory.OwnerCollection = null;
            // 
            // 
            // 
            this.tileNavPane1.DefaultCategory.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            this.tileNavPane1.DefaultCategory.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.tileNavPane1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tileNavPane1.Location = new System.Drawing.Point(0, 0);
            this.tileNavPane1.Name = "tileNavPane1";
            this.tileNavPane1.OptionsPrimaryDropDown.BackColor = System.Drawing.Color.Empty;
            this.tileNavPane1.OptionsSecondaryDropDown.BackColor = System.Drawing.Color.Empty;
            this.tileNavPane1.Size = new System.Drawing.Size(1021, 40);
            this.tileNavPane1.TabIndex = 0;
            this.tileNavPane1.Text = "tileNavPane1";
            this.tileNavPane1.TileClick += new DevExpress.XtraBars.Navigation.NavElementClickEventHandler(this.tileNavPane1_TileClick);
            // 
            // navButton2
            // 
            this.navButton2.Caption = "เมนูหลัก";
            this.navButton2.IsMain = true;
            this.navButton2.Name = "navButton2";
            // 
            // navExit
            // 
            this.navExit.Alignment = DevExpress.XtraBars.Navigation.NavButtonAlignment.Right;
            this.navExit.Caption = "ออกจากระบบ";
            this.navExit.Name = "navExit";
            this.navExit.ElementClick += new DevExpress.XtraBars.Navigation.NavElementClickEventHandler(this.navExit_ElementClick);
            // 
            // navData
            // 
            this.navData.Caption = "สินค้า";
            this.navData.Name = "navData";
            this.navData.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            this.navData.OwnerCollection = this.tileNavPane1.DefaultCategory.Items;
            this.navData.SubItems.AddRange(new DevExpress.XtraBars.Navigation.TileNavSubItem[] {
            this.navSale,
            this.navReceived,
            this.navProduct,
            this.navStock});
            // 
            // 
            // 
            this.navData.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.navData.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navData.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement77.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement77.Image")));
            tileItemElement77.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement77.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement77.Text = "สินค้า";
            this.navData.Tile.Elements.Add(tileItemElement77);
            this.navData.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navData.Tile.Name = "tileBarItem1";
            // 
            // navSale
            // 
            this.navSale.Caption = "ขาย";
            this.navSale.Name = "navSale";
            this.navSale.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            // 
            // 
            // 
            this.navSale.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.navSale.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navSale.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement73.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement73.Image")));
            tileItemElement73.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement73.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement73.Text = "ขาย";
            this.navSale.Tile.Elements.Add(tileItemElement73);
            this.navSale.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navSale.Tile.Name = "tileBarItem2";
            // 
            // navReceived
            // 
            this.navReceived.Caption = "รับเข้า";
            this.navReceived.Name = "navReceived";
            this.navReceived.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            // 
            // 
            // 
            this.navReceived.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.navReceived.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navReceived.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement74.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement74.Image")));
            tileItemElement74.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement74.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement74.Text = "รับเข้า";
            this.navReceived.Tile.Elements.Add(tileItemElement74);
            this.navReceived.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navReceived.Tile.Name = "tileBarItem4";
            // 
            // navProduct
            // 
            this.navProduct.Caption = "ข้อมูล";
            this.navProduct.Name = "navProduct";
            this.navProduct.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            // 
            // 
            // 
            this.navProduct.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.navProduct.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navProduct.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement75.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement75.Image")));
            tileItemElement75.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement75.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement75.Text = "ข้อมูล";
            this.navProduct.Tile.Elements.Add(tileItemElement75);
            this.navProduct.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navProduct.Tile.Name = "tileBarItem3";
            // 
            // navStock
            // 
            this.navStock.Caption = "สต็อค";
            this.navStock.Name = "navStock";
            this.navStock.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            // 
            // 
            // 
            this.navStock.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.navStock.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navStock.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement76.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement76.Image")));
            tileItemElement76.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement76.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement76.Text = "สต็อค";
            this.navStock.Tile.Elements.Add(tileItemElement76);
            this.navStock.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navStock.Tile.Name = "tileBarItem5";
            // 
            // navCustomer
            // 
            this.navCustomer.Caption = "ลูกค้า";
            this.navCustomer.Name = "navCustomer";
            this.navCustomer.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            this.navCustomer.OptionsDropDown.ShowItemShadow = DevExpress.Utils.DefaultBoolean.True;
            this.navCustomer.OwnerCollection = this.tileNavPane1.DefaultCategory.Items;
            // 
            // navCustomerData
            // 
            this.navCustomerData.Caption = "ข้อมูล";
            this.navCustomerData.Name = "navCustomerData";
            this.navCustomerData.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            // 
            // 
            // 
            this.navCustomerData.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.navCustomerData.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navCustomerData.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement78.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement78.Image")));
            tileItemElement78.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement78.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement78.Text = "ข้อมูล";
            this.navCustomerData.Tile.Elements.Add(tileItemElement78);
            this.navCustomerData.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navCustomerData.Tile.Name = "navData";
            this.navCustomer.SubItems.AddRange(new DevExpress.XtraBars.Navigation.TileNavSubItem[] {
            this.navCustomerData});
            // 
            // 
            // 
            this.navCustomer.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.navCustomer.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navCustomer.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement79.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement79.Image")));
            tileItemElement79.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement79.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement79.Text = "ลูกค้า";
            this.navCustomer.Tile.Elements.Add(tileItemElement79);
            this.navCustomer.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navCustomer.Tile.Name = "tileBarItem2";
            // 
            // navClaim
            // 
            this.navClaim.Caption = "เคลม";
            this.navClaim.Name = "navClaim";
            this.navClaim.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            this.navClaim.OwnerCollection = this.tileNavPane1.DefaultCategory.Items;
            // 
            // navClaimData
            // 
            this.navClaimData.Caption = "ข้อมูล";
            this.navClaimData.Name = "navClaimData";
            this.navClaimData.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            // 
            // 
            // 
            this.navClaimData.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.navClaimData.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navClaimData.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement80.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement80.Image")));
            tileItemElement80.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement80.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement80.Text = "ข้อมูล";
            this.navClaimData.Tile.Elements.Add(tileItemElement80);
            this.navClaimData.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navClaimData.Tile.Name = "tileBarItem1";
            this.navClaim.SubItems.AddRange(new DevExpress.XtraBars.Navigation.TileNavSubItem[] {
            this.navClaimData,
            this.navClaimReceivedShop,
            this.navClaimDataShop});
            // 
            // 
            // 
            this.navClaim.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.navClaim.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navClaim.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement83.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement83.Image")));
            tileItemElement83.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement83.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement83.Text = "เคลม";
            this.navClaim.Tile.Elements.Add(tileItemElement83);
            this.navClaim.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navClaim.Tile.Name = "tileBarItem3";
            // 
            // navClaimReceivedShop
            // 
            this.navClaimReceivedShop.Caption = "รับเข้า";
            this.navClaimReceivedShop.Name = "navClaimReceivedShop";
            this.navClaimReceivedShop.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            // 
            // 
            // 
            this.navClaimReceivedShop.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.navClaimReceivedShop.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navClaimReceivedShop.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement81.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement81.Image")));
            tileItemElement81.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement81.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement81.Text = "รับเข้า";
            this.navClaimReceivedShop.Tile.Elements.Add(tileItemElement81);
            this.navClaimReceivedShop.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navClaimReceivedShop.Tile.Name = "tileBarItem1";
            // 
            // navClaimDataShop
            // 
            this.navClaimDataShop.Caption = "สาขา";
            this.navClaimDataShop.Name = "navClaimDataShop";
            this.navClaimDataShop.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            // 
            // 
            // 
            this.navClaimDataShop.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.navClaimDataShop.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navClaimDataShop.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement82.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement82.Image")));
            tileItemElement82.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement82.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement82.Text = "สาขา";
            this.navClaimDataShop.Tile.Elements.Add(tileItemElement82);
            this.navClaimDataShop.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navClaimDataShop.Tile.Name = "tileBarItem1";
            // 
            // navReport
            // 
            this.navReport.Caption = "รายงาน";
            this.navReport.Name = "navReport";
            this.navReport.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            this.navReport.OptionsDropDown.ShowItemShadow = DevExpress.Utils.DefaultBoolean.True;
            this.navReport.OwnerCollection = this.tileNavPane1.DefaultCategory.Items;
            this.navReport.SubItems.AddRange(new DevExpress.XtraBars.Navigation.TileNavSubItem[] {
            this.navReportDaily,
            this.navReportProduct,
            this.navReportStock});
            // 
            // 
            // 
            this.navReport.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.navReport.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navReport.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement87.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement87.Image")));
            tileItemElement87.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement87.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement87.Text = "รายงาน";
            this.navReport.Tile.Elements.Add(tileItemElement87);
            this.navReport.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navReport.Tile.Name = "tileBarItem1";
            // 
            // navReportDaily
            // 
            this.navReportDaily.Caption = "ยอดขาย";
            this.navReportDaily.Name = "navReportDaily";
            this.navReportDaily.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            // 
            // 
            // 
            this.navReportDaily.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.navReportDaily.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navReportDaily.Tile.AppearanceItem.Normal.Options.UseTextOptions = true;
            this.navReportDaily.Tile.AppearanceItem.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.navReportDaily.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement84.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement84.Image")));
            tileItemElement84.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement84.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement84.Text = "ยอดขาย";
            this.navReportDaily.Tile.Elements.Add(tileItemElement84);
            this.navReportDaily.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navReportDaily.Tile.Name = "navReportDaily";
            // 
            // navReportProduct
            // 
            this.navReportProduct.Caption = "สินค้า";
            this.navReportProduct.Name = "navReportProduct";
            this.navReportProduct.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            // 
            // 
            // 
            this.navReportProduct.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.navReportProduct.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navReportProduct.Tile.AppearanceItem.Normal.Options.UseTextOptions = true;
            this.navReportProduct.Tile.AppearanceItem.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.navReportProduct.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement85.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement85.Image")));
            tileItemElement85.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement85.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement85.Text = "สินค้า";
            this.navReportProduct.Tile.Elements.Add(tileItemElement85);
            this.navReportProduct.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navReportProduct.Tile.Name = "tileBarItem1";
            // 
            // navReportStock
            // 
            this.navReportStock.Caption = "สต็อค";
            this.navReportStock.Name = "navReportStock";
            this.navReportStock.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            // 
            // 
            // 
            this.navReportStock.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.navReportStock.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navReportStock.Tile.AppearanceItem.Normal.Options.UseTextOptions = true;
            this.navReportStock.Tile.AppearanceItem.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.navReportStock.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement86.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement86.Image")));
            tileItemElement86.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement86.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement86.Text = "สต็อค";
            this.navReportStock.Tile.Elements.Add(tileItemElement86);
            this.navReportStock.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navReportStock.Tile.Name = "tileBarItem1";
            // 
            // navConfig
            // 
            this.navConfig.Caption = "การตั้งค่า";
            this.navConfig.Name = "navConfig";
            this.navConfig.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            this.navConfig.OwnerCollection = this.tileNavPane1.DefaultCategory.Items;
            // 
            // 
            // 
            this.navConfig.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.navConfig.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navConfig.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement89.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement89.Image")));
            tileItemElement89.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement89.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement89.Text = "การตั้งค่า";
            this.navConfig.Tile.Elements.Add(tileItemElement89);
            this.navConfig.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navConfig.Tile.Name = "tileBarItem4";
            this.navConfig.Tile.ShowItemShadow = DevExpress.Utils.DefaultBoolean.True;
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 40);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1021, 507);
            this.pnlMain.TabIndex = 1;
            // 
            // bwSync
            // 
            this.bwSync.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwSync_DoWork);
            this.bwSync.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwSync_RunWorkerCompleted);
            // 
            // tmSync
            // 
            this.tmSync.Enabled = true;
            this.tmSync.Interval = 10000;
            this.tmSync.Tick += new System.EventHandler(this.tmSync_Tick);
            // 
            // statusProgressPanel
            // 
            this.statusProgressPanel.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.statusProgressPanel.Appearance.Options.UseBackColor = true;
            this.statusProgressPanel.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusProgressPanel.AppearanceCaption.Options.UseFont = true;
            this.statusProgressPanel.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.statusProgressPanel.AppearanceDescription.Options.UseFont = true;
            this.statusProgressPanel.Caption = "กำลัง Sync ข้อมูลเข้าระบบ Cloud";
            this.statusProgressPanel.Description = "";
            this.statusProgressPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusProgressPanel.Location = new System.Drawing.Point(2, -4);
            this.statusProgressPanel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.statusProgressPanel.Name = "statusProgressPanel";
            this.statusProgressPanel.Size = new System.Drawing.Size(1017, 30);
            this.statusProgressPanel.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.statusProgressPanel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 547);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1021, 28);
            this.panelControl1.TabIndex = 2;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 575);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.tileNavPane1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Navigation.TileNavPane tileNavPane1;
        private DevExpress.XtraBars.Navigation.NavButton navButton2;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private System.ComponentModel.BackgroundWorker bwSync;
        private System.Windows.Forms.Timer tmSync;
        private DevExpress.XtraWaitForm.ProgressPanel statusProgressPanel;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraBars.Navigation.NavButton navExit;
        private DevExpress.XtraBars.Navigation.TileNavSubItem navSale;
        private DevExpress.XtraBars.Navigation.TileNavSubItem navProduct;
        private DevExpress.XtraBars.Navigation.TileNavSubItem navReceived;
        private DevExpress.XtraBars.Navigation.TileNavSubItem navStock;
        private DevExpress.XtraBars.Navigation.TileNavSubItem navReportProduct;
        private DevExpress.XtraBars.Navigation.TileNavItem navReport;
        private DevExpress.XtraBars.Navigation.TileNavItem navData;
        private DevExpress.XtraBars.Navigation.TileNavSubItem navReportStock;
        private DevExpress.XtraBars.Navigation.TileNavSubItem navReportDaily;
        private DevExpress.XtraBars.Navigation.TileNavItem navCustomer;
        private DevExpress.XtraBars.Navigation.TileNavItem navConfig;
        private DevExpress.XtraBars.Navigation.TileNavItem navClaim;
        private DevExpress.XtraBars.Navigation.TileNavSubItem navClaimReceivedShop;
        private DevExpress.XtraBars.Navigation.TileNavSubItem navClaimDataShop;
        private DevExpress.XtraBars.Navigation.TileNavItem navCredit;
        private DevExpress.XtraBars.Navigation.TileNavSubItem navClaimData;
        private DevExpress.XtraBars.Navigation.TileNavSubItem navCustomerData;
        private DevExpress.XtraBars.Navigation.TileNavItem navRefresh;
    }
}