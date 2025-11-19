using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Layout
{
    [Table("TAI_KHOAN")]
    public class TAI_KHOAN
    {
        [Key]
        public int MaTaiKhoan { get; set; }

        [Required]
        [StringLength(255)]
        public string MatKhau { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        public int MaVaiTro { get; set; }

        [ForeignKey(nameof(MaVaiTro))]
        public virtual VAI_TRO VaiTro { get; set; }

        [InverseProperty(nameof(ADMIN.TaiKhoan))]
        public virtual ICollection<ADMIN> Admins { get; set; } = new HashSet<ADMIN>();

        [InverseProperty(nameof(NGUOI_DUNG.TaiKhoan))]
        public virtual ICollection<NGUOI_DUNG> NguoiDungs { get; set; } = new HashSet<NGUOI_DUNG>();
    }
}
