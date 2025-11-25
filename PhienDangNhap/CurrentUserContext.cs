using System;

namespace PhienDangNhap
{
    // Class Singleton này giữ thông tin người dùng đang đăng nhập
    public class CurrentUserContext
    {
        public int MaTaiKhoan { get; private set; }
        public string Email { get; private set; }
        public string TenVaiTro { get; private set; }

        // MaAdmin là Primary Key của bảng Admin, dùng làm FK trong bảng ThongBao
        public int? MaAdmin { get; private set; }

        public bool IsAdmin => TenVaiTro.Equals("Admin", StringComparison.OrdinalIgnoreCase);
        public bool IsLoggedIn => MaTaiKhoan > 0;

        // Phương thức này được gọi sau khi đăng nhập thành công
        public void SetCurrentUser(TaiKhoan taiKhoan, int? maAdminValue)
        {
            if (taiKhoan == null)
            {
                ClearUser();
                return;
            }

            this.MaTaiKhoan = taiKhoan.MaTaiKhoan;
            this.Email = taiKhoan.Email;
            this.TenVaiTro = taiKhoan.VaiTro?.TenVaiTro ?? "Unknown";
            this.MaAdmin = maAdminValue; // Gán MaAdmin THẬT từ bảng Admin
        }

        public void ClearUser()
        {
            this.MaTaiKhoan = 0;
            this.Email = null;
            this.TenVaiTro = null;
            this.MaAdmin = null;
        }
    }
}