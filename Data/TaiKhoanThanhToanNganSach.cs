// TaiKhoanThanhToanNganSach.cs
using System.ComponentModel.DataAnnotations.Schema;

[Table("TAI_KHOAN_THANH_TOAN_NGAN_SACH")]
public class TaiKhoanThanhToanNganSach
{
    public int MaTaiKhoanThanhToan { get; set; }
    public int MaNganSach { get; set; }

    [ForeignKey("MaTaiKhoanThanhToan")]
    public TaiKhoanThanhToan TaiKhoanThanhToan { get; set; }

    [ForeignKey("MaNganSach")]
    public BangNganSach BangNganSach { get; set; }
}