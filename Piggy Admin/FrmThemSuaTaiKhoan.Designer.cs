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
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(100, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(250, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "THÔNG TIN TÀI KHOẢN";
            // 
            // lblEmail
            // 
            lblEmail.Location = new Point(30, 70);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(100, 23);
            lblEmail.TabIndex = 1;
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(30, 95);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(320, 31);
            txtEmail.TabIndex = 2;
            // 
            // lblPassword
            // 
            lblPassword.Location = new Point(30, 135);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(100, 23);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Mật khẩu:";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(30, 160);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(320, 31);
            txtPassword.TabIndex = 4;
            // 
            // lblHoTen
            // 
            lblHoTen.Location = new Point(30, 220);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(100, 23);
            lblHoTen.TabIndex = 6;
            lblHoTen.Text = "Họ tên:";
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(30, 245);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(320, 31);
            txtHoTen.TabIndex = 7;
            // 
            // lblVaiTro
            // 
            lblVaiTro.Location = new Point(30, 285);
            lblVaiTro.Name = "lblVaiTro";
            lblVaiTro.Size = new Size(100, 23);
            lblVaiTro.TabIndex = 8;
            lblVaiTro.Text = "Vai trò:";
            // 
            // cboVaiTro
            // 
            cboVaiTro.DropDownStyle = ComboBoxStyle.DropDownList;
            cboVaiTro.Location = new Point(30, 310);
            cboVaiTro.Name = "cboVaiTro";
            cboVaiTro.Size = new Size(320, 33);
            cboVaiTro.TabIndex = 9;
            // 
            // btnLuu
            // 
            btnLuu.BackColor = Color.FromArgb(89, 105, 223);
            btnLuu.ForeColor = Color.White;
            btnLuu.Location = new Point(200, 370);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(150, 40);
            btnLuu.TabIndex = 10;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = false;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnHuy
            // 
            btnHuy.Location = new Point(30, 370);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(150, 40);
            btnHuy.TabIndex = 11;
            btnHuy.Text = "Hủy";
            btnHuy.Click += btnHuy_Click;
            // 
            // lblNote
            // 
            lblNote.AutoSize = true;
            lblNote.ForeColor = Color.Gray;
            lblNote.Location = new Point(30, 190);
            lblNote.Name = "lblNote";
            lblNote.Size = new Size(0, 25);
            lblNote.TabIndex = 5;
            // 
            // FormTaoCapNhatTaiKhoan
            // 
            ClientSize = new Size(400, 450);
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
            Name = "FormTaoCapNhatTaiKhoan";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thêm tài khoản";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}