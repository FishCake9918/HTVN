// TaiKhoanThanhToan.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TAI_KHOAN_THANH_TOAN")]
public class TaiKhoanThanhToan
{
    [Key]
    [Column("MaTaiKhoanThanhToan")]
    public int MaTaiKhoanThanhToan { get; set; }

    public string TenTaiKhoan { get; set; }
    public decimal SoDuBanDau { get; set; }
    public string TrangThai { get; set; }

    public int MaNguoiDung { get; set; }
    public int MaLoaiTaiKhoan { get; set; }

    [ForeignKey("MaNguoiDung")]
    public NguoiDung NguoiDung { get; set; }

    [ForeignKey("MaLoaiTaiKhoan")]
    public LoaiTaiKhoan LoaiTaiKhoan { get; set; }

    public ICollection<GiaoDich> GiaoDichs { get; set; }
    public ICollection<TaiKhoanThanhToanNganSach> TaiKhoanThanhToanNganSachs { get; set; }
}