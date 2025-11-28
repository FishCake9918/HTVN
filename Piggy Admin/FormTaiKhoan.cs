using System;
using System.Windows.Forms;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Drawing; // Thêm namespace này cho Color

namespace Piggy_Admin
{
    public partial class FormTaiKhoan : Form
    {
        private readonly CurrentUserContext _userContext;
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider;

        // Sự kiện báo hiệu đăng xuất
        public event Action LogoutRequested;

        // --- KHAI BÁO BIẾN CHO HIỆU ỨNG FADE ---
        private System.Windows.Forms.Timer _fadeTimer; // Biến cục bộ để quản lý Timer
        private bool _isClosing = false; // Biến cờ để kiểm soát việc đóng form

        public FormTaiKhoan(CurrentUserContext userContext, IDbContextFactory<QLTCCNContext> dbFactory, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _userContext = userContext;
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;

            // Khởi tạo Timer và thiết lập (Sử dụng timer1 đã kéo thả)
            _fadeTimer = timer1; // Gán Timer đã kéo thả (timer1) vào biến cục bộ
            _fadeTimer.Interval = 20; // Tốc độ fade (20ms/bước)
            _fadeTimer.Tick += new EventHandler(FadeTimer_Tick);

            // 1. CHUẨN BỊ CHO FADE IN (Form bắt đầu với độ mờ = 0)
            this.Opacity = 0;
            this.Load += FormTaiKhoan_Load; // Đăng ký sự kiện Load để bắt đầu Fade In

            // 2. TỰ ĐÓNG KHI MẤT FOCUS
            this.Deactivate += FormTaiKhoan_Deactivate;

            LoadUserData();
        }

        // --- PHƯƠNG THỨC HỖ TRỢ HIỆU ỨNG FADE ---

        private void FormTaiKhoan_Load(object sender, EventArgs e)
        {
            // Bắt đầu Fade In khi Form được tải
            _fadeTimer.Start();
        }

        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            if (_isClosing)
            {
                // HIỆU ỨNG FADE OUT
                if (this.Opacity > 0)
                {
                    this.Opacity -= 0.1; // Giảm độ mờ (Tốc độ 5%)
                }
                else
                {
                    _fadeTimer.Stop(); // Dừng Timer
                    // Đóng Form thực sự
                    base.Close();
                }
            }
            else
            {
                // HIỆU ỨNG FADE IN
                if (this.Opacity < 1)
                {
                    this.Opacity += 0.1; // Tăng độ mờ (Tốc độ 5%)
                }
                else
                {
                    _fadeTimer.Stop(); // Dừng Timer khi Form đã hiển thị hoàn toàn
                }
            }
        }

        // Ghi đè phương thức Close để bắt đầu Fade Out
        public new void Close()
        {
            if (!_isClosing)
            {
                // Thay vì đóng ngay, ta bắt đầu Fade Out
                _isClosing = true;
                _fadeTimer.Start();
            }
        }

        // --- PHƯƠNG THỨC TỰ ĐÓNG KHI MẤT FOCUS (POPUP BEHAVIOR) ---
        private void FormTaiKhoan_Deactivate(object sender, EventArgs e)
        {
            // Nếu đã bắt đầu đóng (do click button hoặc đã gọi Close()), bỏ qua
            if (_isClosing) return;

            // Bắt đầu quá trình Fade Out khi Form mất focus
            // Điều này áp dụng khi người dùng click ra ngoài FormTaiKhoan
            Close();
        }

        // --- CÁC PHƯƠNG THỨC KHÁC ---

        private void LoadUserData()
        {
            if (_userContext.IsLoggedIn)
            {
                if (lblName != null) lblName.Text = _userContext.DisplayName;
                if (lblEmail != null) lblEmail.Text = _userContext.Email;
                if (lblRole != null) lblRole.Text = $"Vai trò: {_userContext.TenVaiTro}";

                if (_userContext.IsAdmin)
                {
                    if (lblTitle != null) lblTitle.Text = "HỒ SƠ QUẢN TRỊ VIÊN";
                    this.Text = "Thông tin Admin";
                    if (lblTitle != null) lblTitle.ForeColor = Color.DarkBlue;
                    try { picAvatar.Image = Properties.Resources.user_setting; } catch { }
                }
                else
                {
                    if (lblTitle != null) lblTitle.Text = "HỒ SƠ CÁ NHÂN";
                    this.Text = "Thông tin người dùng";
                    if (lblTitle != null) lblTitle.ForeColor = Color.SeaGreen;
                    try { picAvatar.Image = Properties.Resources.user; } catch { }
                }

                CenterLabel(lblName);
                CenterLabel(lblEmail);
                CenterLabel(lblRole);
                CenterLabel(lblTitle);
            }
        }

        private void CenterLabel(Label lbl)
        {
            if (lbl == null) return;
            int x = (this.ClientSize.Width - lbl.Width) / 2;
            lbl.Location = new System.Drawing.Point(x, lbl.Location.Y);
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            // QUAN TRỌNG: GỠ BỎ sự kiện Deactivate để Form không tự đóng khi mở FormDoiMatKhau
            this.Deactivate -= FormTaiKhoan_Deactivate;

            try
            {
                // Sử dụng 'using' để đảm bảo form được hủy đúng cách
                using (var frmChangePass = _serviceProvider.GetRequiredService<FormDoiMatKhau>())
                {
                    // FormDoiMatKhau mở dưới dạng dialog, chặn FormTaiKhoan nhưng không kích hoạt Deactivate sau khi gỡ
                    frmChangePass.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở form đổi mật khẩu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // ĐĂNG KÝ LẠI sự kiện Deactivate sau khi Form con đóng
                this.Deactivate += FormTaiKhoan_Deactivate;
            }
        }

        private void btnXoaTaiKhoan_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa tài khoản ADMIN này vĩnh viễn? Hành động này không thể hoàn tác!",
                "Cảnh báo Xóa Tài Khoản",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Error);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    using (var db = _dbFactory.CreateDbContext())
                    {
                        var taiKhoan = db.Set<TaiKhoan>().Find(_userContext.MaTaiKhoan);
                        if (taiKhoan != null)
                        {
                            db.Set<TaiKhoan>().Remove(taiKhoan);
                            db.SaveChanges();

                            MessageBox.Show("Tài khoản đã được xóa thành công. Ứng dụng sẽ đăng xuất.",
                                            "Thành công",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);

                            PerformLogout();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy tài khoản.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                PerformLogout();
            }
        }

        private void PerformLogout()
        {
            // Xóa dữ liệu phiên
            _userContext.ClearUser();

            // Kích hoạt Fade Out (gọi phương thức Close() đã ghi đè)
            Close();

            // Kích hoạt sự kiện đăng xuất 
            LogoutRequested?.Invoke();
        }

        // Xử lý nút [X] hoặc Alt+F4
        private void FormTaiKhoan_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Nếu đang đóng (do gọi Close() đã ghi đè) và Opacity vẫn > 0, 
            // thì hủy việc đóng mặc định để Timer hoàn thành hiệu ứng Fade Out.
            if (_isClosing && Opacity > 0)
            {
                e.Cancel = true;
            }
            // Xử lý trường hợp người dùng đóng bằng nút [X] mà chưa gọi Close()
            else if (Opacity == 1 && e.CloseReason == CloseReason.UserClosing && !_isClosing)
            {
                e.Cancel = true; // Hủy việc đóng mặc định
                // Bắt đầu quá trình Fade Out
                _isClosing = true;
                _fadeTimer.Start();
            }
        }
    }
}