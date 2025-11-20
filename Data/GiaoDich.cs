// GiaoDich.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("GIAO_DICH")]
public class GiaoDich
{
    [Key]
    [Column("MaGiaoDich")]
    public int MaGiaoDich { get; set; }

    public string TenGiaoDich { get; set; }
    public string GhiChu { get; set; }
    public decimal SoTien { get; set; }
    public DateTime NgayGiaoDich { get; set; }

    public int? MaDanhMuc { get; set; } // Nullable FK
    public int? MaTaiKhoanThanhToan { get; set; } // Nullable FK
    public int? MaLoaiGiaoDich { get; set; } // Nullable FK
    public int? MaNguoiDung { get; set; } // Nullable FK
    public int? MaDoiTuongGiaoDich { get; set; } // Nullable FK

    [ForeignKey("MaDanhMuc")]
    public DanhMucChiTieu DanhMucChiTieu { get; set; }

    [ForeignKey("MaTaiKhoanThanhToan")]
    public TaiKhoanThanhToan TaiKhoanThanhToan { get; set; }

    [ForeignKey("MaLoaiGiaoDich")]
    public LoaiGiaoDich LoaiGiaoDich { get; set; }

    [ForeignKey("MaNguoiDung")]
    public NguoiDung NguoiDung { get; set; }

    [ForeignKey("MaDoiTuongGiaoDich")]
    public DoiTuongGiaoDich DoiTuongGiaoDich { get; set; }

    public ICollection<GiaoDichNganSach> GiaoDichNganSachs { get; set; }
}