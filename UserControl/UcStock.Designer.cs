namespace PowerPOS
{
    partial class UcStock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcStock));
            this.stockGridControl = new DevExpress.XtraGrid.GridControl();
            this.stockGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clProduct = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clProgress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clSku = new DevExpress.XtraGrid.Columns.GridColumn();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.ptbProduct = new System.Windows.Forms.PictureBox();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.panelControl11 = new DevExpress.XtraEditors.PanelControl();
            this.txtBarcode = new DevExpress.XtraEditors.TextEdit();
            this.panelControl12 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            this.btnNewCount = new DevExpress.XtraEditors.SimpleButton();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.panelControl7 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.stockGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockGridView)).BeginInit();
            this.navBarGroupControlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBarcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).BeginInit();
            this.SuspendLayout();
            // 
            // stockGridControl
            // 
            this.stockGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stockGridControl.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stockGridControl.Location = new System.Drawing.Point(227, 0);
            this.stockGridControl.MainView = this.stockGridView;
            this.stockGridControl.Name = "stockGridControl";
            this.stockGridControl.Size = new System.Drawing.Size(747, 545);
            this.stockGridControl.TabIndex = 6;
            this.stockGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.stockGridView});
            // 
            // stockGridView
            // 
            this.stockGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clNo,
            this.clProduct,
            this.clName,
            this.clCategory,
            this.clQty,
            this.clCheck,
            this.clProgress,
            this.clSku});
            this.stockGridView.GridControl = this.stockGridControl;
            this.stockGridView.Name = "stockGridView";
            this.stockGridView.OptionsView.ShowGroupPanel = false;
            this.stockGridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.stockGridView_FocusedRowChanged);
            // 
            // clNo
            // 
            this.clNo.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clNo.AppearanceCell.Options.UseFont = true;
            this.clNo.AppearanceCell.Options.UseTextOptions = true;
            this.clNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.clNo.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clNo.AppearanceHeader.Options.UseFont = true;
            this.clNo.AppearanceHeader.Options.UseTextOptions = true;
            this.clNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clNo.Caption = "ที่";
            this.clNo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.clNo.FieldName = "No";
            this.clNo.Name = "clNo";
            this.clNo.OptionsColumn.AllowEdit = false;
            this.clNo.OptionsColumn.AllowMove = false;
            this.clNo.OptionsColumn.FixedWidth = true;
            this.clNo.Visible = true;
            this.clNo.VisibleIndex = 0;
            this.clNo.Width = 40;
            // 
            // clProduct
            // 
            this.clProduct.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clProduct.AppearanceCell.Options.UseFont = true;
            this.clProduct.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clProduct.AppearanceHeader.Options.UseFont = true;
            this.clProduct.AppearanceHeader.Options.UseTextOptions = true;
            this.clProduct.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clProduct.Caption = "รหัสสินค้า";
            this.clProduct.FieldName = "Product";
            this.clProduct.Name = "clProduct";
            this.clProduct.OptionsColumn.AllowEdit = false;
            this.clProduct.OptionsColumn.AllowMove = false;
            this.clProduct.OptionsColumn.FixedWidth = true;
            this.clProduct.Visible = true;
            this.clProduct.VisibleIndex = 1;
            this.clProduct.Width = 100;
            // 
            // clName
            // 
            this.clName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clName.AppearanceCell.Options.UseFont = true;
            this.clName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clName.AppearanceHeader.Options.UseFont = true;
            this.clName.AppearanceHeader.Options.UseTextOptions = true;
            this.clName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clName.Caption = "ชื่อสินค้า";
            this.clName.FieldName = "Name";
            this.clName.Name = "clName";
            this.clName.OptionsColumn.AllowEdit = false;
            this.clName.OptionsColumn.AllowMove = false;
            this.clName.OptionsColumn.FixedWidth = true;
            this.clName.Visible = true;
            this.clName.VisibleIndex = 2;
            this.clName.Width = 250;
            // 
            // clCategory
            // 
            this.clCategory.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clCategory.AppearanceCell.Options.UseFont = true;
            this.clCategory.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clCategory.AppearanceHeader.Options.UseFont = true;
            this.clCategory.AppearanceHeader.Options.UseTextOptions = true;
            this.clCategory.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clCategory.Caption = "ประเภทสินค้า";
            this.clCategory.FieldName = "Category";
            this.clCategory.Name = "clCategory";
            this.clCategory.OptionsColumn.AllowEdit = false;
            this.clCategory.OptionsColumn.AllowMove = false;
            this.clCategory.OptionsColumn.FixedWidth = true;
            this.clCategory.Visible = true;
            this.clCategory.VisibleIndex = 3;
            this.clCategory.Width = 100;
            // 
            // clQty
            // 
            this.clQty.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clQty.AppearanceCell.Options.UseFont = true;
            this.clQty.AppearanceCell.Options.UseTextOptions = true;
            this.clQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.clQty.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clQty.AppearanceHeader.Options.UseFont = true;
            this.clQty.AppearanceHeader.Options.UseTextOptions = true;
            this.clQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clQty.Caption = "จำนวน";
            this.clQty.FieldName = "Qty";
            this.clQty.Name = "clQty";
            this.clQty.OptionsColumn.AllowEdit = false;
            this.clQty.OptionsColumn.AllowMove = false;
            this.clQty.OptionsColumn.FixedWidth = true;
            this.clQty.Visible = true;
            this.clQty.VisibleIndex = 4;
            this.clQty.Width = 80;
            // 
            // clCheck
            // 
            this.clCheck.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clCheck.AppearanceCell.Options.UseFont = true;
            this.clCheck.AppearanceCell.Options.UseTextOptions = true;
            this.clCheck.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.clCheck.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clCheck.AppearanceHeader.Options.UseFont = true;
            this.clCheck.AppearanceHeader.Options.UseTextOptions = true;
            this.clCheck.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clCheck.Caption = "ตรวจสอบแล้ว";
            this.clCheck.FieldName = "Check";
            this.clCheck.Name = "clCheck";
            this.clCheck.OptionsColumn.AllowEdit = false;
            this.clCheck.OptionsColumn.AllowMove = false;
            this.clCheck.OptionsColumn.FixedWidth = true;
            this.clCheck.Visible = true;
            this.clCheck.VisibleIndex = 5;
            this.clCheck.Width = 80;
            // 
            // clProgress
            // 
            this.clProgress.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clProgress.AppearanceCell.Options.UseFont = true;
            this.clProgress.AppearanceCell.Options.UseTextOptions = true;
            this.clProgress.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clProgress.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clProgress.AppearanceHeader.Options.UseFont = true;
            this.clProgress.AppearanceHeader.Options.UseTextOptions = true;
            this.clProgress.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clProgress.Caption = "เปอร์เซ็นต์ตรวจสอบ";
            this.clProgress.FieldName = "Progress";
            this.clProgress.Name = "clProgress";
            this.clProgress.OptionsColumn.AllowEdit = false;
            this.clProgress.OptionsColumn.AllowMove = false;
            this.clProgress.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.clProgress.OptionsColumn.FixedWidth = true;
            this.clProgress.OptionsFilter.AllowAutoFilter = false;
            this.clProgress.OptionsFilter.AllowFilter = false;
            this.clProgress.Visible = true;
            this.clProgress.VisibleIndex = 6;
            this.clProgress.Width = 100;
            // 
            // clSku
            // 
            this.clSku.Caption = "Sku";
            this.clSku.FieldName = "Sku";
            this.clSku.Name = "clSku";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.navBarGroup1.Appearance.Options.UseFont = true;
            this.navBarGroup1.Caption = "ตรวจสอบสต็อคสินค้า";
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
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(227, 459);
            this.navBarGroupControlContainer2.TabIndex = 1;
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.panelControl2.Appearance.Options.UseFont = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.ptbProduct);
            this.panelControl2.Controls.Add(this.panelControl5);
            this.panelControl2.Controls.Add(this.panelControl4);
            this.panelControl2.Controls.Add(this.panelControl1);
            this.panelControl2.Controls.Add(this.progressBarControl1);
            this.panelControl2.Controls.Add(this.panelControl3);
            this.panelControl2.Controls.Add(this.lblStatus);
            this.panelControl2.Controls.Add(this.panelControl11);
            this.panelControl2.Controls.Add(this.txtBarcode);
            this.panelControl2.Controls.Add(this.panelControl12);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Controls.Add(this.panelControl6);
            this.panelControl2.Controls.Add(this.btnNewCount);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Padding = new System.Windows.Forms.Padding(3);
            this.panelControl2.Size = new System.Drawing.Size(227, 459);
            this.panelControl2.TabIndex = 0;
            // 
            // ptbProduct
            // 
            this.ptbProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.ptbProduct.Location = new System.Drawing.Point(13, 118);
            this.ptbProduct.Name = "ptbProduct";
            this.ptbProduct.Size = new System.Drawing.Size(201, 201);
            this.ptbProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbProduct.TabIndex = 12;
            this.ptbProduct.TabStop = false;
            // 
            // panelControl5
            // 
            this.panelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl5.Location = new System.Drawing.Point(214, 118);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(10, 302);
            this.panelControl5.TabIndex = 15;
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl4.Location = new System.Drawing.Point(3, 118);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(10, 302);
            this.panelControl4.TabIndex = 14;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(3, 113);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(221, 5);
            this.panelControl1.TabIndex = 13;
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBarControl1.Location = new System.Drawing.Point(3, 84);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Properties.ShowTitle = true;
            this.progressBarControl1.Size = new System.Drawing.Size(221, 29);
            this.progressBarControl1.TabIndex = 11;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(3, 79);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(221, 5);
            this.panelControl3.TabIndex = 10;
            // 
            // lblStatus
            // 
            this.lblStatus.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblStatus.Appearance.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblStatus.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatus.Location = new System.Drawing.Point(3, 54);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(221, 25);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "ไม่พบข้อมูลสินค้าชิ้นนี้";
            this.lblStatus.Visible = false;
            // 
            // panelControl11
            // 
            this.panelControl11.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl11.Location = new System.Drawing.Point(3, 49);
            this.panelControl11.Name = "panelControl11";
            this.panelControl11.Size = new System.Drawing.Size(221, 5);
            this.panelControl11.TabIndex = 7;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtBarcode.EditValue = "";
            this.txtBarcode.Location = new System.Drawing.Point(3, 21);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Properties.Appearance.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.txtBarcode.Properties.Appearance.Options.UseFont = true;
            this.txtBarcode.Properties.Appearance.Options.UseTextOptions = true;
            this.txtBarcode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtBarcode.Size = new System.Drawing.Size(221, 28);
            this.txtBarcode.TabIndex = 1;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            // 
            // panelControl12
            // 
            this.panelControl12.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl12.Location = new System.Drawing.Point(3, 16);
            this.panelControl12.Name = "panelControl12";
            this.panelControl12.Size = new System.Drawing.Size(221, 5);
            this.panelControl12.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.DarkGray;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControl3.Location = new System.Drawing.Point(3, 3);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(221, 13);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "บาร์โค้ด";
            // 
            // panelControl6
            // 
            this.panelControl6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl6.Location = new System.Drawing.Point(3, 420);
            this.panelControl6.Name = "panelControl6";
            this.panelControl6.Size = new System.Drawing.Size(221, 5);
            this.panelControl6.TabIndex = 17;
            // 
            // btnNewCount
            // 
            this.btnNewCount.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewCount.Appearance.Options.UseFont = true;
            this.btnNewCount.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnNewCount.Location = new System.Drawing.Point(3, 425);
            this.btnNewCount.Name = "btnNewCount";
            this.btnNewCount.Size = new System.Drawing.Size(221, 31);
            this.btnNewCount.TabIndex = 16;
            this.btnNewCount.Text = "เริ่มนับสต็อคใหม่";
            this.btnNewCount.Click += new System.EventHandler(this.btnNewCount_Click);
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
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 227;
            this.navBarControl1.OptionsNavPane.ShowOverflowPanel = false;
            this.navBarControl1.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.navBarControl1.Size = new System.Drawing.Size(227, 545);
            this.navBarControl1.TabIndex = 2;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // panelControl7
            // 
            this.panelControl7.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl7.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl7.Location = new System.Drawing.Point(974, 0);
            this.panelControl7.Name = "panelControl7";
            this.panelControl7.Size = new System.Drawing.Size(10, 545);
            this.panelControl7.TabIndex = 7;
            // 
            // UcStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.stockGridControl);
            this.Controls.Add(this.panelControl7);
            this.Controls.Add(this.navBarControl1);
            this.Name = "UcStock";
            this.Size = new System.Drawing.Size(984, 545);
            this.Load += new System.EventHandler(this.UcStock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.stockGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockGridView)).EndInit();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ptbProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBarcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl stockGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView stockGridView;
        private DevExpress.XtraGrid.Columns.GridColumn clNo;
        private DevExpress.XtraGrid.Columns.GridColumn clProduct;
        private DevExpress.XtraGrid.Columns.GridColumn clName;
        private DevExpress.XtraGrid.Columns.GridColumn clQty;
        private DevExpress.XtraGrid.Columns.GridColumn clCheck;
        private DevExpress.XtraGrid.Columns.GridColumn clProgress;
        private DevExpress.XtraGrid.Columns.GridColumn clCategory;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraEditors.PanelControl panelControl11;
        private DevExpress.XtraEditors.TextEdit txtBarcode;
        private DevExpress.XtraEditors.PanelControl panelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private System.Windows.Forms.PictureBox ptbProduct;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn clSku;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl6;
        private DevExpress.XtraEditors.SimpleButton btnNewCount;
        private DevExpress.XtraEditors.PanelControl panelControl7;
    }
}
