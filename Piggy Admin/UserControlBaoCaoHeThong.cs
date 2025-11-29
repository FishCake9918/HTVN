using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Data; // Namespace chứa Context và Models
using Microsoft.EntityFrameworkCore;
using LiveCharts;
using LiveCharts.Wpf; // Cần thiết cho ColumnSeries, PieSeries
using System.Windows.Media; // Dùng cho LiveCharts (Brushes)
using System.Drawing; // Cần cho Bitmap
using System.Drawing.Imaging; // Cần cho ImageFormat
using System.IO; // Cần cho MemoryStream
using MiniExcelLibs; // <--- Thư viện xử lý Excel siêu nhanh

namespace Piggy_Admin
{
    public partial class UserControlBaoCaoHeThong : UserControl
    {
        // ==================================================================================
        // 1. KHAI BÁO BIẾN & CẤU HÌNH BAN ĐẦU
        // ==================================================================================
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;

        // Danh sách các tính năng quan trọng cần theo dõi trên biểu đồ tròn
        private readonly string[] _danhSachTinhNang =
        {
            "Quản lý danh mục chi tiêu", "Quản lý giao dịch", "Quản lý báo cáo",
            "Quản lý đối tượng giao dịch", "Quản lý tài khoản thanh toán", "Quản lý ngân sách"
        };

        public UserControlBaoCaoHeThong(IDbContextFactory<QLTCCNContext> dbFactory)
        {
            InitializeComponent();
            _dbFactory = dbFactory;

            // Thiết lập giao diện biểu đồ ngay khi khởi tạo
            ConfigCharts();
        }

        private void ConfigCharts()
        {
            // Đặt chú thích (Legend) bên phải cho gọn
            chartTinhNang.LegendLocation = LegendLocation.Right;

            // Tắt tooltip mặc định nếu cần (hoặc tùy chỉnh lại)
            chartTanSuatDangNhap.DataTooltip = new DefaultTooltip();
        }

        // ==================================================================================
        // 2. SỰ KIỆN LOAD & ĐIỀU KHIỂN THỜI GIAN
        // ==================================================================================
        private void UserControlBaoCaoHeThong_Load(object sender, EventArgs e)
        {
            // 1. Đổ dữ liệu vào ComboBox chọn thời gian (Hôm nay, Tuần này...)
            KhoiTaoMocThoiGian();

            // 2. Tải toàn bộ dữ liệu báo cáo lần đầu
            LoadAllReports();
        }

        private void KhoiTaoMocThoiGian()
        {
            cboMocThoiGian.Items.Clear();
            cboMocThoiGian.Items.Add("Hôm nay");
            cboMocThoiGian.Items.Add("7 ngày qua");
            cboMocThoiGian.Items.Add("Tuần này"); // T2 -> CN
            cboMocThoiGian.Items.Add("30 ngày qua");
            cboMocThoiGian.Items.Add("Tháng này");

            // Mặc định chọn "Hôm nay" (Index = 0)
            cboMocThoiGian.SelectedIndex = 0;
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadAllReports();
        }

        private void cboMocThoiGian_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAllReports();
        }

        // --- HÀM CORE: TÍNH TOÁN NGÀY BẮT ĐẦU & KẾT THÚC ---
        private void LayKhoangThoiGian(out DateTime tuNgay, out DateTime denNgay)
        {
            DateTime now = DateTime.Now;
            string luaChon = cboMocThoiGian.SelectedItem?.ToString();

            switch (luaChon)
            {
                case "Hôm nay":
                    tuNgay = now;
                    denNgay = now;
                    break;

                case "7 ngày qua":
                    tuNgay = now.AddDays(-6); // Lùi lại 6 ngày + hôm nay = 7 ngày
                    denNgay = now;
                    break;

                case "Tuần này":
                    // Tìm ngày Thứ 2 của tuần hiện tại
                    int delta = DayOfWeek.Monday - now.DayOfWeek;
                    if (delta > 0) delta -= 7; // Fix lỗi nếu hôm nay là Chủ Nhật

                    tuNgay = now.AddDays(delta); // Thứ 2
                    denNgay = tuNgay.AddDays(6); // Chủ Nhật
                    break;

                case "30 ngày qua":
                    tuNgay = now.AddDays(-29);
                    denNgay = now;
                    break;

                case "Tháng này":
                    // Ngày 1 đầu tháng
                    tuNgay = new DateTime(now.Year, now.Month, 1);
                    // Ngày cuối tháng = Ngày 1 tháng sau trừ đi 1 ngày
                    denNgay = tuNgay.AddMonths(1).AddDays(-1);
                    break;

                default: // Mặc định lấy tháng này
                    tuNgay = new DateTime(now.Year, now.Month, 1);
                    denNgay = now;
                    break;
            }
        }

