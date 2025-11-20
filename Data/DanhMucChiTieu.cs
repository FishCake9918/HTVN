// DanhMucChiTieu.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("DANH_MUC_CHI_TIEU")]
public class DanhMucChiTieu
{
    [Key]
    [Column("MaDanhMuc")]
    public int MaDanhMuc { get; set; }

    public string TenDanhMuc { get; set; }

    public int? DanhMucCha { get; set; } // Nullable FK
    public int MaNguoiDung { get; set; }

    [ForeignKey("DanhMucCha")]
    public DanhMucChiTieu DanhMucChaNavigation { get; set; } // Tự tham chiếu

    [ForeignKey("MaNguoiDung")]
    public NguoiDung NguoiDung { get; set; }

    public ICollection<GiaoDich> GiaoDichs { get; set; }
    public ICollection<BangNganSach> BangNganSachs { get; set; }
    public ICollection<DanhMucChiTieuNganSach> DanhMucChiTieuNganSachs { get; set; }
    public ICollection<DanhMucChiTieu> DanhMucCon { get; set; } // Quan hệ tự tham chiếu
}