using Demo_Layout;
using Microsoft.EntityFrameworkCore;

namespace Demo_Layout
    {
        public partial class QLTCCN_DbContext : DbContext
        {
            public QLTCCN_DbContext() { }

            public QLTCCN_DbContext(DbContextOptions<QLTCCN_DbContext> options) : base(options) { }

            private const string connectionString = @"
                Data Source=LUCAS;
                Database=QLTCCN;Trusted_Connection=True;TrustServerCertificate=True";

            // Khai báo bảng của bạn

            // Đăng ký tất cả 16 bảng
            public virtual DbSet<VAI_TRO> VAI_TRO { get; set; }
            public virtual DbSet<TAI_KHOAN> TAI_KHOAN { get; set; }
            public virtual DbSet<ADMIN> ADMIN { get; set; }
            public virtual DbSet<NGUOI_DUNG> NGUOI_DUNG { get; set; }
            public virtual DbSet<LOAI_TAI_KHOAN> LOAI_TAI_KHOAN { get; set; }
            public virtual DbSet<TAI_KHOAN_THANH_TOAN> TAI_KHOAN_THANH_TOAN { get; set; }
            public virtual DbSet<LOAI_GIAO_DICH> LOAI_GIAO_DICH { get; set; }
            public virtual DbSet<DANH_MUC_CHI_TIEU> DANH_MUC_CHI_TIEU { get; set; }
            public virtual DbSet<DOI_TUONG_GIAO_DICH> DOI_TUONG_GIAO_DICH { get; set; }
            public virtual DbSet<GIAO_DICH> GIAO_DICH { get; set; }
            public virtual DbSet<BANG_NGAN_SACH> BANG_NGAN_SACH { get; set; }
            public virtual DbSet<THONG_BAO> THONG_BAO { get; set; }

            // 4 Bảng trung gian
            public virtual DbSet<DANH_MUC_CHI_TIEU_NGAN_SACH> DANH_MUC_CHI_TIEU_NGAN_SACH { get; set; }
            public virtual DbSet<GIAO_DICH_NGAN_SACH> GIAO_DICH_NGAN_SACH { get; set; }
            public virtual DbSet<TAI_KHOAN_THANH_TOAN_NGAN_SACH> TAI_KHOAN_THANH_TOAN_NGAN_SACH { get; set; }
            public virtual DbSet<DOI_TUONG_GIAO_DICH_NGAN_SACH> DOI_TUONG_GIAO_DICH_NGAN_SACH { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    // !!! THAY THẾ CHUỖI KẾT NỐI CỦA BẠN VÀO ĐÂY !!!
                    // Ví dụ: "Server=.;Database=QLTCCN;Trusted_Connection=True;TrustServerCertificate=True;"
                    optionsBuilder.UseSqlServer(connectionString);
                }
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // 1. Cấu hình Khóa chính phức hợp (Composite Keys) cho bảng trung gian
                modelBuilder.Entity<DANH_MUC_CHI_TIEU_NGAN_SACH>().HasKey(t => new { t.MaDanhMuc, t.MaNganSach });
                modelBuilder.Entity<GIAO_DICH_NGAN_SACH>().HasKey(t => new { t.MaGiaoDich, t.MaNganSach });
                modelBuilder.Entity<TAI_KHOAN_THANH_TOAN_NGAN_SACH>().HasKey(t => new { t.MaTaiKhoanThanhToan, t.MaNganSach });
                modelBuilder.Entity<DOI_TUONG_GIAO_DICH_NGAN_SACH>().HasKey(t => new { t.MaDoiTuongGiaoDich, t.MaNganSach });

                // 2. Cấu hình quan hệ đặc biệt (Self-Referencing)
                modelBuilder.Entity<DANH_MUC_CHI_TIEU>(entity =>
                {
                    // Cấu hình mối quan hệ tự tham chiếu (Cha-Con)
                    entity.HasOne(d => d.DanhMucChaNavigation) // Một mục con...
                          .WithMany(p => p.InverseDanhMucChaNavigation) // ...có một mục cha
                          .HasForeignKey(d => d.DanhMucCha) // Khóa ngoại là 'DanhMucCha'
                          .OnDelete(DeleteBehavior.ClientSetNull); // Tránh lỗi vòng lặp khi xóa
                });

                // 3. Cấu hình Cascade Delete (Xóa người dùng -> Xóa hết dữ liệu liên quan)
                modelBuilder.Entity<TAI_KHOAN_THANH_TOAN>().HasOne(d => d.NguoiDung).WithMany(p => p.TaiKhoanThanhToans).HasForeignKey(d => d.MaNguoiDung).OnDelete(DeleteBehavior.Cascade);
                modelBuilder.Entity<DANH_MUC_CHI_TIEU>().HasOne(d => d.NguoiDung).WithMany(p => p.DanhMucs).HasForeignKey(d => d.MaNguoiDung).OnDelete(DeleteBehavior.Cascade);
                modelBuilder.Entity<DOI_TUONG_GIAO_DICH>().HasOne(d => d.NguoiDung).WithMany(p => p.DoiTuongGiaoDichs).HasForeignKey(d => d.MaNguoiDung).OnDelete(DeleteBehavior.Cascade);
                modelBuilder.Entity<GIAO_DICH>().HasOne(d => d.NguoiDung).WithMany(p => p.GiaoDichs).HasForeignKey(d => d.MaNguoiDung).OnDelete(DeleteBehavior.Cascade); // Cẩn thận: Giao dịch có thể cần giữ lại


                OnModelCreatingPartial(modelBuilder);
            }

            partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        }
}
