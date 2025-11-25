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
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Drawing; // Cần cho Bitmap
using System.Drawing.Imaging; // Cần cho ImageFormat
using System.IO; // Cần cho MemoryStream
using MiniExcelLibs; // <--- NHỚ THÊM DÒNG NÀY

namespace Piggy_Admin
{
    public partial class UserControlBaoCaoHeThong : UserControl
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;

        // Các hành động được coi là "Tính năng" để vẽ biểu đồ tròn
        private readonly string[] _danhSachTinhNang =
        {
            "Quản lý danh mục chi tiêu", "Quản lý giao dịch", "Quản lý báo cáo",
            "Quản lý đối tượng giao dịch", "Quản lý tài khoản thanh toán", "Quản lý ngân sách"
        };

        public UserControlBaoCaoHeThong(IDbContextFactory<QLTCCNContext> dbFactory)
        {
            InitializeComponent();
            _dbFactory = dbFactory;

            // Cấu hình mặc định cho biểu đồ
            ConfigCharts();
        }

        private void ConfigCharts()
        {
            // Legend bên phải cho đẹp
            chartTinhNang.LegendLocation = LegendLocation.Right;

            // Tắt hiệu ứng zoom để tránh rối khi ít dữ liệu
            chartTanSuatDangNhap.DataTooltip = new DefaultTooltip();
        }

        private void UserControlBaoCaoHeThong_Load(object sender, EventArgs e)
        {
            // Mặc định lấy dữ liệu 7 ngày qua
            KhoiTaoMocThoiGian();

            LoadAllReports();
        }

