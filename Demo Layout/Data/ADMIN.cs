using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Layout
{
    [Table("ADMIN")]
    public class ADMIN
    {
        [Key]
        public int MaAdmin { get; set; }

        [Required]
        [StringLength(100)]
        public string HoTenAdmin { get; set; }

        public int MaTaiKhoan { get; set; }

        [ForeignKey(nameof(MaTaiKhoan))]
        public virtual TAI_KHOAN TaiKhoan { get; set; }

        [InverseProperty(nameof(THONG_BAO.Admin))]
        public virtual ICollection<THONG_BAO> ThongBaos { get; set; } = new HashSet<THONG_BAO>();
    }
}
