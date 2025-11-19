using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Layout
{
    [Table("DOI_TUONG_GIAO_DICH")]
    public class DOI_TUONG_GIAO_DICH
    {
        [Key]
        public int MaDoiTuongGiaoDich { get; set; }

        [Required]
        [StringLength(100)]
        public string TenDoiTuong { get; set; }

        [StringLength(255)]
        public string GhiChu { get; set; }

        public int MaNguoiDung { get; set; }

        [ForeignKey(nameof(MaNguoiDung))]
        public virtual NGUOI_DUNG NguoiDung { get; set; }

        public virtual ICollection<GIAO_DICH> GiaoDichs { get; set; } = new HashSet<GIAO_DICH>();
        public virtual ICollection<DOI_TUONG_GIAO_DICH_NGAN_SACH> DoiTuongNganSachs { get; set; } = new HashSet<DOI_TUONG_GIAO_DICH_NGAN_SACH>();
    }
}
