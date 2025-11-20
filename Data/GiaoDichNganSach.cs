// GiaoDichNganSach.cs
using System.ComponentModel.DataAnnotations.Schema;

[Table("GIAO_DICH_NGAN_SACH")]
public class GiaoDichNganSach
{
    public int MaGiaoDich { get; set; }
    public int MaNganSach { get; set; }

    [ForeignKey("MaGiaoDich")]
    public GiaoDich GiaoDich { get; set; }

    [ForeignKey("MaNganSach")]
    public BangNganSach BangNganSach { get; set; }
}