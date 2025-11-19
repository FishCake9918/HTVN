using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Layout
{
    [Table("DOI_TUONG_GIAO_DICH_NGAN_SACH")]
    public class DOI_TUONG_GIAO_DICH_NGAN_SACH
    {
        public int MaDoiTuongGiaoDich { get; set; }
        public int MaNganSach { get; set; }

        [ForeignKey(nameof(MaDoiTuongGiaoDich))]
        public virtual DOI_TUONG_GIAO_DICH DoiTuongGiaoDich { get; set; }

        [ForeignKey(nameof(MaNganSach))]
        public virtual BANG_NGAN_SACH NganSach { get; set; }
    }
}
