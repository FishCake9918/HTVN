namespace Piggy_Admin
{
    partial class FrmThemSuaTaiKhoan
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblVaiTro;
        private System.Windows.Forms.ComboBox cboVaiTro;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Label lblNote;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblHoTen = new Label();
            txtHoTen = new TextBox();
            lblVaiTro = new Label();
            cboVaiTro = new ComboBox();
            btnLuu = new Button();
            btnHuy = new Button();
            lblNote = new Label();
            nightControlBox1 = new ReaLTaiizor.Controls.NightControlBox();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = SystemColors.ControlDarkDark;
            lblTitle.Location = new Point(48, 49);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(288, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "THÔNG TIN TÀI KHOẢN";
            // 
            // lblEmail
            // 
            lblEmail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEmail.ForeColor = SystemColors.ControlDarkDark;
            lblEmail.Location = new Point(30, 93);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(100, 23);
            lblEmail.TabIndex = 1;
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(30, 118);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(320, 27);
            txtEmail.TabIndex = 2;
            // 
            // lblPassword
            // 
            lblPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPassword.ForeColor = SystemColors.ControlDarkDark;
            lblPassword.Location = new Point(30, 158);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(100, 23);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Mật khẩu:";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(30, 183);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(320, 27);
            txtPassword.TabIndex = 4;
            // 
            // lblHoTen
            // 
            lblHoTen.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHoTen.ForeColor = SystemColors.ControlDarkDark;
            lblHoTen.Location = new Point(30, 223);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(100, 23);
            lblHoTen.TabIndex = 6;
            lblHoTen.Text = "Họ tên:";
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(30, 248);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(320, 27);
            txtHoTen.TabIndex = 7;
            // 
            // lblVaiTro
            // 
            lblVaiTro.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblVaiTro.ForeColor = SystemColors.ControlDarkDark;
            lblVaiTro.Location = new Point(30, 288);
            lblVaiTro.Name = "lblVaiTro";
            lblVaiTro.Size = new Size(100, 23);
            lblVaiTro.TabIndex = 8;
            lblVaiTro.Text = "Vai trò:";
            // 
            // cboVaiTro
            // 
            cboVaiTro.DropDownStyle = ComboBoxStyle.DropDownList;
            cboVaiTro.Location = new Point(30, 313);
            cboVaiTro.Name = "cboVaiTro";
            cboVaiTro.Size = new Size(320, 28);
            cboVaiTro.TabIndex = 9;
            // 
            // btnLuu
            // 
            btnLuu.BackColor = Color.FromArgb(47, 67, 215);
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLuu.ForeColor = Color.White;
            btnLuu.Location = new Point(200, 355);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(150, 40);
            btnLuu.TabIndex = 10;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = false;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnHuy
            // 
            btnHuy.BackColor = Color.FromArgb(47, 67, 215);
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnHuy.ForeColor = Color.White;
            btnHuy.Location = new Point(30, 355);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(150, 40);
            btnHuy.TabIndex = 11;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = false;
            btnHuy.Click += btnHuy_Click;
            // 
            // lblNote
            // 
            lblNote.AutoSize = true;
            lblNote.ForeColor = Color.Gray;
            lblNote.Location = new Point(30, 190);
            lblNote.Name = "lblNote";
            lblNote.Size = new Size(0, 20);
            lblNote.TabIndex = 5;
            // 
            // nightControlBox1
            // 
            nightControlBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            nightControlBox1.BackColor = Color.Transparent;
            nightControlBox1.CloseHoverColor = Color.FromArgb(199, 80, 80);
            nightControlBox1.CloseHoverForeColor = Color.White;
            nightControlBox1.DefaultLocation = true;
            nightControlBox1.DisableMaximizeColor = Color.FromArgb(105, 105, 105);
            nightControlBox1.DisableMinimizeColor = Color.FromArgb(105, 105, 105);
            nightControlBox1.EnableCloseColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.EnableMaximizeButton = true;
            nightControlBox1.EnableMaximizeColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.EnableMinimizeButton = true;
            nightControlBox1.EnableMinimizeColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.Location = new Point(236, 3);
            nightControlBox1.MaximizeHoverColor = Color.FromArgb(15, 255, 255, 255);
            nightControlBox1.MaximizeHoverForeColor = Color.White;
            nightControlBox1.MinimizeHoverColor = Color.FromArgb(15, 255, 255, 255);
            nightControlBox1.MinimizeHoverForeColor = Color.White;
            nightControlBox1.Name = "nightControlBox1";
            nightControlBox1.Size = new Size(139, 31);
            nightControlBox1.TabIndex = 12;
            // 
            // FrmThemSuaTaiKhoan
            // 
            BackColor = Color.FromArgb(213, 217, 247);
            ClientSize = new Size(376, 416);
            Controls.Add(nightControlBox1);
            Controls.Add(lblTitle);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(lblNote);
            Controls.Add(lblHoTen);
            Controls.Add(txtHoTen);
            Controls.Add(lblVaiTro);
            Controls.Add(cboVaiTro);
            Controls.Add(btnLuu);
            Controls.Add(btnHuy);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmThemSuaTaiKhoan";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thêm tài khoản";
            ResumeLayout(false);
            PerformLayout();
        }
        private ReaLTaiizor.Controls.NightControlBox nightControlBox1;
    }
}