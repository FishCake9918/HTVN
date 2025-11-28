using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using Krypton.Toolkit;
using Microsoft.Extensions.DependencyInjection;
using Piggy_Admin; // Cần cho DI

namespace Demo_Layout
{
    public partial class UserControlTaiKhoanThanhToan : UserControl
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider; // <-- Thêm ServiceProvider
        private readonly CurrentUserContext _userContext;   // <-- Thêm Context
        private BindingSource bsTaiKhoan = new BindingSource();

        private List<TaiKhoanDisplayModel> _displayList = new List<TaiKhoanDisplayModel>();
        private List<TaiKhoanFullModel> _fullAccountsList = new List<TaiKhoanFullModel>();

        public KryptonComboBox cmbLocTaiKhoan;
        public KryptonLabel lblSoDuBanDauDetail;
        public KryptonLabel lblSoDuKhaDungDetail;

        // Constructor nhận DI
        public UserControlTaiKhoanThanhToan(
            IDbContextFactory<QLTCCNContext> dbFactory,
            IServiceProvider serviceProvider,
            CurrentUserContext userContext)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
            _userContext = userContext;
            Dinhdangluoi.DinhDangLuoiNguoiDung(kryptonDataGridView1);
            kryptonDataGridView1.DataSource = bsTaiKhoan;


            if (cmbLocTaiKhoan != null)
            {
                this.cmbLocTaiKhoan.SelectedIndexChanged += CmbLocTaiKhoan_SelectedIndexChanged;
            }

