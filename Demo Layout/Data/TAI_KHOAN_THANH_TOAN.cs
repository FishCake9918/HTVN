using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Layout
{
    [Table("TAI_KHOAN_THANH_TOAN")]
    public class TAI_KHOAN_THANH_TOAN
    {
        [Key]
        public int MaTaiKhoanThanhToan { get; set; }

        [Required]
        [StringLength(100)]
        public string TenTaiKhoan { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal SoDuBanDau { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        public int MaNguoiDung { get; set; }
        public int MaLoaiTaiKhoan { get; set; }

        [ForeignKey(nameof(MaNguoiDung))]
        public virtual NGUOI_DUNG NguoiDung { get; set; }

        [ForeignKey(nameof(MaLoaiTaiKhoan))]
        public virtual LOAI_TAI_KHOAN LoaiTaiKhoan { get; set; }

        public virtual ICollection<GIAO_DICH> GiaoDichs { get; set; } = new HashSet<GIAO_DICH>();
        public virtual ICollection<TAI_KHOAN_THANH_TOAN_NGAN_SACH> TaiKhoanNganSachs { get; set; } = new HashSet<TAI_KHOAN_THANH_TOAN_NGAN_SACH>();
    }

}
