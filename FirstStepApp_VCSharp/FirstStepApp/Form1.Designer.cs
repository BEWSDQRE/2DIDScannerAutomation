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
            this.btnSendEmail = new System.Windows.Forms.Button();
            this.lblEmailTo = new System.Windows.Forms.Label();
            this.txtEmailDisplay = new System.Windows.Forms.TextBox();
            this.btnEmailDropdown = new System.Windows.Forms.Button();
            this.clbEmailRecipients = new System.Windows.Forms.CheckedListBox();
            this.lblStep1 = new System.Windows.Forms.Label();
            this.lblStep2 = new System.Windows.Forms.Label();
            this.lblStep3 = new System.Windows.Forms.Label();
            this.lblNicHint = new System.Windows.Forms.Label();
            this.lblReaderHint = new System.Windows.Forms.Label();
            this.rbLooseUnit = new System.Windows.Forms.RadioButton();
            this.rbTray = new System.Windows.Forms.RadioButton();
            this.lblMode = new System.Windows.Forms.Label();
            this.lblTrayRows = new System.Windows.Forms.Label();
            this.lblTrayCols = new System.Windows.Forms.Label();
            this.numTrayRows = new System.Windows.Forms.NumericUpDown();
            this.numTrayCols = new System.Windows.Forms.NumericUpDown();
            this.lblTrayX = new System.Windows.Forms.Label();
            this.btnSkipCell = new System.Windows.Forms.Button();
            this.btnNextRow = new System.Windows.Forms.Button();
            this.pnlTrayConfig = new System.Windows.Forms.Panel();
            this.lblCurrentPos = new System.Windows.Forms.Label();
            this.pnlTrayGrid = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numTrayRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTrayCols)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblMode.Location = new System.Drawing.Point(9, 9);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(80, 13);
            this.lblMode.TabIndex = 21;
            this.lblMode.Text = "Scan Mode:";
            // 
            // rbLooseUnit
            // 
            this.rbLooseUnit.AutoSize = true;
            this.rbLooseUnit.Checked = true;
            this.rbLooseUnit.Location = new System.Drawing.Point(95, 7);
            this.rbLooseUnit.Name = "rbLooseUnit";
            this.rbLooseUnit.Size = new System.Drawing.Size(80, 16);
            this.rbLooseUnit.TabIndex = 22;
            this.rbLooseUnit.TabStop = true;
            this.rbLooseUnit.Text = "Loose Unit";
            this.rbLooseUnit.UseVisualStyleBackColor = true;
            this.rbLooseUnit.CheckedChanged += new System.EventHandler(this.rbLooseUnit_CheckedChanged);
            // 
            // rbTray
            // 
            this.rbTray.AutoSize = true;
            this.rbTray.Location = new System.Drawing.Point(180, 7);
            this.rbTray.Name = "rbTray";
            this.rbTray.Size = new System.Drawing.Size(46, 16);
            this.rbTray.TabIndex = 23;
            this.rbTray.Text = "Tray";
            this.rbTray.UseVisualStyleBackColor = true;
            this.rbTray.CheckedChanged += new System.EventHandler(this.rbTray_CheckedChanged);
            // 
            // pnlTrayConfig
            // 
            this.pnlTrayConfig.Location = new System.Drawing.Point(9, 70);
            this.pnlTrayConfig.Name = "pnlTrayConfig";
            this.pnlTrayConfig.Size = new System.Drawing.Size(300, 24);
            this.pnlTrayConfig.TabIndex = 24;
            this.pnlTrayConfig.Visible = false;
            // 
            // lblTrayRows
            // 
            this.lblTrayRows.AutoSize = true;
            this.lblTrayRows.Location = new System.Drawing.Point(3, 5);
            this.lblTrayRows.Name = "lblTrayRows";
            this.lblTrayRows.Size = new System.Drawing.Size(35, 12);
            this.lblTrayRows.TabIndex = 25;
            this.lblTrayRows.Text = "Rows:";
            this.pnlTrayConfig.Controls.Add(this.lblTrayRows);
            // 
            // numTrayRows
            // 
            this.numTrayRows.Location = new System.Drawing.Point(40, 2);
            this.numTrayRows.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.numTrayRows.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numTrayRows.Name = "numTrayRows";
            this.numTrayRows.Size = new System.Drawing.Size(45, 19);
            this.numTrayRows.TabIndex = 26;
            this.numTrayRows.Value = new decimal(new int[] { 20, 0, 0, 0 });
            this.pnlTrayConfig.Controls.Add(this.numTrayRows);
            // 
            // lblTrayX
            // 
            this.lblTrayX.AutoSize = true;
            this.lblTrayX.Location = new System.Drawing.Point(88, 5);
            this.lblTrayX.Name = "lblTrayX";
            this.lblTrayX.Size = new System.Drawing.Size(13, 12);
            this.lblTrayX.TabIndex = 27;
            this.lblTrayX.Text = "x";
            this.pnlTrayConfig.Controls.Add(this.lblTrayX);
            // 
            // lblTrayCols
            // 
            this.lblTrayCols.AutoSize = true;
            this.lblTrayCols.Location = new System.Drawing.Point(103, 5);
            this.lblTrayCols.Name = "lblTrayCols";
            this.lblTrayCols.Size = new System.Drawing.Size(31, 12);
            this.lblTrayCols.TabIndex = 28;
            this.lblTrayCols.Text = "Cols:";
            this.pnlTrayConfig.Controls.Add(this.lblTrayCols);
            // 
            // numTrayCols
            // 
            this.numTrayCols.Location = new System.Drawing.Point(135, 2);
            this.numTrayCols.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.numTrayCols.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numTrayCols.Name = "numTrayCols";
            this.numTrayCols.Size = new System.Drawing.Size(45, 19);
            this.numTrayCols.TabIndex = 29;
            this.numTrayCols.Value = new decimal(new int[] { 30, 0, 0, 0 });
            this.pnlTrayConfig.Controls.Add(this.numTrayCols);
            // 
            // lblEmailTo
            // 
            this.lblEmailTo.AutoSize = true;
            this.lblEmailTo.Location = new System.Drawing.Point(9, 32);
            this.lblEmailTo.Name = "lblEmailTo";
            this.lblEmailTo.Size = new System.Drawing.Size(55, 12);
            this.lblEmailTo.TabIndex = 36;
            this.lblEmailTo.Text = "Email To:";
            // 
            // txtEmailDisplay
            // 
            this.txtEmailDisplay.Location = new System.Drawing.Point(72, 29);
            this.txtEmailDisplay.Name = "txtEmailDisplay";
            this.txtEmailDisplay.ReadOnly = true;
            this.txtEmailDisplay.Size = new System.Drawing.Size(200, 19);
            this.txtEmailDisplay.TabIndex = 37;
            this.txtEmailDisplay.Text = "(click \u25BC to select)";
            this.txtEmailDisplay.Click += new System.EventHandler(this.btnEmailDropdown_Click);
            // 
            // btnEmailDropdown
            // 
            this.btnEmailDropdown.Location = new System.Drawing.Point(272, 28);
            this.btnEmailDropdown.Name = "btnEmailDropdown";
            this.btnEmailDropdown.Size = new System.Drawing.Size(24, 21);
            this.btnEmailDropdown.TabIndex = 38;
            this.btnEmailDropdown.Text = "\u25BC";
            this.btnEmailDropdown.UseVisualStyleBackColor = true;
            this.btnEmailDropdown.Click += new System.EventHandler(this.btnEmailDropdown_Click);
            // 
            // clbEmailRecipients
            // 
            this.clbEmailRecipients.CheckOnClick = true;
            this.clbEmailRecipients.FormattingEnabled = true;
            this.clbEmailRecipients.Location = new System.Drawing.Point(72, 50);
            this.clbEmailRecipients.Name = "clbEmailRecipients";
            this.clbEmailRecipients.Size = new System.Drawing.Size(224, 100);
            this.clbEmailRecipients.TabIndex = 39;
            this.clbEmailRecipients.Visible = false;
            this.clbEmailRecipients.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbEmailRecipients_ItemCheck);
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.Location = new System.Drawing.Point(300, 28);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(50, 21);
            this.btnSendEmail.TabIndex = 40;
            this.btnSendEmail.Text = "Send";
            this.btnSendEmail.UseVisualStyleBackColor = true;
            this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
            // 
            // lblStep1
            // 
            this.lblStep1.AutoSize = true;
            this.lblStep1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStep1.Location = new System.Drawing.Point(9, 54);
            this.lblStep1.Name = "lblStep1";
            this.lblStep1.Size = new System.Drawing.Size(130, 13);
            this.lblStep1.TabIndex = 16;
            this.lblStep1.Text = "Step 1: Create Excel File";
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(9, 73);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(57, 12);
            this.lblFileName.TabIndex = 10;
            this.lblFileName.Text = "File Name:";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(72, 70);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(120, 19);
            this.txtFileName.TabIndex = 1;
            this.txtFileName.Text = "ScanData";
            // 
            // btnCreateFile
            // 
            this.btnCreateFile.Location = new System.Drawing.Point(198, 68);
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
            this.lblStep2.Location = new System.Drawing.Point(9, 100);
            this.lblStep2.Name = "lblStep2";
            this.lblStep2.Size = new System.Drawing.Size(144, 13);
            this.lblStep2.TabIndex = 17;
            this.lblStep2.Text = "Step 2: Scanner (Auto-Connect)";
            // 
            // NICcomboBox
            // 
            this.NICcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NICcomboBox.FormattingEnabled = true;
            this.NICcomboBox.Location = new System.Drawing.Point(11, 119);
            this.NICcomboBox.Name = "NICcomboBox";
            this.NICcomboBox.Size = new System.Drawing.Size(130, 20);
            this.NICcomboBox.TabIndex = 3;
            this.NICcomboBox.Visible = false;
            // 
            // SchBtn
            // 
            this.SchBtn.Location = new System.Drawing.Point(147, 117);
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
            this.lblNicHint.Location = new System.Drawing.Point(9, 119);
            this.lblNicHint.Name = "lblNicHint";
            this.lblNicHint.Size = new System.Drawing.Size(200, 12);
            this.lblNicHint.TabIndex = 19;
            this.lblNicHint.Text = "Auto-connecting to 192.168.100.100...";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(11, 145);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(130, 20);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.Visible = false;
            // 
            // SctBtn
            // 
            this.SctBtn.Appearance = System.Windows.Forms.Appearance.Button;
            this.SctBtn.Enabled = false;
            this.SctBtn.Location = new System.Drawing.Point(147, 143);
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
            this.lblReaderHint.Location = new System.Drawing.Point(205, 148);
            this.lblReaderHint.Name = "lblReaderHint";
            this.lblReaderHint.Size = new System.Drawing.Size(120, 12);
            this.lblReaderHint.TabIndex = 20;
            this.lblReaderHint.Text = "";
            this.lblReaderHint.Visible = false;
            // 
            // TgrBtn
            // 
            this.TgrBtn.Enabled = false;
            this.TgrBtn.Location = new System.Drawing.Point(11, 140);
            this.TgrBtn.Name = "TgrBtn";
            this.TgrBtn.Size = new System.Drawing.Size(340, 26);
            this.TgrBtn.TabIndex = 7;
            this.TgrBtn.Text = "Trigger On (Scan) - (`)";
            this.TgrBtn.UseVisualStyleBackColor = true;
            this.TgrBtn.Click += new System.EventHandler(this.TgrBtn_Click);
            // 
            // btnSkipCell
            // 
            this.btnSkipCell.Enabled = false;
            this.btnSkipCell.Location = new System.Drawing.Point(11, 172);
            this.btnSkipCell.Name = "btnSkipCell";
            this.btnSkipCell.Size = new System.Drawing.Size(165, 26);
            this.btnSkipCell.TabIndex = 30;
            this.btnSkipCell.Text = "Skip Cell - (-)";
            this.btnSkipCell.UseVisualStyleBackColor = true;
            this.btnSkipCell.Visible = false;
            this.btnSkipCell.Click += new System.EventHandler(this.btnSkipCell_Click);
            // 
            // btnNextRow
            // 
            this.btnNextRow.Enabled = false;
            this.btnNextRow.Location = new System.Drawing.Point(186, 172);
            this.btnNextRow.Name = "btnNextRow";
            this.btnNextRow.Size = new System.Drawing.Size(165, 26);
            this.btnNextRow.TabIndex = 31;
            this.btnNextRow.Text = "Next Row - (=)";
            this.btnNextRow.UseVisualStyleBackColor = true;
            this.btnNextRow.Visible = false;
            this.btnNextRow.Click += new System.EventHandler(this.btnNextRow_Click);
            // 
            // lblCurrentPos
            // 
            this.lblCurrentPos.AutoSize = true;
            this.lblCurrentPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCurrentPos.ForeColor = System.Drawing.Color.Blue;
            this.lblCurrentPos.Location = new System.Drawing.Point(233, 119);
            this.lblCurrentPos.Name = "lblCurrentPos";
            this.lblCurrentPos.Size = new System.Drawing.Size(100, 13);
            this.lblCurrentPos.TabIndex = 32;
            this.lblCurrentPos.Text = "";
            this.lblCurrentPos.Visible = false;
            // 
            // liveviewForm1
            // 
            this.liveviewForm1.BackColor = System.Drawing.Color.Black;
            this.liveviewForm1.BinningType = Keyence.AutoID.SDK.LiveviewForm.ImageBinningType.None;
            this.liveviewForm1.ImageFormat = Keyence.AutoID.SDK.LiveviewForm.ImageFormatType.Jpeg;
            this.liveviewForm1.ImageQuality = 3;
            this.liveviewForm1.IpAddress = "192.168.100.100";
            this.liveviewForm1.Location = new System.Drawing.Point(11, 174);
            this.liveviewForm1.Name = "liveviewForm1";
            this.liveviewForm1.PullTimeSpan = 1;
            this.liveviewForm1.Size = new System.Drawing.Size(340, 180);
            this.liveviewForm1.TabIndex = 0;
            this.liveviewForm1.TimeoutMs = 100;
            // 
            // lblStep3
            // 
            this.lblStep3.AutoSize = true;
            this.lblStep3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStep3.Location = new System.Drawing.Point(9, 362);
            this.lblStep3.Name = "lblStep3";
            this.lblStep3.Size = new System.Drawing.Size(180, 13);
            this.lblStep3.TabIndex = 18;
            this.lblStep3.Text = "Step 3: Scan (Auto-saves to Excel)";
            // 
            // DataText
            // 
            this.DataText.BackColor = System.Drawing.SystemColors.Control;
            this.DataText.Location = new System.Drawing.Point(11, 382);
            this.DataText.Name = "DataText";
            this.DataText.ReadOnly = true;
            this.DataText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DataText.Size = new System.Drawing.Size(340, 19);
            this.DataText.TabIndex = 8;
            // 
            // pnlTrayGrid
            // 
            this.pnlTrayGrid.AutoScroll = true;
            this.pnlTrayGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTrayGrid.Location = new System.Drawing.Point(545, 30);
            this.pnlTrayGrid.Name = "pnlTrayGrid";
            this.pnlTrayGrid.Size = new System.Drawing.Size(320, 370);
            this.pnlTrayGrid.TabIndex = 33;
            this.pnlTrayGrid.Visible = false;
            this.pnlTrayGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            // 
            // lblStatus
            // 
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus.Location = new System.Drawing.Point(360, 30);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(170, 344);
            this.lblStatus.TabIndex = 14;
            this.lblStatus.Text = "Status:\nAuto-connecting...\n\nWorkflow:\n1. Select recipient email\n2. Select mode\n3. Create Excel file\n4. Scanner auto-connects\n5. Scan and save\n6. Click Send to email file";
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(360, 382);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(170, 26);
            this.btnHelp.TabIndex = 10;
            this.btnHelp.Text = "Help / User Guide";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // lblFooter
            // 
            this.lblFooter = new System.Windows.Forms.Label();
            this.lblFooter.AutoSize = true;
            this.lblFooter.ForeColor = System.Drawing.Color.Gray;
            this.lblFooter.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblFooter.Location = new System.Drawing.Point(9, 409);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(300, 12);
            this.lblFooter.TabIndex = 34;
            this.lblFooter.Text = "For any issues, please contact You Shen Chew, WSD NPI QRE";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 464);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.rbLooseUnit);
            this.Controls.Add(this.rbTray);
            this.Controls.Add(this.pnlTrayConfig);
            this.Controls.Add(this.lblEmailTo);
            this.Controls.Add(this.txtEmailDisplay);
            this.Controls.Add(this.btnEmailDropdown);
            this.Controls.Add(this.btnSendEmail);
            this.Controls.Add(this.clbEmailRecipients);
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
            this.Controls.Add(this.btnSkipCell);
            this.Controls.Add(this.btnNextRow);
            this.Controls.Add(this.lblCurrentPos);
            this.Controls.Add(this.liveviewForm1);
            this.Controls.Add(this.lblStep3);
            this.Controls.Add(this.DataText);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.pnlTrayGrid);
            this.Controls.Add(this.lblFooter);
            this.Name = "Form1";
            this.Text = "FirstStepApp - 2D Barcode Scanner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numTrayRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTrayCols)).EndInit();
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
        private System.Windows.Forms.RadioButton rbLooseUnit;
        private System.Windows.Forms.RadioButton rbTray;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.Label lblTrayRows;
        private System.Windows.Forms.Label lblTrayCols;
        private System.Windows.Forms.NumericUpDown numTrayRows;
        private System.Windows.Forms.NumericUpDown numTrayCols;
        private System.Windows.Forms.Label lblTrayX;
        private System.Windows.Forms.Button btnSkipCell;
        private System.Windows.Forms.Button btnNextRow;
        private System.Windows.Forms.Panel pnlTrayConfig;
        private System.Windows.Forms.Label lblCurrentPos;
        private System.Windows.Forms.Panel pnlTrayGrid;
        private System.Windows.Forms.Label lblFooter;
        private System.Windows.Forms.Button btnSendEmail;
        private System.Windows.Forms.Label lblEmailTo;
        private System.Windows.Forms.TextBox txtEmailDisplay;
        private System.Windows.Forms.Button btnEmailDropdown;
        private System.Windows.Forms.CheckedListBox clbEmailRecipients;
    }
}

