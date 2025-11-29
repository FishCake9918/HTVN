using System.Drawing;
using System.Windows.Forms;

namespace PhanQuyen
{
    partial class FrmDangKy
    {
        private System.ComponentModel.IContainer components = null;

        // Controls
        private Panel panelMain;
        private Label lblTitle;
        private Button button1; // Nút Close

        // Inputs
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblConfirmPassword;
        private TextBox txtConfirmPassword;
        private Label lblHoTen;
        private TextBox txtHoTen;
        private Label lblGioiTinh;
        private ComboBox cbGioiTinh;
        private Label lblNgaySinh;
        private DateTimePicker dtpNgaySinh;

        // Buttons
        private Button btnDangKy;
        private Button btnHuy;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelMain = new Panel();
            button1 = new Button();
            lblTitle = new Label();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblConfirmPassword = new Label();
            txtConfirmPassword = new TextBox();
            lblHoTen = new Label();
            txtHoTen = new TextBox();
            lblGioiTinh = new Label();
            cbGioiTinh = new ComboBox();
            lblNgaySinh = new Label();
            dtpNgaySinh = new DateTimePicker();
            btnDangKy = new Button();
            btnHuy = new Button();
            panelMain.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.White;
            panelMain.Controls.Add(button1);
            panelMain.Controls.Add(lblTitle);
            panelMain.Controls.Add(lblEmail);
            panelMain.Controls.Add(txtEmail);
            panelMain.Controls.Add(lblPassword);
            panelMain.Controls.Add(txtPassword);
            panelMain.Controls.Add(lblConfirmPassword);
            panelMain.Controls.Add(txtConfirmPassword);
            panelMain.Controls.Add(lblHoTen);
            panelMain.Controls.Add(txtHoTen);
            panelMain.Controls.Add(lblGioiTinh);
            panelMain.Controls.Add(cbGioiTinh);
            panelMain.Controls.Add(lblNgaySinh);
            panelMain.Controls.Add(dtpNgaySinh);
            panelMain.Controls.Add(btnDangKy);
            panelMain.Controls.Add(btnHuy);
            panelMain.Location = new Point(38, 38);
            panelMain.Margin = new Padding(4);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(675, 720);
            panelMain.TabIndex = 0;
            // 
            // button1
            // 
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button1.ForeColor = Color.Gray;
            button1.Location = new Point(615, 8);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(52, 52);
            button1.TabIndex = 10;
            button1.Text = "✕";
            button1.Click += button1_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(82, 108, 91);
            lblTitle.Location = new Point(217, 29);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(242, 65);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "ĐĂNG KÝ";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEmail.ForeColor = Color.Gray;
            lblEmail.Location = new Point(60, 120);
            lblEmail.Margin = new Padding(4, 0, 4, 0);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(64, 28);
            lblEmail.TabIndex = 11;
            lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.WhiteSmoke;
            txtEmail.BorderStyle = BorderStyle.None;
            txtEmail.Font = new Font("Segoe UI", 11F);
            txtEmail.Location = new Point(60, 158);
            txtEmail.Margin = new Padding(4);
            txtEmail.Multiline = true;
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(555, 45);
            txtEmail.TabIndex = 1;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPassword.ForeColor = Color.Gray;
            lblPassword.Location = new Point(60, 218);
            lblPassword.Margin = new Padding(4, 0, 4, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(102, 28);
            lblPassword.TabIndex = 12;
            lblPassword.Text = "Mật khẩu";
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.WhiteSmoke;
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Segoe UI", 11F);
            txtPassword.Location = new Point(60, 255);
            txtPassword.Margin = new Padding(4);
            txtPassword.Multiline = true;
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '•';
            txtPassword.Size = new Size(262, 45);
            txtPassword.TabIndex = 2;
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblConfirmPassword.ForeColor = Color.Gray;
            lblConfirmPassword.Location = new Point(352, 218);
            lblConfirmPassword.Margin = new Padding(4, 0, 4, 0);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(99, 28);
            lblConfirmPassword.TabIndex = 13;
            lblConfirmPassword.Text = "Xác nhận";
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.BackColor = Color.WhiteSmoke;
            txtConfirmPassword.BorderStyle = BorderStyle.None;
            txtConfirmPassword.Font = new Font("Segoe UI", 11F);
            txtConfirmPassword.Location = new Point(352, 255);
            txtConfirmPassword.Margin = new Padding(4);
            txtConfirmPassword.Multiline = true;
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PasswordChar = '•';
            txtConfirmPassword.Size = new Size(262, 45);
            txtConfirmPassword.TabIndex = 3;
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHoTen.ForeColor = Color.Gray;
            lblHoTen.Location = new Point(60, 315);
            lblHoTen.Margin = new Padding(4, 0, 4, 0);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(76, 28);
            lblHoTen.TabIndex = 14;
            lblHoTen.Text = "Họ tên";
            // 
            // txtHoTen
            // 
            txtHoTen.BackColor = Color.WhiteSmoke;
            txtHoTen.BorderStyle = BorderStyle.None;
            txtHoTen.Font = new Font("Segoe UI", 11F);
            txtHoTen.Location = new Point(60, 352);
            txtHoTen.Margin = new Padding(4);
            txtHoTen.Multiline = true;
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(555, 45);
            txtHoTen.TabIndex = 4;
            // 
            // lblGioiTinh
            // 
            lblGioiTinh.AutoSize = true;
            lblGioiTinh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblGioiTinh.ForeColor = Color.Gray;
            lblGioiTinh.Location = new Point(60, 412);
            lblGioiTinh.Margin = new Padding(4, 0, 4, 0);
            lblGioiTinh.Name = "lblGioiTinh";
            lblGioiTinh.Size = new Size(95, 28);
            lblGioiTinh.TabIndex = 15;
            lblGioiTinh.Text = "Giới tính";
            // 
            // cbGioiTinh
            // 
            cbGioiTinh.BackColor = Color.WhiteSmoke;
            cbGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cbGioiTinh.FlatStyle = FlatStyle.Flat;
            cbGioiTinh.Font = new Font("Segoe UI", 11F);
            cbGioiTinh.FormattingEnabled = true;
            cbGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            cbGioiTinh.Location = new Point(60, 450);
            cbGioiTinh.Margin = new Padding(4);
            cbGioiTinh.Name = "cbGioiTinh";
            cbGioiTinh.Size = new Size(260, 38);
            cbGioiTinh.TabIndex = 5;
            // 
            // lblNgaySinh
            // 
            lblNgaySinh.AutoSize = true;
            lblNgaySinh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNgaySinh.ForeColor = Color.Gray;
            lblNgaySinh.Location = new Point(352, 412);
            lblNgaySinh.Margin = new Padding(4, 0, 4, 0);
            lblNgaySinh.Name = "lblNgaySinh";
            lblNgaySinh.Size = new Size(107, 28);
            lblNgaySinh.TabIndex = 16;
            lblNgaySinh.Text = "Ngày sinh";
            // 
            // dtpNgaySinh
            // 
            dtpNgaySinh.Font = new Font("Segoe UI", 11F);
            dtpNgaySinh.Format = DateTimePickerFormat.Short;
            dtpNgaySinh.Location = new Point(352, 450);
            dtpNgaySinh.Margin = new Padding(4);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.Size = new Size(260, 37);
            dtpNgaySinh.TabIndex = 6;
            // 
            // btnDangKy
            // 
            btnDangKy.BackColor = Color.FromArgb(240, 112, 85);
            btnDangKy.Cursor = Cursors.Hand;
            btnDangKy.FlatAppearance.BorderSize = 0;
            btnDangKy.FlatStyle = FlatStyle.Flat;
            btnDangKy.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnDangKy.ForeColor = Color.White;
            btnDangKy.Location = new Point(57, 541);
            btnDangKy.Margin = new Padding(4);
            btnDangKy.Name = "btnDangKy";
            btnDangKy.Size = new Size(555, 68);
            btnDangKy.TabIndex = 7;
            btnDangKy.Text = "HOÀN TẤT ĐĂNG KÝ";
            btnDangKy.UseVisualStyleBackColor = false;
            btnDangKy.Click += btnDangKy_Click;
            // 
            // btnHuy
            // 
            btnHuy.BackColor = Color.WhiteSmoke;
            btnHuy.Cursor = Cursors.Hand;
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnHuy.ForeColor = Color.Gray;
            btnHuy.Location = new Point(57, 631);
            btnHuy.Margin = new Padding(4);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(555, 52);
            btnHuy.TabIndex = 8;
            btnHuy.Text = "Quay lại Đăng nhập";
            btnHuy.UseVisualStyleBackColor = false;
            btnHuy.Click += btnHuy_Click;
            // 
            // FrmDangKy
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(82, 108, 91);
            ClientSize = new Size(750, 809);
            Controls.Add(panelMain);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "FrmDangKy";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Đăng Ký Tài Khoản";
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}