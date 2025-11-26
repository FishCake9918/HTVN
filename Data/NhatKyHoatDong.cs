using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{

    [Table("NHAT_KY_HOAT_DONG")]
    public class NhatKyHoatDong
    {
        [Key]
        [Column("MaNhatKyHoatDong")]
        public int MaNhatKyHoatDong { get; set; }

        [Column("HanhDong")]
        public string HanhDong { get; set; } // VD: DANG_NHAP, THEM_GIAO_DICH

        [Column("MoTa")]
        public string? MoTa { get; set; } // Chi tiết log

        [Column("ThoiGian")]
        public DateTime ThoiGian { get; set; }

        [Column("MaNguoiDung")]
        public int? MaNguoiDung { get; set; } // Nullable để hỗ trợ ON DELETE SET NULL

        [ForeignKey("MaNguoiDung")]
        public NguoiDung NguoiDung { get; set; }
    }
}
