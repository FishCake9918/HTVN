using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Piggy_Admin;

namespace Demo_Layout
{
    public partial class UserControlNganSach : UserControl
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider; // <-- Thêm
        private readonly CurrentUserContext _userContext;   // <-- Thêm
        private BindingSource bsNganSach = new BindingSource();
        private List<NganSachViewModel> _fullList = new List<NganSachViewModel>();
        private const int MA_LOAI_GIAO_DICH_CHI = 2;

        public UserControlNganSach(
            IDbContextFactory<QLTCCNContext> dbFactory,
            IServiceProvider serviceProvider,
            CurrentUserContext userContext)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
            _userContext = userContext;
            Dinhdangluoi.DinhDangLuoiNguoiDung(dataGridView1);
            ConfigCharts();
            if (dataGridView1 != null) { dataGridView1.DataSource = bsNganSach; dataGridView1.DoubleClick += KryptonDataGridView1_DoubleClick; }

            this.Load += UserControlNganSach_Load;
            if (txtTimKiem != null) this.txtTimKiem.TextChanged += (s, e) => TimKiemVaLoc();
            if (cmbLocThang != null) this.cmbLocThang.SelectedIndexChanged += (s, e) => TimKiemVaLoc();
            if (txtLocNam != null) { this.txtLocNam.TextChanged += (s, e) => TimKiemVaLoc(); this.txtLocNam.KeyPress += TxtLocNam_KeyPress; }

            if (btnThem != null) this.btnThem.Click += BtnThem_Click;
            if (btnSua != null) this.btnSua.Click += BtnSua_Click;
            if (btnXoa != null) this.btnXoa.Click += BtnXoa_Click;

