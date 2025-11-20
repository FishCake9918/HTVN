using LiveCharts.Wpf;
using LiveCharts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data;

namespace Demo_Layout
{
    public partial class UserControlBaoCao : UserControl
    {
        private const int CURRENT_USER_ID = 1;

        public UserControlBaoCao()
        {
            InitializeComponent();
            ConfigCharts(); // Cấu hình giao diện biểu đồ
        }

        private void ConfigCharts()
        {
            // Cấu hình màu nền và legend chung
            pieChartChiTieu.LegendLocation = LegendLocation.Right;
        }

        private void UserControlBaoCao_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
            dtpDenNgay.Value = now;

            LoadComboBoxTaiKhoan();
            LoadDashboardData();
        }

        private void LoadComboBoxTaiKhoan()
        {
            try
            {
                using (var db = new QLTCCNContext())
                {
                    var listTK = db.TaiKhoanThanhToans
                                   .Where(t => t.MaNguoiDung == CURRENT_USER_ID)
                                   .Select(t => new { t.MaTaiKhoanThanhToan, t.TenTaiKhoan })
                                   .ToList();

                    listTK.Insert(0, new { MaTaiKhoanThanhToan = 0, TenTaiKhoan = "(Tất cả tài khoản)" });

                    cboTaiKhoan.DataSource = listTK;
                    cboTaiKhoan.DisplayMember = "TenTaiKhoan";
                    cboTaiKhoan.ValueMember = "MaTaiKhoanThanhToan";
                }
            }
            catch { }
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
            DateTime toDate = dtpDenNgay.Value.Date;
            int maTaiKhoan = (int)cboTaiKhoan.SelectedValue;

            try
            {
                using (var db = new QLTCCNContext())
                {
                    // 1. LẤY DỮ LIỆU THÔ (Raw Data) MỘT LẦN DUY NHẤT
                    var query = db.GIAO_DICH
                        .Include(g => g.DanhMuc)
                        .Where(g => g.MaNguoiDung == CURRENT_USER_ID &&
                                    g.NgayGiaoDich >= fromDate &&
                                    g.NgayGiaoDich <= toDate);

                    if (maTaiKhoan != 0)
                    {
                        query = query.Where(g => g.MaTaiKhoanThanhToan == maTaiKhoan);
                    }

                    // Chuyển dữ liệu sang DTO để dễ truyền vào các hàm con
                    var rawData = query.Select(g => new DashboardDto
                    {
                        SoTien = (double)g.SoTien,
                        NgayGiaoDich = g.NgayGiaoDich,
                        MaLoaiGiaoDich = g.MaLoaiGiaoDich ?? 0,
                        TenDanhMuc = g.DanhMuc.TenDanhMuc
                    }).ToList();

                    // 2. GỌI CÁC HÀM CON ĐỂ CẬP NHẬT TỪNG PHẦN
                    UpdatePieChart_CoCauChiTieu(rawData);
                    UpdateLabel_TongThuNhap(rawData);
                    UpdateLineChart_XuHuong(rawData);
                    UpdateColumnChart_ThuChi(rawData);
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
            // Lọc Chi (MaLoai = 2) và Group theo Danh Mục
            var chiTieuData = data
                .Where(x => x.MaLoaiGiaoDich == 2)
                .GroupBy(x => x.TenDanhMuc)
                .Select(g => new { Name = g.Key, Total = g.Sum(x => x.SoTien) })
                .OrderByDescending(x => x.Total)
                .ToList();

            var pieSeries = new SeriesCollection();

            // Lấy Top 5 danh mục lớn nhất (như yêu cầu)
            foreach (var item in chiTieuData.Take(5))
            {
                pieSeries.Add(new PieSeries
                {
                    Title = item.Name,
                    Values = new ChartValues<double> { item.Total },
                    DataLabels = true,
                    LabelPoint = p => string.Format("({1:P})", p.Y.ToString("N0"), p.Participation)
                });
            }

            // Gom nhóm phần còn lại vào mục "Khác"
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
            // Tính tổng các giao dịch Thu (MaLoai = 1)
            var tongThu = data.Where(x => x.MaLoaiGiaoDich == 1).Sum(x => x.SoTien);
            lblTongThuNhap.Text = $"{tongThu:N0} đ";
        }

        // ==================================================================================
        // HÀM 3: CẬP NHẬT BIỂU ĐỒ ĐƯỜNG (XU HƯỚNG CHI TIÊU)
        // ==================================================================================
        private void UpdateLineChart_XuHuong(List<DashboardDto> data)
        {
            // Group Chi tiêu theo Ngày
            var xuHuongData = data
                .Where(x => x.MaLoaiGiaoDich == 2)
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
                    Labels = xuHuongData.Select(x => x.Date.ToString("dd/mm")).ToList(),
                    Separator = new Separator { Step = 3 }
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
        private void UpdateColumnChart_ThuChi(List<DashboardDto> data)
        {
            cartesianChartThuChi.Series.Clear();
            cartesianChartThuChi.AxisX.Clear();
            cartesianChartThuChi.AxisY.Clear();

            int maTaiKhoan = 0;
            if (cboTaiKhoan.SelectedValue != null)
            {
                int.TryParse(cboTaiKhoan.SelectedValue.ToString(), out maTaiKhoan);
            }

            // TRƯỜNG HỢP 1: CHỌN "TẤT CẢ TÀI KHOẢN" -> HIỂN THỊ CẶP CỘT THU/CHI CHO TỪNG TÀI KHOẢN
            if (maTaiKhoan == 0)
            {
                List<string> accountNames = new List<string>();
                ChartValues<double> incomeValues = new ChartValues<double>(); // Danh sách giá trị Thu
                ChartValues<double> expenseValues = new ChartValues<double>(); // Danh sách giá trị Chi

                using (var db = new QLTCCN_DbContext())
                {
                    var taiKhoans = db.TAI_KHOAN_THANH_TOAN
                                      .Where(t => t.MaNguoiDung == CURRENT_USER_ID)
                                      .ToList();

                    foreach (var tk in taiKhoans)
                    {
                        // Tính riêng Thu và Chi cho tài khoản này
                        var thu = db.GIAO_DICH
                            .Where(g => g.MaTaiKhoanThanhToan == tk.MaTaiKhoanThanhToan
                                     && g.MaLoaiGiaoDich == 1 // Thu
                                     && g.NgayGiaoDich >= dtpTuNgay.Value
                                     && g.NgayGiaoDich <= dtpDenNgay.Value)
                            .Sum(g => g.SoTien);

                        var chi = db.GIAO_DICH
                            .Where(g => g.MaTaiKhoanThanhToan == tk.MaTaiKhoanThanhToan
                                     && g.MaLoaiGiaoDich == 2 // Chi
                                     && g.NgayGiaoDich >= dtpTuNgay.Value
                                     && g.NgayGiaoDich <= dtpDenNgay.Value)
                            .Sum(g => g.SoTien);

                        accountNames.Add(tk.TenTaiKhoan);
                        incomeValues.Add((double)thu);
                        expenseValues.Add((double)chi);
                    }
                }

                // Tạo 2 Series: Một cho Thu (Xanh), Một cho Chi (Đỏ)
                // LiveCharts sẽ tự động nhóm chúng lại theo từng mục trên trục X
                cartesianChartThuChi.Series = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Thu",
                        Values = incomeValues,
                        Fill = System.Windows.Media.Brushes.Green,
                        DataLabels = true,
                        LabelPoint = p => p.Y.ToString("N0"),
                        MaxColumnWidth = 30 // Độ rộng cột vừa phải
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
                    //Title = "Tài khoản",
                    Labels = accountNames, // Tên các tài khoản nằm dưới trục X
                    Separator = new Separator { Step = 1 }
                });
            }
            // TRƯỜNG HỢP 2: CHỌN 1 TÀI KHOẢN CỤ THỂ (Giữ nguyên logic so sánh tổng quát)
            else
            {
                var totalThu = data.Where(x => x.MaLoaiGiaoDich == 1).Sum(x => x.SoTien);
                var totalChi = data.Where(x => x.MaLoaiGiaoDich == 2).Sum(x => x.SoTien);

                cartesianChartThuChi.Series = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Tổng Thu",
                        Values = new ChartValues<double> { totalThu },
                        Fill = System.Windows.Media.Brushes.Green,
                        DataLabels = true,
                        LabelPoint = p => p.Y.ToString("N0"),
                        MaxColumnWidth = 50
                    },
                    new ColumnSeries
                    {
                        Title = "Tổng Chi",
                        Values = new ChartValues<double> { totalChi },
                        Fill = System.Windows.Media.Brushes.Red,
                        DataLabels = true,
                        LabelPoint = p => p.Y.ToString("N0"),
                        MaxColumnWidth = 50
                    }
                };

                cartesianChartThuChi.AxisX.Add(new Axis { Labels = new[] { "Tổng quan" }, ShowLabels = false });
            }

            // Trục Y chung
            cartesianChartThuChi.AxisY.Add(new Axis
            {
                LabelFormatter = value => value.ToString("N0")
            });
        }

        // Class hỗ trợ truyền dữ liệu (DTO)
        public class DashboardDto
        {
            public double SoTien { get; set; }
            public DateTime NgayGiaoDich { get; set; }
            public int MaLoaiGiaoDich { get; set; }
            public string TenDanhMuc { get; set; }
        }
    }
}

