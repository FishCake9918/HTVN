// VaiTro.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("VAI_TRO")]
public class VaiTro
{
    [Key]
    [Column("MaVaiTro")]
    public int MaVaiTro { get; set; }

    public string TenVaiTro { get; set; }

    public ICollection<TaiKhoan> TaiKhoans { get; set; }
}