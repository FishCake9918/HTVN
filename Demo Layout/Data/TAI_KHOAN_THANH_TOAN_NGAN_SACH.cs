using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Layout
{
    [Table("TAI_KHOAN_THANH_TOAN_NGAN_SACH")]
    public class TAI_KHOAN_THANH_TOAN_NGAN_SACH
    {
        public int MaTaiKhoanThanhToan { get; set; }
        public int MaNganSach { get; set; }

        [ForeignKey(nameof(MaTaiKhoanThanhToan))]
        public virtual TAI_KHOAN_THANH_TOAN TaiKhoanThanhToan { get; set; }

        [ForeignKey(nameof(MaNganSach))]
        public virtual BANG_NGAN_SACH NganSach { get; set; }
    }
}
