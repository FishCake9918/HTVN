//using System;
//using System.Windows.Forms;
//using Microsoft.Extensions.DependencyInjection;
//using Data; // Giả định namespace chứa DbContext và Models (TaiKhoan, VaiTro)
//using System.Linq;
//using Microsoft.EntityFrameworkCore;

//namespace PhanQuyen
//{
//    public partial class DangKy : Form
//    {
//        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;

//        // Constructor dùng Dependency Injection
//        public RegistrationForm(IDbContextFactory<QLTCCNContext> dbFactory)
//        {
//            InitializeComponent();
//            _dbFactory = dbFactory;
//        }

//        private void btnDangKy_Click(object sender, EventArgs e)
//        {
//            string email = txtEmail.Text.Trim();
//            string password = txtPassword.Text.Trim();
//            string confirmPassword = txtConfirmPassword.Text.Trim();

//            // 1. Validation cơ bản
//            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
//            {
//                MessageBox.Show("Vui lòng điền đầy đủ Email và Mật khẩu.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            if (password != confirmPassword)
//            {
//                MessageBox.Show("Mật khẩu xác nhận không khớp.", "Lỗi Mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            // 2. Thao tác với Database
//            using (var dbContext = _dbFactory.CreateDbContext())
//            {
//                try
//                {
//                    // Kiểm tra email đã tồn tại chưa
//                    if (dbContext.Set<TaiKhoan>().Any(tk => tk.Email.ToLower() == email.ToLower()))
//                    {
//                        MessageBox.Show("Email đã tồn tại. Vui lòng sử dụng Email khác.", "Đăng ký thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        return;
//                    }

//                    // Tìm MaVaiTro cho Vai trò "Customer"
//                    var vaiTroCustomer = dbContext.Set<VaiTro>()
//                        .FirstOrDefault(vt => vt.TenVaiTro.Equals("Customer", StringComparison.OrdinalIgnoreCase));

//                    if (vaiTroCustomer == null)
//                    {
//                        MessageBox.Show("Không tìm thấy Vai trò 'Customer' trong cơ sở dữ liệu. Vui lòng kiểm tra dữ liệu gốc.", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        return;
//                    }

//                    // Tạo đối tượng TaiKhoan mới
//                    var taiKhoanMoi = new TaiKhoan
//                    {
//                        Email = email,
//                        MatKhau = password, // LƯU Ý: Cần mã hóa mật khẩu trong thực tế
//                        MaVaiTro = vaiTroCustomer.MaVaiTro,
//                    };

//                    dbContext.Set<TaiKhoan>().Add(taiKhoanMoi);
//                    dbContext.SaveChanges();

//                    MessageBox.Show("Đăng ký tài khoản thành công! Bạn có thể đăng nhập ngay bây giờ.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

//                    // Đóng Form Đăng ký để quay lại Form Đăng nhập
//                    this.Close();
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Lỗi trong quá trình đăng ký: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void btnHuy_Click(object sender, EventArgs e)
//        {
//            this.Close();
//        }
//    }
//}