namespace PowerPOS
{
    partial class FmProfitConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FmProfitConfig));
            this.cbCategory = new DevExpress.XtraEditors.ComboBoxEdit();
            this.nudPrice4 = new DevExpress.XtraEditors.SpinEdit();
            this.nudPrice3 = new DevExpress.XtraEditors.SpinEdit();
            this.nudPrice2 = new DevExpress.XtraEditors.SpinEdit();
            this.nudPrice1 = new DevExpress.XtraEditors.SpinEdit();
            this.nudPrice = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cbCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cbCategory
            // 
            this.cbCategory.Location = new System.Drawing.Point(101, 12);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCategory.Properties.Appearance.Options.UseFont = true;
            this.cbCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbCategory.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbCategory.Size = new System.Drawing.Size(279, 26);
            this.cbCategory.TabIndex = 0;
            this.cbCategory.SelectedIndexChanged += new System.EventHandler(this.cbCategory_SelectedIndexChanged);
            // 
            // nudPrice4
            // 
            this.nudPrice4.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudPrice4.Location = new System.Drawing.Point(310, 65);
            this.nudPrice4.Name = "nudPrice4";
            this.nudPrice4.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPrice4.Properties.Appearance.Options.UseFont = true;
            this.nudPrice4.Properties.Appearance.Options.UseTextOptions = true;
            this.nudPrice4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.nudPrice4.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.nudPrice4.Properties.DisplayFormat.FormatString = "0";
            this.nudPrice4.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.nudPrice4.Properties.Mask.EditMask = "n";
            this.nudPrice4.Properties.MaxLength = 8;
            this.nudPrice4.Properties.MaxValue = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudPrice4.Size = new System.Drawing.Size(70, 22);
            this.nudPrice4.TabIndex = 27;
            this.nudPrice4.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // nudPrice3
            // 
            this.nudPrice3.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudPrice3.Location = new System.Drawing.Point(235, 65);
            this.nudPrice3.Name = "nudPrice3";
            this.nudPrice3.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPrice3.Properties.Appearance.Options.UseFont = true;
            this.nudPrice3.Properties.Appearance.Options.UseTextOptions = true;
            this.nudPrice3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.nudPrice3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.nudPrice3.Properties.DisplayFormat.FormatString = "0";
            this.nudPrice3.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.nudPrice3.Properties.Mask.EditMask = "n";
            this.nudPrice3.Properties.MaxLength = 8;
            this.nudPrice3.Properties.MaxValue = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudPrice3.Size = new System.Drawing.Size(70, 22);
            this.nudPrice3.TabIndex = 26;
            this.nudPrice3.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // nudPrice2
            // 
            this.nudPrice2.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudPrice2.Location = new System.Drawing.Point(160, 65);
            this.nudPrice2.Name = "nudPrice2";
            this.nudPrice2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPrice2.Properties.Appearance.Options.UseFont = true;
            this.nudPrice2.Properties.Appearance.Options.UseTextOptions = true;
            this.nudPrice2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.nudPrice2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.nudPrice2.Properties.DisplayFormat.FormatString = "0";
            this.nudPrice2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.nudPrice2.Properties.Mask.EditMask = "n";
            this.nudPrice2.Properties.MaxLength = 8;
            this.nudPrice2.Properties.MaxValue = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudPrice2.Size = new System.Drawing.Size(70, 22);
            this.nudPrice2.TabIndex = 25;
            this.nudPrice2.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // nudPrice1
            // 
            this.nudPrice1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudPrice1.Location = new System.Drawing.Point(86, 65);
            this.nudPrice1.Name = "nudPrice1";
            this.nudPrice1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPrice1.Properties.Appearance.Options.UseFont = true;
            this.nudPrice1.Properties.Appearance.Options.UseTextOptions = true;
            this.nudPrice1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.nudPrice1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.nudPrice1.Properties.DisplayFormat.FormatString = "0";
            this.nudPrice1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.nudPrice1.Properties.Mask.EditMask = "n";
            this.nudPrice1.Properties.MaxLength = 8;
            this.nudPrice1.Properties.MaxValue = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudPrice1.Size = new System.Drawing.Size(70, 22);
            this.nudPrice1.TabIndex = 24;
            this.nudPrice1.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // nudPrice
            // 
            this.nudPrice.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudPrice.Location = new System.Drawing.Point(12, 65);
            this.nudPrice.Name = "nudPrice";
            this.nudPrice.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPrice.Properties.Appearance.Options.UseFont = true;
            this.nudPrice.Properties.Appearance.Options.UseTextOptions = true;
            this.nudPrice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.nudPrice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.nudPrice.Properties.DisplayFormat.FormatString = "0";
            this.nudPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.nudPrice.Properties.Mask.EditMask = "n";
            this.nudPrice.Properties.MaxLength = 8;
            this.nudPrice.Properties.MaxValue = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudPrice.Size = new System.Drawing.Size(70, 22);
            this.nudPrice.TabIndex = 23;
            this.nudPrice.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Location = new System.Drawing.Point(255, 46);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(25, 16);
            this.labelControl7.TabIndex = 32;
            this.labelControl7.Text = "ส่ง 3";
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl10.Location = new System.Drawing.Point(341, 46);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(25, 16);
            this.labelControl10.TabIndex = 31;
            this.labelControl10.Text = "ส่ง 4";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Location = new System.Drawing.Point(33, 46);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(24, 16);
            this.labelControl6.TabIndex = 30;
            this.labelControl6.Text = "ปลีก";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(106, 46);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(25, 16);
            this.labelControl5.TabIndex = 29;
            this.labelControl5.Text = "ส่ง 1";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(182, 46);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(25, 16);
            this.labelControl4.TabIndex = 28;
            this.labelControl4.Text = "ส่ง 2";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(12, 18);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(83, 16);
            this.labelControl1.TabIndex = 33;
            this.labelControl1.Text = "เลือกหมวดหมู่ :";
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Enabled = false;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(278, 93);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 34);
            this.btnSave.TabIndex = 34;
            this.btnSave.Text = "บันทึกข้อมูล";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FmProfitConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 133);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl10);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.nudPrice4);
            this.Controls.Add(this.nudPrice3);
            this.Controls.Add(this.nudPrice2);
            this.Controls.Add(this.nudPrice1);
            this.Controls.Add(this.nudPrice);
            this.Controls.Add(this.cbCategory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FmProfitConfig";
            this.Text = "เปอร์เซ็นต์กำไร";
            this.Load += new System.EventHandler(this.FmProfitConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cbCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit cbCategory;
        private DevExpress.XtraEditors.SpinEdit nudPrice4;
        private DevExpress.XtraEditors.SpinEdit nudPrice3;
        private DevExpress.XtraEditors.SpinEdit nudPrice2;
        private DevExpress.XtraEditors.SpinEdit nudPrice1;
        private DevExpress.XtraEditors.SpinEdit nudPrice;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
    }
}