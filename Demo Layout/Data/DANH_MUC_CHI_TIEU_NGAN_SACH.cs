using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Layout
{
    [Table("DANH_MUC_CHI_TIEU_NGAN_SACH")]
    public class DANH_MUC_CHI_TIEU_NGAN_SACH
    {
        public int MaDanhMuc { get; set; }
        public int MaNganSach { get; set; }

        [ForeignKey(nameof(MaDanhMuc))]
        public virtual DANH_MUC_CHI_TIEU DanhMuc { get; set; }

        [ForeignKey(nameof(MaNganSach))]
        public virtual BANG_NGAN_SACH NganSach { get; set; }
    }
}
