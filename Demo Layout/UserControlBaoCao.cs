using LiveCharts.Wpf;
using LiveCharts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Data; // Namespace chứa QLTCCNContext và Entity

namespace Demo_Layout
{
    public partial class UserControlBaoCao : UserControl
    {
        private const int CURRENT_USER_ID = 1;

        // Chuỗi kết nối
        private const string ConnectionString = "Data Source=DESKTOP-6QOFBT9\\SQLEXPRESS;Initial Catalog=QLTCCN;Integrated Security=True;TrustServerCertificate=True";

        public UserControlBaoCao()
        {
            InitializeComponent();
            ConfigCharts();
        }

        // --- HÀM HỖ TRỢ KHỞI TẠO CONTEXT ---
        private QLTCCNContext GetContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<QLTCCNContext>();
            optionsBuilder.UseSqlServer(ConnectionString);
            return new QLTCCNContext(optionsBuilder.Options);
        }

        private void ConfigCharts()
        {
            pieChartChiTieu.LegendLocation = LegendLocation.Right;

            // Cấu hình zoom/pan cho biểu đồ đường nếu cần
            cartesianChartXuHuong.Zoom = ZoomingOptions.X;
            cartesianChartXuHuong.Pan = PanningOptions.X;
        }

        private void UserControlBaoCao_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            // Mặc định từ đầu tháng đến hiện tại
            dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
            dtpDenNgay.Value = now;

