// ThongBao.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("THONG_BAO")]
public class ThongBao
{
    [Key]
    [Column("MaThongBao")]
    public int MaThongBao { get; set; }

    public string TieuDe { get; set; }
    public string NoiDung { get; set; }
    public DateTime NgayTao { get; set; }

    public int? MaAdmin { get; set; } // Nullable FK (ON DELETE SET NULL)

    [ForeignKey("MaAdmin")]
    public Admin Admin { get; set; }
}