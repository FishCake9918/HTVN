// DoiTuongGiaoDich.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("DOI_TUONG_GIAO_DICH")]
public class DoiTuongGiaoDich
{
    [Key]
    [Column("MaDoiTuongGiaoDich")]
    public int MaDoiTuongGiaoDich { get; set; }

    public string TenDoiTuong { get; set; }
    public string GhiChu { get; set; }

    public int MaNguoiDung { get; set; }

    [ForeignKey("MaNguoiDung")]
    public NguoiDung NguoiDung { get; set; }

    public ICollection<GiaoDich> GiaoDichs { get; set; }
    public ICollection<DoiTuongGiaoDichNganSach> DoiTuongGiaoDichNganSachs { get; set; }
}