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
using System.IO;      // Nhớ using System.IO

namespace Demo_Layout
{
    public partial class UserControlBaoCao : UserControl
    {
        // ==================================================================================
        // 1. KHAI BÁO BIẾN & TÀI NGUYÊN
        // ==================================================================================
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory; // Factory tạo kết nối CSDL an toàn
        private readonly IServiceProvider _serviceProvider;

        // Biến lưu thông tin người dùng đang đăng nhập
        private readonly CurrentUserContext _userContext;

        // Cờ trạng thái để ngăn sự kiện chạy khi form chưa load xong
        private bool _isLoading = true;

        public UserControlBaoCao(IDbContextFactory<QLTCCNContext> dbFactory, CurrentUserContext userContext)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _userContext = userContext; // Lưu lại context user

            ConfigCharts();

            // Gán sự kiện kiểm tra ngày tháng
            dtpTuNgay.ValueChanged += Dtp_ValueChanged;
            dtpDenNgay.ValueChanged += Dtp_ValueChanged;
        }

        private void ConfigCharts()
        {
            // Cấu hình vị trí chú thích (Legend) cho biểu đồ tròn nằm bên phải
            pieChartChiTieu.LegendLocation = LegendLocation.Right;
        }

        // ==================================================================================
        // 2. SỰ KIỆN LOAD FORM (KHỞI TẠO DỮ LIỆU BAN ĐẦU)
        // ==================================================================================
        private void UserControlBaoCao_Load(object sender, EventArgs e)
        {
            // Thiết lập mặc định: Từ ngày 1 tháng này -> Hiện tại
            DateTime now = DateTime.Now;
            dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
            dtpDenNgay.Value = now;

            // Ghi log hệ thống
            LogHelper.GhiLog(_dbFactory, "Quản lý báo cáo", _userContext.MaNguoiDung);

            // Tải dữ liệu
            LoadComboBoxTaiKhoan();
            LoadDashboardData();

            _isLoading = false; // Đã load xong, mở khóa sự kiện
        }

        // ==================================================================================
        // 3. XỬ LÝ LOGIC THỜI GIAN
        // ==================================================================================
        private void Dtp_ValueChanged(object sender, EventArgs e)
        {
            if (_isLoading) return;

            // Ràng buộc: Ngày bắt đầu không được lớn hơn ngày kết thúc
            if (dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Lỗi thời gian", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Reset lại ngày về mốc an toàn
                _isLoading = true; // Tạm khóa để không bị loop sự kiện
                DateTime now = DateTime.Now;
                dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
                dtpDenNgay.Value = now;
                _isLoading = false;

                return;
            }

            // Nếu ngày hợp lệ thì load lại dữ liệu
            LoadDashboardData();
        }

        // ==================================================================================
        // 4. LOAD DANH SÁCH TÀI KHOẢN VÀO COMBOBOX
        // ==================================================================================
        private void LoadComboBoxTaiKhoan()
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    int userId = _userContext.MaNguoiDung.Value;

                    // Lấy danh sách tài khoản chưa bị đóng
                    var listTK = db.TaiKhoanThanhToans
                                   .Where(t => t.MaNguoiDung == userId
                                            && t.TrangThai != "Đóng")
                                   .Select(t => new { t.MaTaiKhoanThanhToan, t.TenTaiKhoan })
                                   .ToList();

                    // Thêm mục "Tất cả" lên đầu danh sách
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

