namespace PowerPOS
{
    partial class FmSelectCustomer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FmSelectCustomer));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.customerGridControl = new DevExpress.XtraGrid.GridControl();
            this.customerGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clfirstname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clLastname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clNickname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clCardNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clMobile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clCitizen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clSellPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clBirthday = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clSex = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(795, 49);
            this.panelControl1.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.txtSearch);
            this.panelControl2.Controls.Add(this.btnSearch);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(557, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(238, 49);
            this.panelControl2.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(14, 13);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Properties.Appearance.Options.UseFont = true;
            this.txtSearch.Size = new System.Drawing.Size(180, 22);
            this.txtSearch.TabIndex = 13;
            this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
            // 
            // btnSearch
            // 
            this.btnSearch.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(199, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(25, 23);
            this.btnSearch.TabIndex = 12;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // customerGridControl
            // 
            this.customerGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customerGridControl.Location = new System.Drawing.Point(0, 49);
            this.customerGridControl.MainView = this.customerGridView;
            this.customerGridControl.Name = "customerGridControl";
            this.customerGridControl.Size = new System.Drawing.Size(795, 338);
            this.customerGridControl.TabIndex = 1;
            this.customerGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.customerGridView});
            this.customerGridControl.DoubleClick += new System.EventHandler(this.customerGridControl_DoubleClick);
            // 
            // customerGridView
            // 
            this.customerGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clNo,
            this.clID,
            this.clfirstname,
            this.clLastname,
            this.clNickname,
            this.clCardNo,
            this.clMobile,
            this.clCitizen,
            this.clSellPrice,
            this.clBirthday,
            this.clSex});
            this.customerGridView.GridControl = this.customerGridControl;
            this.customerGridView.Name = "customerGridView";
            this.customerGridView.OptionsView.ShowGroupPanel = false;
            // 
            // clNo
            // 
            this.clNo.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clNo.AppearanceCell.Options.UseFont = true;
            this.clNo.AppearanceCell.Options.UseTextOptions = true;
            this.clNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
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
            this.clNo.Width = 35;
            // 
            // clID
            // 
            this.clID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clID.AppearanceCell.Options.UseFont = true;
            this.clID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clID.AppearanceHeader.Options.UseFont = true;
            this.clID.AppearanceHeader.Options.UseTextOptions = true;
            this.clID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clID.Caption = "รหัสลูกค้า";
            this.clID.FieldName = "ID";
            this.clID.Name = "clID";
            // 
            // clfirstname
            // 
            this.clfirstname.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clfirstname.AppearanceCell.Options.UseFont = true;
            this.clfirstname.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clfirstname.AppearanceHeader.Options.UseFont = true;
            this.clfirstname.AppearanceHeader.Options.UseTextOptions = true;
            this.clfirstname.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clfirstname.Caption = "ชื่อ";
            this.clfirstname.FieldName = "Firstname";
            this.clfirstname.Name = "clfirstname";
            this.clfirstname.OptionsColumn.AllowEdit = false;
            this.clfirstname.OptionsColumn.AllowMove = false;
            this.clfirstname.OptionsColumn.FixedWidth = true;
            this.clfirstname.Visible = true;
            this.clfirstname.VisibleIndex = 1;
            this.clfirstname.Width = 100;
            // 
            // clLastname
            // 
            this.clLastname.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clLastname.AppearanceCell.Options.UseFont = true;
            this.clLastname.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clLastname.AppearanceHeader.Options.UseFont = true;
            this.clLastname.AppearanceHeader.Options.UseTextOptions = true;
            this.clLastname.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clLastname.Caption = "นามสกุล";
            this.clLastname.FieldName = "Lastname";
            this.clLastname.Name = "clLastname";
            this.clLastname.OptionsColumn.AllowEdit = false;
            this.clLastname.OptionsColumn.AllowMove = false;
            this.clLastname.OptionsColumn.FixedWidth = true;
            this.clLastname.Visible = true;
            this.clLastname.VisibleIndex = 2;
            this.clLastname.Width = 99;
            // 
            // clNickname
            // 
            this.clNickname.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clNickname.AppearanceCell.Options.UseFont = true;
            this.clNickname.AppearanceCell.Options.UseTextOptions = true;
            this.clNickname.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clNickname.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clNickname.AppearanceHeader.Options.UseFont = true;
            this.clNickname.AppearanceHeader.Options.UseTextOptions = true;
            this.clNickname.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clNickname.Caption = "ชื่อเล่น";
            this.clNickname.FieldName = "Nickname";
            this.clNickname.Name = "clNickname";
            this.clNickname.OptionsColumn.AllowEdit = false;
            this.clNickname.OptionsColumn.AllowMove = false;
            this.clNickname.OptionsColumn.FixedWidth = true;
            this.clNickname.Visible = true;
            this.clNickname.VisibleIndex = 3;
            this.clNickname.Width = 92;
            // 
            // clCardNo
            // 
            this.clCardNo.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clCardNo.AppearanceCell.Options.UseFont = true;
            this.clCardNo.AppearanceCell.Options.UseTextOptions = true;
            this.clCardNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clCardNo.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clCardNo.AppearanceHeader.Options.UseFont = true;
            this.clCardNo.AppearanceHeader.Options.UseTextOptions = true;
            this.clCardNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clCardNo.Caption = "เลขบัตรสมาชิก";
            this.clCardNo.FieldName = "CardNo";
            this.clCardNo.Name = "clCardNo";
            this.clCardNo.OptionsColumn.AllowEdit = false;
            this.clCardNo.OptionsColumn.AllowMove = false;
            this.clCardNo.OptionsColumn.FixedWidth = true;
            this.clCardNo.Visible = true;
            this.clCardNo.VisibleIndex = 4;
            this.clCardNo.Width = 83;
            // 
            // clMobile
            // 
            this.clMobile.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clMobile.AppearanceCell.Options.UseFont = true;
            this.clMobile.AppearanceCell.Options.UseTextOptions = true;
            this.clMobile.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clMobile.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clMobile.AppearanceHeader.Options.UseFont = true;
            this.clMobile.AppearanceHeader.Options.UseTextOptions = true;
            this.clMobile.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clMobile.Caption = "มือถือ";
            this.clMobile.FieldName = "Mobile";
            this.clMobile.Name = "clMobile";
            this.clMobile.OptionsColumn.AllowEdit = false;
            this.clMobile.OptionsColumn.AllowMove = false;
            this.clMobile.OptionsColumn.FixedWidth = true;
            this.clMobile.Visible = true;
            this.clMobile.VisibleIndex = 5;
            this.clMobile.Width = 83;
            // 
            // clCitizen
            // 
            this.clCitizen.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clCitizen.AppearanceCell.Options.UseFont = true;
            this.clCitizen.AppearanceCell.Options.UseTextOptions = true;
            this.clCitizen.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clCitizen.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clCitizen.AppearanceHeader.Options.UseFont = true;
            this.clCitizen.AppearanceHeader.Options.UseTextOptions = true;
            this.clCitizen.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clCitizen.Caption = "เลขประจำตัวประชาชน";
            this.clCitizen.FieldName = "Citizen";
            this.clCitizen.Name = "clCitizen";
            this.clCitizen.OptionsColumn.AllowEdit = false;
            this.clCitizen.OptionsColumn.AllowMove = false;
            this.clCitizen.OptionsColumn.FixedWidth = true;
            this.clCitizen.Visible = true;
            this.clCitizen.VisibleIndex = 6;
            this.clCitizen.Width = 103;
            // 
            // clSellPrice
            // 
            this.clSellPrice.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clSellPrice.AppearanceCell.Options.UseFont = true;
            this.clSellPrice.AppearanceCell.Options.UseTextOptions = true;
            this.clSellPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clSellPrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clSellPrice.AppearanceHeader.Options.UseFont = true;
            this.clSellPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.clSellPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clSellPrice.Caption = "ราคาขาย";
            this.clSellPrice.FieldName = "SellPrice";
            this.clSellPrice.Name = "clSellPrice";
            this.clSellPrice.OptionsColumn.AllowEdit = false;
            this.clSellPrice.OptionsColumn.AllowMove = false;
            this.clSellPrice.OptionsColumn.FixedWidth = true;
            this.clSellPrice.Width = 70;
            // 
            // clBirthday
            // 
            this.clBirthday.Caption = "วันเกิด";
            this.clBirthday.FieldName = "Birthday";
            this.clBirthday.Name = "clBirthday";
            // 
            // clSex
            // 
            this.clSex.Caption = "เพศ";
            this.clSex.FieldName = "Sex";
            this.clSex.Name = "clSex";
            // 
            // FmSelectCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 387);
            this.Controls.Add(this.customerGridControl);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FmSelectCustomer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ค้นหาข้อมูลลูกค้า";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraGrid.GridControl customerGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView customerGridView;
        private DevExpress.XtraGrid.Columns.GridColumn clNo;
        private DevExpress.XtraGrid.Columns.GridColumn clfirstname;
        private DevExpress.XtraGrid.Columns.GridColumn clLastname;
        private DevExpress.XtraGrid.Columns.GridColumn clNickname;
        private DevExpress.XtraGrid.Columns.GridColumn clCardNo;
        private DevExpress.XtraGrid.Columns.GridColumn clMobile;
        private DevExpress.XtraGrid.Columns.GridColumn clCitizen;
        private DevExpress.XtraGrid.Columns.GridColumn clSellPrice;
        private DevExpress.XtraGrid.Columns.GridColumn clID;
        private DevExpress.XtraGrid.Columns.GridColumn clBirthday;
        private DevExpress.XtraGrid.Columns.GridColumn clSex;
    }
}