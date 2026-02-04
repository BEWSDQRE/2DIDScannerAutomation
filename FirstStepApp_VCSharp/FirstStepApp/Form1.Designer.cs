namespace FirstStepApp
{
    partial class Form1
    {
        /// <summary>
        /// ?????????????
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ???????????????????????
        /// </summary>
        /// <param name="disposing">???? ???????????? true ???????????? false ???????</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows ???? ??????????????

        /// <summary>
        /// ????? ?????????????????????????
        /// ??? ?????????????????
        /// </summary>
        private void InitializeComponent()
        {
            this.liveviewForm1 = new Keyence.AutoID.SDK.LiveviewForm();
            this.TgrBtn = new System.Windows.Forms.Button();
            this.DataText = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SchBtn = new System.Windows.Forms.Button();
            this.SctBtn = new System.Windows.Forms.CheckBox();
            this.NICcomboBox = new System.Windows.Forms.ComboBox();
            this.lblFileName = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnCreateFile = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnHelp = new System.Windows.Forms.Button();
            this.lblStep1 = new System.Windows.Forms.Label();
            this.lblStep2 = new System.Windows.Forms.Label();
            this.lblStep3 = new System.Windows.Forms.Label();
            this.lblNicHint = new System.Windows.Forms.Label();
            this.lblReaderHint = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblStep1
            // 
            this.lblStep1.AutoSize = true;
            this.lblStep1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStep1.Location = new System.Drawing.Point(9, 9);
            this.lblStep1.Name = "lblStep1";
            this.lblStep1.Size = new System.Drawing.Size(130, 13);
            this.lblStep1.TabIndex = 16;
            this.lblStep1.Text = "Step 1: Create Excel File";
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(9, 28);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(57, 12);
            this.lblFileName.TabIndex = 10;
            this.lblFileName.Text = "File Name:";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(72, 25);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(120, 19);
            this.txtFileName.TabIndex = 1;
            this.txtFileName.Text = "ScanData";
            // 
            // btnCreateFile
            // 
            this.btnCreateFile.Location = new System.Drawing.Point(198, 23);
            this.btnCreateFile.Name = "btnCreateFile";
            this.btnCreateFile.Size = new System.Drawing.Size(120, 23);
            this.btnCreateFile.TabIndex = 2;
            this.btnCreateFile.Text = "Create Excel File";
            this.btnCreateFile.UseVisualStyleBackColor = true;
            this.btnCreateFile.Click += new System.EventHandler(this.btnCreateFile_Click);
            // 
            // lblStep2
            // 
            this.lblStep2.AutoSize = true;
            this.lblStep2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStep2.Location = new System.Drawing.Point(9, 55);
            this.lblStep2.Name = "lblStep2";
            this.lblStep2.Size = new System.Drawing.Size(144, 13);
            this.lblStep2.TabIndex = 17;
            this.lblStep2.Text = "Step 2: Scanner (Auto-Connect)";
            // 
            // NICcomboBox
            // 
            this.NICcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NICcomboBox.FormattingEnabled = true;
            this.NICcomboBox.Location = new System.Drawing.Point(11, 74);
            this.NICcomboBox.Name = "NICcomboBox";
            this.NICcomboBox.Size = new System.Drawing.Size(130, 20);
            this.NICcomboBox.TabIndex = 3;
            this.NICcomboBox.Visible = false;
            // 
            // SchBtn
            // 
            this.SchBtn.Location = new System.Drawing.Point(147, 72);
            this.SchBtn.Name = "SchBtn";
            this.SchBtn.Size = new System.Drawing.Size(55, 23);
            this.SchBtn.TabIndex = 4;
            this.SchBtn.Text = "Search";
            this.SchBtn.UseVisualStyleBackColor = true;
            this.SchBtn.Visible = false;
            this.SchBtn.Click += new System.EventHandler(this.SchBtn_Click);
            // 
            // lblNicHint
            // 
            this.lblNicHint.AutoSize = true;
            this.lblNicHint.ForeColor = System.Drawing.Color.Green;
            this.lblNicHint.Location = new System.Drawing.Point(9, 74);
            this.lblNicHint.Name = "lblNicHint";
            this.lblNicHint.Size = new System.Drawing.Size(200, 12);
            this.lblNicHint.TabIndex = 19;
            this.lblNicHint.Text = "Auto-connecting to 192.168.100.100...";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(11, 100);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(130, 20);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.Visible = false;
            // 
            // SctBtn
            // 
            this.SctBtn.Appearance = System.Windows.Forms.Appearance.Button;
            this.SctBtn.Enabled = false;
            this.SctBtn.Location = new System.Drawing.Point(147, 98);
            this.SctBtn.Name = "SctBtn";
            this.SctBtn.Size = new System.Drawing.Size(55, 23);
            this.SctBtn.TabIndex = 6;
            this.SctBtn.Text = "Select";
            this.SctBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SctBtn.UseVisualStyleBackColor = true;
            this.SctBtn.Visible = false;
            this.SctBtn.CheckedChanged += new System.EventHandler(this.SctBtn_CheckedChanged);
            // 
            // lblReaderHint
            // 
            this.lblReaderHint.AutoSize = true;
            this.lblReaderHint.ForeColor = System.Drawing.Color.Blue;
            this.lblReaderHint.Location = new System.Drawing.Point(205, 103);
            this.lblReaderHint.Name = "lblReaderHint";
            this.lblReaderHint.Size = new System.Drawing.Size(120, 12);
            this.lblReaderHint.TabIndex = 20;
            this.lblReaderHint.Text = "";
            this.lblReaderHint.Visible = false;
            // 
            // TgrBtn
            // 
            this.TgrBtn.Enabled = false;
            this.TgrBtn.Location = new System.Drawing.Point(11, 95);
            this.TgrBtn.Name = "TgrBtn";
            this.TgrBtn.Size = new System.Drawing.Size(340, 26);
            this.TgrBtn.TabIndex = 7;
            this.TgrBtn.Text = "Trigger On (Scan)";
            this.TgrBtn.UseVisualStyleBackColor = true;
            this.TgrBtn.Click += new System.EventHandler(this.TgrBtn_Click);
            // 
            // liveviewForm1
            // 
            this.liveviewForm1.BackColor = System.Drawing.Color.Black;
            this.liveviewForm1.BinningType = Keyence.AutoID.SDK.LiveviewForm.ImageBinningType.None;
            this.liveviewForm1.ImageFormat = Keyence.AutoID.SDK.LiveviewForm.ImageFormatType.Jpeg;
            this.liveviewForm1.ImageQuality = 3;
            this.liveviewForm1.IpAddress = "192.168.100.100";
            this.liveviewForm1.Location = new System.Drawing.Point(11, 127);
            this.liveviewForm1.Name = "liveviewForm1";
            this.liveviewForm1.PullTimeSpan = 1;
            this.liveviewForm1.Size = new System.Drawing.Size(340, 200);
            this.liveviewForm1.TabIndex = 0;
            this.liveviewForm1.TimeoutMs = 100;
            // 
            // lblStep3
            // 
            this.lblStep3.AutoSize = true;
            this.lblStep3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStep3.Location = new System.Drawing.Point(9, 334);
            this.lblStep3.Name = "lblStep3";
            this.lblStep3.Size = new System.Drawing.Size(180, 13);
            this.lblStep3.TabIndex = 18;
            this.lblStep3.Text = "Step 3: Scan (Auto-saves to Excel)";
            // 
            // DataText
            // 
            this.DataText.BackColor = System.Drawing.SystemColors.Control;
            this.DataText.Location = new System.Drawing.Point(11, 354);
            this.DataText.Name = "DataText";
            this.DataText.ReadOnly = true;
            this.DataText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DataText.Size = new System.Drawing.Size(340, 19);
            this.DataText.TabIndex = 8;
            // 
            // lblStatus
            // 
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus.Location = new System.Drawing.Point(360, 9);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(170, 325);
            this.lblStatus.TabIndex = 14;
            this.lblStatus.Text = "Status:\nAuto-connecting...\n\nWorkflow:\n1. Create Excel file\n2. Scanner auto-connects\n3. Scan and save";
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(360, 349);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(170, 26);
            this.btnHelp.TabIndex = 10;
            this.btnHelp.Text = "Help / User Guide";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 390);
            this.Controls.Add(this.lblStep1);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.btnCreateFile);
            this.Controls.Add(this.lblStep2);
            this.Controls.Add(this.NICcomboBox);
            this.Controls.Add(this.SchBtn);
            this.Controls.Add(this.lblNicHint);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.SctBtn);
            this.Controls.Add(this.lblReaderHint);
            this.Controls.Add(this.TgrBtn);
            this.Controls.Add(this.liveviewForm1);
            this.Controls.Add(this.lblStep3);
            this.Controls.Add(this.DataText);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnHelp);
            this.Name = "Form1";
            this.Text = "FirstStepApp - 2D Barcode Scanner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Keyence.AutoID.SDK.LiveviewForm liveviewForm1;
        private System.Windows.Forms.Button TgrBtn;
        private System.Windows.Forms.TextBox DataText;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button SchBtn;
        private System.Windows.Forms.CheckBox SctBtn;
        private System.Windows.Forms.ComboBox NICcomboBox;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnCreateFile;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Label lblStep1;
        private System.Windows.Forms.Label lblStep2;
        private System.Windows.Forms.Label lblStep3;
        private System.Windows.Forms.Label lblNicHint;
        private System.Windows.Forms.Label lblReaderHint;
    }
}

