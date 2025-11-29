using System;
using System.Windows.Forms;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Drawing;

namespace Piggy_Admin
{
    public partial class FrmDoiMatKhau : Form
    {
        private readonly CurrentUserContext _userContext;
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;

        public FrmDoiMatKhau(CurrentUserContext userContext, IDbContextFactory<QLTCCNContext> dbFactory)
        {
            InitializeComponent();
            _userContext = userContext;
            _dbFactory = dbFactory;
            this.Paint += Vien_Paint; // Vẽ viền cho form
        }

        // Vẽ khung viền thủ công cho form
        private void Vien_Paint(object sender, PaintEventArgs e)
        {
            Color borderColor = Color.Black;
            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            using (Pen pen = new Pen(borderColor, 3))
            {
                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        // Xử lý sự kiện nút Lưu
        private void btnLuu_Click(object sender, EventArgs e)
        {
            string oldPass = txtOldPass.Text.Trim();
            string newPass = txtNewPass.Text.Trim();
            string confirmPass = txtConfirmPass.Text.Trim();

            // 1. Kiểm tra nhập liệu
            if (string.IsNullOrEmpty(oldPass) || string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(confirmPass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra khớp mật khẩu mới
            if (newPass != confirmPass)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Kiểm tra độ dài
            if (newPass.Length < 6)
            {
                MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 4. Cập nhật vào DB
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    var taiKhoan = db.Set<TaiKhoan>().Find(_userContext.MaTaiKhoan);

                    if (taiKhoan != null)
                    {
                        // Kiểm tra mật khẩu cũ
                        if (taiKhoan.MatKhau != oldPass)
                        {
                            MessageBox.Show("Mật khẩu cũ không chính xác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Lưu mật khẩu mới
                        taiKhoan.MatKhau = newPass;
                        db.SaveChanges();

                        MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy tài khoản. Vui lòng đăng nhập lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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