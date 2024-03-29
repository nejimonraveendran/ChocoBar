namespace ChocoBar
{
    partial class frmAddEditMyButton
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddEditMyButton));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSliderUpShortcut = new System.Windows.Forms.TextBox();
            this.txtButtonAndSliderUpShortcut = new System.Windows.Forms.TextBox();
            this.radPush = new System.Windows.Forms.RadioButton();
            this.radSlider = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.lblShortcut = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtText = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTooltip = new System.Windows.Forms.TextBox();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.picBackColor = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.picForeColor = new System.Windows.Forms.PictureBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.clrDlg = new System.Windows.Forms.ColorDialog();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.btnClear = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBackColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picForeColor)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSliderUpShortcut);
            this.groupBox1.Controls.Add(this.txtButtonAndSliderUpShortcut);
            this.groupBox1.Controls.Add(this.radPush);
            this.groupBox1.Controls.Add(this.radSlider);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblShortcut);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(457, 213);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Keys";
            // 
            // txtSliderUpShortcut
            // 
            this.txtSliderUpShortcut.BackColor = System.Drawing.Color.AliceBlue;
            this.txtSliderUpShortcut.Location = new System.Drawing.Point(238, 154);
            this.txtSliderUpShortcut.Name = "txtSliderUpShortcut";
            this.txtSliderUpShortcut.Size = new System.Drawing.Size(197, 26);
            this.txtSliderUpShortcut.TabIndex = 16;
            this.txtSliderUpShortcut.TextChanged += new System.EventHandler(this.txtSliderUpShortcut_TextChanged);
            this.txtSliderUpShortcut.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSliderUpShortcut_KeyDown);
            this.txtSliderUpShortcut.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtSliderUpShortcut_PreviewKeyDown);
            // 
            // txtButtonAndSliderUpShortcut
            // 
            this.txtButtonAndSliderUpShortcut.BackColor = System.Drawing.Color.AliceBlue;
            this.txtButtonAndSliderUpShortcut.Location = new System.Drawing.Point(23, 154);
            this.txtButtonAndSliderUpShortcut.Name = "txtButtonAndSliderUpShortcut";
            this.txtButtonAndSliderUpShortcut.Size = new System.Drawing.Size(200, 26);
            this.txtButtonAndSliderUpShortcut.TabIndex = 14;
            this.txtButtonAndSliderUpShortcut.TextChanged += new System.EventHandler(this.txtButtonShortcut_TextChanged);
            this.txtButtonAndSliderUpShortcut.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtButtonShortcut_KeyDown);
            this.txtButtonAndSliderUpShortcut.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtButtonAndSliderUpShortcut_PreviewKeyDown);
            // 
            // radPush
            // 
            this.radPush.AutoSize = true;
            this.radPush.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.radPush.Location = new System.Drawing.Point(26, 61);
            this.radPush.Name = "radPush";
            this.radPush.Size = new System.Drawing.Size(74, 24);
            this.radPush.TabIndex = 0;
            this.radPush.TabStop = true;
            this.radPush.Text = "Push";
            this.radPush.UseVisualStyleBackColor = true;
            this.radPush.CheckedChanged += new System.EventHandler(this.radPush_CheckedChanged);
            // 
            // radSlider
            // 
            this.radSlider.AutoSize = true;
            this.radSlider.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.radSlider.Location = new System.Drawing.Point(149, 61);
            this.radSlider.Name = "radSlider";
            this.radSlider.Size = new System.Drawing.Size(80, 24);
            this.radSlider.TabIndex = 1;
            this.radSlider.TabStop = true;
            this.radSlider.Text = "Slider";
            this.radSlider.UseVisualStyleBackColor = true;
            this.radSlider.CheckedChanged += new System.EventHandler(this.radSlider_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label3.Location = new System.Drawing.Point(234, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Slider down shortcut";
            // 
            // lblShortcut
            // 
            this.lblShortcut.AutoSize = true;
            this.lblShortcut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblShortcut.Location = new System.Drawing.Point(22, 129);
            this.lblShortcut.Name = "lblShortcut";
            this.lblShortcut.Size = new System.Drawing.Size(134, 20);
            this.lblShortcut.TabIndex = 2;
            this.lblShortcut.Text = "Button shortcut";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(22, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Button type";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label5.Location = new System.Drawing.Point(15, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Text";
            // 
            // txtText
            // 
            this.txtText.BackColor = System.Drawing.Color.AliceBlue;
            this.txtText.Location = new System.Drawing.Point(19, 75);
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(47, 26);
            this.txtText.TabIndex = 8;
            this.txtText.TextChanged += new System.EventHandler(this.txtText_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtTooltip);
            this.groupBox2.Controls.Add(this.picIcon);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.picBackColor);
            this.groupBox2.Controls.Add(this.txtText);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox2.Location = new System.Drawing.Point(492, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 213);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Text and Appearance";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(15, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 20);
            this.label2.TabIndex = 17;
            this.label2.Text = "Icon";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label8.Location = new System.Drawing.Point(86, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(149, 20);
            this.label8.TabIndex = 15;
            this.label8.Text = "Background color";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label6.Location = new System.Drawing.Point(84, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "Description";
            // 
            // txtTooltip
            // 
            this.txtTooltip.BackColor = System.Drawing.Color.AliceBlue;
            this.txtTooltip.Location = new System.Drawing.Point(88, 75);
            this.txtTooltip.Name = "txtTooltip";
            this.txtTooltip.Size = new System.Drawing.Size(148, 26);
            this.txtTooltip.TabIndex = 9;
            this.txtTooltip.TextChanged += new System.EventHandler(this.txtTooltip_TextChanged);
            // 
            // picIcon
            // 
            this.picIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picIcon.Location = new System.Drawing.Point(19, 149);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(50, 50);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picIcon.TabIndex = 16;
            this.picIcon.TabStop = false;
            this.picIcon.Click += new System.EventHandler(this.picIcon_Click);
            // 
            // picBackColor
            // 
            this.picBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBackColor.Location = new System.Drawing.Point(90, 149);
            this.picBackColor.Name = "picBackColor";
            this.picBackColor.Size = new System.Drawing.Size(146, 50);
            this.picBackColor.TabIndex = 2;
            this.picBackColor.TabStop = false;
            this.picBackColor.Click += new System.EventHandler(this.picBackColor_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label7.Location = new System.Drawing.Point(672, 379);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Text Color";
            // 
            // picForeColor
            // 
            this.picForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picForeColor.Location = new System.Drawing.Point(676, 402);
            this.picForeColor.Name = "picForeColor";
            this.picForeColor.Size = new System.Drawing.Size(94, 45);
            this.picForeColor.TabIndex = 0;
            this.picForeColor.TabStop = false;
            this.picForeColor.Click += new System.EventHandler(this.picForeColor_Click);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.Teal;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOk.Location = new System.Drawing.Point(633, 246);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(129, 52);
            this.btnOk.TabIndex = 12;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCancel.Location = new System.Drawing.Point(484, 246);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(129, 52);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.FileName = "openFileDialog1";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.SteelBlue;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClear.Location = new System.Drawing.Point(12, 246);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(129, 52);
            this.btnClear.TabIndex = 15;
            this.btnClear.Text = "Clear All";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // frmAddEditMyButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(782, 336);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.picForeColor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAddEditMyButton";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add New Button";
            this.Load += new System.EventHandler(this.frmAddEditMyButton_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBackColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picForeColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblShortcut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox picBackColor;
        private System.Windows.Forms.PictureBox picForeColor;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTooltip;
        private System.Windows.Forms.RadioButton radSlider;
        private System.Windows.Forms.RadioButton radPush;
        private System.Windows.Forms.TextBox txtSliderUpShortcut;
        private System.Windows.Forms.ColorDialog clrDlg;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtButtonAndSliderUpShortcut;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
        private System.Windows.Forms.Button btnClear;
    }
}