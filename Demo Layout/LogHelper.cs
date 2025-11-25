using System;
using System.Threading.Tasks;
using Data; // Namespace chứa QLTCCNContext và Model NhatKyHoatDong
using Microsoft.EntityFrameworkCore; // Cần thiết cho IDbContextFactory

namespace Demo_Layout
{
    public static class LogHelper
    {
        /// <summary>
        /// Ghi nhật ký hoạt động vào Database (Chạy ngầm không ảnh hưởng UI)
        /// </summary>
        /// <param name="dbFactory">Nhà máy tạo kết nối DB</param>
        /// <param name="hanhDong">Mã hành động (VD: THEM_GIAO_DICH)</param>
        /// <param name="userId">ID người thực hiện (Có thể null nếu lỗi hệ thống)</param>
        /// <param name="moTa">Mô tả chi tiết (Tùy chọn)</param>
        public static void GhiLog(IDbContextFactory<QLTCCNContext> dbFactory, string hanhDong, int? userId)
        {
            // Chạy trên luồng phụ (Background Thread) để không làm đơ giao diện User
            Task.Run(() =>
            {
                try
                {
                    using (var db = dbFactory.CreateDbContext())
                    {
                        var log = new NhatKyHoatDong
                        {
                            MaNguoiDung = userId,
                            HanhDong = hanhDong,
                            ThoiGian = DateTime.Now // Tự lấy giờ hiện tại
                        };

                        db.NhatKyHoatDongs.Add(log);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    // Nếu ghi log thất bại (mất mạng, lỗi DB...) thì bỏ qua.
                    // Không được để lỗi ghi log làm crash ứng dụng của người dùng.
                    // Có thể ghi tạm ra file text debug nếu cần: 
                    // System.IO.File.AppendAllText("error_log.txt", ex.Message);
                }
            });
        }
    }
}