            ConfigureGridView();
            LoadLocThangComboBox();
        }

        private void ConfigCharts()
        {
            txtTimKiem.Text = string.Empty;
            txtTimKiem.ForeColor = Color.Black;
            if (pieChartNganSach != null) { pieChartNganSach.LegendLocation = LiveCharts.LegendLocation.Right; pieChartNganSach.InnerRadius = 50; }
        }

        private void UserControlNganSach_Load(object sender, EventArgs e)
        {
            LogHelper.GhiLog(_dbFactory, "Quản lý ngân sách", _userContext.MaNguoiDung); // ghi log
            if (_userContext.MaNguoiDung == null) return;
            if (txtTimKiem != null) this.txtTimKiem.Text = string.Empty;
            if (txtLocNam != null) this.txtLocNam.Text = DateTime.Today.Year.ToString();
            if (cmbLocThang != null && cmbLocThang.DataSource != null) cmbLocThang.SelectedValue = DateTime.Today.Month;
            LoadDanhSach();
        }

        private void LoadLocThangComboBox()
        {
            var months = Enumerable.Range(1, 12).Select(m => new { MonthValue = m, MonthName = $"Tháng {m}" }).ToList();
            months.Insert(0, new { MonthValue = 0, MonthName = "Tất cả Tháng" });
            cmbLocThang.DataSource = months;
            cmbLocThang.DisplayMember = "MonthName";
            cmbLocThang.ValueMember = "MonthValue";
        }

        private List<int> GetDescendantCategoryIds(int parentId, List<DanhMucChiTieu> allCategories)
        {
            var resultIds = new List<int> { parentId };
            var queue = new Queue<int>();
            queue.Enqueue(parentId);
            while (queue.Count > 0)
            {
                var currentParentId = queue.Dequeue();
                var children = allCategories.Where(c => c.DanhMucCha.HasValue && c.DanhMucCha.Value == currentParentId).ToList();
                foreach (var child in children) if (!resultIds.Contains(child.MaDanhMuc)) { resultIds.Add(child.MaDanhMuc); queue.Enqueue(child.MaDanhMuc); }
            }
            return resultIds;
        }

        public void LoadDanhSach()
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    int currentUserId = _userContext.MaNguoiDung.Value;
                    var allCategories = db.DanhMucChiTieus.ToList();

                    // SỬA: Lọc theo User Context
                    var nganSachList = db.BangNganSachs.Include(n => n.DanhMucChiTieu).Where(n => n.MaNguoiDung == currentUserId).ToList();
                    var allGiaoDichChi = db.GiaoDichs.Where(gd => gd.MaNguoiDung == currentUserId && gd.MaLoaiGiaoDich == MA_LOAI_GIAO_DICH_CHI).ToList();

                    _fullList = nganSachList.Select(n =>
                    {
                        List<int> relevantCategoryIds = GetDescendantCategoryIds(n.MaDanhMuc.Value, allCategories);
                        decimal daChi = allGiaoDichChi.Where(gd => gd.MaDanhMuc.HasValue && relevantCategoryIds.Contains(gd.MaDanhMuc.Value) && gd.NgayGiaoDich >= n.NgayBatDau && gd.NgayGiaoDich <= n.NgayKetThuc).Sum(gd => gd.SoTien);
                        return new NganSachViewModel { MaNganSach = n.MaNganSach, TenDanhMuc = n.DanhMucChiTieu.TenDanhMuc, SoTienNganSach = n.SoTien, NgayBatDau = n.NgayBatDau, NgayKetThuc = n.NgayKetThuc, SoTienDaChi = daChi, SoTienConLai = n.SoTien - daChi };
                    }).ToList();
                    CapNhatPieChart(_fullList);
                    TimKiemVaLoc();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        public void TimKiemVaLoc()
        {
            var danhSachLoc = _fullList.AsEnumerable();
            string tuKhoa = txtTimKiem.Text.Trim();
            int? thangLoc = cmbLocThang.SelectedValue as int?;
            int? namLoc = int.TryParse(txtLocNam.Text.Trim(), out int n) ? (int?)n : null;

            if (namLoc.HasValue && namLoc.Value > 0) danhSachLoc = danhSachLoc.Where(n => n.NgayBatDau.HasValue && n.NgayBatDau.Value.Year == namLoc.Value);
            if (thangLoc.HasValue && thangLoc.Value > 0) danhSachLoc = danhSachLoc.Where(n => n.NgayBatDau.HasValue && n.NgayBatDau.Value.Month == thangLoc.Value);
            if (!string.IsNullOrEmpty(tuKhoa)) danhSachLoc = danhSachLoc.Where(p => (p.TenDanhMuc != null && p.TenDanhMuc.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)));

            List<NganSachViewModel> ketQuaLoc = danhSachLoc.ToList();
            bsNganSach.DataSource = ketQuaLoc;
            bsNganSach.ResetBindings(false);
            CapNhatTongNganSach(ketQuaLoc);
        }

        private void CapNhatTongNganSach(List<NganSachViewModel> hienThiList)
        {
            decimal tongSoTien = hienThiList.Sum(n => n.SoTienNganSach);
            decimal tongDaChi = hienThiList.Sum(n => n.SoTienDaChi);
            decimal tongConLai = tongSoTien - tongDaChi;
            if (labelTongNS != null) labelTongNS.Text = $"TỔNG QUAN NGÂN SÁCH ĐÃ LỌC";
            if (lblValueTongNS != null) lblValueTongNS.Text = string.Format("Tổng Ngân sách: {0:N0} VNĐ", tongSoTien);
            if (lblValueTongDaChi != null) lblValueTongDaChi.Text = string.Format("Tổng Đã chi: {0:N0} VNĐ", tongDaChi);
            if (lblValueTongConLai != null) lblValueTongConLai.Text = string.Format("TỔNG CÒN LẠI: {0:N0} VNĐ", tongConLai);
        }
        private System.Windows.Media.Color[] _chartColors = new System.Windows.Media.Color[]
        {
            System.Windows.Media.Color.FromRgb(255, 99, 132), System.Windows.Media.Color.FromRgb(54, 162, 235), System.Windows.Media.Color.FromRgb(255, 206, 86),
            System.Windows.Media.Color.FromRgb(75, 192, 192), System.Windows.Media.Color.FromRgb(153, 102, 255), System.Windows.Media.Color.FromRgb(255, 159, 64),
            System.Windows.Media.Color.FromRgb(200, 200, 200)
        };
        private void CapNhatPieChart(List<NganSachViewModel> hienThiList)
        {
            if (pieChartNganSach == null) return;
            decimal tongNganSach = hienThiList.Sum(n => n.SoTienNganSach);
            pieChartNganSach.Series.Clear();
            if (tongNganSach == 0) return;
            var coCauNganSachData = hienThiList.Where(n => n.SoTienNganSach > 0).GroupBy(n => n.TenDanhMuc).Select(g => new { Name = g.Key, Total = g.Sum(x => x.SoTienNganSach) }).OrderByDescending(x => x.Total).ToList();
            var pieSeries = new SeriesCollection();
            int colorIndex = 0;
            var topItems = coCauNganSachData.Take(5).ToList();
            var otherTotal = coCauNganSachData.Skip(5).Sum(x => x.Total);
            foreach (var item in topItems)
            {
                pieSeries.Add(new PieSeries { Title = item.Name, Values = new ChartValues<double> { (double)item.Total }, DataLabels = true, LabelPoint = point => string.Format("{0:N0} VNĐ ({1:P0})", (decimal)point.Y, point.Participation), Fill = new System.Windows.Media.SolidColorBrush(_chartColors[colorIndex % _chartColors.Length]), Stroke = System.Windows.Media.Brushes.Transparent, });
                colorIndex++;
            }
            if (otherTotal > 0)
            {
                pieSeries.Add(new PieSeries { Title = "Khác", Values = new ChartValues<double> { (double)otherTotal }, DataLabels = true, LabelPoint = point => string.Format("{0:N0} VNĐ ({1:P0})", (decimal)point.Y, point.Participation), Fill = new System.Windows.Media.SolidColorBrush(_chartColors[colorIndex % _chartColors.Length]), Stroke = System.Windows.Media.Brushes.Transparent, });
            }
            pieChartNganSach.Series = pieSeries; pieChartNganSach.LegendLocation = LiveCharts.LegendLocation.Right; pieChartNganSach.Update();
        }
        private void ConfigureGridView()
        {
            if (dataGridView1 == null) return;
            dataGridView1.AutoGenerateColumns = false; dataGridView1.AllowUserToAddRows = false; dataGridView1.ReadOnly = true; dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView1.MultiSelect = false; dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaNganSach", HeaderText = "ID", DataPropertyName = "MaNganSach", Visible = false });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "DanhMuc", HeaderText = "Danh mục", DataPropertyName = "TenDanhMuc" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoTienNS", HeaderText = "Số tiền NS", DataPropertyName = "SoTienNganSach", DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoTienDaChi", HeaderText = "Đã chi", DataPropertyName = "SoTienDaChi", DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight, ForeColor = Color.Red } });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoTienConLai", HeaderText = "Còn lại", DataPropertyName = "SoTienConLai", DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight, ForeColor = Color.Green } });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayBatDau", HeaderText = "Bắt đầu", DataPropertyName = "NgayBatDau", Visible = false });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayKetThuc", HeaderText = "Kết thúc", DataPropertyName = "NgayKetThuc", Visible = false });
        }
        private void TxtLocNam_KeyPress(object sender, KeyPressEventArgs e) { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true; }
        private void KryptonDataGridView1_DoubleClick(object sender, EventArgs e) => BtnSua_Click(sender, e);

        // --- XỬ LÝ MỞ FORM TRỰC TIẾP ---
        private void BtnThem_Click(object sender, EventArgs e)
        {
            var frm = ActivatorUtilities.CreateInstance<FrmThemSuaNganSach>(_serviceProvider);
            frm.SetId(0);
            if (frm.ShowDialog() == DialogResult.OK) LoadDanhSach();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (bsNganSach.Current == null) { MessageBox.Show("Vui lòng chọn ngân sách."); return; }
            int selectedId = ((NganSachViewModel)bsNganSach.Current).MaNganSach;

            var frm = ActivatorUtilities.CreateInstance<FrmThemSuaNganSach>(_serviceProvider);
            frm.SetId(selectedId);
            if (frm.ShowDialog() == DialogResult.OK) LoadDanhSach();
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (bsNganSach.Current == null) { MessageBox.Show("Vui lòng chọn ngân sách."); return; }
            var selectedNs = (NganSachViewModel)bsNganSach.Current;
            if (MessageBox.Show($"Xóa ngân sách '{selectedNs.TenDanhMuc}'?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (var db = _dbFactory.CreateDbContext())
                    {
                        var entityToDelete = db.BangNganSachs.FirstOrDefault(n => n.MaNganSach == selectedNs.MaNganSach);
                        if (entityToDelete != null) { db.BangNganSachs.Remove(entityToDelete); db.SaveChanges(); }
                    }
                    LoadDanhSach();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
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