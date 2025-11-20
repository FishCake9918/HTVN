// DoiTuongGiaoDichNganSach.cs
using System.ComponentModel.DataAnnotations.Schema;

[Table("DOI_TUONG_GIAO_DICH_NGAN_SACH")]
public class DoiTuongGiaoDichNganSach
{
    public int MaDoiTuongGiaoDich { get; set; }
    public int MaNganSach { get; set; }

    [ForeignKey("MaDoiTuongGiaoDich")]
    public DoiTuongGiaoDich DoiTuongGiaoDich { get; set; }

    [ForeignKey("MaNganSach")]
    public BangNganSach BangNganSach { get; set; }
}