        // ==================================================================================
        // 5. CHỨC NĂNG XUẤT BÁO CÁO RA EXCEL (MiniExcel)
        // ==================================================================================
        private void btnIn_Click(object sender, EventArgs e)
        {
            // B1. Cấu hình hộp thoại lưu file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel (*.xlsx)|*.xlsx";
            saveFileDialog.FileName = $"ThongKeGiaoDich_{DateTime.Now:ddMMyyyy_HHmmss}.xlsx";

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                // B2. Lấy điều kiện lọc (Giống hệt LoadData)
                int maTaiKhoanLoc = 0;
                if (cboTaiKhoan.SelectedValue != null && int.TryParse(cboTaiKhoan.SelectedValue.ToString(), out int val))
                {
                    maTaiKhoanLoc = val;
                }

                // B3. Truy vấn dữ liệu từ CSDL
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

                    // B4. Chuẩn bị dữ liệu xuất (Chọn cột cần hiển thị trong Excel)
                    var dataToExport = query
                        .OrderByDescending(x => x.NgayGiaoDich)
                        .Select(gd => new
                        {
                            NgayGiaoDich = gd.NgayGiaoDich,
                            TenGiaoDich = gd.TenGiaoDich,
                            SoTien = gd.SoTien,
                            Loai = gd.LoaiGiaoDich != null ? gd.LoaiGiaoDich.TenLoaiGiaoDich : "",
                            DanhMuc = gd.DanhMucChiTieu != null ? gd.DanhMucChiTieu.TenDanhMuc : "Khác",
                            TaiKhoan = gd.TaiKhoanThanhToan != null ? gd.TaiKhoanThanhToan.TenTaiKhoan : "",
                            DoiTuong = gd.DoiTuongGiaoDich != null ? gd.DoiTuongGiaoDich.TenDoiTuong : "",
                            GhiChu = gd.GhiChu
                        })
                        .ToList();

                    if (dataToExport.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu giao dịch nào để xuất.");
                        return;
                    }

                    // B5. Ghi ra file Excel
                    MiniExcel.SaveAs(saveFileDialog.FileName, dataToExport);

                    // B6. Tự động mở file sau khi xuất xong
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
        // 6. HÀM CHÍNH: ĐIỀU PHỐI DỮ LIỆU DASHBOARD
        // ==================================================================================
        private void LoadDashboardData()
        {
            DateTime fromDate = dtpTuNgay.Value.Date;
            DateTime toDate = dtpDenNgay.Value.Date;

            // Fix lỗi lấy ngày: Chỉnh toDate thành cuối ngày 23:59:59
            toDate = toDate.AddDays(1).AddTicks(-1);

            int maTaiKhoan = (int)cboTaiKhoan.SelectedValue;

            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    int userId = _userContext.MaNguoiDung.Value;

                    // B1. Truy vấn bảng GIAO_DICH (Include các bảng liên quan)
                    var query = db.GiaoDichs
                                .Include(g => g.TaiKhoanThanhToan)
                                .Include(g => g.DanhMucChiTieu)
                                    .ThenInclude(dm => dm.DanhMucChaNavigation) // Lấy danh mục cha
                                .Where(g => g.MaNguoiDung == userId &&
                                            g.NgayGiaoDich >= fromDate &&
                                            g.NgayGiaoDich <= toDate);

                    query = query.Where(g => g.TaiKhoanThanhToan.TrangThai != "Đóng");

                    // Nếu có chọn tài khoản cụ thể thì lọc thêm
                    if (maTaiKhoan != 0)
                    {
                        query = query.Where(g => g.MaTaiKhoanThanhToan == maTaiKhoan);
                    }

                    // B2. Chuyển đổi dữ liệu (Projection) để xử lý logic Danh mục Cha/Con
                    var rawData = query.Select(g => new DashboardDto
                    {
                        SoTien = (double)g.SoTien,
                        NgayGiaoDich = g.NgayGiaoDich,
                        MaLoaiGiaoDich = g.MaLoaiGiaoDich ?? 0,
                        // Logic: Ưu tiên lấy tên Cha, nếu không có thì lấy tên chính nó
                        TenDanhMuc = (g.DanhMucChiTieu.DanhMucChaNavigation != null)
                                     ? g.DanhMucChiTieu.DanhMucChaNavigation.TenDanhMuc
                                     : (g.DanhMucChiTieu != null ? g.DanhMucChiTieu.TenDanhMuc : "Khác")
                    }).ToList();

                    // B3. Gọi các hàm vẽ biểu đồ con
                    UpdatePieChart_CoCauChiTieu(rawData);
                    UpdateLabel_TongChiTieu(rawData);
                    UpdateLineChart_XuHuong(rawData);

                    // Hàm này cần query logic riêng nên tách ra
                    UpdateColumnChart_ThuChi(fromDate, toDate, maTaiKhoan);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu báo cáo: " + ex.Message);
            }
        }

        // ==================================================================================
        // 7. CẬP NHẬT BIỂU ĐỒ TRÒN (CƠ CẤU CHI TIÊU)
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

