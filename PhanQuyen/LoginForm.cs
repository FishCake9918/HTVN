using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Data; // Giả định namespace chứa DbContext và Models (TaiKhoan, VaiTro)
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Demo_Layout; // Cần dùng Form/Namespace của Piggy Bank
using Piggy_Admin; // Cần dùng Form/Namespace của Piggy Admin


namespace PhanQuyen
{
    public partial class LoginForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;

        private const string ADMIN_ROLE_NAME = "Admin";
        private const string CUSTOMER_ROLE_NAME = "Customer";

        public LoginForm(IDbContextFactory<QLTCCNContext> dbFactory, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string email = txtUsername.Text;
            string password = txtPassword.Text;

            string userRole = AuthenticateAndGetRole(email, password);

            if (userRole != null)
            {
                Form mainFormToRun = null;

                // === LOGIC PHÂN QUYỀN ===
                if (userRole.Equals(ADMIN_ROLE_NAME, StringComparison.OrdinalIgnoreCase))
                {
                    mainFormToRun = _serviceProvider.GetRequiredService<FrmMainAdmin>();
                }
                else
                {
                    mainFormToRun = _serviceProvider.GetRequiredService<Demo_Layout.FrmMain>();
                }

                if (mainFormToRun != null)
                {
                    // 1. Ẩn Form Login (Giữ luồng WinForms chạy)
                    this.Hide();

                    // 2. ĐĂNG KÝ SỰ KIỆN: Khi Form chính mới đóng, ứng dụng sẽ thoát.
                    // Điều này giữ cho luồng WinForms chạy cho đến khi Form này đóng.
                    mainFormToRun.FormClosed += (s, args) => {
                        // 3. ĐÓNG FORM LOGIN (giải phóng bộ nhớ) VÀ GỌI APPLICATION.EXIT()
                        // Application.Exit() sẽ chấm dứt luồng WinForms một cách an toàn.
                        this.Dispose(); // Giải phóng Form Login khỏi bộ nhớ
                        Application.Exit();
                    };

                    // 4. HIỂN THỊ FORM MỚI
                    mainFormToRun.Show();

                    // Không gọi this.Close() ở đây nữa, vì nó đã được xử lý bằng Application.Exit() khi mainFormToRun đóng.
                }
            }
            else
            {
                MessageBox.Show("Email hoặc Mật khẩu không đúng.", "Lỗi Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string AuthenticateAndGetRole(string email, string password)
        {
            using (var dbContext = _dbFactory.CreateDbContext())
            {
                var taiKhoan = dbContext.Set<TaiKhoan>()
                    .Include(tk => tk.VaiTro)
                    .FirstOrDefault(tk =>
                        tk.Email.ToLower() == email.ToLower() &&
                        tk.MatKhau == password);

                if (taiKhoan != null && taiKhoan.VaiTro != null)
                {
                    return taiKhoan.VaiTro.TenVaiTro;
                }

                return null;
            }
        }
    }
}