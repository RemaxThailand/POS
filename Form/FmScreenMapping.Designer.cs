namespace PowerPOS
{
    partial class FmScreenMapping
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FmScreenMapping));
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.screenGridControl = new DevExpress.XtraGrid.GridControl();
            this.mainScreenGridview = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.employeeControl = new DevExpress.XtraGrid.GridControl();
            this.employeeGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.subScreenGridControl = new DevExpress.XtraGrid.GridControl();
            this.subScreenGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.screenGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainScreenGridview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.employeeControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.employeeGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subScreenGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subScreenGridView)).BeginInit();
            this.SuspendLayout();
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
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 180;
            this.navBarControl1.OptionsNavPane.ShowOverflowPanel = false;
            this.navBarControl1.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.navBarControl1.Size = new System.Drawing.Size(180, 454);
            this.navBarControl1.TabIndex = 10;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.navBarGroup1.Appearance.Options.UseFont = true;
            this.navBarGroup1.Caption = "หน้าจอหลัก";
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
            this.navBarGroupControlContainer2.Controls.Add(this.panelControl1);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(180, 368);
            this.navBarGroupControlContainer2.TabIndex = 1;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.panelControl1.Appearance.Options.UseFont = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.screenGridControl);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Padding = new System.Windows.Forms.Padding(3);
            this.panelControl1.Size = new System.Drawing.Size(180, 368);
            this.panelControl1.TabIndex = 0;
            // 
            // screenGridControl
            // 
            this.screenGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.screenGridControl.Location = new System.Drawing.Point(3, 3);
            this.screenGridControl.MainView = this.mainScreenGridview;
            this.screenGridControl.Name = "screenGridControl";
            this.screenGridControl.Size = new System.Drawing.Size(174, 362);
            this.screenGridControl.TabIndex = 7;
            this.screenGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.mainScreenGridview});
            // 
            // mainScreenGridview
            // 
            this.mainScreenGridview.ColumnPanelRowHeight = 24;
            this.mainScreenGridview.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.clName});
            this.mainScreenGridview.GridControl = this.screenGridControl;
            this.mainScreenGridview.Name = "mainScreenGridview";
            this.mainScreenGridview.OptionsView.ShowGroupPanel = false;
            this.mainScreenGridview.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.mainScreenGridview_FocusedRowChanged);
            this.mainScreenGridview.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.mainScreenGridview_CellValueChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = " ";
            this.gridColumn1.FieldName = "canView";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 20;
            // 
            // clName
            // 
            this.clName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clName.AppearanceCell.Options.UseFont = true;
            this.clName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clName.AppearanceHeader.Options.UseFont = true;
            this.clName.AppearanceHeader.Options.UseTextOptions = true;
            this.clName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clName.Caption = "หน้าจอหลัก";
            this.clName.FieldName = "name";
            this.clName.Name = "clName";
            this.clName.OptionsColumn.AllowEdit = false;
            this.clName.OptionsColumn.AllowMove = false;
            this.clName.Visible = true;
            this.clName.VisibleIndex = 1;
            this.clName.Width = 115;
            // 
            // employeeControl
            // 
            this.employeeControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.employeeControl.Location = new System.Drawing.Point(354, 0);
            this.employeeControl.MainView = this.employeeGridView;
            this.employeeControl.Name = "employeeControl";
            this.employeeControl.Size = new System.Drawing.Size(630, 454);
            this.employeeControl.TabIndex = 11;
            this.employeeControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.employeeGridView});
            // 
            // employeeGridView
            // 
            this.employeeGridView.ColumnPanelRowHeight = 24;
            this.employeeGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn3});
            this.employeeGridView.GridControl = this.employeeControl;
            this.employeeGridView.Name = "employeeGridView";
            this.employeeGridView.OptionsView.ShowGroupPanel = false;
            this.employeeGridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.employeeGridView_CellValueChanged);
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = " ";
            this.gridColumn2.FieldName = "canDo";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 20;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn3.AppearanceCell.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "สิทธิ์การใช้งาน";
            this.gridColumn3.FieldName = "description";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowMove = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 628;
            // 
            // subScreenGridControl
            // 
            this.subScreenGridControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.subScreenGridControl.Location = new System.Drawing.Point(180, 0);
            this.subScreenGridControl.MainView = this.subScreenGridView;
            this.subScreenGridControl.Name = "subScreenGridControl";
            this.subScreenGridControl.Size = new System.Drawing.Size(174, 454);
            this.subScreenGridControl.TabIndex = 12;
            this.subScreenGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.subScreenGridView});
            // 
            // subScreenGridView
            // 
            this.subScreenGridView.ColumnPanelRowHeight = 24;
            this.subScreenGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn5});
            this.subScreenGridView.GridControl = this.subScreenGridControl;
            this.subScreenGridView.Name = "subScreenGridView";
            this.subScreenGridView.OptionsView.ShowGroupPanel = false;
            this.subScreenGridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.subScreenGridView_FocusedRowChanged);
            this.subScreenGridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.subScreenGridView_CellValueChanged);
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = " ";
            this.gridColumn4.FieldName = "canView";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 20;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn5.AppearanceCell.Options.UseFont = true;
            this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn5.AppearanceHeader.Options.UseFont = true;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "หน้าจอย่อย";
            this.gridColumn5.FieldName = "name";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowMove = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 115;
            // 
            // FmScreenMapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 454);
            this.Controls.Add(this.employeeControl);
            this.Controls.Add(this.subScreenGridControl);
            this.Controls.Add(this.navBarControl1);
            this.Name = "FmScreenMapping";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "กำหนดสิทธิ์การใช้งานหน้าจอในระบบ";
            this.Load += new System.EventHandler(this.FmScreenMapping_Load);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.screenGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainScreenGridview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.employeeControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.employeeGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subScreenGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subScreenGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl screenGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView mainScreenGridview;
        private DevExpress.XtraGrid.Columns.GridColumn clName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.GridControl employeeControl;
        private DevExpress.XtraGrid.Views.Grid.GridView employeeGridView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.GridControl subScreenGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView subScreenGridView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
    }
}