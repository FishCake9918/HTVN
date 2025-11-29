using System;
using System.Windows.Forms;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Drawing;

namespace Piggy_Admin
{
    public partial class FrmTaiKhoan : Form
    {
        private readonly CurrentUserContext _userContext;
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider;

        public event Action LogoutRequested;

        private bool _isShowingDialog = false; // Cờ chặn sự kiện đóng form
        private System.Windows.Forms.Timer _fadeTimer;
        private bool _isClosing = false;

        public FrmTaiKhoan(CurrentUserContext userContext, IDbContextFactory<QLTCCNContext> dbFactory, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _userContext = userContext;
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;

            // 1. Cấu hình hiệu ứng Fade In/Out
            _fadeTimer = timer1;
            _fadeTimer.Interval = 20;
            _fadeTimer.Tick += new EventHandler(FadeTimer_Tick);

            this.Opacity = 0; // Bắt đầu ẩn
            this.Load += FormTaiKhoan_Load;
            this.Deactivate += FormTaiKhoan_Deactivate; // Tự đóng khi mất focus

            LoadUserData();
        }

        // --- HIỆU ỨNG FADE ---
        private void FormTaiKhoan_Load(object sender, EventArgs e)
        {
            _fadeTimer.Start(); // Bắt đầu hiện dần
        }

        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            if (_isClosing)
            {
                // Đang đóng -> Giảm độ mờ
                if (this.Opacity > 0) this.Opacity -= 0.1;
                else
                {
                    _fadeTimer.Stop();
                    base.Close(); // Đóng thật sự
                }
            }
            else
            {
                // Đang mở -> Tăng độ mờ
                if (this.Opacity < 1) this.Opacity += 0.1;
                else _fadeTimer.Stop();
            }
        }

        public new void Close()
        {
            if (!_isClosing)
            {
                _isClosing = true; // Kích hoạt cờ đóng để Timer xử lý Fade Out
                _fadeTimer.Start();
            }
        }

        private void FormTaiKhoan_Deactivate(object sender, EventArgs e)
        {
            // Nếu đang hiện Dialog con (như xác nhận xóa) thì không tự đóng
            if (_isShowingDialog) return;
            if (_isClosing) return;

            Close(); // Tự đóng khi click ra ngoài
        }

        // --- TẢI DỮ LIỆU NGƯỜI DÙNG ---
        private void LoadUserData()
        {
            if (_userContext.IsLoggedIn)
            {
                if (lblName != null) lblName.Text = _userContext.DisplayName;
                if (lblEmail != null) lblEmail.Text = _userContext.Email;
                if (lblRole != null) lblRole.Text = $"Vai trò: {_userContext.TenVaiTro}";

                // Giao diện khác nhau giữa Admin và User
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
                    panelMain.BackColor = Color.FromArgb(244, 244, 238);
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

        // --- CÁC NÚT CHỨC NĂNG ---

        // Chức năng Đổi mật khẩu
        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            // Tạm gỡ sự kiện Deactivate để form không tự đóng khi mở form con
            this.Deactivate -= FormTaiKhoan_Deactivate;

            try
            {
                using (var frmChangePass = _serviceProvider.GetRequiredService<FrmDoiMatKhau>())
                {
                    frmChangePass.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở form đổi mật khẩu: " + ex.Message);
            }
            finally
            {
                this.Deactivate += FormTaiKhoan_Deactivate; // Gán lại sau khi xong
            }
        }

        // Chức năng Xóa tài khoản
        private void btnXoaTaiKhoan_Click(object sender, EventArgs e)
        {
            _isShowingDialog = true;

            var confirmResult = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa tài khoản này vĩnh viễn? Không thể hoàn tác!",
                "Cảnh báo Xóa Tài Khoản",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Error);

            _isShowingDialog = false;

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

                            _isShowingDialog = true;
                            MessageBox.Show("Xóa tài khoản thành công. Ứng dụng sẽ đăng xuất.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isShowingDialog = false;

                            PerformLogout();
                        }
                        else
                        {
                            this.Focus();
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

        // Chức năng Đăng xuất
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            _isShowingDialog = true;
            var result = MessageBox.Show("Bạn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            _isShowingDialog = false;

            if (result == DialogResult.Yes)
            {
                PerformLogout();
            }
            else
            {
                this.Focus(); // Focus lại để tránh bị đóng
            }
        }

        private void PerformLogout()
        {
            _userContext.ClearUser(); // Xóa phiên đăng nhập
            Close(); // Đóng form hồ sơ
            LogoutRequested?.Invoke(); // Thông báo ra ngoài để đóng form chính
        }
    }
}