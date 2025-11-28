using System.Windows.Forms;
using System.Drawing;

namespace Piggy_Admin
{
    partial class FormTaiKhoan
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblRole;
        private Label lblName;
        private Label lblEmail;
        private Button btnDoiMatKhau;
        private Button btnXoaTaiKhoan;
        private Button btnDangXuat;
        private PictureBox picAvatar;
        private Panel panelMain;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblTitle = new Label();
            lblRole = new Label();
            lblName = new Label();
            lblEmail = new Label();
            btnDoiMatKhau = new Button();
            btnXoaTaiKhoan = new Button();
            btnDangXuat = new Button();
            picAvatar = new PictureBox();
            panelMain = new Panel();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            panelMain.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.DarkBlue;
            lblTitle.Location = new Point(152, 16);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(101, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "HỒ SƠ";
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblRole.ForeColor = Color.Gray;
            lblRole.Location = new Point(150, 180);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(118, 23);
            lblRole.TabIndex = 2;
            lblRole.Text = "Vai trò: Admin";
            lblRole.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblName.Location = new Point(50, 210);
            lblName.Name = "lblName";
            lblName.Size = new Size(113, 28);
            lblName.TabIndex = 3;
            lblName.Text = "Tên Admin";
            lblName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F);
            lblEmail.Location = new Point(50, 240);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(175, 23);
            lblEmail.TabIndex = 4;
            lblEmail.Text = "admin@example.com";
            lblEmail.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnDoiMatKhau
            // 
            btnDoiMatKhau.BackColor = Color.FromArgb(89, 105, 223);
            btnDoiMatKhau.FlatStyle = FlatStyle.Flat;
            btnDoiMatKhau.ForeColor = Color.White;
            btnDoiMatKhau.Location = new Point(50, 290);
            btnDoiMatKhau.Name = "btnDoiMatKhau";
            btnDoiMatKhau.Size = new Size(300, 40);
            btnDoiMatKhau.TabIndex = 5;
            btnDoiMatKhau.Text = "Đổi Mật Khẩu";
            btnDoiMatKhau.UseVisualStyleBackColor = false;
            btnDoiMatKhau.Click += btnDoiMatKhau_Click;
            // 
            // btnXoaTaiKhoan
            // 
            btnXoaTaiKhoan.BackColor = Color.IndianRed;
            btnXoaTaiKhoan.FlatStyle = FlatStyle.Flat;
            btnXoaTaiKhoan.ForeColor = Color.White;
            btnXoaTaiKhoan.Location = new Point(50, 340);
            btnXoaTaiKhoan.Name = "btnXoaTaiKhoan";
            btnXoaTaiKhoan.Size = new Size(300, 40);
            btnXoaTaiKhoan.TabIndex = 6;
            btnXoaTaiKhoan.Text = "Xóa Tài Khoản";
            btnXoaTaiKhoan.UseVisualStyleBackColor = false;
            btnXoaTaiKhoan.Click += btnXoaTaiKhoan_Click;
            // 
            // btnDangXuat
            // 
            btnDangXuat.BackColor = Color.Gray;
            btnDangXuat.FlatStyle = FlatStyle.Flat;
            btnDangXuat.ForeColor = Color.White;
            btnDangXuat.Location = new Point(50, 390);
            btnDangXuat.Name = "btnDangXuat";
            btnDangXuat.Size = new Size(300, 40);
            btnDangXuat.TabIndex = 7;
            btnDangXuat.Text = "Đăng Xuất";
            btnDangXuat.UseVisualStyleBackColor = false;
            btnDangXuat.Click += btnDangXuat_Click;
            // 
            // picAvatar
            // 
            picAvatar.BackColor = Color.LightGray;
            picAvatar.Location = new Point(150, 70);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(100, 100);
            picAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
            picAvatar.TabIndex = 1;
            picAvatar.TabStop = false;
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.White;
            panelMain.Controls.Add(lblTitle);
            panelMain.Controls.Add(picAvatar);
            panelMain.Controls.Add(lblRole);
            panelMain.Controls.Add(lblName);
            panelMain.Controls.Add(lblEmail);
            panelMain.Controls.Add(btnDoiMatKhau);
            panelMain.Controls.Add(btnXoaTaiKhoan);
            panelMain.Controls.Add(btnDangXuat);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(395, 452);
            panelMain.TabIndex = 0;
            // 
            // timer1
            // 
            timer1.Interval = 10;
            // 
            // FormTaiKhoan
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(395, 452);
            Controls.Add(panelMain);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormTaiKhoan";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thông Tin Tài Khoản";
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ResumeLayout(false);
        }
        private System.Windows.Forms.Timer timer1;
    }
}