        private void KhoiTaoMocThoiGian()
        {
            // Thêm các mục vào ComboBox
            cboMocThoiGian.Items.Clear();
            cboMocThoiGian.Items.Add("Hôm nay");
            cboMocThoiGian.Items.Add("7 ngày qua");
            cboMocThoiGian.Items.Add("Tuần này"); // T2 -> CN
            cboMocThoiGian.Items.Add("30 ngày qua");
            cboMocThoiGian.Items.Add("Tháng này");

            // Mặc định chọn "Tháng này" (Index = 4)
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

        // --- HÀM LOGIC TÍNH NGÀY (CORE) ---
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
                    tuNgay = now.AddDays(-6); // Tính cả hôm nay là 7 ngày
                    denNgay = now;
                    break;

                case "Tuần này":
                    // Tính ngày Thứ 2 đầu tuần
                    int delta = DayOfWeek.Monday - now.DayOfWeek;
                    if (delta > 0) delta -= 7; // Nếu hôm nay là CN, delta sẽ sai nên cần trừ 7

                    tuNgay = now.AddDays(delta); // Thứ 2 tuần này
                    denNgay = tuNgay.AddDays(6); // Chủ nhật tuần này
                    break;

                case "30 ngày qua":
                    tuNgay = now.AddDays(-29);
                    denNgay = now;
                    break;

                case "Tháng này":
                    // Ngày 1 của tháng hiện tại
                    tuNgay = new DateTime(now.Year, now.Month, 1);
                    // Ngày cuối của tháng hiện tại
                    denNgay = tuNgay.AddMonths(1).AddDays(-1);
                    break;

                default: // Mặc định lấy tháng này
                    tuNgay = new DateTime(now.Year, now.Month, 1);
                    denNgay = now;
                    break;
            }
        }

        private void LoadAllReports()
        {
            DateTime fromDate, toDate;
            LayKhoangThoiGian(out fromDate, out toDate);

            // Xử lý lấy cuối ngày cho toDate (23:59:59)
            toDate = toDate.Date.AddDays(1).AddTicks(-1);
            fromDate = fromDate.Date; // Reset về 00:00:00

            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    // Lấy dữ liệu thô 1 lần để xử lý
                    var logs = db.NhatKyHoatDongs
                        .Where(x => x.ThoiGian >= fromDate && x.ThoiGian <= toDate)
                        .Select(x => new { x.HanhDong, x.ThoiGian, x.MaNguoiDung })
                        .ToList();

                    // GỌI 4 HÀM XỬ LÝ 4 YÊU CẦU
                    VeBieuDoTanSuatDangNhap(logs);
                    VeBieuDoTinhNang(logs);
                    HienThiSoNguoiDungTruyCap(db);
                    HienThiThoiGianSuDungTB(logs);

                    chartTanSuatDangNhap.Update(true, true);
                    chartTinhNang.Update(true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải báo cáo hệ thống: " + ex.Message);
            }
        }

        // ========================================================================
        // CÁC HÀM VẼ BIỂU ĐỒ (GIỮ NGUYÊN)
        // ========================================================================
        private void VeBieuDoTanSuatDangNhap(dynamic logs)
        {
            var dataLogs = (IEnumerable<dynamic>)logs;

            var loginData = dataLogs
                .Where(x => x.HanhDong == "Đăng nhập")
                .GroupBy(x => ((DateTime)x.ThoiGian).Date)
                .Select(g => new { Ngay = g.Key, SoLuot = g.Count() })
                .OrderBy(x => x.Ngay)
                .ToList();

            chartTanSuatDangNhap.Series.Clear();
            chartTanSuatDangNhap.AxisX.Clear();
            chartTanSuatDangNhap.AxisY.Clear();

            var columnSeries = new ColumnSeries
            {
                Title = "Lượt đăng nhập",
                Values = new ChartValues<int>(loginData.Select(x => x.SoLuot)),
                DataLabels = true
            };

            chartTanSuatDangNhap.Series = new SeriesCollection { columnSeries };

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

        private void VeBieuDoTinhNang(dynamic logs)
        {
            var dataLogs = (IEnumerable<dynamic>)logs;

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
                    LabelPoint = chartPoint => string.Format("{1:P}", chartPoint.Y, chartPoint.Participation)
                });
            }

            chartTinhNang.Series = pieSeries;
        }

        private void HienThiSoNguoiDungTruyCap(QLTCCNContext db)
        {
            DateTime today = DateTime.Now.Date;
            int uniqueUsers = db.NhatKyHoatDongs
                .Where(x => x.HanhDong == "Đăng nhập" && x.ThoiGian.Date == today)
                .Select(x => x.MaNguoiDung)
                .Distinct()
                .Count();

            lblDAU.Text = uniqueUsers.ToString();
        }

        private void HienThiThoiGianSuDungTB(dynamic logs)
        {
            var dataLogs = (IEnumerable<dynamic>)logs;
            var sessions = dataLogs
                .Where(x => x.HanhDong == "Đăng nhập" || x.HanhDong == "Đăng xuất")
                .OrderBy(x => x.MaNguoiDung).ThenBy(x => x.ThoiGian)
                .ToList();

            double tongPhut = 0;
            int soPhien = 0;

            for (int i = 0; i < sessions.Count - 1; i++)
            {
                var hienTai = sessions[i];
                var keTiep = sessions[i + 1];

                if (hienTai.MaNguoiDung == keTiep.MaNguoiDung &&
                    hienTai.HanhDong == "Đăng nhập" &&
                    keTiep.HanhDong == "Đăng xuất")
                {
                    TimeSpan duration = ((DateTime)keTiep.ThoiGian) - ((DateTime)hienTai.ThoiGian);
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

        // ========================================================================
        // TÍNH NĂNG 1: IN NGUYÊN DASHBOARD (CHỤP ẢNH DÁN VÀO PDF)
        // ========================================================================
        private void btnInDashboard_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Chụp ảnh UserControl
                Bitmap bmp = new Bitmap(this.Width, this.Height);
                this.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, this.Width, this.Height));

                // 2. [SỬA LỖI] Sử dụng System.Drawing.Imaging.ImageFormat rõ ràng
                byte[] imageData;
                using (var stream = new MemoryStream())
                {
                    bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    imageData = stream.ToArray();
                }

                // 3. Tạo PDF
                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4.Landscape());
                        page.Margin(1, Unit.Centimetre);

                        page.Content()
                            .Column(col =>
                            {
                                // [SỬA LỖI] Sử dụng QuestPDF.Helpers.Colors rõ ràng
                                col.Item().Text("BÁO CÁO TỔNG QUAN HỆ THỐNG")
                                   .FontSize(20).Bold().AlignCenter().FontColor(QuestPDF.Helpers.Colors.Blue.Medium);

                                col.Item().Text($"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}")
                                   .FontSize(10).AlignCenter();

                                // [SỬA LỖI] Colors.Grey.Light
                                col.Item().PaddingVertical(10).LineHorizontal(1).LineColor(QuestPDF.Helpers.Colors.Grey.Lighten1);

                                col.Item().Image(imageData, ImageScaling.FitArea);
                            });
                    });
                });

                string fileName = $"Dashboard_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                document.GeneratePdf(fileName);

                var p = new System.Diagnostics.Process();
                p.StartInfo = new System.Diagnostics.ProcessStartInfo(fileName) { UseShellExecute = true };
                p.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất PDF: " + ex.Message);
            }
        }

        // ========================================================================
        // TÍNH NĂNG 2: IN NHẬT KÝ HOẠT ĐỘNG
        // ========================================================================
        private void btnInLog_Click(object sender, EventArgs e)
        {
            //// 1. Lấy mốc thời gian từ ComboBox (giống hệt Dashboard)
            //DateTime fromDate, toDate;
            //LayKhoangThoiGian(out fromDate, out toDate);

            //// 2. [QUAN TRỌNG] Phải mở rộng toDate đến hết ngày (23:59:59)
            //// Nếu không làm bước này, SQL sẽ hiểu toDate là 00:00:00 sáng nay -> Mất hết dữ liệu trong ngày
            //toDate = toDate.Date.AddDays(1).AddTicks(-1);
            //fromDate = fromDate.Date; // Reset về 0h sáng cho chắc

            //using (var db = _dbFactory.CreateDbContext())
            //{
            //    // 3. Query dữ liệu
            //    var logs = db.NhatKyHoatDongs
            //        .Where(x => x.ThoiGian >= fromDate && x.ThoiGian <= toDate)
            //        .Include(x => x.NguoiDung)
            //        .OrderByDescending(x => x.ThoiGian)
            //        .ToList();

            //    if (logs.Count == 0)
            //    {
            //        MessageBox.Show($"Không tìm thấy dữ liệu từ {fromDate:dd/MM} đến {toDate:dd/MM} để in.", "Thông báo");
            //        return;
            //    }

            //    XuatPdfNhatKy(logs);
            //}

            // 1. Lấy khoảng thời gian (Logic cũ giữ nguyên)
            DateTime fromDate, toDate;
            LayKhoangThoiGian(out fromDate, out toDate);
            toDate = toDate.Date.AddDays(1).AddTicks(-1); // Lấy đến 23:59:59
            fromDate = fromDate.Date;

            // 2. Mở hộp thoại cho người dùng chọn nơi lưu file Excel
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel Files|*.xlsx";
            saveDialog.FileName = $"NhatKy_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"; // Tên file mặc định
            saveDialog.Title = "Chọn nơi lưu báo cáo Excel";

            if (saveDialog.ShowDialog() != DialogResult.OK)
            {
                return; // Người dùng bấm Hủy thì thoát
            }

            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    // 3. Lấy dữ liệu từ DB
                    var logs = db.NhatKyHoatDongs
                        .Where(x => x.ThoiGian >= fromDate && x.ThoiGian <= toDate)
                        .Include(x => x.NguoiDung)
                        .OrderByDescending(x => x.ThoiGian)
                        .ToList();

                    if (logs.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu để xuất Excel.");
                        return;
                    }

                    // 4. CHUẨN BỊ DỮ LIỆU XUẤT EXCEL (Quan trọng)
                    // MiniExcel sẽ lấy tên thuộc tính ở đây làm Tiêu đề cột trong Excel
                    var dataToExport = logs.Select((x, index) => new
                    {
                        STT = index + 1,
                        ThoiGian = x.ThoiGian.ToString("dd/MM/yyyy HH:mm:ss"),
                        NguoiDung = x.NguoiDung != null ? x.NguoiDung.HoTen : "Hệ thống/Đã xóa",
                        HanhDong = x.HanhDong
                        
                    });

                    // 5. GHI FILE EXCEL (Chỉ 1 dòng code duy nhất!)
                    MiniExcel.SaveAs(saveDialog.FileName, dataToExport);

                    // 6. Mở file lên cho user xem
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

        // Lưu ý: Đổi tham số từ 'dynamic' sang 'List<NhatKyHoatDong>'
        private void XuatPdfNhatKy(List<NhatKyHoatDong> logs)
        {
            try
            {
                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.PageColor(QuestPDF.Helpers.Colors.White);

                        // Cài đặt Font chữ mặc định
                        page.DefaultTextStyle(x => x.FontSize(10));

                        // --- HEADER ---
                        page.Header().Row(row =>
                        {
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().Text("BÁO CÁO NHẬT KÝ HOẠT ĐỘNG")
                                   .FontSize(16).Bold().FontColor(QuestPDF.Helpers.Colors.Blue.Medium);
                                col.Item().Text($"Thời gian xuất: {DateTime.Now:dd/MM/yyyy HH:mm}");
                            });
                        });

                        // --- CONTENT ---
                        page.Content().PaddingVertical(10).Table(table =>
                        {
                            // Định nghĩa cột
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(30);  // STT
                                columns.ConstantColumn(100); // Thời gian
                                columns.ConstantColumn(100); // Người dùng
                                columns.ConstantColumn(120); // Hành động
                                columns.RelativeColumn();    // Chi tiết
                            });

                            // Tiêu đề bảng
                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("#");
                                header.Cell().Element(CellStyle).Text("Thời gian");
                                header.Cell().Element(CellStyle).Text("Người dùng");
                                header.Cell().Element(CellStyle).Text("Hành động");
                                header.Cell().Element(CellStyle).Text("Chi tiết");

                                static IContainer CellStyle(IContainer container)
                                {
                                    return container.DefaultTextStyle(x => x.SemiBold())
                                                    .PaddingVertical(5)
                                                    .BorderBottom(1)
                                                    .BorderColor(QuestPDF.Helpers.Colors.Black);
                                }
                            });

                            // Dữ liệu dòng
                            int stt = 1;
                            foreach (var item in logs)
                            {
                                // 1. Số thứ tự
                                table.Cell().Element(BlockStyle).Text(stt++.ToString());

                                // 2. Thời gian (Convert sang String)
                                table.Cell().Element(BlockStyle).Text(item.ThoiGian.ToString("dd/MM/yyyy HH:mm"));

                                // 3. Người dùng (Kiểm tra null an toàn)
                                string tenUser = item.NguoiDung != null ? item.NguoiDung.HoTen : "Hệ thống/Đã xóa";
                                table.Cell().Element(BlockStyle).Text(tenUser);

                                // 4. Hành động (Đảm bảo không null)
                                table.Cell().Element(BlockStyle).Text(item.HanhDong ?? "");

                                // 5. Mô tả (Đảm bảo không null)
                                table.Cell().Element(BlockStyle).Text(item.MoTa ?? "");

                                // Style kẻ dòng mờ
                                static IContainer BlockStyle(IContainer container)
                                {
                                    return container.BorderBottom(1)
                                                    // [ĐÃ SỬA LỖI MÀU SẮC TẠI ĐÂY]
                                                    .BorderColor(QuestPDF.Helpers.Colors.Grey.Lighten2)
                                                    .PaddingVertical(5);
                                }
                            }
                        });

                        // --- FOOTER ---
                        page.Footer().AlignCenter().Text(x =>
                        {
                            x.Span("Trang ");
                            x.CurrentPageNumber();
                            x.Span(" / ");
                            x.TotalPages();
                        });
                    });
                });

                // Xuất file và mở
                string fileName = $"LogBook_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                document.GeneratePdf(fileName);

                var p = new System.Diagnostics.Process();
                p.StartInfo = new System.Diagnostics.ProcessStartInfo(fileName) { UseShellExecute = true };
                p.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}