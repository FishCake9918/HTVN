using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Layout
{
    [Table("THONG_BAO")]
    public class THONG_BAO
    {
        [Key]
        public int MaThongBao { get; set; }

        [Required]
        [StringLength(200)]
        public string TieuDe { get; set; }

        public string NoiDung { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? MaAdmin { get; set; }

        [ForeignKey(nameof(MaAdmin))]
        public virtual ADMIN Admin { get; set; }
    }
}
