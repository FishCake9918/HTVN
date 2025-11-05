namespace Demo_Layout
{
    partial class QuanLyTaiKhoan
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuanLyTaiKhoan));
            pnlTieuDe = new Panel();
            lblTieuDe = new Label();
            pnlNoiDung = new Panel();
            lblEmail = new Label();
            lblTenNguoiDung = new Label();
            lblTenDangNhap = new Label();
            picAnhDaiDien = new PictureBox();
            btnCapNhatMatKhau = new Button();
            btnXoaTaiKhoan = new Button();
            btnDangXuat = new Button();
            lblThongTinTaiKhoan = new Label();
            pnlTieuDe.SuspendLayout();
            pnlNoiDung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAnhDaiDien).BeginInit();
            SuspendLayout();
            // 
            // pnlTieuDe
            // 
            pnlTieuDe.BackColor = Color.FromArgb(255, 255, 192);
            pnlTieuDe.Controls.Add(lblTieuDe);
            pnlTieuDe.Dock = DockStyle.Top;
            pnlTieuDe.Location = new Point(0, 0);
            pnlTieuDe.Name = "pnlTieuDe";
            pnlTieuDe.Padding = new Padding(20);
            pnlTieuDe.Size = new Size(1514, 140);
            pnlTieuDe.TabIndex = 1;
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Cambria", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTieuDe.ForeColor = Color.Black;
            lblTieuDe.Location = new Point(16, 51);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(471, 57);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "QUẢN LÝ TÀI KHOẢN";
            // 
            // pnlNoiDung
            // 
            pnlNoiDung.BackColor = Color.FromArgb(255, 255, 192);
            pnlNoiDung.Controls.Add(lblThongTinTaiKhoan);
            pnlNoiDung.Controls.Add(lblEmail);
            pnlNoiDung.Controls.Add(lblTenNguoiDung);
            pnlNoiDung.Controls.Add(lblTenDangNhap);
            pnlNoiDung.Controls.Add(picAnhDaiDien);
            pnlNoiDung.Controls.Add(btnCapNhatMatKhau);
            pnlNoiDung.Controls.Add(btnXoaTaiKhoan);
            pnlNoiDung.Controls.Add(btnDangXuat);
            pnlNoiDung.Dock = DockStyle.Fill;
            pnlNoiDung.Location = new Point(0, 140);
            pnlNoiDung.Name = "pnlNoiDung";
            pnlNoiDung.Padding = new Padding(24);
            pnlNoiDung.Size = new Size(1514, 668);
            pnlNoiDung.TabIndex = 0;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEmail.ForeColor = Color.Gray;
            lblEmail.Location = new Point(799, 323);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(97, 38);
            lblEmail.TabIndex = 9;
            lblEmail.Text = "Email: ";
            // 
            // lblTenNguoiDung
            // 
            lblTenNguoiDung.AutoSize = true;
            lblTenNguoiDung.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTenNguoiDung.ForeColor = Color.Black;
            lblTenNguoiDung.Location = new Point(799, 227);
            lblTenNguoiDung.Name = "lblTenNguoiDung";
            lblTenNguoiDung.Size = new Size(267, 45);
            lblTenNguoiDung.TabIndex = 1;
            lblTenNguoiDung.Text = "Tên Người Dùng";
            // 
            // lblTenDangNhap
            // 
            lblTenDangNhap.AutoSize = true;
            lblTenDangNhap.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTenDangNhap.ForeColor = Color.Gray;
            lblTenDangNhap.Location = new Point(799, 285);
            lblTenDangNhap.Name = "lblTenDangNhap";
            lblTenDangNhap.Size = new Size(214, 38);
            lblTenDangNhap.TabIndex = 2;
            lblTenDangNhap.Text = "Tên đăng nhập: ";
            // 
            // picAnhDaiDien
            // 
            picAnhDaiDien.BorderStyle = BorderStyle.FixedSingle;
            picAnhDaiDien.Image = (Image)resources.GetObject("picAnhDaiDien.Image");
            picAnhDaiDien.Location = new Point(446, 227);
            picAnhDaiDien.Name = "picAnhDaiDien";
            picAnhDaiDien.Size = new Size(239, 298);
            picAnhDaiDien.SizeMode = PictureBoxSizeMode.Zoom;
            picAnhDaiDien.TabIndex = 4;
            picAnhDaiDien.TabStop = false;
            // 
            // btnCapNhatMatKhau
            // 
            btnCapNhatMatKhau.BackColor = Color.Goldenrod;
            btnCapNhatMatKhau.Font = new Font("Segoe UI", 10F);
            btnCapNhatMatKhau.ForeColor = Color.White;
            btnCapNhatMatKhau.Location = new Point(1280, 27);
            btnCapNhatMatKhau.Name = "btnCapNhatMatKhau";
            btnCapNhatMatKhau.Size = new Size(184, 50);
            btnCapNhatMatKhau.TabIndex = 5;
            btnCapNhatMatKhau.Text = "Cập nhật mật khẩu";
            btnCapNhatMatKhau.UseVisualStyleBackColor = false;
            btnCapNhatMatKhau.Click += btnCapNhatMatKhau_Click;
            // 
            // btnXoaTaiKhoan
            // 
            btnXoaTaiKhoan.BackColor = Color.IndianRed;
            btnXoaTaiKhoan.Font = new Font("Segoe UI", 10F);
            btnXoaTaiKhoan.ForeColor = Color.White;
            btnXoaTaiKhoan.Location = new Point(1098, 27);
            btnXoaTaiKhoan.Name = "btnXoaTaiKhoan";
            btnXoaTaiKhoan.Size = new Size(140, 50);
            btnXoaTaiKhoan.TabIndex = 6;
            btnXoaTaiKhoan.Text = "Xóa tài khoản";
            btnXoaTaiKhoan.UseVisualStyleBackColor = false;
            btnXoaTaiKhoan.Click += btnXoaTaiKhoan_Click;
            // 
            // btnDangXuat
            // 
            btnDangXuat.BackColor = Color.SteelBlue;
            btnDangXuat.Font = new Font("Segoe UI", 10F);
            btnDangXuat.ForeColor = Color.White;
            btnDangXuat.Location = new Point(937, 27);
            btnDangXuat.Name = "btnDangXuat";
            btnDangXuat.Size = new Size(110, 50);
            btnDangXuat.TabIndex = 7;
            btnDangXuat.Text = "Đăng xuất";
            btnDangXuat.UseVisualStyleBackColor = false;
            btnDangXuat.Click += btnDangXuat_Click;
            // 
            // lblThongTinTaiKhoan
            // 
            lblThongTinTaiKhoan.AutoSize = true;
            lblThongTinTaiKhoan.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblThongTinTaiKhoan.ForeColor = Color.DimGray;
            lblThongTinTaiKhoan.Location = new Point(16, 67);
            lblThongTinTaiKhoan.Name = "lblThongTinTaiKhoan";
            lblThongTinTaiKhoan.Size = new Size(363, 48);
            lblThongTinTaiKhoan.TabIndex = 10;
            lblThongTinTaiKhoan.Text = "Thông tin tài khoản:";
            // 
            // QuanLyTaiKhoan
            // 
            ClientSize = new Size(1514, 808);
            Controls.Add(pnlNoiDung);
            Controls.Add(pnlTieuDe);
            MaximumSize = new Size(1536, 864);
            MinimumSize = new Size(1536, 864);
            Name = "QuanLyTaiKhoan";
            Text = "Quản lý tài khoản cá nhân";
            pnlTieuDe.ResumeLayout(false);
            pnlTieuDe.PerformLayout();
            pnlNoiDung.ResumeLayout(false);
            pnlNoiDung.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picAnhDaiDien).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTieuDe;
        private Label lblTieuDe;
        private Panel pnlNoiDung;
        private Label lblTenNguoiDung;
        private Label lblTenDangNhap;
        private PictureBox picAnhDaiDien;
        private Button btnCapNhatMatKhau;
        private Button btnXoaTaiKhoan;
        private Button btnDangXuat;
        private Label lblEmail;
        private Label lblThongTinTaiKhoan;
    }
}
