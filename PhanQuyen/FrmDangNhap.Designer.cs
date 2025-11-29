using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace PhanQuyen
{
    partial class FrmDangNhap
    {
        private System.ComponentModel.IContainer components = null;

        // Control cũ
        private ReaLTaiizor.Controls.NightControlBox nightControlBox1;
        private Panel panelMain; // Sẽ dùng làm cột phải (form nhập)
        private Button btnDangKyMoi;
        private Button btnDangNhap;
        private TextBox txtPassword;
        private Label lblPassword;
        private TextBox txtUsername;
        private Label lblUsername;
        private Label lblTitle;

        // CONTROL MỚI (Trang trí)
        private Panel pnlLeft; // Cột trái chứa hình/slogan
        private PictureBox pbLogo; // Nếu có logo

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            nightControlBox1 = new ReaLTaiizor.Controls.NightControlBox();
            panelMain = new Panel();
            btnDangKyMoi = new Button();
            btnDangNhap = new Button();
            txtPassword = new TextBox();
            lblPassword = new Label();
            txtUsername = new TextBox();
            lblUsername = new Label();
            lblTitle = new Label();
            pnlLeft = new Panel();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            panelMain.SuspendLayout();
            pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // nightControlBox1
            // 
            nightControlBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            nightControlBox1.BackColor = Color.Transparent;
            nightControlBox1.CloseHoverColor = Color.FromArgb(199, 80, 80);
            nightControlBox1.CloseHoverForeColor = Color.White;
            nightControlBox1.DefaultLocation = false;
            nightControlBox1.DisableMaximizeColor = Color.FromArgb(105, 105, 105);
            nightControlBox1.DisableMinimizeColor = Color.FromArgb(105, 105, 105);
            nightControlBox1.EnableCloseColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.EnableMaximizeButton = false;
            nightControlBox1.EnableMaximizeColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.EnableMinimizeButton = true;
            nightControlBox1.EnableMinimizeColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.Location = new Point(915, 0);
            nightControlBox1.MaximizeHoverColor = Color.DimGray;
            nightControlBox1.MaximizeHoverForeColor = Color.White;
            nightControlBox1.MinimizeHoverColor = Color.DimGray;
            nightControlBox1.MinimizeHoverForeColor = Color.White;
            nightControlBox1.Name = "nightControlBox1";
            nightControlBox1.Size = new Size(139, 31);
            nightControlBox1.TabIndex = 0;
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.White;
            panelMain.Controls.Add(btnDangKyMoi);
            panelMain.Controls.Add(btnDangNhap);
            panelMain.Controls.Add(txtPassword);
            panelMain.Controls.Add(lblPassword);
            panelMain.Controls.Add(txtUsername);
            panelMain.Controls.Add(lblUsername);
            panelMain.Controls.Add(lblTitle);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(450, 0);
            panelMain.Margin = new Padding(4);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(60);
            panelMain.Size = new Size(675, 675);
            panelMain.TabIndex = 2;
            // 
            // btnDangKyMoi
            // 
            btnDangKyMoi.BackColor = Color.White;
            btnDangKyMoi.Cursor = Cursors.Hand;
            btnDangKyMoi.FlatAppearance.BorderSize = 0;
            btnDangKyMoi.FlatStyle = FlatStyle.Flat;
            btnDangKyMoi.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            btnDangKyMoi.ForeColor = Color.FromArgb(82, 108, 91);
            btnDangKyMoi.Location = new Point(72, 525);
            btnDangKyMoi.Margin = new Padding(4);
            btnDangKyMoi.Name = "btnDangKyMoi";
            btnDangKyMoi.Size = new Size(525, 52);
            btnDangKyMoi.TabIndex = 4;
            btnDangKyMoi.Text = "Chưa có tài khoản? Đăng ký ngay";
            btnDangKyMoi.UseVisualStyleBackColor = false;
            // 
            // btnDangNhap
            // 
            btnDangNhap.BackColor = Color.FromArgb(240, 112, 85);
            btnDangNhap.Cursor = Cursors.Hand;
            btnDangNhap.FlatAppearance.BorderSize = 0;
            btnDangNhap.FlatStyle = FlatStyle.Flat;
            btnDangNhap.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnDangNhap.ForeColor = Color.White;
            btnDangNhap.Location = new Point(72, 435);
            btnDangNhap.Margin = new Padding(4);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(525, 68);
            btnDangNhap.TabIndex = 3;
            btnDangNhap.Text = "ĐĂNG NHẬP";
            btnDangNhap.UseVisualStyleBackColor = false;
            btnDangNhap.Click += btnDangNhap_Click;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.WhiteSmoke;
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Segoe UI", 13F);
            txtPassword.Location = new Point(72, 322);
            txtPassword.Margin = new Padding(4);
            txtPassword.Multiline = true;
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '•';
            txtPassword.Size = new Size(525, 52);
            txtPassword.TabIndex = 2;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPassword.ForeColor = Color.Gray;
            lblPassword.Location = new Point(68, 285);
            lblPassword.Margin = new Padding(4, 0, 4, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(122, 28);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "PASSWORD";
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.WhiteSmoke;
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Font = new Font("Segoe UI", 13F);
            txtUsername.Location = new Point(72, 202);
            txtUsername.Margin = new Padding(4);
            txtUsername.Multiline = true;
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(525, 52);
            txtUsername.TabIndex = 1;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUsername.ForeColor = Color.Gray;
            lblUsername.Location = new Point(68, 165);
            lblUsername.Margin = new Padding(4, 0, 4, 0);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(72, 28);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "EMAIL";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(82, 108, 91);
            lblTitle.Location = new Point(60, 60);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(238, 60);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "WELCOME";
            // 
            // pnlLeft
            // 
            pnlLeft.BackColor = Color.FromArgb(82, 108, 91);
            pnlLeft.Controls.Add(pictureBox1);
            pnlLeft.Controls.Add(label1);
            pnlLeft.Dock = DockStyle.Left;
            pnlLeft.Location = new Point(0, 0);
            pnlLeft.Margin = new Padding(4);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Size = new Size(450, 675);
            pnlLeft.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Top;
            pictureBox1.Image = Properties.Resources.piggy_bank;
            pictureBox1.Location = new Point(0, 313);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(450, 308);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(0, 0);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(450, 313);
            label1.TabIndex = 1;
            label1.Text = "PIGGY BANK\n\nQuản lý chi tiêu\nhiệu quả";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FrmDangNhap
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(1125, 675);
            Controls.Add(nightControlBox1);
            Controls.Add(panelMain);
            Controls.Add(pnlLeft);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "FrmDangNhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng Nhập Hệ Thống";
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private PictureBox pictureBox1;
    }
}