namespace PowerPOS
{
    partial class FmSelectPrinter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FmSelectPrinter));
            this.gcPrinter = new DevExpress.XtraEditors.GroupControl();
            this.panelControl12 = new DevExpress.XtraEditors.PanelControl();
            this.cbxPrinter = new DevExpress.XtraEditors.ComboBoxEdit();
            this.panelControl11 = new DevExpress.XtraEditors.PanelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcPrinter)).BeginInit();
            this.gcPrinter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxPrinter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl11)).BeginInit();
            this.SuspendLayout();
            // 
            // gcPrinter
            // 
            this.gcPrinter.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcPrinter.AppearanceCaption.Options.UseFont = true;
            this.gcPrinter.Controls.Add(this.panelControl12);
            this.gcPrinter.Controls.Add(this.cbxPrinter);
            this.gcPrinter.Controls.Add(this.panelControl11);
            this.gcPrinter.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcPrinter.Location = new System.Drawing.Point(0, 0);
            this.gcPrinter.Name = "gcPrinter";
            this.gcPrinter.Size = new System.Drawing.Size(295, 61);
            this.gcPrinter.TabIndex = 26;
            this.gcPrinter.Text = "เครื่องพิมพ์";
            // 
            // panelControl12
            // 
            this.panelControl12.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl12.Location = new System.Drawing.Point(2, 52);
            this.panelControl12.Name = "panelControl12";
            this.panelControl12.Size = new System.Drawing.Size(291, 5);
            this.panelControl12.TabIndex = 25;
            // 
            // cbxPrinter
            // 
            this.cbxPrinter.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbxPrinter.Location = new System.Drawing.Point(2, 28);
            this.cbxPrinter.Name = "cbxPrinter";
            this.cbxPrinter.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxPrinter.Properties.Appearance.Options.UseFont = true;
            this.cbxPrinter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxPrinter.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxPrinter.Size = new System.Drawing.Size(291, 24);
            this.cbxPrinter.TabIndex = 0;
            // 
            // panelControl11
            // 
            this.panelControl11.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl11.Location = new System.Drawing.Point(2, 23);
            this.panelControl11.Name = "panelControl11";
            this.panelControl11.Size = new System.Drawing.Size(291, 5);
            this.panelControl11.TabIndex = 24;
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(206, 61);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(89, 35);
            this.btnSave.TabIndex = 28;
            this.btnSave.Text = "ตกลง";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FmSelectPrinter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 96);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gcPrinter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FmSelectPrinter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "กรุณาเลือกเครื่องพิมพ์";
            this.Load += new System.EventHandler(this.FmSelectPrinter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcPrinter)).EndInit();
            this.gcPrinter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxPrinter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl11)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gcPrinter;
        private DevExpress.XtraEditors.PanelControl panelControl12;
        private DevExpress.XtraEditors.ComboBoxEdit cbxPrinter;
        private DevExpress.XtraEditors.PanelControl panelControl11;
        private DevExpress.XtraEditors.SimpleButton btnSave;
    }
}