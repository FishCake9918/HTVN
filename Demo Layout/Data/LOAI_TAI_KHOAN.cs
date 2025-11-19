using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Layout
{
    [Table("LOAI_TAI_KHOAN")]
    public class LOAI_TAI_KHOAN
    {
        [Key]
        public int MaLoaiTaiKhoan { get; set; }

        [Required]
        [StringLength(50)]
        public string TenLoaiTaiKhoan { get; set; }

        [InverseProperty(nameof(TAI_KHOAN_THANH_TOAN.LoaiTaiKhoan))]
        public virtual ICollection<TAI_KHOAN_THANH_TOAN> TaiKhoanThanhToans { get; set; } = new HashSet<TAI_KHOAN_THANH_TOAN>();
    }
}
