using System;
using System.Windows.Forms;
using Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PhanQuyen
{
    public partial class FrmDangNhap : Form
    {
        // ==================================================================================
        // 1. KHAI BÁO BIẾN & KHỞI TẠO
        // ==================================================================================
        private readonly IServiceProvider _serviceProvider;
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly CurrentUserContext _userContext; // Nơi lưu phiên đăng nhập

        public FrmDangNhap(
            IDbContextFactory<QLTCCNContext> dbFactory,
            IServiceProvider serviceProvider,
            CurrentUserContext userContext)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
            _userContext = userContext;

            // Đăng ký sự kiện mở form Đăng Ký
            if (this.btnDangKyMoi != null)
            {
                this.btnDangKyMoi.Click += (s, e) =>
                {
                    // Sử dụng DI để khởi tạo form
                    using (var regForm = _serviceProvider.GetRequiredService<FrmDangKy>())
                    {
                        regForm.ShowDialog(this);
                    }
                };
            }
        }

        // ==================================================================================
        // 2. XỬ LÝ SỰ KIỆN ĐĂNG NHẬP
        // ==================================================================================
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string email = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập Email và Mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Thực hiện xác thực dưới DB
            var authResult = AuthenticateAndGetDetails(email, password);

            if (authResult.TaiKhoan != null)
            {
                // Lưu thông tin người đăng nhập để dùng toàn cục
                _userContext.SetCurrentUser(
                    authResult.TaiKhoan,
                    authResult.MaAdmin,
                    authResult.MaNguoiDung,
                    authResult.HoTen
                );

                // Trả về kết quả để chương trình biết và mở Form chính
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Email hoặc Mật khẩu không đúng.", "Lỗi Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================================================================================
        // 3. LOGIC NGHIỆP VỤ 
        // ==================================================================================

        // Class trả về kết quả xác thực kèm thông tin chi tiết
        private class AuthResultDetails
        {
            public TaiKhoan TaiKhoan { get; set; }
            public int? MaAdmin { get; set; }
            public int? MaNguoiDung { get; set; }
            public string HoTen { get; set; }
        }

        // Hàm truy vấn CSDL để kiểm tra tài khoản và lấy thông tin Admin/User
        private AuthResultDetails AuthenticateAndGetDetails(string email, string password)
        {
            using (var dbContext = _dbFactory.CreateDbContext())
            {
                // Tìm tài khoản khớp Email & Password
                // Dùng Include để lấy luôn thông tin bảng liên quan (Vai trò, Admin, Người dùng)
                var tk = dbContext.Set<TaiKhoan>()
                    .Include(t => t.VaiTro)
                    .Include(t => t.Admin)
                    .Include(t => t.NguoiDung)
                    .FirstOrDefault(t =>
                        t.Email.ToLower() == email.ToLower() &&
                        t.MatKhau == password);

                // Nếu không tìm thấy
                if (tk == null) return new AuthResultDetails { TaiKhoan = null };

                // Phân loại tài khoản để lấy ID và Họ tên tương ứng
                int? maAdmin = null;
                int? maNguoiDung = null;
                string hoTen = tk.Email; // Mặc định lấy email làm tên nếu chưa có hồ sơ

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