using System;
using System.Windows.Forms;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Piggy_Admin
{
    public partial class FormTaiKhoan : Form
    {
        private readonly CurrentUserContext _userContext;
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider; // <-- THÊM BIẾN NÀY

        // Sự kiện báo hiệu đăng xuất
        public event Action LogoutRequested;

        public FormTaiKhoan(CurrentUserContext userContext, IDbContextFactory<QLTCCNContext> dbFactory, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _userContext = userContext;
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider; // <-- QUAN TRỌNG: Dòng này giúp biến không bị null
            LoadUserData();
        }

        private void LoadUserData()
        {
            if (_userContext.IsLoggedIn)
            {
                if (lblName != null) lblName.Text = _userContext.DisplayName;
                if (lblEmail != null) lblEmail.Text = _userContext.Email;
                if (lblRole != null) lblRole.Text = $"Vai trò: {_userContext.TenVaiTro}";
                // 2. TÙY BIẾN GIAO DIỆN THEO VAI TRÒ
                if (_userContext.IsAdmin)
                {
                    // Nếu là ADMIN
                    if (lblTitle != null) lblTitle.Text = "HỒ SƠ QUẢN TRỊ VIÊN"; // Đổi tiêu đề Label
                    this.Text = "Thông tin Admin"; // Đổi tiêu đề cửa sổ

                    // Đổi màu chữ cho Admin (Ví dụ: Xanh đậm)
                    if (lblTitle != null) lblTitle.ForeColor = Color.DarkBlue;

                    // Đổi Avatar Admin
                    // Lưu ý: Cần đảm bảo đã thêm ảnh vào Resources với tên 'admin_avatar'
                    try { picAvatar.Image = Properties.Resources.user_setting; } catch { }
                }
                else
                {
                    // Nếu là NGƯỜI DÙNG
                    if (lblTitle != null) lblTitle.Text = "HỒ SƠ CÁ NHÂN"; // Đổi tiêu đề Label
                    this.Text = "Thông tin người dùng"; // Đổi tiêu đề cửa sổ

                    // Đổi màu chữ cho User (Ví dụ: Xanh lá)
                    if (lblTitle != null) lblTitle.ForeColor = Color.SeaGreen;

                    // Đổi Avatar User
                    // Lưu ý: Cần đảm bảo đã thêm ảnh vào Resources với tên 'user_avatar'
                    try { picAvatar.Image = Properties.Resources.user; } catch { }
                }

                // Căn giữa lại các label sau khi đổi nội dung
                CenterLabel(lblName);
                CenterLabel(lblEmail);
                CenterLabel(lblRole);
                CenterLabel(lblTitle); // Căn giữa cả tiêu đề
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
            try
            {
                // Tạo và mở Form Đổi Mật Khẩu dưới dạng Dialog (cửa sổ con)
                using (var frmChangePass = _serviceProvider.GetRequiredService<FormDoiMatKhau>())
                {
                    frmChangePass.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở form đổi mật khẩu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                            // MessageBox này sẽ hiện và CHỜ bạn bấm OK
                            // Vì ta đã bỏ sự kiện Deactivate ở form cha, nên form này sẽ KHÔNG tự đóng khi hiện MessageBox
                            MessageBox.Show("Tài khoản đã được xóa thành công. Ứng dụng sẽ đăng xuất.",
                                            "Thành công",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);

                            // Sau khi bấm OK, gọi hàm đăng xuất
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

            // Đóng form này trước
            this.Close();

            // Kích hoạt sự kiện -> FrmMainAdmin sẽ nhận được và tự đóng -> Program.cs sẽ mở lại Login
            LogoutRequested?.Invoke();
        }
    }
}