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
            DevExpress.XtraEditors.TileItemElement tileItemElement5 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement1 = new DevExpress.XtraEditors.TileItemElement();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            DevExpress.XtraEditors.TileItemElement tileItemElement2 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement3 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement4 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement7 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement6 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement8 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement12 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement9 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement10 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement11 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement13 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement14 = new DevExpress.XtraEditors.TileItemElement();
            this.z = new DevExpress.XtraBars.Navigation.TileNavPane();
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
            this.navReport = new DevExpress.XtraBars.Navigation.TileNavItem();
            this.navReportDaily = new DevExpress.XtraBars.Navigation.TileNavSubItem();
            this.navStatistic = new DevExpress.XtraBars.Navigation.TileNavSubItem();
            this.navReportProduct = new DevExpress.XtraBars.Navigation.TileNavSubItem();
            this.navCredit = new DevExpress.XtraBars.Navigation.TileNavItem();
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
            // z
            // 
            this.z.Appearance.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.z.Appearance.Options.UseFont = true;
            this.z.AppearanceHovered.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.z.AppearanceHovered.Options.UseFont = true;
            this.z.AppearanceSelected.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.z.AppearanceSelected.Options.UseFont = true;
            this.z.ButtonPadding = new System.Windows.Forms.Padding(12);
            this.z.Buttons.Add(this.navButton2);
            this.z.Buttons.Add(this.navExit);
            this.z.ContinuousNavigation = true;
            // 
            // tileNavCategory1
            // 
            this.z.DefaultCategory.Items.AddRange(new DevExpress.XtraBars.Navigation.TileNavItem[] {
            this.navData,
            this.navCustomer,
            this.navClaim,
            this.navReport,
            this.navCredit,
            this.navConfig});
            this.z.DefaultCategory.Name = "tileNavCategory1";
            this.z.DefaultCategory.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            this.z.DefaultCategory.OwnerCollection = null;
            // 
            // 
            // 
            this.z.DefaultCategory.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            this.z.DefaultCategory.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.z.Dock = System.Windows.Forms.DockStyle.Top;
            this.z.Location = new System.Drawing.Point(0, 0);
            this.z.Name = "z";
            this.z.OptionsPrimaryDropDown.BackColor = System.Drawing.Color.Empty;
            this.z.OptionsSecondaryDropDown.BackColor = System.Drawing.Color.Empty;
            this.z.Size = new System.Drawing.Size(1021, 40);
            this.z.TabIndex = 0;
            this.z.Text = "tileNavPane1";
            this.z.TileClick += new DevExpress.XtraBars.Navigation.NavElementClickEventHandler(this.tileNavPane1_TileClick);
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
            this.navData.OwnerCollection = this.z.DefaultCategory.Items;
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
            tileItemElement5.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement5.Image")));
            tileItemElement5.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement5.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement5.Text = "สินค้า";
            this.navData.Tile.Elements.Add(tileItemElement5);
            this.navData.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navData.Tile.Name = "tileBarItem1";
            this.navData.Tile.ShowDropDownButton = DevExpress.Utils.DefaultBoolean.False;
            this.navData.Tile.ShowItemShadow = DevExpress.Utils.DefaultBoolean.True;
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
            tileItemElement1.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement1.Image")));
            tileItemElement1.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement1.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement1.Text = "ขาย";
            this.navSale.Tile.Elements.Add(tileItemElement1);
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
            tileItemElement2.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement2.Image")));
            tileItemElement2.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement2.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement2.Text = "รับเข้า";
            this.navReceived.Tile.Elements.Add(tileItemElement2);
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
            tileItemElement3.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement3.Image")));
            tileItemElement3.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement3.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement3.Text = "ข้อมูล";
            this.navProduct.Tile.Elements.Add(tileItemElement3);
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
            tileItemElement4.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement4.Image")));
            tileItemElement4.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement4.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement4.Text = "สต็อค";
            this.navStock.Tile.Elements.Add(tileItemElement4);
            this.navStock.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navStock.Tile.Name = "tileBarItem5";
            // 
            // navCustomer
            // 
            this.navCustomer.Caption = "ลูกค้า";
            this.navCustomer.Name = "navCustomer";
            this.navCustomer.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            this.navCustomer.OptionsDropDown.ShowItemShadow = DevExpress.Utils.DefaultBoolean.True;
            this.navCustomer.OwnerCollection = this.z.DefaultCategory.Items;
            this.navCustomer.SubItems.AddRange(new DevExpress.XtraBars.Navigation.TileNavSubItem[] {
            this.navCustomerData});
            // 
            // 
            // 
            this.navCustomer.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.navCustomer.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navCustomer.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement7.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement7.Image")));
            tileItemElement7.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement7.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement7.Text = "ลูกค้า";
            this.navCustomer.Tile.Elements.Add(tileItemElement7);
            this.navCustomer.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navCustomer.Tile.Name = "tileBarItem2";
            this.navCustomer.Tile.ShowDropDownButton = DevExpress.Utils.DefaultBoolean.False;
            this.navCustomer.Tile.ShowItemShadow = DevExpress.Utils.DefaultBoolean.True;
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
            tileItemElement6.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement6.Image")));
            tileItemElement6.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement6.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement6.Text = "ข้อมูล";
            this.navCustomerData.Tile.Elements.Add(tileItemElement6);
            this.navCustomerData.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navCustomerData.Tile.Name = "navData";
            // 
            // navClaim
            // 
            this.navClaim.Caption = "เคลม";
            this.navClaim.Name = "navClaim";
            this.navClaim.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            this.navClaim.OwnerCollection = this.z.DefaultCategory.Items;
            // 
            // 
            // 
            this.navClaim.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.navClaim.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navClaim.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement8.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement8.Image")));
            tileItemElement8.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement8.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement8.Text = "เคลม";
            this.navClaim.Tile.Elements.Add(tileItemElement8);
            this.navClaim.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navClaim.Tile.Name = "tileBarItem3";
            this.navClaim.Tile.ShowItemShadow = DevExpress.Utils.DefaultBoolean.True;
            // 
            // navReport
            // 
            this.navReport.Caption = "รายงาน";
            this.navReport.Name = "navReport";
            this.navReport.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            this.navReport.OptionsDropDown.ShowItemShadow = DevExpress.Utils.DefaultBoolean.True;
            this.navReport.OwnerCollection = this.z.DefaultCategory.Items;
            this.navReport.SubItems.AddRange(new DevExpress.XtraBars.Navigation.TileNavSubItem[] {
            this.navReportDaily,
            this.navStatistic,
            this.navReportProduct});
            // 
            // 
            // 
            this.navReport.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.navReport.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navReport.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement12.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement12.Image")));
            tileItemElement12.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement12.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement12.Text = "รายงาน";
            this.navReport.Tile.Elements.Add(tileItemElement12);
            this.navReport.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navReport.Tile.Name = "tileBarItem1";
            this.navReport.Tile.ShowDropDownButton = DevExpress.Utils.DefaultBoolean.False;
            this.navReport.Tile.ShowItemShadow = DevExpress.Utils.DefaultBoolean.True;
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
            tileItemElement9.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement9.Image")));
            tileItemElement9.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement9.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement9.Text = "ยอดขาย";
            this.navReportDaily.Tile.Elements.Add(tileItemElement9);
            this.navReportDaily.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navReportDaily.Tile.Name = "navReportDaily";
            // 
            // navStatistic
            // 
            this.navStatistic.Caption = "สถิติ";
            this.navStatistic.Name = "navStatistic";
            this.navStatistic.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            // 
            // 
            // 
            this.navStatistic.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.navStatistic.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navStatistic.Tile.AppearanceItem.Normal.Options.UseTextOptions = true;
            this.navStatistic.Tile.AppearanceItem.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.navStatistic.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement10.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement10.Image")));
            tileItemElement10.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement10.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement10.Text = "สถิติ";
            this.navStatistic.Tile.Elements.Add(tileItemElement10);
            this.navStatistic.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navStatistic.Tile.Name = "tileBarItem1";
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
            tileItemElement11.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement11.Image")));
            tileItemElement11.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement11.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement11.Text = "สินค้า";
            this.navReportProduct.Tile.Elements.Add(tileItemElement11);
            this.navReportProduct.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navReportProduct.Tile.Name = "tileBarItem1";
            // 
            // navCredit
            // 
            this.navCredit.Caption = "ลูกหนี้";
            this.navCredit.Name = "navCredit";
            this.navCredit.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            this.navCredit.OwnerCollection = this.z.DefaultCategory.Items;
            // 
            // 
            // 
            this.navCredit.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.navCredit.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navCredit.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement13.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement13.Image")));
            tileItemElement13.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement13.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement13.Text = "ลูกหนี้";
            this.navCredit.Tile.Elements.Add(tileItemElement13);
            this.navCredit.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Default;
            this.navCredit.Tile.Name = "tileNavItem";
            this.navCredit.Tile.ShowDropDownButton = DevExpress.Utils.DefaultBoolean.False;
            this.navCredit.Tile.ShowItemShadow = DevExpress.Utils.DefaultBoolean.True;
            // 
            // navConfig
            // 
            this.navConfig.Caption = "การตั้งค่า";
            this.navConfig.Name = "navConfig";
            this.navConfig.OptionsDropDown.BackColor = System.Drawing.Color.Empty;
            this.navConfig.OwnerCollection = this.z.DefaultCategory.Items;
            // 
            // 
            // 
            this.navConfig.Tile.AppearanceItem.Normal.Font = new System.Drawing.Font("DilleniaUPC", 22F, System.Drawing.FontStyle.Bold);
            this.navConfig.Tile.AppearanceItem.Normal.Options.UseFont = true;
            this.navConfig.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement14.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement14.Image")));
            tileItemElement14.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement14.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement14.Text = "การตั้งค่า";
            this.navConfig.Tile.Elements.Add(tileItemElement14);
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
            this.statusProgressPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusProgressPanel.Location = new System.Drawing.Point(2, 2);
            this.statusProgressPanel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.statusProgressPanel.Name = "statusProgressPanel";
            this.statusProgressPanel.Size = new System.Drawing.Size(1017, 22);
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
            this.Controls.Add(this.z);
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

        private DevExpress.XtraBars.Navigation.TileNavPane z;
        private DevExpress.XtraBars.Navigation.NavButton navButton2;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraBars.Navigation.TileNavSubItem navCustomerData;
        private System.ComponentModel.BackgroundWorker bwSync;
        private System.Windows.Forms.Timer tmSync;
        private DevExpress.XtraWaitForm.ProgressPanel statusProgressPanel;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraBars.Navigation.TileNavItem navReport;
        private DevExpress.XtraBars.Navigation.TileNavSubItem navReportDaily;
        private DevExpress.XtraBars.Navigation.TileNavItem navClaim;
        private DevExpress.XtraBars.Navigation.TileNavItem navData;
        private DevExpress.XtraBars.Navigation.TileNavItem navCustomer;
        private DevExpress.XtraBars.Navigation.NavButton navExit;
        private DevExpress.XtraBars.Navigation.TileNavSubItem navSale;
        private DevExpress.XtraBars.Navigation.TileNavSubItem navProduct;
        private DevExpress.XtraBars.Navigation.TileNavItem navConfig;
        private DevExpress.XtraBars.Navigation.TileNavSubItem navReceived;
        private DevExpress.XtraBars.Navigation.TileNavSubItem navStock;
        private DevExpress.XtraBars.Navigation.TileNavSubItem navStatistic;
        private DevExpress.XtraBars.Navigation.TileNavSubItem navReportProduct;
        private DevExpress.XtraBars.Navigation.TileNavItem navCredit;
    }
}