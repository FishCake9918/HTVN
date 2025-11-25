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
            this.lblTitle = new Label();
            this.lblRole = new Label();
            this.lblName = new Label();
            this.lblEmail = new Label();
            this.btnDoiMatKhau = new Button();
            this.btnXoaTaiKhoan = new Button();
            this.btnDangXuat = new Button();
            this.picAvatar = new PictureBox();
            this.panelMain = new Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();

            // 
            // panelMain
            // 
            this.panelMain.BackColor = Color.White;
            this.panelMain.Controls.Add(this.lblTitle);
            this.panelMain.Controls.Add(this.picAvatar);
            this.panelMain.Controls.Add(this.lblRole);
            this.panelMain.Controls.Add(this.lblName);
            this.panelMain.Controls.Add(this.lblEmail);
            this.panelMain.Controls.Add(this.btnDoiMatKhau);
            this.panelMain.Controls.Add(this.btnXoaTaiKhoan);
            this.panelMain.Controls.Add(this.btnDangXuat);
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.Location = new Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new Size(400, 500);
            this.panelMain.TabIndex = 0;

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.DarkBlue;
            this.lblTitle.Location = new Point(100, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(200, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "HỒ SƠ ADMIN";

            // 
            // picAvatar
            // 
            this.picAvatar.BackColor = Color.LightGray;
            this.picAvatar.Location = new Point(150, 70);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Size = new Size(100, 100);
            this.picAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
            this.picAvatar.TabIndex = 1;
            this.picAvatar.TabStop = false;

            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            this.lblRole.ForeColor = Color.Gray;
            this.lblRole.Location = new Point(150, 180);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new Size(100, 23);
            this.lblRole.TabIndex = 2;
            this.lblRole.Text = "Vai trò: Admin";
            this.lblRole.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblName.Location = new Point(50, 210);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(300, 28);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Tên Admin";
            this.lblName.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new Font("Segoe UI", 10F);
            this.lblEmail.Location = new Point(50, 240);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new Size(300, 23);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "admin@example.com";
            this.lblEmail.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // btnDoiMatKhau
            // 
            this.btnDoiMatKhau.BackColor = Color.FromArgb(89, 105, 223);
            this.btnDoiMatKhau.FlatStyle = FlatStyle.Flat;
            this.btnDoiMatKhau.ForeColor = Color.White;
            this.btnDoiMatKhau.Location = new Point(50, 290);
            this.btnDoiMatKhau.Name = "btnDoiMatKhau";
            this.btnDoiMatKhau.Size = new Size(300, 40);
            this.btnDoiMatKhau.TabIndex = 5;
            this.btnDoiMatKhau.Text = "Đổi Mật Khẩu";
            this.btnDoiMatKhau.UseVisualStyleBackColor = false;
            this.btnDoiMatKhau.Click += new System.EventHandler(this.btnDoiMatKhau_Click);

            // 
            // btnXoaTaiKhoan
            // 
            this.btnXoaTaiKhoan.BackColor = Color.IndianRed;
            this.btnXoaTaiKhoan.FlatStyle = FlatStyle.Flat;
            this.btnXoaTaiKhoan.ForeColor = Color.White;
            this.btnXoaTaiKhoan.Location = new Point(50, 340);
            this.btnXoaTaiKhoan.Name = "btnXoaTaiKhoan";
            this.btnXoaTaiKhoan.Size = new Size(300, 40);
            this.btnXoaTaiKhoan.TabIndex = 6;
            this.btnXoaTaiKhoan.Text = "Xóa Tài Khoản";
            this.btnXoaTaiKhoan.UseVisualStyleBackColor = false;
            this.btnXoaTaiKhoan.Click += new System.EventHandler(this.btnXoaTaiKhoan_Click);

            // 
            // btnDangXuat
            // 
            this.btnDangXuat.BackColor = Color.Gray;
            this.btnDangXuat.FlatStyle = FlatStyle.Flat;
            this.btnDangXuat.ForeColor = Color.White;
            this.btnDangXuat.Location = new Point(50, 390);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new Size(300, 40);
            this.btnDangXuat.TabIndex = 7;
            this.btnDangXuat.Text = "Đăng Xuất";
            this.btnDangXuat.UseVisualStyleBackColor = false;
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);

            // 
            // FormTaiKhoan
            // 
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(400, 500);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTaiKhoan";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Thông Tin Tài Khoản";
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}