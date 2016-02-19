namespace PowerPOS
{
    partial class FmSaleDetial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FmSaleDetial));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblSellDate = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSellNo = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.saleGridControl = new DevExpress.XtraGrid.GridControl();
            this.saleGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clProduct = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clProductName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clSku = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.saleGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saleGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.groupBox1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(715, 84);
            this.panelControl1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblTotal);
            this.groupBox1.Controls.Add(this.lblSellDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblSellNo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblCustomer);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(13, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(690, 71);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(17, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "เลขที่การขาย :";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblTotal.Location = new System.Drawing.Point(441, 40);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(0, 18);
            this.lblTotal.TabIndex = 15;
            // 
            // lblSellDate
            // 
            this.lblSellDate.AutoSize = true;
            this.lblSellDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblSellDate.Location = new System.Drawing.Point(441, 16);
            this.lblSellDate.Name = "lblSellDate";
            this.lblSellDate.Size = new System.Drawing.Size(0, 18);
            this.lblSellDate.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(372, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 18);
            this.label2.TabIndex = 9;
            this.label2.Text = "วันที่ขาย :";
            // 
            // lblSellNo
            // 
            this.lblSellNo.AutoSize = true;
            this.lblSellNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblSellNo.Location = new System.Drawing.Point(103, 16);
            this.lblSellNo.Name = "lblSellNo";
            this.lblSellNo.Size = new System.Drawing.Size(0, 18);
            this.lblSellNo.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(372, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 18);
            this.label5.TabIndex = 14;
            this.label5.Text = "ยอดรวม :";
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblCustomer.Location = new System.Drawing.Point(68, 40);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(0, 18);
            this.lblCustomer.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(17, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "ลูกค้า :";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 349);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(715, 42);
            this.panelControl2.TabIndex = 1;
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
            // saleGridControl
            // 
            this.saleGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saleGridControl.Location = new System.Drawing.Point(0, 84);
            this.saleGridControl.MainView = this.saleGridView;
            this.saleGridControl.Name = "saleGridControl";
            this.saleGridControl.Size = new System.Drawing.Size(715, 265);
            this.saleGridControl.TabIndex = 2;
            this.saleGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.saleGridView});
            // 
            // saleGridView
            // 
            this.saleGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clNo,
            this.clProduct,
            this.clProductName,
            this.clPrice,
            this.clAmount,
            this.clTotal,
            this.clSku});
            this.saleGridView.GridControl = this.saleGridControl;
            this.saleGridView.Name = "saleGridView";
            this.saleGridView.OptionsView.ShowGroupPanel = false;
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
            this.clNo.OptionsColumn.AllowSize = false;
            this.clNo.OptionsColumn.FixedWidth = true;
            this.clNo.Visible = true;
            this.clNo.VisibleIndex = 0;
            this.clNo.Width = 40;
            // 
            // clProduct
            // 
            this.clProduct.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clProduct.AppearanceCell.Options.UseFont = true;
            this.clProduct.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clProduct.AppearanceHeader.Options.UseFont = true;
            this.clProduct.AppearanceHeader.Options.UseTextOptions = true;
            this.clProduct.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clProduct.Caption = "รหัสสินค้า";
            this.clProduct.FieldName = "Product";
            this.clProduct.Name = "clProduct";
            this.clProduct.OptionsColumn.AllowEdit = false;
            this.clProduct.OptionsColumn.AllowMove = false;
            this.clProduct.OptionsColumn.AllowSize = false;
            this.clProduct.OptionsColumn.FixedWidth = true;
            this.clProduct.Width = 80;
            // 
            // clProductName
            // 
            this.clProductName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clProductName.AppearanceCell.Options.UseFont = true;
            this.clProductName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clProductName.AppearanceHeader.Options.UseFont = true;
            this.clProductName.AppearanceHeader.Options.UseTextOptions = true;
            this.clProductName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clProductName.Caption = "ชื่อสินค้า";
            this.clProductName.FieldName = "ProductName";
            this.clProductName.Name = "clProductName";
            this.clProductName.OptionsColumn.AllowEdit = false;
            this.clProductName.OptionsColumn.AllowMove = false;
            this.clProductName.OptionsColumn.AllowSize = false;
            this.clProductName.OptionsColumn.FixedWidth = true;
            this.clProductName.Visible = true;
            this.clProductName.VisibleIndex = 2;
            this.clProductName.Width = 250;
            // 
            // clPrice
            // 
            this.clPrice.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clPrice.AppearanceCell.Options.UseFont = true;
            this.clPrice.AppearanceCell.Options.UseTextOptions = true;
            this.clPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.clPrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clPrice.AppearanceHeader.Options.UseFont = true;
            this.clPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.clPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clPrice.Caption = "ราคาสินค้า";
            this.clPrice.FieldName = "Price";
            this.clPrice.Name = "clPrice";
            this.clPrice.OptionsColumn.AllowEdit = false;
            this.clPrice.OptionsColumn.AllowMove = false;
            this.clPrice.OptionsColumn.AllowSize = false;
            this.clPrice.OptionsColumn.FixedWidth = true;
            this.clPrice.Visible = true;
            this.clPrice.VisibleIndex = 4;
            // 
            // clAmount
            // 
            this.clAmount.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clAmount.AppearanceCell.Options.UseFont = true;
            this.clAmount.AppearanceCell.Options.UseTextOptions = true;
            this.clAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clAmount.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clAmount.AppearanceHeader.Options.UseFont = true;
            this.clAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.clAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clAmount.Caption = "จำนวน";
            this.clAmount.FieldName = "Amount";
            this.clAmount.Name = "clAmount";
            this.clAmount.OptionsColumn.AllowEdit = false;
            this.clAmount.OptionsColumn.AllowMove = false;
            this.clAmount.OptionsColumn.AllowSize = false;
            this.clAmount.OptionsColumn.FixedWidth = true;
            this.clAmount.Visible = true;
            this.clAmount.VisibleIndex = 5;
            // 
            // clTotal
            // 
            this.clTotal.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clTotal.AppearanceCell.Options.UseFont = true;
            this.clTotal.AppearanceCell.Options.UseTextOptions = true;
            this.clTotal.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.clTotal.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clTotal.AppearanceHeader.Options.UseFont = true;
            this.clTotal.AppearanceHeader.Options.UseTextOptions = true;
            this.clTotal.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clTotal.Caption = "ราคาขาย";
            this.clTotal.FieldName = "Total";
            this.clTotal.Name = "clTotal";
            this.clTotal.OptionsColumn.AllowEdit = false;
            this.clTotal.OptionsColumn.AllowMove = false;
            this.clTotal.OptionsColumn.AllowSize = false;
            this.clTotal.OptionsColumn.FixedWidth = true;
            this.clTotal.Visible = true;
            this.clTotal.VisibleIndex = 6;
            // 
            // clSku
            // 
            this.clSku.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clSku.AppearanceCell.Options.UseFont = true;
            this.clSku.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.clSku.AppearanceHeader.Options.UseFont = true;
            this.clSku.AppearanceHeader.Options.UseTextOptions = true;
            this.clSku.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clSku.Caption = "รหัสสินค้า";
            this.clSku.FieldName = "Sku";
            this.clSku.Name = "clSku";
            this.clSku.OptionsColumn.AllowEdit = false;
            this.clSku.OptionsColumn.AllowMove = false;
            this.clSku.OptionsColumn.AllowSize = false;
            this.clSku.OptionsColumn.FixedWidth = true;
            this.clSku.Visible = true;
            this.clSku.VisibleIndex = 1;
            this.clSku.Width = 80;
            // 
            // FmSaleDetial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 391);
            this.Controls.Add(this.saleGridControl);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FmSaleDetial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "รายละเอียดการขาย";
            this.Load += new System.EventHandler(this.FmSaleDetial_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.saleGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saleGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl saleGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView saleGridView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblSellDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSellNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraGrid.Columns.GridColumn clNo;
        private DevExpress.XtraGrid.Columns.GridColumn clProduct;
        private DevExpress.XtraGrid.Columns.GridColumn clProductName;
        private DevExpress.XtraGrid.Columns.GridColumn clPrice;
        private DevExpress.XtraGrid.Columns.GridColumn clAmount;
        private DevExpress.XtraGrid.Columns.GridColumn clTotal;
        private DevExpress.XtraGrid.Columns.GridColumn clSku;
    }
}