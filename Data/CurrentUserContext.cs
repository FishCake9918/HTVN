using System;

namespace Data
{
    // Class Singleton này giữ thông tin người dùng đang đăng nhập
    // Đặt tại Project Data để cả Piggy Admin và Piggy Bank đều truy cập được
    public class CurrentUserContext
    {
        // Thông tin chung từ bảng TaiKhoan
        public int MaTaiKhoan { get; private set; }
        public string Email { get; private set; }
        public string TenVaiTro { get; private set; }
        public string DisplayName { get; private set; } // Họ tên để hiển thị (Xin chào, ...)

        // Thông tin riêng (Nullable vì user này có thể không phải là user kia)
        public int? MaAdmin { get; private set; }      // PK của bảng Admin
        public int? MaNguoiDung { get; private set; }  // PK của bảng NguoiDung

        // Helper properties
        public bool IsAdmin => string.Equals(TenVaiTro, "Admin", StringComparison.OrdinalIgnoreCase);
        public bool IsLoggedIn => MaTaiKhoan > 0;

        // Hàm nạp dữ liệu sau khi đăng nhập thành công
        public void SetCurrentUser(TaiKhoan taiKhoan, int? maAdmin, int? maNguoiDung, string displayName)
        {
            if (taiKhoan == null)
            {
                ClearUser();
                return;
            }

            this.MaTaiKhoan = taiKhoan.MaTaiKhoan;
            this.Email = taiKhoan.Email;
            this.TenVaiTro = taiKhoan.VaiTro?.TenVaiTro ?? "Unknown";

            this.MaAdmin = maAdmin;
            this.MaNguoiDung = maNguoiDung;

            // Ưu tiên hiển thị Họ tên, nếu không có thì hiện Email
            this.DisplayName = !string.IsNullOrEmpty(displayName) ? displayName : taiKhoan.Email;
        }

        // Hàm đăng xuất / Xóa dữ liệu
        public void ClearUser()
        {
            this.MaTaiKhoan = 0;
            this.Email = null;
            this.TenVaiTro = null;
            this.DisplayName = null;
            this.MaAdmin = null;
            this.MaNguoiDung = null;
        }
    }
}