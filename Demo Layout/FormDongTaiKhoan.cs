using Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;
using Krypton.Toolkit;
using System.Globalization;

namespace Demo_Layout
{
    public partial class FormDongTaiKhoan : KryptonForm
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private int _maTaiKhoanDong;
        private const int MA_NGUOI_DUNG_HIEN_TAI = 1;
        private decimal _currentBalance = 0;

        public FormDongTaiKhoan(IDbContextFactory<QLTCCNContext> dbFactory)
        {
            InitializeComponent();
            _dbFactory = dbFactory;

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
            // ... (Logic tính số dư giữ nguyên)
            using (var db = _dbFactory.CreateDbContext())
            {
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
            try
            {
                _currentBalance = CalculateCurrentBalance(_maTaiKhoanDong);

                using (var db = _dbFactory.CreateDbContext())
                {
                    var taiKhoanDong = db.TaiKhoanThanhToans.FirstOrDefault(t => t.MaTaiKhoanThanhToan == _maTaiKhoanDong);

                    if (taiKhoanDong != null)
                    {
                        lblTenTaiKhoan.Text = taiKhoanDong.TenTaiKhoan;
                        lblSoDuHienTai.Text = _currentBalance.ToString("N0", CultureInfo.CurrentCulture);
                    }

                    var taiKhoanChuyenList = db.TaiKhoanThanhToans
                        .Where(t => t.MaNguoiDung == MA_NGUOI_DUNG_HIEN_TAI &&
                                    t.MaTaiKhoanThanhToan != _maTaiKhoanDong &&
                                    t.TrangThai == "Đang hoạt động")
                        .ToList();

                    cmbTaiKhoanChuyen.DataSource = taiKhoanChuyenList;
                    cmbTaiKhoanChuyen.DisplayMember = "TenTaiKhoan";
                    cmbTaiKhoanChuyen.ValueMember = "MaTaiKhoanThanhToan";

                    cmbTaiKhoanChuyen.SelectedIndex = -1; // Đã fix lỗi tên control

                    if (!taiKhoanChuyenList.Any())
                    {
                        MessageBox.Show("Không tìm thấy tài khoản để chuyển số dư hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (_currentBalance != 0)
                        {
                            btnDong.Enabled = false;
                        }
                        else
                        {
                            btnDong.Enabled = true;
                        }
                    }
                    else
                    {
                        btnDong.Enabled = true;
                        cmbTaiKhoanChuyen.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDong_Click(object sender, EventArgs e)
        {
            if (_currentBalance != 0 && cmbTaiKhoanChuyen.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản nhận số dư.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_currentBalance < 0)
            {
                MessageBox.Show("Tài khoản đang âm, không thể đóng khi chưa xử lý số âm.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maTaiKhoanNhan = cmbTaiKhoanChuyen.SelectedValue as int? ?? 0;

            PerformChuyenTienVaDongTaiKhoan(maTaiKhoanNhan, _currentBalance);
        }

        private void PerformChuyenTienVaDongTaiKhoan(int maTaiKhoanNhan, decimal soTien)
        {
            if (soTien != 0 && maTaiKhoanNhan == 0)
            {
                MessageBox.Show("Không thể chuyển tiền vì không tìm thấy tài khoản nhận hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var db = _dbFactory.CreateDbContext())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var taiKhoanDong = db.TaiKhoanThanhToans.FirstOrDefault(t => t.MaTaiKhoanThanhToan == _maTaiKhoanDong);
                    if (taiKhoanDong == null) throw new Exception("Không tìm thấy tài khoản để đóng.");

                    if (soTien > 0)
                    {
                        var taiKhoanNhan = db.TaiKhoanThanhToans.FirstOrDefault(t => t.MaTaiKhoanThanhToan == maTaiKhoanNhan);
                        if (taiKhoanNhan == null) throw new Exception("Không tìm thấy tài khoản nhận.");

                        taiKhoanNhan.SoDuBanDau += soTien;
                    }

                    // Đánh dấu tài khoản đóng
                    taiKhoanDong.SoDuBanDau = 0;
                    taiKhoanDong.TrangThai = "Đóng"; // Đã fix lỗi cú pháp N"string"

                    db.SaveChanges();
                    transaction.Commit();

                    MessageBox.Show($"Đóng tài khoản {taiKhoanDong.TenTaiKhoan} thành công. {(soTien != 0 ? "Số dư đã được chuyển." : "")}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Lỗi xử lý đóng tài khoản: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}