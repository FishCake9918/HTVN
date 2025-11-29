using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace PhanQuyen
{
    public partial class FrmDangKy : Form
    {
        // ==================================================================================
        // 1. KHAI BÁO BIẾN & KHỞI TẠO
        // ==================================================================================
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IEmailService _emailService; // Service gửi email

        public FrmDangKy(IDbContextFactory<QLTCCNContext> dbFactory, IEmailService emailService)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _emailService = emailService;

            // Chọn sẵn giới tính đầu tiên
            if (cbGioiTinh.Items.Count > 0)
            {
                cbGioiTinh.SelectedIndex = 0;
            }
        }

        // ==================================================================================
        // 2. XỬ LÝ SỰ KIỆN ĐĂNG KÝ (ASYNC)
        // ==================================================================================
        private async void btnDangKy_Click(object sender, EventArgs e)
        {
            // --- A. Thu thập dữ liệu từ form ---
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string gioiTinh = cbGioiTinh.SelectedItem?.ToString() ?? "Khác";
            DateTime ngaySinh = dtpNgaySinh.Value.Date;

            // --- B. Kiểm tra hợp lệ ---

            // 1. Kiểm tra rỗng
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hoTen))
            {
                MessageBox.Show("Vui lòng điền đầy đủ Email, Mật khẩu và Họ tên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra mật khẩu nhập lại
            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.", "Lỗi Mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Kiểm tra độ tuổi (Yêu cầu >= 16 tuổi)
            if (ngaySinh.AddYears(16) > DateTime.Now)
            {
                MessageBox.Show("Bạn phải từ 16 tuổi trở lên để đăng ký.", "Lỗi Tuổi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 4. Kiểm tra độ dài mật khẩu
            if (password.Length < 6)
            {
                MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- C. Lưu dữ liệu vào Database (Dùng Transaction) ---
            using (var dbContext = _dbFactory.CreateDbContext())
            {
                // Lưu cả 2 bảng hoặc không lưu gì cả giúp đảm bảo toàn vẹn dữ liệu
                using (var transaction = await dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Kiểm tra Email đã tồn tại chưa
                        if (dbContext.Set<TaiKhoan>().Any(tk => tk.Email.ToLower() == email.ToLower()))
                        {
                            MessageBox.Show("Email đã tồn tại. Vui lòng sử dụng Email khác.", "Đăng ký thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Lấy ID của vai trò "người dùng"
                        var vaiTroCustomer = dbContext.Set<VaiTro>()
                            .FirstOrDefault(vt => vt.TenVaiTro.ToLower() == "người dùng");

                        if (vaiTroCustomer == null)
                        {
                            MessageBox.Show("Lỗi hệ thống: Không tìm thấy Vai trò 'người dùng'.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Bước 1: Tạo Tài khoản đăng nhập
                        var taiKhoanMoi = new TaiKhoan
                        {
                            Email = email,
                            MatKhau = password,
                            MaVaiTro = vaiTroCustomer.MaVaiTro,
                        };

                        dbContext.Set<TaiKhoan>().Add(taiKhoanMoi);
                        await dbContext.SaveChangesAsync(); // Lưu ngay để lấy ID (PK)

                        // Bước 2: Tạo Hồ sơ người dùng (Liên kết với Tài khoản vừa tạo)
                        var nguoiDungMoi = new NguoiDung
                        {
                            HoTen = hoTen,
                            GioiTinh = gioiTinh,
                            NgaySinh = ngaySinh,
                            MaTaiKhoan = taiKhoanMoi.MaTaiKhoan // Khóa ngoại trỏ về bảng Tài khoản
                        };

                        dbContext.Set<NguoiDung>().Add(nguoiDungMoi);
                        await dbContext.SaveChangesAsync();

                        // Hoàn tất
                        await transaction.CommitAsync();

                        // --- D. Gửi Email thông báo ---
                        bool emailSent = await _emailService.SendRegistrationSuccessEmail(email, hoTen);

                        string msg = emailSent
                            ? "Đăng ký thành công! Email xác nhận đã được gửi."
                            : "Đăng ký thành công, nhưng gửi email thất bại. Vui lòng kiểm tra lại sau.";

                        MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        // Nếu có lỗi, hoàn tác mọi thay đổi trong DB
                        await transaction.RollbackAsync();
                        MessageBox.Show($"Lỗi Database: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Các nút Hủy/Thoát
        private void btnHuy_Click(object sender, EventArgs e) => this.Close();
        private void button1_Click(object sender, EventArgs e) => this.Close();
    }
}