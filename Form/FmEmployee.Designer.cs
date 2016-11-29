namespace PowerPOS
{
    partial class FmEmployee
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FmEmployee));
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.employeeGroupGridControl = new DevExpress.XtraGrid.GridControl();
            this.employeeGroupGridview = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnDeleteEmployeeType = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddEmployeeType = new DevExpress.XtraEditors.SimpleButton();
            this.employeeGridControl = new DevExpress.XtraGrid.GridControl();
            this.employeeGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clFirstname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clLastname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clNickname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clMobile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clUsername = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clPassword = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.btnScreen = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.employeeGroupGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.employeeGroupGridview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.employeeGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.employeeGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
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
            this.navBarControl1.Size = new System.Drawing.Size(180, 449);
            this.navBarControl1.TabIndex = 9;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.navBarGroup1.Appearance.Options.UseFont = true;
            this.navBarGroup1.Caption = "ข้อมูลกลุ่มผู้ใช้งาน";
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
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(180, 363);
            this.navBarGroupControlContainer2.TabIndex = 1;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.panelControl1.Appearance.Options.UseFont = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.employeeGroupGridControl);
            this.panelControl1.Controls.Add(this.panelControl3);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Padding = new System.Windows.Forms.Padding(3);
            this.panelControl1.Size = new System.Drawing.Size(180, 363);
            this.panelControl1.TabIndex = 0;
            // 
            // employeeGroupGridControl
            // 
            this.employeeGroupGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.employeeGroupGridControl.Location = new System.Drawing.Point(3, 3);
            this.employeeGroupGridControl.MainView = this.employeeGroupGridview;
            this.employeeGroupGridControl.Name = "employeeGroupGridControl";
            this.employeeGroupGridControl.Size = new System.Drawing.Size(174, 322);
            this.employeeGroupGridControl.TabIndex = 7;
            this.employeeGroupGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.employeeGroupGridview});
            // 
            // employeeGroupGridview
            // 
            this.employeeGroupGridview.ColumnPanelRowHeight = 24;
            this.employeeGroupGridview.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clName});
            this.employeeGroupGridview.GridControl = this.employeeGroupGridControl;
            this.employeeGroupGridview.Name = "employeeGroupGridview";
            this.employeeGroupGridview.OptionsView.ShowGroupPanel = false;
            this.employeeGroupGridview.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.employeeGroupGridview_FocusedRowChanged);
            this.employeeGroupGridview.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.employeeGroupGridview_CellValueChanged);
            // 
            // clName
            // 
            this.clName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clName.AppearanceCell.Options.UseFont = true;
            this.clName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clName.AppearanceHeader.Options.UseFont = true;
            this.clName.AppearanceHeader.Options.UseTextOptions = true;
            this.clName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clName.Caption = "ชื่อกลุ่มผู้ใช้งาน";
            this.clName.FieldName = "name";
            this.clName.Name = "clName";
            this.clName.OptionsColumn.AllowMove = false;
            this.clName.Visible = true;
            this.clName.VisibleIndex = 0;
            this.clName.Width = 50;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(3, 325);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(174, 5);
            this.panelControl3.TabIndex = 20;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.btnDeleteEmployeeType);
            this.panelControl2.Controls.Add(this.btnAddEmployeeType);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(3, 330);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(174, 30);
            this.panelControl2.TabIndex = 19;
            // 
            // btnDeleteEmployeeType
            // 
            this.btnDeleteEmployeeType.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteEmployeeType.Appearance.Options.UseFont = true;
            this.btnDeleteEmployeeType.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDeleteEmployeeType.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteEmployeeType.Image")));
            this.btnDeleteEmployeeType.Location = new System.Drawing.Point(0, 0);
            this.btnDeleteEmployeeType.Name = "btnDeleteEmployeeType";
            this.btnDeleteEmployeeType.Size = new System.Drawing.Size(81, 30);
            this.btnDeleteEmployeeType.TabIndex = 20;
            this.btnDeleteEmployeeType.Text = "ลบข้อมูล";
            this.btnDeleteEmployeeType.Click += new System.EventHandler(this.btnDeleteEmployee_Click);
            // 
            // btnAddEmployeeType
            // 
            this.btnAddEmployeeType.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddEmployeeType.Appearance.Options.UseFont = true;
            this.btnAddEmployeeType.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAddEmployeeType.Image = ((System.Drawing.Image)(resources.GetObject("btnAddEmployeeType.Image")));
            this.btnAddEmployeeType.Location = new System.Drawing.Point(87, 0);
            this.btnAddEmployeeType.Name = "btnAddEmployeeType";
            this.btnAddEmployeeType.Size = new System.Drawing.Size(87, 30);
            this.btnAddEmployeeType.TabIndex = 19;
            this.btnAddEmployeeType.Text = "เพิ่มข้อมูล";
            this.btnAddEmployeeType.Click += new System.EventHandler(this.btnAddEmployee_Click);
            // 
            // employeeGridControl
            // 
            this.employeeGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.employeeGridControl.Location = new System.Drawing.Point(180, 0);
            this.employeeGridControl.MainView = this.employeeGridView;
            this.employeeGridControl.Name = "employeeGridControl";
            this.employeeGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.employeeGridControl.Size = new System.Drawing.Size(799, 414);
            this.employeeGridControl.TabIndex = 21;
            this.employeeGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.employeeGridView});
            // 
            // employeeGridView
            // 
            this.employeeGridView.ColumnPanelRowHeight = 24;
            this.employeeGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clFirstname,
            this.clLastname,
            this.clNickname,
            this.clMobile,
            this.clUsername,
            this.clPassword,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7});
            this.employeeGridView.GridControl = this.employeeGridControl;
            this.employeeGridView.Name = "employeeGridView";
            this.employeeGridView.OptionsView.ShowGroupPanel = false;
            this.employeeGridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.employeeGridView_CellValueChanged);
            // 
            // clFirstname
            // 
            this.clFirstname.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clFirstname.AppearanceCell.Options.UseFont = true;
            this.clFirstname.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clFirstname.AppearanceHeader.Options.UseFont = true;
            this.clFirstname.AppearanceHeader.Options.UseTextOptions = true;
            this.clFirstname.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clFirstname.Caption = "ชื่อ";
            this.clFirstname.FieldName = "firstname";
            this.clFirstname.Name = "clFirstname";
            this.clFirstname.OptionsColumn.AllowMove = false;
            this.clFirstname.Visible = true;
            this.clFirstname.VisibleIndex = 0;
            this.clFirstname.Width = 95;
            // 
            // clLastname
            // 
            this.clLastname.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clLastname.AppearanceCell.Options.UseFont = true;
            this.clLastname.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clLastname.AppearanceHeader.Options.UseFont = true;
            this.clLastname.AppearanceHeader.Options.UseTextOptions = true;
            this.clLastname.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clLastname.Caption = "นามสกุล";
            this.clLastname.FieldName = "lastname";
            this.clLastname.Name = "clLastname";
            this.clLastname.Visible = true;
            this.clLastname.VisibleIndex = 1;
            this.clLastname.Width = 95;
            // 
            // clNickname
            // 
            this.clNickname.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clNickname.AppearanceCell.Options.UseFont = true;
            this.clNickname.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clNickname.AppearanceHeader.Options.UseFont = true;
            this.clNickname.AppearanceHeader.Options.UseTextOptions = true;
            this.clNickname.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clNickname.Caption = "ชื่อเล่น";
            this.clNickname.FieldName = "nickname";
            this.clNickname.Name = "clNickname";
            this.clNickname.Visible = true;
            this.clNickname.VisibleIndex = 2;
            this.clNickname.Width = 47;
            // 
            // clMobile
            // 
            this.clMobile.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clMobile.AppearanceCell.Options.UseFont = true;
            this.clMobile.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clMobile.AppearanceHeader.Options.UseFont = true;
            this.clMobile.AppearanceHeader.Options.UseTextOptions = true;
            this.clMobile.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clMobile.Caption = "โทรศัพท์";
            this.clMobile.FieldName = "mobile";
            this.clMobile.Name = "clMobile";
            this.clMobile.Visible = true;
            this.clMobile.VisibleIndex = 3;
            this.clMobile.Width = 91;
            // 
            // clUsername
            // 
            this.clUsername.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clUsername.AppearanceCell.Options.UseFont = true;
            this.clUsername.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clUsername.AppearanceHeader.Options.UseFont = true;
            this.clUsername.AppearanceHeader.Options.UseTextOptions = true;
            this.clUsername.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clUsername.Caption = "ชื่อผู้ใช้";
            this.clUsername.FieldName = "username";
            this.clUsername.Name = "clUsername";
            this.clUsername.Visible = true;
            this.clUsername.VisibleIndex = 4;
            this.clUsername.Width = 97;
            // 
            // clPassword
            // 
            this.clPassword.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clPassword.AppearanceCell.Options.UseFont = true;
            this.clPassword.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clPassword.AppearanceHeader.Options.UseFont = true;
            this.clPassword.AppearanceHeader.Options.UseTextOptions = true;
            this.clPassword.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clPassword.Caption = "รหัสผ่าน";
            this.clPassword.ColumnEdit = this.repositoryItemTextEdit1;
            this.clPassword.FieldName = "passtmp";
            this.clPassword.Name = "clPassword";
            this.clPassword.Visible = true;
            this.clPassword.VisibleIndex = 5;
            this.clPassword.Width = 100;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.PasswordChar = '*';
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn5.AppearanceCell.Options.UseFont = true;
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn5.AppearanceHeader.Options.UseFont = true;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "เพิ่มข้อมูล";
            this.gridColumn5.DisplayFormat.FormatString = "dd/MM/yy";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn5.FieldName = "addDate";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 6;
            this.gridColumn5.Width = 66;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn6.AppearanceCell.Options.UseFont = true;
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn6.AppearanceHeader.Options.UseFont = true;
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "เข้าระบบล่าสุด";
            this.gridColumn6.DisplayFormat.FormatString = "dd/MM/yy";
            this.gridColumn6.FieldName = "loginDate";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 7;
            this.gridColumn6.Width = 85;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn7.AppearanceCell.Options.UseFont = true;
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn7.AppearanceHeader.Options.UseFont = true;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "จำนวนครั้ง";
            this.gridColumn7.FieldName = "loginCount";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 8;
            this.gridColumn7.Width = 76;
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl4.Location = new System.Drawing.Point(180, 414);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(799, 5);
            this.panelControl4.TabIndex = 23;
            // 
            // panelControl5
            // 
            this.panelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl5.Controls.Add(this.btnScreen);
            this.panelControl5.Controls.Add(this.btnDelete);
            this.panelControl5.Controls.Add(this.panelControl6);
            this.panelControl5.Controls.Add(this.btnAdd);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl5.Location = new System.Drawing.Point(180, 419);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(799, 30);
            this.panelControl5.TabIndex = 22;
            // 
            // btnScreen
            // 
            this.btnScreen.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScreen.Appearance.Options.UseFont = true;
            this.btnScreen.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnScreen.Image = ((System.Drawing.Image)(resources.GetObject("btnScreen.Image")));
            this.btnScreen.Location = new System.Drawing.Point(0, 0);
            this.btnScreen.Name = "btnScreen";
            this.btnScreen.Size = new System.Drawing.Size(188, 30);
            this.btnScreen.TabIndex = 22;
            this.btnScreen.Text = "กำหนดสิทธิ์การใช้งานหน้าจอ";
            this.btnScreen.Click += new System.EventHandler(this.btnScreen_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(623, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(81, 30);
            this.btnDelete.TabIndex = 20;
            this.btnDelete.Text = "ลบข้อมูล";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // panelControl6
            // 
            this.panelControl6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl6.Location = new System.Drawing.Point(704, 0);
            this.panelControl6.Name = "panelControl6";
            this.panelControl6.Size = new System.Drawing.Size(8, 30);
            this.panelControl6.TabIndex = 21;
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Appearance.Options.UseFont = true;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(712, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(87, 30);
            this.btnAdd.TabIndex = 19;
            this.btnAdd.Text = "เพิ่มข้อมูล";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // FmEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 454);
            this.Controls.Add(this.employeeGridControl);
            this.Controls.Add(this.panelControl4);
            this.Controls.Add(this.panelControl5);
            this.Controls.Add(this.navBarControl1);
            this.Name = "FmEmployee";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 5, 5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "จัดการข้อมูลผู้ใช้งานในระบบ";
            this.Load += new System.EventHandler(this.FmEmployee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.employeeGroupGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.employeeGroupGridview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.employeeGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.employeeGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl employeeGroupGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView employeeGroupGridview;
        private DevExpress.XtraGrid.Columns.GridColumn clName;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnDeleteEmployeeType;
        private DevExpress.XtraEditors.SimpleButton btnAddEmployeeType;
        private DevExpress.XtraGrid.GridControl employeeGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView employeeGridView;
        private DevExpress.XtraGrid.Columns.GridColumn clFirstname;
        private DevExpress.XtraGrid.Columns.GridColumn clNickname;
        private DevExpress.XtraGrid.Columns.GridColumn clMobile;
        private DevExpress.XtraGrid.Columns.GridColumn clUsername;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.PanelControl panelControl6;
        private DevExpress.XtraGrid.Columns.GridColumn clLastname;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn clPassword;
        private DevExpress.XtraEditors.SimpleButton btnScreen;
    }
}