namespace Piggy_Admin
{
    partial class FrmMainAdmin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMainAdmin));
            this.panel2 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.accountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findSchedulesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentationToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.keyboardShortcutsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.button9 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picUserProfile = new System.Windows.Forms.PictureBox();

            // --- KHAI BÁO LABEL MỚI ---
            this.lblTenHienThi = new System.Windows.Forms.Label();
            this.lblVaiTro = new System.Windows.Forms.Label();
            // ---------------------------

            this.button5 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pnlHienThi = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.pnlMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUserProfile)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(142)))), ((int)(((byte)(231)))));
            this.panel2.Controls.Add(this.menuStrip1);
            this.panel2.Controls.Add(this.button9);
            this.panel2.Controls.Add(this.button7);
            this.panel2.Controls.Add(this.button8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1232, 34);
            this.panel2.TabIndex = 1;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(142)))), ((int)(((byte)(231)))));
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(9, 5);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 1, 0, 1);
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(252, 26);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // accountToolStripMenuItem
            // 
            this.accountToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.signInToolStripMenuItem,
            this.signOutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.accountToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            this.accountToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.accountToolStripMenuItem.Text = "Account ";
            // 
            // signInToolStripMenuItem
            // 
            this.signInToolStripMenuItem.Name = "signInToolStripMenuItem";
            this.signInToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
            this.signInToolStripMenuItem.Text = "Sign in ";
            // 
            // signOutToolStripMenuItem
            // 
            this.signOutToolStripMenuItem.Name = "signOutToolStripMenuItem";
            this.signOutToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
            this.signOutToolStripMenuItem.Text = "Sign out";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem1,
            this.redoToolStripMenuItem1});
            this.editToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem1
            // 
            this.undoToolStripMenuItem1.Name = "undoToolStripMenuItem1";
            this.undoToolStripMenuItem1.Size = new System.Drawing.Size(128, 26);
            this.undoToolStripMenuItem1.Text = "Undo";
            // 
            // redoToolStripMenuItem1
            // 
            this.redoToolStripMenuItem1.Name = "redoToolStripMenuItem1";
            this.redoToolStripMenuItem1.Size = new System.Drawing.Size(128, 26);
            this.redoToolStripMenuItem1.Text = "Redo";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findSchedulesToolStripMenuItem1,
            this.minimizeToolStripMenuItem1});
            this.toolsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // findSchedulesToolStripMenuItem1
            // 
            this.findSchedulesToolStripMenuItem1.Name = "findSchedulesToolStripMenuItem1";
            this.findSchedulesToolStripMenuItem1.Size = new System.Drawing.Size(188, 26);
            this.findSchedulesToolStripMenuItem1.Text = "Find schedules";
            // 
            // minimizeToolStripMenuItem1
            // 
            this.minimizeToolStripMenuItem1.Name = "minimizeToolStripMenuItem1";
            this.minimizeToolStripMenuItem1.Size = new System.Drawing.Size(188, 26);
            this.minimizeToolStripMenuItem1.Text = "Minimize";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.documentationToolStripMenuItem1,
            this.keyboardShortcutsToolStripMenuItem1});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // documentationToolStripMenuItem1
            // 
            this.documentationToolStripMenuItem1.Name = "documentationToolStripMenuItem1";
            this.documentationToolStripMenuItem1.Size = new System.Drawing.Size(221, 26);
            this.documentationToolStripMenuItem1.Text = "Documentation";
            // 
            // keyboardShortcutsToolStripMenuItem1
            // 
            this.keyboardShortcutsToolStripMenuItem1.Name = "keyboardShortcutsToolStripMenuItem1";
            this.keyboardShortcutsToolStripMenuItem1.Size = new System.Drawing.Size(221, 26);
            this.keyboardShortcutsToolStripMenuItem1.Text = "Keyboard Shortcuts";
            // 
            // button9
            // 
            this.button9.Dock = System.Windows.Forms.DockStyle.Right;
            this.button9.FlatAppearance.BorderSize = 0;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Image = ((System.Drawing.Image)(resources.GetObject("button9.Image")));
            this.button9.Location = new System.Drawing.Point(1130, 0);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(34, 34);
            this.button9.TabIndex = 2;
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button7
            // 
            this.button7.Dock = System.Windows.Forms.DockStyle.Right;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
            this.button7.Location = new System.Drawing.Point(1164, 0);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(34, 34);
            this.button7.TabIndex = 0;
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Dock = System.Windows.Forms.DockStyle.Right;
            this.button8.FlatAppearance.BorderSize = 0;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Image = ((System.Drawing.Image)(resources.GetObject("button8.Image")));
            this.button8.Location = new System.Drawing.Point(1198, 0);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(34, 34);
            this.button8.TabIndex = 1;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(67)))), ((int)(((byte)(215)))));
            this.panel3.Controls.Add(this.pictureBox2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 34);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1232, 51);
            this.panel3.TabIndex = 2;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(1180, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 40);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(105)))), ((int)(((byte)(223)))));
            this.pnlMenu.Controls.Add(this.panel1);
            this.pnlMenu.Controls.Add(this.button5);
            this.pnlMenu.Controls.Add(this.button2);
            this.pnlMenu.Controls.Add(this.button1);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Location = new System.Drawing.Point(0, 85);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(310, 611);
            this.pnlMenu.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.panel1.Controls.Add(this.lblVaiTro); // Add Label Vai trò
            this.panel1.Controls.Add(this.lblTenHienThi); // Add Label Tên
            this.panel1.Controls.Add(this.picUserProfile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 531);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(310, 80);
            this.panel1.TabIndex = 6;
            // 
            // picUserProfile
            // 
            this.picUserProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.picUserProfile.Image = ((System.Drawing.Image)(resources.GetObject("picUserProfile.Image")));
            this.picUserProfile.Location = new System.Drawing.Point(12, 12);
            this.picUserProfile.Name = "picUserProfile";
            this.picUserProfile.Size = new System.Drawing.Size(56, 56);
            this.picUserProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picUserProfile.TabIndex = 2;
            this.picUserProfile.TabStop = false;
            this.picUserProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picUserProfile.Click += new System.EventHandler(this.pictureBox1_Click);

            // 
            // lblTenHienThi
            // 
            this.lblTenHienThi.AutoSize = true;
            this.lblTenHienThi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTenHienThi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(67)))), ((int)(((byte)(215)))));
            this.lblTenHienThi.Location = new System.Drawing.Point(80, 15);
            this.lblTenHienThi.Name = "lblTenHienThi";
            this.lblTenHienThi.Size = new System.Drawing.Size(150, 23);
            this.lblTenHienThi.TabIndex = 3;
            this.lblTenHienThi.Text = "Tên Người Dùng";
            // 
            // lblVaiTro
            // 
            this.lblVaiTro.AutoSize = true;
            this.lblVaiTro.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblVaiTro.ForeColor = System.Drawing.Color.DimGray;
            this.lblVaiTro.Location = new System.Drawing.Point(80, 40);
            this.lblVaiTro.Name = "lblVaiTro";
            this.lblVaiTro.Size = new System.Drawing.Size(54, 20);
            this.lblVaiTro.TabIndex = 4;
            this.lblVaiTro.Text = "Vai trò";

            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(105)))), ((int)(((byte)(223)))));
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.button5.ForeColor = System.Drawing.Color.Cyan;
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.Location = new System.Drawing.Point(6, 158);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(301, 70);
            this.button5.TabIndex = 4;
            this.button5.Text = "Quản lý thông báo";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(105)))), ((int)(((byte)(223)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.Cyan;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(9, 82);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(301, 70);
            this.button2.TabIndex = 1;
            this.button2.Text = "  Quản lý tài khoản";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(105)))), ((int)(((byte)(223)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.Cyan;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(9, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(301, 70);
            this.button1.TabIndex = 0;
            this.button1.Text = "  Báo cáo thống kê";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pnlHienThi
            // 
            this.pnlHienThi.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlHienThi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHienThi.Location = new System.Drawing.Point(310, 85);
            this.pnlHienThi.Name = "pnlHienThi";
            this.pnlHienThi.Size = new System.Drawing.Size(922, 611);
            this.pnlHienThi.TabIndex = 4;
            // 
            // FrmMainAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 696);
            this.Controls.Add(this.pnlHienThi);
            this.Controls.Add(this.pnlMenu);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmMainAdmin";
            this.Text = "FrmMainAdmin";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.pnlMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUserProfile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem accountToolStripMenuItem;
        private ToolStripMenuItem signInToolStripMenuItem;
        private ToolStripMenuItem signOutToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem1;
        private ToolStripMenuItem redoToolStripMenuItem1;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem findSchedulesToolStripMenuItem1;
        private ToolStripMenuItem minimizeToolStripMenuItem1;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem documentationToolStripMenuItem1;
        private ToolStripMenuItem keyboardShortcutsToolStripMenuItem1;
        private Button button9;
        private Button button7;
        private Button button8;
        private Panel panel3;
        private Panel pnlMenu;
        private Panel panel1;
        private PictureBox picUserProfile;
        private Button button5;
        private Button button2;
        private Button button1;
        private Panel pnlHienThi;
        private PictureBox pictureBox2;

        // Labels mới
        private System.Windows.Forms.Label lblTenHienThi;
        private System.Windows.Forms.Label lblVaiTro;
    }
}