        // ==================================================================================
        // 3. HÀM ĐIỀU PHỐI DỮ LIỆU (LoadAllReports)
        // ==================================================================================
        private void LoadAllReports()
        {
            DateTime fromDate, toDate;
            LayKhoangThoiGian(out fromDate, out toDate);

            // [QUAN TRỌNG] Chốt thời gian:
            // fromDate: 00:00:00
            // toDate: 23:59:59 (Để không bị sót dữ liệu của ngày cuối cùng)
            toDate = toDate.Date.AddDays(1).AddTicks(-1);
            fromDate = fromDate.Date;

            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    // 1. Tải dữ liệu thô (Raw Data) từ DB về RAM một lần duy nhất
                    // Giúp giảm số lần kết nối DB khi vẽ nhiều biểu đồ
                    var logs = db.NhatKyHoatDongs
                        .Where(x => x.ThoiGian >= fromDate && x.ThoiGian <= toDate)
                        .Select(x => new { x.HanhDong, x.ThoiGian, x.MaNguoiDung }) // Chỉ lấy cột cần thiết
                        .ToList();

                    // 2. Phân phối dữ liệu cho các hàm xử lý hiển thị
                    VeBieuDoTanSuatDangNhap(logs);       // Biểu đồ cột
                    VeBieuDoTinhNang(logs);              // Biểu đồ tròn
                    HienThiSoNguoiDungTruyCap(db);       // Chỉ số DAU (Truy vấn riêng vì cần Distinct)
                    HienThiThoiGianSuDungTB(logs);       // Chỉ số KPI thời gian

                    // 3. Cập nhật lại giao diện biểu đồ (Force Update)
                    chartTanSuatDangNhap.Update(true, true);
                    chartTinhNang.Update(true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải báo cáo hệ thống: " + ex.Message);
            }
        }

        // ==================================================================================
        // 4. CÁC HÀM VẼ BIỂU ĐỒ & TÍNH KPI
        // ==================================================================================

        // --- Biểu đồ Cột: Tần suất đăng nhập theo ngày ---
        private void VeBieuDoTanSuatDangNhap(dynamic logs)
        {
            var dataLogs = (IEnumerable<dynamic>)logs;

            // Group by Ngày -> Đếm số lượt
            var loginData = dataLogs
                .Where(x => x.HanhDong == "Đăng nhập")
                .GroupBy(x => ((DateTime)x.ThoiGian).Date)
                .Select(g => new { Ngay = g.Key, SoLuot = g.Count() })
                .OrderBy(x => x.Ngay)
                .ToList();

            chartTanSuatDangNhap.Series.Clear();
            chartTanSuatDangNhap.AxisX.Clear();
            chartTanSuatDangNhap.AxisY.Clear();

            // Tạo cột
            var columnSeries = new ColumnSeries
            {
                Title = "Lượt đăng nhập",
                Values = new ChartValues<int>(loginData.Select(x => x.SoLuot)),
                DataLabels = true
            };

            chartTanSuatDangNhap.Series = new SeriesCollection { columnSeries };

            // Trục X hiển thị ngày tháng
            chartTanSuatDangNhap.AxisX.Add(new Axis
            {
                Title = "Ngày",
                Labels = loginData.Select(x => ((DateTime)x.Ngay).ToString("dd/MM")).ToList(),
                Separator = new Separator { Step = 1 }
            });

            chartTanSuatDangNhap.AxisY.Add(new Axis
            {
                LabelFormatter = value => value.ToString("N0")
            });
        }

        // --- Biểu đồ Tròn: Tỷ lệ sử dụng tính năng ---
        private void VeBieuDoTinhNang(dynamic logs)
        {
            var dataLogs = (IEnumerable<dynamic>)logs;

            // Lọc các hành động nằm trong danh sách tính năng quan tâm -> Group by Tên -> Đếm
            var featureData = dataLogs
                .Where(x => _danhSachTinhNang.Contains((string)x.HanhDong))
                .GroupBy(x => (string)x.HanhDong)
                .Select(g => new { TenTinhNang = g.Key, SoLan = g.Count() })
                .OrderByDescending(x => x.SoLan)
                .ToList();

            var pieSeries = new SeriesCollection();

            foreach (var item in featureData)
            {
                pieSeries.Add(new PieSeries
                {
                    Title = item.TenTinhNang,
                    Values = new ChartValues<int> { item.SoLan },
                    DataLabels = true,
                    // Format hiển thị: Giá trị (Phần trăm)
                    LabelPoint = chartPoint => string.Format("{1:P}", chartPoint.Y, chartPoint.Participation)
                });
            }

            chartTinhNang.Series = pieSeries;
        }

