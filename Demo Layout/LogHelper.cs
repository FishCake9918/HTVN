using System;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Demo_Layout
{
    // Lớp tiện ích hỗ trợ ghi lại lịch sử hoạt động (Log) của người dùng
    public static class LogHelper
    {
        public static void GhiLog(IDbContextFactory<QLTCCNContext> dbFactory, string hanhDong, int? userId)
        {
            // 1. KỸ THUẬT CHẠY NGẦM (BACKGROUND THREAD)
            // Đẩy việc ghi dữ liệu sang luồng phụ để giao diện chính không bị đơ (lag) khi bấm nút.
            Task.Run(() =>
            {
                try
                {
                    // 2. TẠO KẾT NỐI RIÊNG BIỆT
                    // Tạo một kết nối CSDL mới hoàn toàn để đảm bảo an toàn, không xung đột với các màn hình đang mở.
                    using (var db = dbFactory.CreateDbContext())
                    {
                        var log = new NhatKyHoatDong
                        {
                            MaNguoiDung = userId,
                            HanhDong = hanhDong,
                            ThoiGian = DateTime.Now
                        };

                        db.NhatKyHoatDongs.Add(log);
                        db.SaveChanges(); // Lưu vào Database
                    }
                }
                catch (Exception)
                {
                    // 3. CƠ CHẾ AN TOÀN (FAIL-SAFE)
                    // Nếu việc ghi log bị lỗi (do mất mạng, lỗi DB...), hệ thống sẽ tự động bỏ qua.
                    // Mục đích: Không để lỗi phụ này làm crash ứng dụng của người dùng.
                }
            });
        }
    }
}