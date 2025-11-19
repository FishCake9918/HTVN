using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Layout
{
    [Table("NGUOI_DUNG")]
    public class NGUOI_DUNG
    {
        [Key]
        public int MaNguoiDung { get; set; }

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; }

        [StringLength(10)]
        public string GioiTinh { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgaySinh { get; set; }

        public int MaTaiKhoan { get; set; }

        [ForeignKey(nameof(MaTaiKhoan))]
        public virtual TAI_KHOAN TaiKhoan { get; set; }

        public virtual ICollection<TAI_KHOAN_THANH_TOAN> TaiKhoanThanhToans { get; set; } = new HashSet<TAI_KHOAN_THANH_TOAN>();
        public virtual ICollection<DANH_MUC_CHI_TIEU> DanhMucs { get; set; } = new HashSet<DANH_MUC_CHI_TIEU>();
        public virtual ICollection<DOI_TUONG_GIAO_DICH> DoiTuongGiaoDichs { get; set; } = new HashSet<DOI_TUONG_GIAO_DICH>();
        public virtual ICollection<GIAO_DICH> GiaoDichs { get; set; } = new HashSet<GIAO_DICH>();
        public virtual ICollection<BANG_NGAN_SACH> NganSachs { get; set; } = new HashSet<BANG_NGAN_SACH>();
    }

}
