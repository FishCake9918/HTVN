// DanhMucChiTieuNganSach.cs
using System.ComponentModel.DataAnnotations.Schema;

[Table("DANH_MUC_CHI_TIEU_NGAN_SACH")]
public class DanhMucChiTieuNganSach
{
    // Không cần [Key], Khóa kép được định nghĩa trong DbContext

    public int MaDanhMuc { get; set; }
    public int MaNganSach { get; set; }

    [ForeignKey("MaDanhMuc")]
    public DanhMucChiTieu DanhMucChiTieu { get; set; }

    [ForeignKey("MaNganSach")]
    public BangNganSach BangNganSach { get; set; }
}