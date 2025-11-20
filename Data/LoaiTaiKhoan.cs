// LoaiTaiKhoan.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("LOAI_TAI_KHOAN")]
public class LoaiTaiKhoan
{
    [Key]
    [Column("MaLoaiTaiKhoan")]
    public int MaLoaiTaiKhoan { get; set; }

    public string TenLoaiTaiKhoan { get; set; }

    public ICollection<TaiKhoanThanhToan> TaiKhoanThanhToans { get; set; }
}