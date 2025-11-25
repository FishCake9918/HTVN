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

        // Sự kiện báo hiệu đăng xuất
        public event Action LogoutRequested;

        public FormTaiKhoan(CurrentUserContext userContext, IDbContextFactory<QLTCCNContext> dbFactory)
        {
            InitializeComponent();
            _userContext = userContext;
            _dbFactory = dbFactory;
            LoadUserData();
        }

        private void LoadUserData()
        {
            if (_userContext.IsLoggedIn)
            {
                if (lblName != null) lblName.Text = _userContext.DisplayName;
                if (lblEmail != null) lblEmail.Text = _userContext.Email;
                if (lblRole != null) lblRole.Text = $"Vai trò: {_userContext.TenVaiTro}";
                CenterLabel(lblName);
                CenterLabel(lblEmail);
                CenterLabel(lblRole);
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
            MessageBox.Show("Tính năng Đổi mật khẩu đang phát triển.", "Thông báo");
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