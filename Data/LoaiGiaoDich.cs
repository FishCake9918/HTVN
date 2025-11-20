// LoaiGiaoDich.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("LOAI_GIAO_DICH")]
public class LoaiGiaoDich
{
    [Key]
    [Column("MaLoaiGiaoDich")]
    public int MaLoaiGiaoDich { get; set; }

    public string TenLoaiGiaoDich { get; set; }

    public ICollection<GiaoDich> GiaoDichs { get; set; }
}