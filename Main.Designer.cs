﻿namespace PowerPOS
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
            DevExpress.XtraEditors.TileItemElement tileItemElement19 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement15 = new DevExpress.XtraEditors.TileItemElement();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            DevExpress.XtraEditors.TileItemElement tileItemElement16 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement17 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement18 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement21 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement20 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement22 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement26 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement23 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement24 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement25 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement27 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement28 = new DevExpress.XtraEditors.TileItemElement();
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
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
            tileItemElement19.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement19.Image")));
            tileItemElement19.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement19.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement19.Text = "สินค้า";
            this.navData.Tile.Elements.Add(tileItemElement19);
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
            tileItemElement15.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement15.Image")));
            tileItemElement15.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement15.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement15.Text = "ขาย";
            this.navSale.Tile.Elements.Add(tileItemElement15);
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
            tileItemElement16.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement16.Image")));
            tileItemElement16.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement16.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement16.Text = "รับเข้า";
            this.navReceived.Tile.Elements.Add(tileItemElement16);
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
            tileItemElement17.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement17.Image")));
            tileItemElement17.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement17.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement17.Text = "ข้อมูล";
            this.navProduct.Tile.Elements.Add(tileItemElement17);
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
            tileItemElement18.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement18.Image")));
            tileItemElement18.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement18.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement18.Text = "สต็อค";
            this.navStock.Tile.Elements.Add(tileItemElement18);
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
            tileItemElement21.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement21.Image")));
            tileItemElement21.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement21.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement21.Text = "ลูกค้า";
            this.navCustomer.Tile.Elements.Add(tileItemElement21);
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
            tileItemElement20.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement20.Image")));
            tileItemElement20.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement20.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement20.Text = "ข้อมูล";
            this.navCustomerData.Tile.Elements.Add(tileItemElement20);
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
            tileItemElement22.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement22.Image")));
            tileItemElement22.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement22.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement22.Text = "เคลม";
            this.navClaim.Tile.Elements.Add(tileItemElement22);
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
            tileItemElement26.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement26.Image")));
            tileItemElement26.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement26.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement26.Text = "รายงาน";
            this.navReport.Tile.Elements.Add(tileItemElement26);
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
            tileItemElement23.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement23.Image")));
            tileItemElement23.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement23.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement23.Text = "ยอดขาย";
            this.navReportDaily.Tile.Elements.Add(tileItemElement23);
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
            tileItemElement24.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement24.Image")));
            tileItemElement24.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement24.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement24.Text = "สถิติ";
            this.navStatistic.Tile.Elements.Add(tileItemElement24);
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
            tileItemElement25.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement25.Image")));
            tileItemElement25.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement25.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement25.Text = "สินค้า";
            this.navReportProduct.Tile.Elements.Add(tileItemElement25);
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
            tileItemElement27.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement27.Image")));
            tileItemElement27.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement27.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement27.Text = "ลูกหนี้";
            this.navCredit.Tile.Elements.Add(tileItemElement27);
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
            tileItemElement28.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement28.Image")));
            tileItemElement28.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement28.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement28.Text = "การตั้งค่า";
            this.navConfig.Tile.Elements.Add(tileItemElement28);
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
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.statusProgressPanel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 547);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1021, 28);
            this.panelControl1.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(963, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(45, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "V 1.0.0.9";
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
            this.panelControl1.PerformLayout();
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
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}