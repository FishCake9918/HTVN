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
using MiniExcelLibs;
using System.IO;     // Nhớ using System.IO

namespace Demo_Layout
{
    public partial class UserControlBaoCao : UserControl
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider;

        // 1. Khai báo biến giữ thông tin User
        private readonly CurrentUserContext _userContext;

        private bool _isLoading = true;

        public UserControlBaoCao(IDbContextFactory<QLTCCNContext> dbFactory, CurrentUserContext userContext)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _userContext = userContext; // <--- Lưu lại để dùng

            ConfigCharts();

            // [MỚI] Gán sự kiện kiểm tra ngày tháng
            dtpTuNgay.ValueChanged += Dtp_ValueChanged;
            dtpDenNgay.ValueChanged += Dtp_ValueChanged;
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
            LogHelper.GhiLog(_dbFactory, "Quản lý báo cáo", _userContext.MaNguoiDung); // ghi log


            LoadComboBoxTaiKhoan();
            LoadDashboardData();

            _isLoading = false; // Load xong
        }

        private void Dtp_ValueChanged(object sender, EventArgs e)
        {
            if (_isLoading) return;

            // Ràng buộc: Từ ngày không được lớn hơn Đến ngày
            if (dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Lỗi thời gian", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Quay về mốc set ban đầu (Đầu tháng - Hiện tại)
                _isLoading = true; // Tạm khóa để không bị loop sự kiện
                DateTime now = DateTime.Now;
                dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
                dtpDenNgay.Value = now;
                _isLoading = false;

                return;
            }

            // Nếu đúng thì load lại dữ liệu
            LoadDashboardData();
        }

        private void LoadComboBoxTaiKhoan()
        {
            try
            {
                // 3. DÙNG NHÀ MÁY TẠO KẾT NỐI (Thay vì new trực tiếp)
                using (var db = _dbFactory.CreateDbContext())
                {
                    int userId = _userContext.MaNguoiDung.Value;

                    var listTK = db.TaiKhoanThanhToans
                                   .Where(t => t.MaNguoiDung == userId
                                            && t.TrangThai != "Đóng") // <--- Thêm điều kiện này
                                   .Select(t => new { t.MaTaiKhoanThanhToan, t.TenTaiKhoan })
                                   .ToList();

                    // Thêm mục "Tất cả"
                    listTK.Insert(0, new { MaTaiKhoanThanhToan = 0, TenTaiKhoan = "(Tất cả tài khoản)" });

                    cboTaiKhoan.DataSource = listTK;
                    cboTaiKhoan.DisplayMember = "TenTaiKhoan";
                    cboTaiKhoan.ValueMember = "MaTaiKhoanThanhToan";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải tài khoản: " + ex.Message);
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {

            // 1. Cấu hình hộp thoại lưu file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel (*.xlsx)|*.xlsx";
            saveFileDialog.FileName = $"ThongKeGiaoDich_{DateTime.Now:ddMMyyyy_HHmmss}.xlsx";

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                // 2. Lấy điều kiện lọc (Giống hệt LoadData)
                int maTaiKhoanLoc = 0;
                if (cboTaiKhoan.SelectedValue != null && int.TryParse(cboTaiKhoan.SelectedValue.ToString(), out int val))
                {
                    maTaiKhoanLoc = val;
                }

                // 3. Truy vấn dữ liệu
                using (var context = _dbFactory.CreateDbContext())
                {
                    int userId = _userContext.MaNguoiDung.Value;

                    var query = context.GiaoDichs
                        .Include(g => g.LoaiGiaoDich)
                        .Include(g => g.DoiTuongGiaoDich)
                        .Include(g => g.TaiKhoanThanhToan)
                        .Include(g => g.DanhMucChiTieu)
                        .Where(g => g.MaNguoiDung == userId);

                    // Áp dụng lọc tài khoản nếu có
                    if (maTaiKhoanLoc > 0)
                    {
                        query = query.Where(g => g.MaTaiKhoanThanhToan == maTaiKhoanLoc);
                    }

                    // 4. CHUẨN BỊ DỮ LIỆU XUẤT (Projection)
                    // Lưu ý: Tên thuộc tính ở đây sẽ là Tiêu đề cột trong Excel
                    // Ta bỏ qua các cột "Ma..." vì user không cần xem
                    var dataToExport = query
                        .OrderByDescending(x => x.NgayGiaoDich) // Sắp xếp trước khi lấy
                        .Select(gd => new
                        {
                            NgayGiaoDich = gd.NgayGiaoDich, // Excel sẽ tự format ngày
                            TenGiaoDich = gd.TenGiaoDich,
                            SoTien = gd.SoTien,
                            Loai = gd.LoaiGiaoDich != null ? gd.LoaiGiaoDich.TenLoaiGiaoDich : "",
                            DanhMuc = gd.DanhMucChiTieu != null ? gd.DanhMucChiTieu.TenDanhMuc : "Khác",
                            TaiKhoan = gd.TaiKhoanThanhToan != null ? gd.TaiKhoanThanhToan.TenTaiKhoan : "",
                            DoiTuong = gd.DoiTuongGiaoDich != null ? gd.DoiTuongGiaoDich.TenDoiTuong : "",
                            GhiChu = gd.GhiChu
                        })
                        .ToList(); // Thực thi truy vấn

                    if (dataToExport.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu giao dịch nào để xuất.");
                        return;
                    }

                    // 5. Ghi ra file Excel
                    // OverwriteExisting: Ghi đè nếu file đã tồn tại
                    MiniExcel.SaveAs(saveFileDialog.FileName, dataToExport);

                    // 6. Mở file sau khi xuất xong
                    var p = new System.Diagnostics.Process();
                    p.StartInfo = new System.Diagnostics.ProcessStartInfo(saveFileDialog.FileName) { UseShellExecute = true };
                    p.Start();

                    MessageBox.Show("Xuất dữ liệu thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message);
            }

        }


        // ==================================================================================
        // HÀM CHÍNH: ĐIỀU PHỐI DỮ LIỆU
        // ==================================================================================
        private void LoadDashboardData()
        {
            DateTime fromDate = dtpTuNgay.Value.Date;
            DateTime toDate = dtpDenNgay.Value.Date;

            // Fix lỗi lấy ngày: Nếu toDate là 00:00:00 thì sẽ mất dữ liệu của ngày hôm đó
            // Nên chỉnh toDate thành cuối ngày 23:59:59
            toDate = toDate.AddDays(1).AddTicks(-1);

            int maTaiKhoan = (int)cboTaiKhoan.SelectedValue;

            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {

                    int userId = _userContext.MaNguoiDung.Value;

                    // 1. Truy vấn bảng GIAO_DICH
                    var query = db.GiaoDichs
                                .Include(g => g.TaiKhoanThanhToan)
                                // Include sâu vào DanhMucChaNavigation để lấy tên cha
                                .Include(g => g.DanhMucChiTieu)
                                    .ThenInclude(dm => dm.DanhMucChaNavigation)
                                .Where(g => g.MaNguoiDung == userId &&
                                            g.NgayGiaoDich >= fromDate &&
                                            g.NgayGiaoDich <= toDate);

                    query = query.Where(g => g.TaiKhoanThanhToan.TrangThai != "Đóng");

                    // Nếu có chọn tài khoản cụ thể thì lọc thêm
                    if (maTaiKhoan != 0)
                    {
                        query = query.Where(g => g.MaTaiKhoanThanhToan == maTaiKhoan);
                    }

                    // [SỬA ĐỔI] Logic lấy tên danh mục cha
                    var rawData = query.Select(g => new DashboardDto
                    {
                        SoTien = (double)g.SoTien,
                        NgayGiaoDich = g.NgayGiaoDich,
                        MaLoaiGiaoDich = g.MaLoaiGiaoDich ?? 0,

                        // Logic: Nếu có Cha -> Lấy tên Cha. Nếu không có Cha -> Lấy tên chính nó.
                        // (Toán tử ?. và ?? giúp code gọn và tránh lỗi null)
                        TenDanhMuc = (g.DanhMucChiTieu.DanhMucChaNavigation != null)
                                     ? g.DanhMucChiTieu.DanhMucChaNavigation.TenDanhMuc
                                     : (g.DanhMucChiTieu != null ? g.DanhMucChiTieu.TenDanhMuc : "Khác")
                    }).ToList();

                    // 3. Gọi các hàm vẽ biểu đồ
                    UpdatePieChart_CoCauChiTieu(rawData);
                    UpdateLabel_TongChiTieu(rawData);
                    UpdateLineChart_XuHuong(rawData);

                    // Hàm này cần query riêng nên tách ra
                    UpdateColumnChart_ThuChi(fromDate, toDate, maTaiKhoan);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu báo cáo: " + ex.Message);
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
                    Title = $"{item.Name}",
                    Values = new ChartValues<double> { item.Total },
                    DataLabels = true,
                    //LabelPoint = p => string.Format("({1:P})", p.Y.ToString("N0"), p.Participation)
                    LabelPoint = p => $"{p.Participation:P0}"
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
        private void UpdateLabel_TongChiTieu(List<DashboardDto> data)
        {
            // Tính tổng các giao dịch Thu (MaLoai = 1)
            var tongChi = data.Where(x => x.MaLoaiGiaoDich == 2).Sum(x => x.SoTien);
            lblTongChiTieu.Text = $"{tongChi:N0} đ";
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
                    //Title = "Thời gian",
                    Labels = xuHuongData.Select(x => x.Date.ToString("dd/MM")).ToList(),

                    // TÙY CHỈNH MÀU SẮC Ở ĐÂY:
                    Foreground = System.Windows.Media.Brushes.Black, // Màu chữ (Labels)
                    Separator = new Separator
                    {
                        Step = 1,
                        Stroke = System.Windows.Media.Brushes.Black, // Màu đường kẻ dọc
                        StrokeThickness = 1 // Độ dày đường kẻ
                    }
                });


                cartesianChartXuHuong.AxisY.Add(new Axis
                {
                    Foreground = System.Windows.Media.Brushes.Black, // Màu chữ số tiền
                    LabelFormatter = value => value.ToString("N0"),

                    Separator = new Separator
                    {
                        Stroke = System.Windows.Media.Brushes.Black, // Màu đường kẻ ngang
                        StrokeThickness = 0.5 // Mỏng hơn chút cho dễ nhìn
                    }
                });
            }
        }

        // ==================================================================================
        // HÀM 4: CẬP NHẬT BIỂU ĐỒ CỘT (SO SÁNH THU - CHI)
        // ==================================================================================
        private void UpdateColumnChart_ThuChi(DateTime fromDate, DateTime toDate, int maTaiKhoan)
        {
            cartesianChartThuChi.Series.Clear();
            cartesianChartThuChi.AxisX.Clear();
            cartesianChartThuChi.AxisY.Clear();

            int userId = _userContext.MaNguoiDung.Value;

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

                using (var db = _dbFactory.CreateDbContext())
                {
                    var taiKhoans = db.TaiKhoanThanhToans
                                       .Where(t => t.MaNguoiDung == userId
                                               && t.TrangThai != "Đóng")
                                       .ToList();

                    foreach (var tk in taiKhoans)
                    {
                        // Tính tổng Thu (1)
                        var thu = db.GiaoDichs
                            .Where(g => g.MaTaiKhoanThanhToan == tk.MaTaiKhoanThanhToan
                                     && g.MaLoaiGiaoDich == 1
                                     && g.NgayGiaoDich >= fromDate
                                     && g.NgayGiaoDich <= toDate)
                            .Sum(g => (double?)g.SoTien) ?? 0; // Xử lý null

                        // Tính tổng Chi (2)
                        var chi = db.GiaoDichs
                            .Where(g => g.MaTaiKhoanThanhToan == tk.MaTaiKhoanThanhToan
                                     && g.MaLoaiGiaoDich == 2
                                     && g.NgayGiaoDich >= fromDate
                                     && g.NgayGiaoDich <= toDate)
                            .Sum(g => (double?)g.SoTien) ?? 0;

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
                        MaxColumnWidth = 20 // Độ rộng cột vừa phải
                    },
                    new ColumnSeries
                    {
                        Title = "Chi",
                        Values = expenseValues,
                        Fill = System.Windows.Media.Brushes.Red,
                        DataLabels = true,
                        LabelPoint = p => p.Y.ToString("N0"),
                        MaxColumnWidth = 20
                    }
                };

                cartesianChartThuChi.AxisX.Add(new Axis
                {
                    Labels = accountNames,
                    Foreground = System.Windows.Media.Brushes.Black, // Màu chữ tên tài khoản
                    Separator = new Separator
                    {
                        Step = 1,
                        Stroke = System.Windows.Media.Brushes.Transparent // Ẩn đường kẻ dọc (thường biểu đồ cột không cần kẻ dọc)
                                                                          // Hoặc để Brushes.Black nếu bạn muốn hiện
                    }
                });
            }
            // TRƯỜNG HỢP 2: CHỌN 1 TÀI KHOẢN CỤ THỂ (Giữ nguyên logic so sánh tổng quát)
            else
            {
                double totalThu = 0;
                double totalChi = 0;

                using (var db = _dbFactory.CreateDbContext())
                {
                    totalThu = (double)(db.GiaoDichs
                       .Where(g => g.MaTaiKhoanThanhToan == maTaiKhoan && g.MaLoaiGiaoDich == 1
                               && g.NgayGiaoDich >= fromDate && g.NgayGiaoDich <= toDate)
                       .Sum(g => (double?)g.SoTien) ?? 0);

                    totalChi = (double)(db.GiaoDichs
                       .Where(g => g.MaTaiKhoanThanhToan == maTaiKhoan && g.MaLoaiGiaoDich == 2
                               && g.NgayGiaoDich >= fromDate && g.NgayGiaoDich <= toDate)
                       .Sum(g => (double?)g.SoTien) ?? 0);
                }

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
                Foreground = System.Windows.Media.Brushes.Black, // Màu chữ số tiền
                LabelFormatter = value => value.ToString("N0"),
                Separator = new Separator
                {
                    Stroke = System.Windows.Media.Brushes.Black, // Màu đường kẻ ngang (Grid lines)
                    StrokeThickness = 0.5
                }
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

        private void cboTaiKhoan_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra cờ _isLoading để tránh lỗi khi Form vừa mới mở (lúc đang đổ dữ liệu vào combobox)
            if (_isLoading) return;

            // Gọi hàm load dữ liệu chính
            LoadDashboardData();
        }
    }
}