            // Lấy Top 5 danh mục lớn nhất
            foreach (var item in chiTieuData.Take(5))
            {
                pieSeries.Add(new PieSeries
                {
                    Title = $"{item.Name}",
                    Values = new ChartValues<double> { item.Total },
                    DataLabels = true,
                    LabelPoint = p => $"{p.Participation:P0}" // Hiển thị phần trăm
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
        // 8. CẬP NHẬT LABEL TỔNG CHI TIÊU
        // ==================================================================================
        private void UpdateLabel_TongChiTieu(List<DashboardDto> data)
        {
            // Tính tổng các giao dịch Chi (MaLoai = 2)
            var tongChi = data.Where(x => x.MaLoaiGiaoDich == 2).Sum(x => x.SoTien);
            lblTongChiTieu.Text = $"{tongChi:N0} đ";
        }

        // ==================================================================================
        // 9. CẬP NHẬT BIỂU ĐỒ ĐƯỜNG (XU HƯỚNG CHI TIÊU THEO NGÀY)
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

                // Cấu hình trục X (Ngày)
                cartesianChartXuHuong.AxisX.Add(new Axis
                {
                    Labels = xuHuongData.Select(x => x.Date.ToString("dd/MM")).ToList(),
                    Foreground = System.Windows.Media.Brushes.Black,
                    Separator = new Separator
                    {
                        Step = 1,
                        Stroke = System.Windows.Media.Brushes.Black,
                        StrokeThickness = 1
                    }
                });

                // Cấu hình trục Y (Tiền)
                cartesianChartXuHuong.AxisY.Add(new Axis
                {
                    Foreground = System.Windows.Media.Brushes.Black,
                    LabelFormatter = value => value.ToString("N0"),
                    Separator = new Separator
                    {
                        Stroke = System.Windows.Media.Brushes.Black,
                        StrokeThickness = 0.5
                    }
                });
            }
        }

        // ==================================================================================
        // 10. CẬP NHẬT BIỂU ĐỒ CỘT (SO SÁNH THU - CHI)
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

            // TRƯỜNG HỢP 1: CHỌN "TẤT CẢ TÀI KHOẢN" -> VẼ CỘT CHO TỪNG TÀI KHOẢN
            if (maTaiKhoan == 0)
            {
                List<string> accountNames = new List<string>();
                ChartValues<double> incomeValues = new ChartValues<double>(); // Danh sách giá trị Thu
                ChartValues<double> expenseValues = new ChartValues<double>(); // Danh sách giá trị Chi

                using (var db = _dbFactory.CreateDbContext())
                {
                    var taiKhoans = db.TaiKhoanThanhToans
                                     .Where(t => t.MaNguoiDung == userId && t.TrangThai != "Đóng")
                                     .ToList();

                    foreach (var tk in taiKhoans)
                    {
                        // Tính tổng Thu (1) của TK này
                        var thu = db.GiaoDichs
                            .Where(g => g.MaTaiKhoanThanhToan == tk.MaTaiKhoanThanhToan
                                     && g.MaLoaiGiaoDich == 1
                                     && g.NgayGiaoDich >= fromDate
                                     && g.NgayGiaoDich <= toDate)
                            .Sum(g => (double?)g.SoTien) ?? 0;

                        // Tính tổng Chi (2) của TK này
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

                // Tạo 2 Series: Thu (Xanh) - Chi (Đỏ)
                cartesianChartThuChi.Series = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Thu",
                        Values = incomeValues,
                        Fill = System.Windows.Media.Brushes.Green,
                        DataLabels = true,
                        LabelPoint = p => p.Y.ToString("N0"),
                        MaxColumnWidth = 20
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
                    Foreground = System.Windows.Media.Brushes.Black,
                    Separator = new Separator { Stroke = System.Windows.Media.Brushes.Transparent }
                });
            }
            // TRƯỜNG HỢP 2: CHỌN 1 TÀI KHOẢN CỤ THỂ -> SO SÁNH TỔNG QUÁT CỦA TK ĐÓ
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

            // Trục Y chung cho cả 2 trường hợp
            cartesianChartThuChi.AxisY.Add(new Axis
            {
                Foreground = System.Windows.Media.Brushes.Black,
                LabelFormatter = value => value.ToString("N0"),
                Separator = new Separator
                {
                    Stroke = System.Windows.Media.Brushes.Black,
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
            // Kiểm tra cờ _isLoading để tránh lỗi khi Form vừa mới mở
            if (_isLoading) return;

            // Load lại dữ liệu khi chọn tài khoản khác
            LoadDashboardData();
        }
    }
}