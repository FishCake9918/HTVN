using System;
using System.Windows.Forms;
using Data; // Chứa CurrentUserContext và Models
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Piggy_Admin
{
    public partial class FormDoiMatKhau : Form
    {
        private readonly CurrentUserContext _userContext;
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;

        // Constructor nhận DI
        public FormDoiMatKhau(CurrentUserContext userContext, IDbContextFactory<QLTCCNContext> dbFactory)
        {
            InitializeComponent();
            _userContext = userContext;
            _dbFactory = dbFactory;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string oldPass = txtOldPass.Text.Trim();
            string newPass = txtNewPass.Text.Trim();
            string confirmPass = txtConfirmPass.Text.Trim();

            // 1. Kiểm tra nhập liệu cơ bản
            if (string.IsNullOrEmpty(oldPass) || string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(confirmPass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra xác nhận mật khẩu mới
            if (newPass != confirmPass)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Kiểm tra độ dài mật khẩu (Tùy chọn)
            if (newPass.Length < 6)
            {
                MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 4. Xử lý đổi mật khẩu trong Database
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    // Tìm tài khoản theo ID người đang đăng nhập (Lấy từ Context)
                    var taiKhoan = db.Set<TaiKhoan>().Find(_userContext.MaTaiKhoan);

                    if (taiKhoan != null)
                    {
                        // Kiểm tra mật khẩu cũ (So sánh chuỗi trực tiếp)
                        // Lưu ý: Nếu hệ thống dùng mã hóa (Hashing), bạn cần Hash oldPass trước khi so sánh.
                        if (taiKhoan.MatKhau != oldPass)
                        {
                            MessageBox.Show("Mật khẩu cũ không chính xác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Cập nhật mật khẩu mới
                        taiKhoan.MatKhau = newPass; // Nếu có mã hóa, hãy Hash newPass ở đây
                        db.SaveChanges();

                        MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin tài khoản. Vui lòng đăng nhập lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}