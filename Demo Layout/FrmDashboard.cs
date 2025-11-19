using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

// Thư viện LiveCharts
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.WinForms;
using System.Windows.Media; // Cần cho Brushes (Màu sắc)

namespace Demo_Layout
{
    public partial class FrmDashboard : Form
    {
        private const int CURRENT_USER_ID = 1; // Giả định User ID

        public FrmDashboard()
        {
            InitializeComponent();

            // Cấu hình mặc định biểu đồ cho đẹp
            cartesianChartXuHuong.BackColor = System.Drawing.Color.White;
            cartesianChartThuChi.BackColor = System.Drawing.Color.White;
            pieChartChiTieu.BackColor = System.Drawing.Color.White;
            pieChartChiTieu.LegendLocation = LegendLocation.Right;
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            // 1. Cài đặt ngày mặc định (Tháng hiện tại)
            DateTime now = DateTime.Now;
            dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
            dtpDenNgay.Value = now;

            // 2. Load ComboBox Tài khoản
            LoadComboBoxTaiKhoan();

            // 3. Tải dữ liệu
            LoadDashboardData();
        }

        private void LoadComboBoxTaiKhoan()
        {
            try
            {
                using (var db = new QLTCCN_DbContext())
                {
                    var listTK = db.TAI_KHOAN_THANH_TOAN
                                   .Where(t => t.MaNguoiDung == CURRENT_USER_ID)
                                   .Select(t => new { t.MaTaiKhoanThanhToan, t.TenTaiKhoan })
                                   .ToList();

                    // Thêm mục "Tất cả"
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

        private void LoadDashboardData()
        {
            DateTime fromDate = dtpTuNgay.Value.Date;
            DateTime toDate = dtpDenNgay.Value.Date;
            int maTaiKhoan = (int)cboTaiKhoan.SelectedValue;

            try
            {
                using (var db = new QLTCCN_DbContext())
                {
                    // --- LẤY DỮ LIỆU GỐC (Base Query) ---
                    var query = db.GIAO_DICH
                        .Include(g => g.DanhMuc)
                        .Where(g => g.MaNguoiDung == CURRENT_USER_ID &&
                                    g.NgayGiaoDich >= fromDate &&
                                    g.NgayGiaoDich <= toDate);

                    // Lọc theo tài khoản (nếu không chọn Tất cả)
                    if (maTaiKhoan != 0)
                    {
                        query = query.Where(g => g.MaTaiKhoanThanhToan == maTaiKhoan);
                    }

                    // Tải dữ liệu về RAM để xử lý (vì EF Core đôi khi hạn chế GroupBy phức tạp)
                    var rawData = query.Select(g => new
                    {
                        g.SoTien,
                        g.NgayGiaoDich,
                        g.MaLoaiGiaoDich,
                        TenDanhMuc = g.DanhMuc.TenDanhMuc
                    }).ToList();

                    // ==========================================================
                    // BẢNG 1: CHI TIÊU THEO DANH MỤC (Pie Chart)
                    // ==========================================================
                    // Lọc Chi (MaLoai = 2) -> Group theo Danh Mục
                    var chiTieuData = rawData
                        .Where(x => x.MaLoaiGiaoDich == 2) // 2 = Chi
                        .GroupBy(x => x.TenDanhMuc)
                        .Select(g => new { Name = g.Key, Total = (double)g.Sum(x => x.SoTien) })
                        .OrderByDescending(x => x.Total)
                        .ToList();

                    // Logic Top 3 + Khác
                    var pieSeries = new SeriesCollection();

                    // Lấy Top 3
                    foreach (var item in chiTieuData.Take(3))
                    {
                        pieSeries.Add(new PieSeries
                        {
                            Title = item.Name,
                            Values = new ChartValues<double> { item.Total },
                            DataLabels = true,
                            LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y.ToString("N0"), chartPoint.Participation)
                        });
                    }

                    // Gom nhóm các mục còn lại
                    var otherTotal = chiTieuData.Skip(3).Sum(x => x.Total);
                    if (otherTotal > 0)
                    {
                        pieSeries.Add(new PieSeries
                        {
                            Title = "Khác",
                            Values = new ChartValues<double> { otherTotal },
                            DataLabels = true,
                            LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y.ToString("N0"), chartPoint.Participation),
                            Fill = System.Windows.Media.Brushes.Gray // Màu xám cho mục Khác
                        });
                    }
                    pieChartChiTieu.Series = pieSeries;


                    // ==========================================================
                    // BẢNG 2: TỔNG THU NHẬP (Label)
                    // ==========================================================
                    var tongThu = rawData.Where(x => x.MaLoaiGiaoDich == 1).Sum(x => x.SoTien); // 1 = Thu
                    lblTongThuNhap.Text = $"{tongThu:N0} đ";


                    // ==========================================================
                    // BẢNG 3: XU HƯỚNG CHI TIÊU (Line Graph)
                    // ==========================================================
                    // Group theo Ngày
                    var xuHuongData = rawData
                        .Where(x => x.MaLoaiGiaoDich == 2) // Chỉ tính Chi
                        .GroupBy(x => x.NgayGiaoDich.Date)
                        .Select(g => new { Date = g.Key, Total = (double)g.Sum(x => x.SoTien) })
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
                            LineSmoothness = 0 // 0 = Đường thẳng, 1 = Đường cong mềm
                        };
                        cartesianChartXuHuong.Series = new SeriesCollection { lineSeries };

                        // Trục X: Ngày tháng
                        cartesianChartXuHuong.AxisX.Add(new Axis
                        {
                            Title = "Thời gian",
                            Labels = xuHuongData.Select(x => x.Date.ToString("dd/MM")).ToList(),
                            Separator = new Separator { Step = 3 } // Độ chia là 3 (cứ 3 ngày hiện 1 nhãn)
                        });

                        // Trục Y: Tiền
                        cartesianChartXuHuong.AxisY.Add(new Axis
                        {
                            LabelFormatter = value => value.ToString("N0")
                        });
                    }


                    // ==========================================================
                    // BẢNG 4: THU VS CHI (Column Chart)
                    // ==========================================================
                    var totalThu = (double)rawData.Where(x => x.MaLoaiGiaoDich == 1).Sum(x => x.SoTien);
                    var totalChi = (double)rawData.Where(x => x.MaLoaiGiaoDich == 2).Sum(x => x.SoTien);

                    cartesianChartThuChi.Series.Clear();
                    cartesianChartThuChi.AxisX.Clear();
                    cartesianChartThuChi.AxisY.Clear();

                    var columnSeries = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Tổng Thu",
                            Values = new ChartValues<double> { totalThu },
                            Fill = System.Windows.Media.Brushes.Green, //                             DataLabels = true,
                            LabelPoint = p => p.Y.ToString("N0")
                        },
                        new ColumnSeries
                        {
                            Title = "Tổng Chi",
                            Values = new ChartValues<double> { totalChi },
                            Fill = System.Windows.Media.Brushes.Red, //                             DataLabels = true,
                            LabelPoint = p => p.Y.ToString("N0")
                        }
                    };

                    cartesianChartThuChi.Series = columnSeries;

                    // Trục X (Ẩn nhãn vì đã có Legend màu sắc)
                    cartesianChartThuChi.AxisX.Add(new Axis
                    {
                        Labels = new[] { "Loại giao dịch" },
                        ShowLabels = false
                    });

                    // Trục Y
                    cartesianChartThuChi.AxisY.Add(new Axis
                    {
                        LabelFormatter = value => value.ToString("N0")
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải Dashboard: " + ex.Message);
            }
        }
    }
}