            ConfigureGridView();
        }

        private void UserControlTaiKhoanThanhToan_Load(object sender, EventArgs e)
        {
            if (_userContext.MaNguoiDung == null) return; // Check an toàn

            LogHelper.GhiLog(_dbFactory, "Quản lý tài khoản thanh toán", _userContext.MaNguoiDung); // ghi log
            
            txtTimKiem.Text = string.Empty;
            txtTimKiem.ForeColor = Color.Black;
            LoadDanhSach();

            if (cmbLocTaiKhoan != null)
            {
                LoadComboBoxLocTaiKhoan();
            }
        }

        private void ConfigureGridView()
        {
            kryptonDataGridView1.AllowUserToAddRows = false;
            kryptonDataGridView1.AutoGenerateColumns = false;
            kryptonDataGridView1.ReadOnly = true;
            kryptonDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            kryptonDataGridView1.MultiSelect = false;
            kryptonDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            kryptonDataGridView1.Columns.Clear();
            kryptonDataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenTaiKhoan", HeaderText = "Tên Tài Khoản", DataPropertyName = "TenTaiKhoan" });
            kryptonDataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "LoaiTaiKhoan", HeaderText = "Loại Tài Khoản", DataPropertyName = "TenLoaiTaiKhoan" });
            kryptonDataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoDuBanDau", HeaderText = "Số dư Ban đầu", DataPropertyName = "SoDuBanDau", DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            kryptonDataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoDuHienTai", HeaderText = "Số dư Khả dụng", DataPropertyName = "SoDuHienTai", DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            kryptonDataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaTaiKhoan", HeaderText = "ID", DataPropertyName = "MaTaiKhoanThanhToan", Visible = false });
        }

        private void LoadComboBoxLocTaiKhoan()
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    // SỬA: Lọc theo _userContext.MaNguoiDung
                    var taiKhoanList = db.TaiKhoanThanhToans
                                         .Where(t => t.MaNguoiDung == _userContext.MaNguoiDung)
                                         .Select(t => new { t.MaTaiKhoanThanhToan, t.TenTaiKhoan, t.SoDuBanDau })
                                         .OrderBy(t => t.TenTaiKhoan)
                                         .ToList();

                    var allItem = new { MaTaiKhoanThanhToan = 0, TenTaiKhoan = "(Chọn Tài khoản)", SoDuBanDau = 0M };
                    taiKhoanList.Insert(0, allItem);

                    cmbLocTaiKhoan.DataSource = taiKhoanList;
                    cmbLocTaiKhoan.DisplayMember = "TenTaiKhoan";
                    cmbLocTaiKhoan.ValueMember = "MaTaiKhoanThanhToan";
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải danh sách tài khoản: " + ex.Message); }
        }

        private decimal CalculateCurrentBalance(int maTaiKhoan, decimal soDuBanDau)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                decimal totalThu = db.GiaoDichs
                    .Where(g => g.MaTaiKhoanThanhToan == maTaiKhoan && g.MaLoaiGiaoDich == 1)
                    .Sum(g => (decimal?)g.SoTien) ?? 0;

                decimal totalChi = db.GiaoDichs
                    .Where(g => g.MaTaiKhoanThanhToan == maTaiKhoan && g.MaLoaiGiaoDich == 2)
                    .Sum(g => (decimal?)g.SoTien) ?? 0;

                return soDuBanDau + totalThu - totalChi;
            }
        }

        public void LoadDanhSach()
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    // SỬA: Lọc theo _userContext.MaNguoiDung
                    var taiKhoanList = db.TaiKhoanThanhToans
                                         .Include(t => t.LoaiTaiKhoan)
                                         .Where(t => t.MaNguoiDung == _userContext.MaNguoiDung && t.TrangThai == "Đang hoạt động")
                                         .OrderBy(t => t.TenTaiKhoan)
                                         .ToList();

                    _fullAccountsList = taiKhoanList.Select(t => new TaiKhoanFullModel { MaTaiKhoanThanhToan = t.MaTaiKhoanThanhToan, SoDuBanDau = t.SoDuBanDau }).ToList();

                    _displayList = taiKhoanList.Select(t => new TaiKhoanDisplayModel
                    {
                        MaTaiKhoanThanhToan = t.MaTaiKhoanThanhToan,
                        TenTaiKhoan = t.TenTaiKhoan,
                        TenLoaiTaiKhoan = t.LoaiTaiKhoan != null ? t.LoaiTaiKhoan.TenLoaiTaiKhoan : "N/A",
                        SoDuBanDau = t.SoDuBanDau,
                        SoDuHienTai = CalculateCurrentBalance(t.MaTaiKhoanThanhToan, t.SoDuBanDau)
                    }).ToList();

                    TimKiemVaLoc();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message); }
        }

        private void TimKiemVaLoc()
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            var danhSachLoc = _displayList.AsEnumerable();

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                danhSachLoc = danhSachLoc.Where(p =>
                    (p.TenTaiKhoan != null && p.TenTaiKhoan.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
                    (p.TenLoaiTaiKhoan != null && p.TenLoaiTaiKhoan.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase))
                );
            }
            bsTaiKhoan.DataSource = danhSachLoc.ToList();
            bsTaiKhoan.ResetBindings(false);
        }

        private void CmbLocTaiKhoan_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedTaiKhoanId = (cmbLocTaiKhoan.SelectedValue is int id) ? id : 0;
            UpdateSoDuChiTiet(selectedTaiKhoanId);
            if (selectedTaiKhoanId != 0)
                bsTaiKhoan.DataSource = _displayList.Where(t => t.MaTaiKhoanThanhToan == selectedTaiKhoanId).ToList();
            else
                bsTaiKhoan.DataSource = _displayList;
            bsTaiKhoan.ResetBindings(false);
        }

        private void UpdateSoDuChiTiet(int selectedTaiKhoanId)
        {
            if (lblSoDuBanDauDetail == null || lblSoDuKhaDungDetail == null) return;
            if (selectedTaiKhoanId == 0)
            {
                lblSoDuBanDauDetail.Text = "Số dư Ban đầu: --";
                lblSoDuKhaDungDetail.Text = "Số dư Khả dụng: --";
                return;
            }
            var accountFromDb = _fullAccountsList.FirstOrDefault(t => t.MaTaiKhoanThanhToan == selectedTaiKhoanId);
            var accountFromDisplayList = _displayList.FirstOrDefault(t => t.MaTaiKhoanThanhToan == selectedTaiKhoanId);

            if (accountFromDb != null && accountFromDisplayList != null)
            {
                lblSoDuBanDauDetail.Text = $"Số dư Ban đầu: {accountFromDb.SoDuBanDau:N0} VND";
                lblSoDuKhaDungDetail.Text = $"Số dư Khả dụng: {accountFromDisplayList.SoDuHienTai:N0} VND";
            }
        }

        private void TxtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ') e.Handled = true;
        }
        private void TxtTimKiem_TextChanged(object sender, EventArgs e) => TimKiemVaLoc();

        // --- XỬ LÝ MỞ FORM TRỰC TIẾP TẠI ĐÂY ---
        private void BtnThem_Click(object sender, EventArgs e)
        {
            // Tự mở form, không cần gọi Event ra Main
            var frm = ActivatorUtilities.CreateInstance<FrmThemTaiKhoanThanhToan>(_serviceProvider);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSach();
                if (cmbLocTaiKhoan != null) LoadComboBoxLocTaiKhoan();
            }
        }

        private void BtnDong_Click(object sender, EventArgs e)
        {
            if (bsTaiKhoan.Current == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần đóng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedId = ((TaiKhoanDisplayModel)bsTaiKhoan.Current).MaTaiKhoanThanhToan;

            // Tự mở form đóng
            var frm = ActivatorUtilities.CreateInstance<FrmDongTaiKhoanThanhToan>(_serviceProvider);
            frm.SetTaiKhoanId(selectedId);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSach();
                if (cmbLocTaiKhoan != null) LoadComboBoxLocTaiKhoan();
            }
        }
    }

    public class TaiKhoanDisplayModel
    {
        public int MaTaiKhoanThanhToan { get; set; }
        public string TenTaiKhoan { get; set; }
        public string TenLoaiTaiKhoan { get; set; }
        public decimal SoDuBanDau { get; set; }
        public decimal SoDuHienTai { get; set; }
    }
    public class TaiKhoanFullModel
    {
        public int MaTaiKhoanThanhToan { get; set; }
        public decimal SoDuBanDau { get; set; }
    }
}