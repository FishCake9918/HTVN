using Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing; // Cần thiết cho các thuộc tính Font, Color
using System.Globalization;

namespace Demo_Layout
{
    public partial class FrmDongTaiKhoanThanhToan : Form
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly CurrentUserContext _userContext;
        private int _maTaiKhoanDong;
        private decimal _currentBalance = 0;

        public FrmDongTaiKhoanThanhToan(
            IDbContextFactory<QLTCCNContext> dbFactory,
            CurrentUserContext userContext)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _userContext = userContext;

            this.btnDong.Click += BtnDong_Click;
            this.btnHuy.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
        }

        public void SetTaiKhoanId(int taiKhoanId)
        {
            _maTaiKhoanDong = taiKhoanId;
            LoadTaiKhoanData();
        }

        private decimal CalculateCurrentBalance(int maTaiKhoan)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                // Sử dụng 'decimal?' cho Sum để xử lý trường hợp không có giao dịch
                decimal soDuBanDau = db.TaiKhoanThanhToans
                    .Where(t => t.MaTaiKhoanThanhToan == maTaiKhoan)
                    .Select(t => t.SoDuBanDau)
                    .FirstOrDefault();

                decimal totalThu = db.GiaoDichs
                    .Where(g => g.MaTaiKhoanThanhToan == maTaiKhoan && g.MaLoaiGiaoDich == 1)
                    .Sum(g => (decimal?)g.SoTien) ?? 0;

                decimal totalChi = db.GiaoDichs
                    .Where(g => g.MaTaiKhoanThanhToan == maTaiKhoan && g.MaLoaiGiaoDich == 2)
                    .Sum(g => (decimal?)g.SoTien) ?? 0;

                return soDuBanDau + totalThu - totalChi;
            }
        }

        private void LoadTaiKhoanData()
        {
            if (_userContext.MaNguoiDung == null) return;
            try
            {
                _currentBalance = CalculateCurrentBalance(_maTaiKhoanDong);
                using (var db = _dbFactory.CreateDbContext())
                {
                    var taiKhoanDong = db.TaiKhoanThanhToans.FirstOrDefault(t => t.MaTaiKhoanThanhToan == _maTaiKhoanDong);
                    if (taiKhoanDong != null)
                    {
                        lblTenTaiKhoan.Text = taiKhoanDong.TenTaiKhoan;
                        // Định dạng tiền tệ
                        lblSoDuHienTai.Text = _currentBalance.ToString("N0", CultureInfo.CurrentCulture) + " đ";
                    }

                    // Lọc tài khoản chuyển tiền theo User hiện tại
                    var taiKhoanChuyenList = db.TaiKhoanThanhToans
                        .Where(t => t.MaNguoiDung == _userContext.MaNguoiDung &&
                                    t.MaTaiKhoanThanhToan != _maTaiKhoanDong &&
                                    t.TrangThai == "Đang hoạt động")
                        .ToList();

                    cmbTaiKhoanChuyen.DataSource = taiKhoanChuyenList;
                    cmbTaiKhoanChuyen.DisplayMember = "TenTaiKhoan";
                    cmbTaiKhoanChuyen.ValueMember = "MaTaiKhoanThanhToan";
                    cmbTaiKhoanChuyen.SelectedIndex = -1;

                    if (!taiKhoanChuyenList.Any())
                    {
                        MessageBox.Show("Không tìm thấy tài khoản để chuyển số dư.");
                        btnDong.Enabled = _currentBalance == 0;
                    }
                    else
                    {
                        btnDong.Enabled = true;
                        cmbTaiKhoanChuyen.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void BtnDong_Click(object sender, EventArgs e)
        {
            if (_currentBalance != 0 && cmbTaiKhoanChuyen.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản nhận số dư còn lại.");
                return;
            }
            if (_currentBalance < 0)
            {
                MessageBox.Show("Tài khoản đang âm. Vui lòng nạp tiền để số dư bằng 0 hoặc dương trước khi đóng.");
                return;
            }

            // Ép kiểu SelectedValue sang int? và gán 0 nếu null
            int maTaiKhoanNhan = cmbTaiKhoanChuyen.SelectedValue as int? ?? 0;

            PerformChuyenTienVaDongTaiKhoan(maTaiKhoanNhan, _currentBalance);
        }

        private void PerformChuyenTienVaDongTaiKhoan(int maTaiKhoanNhan, decimal soTien)
        {
            // Nếu số dư = 0, không cần tài khoản nhận. Nếu số dư > 0, bắt buộc phải có tài khoản nhận (maTaiKhoanNhan > 0)
            if (soTien > 0 && maTaiKhoanNhan == 0) return;

            using (var db = _dbFactory.CreateDbContext())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var taiKhoanDong = db.TaiKhoanThanhToans.FirstOrDefault(t => t.MaTaiKhoanThanhToan == _maTaiKhoanDong);
                    if (taiKhoanDong == null) throw new Exception("Không tìm thấy tài khoản cần đóng.");

                    if (soTien > 0)
                    {
                        var taiKhoanNhan = db.TaiKhoanThanhToans.FirstOrDefault(t => t.MaTaiKhoanThanhToan == maTaiKhoanNhan);
                        if (taiKhoanNhan == null) throw new Exception("Không tìm thấy tài khoản nhận số dư.");

                        // Chuyển số dư bằng cách cộng vào SoDuBanDau của tài khoản nhận
                        taiKhoanNhan.SoDuBanDau += soTien;
                    }

                    // Đặt lại SoDuBanDau của tài khoản đóng về 0 và cập nhật trạng thái
                    taiKhoanDong.SoDuBanDau = 0;
                    taiKhoanDong.TrangThai = "Đóng";

                    db.SaveChanges();
                    transaction.Commit();

                    MessageBox.Show("Đóng tài khoản thành công! Số dư đã được chuyển đi.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Lỗi khi đóng tài khoản: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}