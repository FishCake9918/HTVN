using System.Drawing;
using System.Windows.Forms;

namespace PhanQuyen
{
    partial class FrmDangKy
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblConfirmPassword;
        private TextBox txtConfirmPassword;
        private Label lblHoTen; // <-- MỚI
        private TextBox txtHoTen; // <-- MỚI
        private Label lblGioiTinh; // <-- MỚI
        private ComboBox cbGioiTinh; // <-- MỚI
        private Label lblNgaySinh; // <-- MỚI
        private DateTimePicker dtpNgaySinh; // <-- MỚI
        private Button btnDangKy;
        private Button btnHuy;
        private Panel panelMain; // Dùng Panel để nhóm controls

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDangKy));
            panelMain = new Panel();
            dtpNgaySinh = new DateTimePicker();
            lblNgaySinh = new Label();
            cbGioiTinh = new ComboBox();
            lblGioiTinh = new Label();
            txtHoTen = new TextBox();
            lblHoTen = new Label();
            btnHuy = new Button();
            btnDangKy = new Button();
            txtConfirmPassword = new TextBox();
            lblConfirmPassword = new Label();
            txtPassword = new TextBox();
            lblPassword = new Label();
            txtEmail = new TextBox();
            lblEmail = new Label();
            lblTitle = new Label();
            button1 = new Button();
            panelMain.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(230, 235, 255);
            panelMain.Controls.Add(button1);
            panelMain.Controls.Add(dtpNgaySinh);
            panelMain.Controls.Add(lblNgaySinh);
            panelMain.Controls.Add(cbGioiTinh);
            panelMain.Controls.Add(lblGioiTinh);
            panelMain.Controls.Add(txtHoTen);
            panelMain.Controls.Add(lblHoTen);
            panelMain.Controls.Add(btnHuy);
            panelMain.Controls.Add(btnDangKy);
            panelMain.Controls.Add(txtConfirmPassword);
            panelMain.Controls.Add(lblConfirmPassword);
            panelMain.Controls.Add(txtPassword);
            panelMain.Controls.Add(lblPassword);
            panelMain.Controls.Add(txtEmail);
            panelMain.Controls.Add(lblEmail);
            panelMain.Controls.Add(lblTitle);
            panelMain.Location = new Point(16, 16);
            panelMain.Margin = new Padding(2, 2, 2, 2);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(368, 544);
            panelMain.TabIndex = 0;
            // 
            // dtpNgaySinh
            // 
            dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            dtpNgaySinh.Format = DateTimePickerFormat.Custom;
            dtpNgaySinh.Location = new Point(40, 409);
            dtpNgaySinh.Margin = new Padding(2, 2, 2, 2);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.Size = new Size(289, 27);
            dtpNgaySinh.TabIndex = 6;
            // 
            // lblNgaySinh
            // 
            lblNgaySinh.AutoSize = true;
            lblNgaySinh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNgaySinh.Location = new Point(37, 381);
            lblNgaySinh.Margin = new Padding(2, 0, 2, 0);
            lblNgaySinh.Name = "lblNgaySinh";
            lblNgaySinh.Size = new Size(89, 23);
            lblNgaySinh.TabIndex = 14;
            lblNgaySinh.Text = "Ngày sinh";
            // 
            // cbGioiTinh
            // 
            cbGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cbGioiTinh.FormattingEnabled = true;
            cbGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            cbGioiTinh.Location = new Point(175, 344);
            cbGioiTinh.Margin = new Padding(2, 2, 2, 2);
            cbGioiTinh.Name = "cbGioiTinh";
            cbGioiTinh.Size = new Size(153, 28);
            cbGioiTinh.TabIndex = 5;
            // 
            // lblGioiTinh
            // 
            lblGioiTinh.AutoSize = true;
            lblGioiTinh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblGioiTinh.Location = new Point(36, 344);
            lblGioiTinh.Margin = new Padding(2, 0, 2, 0);
            lblGioiTinh.Name = "lblGioiTinh";
            lblGioiTinh.Size = new Size(80, 23);
            lblGioiTinh.TabIndex = 12;
            lblGioiTinh.Text = "Giới tính";
            // 
            // txtHoTen
            // 
            txtHoTen.Font = new Font("Segoe UI", 10F);
            txtHoTen.Location = new Point(40, 300);
            txtHoTen.Margin = new Padding(2, 2, 2, 2);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(289, 30);
            txtHoTen.TabIndex = 4;
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHoTen.Location = new Point(37, 272);
            lblHoTen.Margin = new Padding(2, 0, 2, 0);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(65, 23);
            lblHoTen.TabIndex = 10;
            lblHoTen.Text = "Họ Tên";
            // 
            // btnHuy
            // 
            btnHuy.BackColor = Color.LightGray;
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnHuy.ForeColor = Color.Black;
            btnHuy.Location = new Point(40, 464);
            btnHuy.Margin = new Padding(2, 2, 2, 2);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(128, 40);
            btnHuy.TabIndex = 8;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = false;
            btnHuy.Click += btnHuy_Click;
            // 
            // btnDangKy
            // 
            btnDangKy.BackColor = Color.FromArgb(89, 105, 223);
            btnDangKy.FlatAppearance.BorderSize = 0;
            btnDangKy.FlatStyle = FlatStyle.Flat;
            btnDangKy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDangKy.ForeColor = Color.White;
            btnDangKy.Location = new Point(200, 464);
            btnDangKy.Margin = new Padding(2, 2, 2, 2);
            btnDangKy.Name = "btnDangKy";
            btnDangKy.Size = new Size(128, 40);
            btnDangKy.TabIndex = 7;
            btnDangKy.Text = "Đăng Ký";
            btnDangKy.UseVisualStyleBackColor = false;
            btnDangKy.Click += btnDangKy_Click;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Font = new Font("Segoe UI", 10F);
            txtConfirmPassword.Location = new Point(40, 240);
            txtConfirmPassword.Margin = new Padding(2, 2, 2, 2);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PasswordChar = '•';
            txtConfirmPassword.Size = new Size(289, 30);
            txtConfirmPassword.TabIndex = 3;
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblConfirmPassword.Location = new Point(37, 212);
            lblConfirmPassword.Margin = new Padding(2, 0, 2, 0);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(163, 23);
            lblConfirmPassword.TabIndex = 5;
            lblConfirmPassword.Text = "Xác nhận mật khẩu";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.Location = new Point(40, 171);
            txtPassword.Margin = new Padding(2, 2, 2, 2);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '•';
            txtPassword.Size = new Size(289, 30);
            txtPassword.TabIndex = 2;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPassword.Location = new Point(37, 143);
            lblPassword.Margin = new Padding(2, 0, 2, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(86, 23);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Mật khẩu";
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.Location = new Point(40, 104);
            txtEmail.Margin = new Padding(2, 2, 2, 2);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(289, 30);
            txtEmail.TabIndex = 1;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEmail.Location = new Point(37, 76);
            lblEmail.Margin = new Padding(2, 0, 2, 0);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(54, 23);
            lblEmail.TabIndex = 1;
            lblEmail.Text = "Email";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(47, 67, 215);
            lblTitle.Location = new Point(112, 24);
            lblTitle.Margin = new Padding(2, 0, 2, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(137, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "ĐĂNG KÝ";
            // 
            // button1
            // 
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(324, 15);
            button1.Name = "button1";
            button1.Size = new Size(29, 29);
            button1.TabIndex = 15;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FrmDangKy
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(172, 180, 239);
            ClientSize = new Size(400, 576);
            Controls.Add(panelMain);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2, 2, 2, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmDangKy";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Đăng Ký Tài Khoản Mới";
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private Button button1;
    }
}