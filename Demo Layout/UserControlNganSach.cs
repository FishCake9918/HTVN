using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
// Đã loại bỏ using Krypton.Toolkit
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Configurations;

namespace Demo_Layout
{
    public delegate void OpenNganSachFormHandler(object sender, int nganSachId);

    // Đã thay đổi UserControlNganSach để không dùng Krypton
    public partial class UserControlNganSach : UserControl
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private BindingSource bsNganSach = new BindingSource();
        private List<NganSachViewModel> _fullList = new List<NganSachViewModel>();
        private const int MA_NGUOI_DUNG_HIEN_TAI = 1;
        private const int MA_LOAI_GIAO_DICH_CHI = 2;

        public event OpenNganSachFormHandler OnOpenEditForm;

        public UserControlNganSach(IDbContextFactory<QLTCCNContext> dbFactory)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            // GỌI HÀM CẤU HÌNH GIAO DIỆN CHUNG
            ConfigCharts();

            // Đã đổi từ kryptonDataGridView1 sang dataGridView1
            if (dataGridView1 != null)
            {
                dataGridView1.DataSource = bsNganSach;
                dataGridView1.DoubleClick += KryptonDataGridView1_DoubleClick;
            }

            this.Load += UserControlNganSach_Load;

            // Đã đổi từ Krypton control sang WinForms control tương ứng
            if (txtTimKiem != null) this.txtTimKiem.TextChanged += (s, e) => TimKiemVaLoc();
            if (cmbLocThang != null) this.cmbLocThang.SelectedIndexChanged += (s, e) => TimKiemVaLoc();
            if (txtLocNam != null)
            {
                this.txtLocNam.TextChanged += (s, e) => TimKiemVaLoc();
                this.txtLocNam.KeyPress += TxtLocNam_KeyPress;
            }

            if (btnThem != null) this.btnThem.Click += BtnThem_Click;
            if (btnSua != null) this.btnSua.Click += BtnSua_Click;
            if (btnXoa != null) this.btnXoa.Click += BtnXoa_Click;

