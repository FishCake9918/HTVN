using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace Demo_Layout
{
    // Delegate để mở Form Thêm/Đóng tài khoản
    public delegate void OpenFormHandler(object sender, int taiKhoanId);

    // UserControl quản lý danh sách
    public partial class UserControlTaiKhoanThanhToan : UserControl
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private BindingSource bsTaiKhoan = new BindingSource();
        private const int MA_NGUOI_DUNG_HIEN_TAI = 1;

        public event OpenFormHandler OnOpenThemTaiKhoan;
        public event OpenFormHandler OnOpenDongTaiKhoan;

        private List<TaiKhoanDisplayModel> _displayList = new List<TaiKhoanDisplayModel>();

        public UserControlTaiKhoanThanhToan(IDbContextFactory<QLTCCNContext> dbFactory)
        {
            InitializeComponent();
            _dbFactory = dbFactory;

            kryptonDataGridView1.DataSource = bsTaiKhoan;

            this.Load += UserControlTaiKhoanThanhToan_Load;
            this.txtTimKiem.TextChanged += TxtTimKiem_TextChanged;
            this.btnThem.Click += BtnThem_Click;
            this.btnDong.Click += BtnDong_Click;

            ConfigureGridView();
        }

        private void UserControlTaiKhoanThanhToan_Load(object sender, EventArgs e)
        {
            txtTimKiem.Text = string.Empty;
            txtTimKiem.ForeColor = Color.Black;

            LoadDanhSach();
        }

        // --- Cấu hình DataGridView ---
        private void ConfigureGridView()
        {
            kryptonDataGridView1.AutoGenerateColumns = false;
            kryptonDataGridView1.ReadOnly = true;
            kryptonDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            kryptonDataGridView1.MultiSelect = false;
            kryptonDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            kryptonDataGridView1.Columns.Clear();

            // Chỉ hiển thị các cột cần thiết (Loại bỏ cột Trạng Thái)
            kryptonDataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenTaiKhoan", HeaderText = "Tên Tài Khoản", DataPropertyName = "TenTaiKhoan" });
            kryptonDataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "LoaiTaiKhoan", HeaderText = "Loại Tài Khoản", DataPropertyName = "TenLoaiTaiKhoan" });
            kryptonDataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoDuHienTai", HeaderText = "Số Dư", DataPropertyName = "SoDuHienTai", DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            kryptonDataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaTaiKhoan", HeaderText = "ID", DataPropertyName = "MaTaiKhoanThanhToan", Visible = false });
        }

        // --- Logic Tính Số Dư Hiện Tại (Giữ nguyên) ---
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

        // --- Load dữ liệu (Chỉ lấy tài khoản "Đang hoạt động") ---
        public void LoadDanhSach()
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    // LỌC: Chỉ lấy tài khoản có Trạng Thái là "Đang hoạt động"
                    var taiKhoanList = db.TaiKhoanThanhToans
                                    .Include(t => t.LoaiTaiKhoan)
                                    .Where(t => t.MaNguoiDung == MA_NGUOI_DUNG_HIEN_TAI && t.TrangThai == "Đang hoạt động")
                                    .OrderBy(t => t.TenTaiKhoan)
                                    .ToList();

                    // Map dữ liệu và tính số dư
                    _displayList = taiKhoanList.Select(t => new TaiKhoanDisplayModel
                    {
                        MaTaiKhoanThanhToan = t.MaTaiKhoanThanhToan,
                        TenTaiKhoan = t.TenTaiKhoan,
                        TenLoaiTaiKhoan = t.LoaiTaiKhoan != null ? t.LoaiTaiKhoan.TenLoaiTaiKhoan : "N/A",
                        SoDuHienTai = CalculateCurrentBalance(t.MaTaiKhoanThanhToan, t.SoDuBanDau)
                    }).ToList();

                    TimKiemVaLoc();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Logic Tra cứu ---
        private void TimKiemVaLoc()
        {
            string tuKhoa = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(tuKhoa))
            {
                bsTaiKhoan.DataSource = _displayList;
            }
            else
            {
                // Lọc trên TenTaiKhoan và LoaiTaiKhoan (vì Trạng Thái đã được lọc)
                var ketQuaLoc = _displayList.Where(p =>
                    (p.TenTaiKhoan != null && p.TenTaiKhoan.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
                    (p.TenLoaiTaiKhoan != null && p.TenLoaiTaiKhoan.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase))
                ).ToList();

                bsTaiKhoan.DataSource = ketQuaLoc;
            }
            bsTaiKhoan.ResetBindings(false);
        }

        private void TxtTimKiem_TextChanged(object sender, EventArgs e)
        {
            TimKiemVaLoc();
        }

        // --- Nút Thêm/Đóng (Logic giữ nguyên) ---
        private void BtnThem_Click(object sender, EventArgs e)
        {
            OnOpenThemTaiKhoan?.Invoke(this, 0);
        }

        private void BtnDong_Click(object sender, EventArgs e)
        {
            if (bsTaiKhoan.Current == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần đóng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedId = ((TaiKhoanDisplayModel)bsTaiKhoan.Current).MaTaiKhoanThanhToan;
            OnOpenDongTaiKhoan?.Invoke(this, selectedId);
        }
    }

    // --- CLASS ĐỂ HIỂN THỊ DỮ LIỆU ĐÃ TÍNH TOÁN ---
    public class TaiKhoanDisplayModel
    {
        public int MaTaiKhoanThanhToan { get; set; }
        public string TenTaiKhoan { get; set; }
        public string TenLoaiTaiKhoan { get; set; }
        public decimal SoDuHienTai { get; set; }
        // Đã bỏ thuộc tính TrangThai
    }
}