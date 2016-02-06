namespace PowerPOS
{
    partial class FmReturnSell
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FmReturnSell));
            this.clAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clCustomer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.clDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clSellNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.saleGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clSellPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clCost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clPriceCost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.saleGridControl = new DevExpress.XtraGrid.GridControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.saleGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saleGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // clAmount
            // 
            this.clAmount.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clAmount.AppearanceCell.Options.UseFont = true;
            this.clAmount.AppearanceCell.Options.UseTextOptions = true;
            this.clAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.clAmount.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clAmount.AppearanceHeader.Options.UseFont = true;
            this.clAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.clAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clAmount.Caption = "จำนวน";
            this.clAmount.FieldName = "Amount";
            this.clAmount.Name = "clAmount";
            this.clAmount.OptionsColumn.AllowEdit = false;
            this.clAmount.OptionsColumn.AllowMove = false;
            this.clAmount.OptionsColumn.FixedWidth = true;
            this.clAmount.Visible = true;
            this.clAmount.VisibleIndex = 5;
            // 
            // clCustomer
            // 
            this.clCustomer.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clCustomer.AppearanceCell.Options.UseFont = true;
            this.clCustomer.AppearanceCell.Options.UseTextOptions = true;
            this.clCustomer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.clCustomer.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clCustomer.AppearanceHeader.Options.UseFont = true;
            this.clCustomer.AppearanceHeader.Options.UseTextOptions = true;
            this.clCustomer.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clCustomer.Caption = "ลูกค้า";
            this.clCustomer.FieldName = "Customer";
            this.clCustomer.Name = "clCustomer";
            this.clCustomer.OptionsColumn.AllowEdit = false;
            this.clCustomer.OptionsColumn.AllowMove = false;
            this.clCustomer.OptionsColumn.FixedWidth = true;
            this.clCustomer.Visible = true;
            this.clCustomer.VisibleIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 19);
            this.label1.TabIndex = 8;
            this.label1.Text = "รหัสสินค้า :";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(270, 18);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 19);
            this.lblName.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(186, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 19);
            this.label2.TabIndex = 9;
            this.label2.Text = "ชื่อสินค้า :";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(97, 17);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(0, 19);
            this.lblID.TabIndex = 11;
            // 
            // clDate
            // 
            this.clDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clDate.AppearanceCell.Options.UseFont = true;
            this.clDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clDate.AppearanceHeader.Options.UseFont = true;
            this.clDate.AppearanceHeader.Options.UseTextOptions = true;
            this.clDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clDate.Caption = "วันที่ขาย";
            this.clDate.FieldName = "sellDate";
            this.clDate.Name = "clDate";
            this.clDate.OptionsColumn.AllowEdit = false;
            this.clDate.OptionsColumn.AllowMove = false;
            this.clDate.OptionsColumn.FixedWidth = true;
            this.clDate.Visible = true;
            this.clDate.VisibleIndex = 2;
            this.clDate.Width = 200;
            // 
            // clSellNo
            // 
            this.clSellNo.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clSellNo.AppearanceCell.Options.UseFont = true;
            this.clSellNo.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clSellNo.AppearanceHeader.Options.UseFont = true;
            this.clSellNo.AppearanceHeader.Options.UseTextOptions = true;
            this.clSellNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clSellNo.Caption = "เลขที่ขาย";
            this.clSellNo.FieldName = "sellNo";
            this.clSellNo.Name = "clSellNo";
            this.clSellNo.OptionsColumn.AllowEdit = false;
            this.clSellNo.OptionsColumn.AllowMove = false;
            this.clSellNo.OptionsColumn.FixedWidth = true;
            this.clSellNo.Visible = true;
            this.clSellNo.VisibleIndex = 1;
            this.clSellNo.Width = 80;
            // 
            // clNo
            // 
            this.clNo.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clNo.AppearanceCell.Options.UseFont = true;
            this.clNo.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
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
            this.clNo.Width = 40;
            // 
            // saleGridView
            // 
            this.saleGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clNo,
            this.clSellNo,
            this.clDate,
            this.clCustomer,
            this.clPrice,
            this.clAmount,
            this.clSellPrice,
            this.clCost,
            this.clPriceCost});
            this.saleGridView.GridControl = this.saleGridControl;
            this.saleGridView.Name = "saleGridView";
            this.saleGridView.OptionsView.ShowGroupPanel = false;
            // 
            // clPrice
            // 
            this.clPrice.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clPrice.AppearanceCell.Options.UseFont = true;
            this.clPrice.AppearanceCell.Options.UseTextOptions = true;
            this.clPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clPrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clPrice.AppearanceHeader.Options.UseFont = true;
            this.clPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.clPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clPrice.Caption = "ราคาขาย";
            this.clPrice.FieldName = "Price";
            this.clPrice.Name = "clPrice";
            this.clPrice.OptionsColumn.AllowEdit = false;
            this.clPrice.OptionsColumn.AllowMove = false;
            this.clPrice.OptionsColumn.FixedWidth = true;
            this.clPrice.Visible = true;
            this.clPrice.VisibleIndex = 4;
            // 
            // clSellPrice
            // 
            this.clSellPrice.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clSellPrice.AppearanceCell.Options.UseFont = true;
            this.clSellPrice.AppearanceCell.Options.UseTextOptions = true;
            this.clSellPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.clSellPrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clSellPrice.AppearanceHeader.Options.UseFont = true;
            this.clSellPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.clSellPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clSellPrice.Caption = "ราคาขาย";
            this.clSellPrice.FieldName = "sellPrice";
            this.clSellPrice.Name = "clSellPrice";
            this.clSellPrice.OptionsColumn.AllowEdit = false;
            this.clSellPrice.OptionsColumn.AllowMove = false;
            this.clSellPrice.OptionsColumn.FixedWidth = true;
            this.clSellPrice.Visible = true;
            this.clSellPrice.VisibleIndex = 6;
            // 
            // clCost
            // 
            this.clCost.Caption = "ต้นทุน";
            this.clCost.FieldName = "Cost";
            this.clCost.Name = "clCost";
            // 
            // clPriceCost
            // 
            this.clPriceCost.Caption = "ราคาต้นทุน";
            this.clPriceCost.FieldName = "priceCost";
            this.clPriceCost.Name = "clPriceCost";
            // 
            // saleGridControl
            // 
            this.saleGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saleGridControl.Location = new System.Drawing.Point(0, 61);
            this.saleGridControl.MainView = this.saleGridView;
            this.saleGridControl.Name = "saleGridControl";
            this.saleGridControl.Size = new System.Drawing.Size(727, 352);
            this.saleGridControl.TabIndex = 5;
            this.saleGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.saleGridView});
            this.saleGridControl.Click += new System.EventHandler(this.saleGridControl_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(640, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 31);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 413);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(727, 42);
            this.panelControl2.TabIndex = 4;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.groupBox1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(727, 61);
            this.panelControl1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblID);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(13, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(690, 51);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // FmReturnSell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 455);
            this.Controls.Add(this.saleGridControl);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FmReturnSell";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "เลือกบิลขายที่ต้องการรับคืน";
            this.Load += new System.EventHandler(this.FmReturnSell_Load);
            ((System.ComponentModel.ISupportInitialize)(this.saleGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saleGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn clAmount;
        private DevExpress.XtraGrid.Columns.GridColumn clCustomer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblID;
        private DevExpress.XtraGrid.Columns.GridColumn clDate;
        private DevExpress.XtraGrid.Columns.GridColumn clSellNo;
        private DevExpress.XtraGrid.Columns.GridColumn clNo;
        private DevExpress.XtraGrid.Views.Grid.GridView saleGridView;
        private DevExpress.XtraGrid.Columns.GridColumn clPrice;
        private DevExpress.XtraGrid.GridControl saleGridControl;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraGrid.Columns.GridColumn clSellPrice;
        private DevExpress.XtraGrid.Columns.GridColumn clCost;
        private DevExpress.XtraGrid.Columns.GridColumn clPriceCost;
    }
}