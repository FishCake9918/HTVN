using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Layout
{
    [Table("GIAO_DICH_NGAN_SACH")]
    public class GIAO_DICH_NGAN_SACH
    {
        public int MaGiaoDich { get; set; }
        public int MaNganSach { get; set; }

        [ForeignKey(nameof(MaGiaoDich))]
        public virtual GIAO_DICH GiaoDich { get; set; }

        [ForeignKey(nameof(MaNganSach))]
        public virtual BANG_NGAN_SACH NganSach { get; set; }
    }
}
