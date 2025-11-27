// QLTCCNContext.cs (Trong Project Data)
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class QLTCCNContext : DbContext
    {
        public QLTCCNContext(DbContextOptions<QLTCCNContext> options) : base(options) { }

        // --- Danh sách DbSet (18 bảng) ---
        public DbSet<VaiTro> VaiTros { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<LoaiTaiKhoan> LoaiTaiKhoans { get; set; }
        public DbSet<TaiKhoanThanhToan> TaiKhoanThanhToans { get; set; }
        public DbSet<LoaiGiaoDich> LoaiGiaoDichs { get; set; }
        public DbSet<DanhMucChiTieu> DanhMucChiTieus { get; set; }
        public DbSet<DoiTuongGiaoDich> DoiTuongGiaoDichs { get; set; }
        public DbSet<GiaoDich> GiaoDichs { get; set; }
        public DbSet<BangNganSach> BangNganSachs { get; set; }
        public DbSet<ThongBao> ThongBaos { get; set; }

        // >>> [THÊM MỚI] BẢNG NHẬT KÝ HOẠT ĐỘNG <<<
        public DbSet<NhatKyHoatDong> NhatKyHoatDongs { get; set; }

        // Bảng Trung Gian (Junction Tables)
        public DbSet<DanhMucChiTieuNganSach> DanhMucChiTieuNganSachs { get; set; }
        public DbSet<GiaoDichNganSach> GiaoDichNganSachs { get; set; }
        public DbSet<TaiKhoanThanhToanNganSach> TaiKhoanThanhToanNganSachs { get; set; }
        public DbSet<DoiTuongGiaoDichNganSach> DoiTuongGiaoDichNganSachs { get; set; }

        // --- Cấu hình Fluent API (Khóa kép & Hành vi xóa) ---
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. Khóa kép (Composite Keys) cho 4 bảng trung gian
            modelBuilder.Entity<DanhMucChiTieuNganSach>().HasKey(e => new { e.MaDanhMuc, e.MaNganSach });
            modelBuilder.Entity<GiaoDichNganSach>().HasKey(e => new { e.MaGiaoDich, e.MaNganSach });
            modelBuilder.Entity<TaiKhoanThanhToanNganSach>().HasKey(e => new { e.MaTaiKhoanThanhToan, e.MaNganSach });
            modelBuilder.Entity<DoiTuongGiaoDichNganSach>().HasKey(e => new { e.MaDoiTuongGiaoDich, e.MaNganSach });

            // 2. Cấu hình hành vi Xóa (ON DELETE CASCADE/SET NULL)

            // ADMIN - TAI_KHOAN (CASCADE) - Quan hệ 1-1
            modelBuilder.Entity<Admin>()
                .HasOne(a => a.TaiKhoan)
                .WithOne(t => t.Admin)
                .HasForeignKey<Admin>(a => a.MaTaiKhoan)
                .OnDelete(DeleteBehavior.Cascade);

            // NGUOI_DUNG - TAI_KHOAN (CASCADE) - Quan hệ 1-1
            modelBuilder.Entity<NguoiDung>()
                .HasOne(n => n.TaiKhoan)
                .WithOne(t => t.NguoiDung)
                .HasForeignKey<NguoiDung>(n => n.MaTaiKhoan)
                .OnDelete(DeleteBehavior.Cascade);

            // THONG_BAO - ADMIN (SET NULL)
            modelBuilder.Entity<ThongBao>()
               .HasOne(tb => tb.Admin)
               .WithMany(a => a.ThongBaos)
               .HasForeignKey(tb => tb.MaAdmin)
               .OnDelete(DeleteBehavior.SetNull);

            // >>> [THÊM MỚI] CẤU HÌNH NHAT_KY_HOAT_DONG - NGUOI_DUNG (SET NULL) <<<
            // Khi xóa Người dùng, Log không bị xóa mà MaNguoiDung sẽ về NULL
            modelBuilder.Entity<NhatKyHoatDong>()
                .HasOne(nk => nk.NguoiDung)
                .WithMany() // Một người dùng có nhiều log (không cần khai báo List bên NguoiDung cũng được)
                .HasForeignKey(nk => nk.MaNguoiDung)
                .OnDelete(DeleteBehavior.SetNull);

            // Cấu hình các CASCADE cho các bảng trung gian (phù hợp với scripts SQL của bạn)

            // DANH MỤC - NGÂN SÁCH (CASCADE)
            modelBuilder.Entity<DanhMucChiTieuNganSach>()
                .HasOne(dm => dm.DanhMucChiTieu)
                .WithMany(d => d.DanhMucChiTieuNganSachs)
                .HasForeignKey(dm => dm.MaDanhMuc)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DanhMucChiTieuNganSach>()
                .HasOne(ns => ns.BangNganSach)
                .WithMany(n => n.DanhMucChiTieuNganSachs)
                .HasForeignKey(ns => ns.MaNganSach)
                .OnDelete(DeleteBehavior.Cascade);

            // GIAO DỊCH - NGÂN SÁCH (CASCADE)
            modelBuilder.Entity<GiaoDichNganSach>()
                .HasOne(gd => gd.GiaoDich)
                .WithMany(g => g.GiaoDichNganSachs)
                .HasForeignKey(gd => gd.MaGiaoDich)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GiaoDichNganSach>()
                .HasOne(ns => ns.BangNganSach)
                .WithMany(n => n.GiaoDichNganSachs)
                .HasForeignKey(ns => ns.MaNganSach)
                .OnDelete(DeleteBehavior.Cascade);

            // TÀI KHOẢN THANH TOÁN - NGÂN SÁCH (CASCADE)
            modelBuilder.Entity<TaiKhoanThanhToanNganSach>()
                .HasOne(tk => tk.TaiKhoanThanhToan)
                .WithMany(t => t.TaiKhoanThanhToanNganSachs)
                .HasForeignKey(tk => tk.MaTaiKhoanThanhToan)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaiKhoanThanhToanNganSach>()
                .HasOne(ns => ns.BangNganSach)
                .WithMany(n => n.TaiKhoanThanhToanNganSachs)
                .HasForeignKey(ns => ns.MaNganSach)
                .OnDelete(DeleteBehavior.Cascade);

            // ĐỐI TƯỢNG GIAO DỊCH - NGÂN SÁCH (CASCADE)
            modelBuilder.Entity<DoiTuongGiaoDichNganSach>()
                .HasOne(dt => dt.DoiTuongGiaoDich)
                .WithMany(d => d.DoiTuongGiaoDichNganSachs)
                .HasForeignKey(dt => dt.MaDoiTuongGiaoDich)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DoiTuongGiaoDichNganSach>()
                .HasOne(ns => ns.BangNganSach)
                .WithMany(n => n.DoiTuongGiaoDichNganSachs)
                .HasForeignKey(ns => ns.MaNganSach)
                .OnDelete(DeleteBehavior.Cascade);


            // --- DATA SEEDING (Dữ liệu ban đầu) ---
            // Đưa dữ liệu mặc định vào các bảng tĩnh: VAI TRO, LOAI TAI KHOAN, LOAI GIAO DICH

            // VAI TRO (1: admin, 2: người dùng)
            modelBuilder.Entity<VaiTro>().HasData(
                new VaiTro { MaVaiTro = 1, TenVaiTro = "admin" },
                new VaiTro { MaVaiTro = 2, TenVaiTro = "người dùng" }
            );

            // LOAI TAI KHOAN
            modelBuilder.Entity<LoaiTaiKhoan>().HasData(
                new LoaiTaiKhoan { MaLoaiTaiKhoan = 1, TenLoaiTaiKhoan = "Tiền mặt" },
                new LoaiTaiKhoan { MaLoaiTaiKhoan = 2, TenLoaiTaiKhoan = "Ngân hàng" }
            );

            // LOAI GIAO DICH
            modelBuilder.Entity<LoaiGiaoDich>().HasData(
                new LoaiGiaoDich { MaLoaiGiaoDich = 1, TenLoaiGiaoDich = "thu" },
                new LoaiGiaoDich { MaLoaiGiaoDich = 2, TenLoaiGiaoDich = "chi" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}