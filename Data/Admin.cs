// Admin.cs
using Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ADMIN")]
public class Admin
{
    [Key]
    [Column("MaAdmin")]
    public int MaAdmin { get; set; }

    public string HoTenAdmin { get; set; }

    public int MaTaiKhoan { get; set; }

    [ForeignKey("MaTaiKhoan")]
    public TaiKhoan TaiKhoan { get; set; }

    public ICollection<ThongBao> ThongBaos { get; set; }
}