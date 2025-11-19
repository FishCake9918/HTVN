using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Layout
{
    [Table("LOAI_GIAO_DICH")]
    public class LOAI_GIAO_DICH
    {
        [Key]
        public int MaLoaiGiaoDich { get; set; }

        [Required]
        [StringLength(50)]
        public string TenLoaiGiaoDich { get; set; }

        [InverseProperty(nameof(GIAO_DICH.LoaiGiaoDich))]
        public virtual ICollection<GIAO_DICH> GiaoDichs { get; set; } = new HashSet<GIAO_DICH>();
    }
}
