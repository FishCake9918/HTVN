namespace Demo_Layout
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
            panel2 = new Panel();
            menuStrip1 = new MenuStrip();
            accountToolStripMenuItem = new ToolStripMenuItem();
            signInToolStripMenuItem = new ToolStripMenuItem();
            signOutToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem1 = new ToolStripMenuItem();
            redoToolStripMenuItem1 = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            findSchedulesToolStripMenuItem1 = new ToolStripMenuItem();
            minimizeToolStripMenuItem1 = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            documentationToolStripMenuItem1 = new ToolStripMenuItem();
            keyboardShortcutsToolStripMenuItem1 = new ToolStripMenuItem();
            button9 = new Button();
            button7 = new Button();
            button8 = new Button();
            panel3 = new Panel();
            pictureBox2 = new PictureBox();
            pnlMenu = new Panel();
            panel1 = new Panel();
            picUserProfile = new PictureBox();
            button5 = new Button();
            button2 = new Button();
            button1 = new Button();
            pnlHienThi = new Panel();
            panel2.SuspendLayout();
            menuStrip1.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            pnlMenu.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picUserProfile).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(130, 142, 231);
            panel2.Controls.Add(menuStrip1);
            panel2.Controls.Add(button9);
            panel2.Controls.Add(button7);
            panel2.Controls.Add(button8);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1232, 34);
            panel2.TabIndex = 1;
            panel2.MouseDown += panel2_MouseDown;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.FromArgb(130, 142, 231);
            menuStrip1.Dock = DockStyle.None;
            menuStrip1.Font = new Font("Segoe UI", 9F);
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { accountToolStripMenuItem, editToolStripMenuItem, toolsToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(9, 5);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 1, 0, 1);
            menuStrip1.RenderMode = ToolStripRenderMode.Professional;
            menuStrip1.Size = new Size(252, 26);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // accountToolStripMenuItem
            // 
            accountToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { signInToolStripMenuItem, signOutToolStripMenuItem, exitToolStripMenuItem });
            accountToolStripMenuItem.ForeColor = SystemColors.ControlLightLight;
            accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            accountToolStripMenuItem.Size = new Size(81, 24);
            accountToolStripMenuItem.Text = "Account ";
            // 
            // signInToolStripMenuItem
            // 
            signInToolStripMenuItem.Name = "signInToolStripMenuItem";
            signInToolStripMenuItem.Size = new Size(147, 26);
            signInToolStripMenuItem.Text = "Sign in ";
            // 
            // signOutToolStripMenuItem
            // 
            signOutToolStripMenuItem.Name = "signOutToolStripMenuItem";
            signOutToolStripMenuItem.Size = new Size(147, 26);
            signOutToolStripMenuItem.Text = "Sign out";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(147, 26);
            exitToolStripMenuItem.Text = "Exit";
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem1, redoToolStripMenuItem1 });
            editToolStripMenuItem.ForeColor = SystemColors.ButtonHighlight;
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(49, 24);
            editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem1
            // 
            undoToolStripMenuItem1.Name = "undoToolStripMenuItem1";
            undoToolStripMenuItem1.Size = new Size(128, 26);
            undoToolStripMenuItem1.Text = "Undo";
            // 
            // redoToolStripMenuItem1
            // 
            redoToolStripMenuItem1.Name = "redoToolStripMenuItem1";
            redoToolStripMenuItem1.Size = new Size(128, 26);
            redoToolStripMenuItem1.Text = "Redo";
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { findSchedulesToolStripMenuItem1, minimizeToolStripMenuItem1 });
            toolsToolStripMenuItem.ForeColor = SystemColors.ButtonHighlight;
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(58, 24);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // findSchedulesToolStripMenuItem1
            // 
            findSchedulesToolStripMenuItem1.Name = "findSchedulesToolStripMenuItem1";
            findSchedulesToolStripMenuItem1.Size = new Size(188, 44);
            findSchedulesToolStripMenuItem1.Text = "Find schedules";
            // 
            // minimizeToolStripMenuItem1
            // 
            minimizeToolStripMenuItem1.Name = "minimizeToolStripMenuItem1";
            minimizeToolStripMenuItem1.Size = new Size(188, 44);
            minimizeToolStripMenuItem1.Text = "Minimize\n";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { documentationToolStripMenuItem1, keyboardShortcutsToolStripMenuItem1 });
            helpToolStripMenuItem.ForeColor = SystemColors.Control;
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(55, 24);
            helpToolStripMenuItem.Text = "Help";
            // 
            // documentationToolStripMenuItem1
            // 
            documentationToolStripMenuItem1.Name = "documentationToolStripMenuItem1";
            documentationToolStripMenuItem1.Size = new Size(221, 26);
            documentationToolStripMenuItem1.Text = "Documentation";
            // 
            // keyboardShortcutsToolStripMenuItem1
            // 
            keyboardShortcutsToolStripMenuItem1.Name = "keyboardShortcutsToolStripMenuItem1";
            keyboardShortcutsToolStripMenuItem1.Size = new Size(221, 26);
            keyboardShortcutsToolStripMenuItem1.Text = "Keyboard Shortcuts";
            // 
            // button9
            // 
            button9.Dock = DockStyle.Right;
            button9.FlatAppearance.BorderSize = 0;
            button9.FlatStyle = FlatStyle.Flat;
            button9.Image = (Image)resources.GetObject("button9.Image");
            button9.Location = new Point(1130, 0);
            button9.Name = "button9";
            button9.Size = new Size(34, 34);
            button9.TabIndex = 2;
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // button7
            // 
            button7.Dock = DockStyle.Right;
            button7.FlatAppearance.BorderSize = 0;
            button7.FlatStyle = FlatStyle.Flat;
            button7.Image = (Image)resources.GetObject("button7.Image");
            button7.Location = new Point(1164, 0);
            button7.Name = "button7";
            button7.Size = new Size(34, 34);
            button7.TabIndex = 0;
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button8
            // 
            button8.Dock = DockStyle.Right;
            button8.FlatAppearance.BorderSize = 0;
            button8.FlatStyle = FlatStyle.Flat;
            button8.Image = (Image)resources.GetObject("button8.Image");
            button8.Location = new Point(1198, 0);
            button8.Name = "button8";
            button8.Size = new Size(34, 34);
            button8.TabIndex = 1;
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(47, 67, 215);
            panel3.Controls.Add(pictureBox2);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 34);
            panel3.Name = "panel3";
            panel3.Size = new Size(1232, 51);
            panel3.TabIndex = 2;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(1180, 6);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(40, 40);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = Color.FromArgb(89, 105, 223);
            pnlMenu.Controls.Add(panel1);
            pnlMenu.Controls.Add(button5);
            pnlMenu.Controls.Add(button2);
            pnlMenu.Controls.Add(button1);
            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.Location = new Point(0, 85);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(310, 611);
            pnlMenu.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(213, 217, 247);
            panel1.Controls.Add(picUserProfile);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 531);
            panel1.Name = "panel1";
            panel1.Size = new Size(310, 80);
            panel1.TabIndex = 6;
            // 
            // picUserProfile
            // 
            picUserProfile.BackColor = Color.FromArgb(213, 217, 247);
            picUserProfile.Image = (Image)resources.GetObject("picUserProfile.Image");
            picUserProfile.Location = new Point(12, 12);
            picUserProfile.Name = "picUserProfile";
            picUserProfile.Size = new Size(56, 56);
            picUserProfile.SizeMode = PictureBoxSizeMode.Zoom;
            picUserProfile.TabIndex = 2;
            picUserProfile.TabStop = false;
            picUserProfile.Click += pictureBox1_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.FromArgb(89, 105, 223);
            button5.FlatAppearance.BorderSize = 0;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            button5.ForeColor = Color.Cyan;
            button5.Image = (Image)resources.GetObject("button5.Image");
            button5.ImageAlign = ContentAlignment.MiddleLeft;
            button5.Location = new Point(6, 158);
            button5.Name = "button5";
            button5.Size = new Size(301, 70);
            button5.TabIndex = 4;
            button5.Text = "Quản lý thông báo";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(89, 105, 223);
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            button2.ForeColor = Color.Cyan;
            button2.Image = (Image)resources.GetObject("button2.Image");
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.Location = new Point(9, 82);
            button2.Name = "button2";
            button2.Size = new Size(301, 70);
            button2.TabIndex = 1;
            button2.Text = "  Quản lý tài khoản";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(89, 105, 223);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            button1.ForeColor = Color.Cyan;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Location = new Point(9, 6);
            button1.Name = "button1";
            button1.Size = new Size(301, 70);
            button1.TabIndex = 0;
            button1.Text = "  Báo cáo thống kê";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // pnlHienThi
            // 
            pnlHienThi.BackColor = Color.WhiteSmoke;
            pnlHienThi.Dock = DockStyle.Fill;
            pnlHienThi.Location = new Point(310, 85);
            pnlHienThi.Name = "pnlHienThi";
            pnlHienThi.Size = new Size(922, 611);
            pnlHienThi.TabIndex = 4;
            // 
            // FrmMainAdmin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1232, 696);
            Controls.Add(pnlHienThi);
            Controls.Add(pnlMenu);
            Controls.Add(panel3);
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmMainAdmin";
            Text = "FrmMainAdmin";
            Load += FrmMain_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            pnlMenu.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picUserProfile).EndInit();
            ResumeLayout(false);
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
    }
}