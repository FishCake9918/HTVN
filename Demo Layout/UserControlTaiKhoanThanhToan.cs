using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using Krypton.Toolkit;

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
        private List<TaiKhoanFullModel> _fullAccountsList = new List<TaiKhoanFullModel>(); // Để lưu trữ dữ liệu gốc

        // KHAI BÁO CONTROLS CẦN CÓ Ở CUỐI TRANG (Giả định bạn đã thêm vào panel3)
        public KryptonComboBox cmbLocTaiKhoan;
        public KryptonLabel lblSoDuBanDauDetail; // Label hiển thị SD ban đầu
        public KryptonLabel lblSoDuKhaDungDetail; // Label hiển thị SD khả dụng


        public UserControlTaiKhoanThanhToan(IDbContextFactory<QLTCCNContext> dbFactory)
        {
            InitializeComponent();
            _dbFactory = dbFactory;

            kryptonDataGridView1.DataSource = bsTaiKhoan;

            this.Load += UserControlTaiKhoanThanhToan_Load;
            this.txtTimKiem.TextChanged += TxtTimKiem_TextChanged;
            this.txtTimKiem.KeyPress += TxtTimKiem_KeyPress;

            this.btnThem.Click += BtnThem_Click;
            this.btnDong.Click += BtnDong_Click;
            this.kryptonDataGridView1.DoubleClick += KryptonDataGridView1_DoubleClick;

            // Đăng ký sự kiện cho ComboBox lọc mới
            if (cmbLocTaiKhoan != null)
            {
                this.cmbLocTaiKhoan.SelectedIndexChanged += CmbLocTaiKhoan_SelectedIndexChanged;
            }

            ConfigureGridView();
        }

        private void KryptonDataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (bsTaiKhoan.Current != null)
            {
                int selectedId = ((TaiKhoanDisplayModel)bsTaiKhoan.Current).MaTaiKhoanThanhToan;
                OnOpenThemTaiKhoan?.Invoke(this, selectedId);
            }
        }

        private void UserControlTaiKhoanThanhToan_Load(object sender, EventArgs e)
        {
            txtTimKiem.Text = string.Empty;
            txtTimKiem.ForeColor = Color.Black;
            LogHelper.GhiLog(_dbFactory, "Quản lý tài khoản thanh toán", MA_NGUOI_DUNG_HIEN_TAI); // ghi log
            LoadDanhSach();
            // Load ComboBox lọc tài khoản sau khi LoadDanhSach để có dữ liệu
            if (cmbLocTaiKhoan != null)
            {
                LoadComboBoxLocTaiKhoan();
            }
        }

        // --- Cấu hình DataGridView ---
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

            // CỘT MỚI: Số dư Ban đầu
            kryptonDataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoDuBanDau", HeaderText = "Số dư Ban đầu", DataPropertyName = "SoDuBanDau", DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });

            // Số dư Khả dụng
            kryptonDataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoDuHienTai", HeaderText = "Số dư Khả dụng", DataPropertyName = "SoDuHienTai", DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });

            kryptonDataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaTaiKhoan", HeaderText = "ID", DataPropertyName = "MaTaiKhoanThanhToan", Visible = false });
        }

        // --- LOGIC LỌC MỚI: Load ComboBox Tài khoản ---
        private void LoadComboBoxLocTaiKhoan()
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    var taiKhoanList = db.TaiKhoanThanhToans
                                         .Where(t => t.MaNguoiDung == MA_NGUOI_DUNG_HIEN_TAI)
                                         .Select(t => new
                                         {
                                             t.MaTaiKhoanThanhToan,
                                             t.TenTaiKhoan,
                                             t.SoDuBanDau
                                         })
                                         .OrderBy(t => t.TenTaiKhoan)
                                         .ToList();

                    // Thêm mục "Tất cả" (ID = 0)
                    var allItem = new { MaTaiKhoanThanhToan = 0, TenTaiKhoan = "(Chọn Tài khoản)", SoDuBanDau = 0M };
                    taiKhoanList.Insert(0, allItem);

                    cmbLocTaiKhoan.DataSource = taiKhoanList;
                    cmbLocTaiKhoan.DisplayMember = "TenTaiKhoan";
                    cmbLocTaiKhoan.ValueMember = "MaTaiKhoanThanhToan";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách tài khoản: {ex.Message}", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Logic Tính Số Dư Hiện Tại (Khả dụng) ---
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

                // Công thức: Số dư Khả dụng = Số dư Ban đầu + Thu - Chi
                return soDuBanDau + totalThu - totalChi;
            }
        }

        // --- Load dữ liệu (Read) ---
        public void LoadDanhSach()
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    var taiKhoanList = db.TaiKhoanThanhToans
                                         .Include(t => t.LoaiTaiKhoan)
                                         .Where(t => t.MaNguoiDung == MA_NGUOI_DUNG_HIEN_TAI && t.TrangThai == "Đang hoạt động")
                                         .OrderBy(t => t.TenTaiKhoan)
                                         .ToList();

                    // Lưu trữ dữ liệu full để tính số dư ban đầu cho bộ lọc
                    _fullAccountsList = taiKhoanList.Select(t => new TaiKhoanFullModel
                    {
                        MaTaiKhoanThanhToan = t.MaTaiKhoanThanhToan,
                        SoDuBanDau = t.SoDuBanDau
                    }).ToList();

                    // Map dữ liệu và tính số dư
                    _displayList = taiKhoanList.Select(t => new TaiKhoanDisplayModel
                    {
                        MaTaiKhoanThanhToan = t.MaTaiKhoanThanhToan,
                        TenTaiKhoan = t.TenTaiKhoan,
                        TenLoaiTaiKhoan = t.LoaiTaiKhoan != null ? t.LoaiTaiKhoan.TenLoaiTaiKhoan : "N/A",
                        SoDuBanDau = t.SoDuBanDau, // Thêm Số dư ban đầu
                        SoDuHienTai = CalculateCurrentBalance(t.MaTaiKhoanThanhToan, t.SoDuBanDau) // Số dư Khả dụng
                    }).ToList();

                    TimKiemVaLoc();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Logic Lọc chính (Không áp dụng lọc ComboBox ở đây) ---
        private void TimKiemVaLoc()
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            var danhSachLoc = _displayList.AsEnumerable();

            // Lọc theo TextBox
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

        // --- LOGIC: Xử lý thay đổi ComboBox lọc ---
        private void CmbLocTaiKhoan_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy ID tài khoản được chọn
            int selectedTaiKhoanId = (cmbLocTaiKhoan.SelectedValue is int id) ? id : 0;

            UpdateSoDuChiTiet(selectedTaiKhoanId); // Cập nhật labels chi tiết

            // Nếu người dùng chọn một tài khoản, hiển thị chỉ tài khoản đó trong Grid
            if (selectedTaiKhoanId != 0)
            {
                bsTaiKhoan.DataSource = _displayList.Where(t => t.MaTaiKhoanThanhToan == selectedTaiKhoanId).ToList();
            }
            else
            {
                // Nếu chọn "(Chọn Tài khoản)", hiển thị toàn bộ danh sách
                bsTaiKhoan.DataSource = _displayList;
            }
            bsTaiKhoan.ResetBindings(false);
        }

        // --- LOGIC: Cập nhật Labels Số dư chi tiết ---
        private void UpdateSoDuChiTiet(int selectedTaiKhoanId)
        {
            if (lblSoDuBanDauDetail == null || lblSoDuKhaDungDetail == null) return;

            if (selectedTaiKhoanId == 0)
            {
                // Hiển thị trạng thái chờ chọn
                lblSoDuBanDauDetail.Text = "Số dư Ban đầu: --";
                lblSoDuKhaDungDetail.Text = "Số dư Khả dụng: --";
                return;
            }

            // Tìm kiếm thông tin số dư ban đầu từ list đã lưu
            var accountFromDb = _fullAccountsList.FirstOrDefault(t => t.MaTaiKhoanThanhToan == selectedTaiKhoanId);
            // Tìm kiếm số dư khả dụng đã tính toán từ list hiển thị
            var accountFromDisplayList = _displayList.FirstOrDefault(t => t.MaTaiKhoanThanhToan == selectedTaiKhoanId);

            if (accountFromDb != null && accountFromDisplayList != null)
            {
                decimal soDuBanDau = accountFromDb.SoDuBanDau;
                decimal soDuKhaDung = accountFromDisplayList.SoDuHienTai;

                lblSoDuBanDauDetail.Text = $"Số dư Ban đầu: {soDuBanDau:N0} VND";
                lblSoDuKhaDungDetail.Text = $"Số dư Khả dụng: {soDuKhaDung:N0} VND";
            }
        }

        private void TxtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
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
        public decimal SoDuBanDau { get; set; } // Số dư ban đầu
        public decimal SoDuHienTai { get; set; } // Số dư Khả dụng
    }

    // --- MODEL ĐỂ LƯU TRỮ SỐ DƯ BAN ĐẦU (Dùng cho logic bộ lọc) ---
    public class TaiKhoanFullModel
    {
        public int MaTaiKhoanThanhToan { get; set; }
        public decimal SoDuBanDau { get; set; }
    }
}