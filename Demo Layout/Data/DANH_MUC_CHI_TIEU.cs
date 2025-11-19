using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo_Layout
{
[Table("DANH_MUC_CHI_TIEU")]
    public class DANH_MUC_CHI_TIEU
    {
        [Key]
        public int MaDanhMuc { get; set; }

        [Required]
        [StringLength(100)]
        public string TenDanhMuc { get; set; }

        public int? DanhMucCha { get; set; }
        public int MaNguoiDung { get; set; }

        [ForeignKey(nameof(MaNguoiDung))]
        public virtual NGUOI_DUNG NguoiDung { get; set; }

        [ForeignKey(nameof(DanhMucCha))]
        public virtual DANH_MUC_CHI_TIEU DanhMucChaNavigation { get; set; }

        public virtual ICollection<DANH_MUC_CHI_TIEU> InverseDanhMucChaNavigation { get; set; } = new HashSet<DANH_MUC_CHI_TIEU>();
        public virtual ICollection<GIAO_DICH> GiaoDichs { get; set; } = new HashSet<GIAO_DICH>();
        public virtual ICollection<BANG_NGAN_SACH> NganSachs { get; set; } = new HashSet<BANG_NGAN_SACH>();
        public virtual ICollection<DANH_MUC_CHI_TIEU_NGAN_SACH> DanhMucNganSachs { get; set; } = new HashSet<DANH_MUC_CHI_TIEU_NGAN_SACH>();
    }
    }