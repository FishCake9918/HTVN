using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Layout
{
    [Table("BANG_NGAN_SACH")]
    public class BANG_NGAN_SACH
    {
        [Key]
        public int MaNganSach { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal SoTien { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayBatDau { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayKetThuc { get; set; }

        public int? MaNguoiDung { get; set; }
        public int? MaDanhMuc { get; set; }
        public int? MaGiaoDich { get; set; }

        [ForeignKey(nameof(MaNguoiDung))]
        public virtual NGUOI_DUNG NguoiDung { get; set; }

        [ForeignKey(nameof(MaDanhMuc))]
        public virtual DANH_MUC_CHI_TIEU DanhMuc { get; set; }

        public virtual ICollection<DANH_MUC_CHI_TIEU_NGAN_SACH> DanhMucNganSachs { get; set; } = new HashSet<DANH_MUC_CHI_TIEU_NGAN_SACH>();
        public virtual ICollection<GIAO_DICH_NGAN_SACH> GiaoDichNganSachs { get; set; } = new HashSet<GIAO_DICH_NGAN_SACH>();
        public virtual ICollection<TAI_KHOAN_THANH_TOAN_NGAN_SACH> TaiKhoanNganSachs { get; set; } = new HashSet<TAI_KHOAN_THANH_TOAN_NGAN_SACH>();
        public virtual ICollection<DOI_TUONG_GIAO_DICH_NGAN_SACH> DoiTuongNganSachs { get; set; } = new HashSet<DOI_TUONG_GIAO_DICH_NGAN_SACH>();
    }
}
