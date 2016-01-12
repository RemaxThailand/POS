namespace PowerPOS
{
    partial class UcStatistic
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcStatistic));
            this.miPrintReceipt = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticGridControl = new DevExpress.XtraGrid.GridControl();
            this.statisticGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cllDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clSaleNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clCustomer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clMobile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.spCount = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cbbType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.chartReport = new DevExpress.XtraCharts.ChartControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.statisticGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statisticGridView)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.SuspendLayout();
            // 
            // miPrintReceipt
            // 
            this.miPrintReceipt.Image = global::PowerPOS.Properties.Resources.printer;
            this.miPrintReceipt.Name = "miPrintReceipt";
            this.miPrintReceipt.Size = new System.Drawing.Size(157, 22);
            this.miPrintReceipt.Text = "พิมพ์ใบเสร็จรับเงิน";
            // 
            // statisticGridControl
            // 
            this.statisticGridControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.statisticGridControl.Location = new System.Drawing.Point(240, 0);
            this.statisticGridControl.MainView = this.statisticGridView;
            this.statisticGridControl.Name = "statisticGridControl";
            this.statisticGridControl.Size = new System.Drawing.Size(659, 300);
            this.statisticGridControl.TabIndex = 6;
            this.statisticGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.statisticGridView});
            // 
            // statisticGridView
            // 
            this.statisticGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clNo,
            this.cllDate,
            this.clSaleNo,
            this.clCustomer,
            this.clMobile,
            this.clTotal});
            this.statisticGridView.GridControl = this.statisticGridControl;
            this.statisticGridView.Name = "statisticGridView";
            this.statisticGridView.OptionsView.ShowGroupPanel = false;
            // 
            // clNo
            // 
            this.clNo.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clNo.AppearanceCell.Options.UseFont = true;
            this.clNo.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clNo.AppearanceHeader.Options.UseFont = true;
            this.clNo.AppearanceHeader.Options.UseTextOptions = true;
            this.clNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clNo.Caption = "ที่";
            this.clNo.FieldName = "No";
            this.clNo.Name = "clNo";
            this.clNo.OptionsColumn.AllowEdit = false;
            this.clNo.OptionsColumn.AllowMove = false;
            this.clNo.OptionsColumn.FixedWidth = true;
            this.clNo.Visible = true;
            this.clNo.VisibleIndex = 0;
            this.clNo.Width = 30;
            // 
            // cllDate
            // 
            this.cllDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cllDate.AppearanceCell.Options.UseFont = true;
            this.cllDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cllDate.AppearanceHeader.Options.UseFont = true;
            this.cllDate.AppearanceHeader.Options.UseTextOptions = true;
            this.cllDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cllDate.Caption = "รหัสสินค้า";
            this.cllDate.FieldName = "Product";
            this.cllDate.Name = "cllDate";
            this.cllDate.OptionsColumn.AllowEdit = false;
            this.cllDate.OptionsColumn.AllowMove = false;
            this.cllDate.OptionsColumn.FixedWidth = true;
            this.cllDate.Visible = true;
            this.cllDate.VisibleIndex = 1;
            this.cllDate.Width = 100;
            // 
            // clSaleNo
            // 
            this.clSaleNo.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clSaleNo.AppearanceCell.Options.UseFont = true;
            this.clSaleNo.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clSaleNo.AppearanceHeader.Options.UseFont = true;
            this.clSaleNo.AppearanceHeader.Options.UseTextOptions = true;
            this.clSaleNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clSaleNo.Caption = "ชื่อสินค้า";
            this.clSaleNo.FieldName = "ProductName";
            this.clSaleNo.Name = "clSaleNo";
            this.clSaleNo.OptionsColumn.AllowEdit = false;
            this.clSaleNo.OptionsColumn.AllowMove = false;
            this.clSaleNo.OptionsColumn.FixedWidth = true;
            this.clSaleNo.Visible = true;
            this.clSaleNo.VisibleIndex = 2;
            this.clSaleNo.Width = 200;
            // 
            // clCustomer
            // 
            this.clCustomer.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clCustomer.AppearanceCell.Options.UseFont = true;
            this.clCustomer.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clCustomer.AppearanceHeader.Options.UseFont = true;
            this.clCustomer.AppearanceHeader.Options.UseTextOptions = true;
            this.clCustomer.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clCustomer.Caption = "หมวดหมู่";
            this.clCustomer.FieldName = "Category";
            this.clCustomer.Name = "clCustomer";
            this.clCustomer.OptionsColumn.AllowEdit = false;
            this.clCustomer.OptionsColumn.AllowMove = false;
            this.clCustomer.OptionsColumn.FixedWidth = true;
            this.clCustomer.Visible = true;
            this.clCustomer.VisibleIndex = 3;
            this.clCustomer.Width = 100;
            // 
            // clMobile
            // 
            this.clMobile.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clMobile.AppearanceCell.Options.UseFont = true;
            this.clMobile.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clMobile.AppearanceHeader.Options.UseFont = true;
            this.clMobile.AppearanceHeader.Options.UseTextOptions = true;
            this.clMobile.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clMobile.Caption = "ยี่ห้อ";
            this.clMobile.FieldName = "Brand";
            this.clMobile.Name = "clMobile";
            this.clMobile.OptionsColumn.AllowEdit = false;
            this.clMobile.OptionsColumn.AllowMove = false;
            this.clMobile.OptionsColumn.FixedWidth = true;
            this.clMobile.Visible = true;
            this.clMobile.VisibleIndex = 4;
            this.clMobile.Width = 100;
            // 
            // clTotal
            // 
            this.clTotal.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clTotal.AppearanceCell.Options.UseFont = true;
            this.clTotal.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clTotal.AppearanceHeader.Options.UseFont = true;
            this.clTotal.AppearanceHeader.Options.UseTextOptions = true;
            this.clTotal.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clTotal.Caption = "จำนวนที่ขาย";
            this.clTotal.FieldName = "Total";
            this.clTotal.Name = "clTotal";
            this.clTotal.OptionsColumn.AllowEdit = false;
            this.clTotal.OptionsColumn.AllowMove = false;
            this.clTotal.OptionsColumn.FixedWidth = true;
            this.clTotal.Visible = true;
            this.clTotal.VisibleIndex = 5;
            this.clTotal.Width = 80;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miPrintReceipt,
            this.miDetail});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(158, 48);
            // 
            // miDetail
            // 
            this.miDetail.Image = global::PowerPOS.Properties.Resources.table_money;
            this.miDetail.Name = "miDetail";
            this.miDetail.Size = new System.Drawing.Size(157, 22);
            this.miDetail.Text = "รายละเอียดการขาย";
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup1;
            this.navBarControl1.Appearance.NavigationPaneHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.navBarControl1.Appearance.NavigationPaneHeader.Options.UseFont = true;
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 240;
            this.navBarControl1.OptionsNavPane.ShowOverflowPanel = false;
            this.navBarControl1.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.navBarControl1.Size = new System.Drawing.Size(240, 479);
            this.navBarControl1.TabIndex = 5;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.navBarGroup1.Appearance.Options.UseFont = true;
            this.navBarGroup1.Caption = "สถิติขายสินค้า";
            this.navBarGroup1.ControlContainer = this.navBarGroupControlContainer2;
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.GroupClientHeight = 80;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup1.LargeImage = ((System.Drawing.Image)(resources.GetObject("navBarGroup1.LargeImage")));
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.navBarGroupControlContainer2.Appearance.Options.UseBackColor = true;
            this.navBarGroupControlContainer2.Controls.Add(this.panelControl2);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(240, 393);
            this.navBarGroupControlContainer2.TabIndex = 1;
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.panelControl2.Appearance.Options.UseFont = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.groupControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Padding = new System.Windows.Forms.Padding(3);
            this.panelControl2.Size = new System.Drawing.Size(240, 393);
            this.panelControl2.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnSearch);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.spCount);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.cbbType);
            this.groupControl1.Controls.Add(this.dtpEndDate);
            this.groupControl1.Controls.Add(this.dtpStartDate);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(3, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(234, 171);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "ค้นหาข้อมูล";
            // 
            // btnSearch
            // 
            this.btnSearch.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btnSearch.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Appearance.Options.UseFont = true;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(154, 139);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(68, 27);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "ค้นหา";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(110, 57);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(32, 16);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "อันดับ";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(11, 57);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(35, 16);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "จำนวน";
            // 
            // spCount
            // 
            this.spCount.EditValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spCount.Location = new System.Drawing.Point(49, 56);
            this.spCount.Name = "spCount";
            this.spCount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spCount.Properties.Appearance.Options.UseFont = true;
            this.spCount.Properties.Appearance.Options.UseTextOptions = true;
            this.spCount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.spCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spCount.Properties.DisplayFormat.FormatString = "0";
            this.spCount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spCount.Size = new System.Drawing.Size(55, 22);
            this.spCount.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(27, 113);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(14, 16);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "ถึง";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(12, 84);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(30, 16);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "ตั้งแต่";
            // 
            // cbbType
            // 
            this.cbbType.Location = new System.Drawing.Point(12, 30);
            this.cbbType.Name = "cbbType";
            this.cbbType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbType.Properties.Appearance.Options.UseFont = true;
            this.cbbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbbType.Properties.Items.AddRange(new object[] {
            "สินค้าขายดี",
            "สินค้าขายไม่ดี",
            "สินค้าที่ทำกำไรสูงสุด",
            "สินค้าที่ทำกำไรต่ำสุด"});
            this.cbbType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbbType.Size = new System.Drawing.Size(210, 22);
            this.cbbType.TabIndex = 2;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Location = new System.Drawing.Point(49, 110);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(173, 23);
            this.dtpEndDate.TabIndex = 1;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Location = new System.Drawing.Point(49, 81);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(173, 23);
            this.dtpStartDate.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl1.Location = new System.Drawing.Point(899, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(10, 479);
            this.panelControl1.TabIndex = 7;
            // 
            // chartReport
            // 
            this.chartReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartReport.Location = new System.Drawing.Point(240, 310);
            this.chartReport.Name = "chartReport";
            this.chartReport.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartReport.Size = new System.Drawing.Size(659, 169);
            this.chartReport.TabIndex = 8;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(240, 300);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(659, 10);
            this.panelControl3.TabIndex = 9;
            // 
            // UcStatistic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartReport);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.statisticGridControl);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.navBarControl1);
            this.Name = "UcStatistic";
            this.Size = new System.Drawing.Size(909, 479);
            this.Load += new System.EventHandler(this.UcStatistic_Load);
            ((System.ComponentModel.ISupportInitialize)(this.statisticGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statisticGridView)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem miPrintReceipt;
        private DevExpress.XtraGrid.GridControl statisticGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView statisticGridView;
        private DevExpress.XtraGrid.Columns.GridColumn clNo;
        private DevExpress.XtraGrid.Columns.GridColumn cllDate;
        private DevExpress.XtraGrid.Columns.GridColumn clSaleNo;
        private DevExpress.XtraGrid.Columns.GridColumn clCustomer;
        private DevExpress.XtraGrid.Columns.GridColumn clMobile;
        private DevExpress.XtraGrid.Columns.GridColumn clTotal;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem miDetail;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cbbType;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SpinEdit spCount;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraCharts.ChartControl chartReport;
        private DevExpress.XtraEditors.PanelControl panelControl3;
    }
}
