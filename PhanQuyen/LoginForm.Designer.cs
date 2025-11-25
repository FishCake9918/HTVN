using System.Drawing;
using System.Windows.Forms;

namespace PhanQuyen
{
    partial class LoginForm
    {
     
        private System.ComponentModel.IContainer components = null;

        // Khai báo các Controls (CHỈ KHAI BÁO MỘT LẦN DUY NHẤT)
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnDangKyMoi; // <-- FIX LỖI CS0229 (Đảm bảo không trùng lặp)

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelMain = new Panel();
            btnDangKyMoi = new Button();
            btnDangNhap = new Button();
            txtPassword = new TextBox();
            lblPassword = new Label();
            txtUsername = new TextBox();
            lblUsername = new Label();
            lblTitle = new Label();
            nightControlBox1 = new ReaLTaiizor.Controls.NightControlBox();
            panelMain.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(230, 235, 255);
            panelMain.Controls.Add(btnDangKyMoi);
            panelMain.Controls.Add(btnDangNhap);
            panelMain.Controls.Add(txtPassword);
            panelMain.Controls.Add(lblPassword);
            panelMain.Controls.Add(txtUsername);
            panelMain.Controls.Add(lblUsername);
            panelMain.Controls.Add(lblTitle);
            panelMain.Location = new Point(40, 52);
            panelMain.Margin = new Padding(2);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(320, 348);
            panelMain.TabIndex = 0;
            // 
            // btnDangKyMoi
            // 
            btnDangKyMoi.BackColor = Color.FromArgb(172, 180, 239);
            btnDangKyMoi.FlatAppearance.BorderSize = 0;
            btnDangKyMoi.FlatStyle = FlatStyle.Flat;
            btnDangKyMoi.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnDangKyMoi.ForeColor = Color.FromArgb(47, 67, 215);
            btnDangKyMoi.Location = new Point(40, 280);
            btnDangKyMoi.Margin = new Padding(2);
            btnDangKyMoi.Name = "btnDangKyMoi";
            btnDangKyMoi.Size = new Size(120, 40);
            btnDangKyMoi.TabIndex = 4;
            btnDangKyMoi.Text = "Đăng Ký";
            btnDangKyMoi.UseVisualStyleBackColor = false;
            // 
            // btnDangNhap
            // 
            btnDangNhap.BackColor = Color.FromArgb(89, 105, 223);
            btnDangNhap.FlatAppearance.BorderSize = 0;
            btnDangNhap.FlatStyle = FlatStyle.Flat;
            btnDangNhap.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnDangNhap.ForeColor = Color.White;
            btnDangNhap.Location = new Point(160, 280);
            btnDangNhap.Margin = new Padding(2);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(120, 40);
            btnDangNhap.TabIndex = 3;
            btnDangNhap.Text = "Đăng Nhập";
            btnDangNhap.UseVisualStyleBackColor = false;
            btnDangNhap.Click += btnDangNhap_Click;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.Location = new Point(40, 216);
            txtPassword.Margin = new Padding(2);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '•';
            txtPassword.Size = new Size(241, 30);
            txtPassword.TabIndex = 2;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(47, 67, 215);
            lblPassword.Location = new Point(37, 188);
            lblPassword.Margin = new Padding(2, 0, 2, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(91, 23);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "Mật khẩu:";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.Location = new Point(40, 136);
            txtUsername.Margin = new Padding(2);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(241, 30);
            txtUsername.TabIndex = 1;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(47, 67, 215);
            lblUsername.Location = new Point(37, 108);
            lblUsername.Margin = new Padding(2, 0, 2, 0);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(103, 23);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Email/User:";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(47, 67, 215);
            lblTitle.Location = new Point(72, 32);
            lblTitle.Margin = new Padding(2, 0, 2, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(181, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "ĐĂNG NHẬP";
            // 
            // nightControlBox1
            // 
            nightControlBox1.BackColor = Color.Transparent;
            nightControlBox1.CloseHoverColor = Color.FromArgb(199, 80, 80);
            nightControlBox1.CloseHoverForeColor = Color.White;
            nightControlBox1.DefaultLocation = true;
            nightControlBox1.DisableMaximizeColor = Color.FromArgb(105, 105, 105);
            nightControlBox1.DisableMinimizeColor = Color.FromArgb(105, 105, 105);
            nightControlBox1.Dock = DockStyle.Right;
            nightControlBox1.EnableCloseColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.EnableMaximizeButton = true;
            nightControlBox1.EnableMaximizeColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.EnableMinimizeButton = true;
            nightControlBox1.EnableMinimizeColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.Location = new Point(261, 0);
            nightControlBox1.MaximizeHoverColor = Color.FromArgb(15, 255, 255, 255);
            nightControlBox1.MaximizeHoverForeColor = Color.White;
            nightControlBox1.MinimizeHoverColor = Color.FromArgb(15, 255, 255, 255);
            nightControlBox1.MinimizeHoverForeColor = Color.White;
            nightControlBox1.Name = "nightControlBox1";
            nightControlBox1.Size = new Size(139, 31);
            nightControlBox1.TabIndex = 1;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(172, 180, 239);
            ClientSize = new Size(400, 440);
            Controls.Add(nightControlBox1);
            Controls.Add(panelMain);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng Nhập Hệ Thống";
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Controls.NightControlBox nightControlBox1;
    }
}