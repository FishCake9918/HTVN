// TaiKhoan.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TAI_KHOAN")]
public class TaiKhoan
{
    [Key]
    [Column("MaTaiKhoan")]
    public int MaTaiKhoan { get; set; }

    public string MatKhau { get; set; }

    [ConcurrencyCheck]
    public string Email { get; set; }

    public int MaVaiTro { get; set; }

    [ForeignKey("MaVaiTro")]
    public VaiTro VaiTro { get; set; }

    public Admin Admin { get; set; } // Quan hệ 1-1 với Admin
    public NguoiDung NguoiDung { get; set; } // Quan hệ 1-1 với NguoiDung
}