// BangNganSach.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("BANG_NGAN_SACH")]
public class BangNganSach
{
    [Key]
    [Column("MaNganSach")]
    public int MaNganSach { get; set; }

    public decimal SoTien { get; set; }
    public DateTime? NgayBatDau { get; set; }
    public DateTime? NgayKetThuc { get; set; }

    public int? MaNguoiDung { get; set; } // Nullable FK
    public int? MaDanhMuc { get; set; } // Nullable FK
    public int? MaGiaoDich { get; set; } // KHÔNG CẦN THIẾT (đã có bảng trung gian), tôi giữ lại để phù hợp scripts SQL của bạn

    [ForeignKey("MaNguoiDung")]
    public NguoiDung NguoiDung { get; set; }

    [ForeignKey("MaDanhMuc")]
    public DanhMucChiTieu DanhMucChiTieu { get; set; }

    public ICollection<DanhMucChiTieuNganSach> DanhMucChiTieuNganSachs { get; set; }
    public ICollection<GiaoDichNganSach> GiaoDichNganSachs { get; set; }
    public ICollection<TaiKhoanThanhToanNganSach> TaiKhoanThanhToanNganSachs { get; set; }
    public ICollection<DoiTuongGiaoDichNganSach> DoiTuongGiaoDichNganSachs { get; set; }
}