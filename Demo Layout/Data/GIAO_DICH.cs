using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Layout
{
    [Table("GIAO_DICH")]
    public class GIAO_DICH
    {
        [Key]
        public int MaGiaoDich { get; set; }

        [Required]
        [StringLength(100)]
        public string TenGiaoDich { get; set; }

        [StringLength(255)]
        public string GhiChu { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal SoTien { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayGiaoDich { get; set; }

        public int? MaDanhMuc { get; set; }
        public int? MaTaiKhoanThanhToan { get; set; }
        public int? MaLoaiGiaoDich { get; set; }
        public int? MaNguoiDung { get; set; }
        public int? MaDoiTuongGiaoDich { get; set; }

        [ForeignKey(nameof(MaDanhMuc))]
        public virtual DANH_MUC_CHI_TIEU DanhMuc { get; set; }

        [ForeignKey(nameof(MaTaiKhoanThanhToan))]
        public virtual TAI_KHOAN_THANH_TOAN TaiKhoanThanhToan { get; set; }

        [ForeignKey(nameof(MaLoaiGiaoDich))]
        public virtual LOAI_GIAO_DICH LoaiGiaoDich { get; set; }

        [ForeignKey(nameof(MaNguoiDung))]
        public virtual NGUOI_DUNG NguoiDung { get; set; }

        [ForeignKey(nameof(MaDoiTuongGiaoDich))]
        public virtual DOI_TUONG_GIAO_DICH DoiTuongGiaoDich { get; set; }

        public virtual ICollection<GIAO_DICH_NGAN_SACH> GiaoDichNganSachs { get; set; } = new HashSet<GIAO_DICH_NGAN_SACH>();
    }
}
