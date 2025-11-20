// NguoiDung.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("NGUOI_DUNG")]
public class NguoiDung
{
    [Key]
    [Column("MaNguoiDung")]
    public int MaNguoiDung { get; set; }

    public string HoTen { get; set; }
    public string GioiTinh { get; set; }
    public DateTime? NgaySinh { get; set; }

    public int MaTaiKhoan { get; set; }

    [ForeignKey("MaTaiKhoan")]
    public TaiKhoan TaiKhoan { get; set; }

    public ICollection<TaiKhoanThanhToan> TaiKhoanThanhToans { get; set; }
    public ICollection<DanhMucChiTieu> DanhMucChiTieus { get; set; }
    public ICollection<DoiTuongGiaoDich> DoiTuongGiaoDichs { get; set; }
    public ICollection<GiaoDich> GiaoDichs { get; set; }
    public ICollection<BangNganSach> BangNganSachs { get; set; }
}