            ConfigureGridView();
            LoadLocThangComboBox();
        }
        private System.Windows.Media.Color[] _chartColors = new System.Windows.Media.Color[]
{
            System.Windows.Media.Color.FromRgb(255, 99, 132), // Đỏ
            System.Windows.Media.Color.FromRgb(54, 162, 235), // Xanh dương
            System.Windows.Media.Color.FromRgb(255, 206, 86), // Vàng
            System.Windows.Media.Color.FromRgb(75, 192, 192), // Xanh lá cây
            System.Windows.Media.Color.FromRgb(153, 102, 255), // Tím
            System.Windows.Media.Color.FromRgb(255, 159, 64), // Cam
            System.Windows.Media.Color.FromRgb(200, 200, 200) // Xám cho "Khác"
};
        private void ConfigCharts()
        {
            // txtTimKiem đã là WinForms TextBox
            txtTimKiem.Text = string.Empty;
            txtTimKiem.ForeColor = Color.Black;
            LogHelper.GhiLog(_dbFactory, "Quản lý ngân sách", MA_NGUOI_DUNG_HIEN_TAI); // ghi log
            if (pieChartNganSach != null)
            {
                // Cấu hình cơ bản (chỉ thuộc tính trực tiếp)
                pieChartNganSach.LegendLocation = LiveCharts.LegendLocation.Right;
                pieChartNganSach.InnerRadius = 50;
            }
        }

        // --- RÀNG BUỘC NHẬP SỐ CHO NĂM (Không đổi) ---
        private void TxtLocNam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // --- XỬ LÝ ĐÚP CHUỘT (CHỈNH SỬA) ---
        // Vẫn giữ tên hàm nhưng áp dụng cho DataGridView tiêu chuẩn
        private void KryptonDataGridView1_DoubleClick(object sender, EventArgs e)
        {
            BtnSua_Click(sender, e);
        }

        private void UserControlNganSach_Load(object sender, EventArgs e)
        {
            if (txtTimKiem != null) this.txtTimKiem.Text = string.Empty;
            // Đã đổi txtLocNam thành TextBox
            if (txtLocNam != null) this.txtLocNam.Text = DateTime.Today.Year.ToString();

            // Đã đổi cmbLocThang thành ComboBox
            if (cmbLocThang != null && cmbLocThang.DataSource != null)
            {
                cmbLocThang.SelectedValue = DateTime.Today.Month;
            }

            LoadDanhSach();
        }

        // --- Tải dữ liệu cho ComboBox Lọc Tháng ---
        private void LoadLocThangComboBox()
        {
            var months = Enumerable.Range(1, 12)
                .Select(m => new { MonthValue = m, MonthName = $"Tháng {m}" })
                .ToList();

            months.Insert(0, new { MonthValue = 0, MonthName = "Tất cả Tháng" });

            // cmbLocThang đã đổi thành ComboBox tiêu chuẩn
            cmbLocThang.DataSource = months;
            cmbLocThang.DisplayMember = "MonthName";
            cmbLocThang.ValueMember = "MonthValue";
        }

        // --- PHƯƠNG THỨC MỚI: Lấy ID Danh mục con (BFS) (Không đổi) ---
        private List<int> GetDescendantCategoryIds(int parentId, List<DanhMucChiTieu> allCategories)
        {
            var resultIds = new List<int> { parentId };
            var queue = new Queue<int>();
            queue.Enqueue(parentId);

            while (queue.Count > 0)
            {
                var currentParentId = queue.Dequeue();
                var children = allCategories
                    .Where(c => c.DanhMucCha.HasValue && c.DanhMucCha.Value == currentParentId)
                    .ToList();
                foreach (var child in children)
                {
                    if (!resultIds.Contains(child.MaDanhMuc))
                    {
                        resultIds.Add(child.MaDanhMuc);
                        queue.Enqueue(child.MaDanhMuc);
                    }
                }
            }
            return resultIds;
        }


        // --- Tải toàn bộ Danh sách từ DB và Tính toán Giao dịch (Không đổi) ---
        public void LoadDanhSach()
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    var allCategories = db.DanhMucChiTieus.ToList();

                    var nganSachList = db.BangNganSachs
                                         .Include(n => n.DanhMucChiTieu)
                                         .Where(n => n.MaNguoiDung == MA_NGUOI_DUNG_HIEN_TAI)
                                         .ToList();

                    var allGiaoDichChi = db.GiaoDichs
                        .Where(gd => gd.MaNguoiDung == MA_NGUOI_DUNG_HIEN_TAI &&
                                     gd.MaLoaiGiaoDich == MA_LOAI_GIAO_DICH_CHI)
                        .ToList();

                    // TÍNH TOÁN CÁC CỘT: Đã chi và Còn lại
                    _fullList = nganSachList.Select(n =>
                    {
                        List<int> relevantCategoryIds = GetDescendantCategoryIds(n.MaDanhMuc.Value, allCategories);

                        decimal daChi = allGiaoDichChi
                            .Where(gd => gd.MaDanhMuc.HasValue &&
                                         relevantCategoryIds.Contains(gd.MaDanhMuc.Value) &&
                                         gd.NgayGiaoDich >= n.NgayBatDau &&
                                         gd.NgayGiaoDich <= n.NgayKetThuc)
                            .Sum(gd => gd.SoTien);

                        return new NganSachViewModel
                        {
                            MaNganSach = n.MaNganSach,
                            TenDanhMuc = n.DanhMucChiTieu.TenDanhMuc,
                            SoTienNganSach = n.SoTien,
                            NgayBatDau = n.NgayBatDau,
                            NgayKetThuc = n.NgayKetThuc,
                            SoTienDaChi = daChi,
                            SoTienConLai = n.SoTien - daChi
                        };
                    }).ToList();
                    CapNhatPieChart(_fullList);


                    TimKiemVaLoc();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu ngân sách: {ex.Message}", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Logic Tra cứu & Lọc (Search & Filter) (Không đổi) ---
        public void TimKiemVaLoc()
        {
            var danhSachLoc = _fullList.AsEnumerable();

            string tuKhoa = txtTimKiem.Text.Trim();
            // Đã đổi cmbLocThang thành ComboBox tiêu chuẩn
            int? thangLoc = cmbLocThang.SelectedValue as int?;
            int? namLoc = int.TryParse(txtLocNam.Text.Trim(), out int n) ? (int?)n : null;

            // LỌC THEO NĂM
            if (namLoc.HasValue && namLoc.Value > 0)
            {
                danhSachLoc = danhSachLoc.Where(n => n.NgayBatDau.HasValue && n.NgayBatDau.Value.Year == namLoc.Value);
            }

            // LỌC THEO THÁNG
            if (thangLoc.HasValue && thangLoc.Value > 0)
            {
                danhSachLoc = danhSachLoc.Where(n => n.NgayBatDau.HasValue && n.NgayBatDau.Value.Month == thangLoc.Value);
            }

            // LỌC THEO TỪ KHÓA
            if (!string.IsNullOrEmpty(tuKhoa) && tuKhoa != " Tìm kiếm...")
            {
                danhSachLoc = danhSachLoc.Where(p =>
                    (p.TenDanhMuc != null && p.TenDanhMuc.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase))
                );
            }

            List<NganSachViewModel> ketQuaLoc = danhSachLoc.ToList();
            bsNganSach.DataSource = ketQuaLoc;
            bsNganSach.ResetBindings(false);

            CapNhatTongNganSach(ketQuaLoc);
        }

        // --- Cập nhật Tổng ngân sách ---
        private void CapNhatTongNganSach(List<NganSachViewModel> hienThiList)
        {
            decimal tongSoTien = hienThiList.Sum(n => n.SoTienNganSach);
            decimal tongDaChi = hienThiList.Sum(n => n.SoTienDaChi);
            decimal tongConLai = tongSoTien - tongDaChi;

            // Đã đổi KryptonLabel sang Label
            if (labelTongNS != null)
                labelTongNS.Text = $"TỔNG QUAN NGÂN SÁCH ĐÃ LỌC";

            if (lblValueTongNS != null)
                lblValueTongNS.Text = string.Format("Tổng Ngân sách: {0:N0} VNĐ", tongSoTien);

            if (lblValueTongDaChi != null)
                lblValueTongDaChi.Text = string.Format("Tổng Đã chi: {0:N0} VNĐ", tongDaChi);

            if (lblValueTongConLai != null)
                lblValueTongConLai.Text = string.Format("TỔNG CÒN LẠI: {0:N0} VNĐ", tongConLai);
        }

        // --- Cập nhật Biểu đồ Pie Chart (Không đổi) ---
        private void CapNhatPieChart(List<NganSachViewModel> hienThiList)
        {
            if (pieChartNganSach == null) return;

            decimal tongNganSach = hienThiList.Sum(n => n.SoTienNganSach);
            pieChartNganSach.Series.Clear();

            if (tongNganSach == 0) return;

            var coCauNganSachData = hienThiList
                .Where(n => n.SoTienNganSach > 0)
                .GroupBy(n => n.TenDanhMuc)
                .Select(g => new { Name = g.Key, Total = g.Sum(x => x.SoTienNganSach) })
                .OrderByDescending(x => x.Total)
                .ToList();

            var pieSeries = new SeriesCollection();
            int colorIndex = 0; // Để duyệt qua mảng màu

            // 1. Lấy Top N mục (ví dụ Top 5)
            var topItems = coCauNganSachData.Take(5).ToList();
            var otherTotal = coCauNganSachData.Skip(5).Sum(x => x.Total);

            foreach (var item in topItems)
            {
                pieSeries.Add(new PieSeries
                {
                    Title = item.Name,
                    Values = new ChartValues<double> { (double)item.Total },
                    DataLabels = true,
                    LabelPoint = point => string.Format("{0:N0} VNĐ ({1:P0})", (decimal)point.Y, point.Participation),
                    Fill = new System.Windows.Media.SolidColorBrush(_chartColors[colorIndex % _chartColors.Length]),
                    Stroke = System.Windows.Media.Brushes.Transparent,
                });
                colorIndex++;
            }

            // 2. Gom nhóm phần còn lại vào mục "Khác"
            if (otherTotal > 0)
            {
                pieSeries.Add(new PieSeries
                {
                    Title = "Khác",
                    Values = new ChartValues<double> { (double)otherTotal },
                    DataLabels = true,
                    LabelPoint = point => string.Format("{0:N0} VNĐ ({1:P0})", (decimal)point.Y, point.Participation),
                    Fill = new System.Windows.Media.SolidColorBrush(_chartColors[colorIndex % _chartColors.Length]), // Màu xám hoặc màu cuối cùng trong mảng
                    Stroke = System.Windows.Media.Brushes.Transparent,
                });
            }

            pieChartNganSach.Series = pieSeries;
            pieChartNganSach.LegendLocation = LiveCharts.LegendLocation.Right; // Đặt lại legend
            pieChartNganSach.Update();
        }
        private static readonly CartesianMapper<NganSachViewModel> NganSachMapper =
    Mappers.Xy<NganSachViewModel>()
           .Y(model => (double)model.SoTienNganSach) // Ánh xạ SoTienNganSach (decimal) sang Y (double)
           .X(model => 0); // Trục X không quan trọng cho Pie Chart


        // --- Cấu hình GridView ---
        private void ConfigureGridView()
        {
            // Đã đổi từ kryptonDataGridView1 sang dataGridView1
            if (dataGridView1 == null) return;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaNganSach", HeaderText = "ID", DataPropertyName = "MaNganSach", Visible = false });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "DanhMuc", HeaderText = "Danh mục", DataPropertyName = "TenDanhMuc" });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoTienNS", HeaderText = "Số tiền NS", DataPropertyName = "SoTienNganSach", DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });

            // Lưu ý: Color.Red và Color.Green trong DefaultCellStyle là System.Drawing.Color, không phải Krypton Color
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoTienDaChi", HeaderText = "Đã chi", DataPropertyName = "SoTienDaChi", DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight, ForeColor = Color.Red } });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoTienConLai", HeaderText = "Còn lại", DataPropertyName = "SoTienConLai", DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight, ForeColor = Color.Green } });

            // Ẩn Ngày Bắt đầu/Kết thúc
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayBatDau", HeaderText = "Bắt đầu", DataPropertyName = "NgayBatDau", Visible = false });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayKetThuc", HeaderText = "Kết thúc", DataPropertyName = "NgayKetThuc", Visible = false });
        }

        // --- Xử lý sự kiện CRUD (Không đổi) ---
        private void BtnThem_Click(object sender, EventArgs e)
        {
            OnOpenEditForm?.Invoke(this, 0);
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (bsNganSach.Current == null)
            {
                MessageBox.Show("Vui lòng chọn ngân sách cần Sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedId = ((NganSachViewModel)bsNganSach.Current).MaNganSach;
            OnOpenEditForm?.Invoke(this, selectedId);
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (bsNganSach.Current == null)
            {
                MessageBox.Show("Vui lòng chọn ngân sách cần Xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedNs = (NganSachViewModel)bsNganSach.Current;

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa ngân sách '{selectedNs.TenDanhMuc}' ({selectedNs.SoTienNganSach:N0}) không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (var db = _dbFactory.CreateDbContext())
                    {
                        var entityToDelete = db.BangNganSachs.FirstOrDefault(n => n.MaNganSach == selectedNs.MaNganSach);
                        if (entityToDelete != null)
                        {
                            db.BangNganSachs.Remove(entityToDelete);
                            db.SaveChanges();
                        }
                    }
                    LoadDanhSach();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa dữ liệu: {ex.Message}", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    public class NganSachViewModel
    {
        public int MaNganSach { get; set; }
        public string TenDanhMuc { get; set; }
        public decimal SoTienNganSach { get; set; }
        public decimal SoTienDaChi { get; set; }
        public decimal SoTienConLai { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
    }
}