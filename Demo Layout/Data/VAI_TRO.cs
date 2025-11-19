using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Layout
{
    [Table("VAI_TRO")]
    public class VAI_TRO
    {
        [Key]
        public int MaVaiTro { get; set; }

        [Required]
        [StringLength(50)]
        public string TenVaiTro { get; set; }

        [InverseProperty(nameof(TAI_KHOAN.VaiTro))]
        public virtual ICollection<TAI_KHOAN> TaiKhoans { get; set; } = new HashSet<TAI_KHOAN>();
    }
    
}
