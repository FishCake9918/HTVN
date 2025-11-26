namespace Piggy_Admin
{
    partial class FormTaoCapNhatTaiKhoan
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblVaiTro = new System.Windows.Forms.Label();
            this.cboVaiTro = new System.Windows.Forms.ComboBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.lblNote = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // Style cơ bản (Bạn có thể chỉnh lại sau)
            this.lblTitle.Text = "THÔNG TIN TÀI KHOẢN";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(100, 20);
            this.lblTitle.Size = new System.Drawing.Size(250, 30);

            this.lblEmail.Text = "Email:";
            this.lblEmail.Location = new System.Drawing.Point(30, 70);
            this.txtEmail.Location = new System.Drawing.Point(30, 95);
            this.txtEmail.Size = new System.Drawing.Size(320, 27);

            this.lblPassword.Text = "Mật khẩu:";
            this.lblPassword.Location = new System.Drawing.Point(30, 135);
            this.txtPassword.Location = new System.Drawing.Point(30, 160);
            this.txtPassword.Size = new System.Drawing.Size(320, 27);
            this.txtPassword.PasswordChar = '*';

            this.lblNote.Text = "(Để trống nếu không đổi mật khẩu)";
            this.lblNote.Location = new System.Drawing.Point(30, 190);
            this.lblNote.AutoSize = true;
            this.lblNote.ForeColor = System.Drawing.Color.Gray;

            this.lblHoTen.Text = "Họ tên:";
            this.lblHoTen.Location = new System.Drawing.Point(30, 220);
            this.txtHoTen.Location = new System.Drawing.Point(30, 245);
            this.txtHoTen.Size = new System.Drawing.Size(320, 27);

            this.lblVaiTro.Text = "Vai trò:";
            this.lblVaiTro.Location = new System.Drawing.Point(30, 285);
            this.cboVaiTro.Location = new System.Drawing.Point(30, 310);
            this.cboVaiTro.Size = new System.Drawing.Size(320, 27);
            this.cboVaiTro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.btnLuu.Text = "Lưu";
            this.btnLuu.Location = new System.Drawing.Point(200, 370);
            this.btnLuu.Size = new System.Drawing.Size(150, 40);
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(89, 105, 223);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);

            this.btnHuy.Text = "Hủy";
            this.btnHuy.Location = new System.Drawing.Point(30, 370);
            this.btnHuy.Size = new System.Drawing.Size(150, 40);
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);

            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.lblHoTen);
            this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.lblVaiTro);
            this.Controls.Add(this.cboVaiTro);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnHuy);
            this.Name = "FormTaoCapNhatTaiKhoan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý tài khoản";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}