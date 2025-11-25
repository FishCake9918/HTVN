using System;
using System.Windows.Forms;
using Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PhanQuyen
{
    public partial class LoginForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly CurrentUserContext _userContext;

        public LoginForm(
            IDbContextFactory<QLTCCNContext> dbFactory,
            IServiceProvider serviceProvider,
            CurrentUserContext userContext)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
            _userContext = userContext;

            if (this.btnDangKyMoi != null)
            {
                this.btnDangKyMoi.Click += (s, e) => {
                    using (var regForm = _serviceProvider.GetRequiredService<DangKy>())
                    {
                        regForm.ShowDialog(this);
                    }
                };
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string email = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập Email và Mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var authResult = AuthenticateAndGetDetails(email, password);

            if (authResult.TaiKhoan != null)
            {
                _userContext.SetCurrentUser(
                    authResult.TaiKhoan,
                    authResult.MaAdmin,
                    authResult.MaNguoiDung,
                    authResult.HoTen
                );

                // Đăng nhập thành công -> Trả về OK -> Program.cs sẽ mở Form Chính
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Email hoặc Mật khẩu không đúng.", "Lỗi Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class AuthResultDetails
        {
            public TaiKhoan TaiKhoan { get; set; }
            public int? MaAdmin { get; set; }
            public int? MaNguoiDung { get; set; }
            public string HoTen { get; set; }
        }

        private AuthResultDetails AuthenticateAndGetDetails(string email, string password)
        {
            using (var dbContext = _dbFactory.CreateDbContext())
            {
                var tk = dbContext.Set<TaiKhoan>()
                    .Include(t => t.VaiTro)
                    .Include(t => t.Admin)
                    .Include(t => t.NguoiDung)
                    .FirstOrDefault(t =>
                        t.Email.ToLower() == email.ToLower() &&
                        t.MatKhau == password);

                if (tk == null) return new AuthResultDetails { TaiKhoan = null };

                int? maAdmin = null;
                int? maNguoiDung = null;
                string hoTen = tk.Email;

                if (tk.Admin != null)
                {
                    maAdmin = tk.Admin.MaAdmin;
                    hoTen = tk.Admin.HoTenAdmin;
                }
                if (tk.NguoiDung != null)
                {
                    maNguoiDung = tk.NguoiDung.MaNguoiDung;
                    hoTen = tk.NguoiDung.HoTen;
                }

                return new AuthResultDetails
                {
                    TaiKhoan = tk,
                    MaAdmin = maAdmin,
                    MaNguoiDung = maNguoiDung,
                    HoTen = hoTen
                };
            }
        }
    }
}