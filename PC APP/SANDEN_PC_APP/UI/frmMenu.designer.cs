namespace SANDEN_PC_APP
{
    partial class frmMenu
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenu));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbTanscation = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblPartMaster = new System.Windows.Forms.Label();
            this.picPartMaster = new System.Windows.Forms.PictureBox();
            this.lblStationMaster = new System.Windows.Forms.Label();
            this.picStationMaster = new System.Windows.Forms.PictureBox();
            this.lblGroupMaster = new System.Windows.Forms.Label();
            this.picGroupMaster = new System.Windows.Forms.PictureBox();
            this.lblUserMaster = new System.Windows.Forms.Label();
            this.picUserMaster = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblRework = new System.Windows.Forms.Label();
            this.picRework = new System.Windows.Forms.PictureBox();
            this.lblFinalProcess = new System.Windows.Forms.Label();
            this.picFinalProcess = new System.Windows.Forms.PictureBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lblReport = new System.Windows.Forms.Label();
            this.picReport = new System.Windows.Forms.PictureBox();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.timerAutoLogOut = new System.Windows.Forms.Timer(this.components);
            this.timerReOiling = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.btnMini = new System.Windows.Forms.Button();
            this.picChangePassword = new System.Windows.Forms.PictureBox();
            this.picLogOut = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.tbTanscation.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPartMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStationMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGroupMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUserMaster)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRework)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFinalProcess)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picChangePassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tbTanscation);
            this.panel1.Controls.Add(this.lblWelcome);
            this.panel1.Location = new System.Drawing.Point(8, 56);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1073, 491);
            this.panel1.TabIndex = 8;
            // 
            // tbTanscation
            // 
            this.tbTanscation.Controls.Add(this.tabPage1);
            this.tbTanscation.Controls.Add(this.tabPage2);
            this.tbTanscation.Controls.Add(this.tabPage3);
            this.tbTanscation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTanscation.Location = new System.Drawing.Point(0, 0);
            this.tbTanscation.Name = "tbTanscation";
            this.tbTanscation.SelectedIndex = 0;
            this.tbTanscation.Size = new System.Drawing.Size(1071, 471);
            this.tbTanscation.TabIndex = 140;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblPartMaster);
            this.tabPage1.Controls.Add(this.picPartMaster);
            this.tabPage1.Controls.Add(this.lblStationMaster);
            this.tabPage1.Controls.Add(this.picStationMaster);
            this.tabPage1.Controls.Add(this.lblGroupMaster);
            this.tabPage1.Controls.Add(this.picGroupMaster);
            this.tabPage1.Controls.Add(this.lblUserMaster);
            this.tabPage1.Controls.Add(this.picUserMaster);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1063, 439);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Master";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblPartMaster
            // 
            this.lblPartMaster.AutoSize = true;
            this.lblPartMaster.Enabled = false;
            this.lblPartMaster.Location = new System.Drawing.Point(63, 242);
            this.lblPartMaster.Name = "lblPartMaster";
            this.lblPartMaster.Size = new System.Drawing.Size(85, 19);
            this.lblPartMaster.TabIndex = 23;
            this.lblPartMaster.Text = "Part Master";
            this.lblPartMaster.Visible = false;
            // 
            // picPartMaster
            // 
            this.picPartMaster.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picPartMaster.Enabled = false;
            this.picPartMaster.Image = ((System.Drawing.Image)(resources.GetObject("picPartMaster.Image")));
            this.picPartMaster.Location = new System.Drawing.Point(55, 166);
            this.picPartMaster.Name = "picPartMaster";
            this.picPartMaster.Size = new System.Drawing.Size(100, 73);
            this.picPartMaster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPartMaster.TabIndex = 22;
            this.picPartMaster.TabStop = false;
            this.picPartMaster.Tag = "101";
            this.picPartMaster.Visible = false;
            this.picPartMaster.Click += new System.EventHandler(this.picPartMaster_Click);
            // 
            // lblStationMaster
            // 
            this.lblStationMaster.AutoSize = true;
            this.lblStationMaster.Enabled = false;
            this.lblStationMaster.Location = new System.Drawing.Point(55, 100);
            this.lblStationMaster.Name = "lblStationMaster";
            this.lblStationMaster.Size = new System.Drawing.Size(104, 19);
            this.lblStationMaster.TabIndex = 19;
            this.lblStationMaster.Text = "Station Master";
            // 
            // picStationMaster
            // 
            this.picStationMaster.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picStationMaster.Enabled = false;
            this.picStationMaster.Image = ((System.Drawing.Image)(resources.GetObject("picStationMaster.Image")));
            this.picStationMaster.Location = new System.Drawing.Point(55, 24);
            this.picStationMaster.Name = "picStationMaster";
            this.picStationMaster.Size = new System.Drawing.Size(100, 73);
            this.picStationMaster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picStationMaster.TabIndex = 18;
            this.picStationMaster.TabStop = false;
            this.picStationMaster.Tag = "101";
            this.picStationMaster.Click += new System.EventHandler(this.picStaionMaster_Click);
            // 
            // lblGroupMaster
            // 
            this.lblGroupMaster.AutoSize = true;
            this.lblGroupMaster.Enabled = false;
            this.lblGroupMaster.Location = new System.Drawing.Point(504, 100);
            this.lblGroupMaster.Name = "lblGroupMaster";
            this.lblGroupMaster.Size = new System.Drawing.Size(98, 19);
            this.lblGroupMaster.TabIndex = 3;
            this.lblGroupMaster.Text = "Group Master";
            // 
            // picGroupMaster
            // 
            this.picGroupMaster.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picGroupMaster.Enabled = false;
            this.picGroupMaster.Image = ((System.Drawing.Image)(resources.GetObject("picGroupMaster.Image")));
            this.picGroupMaster.Location = new System.Drawing.Point(503, 24);
            this.picGroupMaster.Name = "picGroupMaster";
            this.picGroupMaster.Size = new System.Drawing.Size(100, 73);
            this.picGroupMaster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picGroupMaster.TabIndex = 2;
            this.picGroupMaster.TabStop = false;
            this.picGroupMaster.Tag = "101";
            this.picGroupMaster.Click += new System.EventHandler(this.picGroupMaster_Click);
            // 
            // lblUserMaster
            // 
            this.lblUserMaster.AutoSize = true;
            this.lblUserMaster.Enabled = false;
            this.lblUserMaster.Location = new System.Drawing.Point(956, 100);
            this.lblUserMaster.Name = "lblUserMaster";
            this.lblUserMaster.Size = new System.Drawing.Size(89, 19);
            this.lblUserMaster.TabIndex = 1;
            this.lblUserMaster.Text = "User Master";
            // 
            // picUserMaster
            // 
            this.picUserMaster.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picUserMaster.Enabled = false;
            this.picUserMaster.Image = ((System.Drawing.Image)(resources.GetObject("picUserMaster.Image")));
            this.picUserMaster.Location = new System.Drawing.Point(951, 24);
            this.picUserMaster.Name = "picUserMaster";
            this.picUserMaster.Size = new System.Drawing.Size(100, 73);
            this.picUserMaster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picUserMaster.TabIndex = 0;
            this.picUserMaster.TabStop = false;
            this.picUserMaster.Tag = "102";
            this.picUserMaster.Click += new System.EventHandler(this.picUserMaster_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblRework);
            this.tabPage2.Controls.Add(this.picRework);
            this.tabPage2.Controls.Add(this.lblFinalProcess);
            this.tabPage2.Controls.Add(this.picFinalProcess);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1063, 439);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Transaction";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblRework
            // 
            this.lblRework.AutoSize = true;
            this.lblRework.Enabled = false;
            this.lblRework.Location = new System.Drawing.Point(188, 93);
            this.lblRework.Name = "lblRework";
            this.lblRework.Size = new System.Drawing.Size(111, 19);
            this.lblRework.TabIndex = 27;
            this.lblRework.Text = "Rework Process";
            // 
            // picRework
            // 
            this.picRework.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picRework.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picRework.Enabled = false;
            this.picRework.Image = global::SANDEN_PC_APP.Properties.Resources.download__1_;
            this.picRework.Location = new System.Drawing.Point(192, 17);
            this.picRework.Name = "picRework";
            this.picRework.Size = new System.Drawing.Size(100, 73);
            this.picRework.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRework.TabIndex = 26;
            this.picRework.TabStop = false;
            this.picRework.Tag = "101";
            this.picRework.Click += new System.EventHandler(this.picRework_Click);
            // 
            // lblFinalProcess
            // 
            this.lblFinalProcess.AutoSize = true;
            this.lblFinalProcess.Enabled = false;
            this.lblFinalProcess.Location = new System.Drawing.Point(35, 93);
            this.lblFinalProcess.Name = "lblFinalProcess";
            this.lblFinalProcess.Size = new System.Drawing.Size(94, 19);
            this.lblFinalProcess.TabIndex = 25;
            this.lblFinalProcess.Text = "Final Process";
            // 
            // picFinalProcess
            // 
            this.picFinalProcess.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picFinalProcess.Enabled = false;
            this.picFinalProcess.Image = ((System.Drawing.Image)(resources.GetObject("picFinalProcess.Image")));
            this.picFinalProcess.Location = new System.Drawing.Point(27, 17);
            this.picFinalProcess.Name = "picFinalProcess";
            this.picFinalProcess.Size = new System.Drawing.Size(100, 73);
            this.picFinalProcess.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFinalProcess.TabIndex = 24;
            this.picFinalProcess.TabStop = false;
            this.picFinalProcess.Tag = "101";
            this.picFinalProcess.Click += new System.EventHandler(this.picFinalProcess_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lblReport);
            this.tabPage3.Controls.Add(this.picReport);
            this.tabPage3.Location = new System.Drawing.Point(4, 28);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1063, 439);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Report";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lblReport
            // 
            this.lblReport.AutoSize = true;
            this.lblReport.Enabled = false;
            this.lblReport.Location = new System.Drawing.Point(53, 101);
            this.lblReport.Name = "lblReport";
            this.lblReport.Size = new System.Drawing.Size(52, 19);
            this.lblReport.TabIndex = 23;
            this.lblReport.Text = "Report";
            // 
            // picReport
            // 
            this.picReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picReport.Enabled = false;
            this.picReport.Image = global::SANDEN_PC_APP.Properties.Resources.iconfinder_AB_testing_3380369;
            this.picReport.Location = new System.Drawing.Point(32, 25);
            this.picReport.Name = "picReport";
            this.picReport.Size = new System.Drawing.Size(100, 73);
            this.picReport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picReport.TabIndex = 22;
            this.picReport.TabStop = false;
            this.picReport.Tag = "101";
            this.picReport.Click += new System.EventHandler(this.picReport_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblWelcome.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.Purple;
            this.lblWelcome.Location = new System.Drawing.Point(0, 471);
            this.lblWelcome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(1071, 18);
            this.lblWelcome.TabIndex = 139;
            this.lblWelcome.Text = "test";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // timerAutoLogOut
            // 
            this.timerAutoLogOut.Tick += new System.EventHandler(this.timerAutoLogOut_Tick);
            // 
            // timerReOiling
            // 
            this.timerReOiling.Tick += new System.EventHandler(this.timerReOiling_Tick);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(62)))), ((int)(((byte)(160)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Cambria", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.AliceBlue;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1089, 51);
            this.label1.TabIndex = 180;
            this.label1.Text = "MAIN MENU";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnMini
            // 
            this.btnMini.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMini.BackColor = System.Drawing.Color.Transparent;
            this.btnMini.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMini.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMini.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btnMini.FlatAppearance.BorderSize = 0;
            this.btnMini.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnMini.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMini.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMini.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMini.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(72)))), ((int)(((byte)(146)))));
            this.btnMini.Image = ((System.Drawing.Image)(resources.GetObject("btnMini.Image")));
            this.btnMini.Location = new System.Drawing.Point(966, 7);
            this.btnMini.Name = "btnMini";
            this.btnMini.Size = new System.Drawing.Size(39, 33);
            this.btnMini.TabIndex = 181;
            this.btnMini.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMini.UseVisualStyleBackColor = false;
            this.btnMini.Click += new System.EventHandler(this.btnMini_Click);
            // 
            // picChangePassword
            // 
            this.picChangePassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picChangePassword.Image = global::SANDEN_PC_APP.Properties.Resources.iconfinder_change_password_63985;
            this.picChangePassword.Location = new System.Drawing.Point(887, 7);
            this.picChangePassword.Name = "picChangePassword";
            this.picChangePassword.Size = new System.Drawing.Size(39, 33);
            this.picChangePassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picChangePassword.TabIndex = 18;
            this.picChangePassword.TabStop = false;
            this.picChangePassword.Click += new System.EventHandler(this.picChangePassword_Click);
            // 
            // picLogOut
            // 
            this.picLogOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picLogOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picLogOut.Image = ((System.Drawing.Image)(resources.GetObject("picLogOut.Image")));
            this.picLogOut.Location = new System.Drawing.Point(1045, 7);
            this.picLogOut.Name = "picLogOut";
            this.picLogOut.Size = new System.Drawing.Size(39, 33);
            this.picLogOut.TabIndex = 16;
            this.picLogOut.TabStop = false;
            this.picLogOut.Click += new System.EventHandler(this.picLogOut_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(861, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Change Password";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(962, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 182;
            this.label3.Text = "Minimize";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(1048, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 183;
            this.label4.Text = "Stop";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(7, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(96, 46);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 262;
            this.pictureBox1.TabStop = false;
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(62)))), ((int)(((byte)(160)))));
            this.ClientSize = new System.Drawing.Size(1089, 552);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnMini);
            this.Controls.Add(this.picChangePassword);
            this.Controls.Add(this.picLogOut);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Calibri", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Menu";
            this.Load += new System.EventHandler(this.frmModelMaster_Load);
            this.panel1.ResumeLayout(false);
            this.tbTanscation.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPartMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStationMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGroupMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUserMaster)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRework)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFinalProcess)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picChangePassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.TabControl tbTanscation;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblGroupMaster;
        private System.Windows.Forms.PictureBox picGroupMaster;
        private System.Windows.Forms.Label lblUserMaster;
        private System.Windows.Forms.PictureBox picUserMaster;
        private System.Windows.Forms.PictureBox picLogOut;
        private System.Windows.Forms.PictureBox picChangePassword;
        private System.Windows.Forms.Timer timerAutoLogOut;
        private System.Windows.Forms.Timer timerReOiling;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStationMaster;
        private System.Windows.Forms.PictureBox picStationMaster;
        private System.Windows.Forms.Label lblPartMaster;
        private System.Windows.Forms.PictureBox picPartMaster;
        private System.Windows.Forms.Button btnMini;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label lblReport;
        private System.Windows.Forms.PictureBox picReport;
        private System.Windows.Forms.Label lblFinalProcess;
        private System.Windows.Forms.PictureBox picFinalProcess;
        private System.Windows.Forms.Label lblRework;
        private System.Windows.Forms.PictureBox picRework;
    }
}