            LoadComboBoxTaiKhoan();
            LoadDashboardData();
        }

        private void LoadComboBoxTaiKhoan()
        {
            try
            {
                using (var context = GetContext())
                {
                    var listTK = context.TaiKhoanThanhToans
                                        .Where(t => t.MaNguoiDung == CURRENT_USER_ID)
                                        .Select(t => new { t.MaTaiKhoanThanhToan, t.TenTaiKhoan })
                                        .ToList();

                    // Chèn mục "Tất cả" lên đầu
                    listTK.Insert(0, new { MaTaiKhoanThanhToan = 0, TenTaiKhoan = "(Tất cả tài khoản)" });

                    cboTaiKhoan.DataSource = listTK;
                    cboTaiKhoan.DisplayMember = "TenTaiKhoan";
                    cboTaiKhoan.ValueMember = "MaTaiKhoanThanhToan";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách tài khoản: " + ex.Message);
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        // ==================================================================================
        // HÀM CHÍNH: ĐIỀU PHỐI DỮ LIỆU
        // ==================================================================================
        private void LoadDashboardData()
        {
            DateTime fromDate = dtpTuNgay.Value.Date;
            DateTime toDate = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1); // Lấy hết ngày cuối cùng

            int maTaiKhoan = 0;
            if (cboTaiKhoan.SelectedValue != null && int.TryParse(cboTaiKhoan.SelectedValue.ToString(), out int val))
            {
                maTaiKhoan = val;
            }

            try
            {
                using (var context = GetContext())
                {
                    // 1. QUERY EF CORE
                    var query = context.GiaoDichs
                        .Include(g => g.DanhMucChiTieu)      // Join DanhMuc
                        .Include(g => g.TaiKhoanThanhToan)   // Join TaiKhoan (để lấy tên hiển thị biểu đồ cột)
                        .Where(g => g.MaNguoiDung == CURRENT_USER_ID &&
                                    g.NgayGiaoDich >= fromDate &&
                                    g.NgayGiaoDich <= toDate);

                    if (maTaiKhoan != 0)
                    {
                        query = query.Where(g => g.MaTaiKhoanThanhToan == maTaiKhoan);
                    }

                    // 2. CHUYỂN DỮ LIỆU SANG DTO
                    var rawData = query.Select(g => new DashboardDto
                    {
                        SoTien = (double)g.SoTien,
                        NgayGiaoDich = g.NgayGiaoDich,
                        MaLoaiGiaoDich = g.MaLoaiGiaoDich ?? 0,

                        // Xử lý null an toàn
                        TenDanhMuc = g.DanhMucChiTieu != null ? g.DanhMucChiTieu.TenDanhMuc : "Không xác định",
                        TenTaiKhoan = g.TaiKhoanThanhToan != null ? g.TaiKhoanThanhToan.TenTaiKhoan : "Đã xóa"
                    }).ToList();

                    // 3. GỌI CÁC HÀM CẬP NHẬT GIAO DIỆN
                    UpdatePieChart_CoCauChiTieu(rawData);
                    UpdateLabel_TongThuNhap(rawData);
                    UpdateLineChart_XuHuong(rawData);
                    UpdateColumnChart_ThuChi(rawData, maTaiKhoan == 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải Dashboard: " + ex.Message);
            }
        }

        // ==================================================================================
        // HÀM 1: CẬP NHẬT BIỂU ĐỒ TRÒN (CƠ CẤU CHI TIÊU)
        // ==================================================================================
        private void UpdatePieChart_CoCauChiTieu(List<DashboardDto> data)
        {
            // Lọc Chi (MaLoai = 2)
            var chiTieuData = data
                .Where(x => x.MaLoaiGiaoDich == 2)
                .GroupBy(x => x.TenDanhMuc)
                .Select(g => new { Name = g.Key, Total = g.Sum(x => x.SoTien) })
                .OrderByDescending(x => x.Total)
                .ToList();

            var pieSeries = new SeriesCollection();

            // Top 5 danh mục
            foreach (var item in chiTieuData.Take(5))
            {
                pieSeries.Add(new PieSeries
                {
                    Title = item.Name,
                    Values = new ChartValues<double> { item.Total },
                    DataLabels = true,
                    LabelPoint = p => string.Format("{0} ({1:P})", p.Y.ToString("N0"), p.Participation)
                });
            }

            // Mục "Khác"
            var otherTotal = chiTieuData.Skip(5).Sum(x => x.Total);
            if (otherTotal > 0)
            {
                pieSeries.Add(new PieSeries
                {
                    Title = "Khác",
                    Values = new ChartValues<double> { otherTotal },
                    DataLabels = true,
                    LabelPoint = p => string.Format("{0} ({1:P})", p.Y.ToString("N0"), p.Participation),
                    Fill = System.Windows.Media.Brushes.Gray
                });
            }

            pieChartChiTieu.Series = pieSeries;
        }

        // ==================================================================================
        // HÀM 2: CẬP NHẬT TỔNG THU NHẬP
        // ==================================================================================
        private void UpdateLabel_TongThuNhap(List<DashboardDto> data)
        {
            var tongThu = data.Where(x => x.MaLoaiGiaoDich == 1).Sum(x => x.SoTien);
            lblTongThuNhap.Text = $"{tongThu:N0} đ";
        }

        // ==================================================================================
        // HÀM 3: CẬP NHẬT BIỂU ĐỒ ĐƯỜNG (XU HƯỚNG CHI TIÊU)
        // ==================================================================================
        private void UpdateLineChart_XuHuong(List<DashboardDto> data)
        {
            var xuHuongData = data
                .Where(x => x.MaLoaiGiaoDich == 2) // Chỉ lấy Chi
                .GroupBy(x => x.NgayGiaoDich.Date)
                .Select(g => new { Date = g.Key, Total = g.Sum(x => x.SoTien) })
                .OrderBy(x => x.Date)
                .ToList();

            cartesianChartXuHuong.Series.Clear();
            cartesianChartXuHuong.AxisX.Clear();
            cartesianChartXuHuong.AxisY.Clear();

            if (xuHuongData.Any())
            {
                var lineSeries = new LineSeries
                {
                    Title = "Chi tiêu",
                    Values = new ChartValues<double>(xuHuongData.Select(x => x.Total)),
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 10,
                    LineSmoothness = 0
                };
                cartesianChartXuHuong.Series = new SeriesCollection { lineSeries };

                cartesianChartXuHuong.AxisX.Add(new Axis
                {
                    Title = "Thời gian",
                    Labels = xuHuongData.Select(x => x.Date.ToString("dd/MM")).ToList(),
                    Separator = new Separator { Step = 1 } // Tùy chỉnh step nếu dữ liệu quá dày
                });

                cartesianChartXuHuong.AxisY.Add(new Axis
                {
                    LabelFormatter = value => value.ToString("N0")
                });
            }
        }

        // ==================================================================================
        // HÀM 4: CẬP NHẬT BIỂU ĐỒ CỘT (SO SÁNH THU - CHI)
        // ==================================================================================
        private void UpdateColumnChart_ThuChi(List<DashboardDto> data, bool isAllAccounts)
        {
            cartesianChartThuChi.Series.Clear();
            cartesianChartThuChi.AxisX.Clear();
            cartesianChartThuChi.AxisY.Clear();

            List<string> labels = new List<string>();
            ChartValues<double> incomeValues = new ChartValues<double>();
            ChartValues<double> expenseValues = new ChartValues<double>();

            if (isAllAccounts)
            {
                // TRƯỜNG HỢP 1: Chọn "Tất cả" -> Nhóm theo Tên Tài Khoản
                // Đã có TenTaiKhoan trong DTO, không cần query lại DB (Tối ưu performance)
                var groupedData = data
                    .GroupBy(x => x.TenTaiKhoan)
                    .Select(g => new
                    {
                        TenTaiKhoan = g.Key,
                        Thu = g.Where(x => x.MaLoaiGiaoDich == 1).Sum(x => x.SoTien),
                        Chi = g.Where(x => x.MaLoaiGiaoDich == 2).Sum(x => x.SoTien)
                    })
                    .ToList();

                foreach (var item in groupedData)
                {
                    labels.Add(item.TenTaiKhoan);
                    incomeValues.Add(item.Thu);
                    expenseValues.Add(item.Chi);
                }
            }
            else
            {
                // TRƯỜNG HỢP 2: Chọn 1 tài khoản -> Hiển thị Tổng Thu vs Tổng Chi
                var totalThu = data.Where(x => x.MaLoaiGiaoDich == 1).Sum(x => x.SoTien);
                var totalChi = data.Where(x => x.MaLoaiGiaoDich == 2).Sum(x => x.SoTien);

                labels.Add("Tổng quan");
                incomeValues.Add(totalThu);
                expenseValues.Add(totalChi);
            }

            // Vẽ biểu đồ
            cartesianChartThuChi.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Thu",
                    Values = incomeValues,
                    Fill = System.Windows.Media.Brushes.Green,
                    DataLabels = true,
                    LabelPoint = p => p.Y.ToString("N0"),
                    MaxColumnWidth = 30
                },
                new ColumnSeries
                {
                    Title = "Chi",
                    Values = expenseValues,
                    Fill = System.Windows.Media.Brushes.Red,
                    DataLabels = true,
                    LabelPoint = p => p.Y.ToString("N0"),
                    MaxColumnWidth = 30
                }
            };

            cartesianChartThuChi.AxisX.Add(new Axis
            {
                Labels = labels,
                Separator = new Separator { Step = 1 }
            });

            cartesianChartThuChi.AxisY.Add(new Axis
            {
                LabelFormatter = value => value.ToString("N0")
            });
        }

        // DTO đã cập nhật thêm TenTaiKhoan
        public class DashboardDto
        {
            public double SoTien { get; set; }
            public DateTime NgayGiaoDich { get; set; }
            public int MaLoaiGiaoDich { get; set; }
            public string TenDanhMuc { get; set; }
            public string TenTaiKhoan { get; set; } // Thêm trường này để tối ưu biểu đồ cột
        }
    }
}