        // --- Chỉ số: Số người dùng duy nhất truy cập trong ngày (DAU) ---
        private void HienThiSoNguoiDungTruyCap(QLTCCNContext db)
        {
            DateTime today = DateTime.Now.Date;
            // Đếm số User ID khác nhau đã đăng nhập hôm nay
            int uniqueUsers = db.NhatKyHoatDongs
                .Where(x => x.HanhDong == "Đăng nhập" && x.ThoiGian.Date == today)
                .Select(x => x.MaNguoiDung)
                .Distinct()
                .Count();

            lblDAU.Text = uniqueUsers.ToString();
        }

        // --- Chỉ số: Thời gian sử dụng trung bình (Session Duration) ---
        private void HienThiThoiGianSuDungTB(dynamic logs)
        {
            var dataLogs = (IEnumerable<dynamic>)logs;

            // Lấy danh sách Log In/Out và sắp xếp theo User và Thời gian
            var sessions = dataLogs
                .Where(x => x.HanhDong == "Đăng nhập" || x.HanhDong == "Đăng xuất")
                .OrderBy(x => x.MaNguoiDung).ThenBy(x => x.ThoiGian)
                .ToList();

            double tongPhut = 0;
            int soPhien = 0;

            // Thuật toán: Duyệt tuần tự, nếu gặp cặp (Login -> Logout) của cùng 1 user thì tính thời gian
            for (int i = 0; i < sessions.Count - 1; i++)
            {
                var hienTai = sessions[i];
                var keTiep = sessions[i + 1];

                if (hienTai.MaNguoiDung == keTiep.MaNguoiDung &&
                    hienTai.HanhDong == "Đăng nhập" &&
                    keTiep.HanhDong == "Đăng xuất")
                {
                    TimeSpan duration = ((DateTime)keTiep.ThoiGian) - ((DateTime)hienTai.ThoiGian);

                    // Lọc rác: Chỉ tính các phiên > 10s và < 24h (tránh lỗi treo máy)
                    if (duration.TotalSeconds > 10 && duration.TotalHours < 24)
                    {
                        tongPhut += duration.TotalMinutes;
                        soPhien++;
                    }
                }
            }

            double trungBinh = (soPhien > 0) ? (tongPhut / soPhien) : 0;
            lblThoiGianTrungBinh.Text = $"{Math.Round(trungBinh, 1)} phút";
        }

        // ==================================================================================
        // 5. CHỨC NĂNG XUẤT EXCEL (Sử dụng MiniExcel)
        // ==================================================================================
        private void btnInLog_Click(object sender, EventArgs e)
        {
            // 1. Lấy lại khoảng thời gian cần xuất
            DateTime fromDate, toDate;
            LayKhoangThoiGian(out fromDate, out toDate);
            toDate = toDate.Date.AddDays(1).AddTicks(-1);
            fromDate = fromDate.Date;

            // 2. Cấu hình hộp thoại lưu file
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel Files|*.xlsx";
            saveDialog.FileName = $"NhatKy_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            saveDialog.Title = "Chọn nơi lưu báo cáo Excel";

            if (saveDialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    // 3. Truy vấn dữ liệu cần xuất
                    var logs = db.NhatKyHoatDongs
                        .Where(x => x.ThoiGian >= fromDate && x.ThoiGian <= toDate)
                        .Include(x => x.NguoiDung) // Join bảng User để lấy tên
                        .OrderByDescending(x => x.ThoiGian)
                        .ToList();

                    if (logs.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu để xuất Excel.");
                        return;
                    }

                    // 4. CHUẨN BỊ DỮ LIỆU (Projection)
                    // MiniExcel sẽ dùng tên thuộc tính của Anonymous Object này làm Header cột Excel
                    var dataToExport = logs.Select((x, index) => new
                    {
                        STT = index + 1,
                        ThoiGian = x.ThoiGian.ToString("dd/MM/yyyy HH:mm:ss"),
                        NguoiDung = x.NguoiDung != null ? x.NguoiDung.HoTen : "Hệ thống/Đã xóa",
                        HanhDong = x.HanhDong
                        // Bạn có thể thêm cột MoTa nếu cần
                    });

                    // 5. GHI FILE (Chỉ 1 dòng lệnh)
                    MiniExcel.SaveAs(saveDialog.FileName, dataToExport);

                    // 6. Mở file ngay sau khi xuất
                    var p = new System.Diagnostics.Process();
                    p.StartInfo = new System.Diagnostics.ProcessStartInfo(saveDialog.FileName) { UseShellExecute = true };
                    p.Start();

                    MessageBox.Show("Xuất Excel thành công!", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message);
            }
        }
    }
}