﻿namespace PowerPOS
{
    partial class FmCashReceive
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FmCashReceive));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblPrice = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.txtCash = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.label6 = new System.Windows.Forms.Label();
            this.lblChange = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDiscountBath = new DevExpress.XtraEditors.TextEdit();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.txtCoupon = new DevExpress.XtraEditors.TextEdit();
            this.label9 = new System.Windows.Forms.Label();
            this.rdbPercent = new System.Windows.Forms.RadioButton();
            this.rdbBath = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDiscountPer = new DevExpress.XtraEditors.TextEdit();
            this.lblPerBath = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCash.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscountBath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCoupon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscountPer.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.Black;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.lblPrice);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            this.panelControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(277, 61);
            this.panelControl1.TabIndex = 1;
            // 
            // lblPrice
            // 
            this.lblPrice.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblPrice.Font = new System.Drawing.Font("Arial", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.ForeColor = System.Drawing.Color.Lime;
            this.lblPrice.Location = new System.Drawing.Point(0, 17);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(277, 44);
            this.lblPrice.TabIndex = 4;
            this.lblPrice.Text = "9,999,999";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Silver;
            this.label4.Location = new System.Drawing.Point(211, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 19);
            this.label4.TabIndex = 2;
            this.label4.Text = "หน่วย = บาท";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(3, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 33);
            this.label3.TabIndex = 3;
            this.label3.Text = "ยอดสุทธิ";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.txtCash);
            this.panelControl2.Controls.Add(this.label1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 61);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(277, 60);
            this.panelControl2.TabIndex = 2;
            // 
            // txtCash
            // 
            this.txtCash.Location = new System.Drawing.Point(67, 12);
            this.txtCash.Name = "txtCash";
            this.txtCash.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCash.Properties.Appearance.Options.UseFont = true;
            this.txtCash.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCash.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtCash.Properties.AutoHeight = false;
            this.txtCash.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.txtCash.Size = new System.Drawing.Size(198, 39);
            this.txtCash.TabIndex = 6;
            this.txtCash.Enter += new System.EventHandler(this.txtCash_Enter);
            this.txtCash.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCash_KeyDown);
            this.txtCash.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCash_KeyPress);
            this.txtCash.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCash_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "เงินสด";
            // 
            // panelControl3
            // 
            this.panelControl3.Appearance.BackColor = System.Drawing.Color.Maroon;
            this.panelControl3.Appearance.Options.UseBackColor = true;
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.label6);
            this.panelControl3.Controls.Add(this.label5);
            this.panelControl3.Controls.Add(this.lblChange);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(0, 285);
            this.panelControl3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            this.panelControl3.LookAndFeel.UseDefaultLookAndFeel = false;
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(277, 65);
            this.panelControl3.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Orange;
            this.label6.Location = new System.Drawing.Point(3, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 33);
            this.label6.TabIndex = 3;
            this.label6.Text = "เงินทอน";
            // 
            // lblChange
            // 
            this.lblChange.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblChange.Font = new System.Drawing.Font("Arial", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChange.ForeColor = System.Drawing.Color.Orange;
            this.lblChange.Location = new System.Drawing.Point(0, 19);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(277, 46);
            this.lblChange.TabIndex = 1;
            this.lblChange.Text = "9,999,999";
            this.lblChange.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Orange;
            this.label5.Location = new System.Drawing.Point(208, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "หน่วย = บาท";
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(172, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 34);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "บันทึกข้อมูล";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(10, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 31);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.btnSave);
            this.panelControl4.Controls.Add(this.btnCancel);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl4.Location = new System.Drawing.Point(0, 350);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(277, 50);
            this.panelControl4.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "ส่วนลด";
            // 
            // txtDiscountBath
            // 
            this.txtDiscountBath.EditValue = "";
            this.txtDiscountBath.Enabled = false;
            this.txtDiscountBath.Location = new System.Drawing.Point(93, 47);
            this.txtDiscountBath.Name = "txtDiscountBath";
            this.txtDiscountBath.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtDiscountBath.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscountBath.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtDiscountBath.Properties.Appearance.Options.UseBackColor = true;
            this.txtDiscountBath.Properties.Appearance.Options.UseFont = true;
            this.txtDiscountBath.Properties.Appearance.Options.UseForeColor = true;
            this.txtDiscountBath.Properties.Appearance.Options.UseTextOptions = true;
            this.txtDiscountBath.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtDiscountBath.Properties.AutoHeight = false;
            this.txtDiscountBath.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.txtDiscountBath.Size = new System.Drawing.Size(121, 39);
            this.txtDiscountBath.TabIndex = 1;
            this.txtDiscountBath.Enter += new System.EventHandler(this.txtDiscountBath_Enter);
            this.txtDiscountBath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDiscountBath_KeyPress);
            this.txtDiscountBath.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDiscountBath_KeyUp);
            // 
            // panelControl5
            // 
            this.panelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl5.Controls.Add(this.lblPerBath);
            this.panelControl5.Controls.Add(this.txtCoupon);
            this.panelControl5.Controls.Add(this.label9);
            this.panelControl5.Controls.Add(this.rdbPercent);
            this.panelControl5.Controls.Add(this.rdbBath);
            this.panelControl5.Controls.Add(this.label8);
            this.panelControl5.Controls.Add(this.label7);
            this.panelControl5.Controls.Add(this.txtDiscountPer);
            this.panelControl5.Controls.Add(this.txtDiscountBath);
            this.panelControl5.Controls.Add(this.label2);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl5.Location = new System.Drawing.Point(0, 121);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(277, 229);
            this.panelControl5.TabIndex = 3;
            // 
            // txtCoupon
            // 
            this.txtCoupon.Location = new System.Drawing.Point(67, 7);
            this.txtCoupon.Name = "txtCoupon";
            this.txtCoupon.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtCoupon.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCoupon.Properties.Appearance.Options.UseBackColor = true;
            this.txtCoupon.Properties.Appearance.Options.UseFont = true;
            this.txtCoupon.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCoupon.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtCoupon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.txtCoupon.Size = new System.Drawing.Size(198, 34);
            this.txtCoupon.TabIndex = 13;
            this.txtCoupon.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCoupon_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(13, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 19);
            this.label9.TabIndex = 12;
            this.label9.Text = "คูปอง";
            // 
            // rdbPercent
            // 
            this.rdbPercent.AutoSize = true;
            this.rdbPercent.Location = new System.Drawing.Point(67, 106);
            this.rdbPercent.Name = "rdbPercent";
            this.rdbPercent.Size = new System.Drawing.Size(14, 13);
            this.rdbPercent.TabIndex = 11;
            this.rdbPercent.TabStop = true;
            this.rdbPercent.UseVisualStyleBackColor = true;
            this.rdbPercent.CheckedChanged += new System.EventHandler(this.CheckedDiscount);
            // 
            // rdbBath
            // 
            this.rdbBath.AutoSize = true;
            this.rdbBath.Location = new System.Drawing.Point(67, 61);
            this.rdbBath.Name = "rdbBath";
            this.rdbBath.Size = new System.Drawing.Size(14, 13);
            this.rdbBath.TabIndex = 6;
            this.rdbBath.TabStop = true;
            this.rdbBath.UseVisualStyleBackColor = true;
            this.rdbBath.CheckedChanged += new System.EventHandler(this.CheckedDiscount);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Enabled = false;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(220, 106);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "( % )";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Enabled = false;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(220, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "( บาท )";
            // 
            // txtDiscountPer
            // 
            this.txtDiscountPer.EditValue = "";
            this.txtDiscountPer.Location = new System.Drawing.Point(93, 93);
            this.txtDiscountPer.Name = "txtDiscountPer";
            this.txtDiscountPer.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtDiscountPer.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscountPer.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtDiscountPer.Properties.Appearance.Options.UseBackColor = true;
            this.txtDiscountPer.Properties.Appearance.Options.UseFont = true;
            this.txtDiscountPer.Properties.Appearance.Options.UseForeColor = true;
            this.txtDiscountPer.Properties.Appearance.Options.UseTextOptions = true;
            this.txtDiscountPer.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtDiscountPer.Properties.AutoHeight = false;
            this.txtDiscountPer.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.txtDiscountPer.Size = new System.Drawing.Size(121, 39);
            this.txtDiscountPer.TabIndex = 9;
            this.txtDiscountPer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDiscountPer_KeyPress);
            this.txtDiscountPer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDiscountPer_KeyUp);
            // 
            // lblPerBath
            // 
            this.lblPerBath.AutoSize = true;
            this.lblPerBath.Enabled = false;
            this.lblPerBath.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPerBath.ForeColor = System.Drawing.Color.Black;
            this.lblPerBath.Location = new System.Drawing.Point(90, 135);
            this.lblPerBath.Name = "lblPerBath";
            this.lblPerBath.Size = new System.Drawing.Size(28, 18);
            this.lblPerBath.TabIndex = 14;
            this.lblPerBath.Text = "....";
            this.lblPerBath.Visible = false;
            // 
            // FmCashReceive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 400);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl5);
            this.Controls.Add(this.panelControl4);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FmCashReceive";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ชำระเงิน";
            this.Load += new System.EventHandler(this.FmCashReceive_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCash.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscountBath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            this.panelControl5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCoupon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscountPer.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        public System.Windows.Forms.Label lblPrice;
        public DevExpress.XtraEditors.TextEdit txtCash;
        public System.Windows.Forms.Label lblChange;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private System.Windows.Forms.Label label2;
        public DevExpress.XtraEditors.TextEdit txtDiscountBath;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rdbBath;
        private System.Windows.Forms.RadioButton rdbPercent;
        private System.Windows.Forms.Label label8;
        public DevExpress.XtraEditors.TextEdit txtDiscountPer;
        public DevExpress.XtraEditors.TextEdit txtCoupon;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblPerBath;
    }
}