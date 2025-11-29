using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Data; // Giả định namespace chứa DbContext và Models (TaiKhoan, VaiTro, NguoiDung)
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace PhanQuyen
{
    public partial class FrmDangKy : Form
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IEmailService _emailService;

        public FrmDangKy(IDbContextFactory<QLTCCNContext> dbFactory, IEmailService emailService)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _emailService = emailService;

            // Khởi tạo giá trị mặc định cho ComboBox Giới tính
            if (cbGioiTinh.Items.Count > 0)
            {
                cbGioiTinh.SelectedIndex = 0;
            }
        }

        private async void btnDangKy_Click(object sender, EventArgs e)
        {
            // === 1. THU THẬP VÀ XÁC THỰC DỮ LIỆU ===
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string gioiTinh = cbGioiTinh.SelectedItem?.ToString() ?? "Khác";
            DateTime ngaySinh = dtpNgaySinh.Value.Date;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hoTen))
            {
                MessageBox.Show("Vui lòng điền đầy đủ Email, Mật khẩu và Họ tên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.", "Lỗi Mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra tuổi (chỉ là ví dụ)
            if (ngaySinh.AddYears(16) > DateTime.Now)
            {
                MessageBox.Show("Bạn phải từ 16 tuổi trở lên để đăng ký.", "Lỗi Tuổi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // === 2. THAO TÁC VỚI DATABASE (LƯU 2 BẢNG) ===
            using (var dbContext = _dbFactory.CreateDbContext())
            {
                // Sử dụng Transaction để đảm bảo 2 bảng được lưu thành công hoặc thất bại cùng nhau
                using (var transaction = await dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Kiểm tra email đã tồn tại chưa
                        if (dbContext.Set<TaiKhoan>().Any(tk => tk.Email.ToLower() == email.ToLower()))
                        {
                            MessageBox.Show("Email đã tồn tại. Vui lòng sử dụng Email khác.", "Đăng ký thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Lấy Vai trò "người dùng"
                        var vaiTroCustomer = dbContext.Set<VaiTro>()
                            .FirstOrDefault(vt => vt.TenVaiTro.ToLower() == "người dùng");

                        if (vaiTroCustomer == null)
                        {
                            MessageBox.Show("Không tìm thấy Vai trò 'người dùng' trong CSDL.", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // BƯỚC A: LƯU TAI KHOAN TRƯỚC
                        var taiKhoanMoi = new TaiKhoan
                        {
                            Email = email,
                            MatKhau = password,
                            MaVaiTro = vaiTroCustomer.MaVaiTro,
                        };

                        dbContext.Set<TaiKhoan>().Add(taiKhoanMoi);
                        await dbContext.SaveChangesAsync(); // <-- LƯU ĐỂ LẤY MaTaiKhoan (PK)

                        // BƯỚC B: LƯU NGUOI DUNG VÀ DÙNG FK
                        var nguoiDungMoi = new NguoiDung
                        {
                            HoTen = hoTen,
                            GioiTinh = gioiTinh,
                            NgaySinh = ngaySinh,
                            MaTaiKhoan = taiKhoanMoi.MaTaiKhoan // <-- GÁN MA_TAI_KHOAN VỪA TẠO
                        };

                        dbContext.Set<NguoiDung>().Add(nguoiDungMoi);
                        await dbContext.SaveChangesAsync(); // <-- LƯU NGUOI DUNG

                        // Commit Transaction
                        await transaction.CommitAsync();

                        // === 3. GỬI EMAIL XÁC NHẬN ===
                        bool emailSent = await _emailService.SendRegistrationSuccessEmail(email, hoTen);

                        if (emailSent)
                        {
                            MessageBox.Show("Đăng ký tài khoản thành công! Một email xác nhận đã được gửi.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Đăng ký thành công, nhưng không thể gửi email xác nhận. Vui lòng kiểm tra cấu hình EmailSettings.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        this.Close();

                    }
                    catch (Exception ex)
                    {
                        // Rollback nếu có bất kỳ lỗi nào xảy ra trong quá trình lưu 2 bảng
                        await transaction.RollbackAsync();
                        MessageBox.Show($"Lỗi trong quá trình đăng ký (CSDL): {ex.Message}\nĐảm bảo bảng 'VaiTro' có giá trị 'người dùng'.", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}