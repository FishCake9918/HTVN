using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Drawing; // Cần cho Color, Point, Size

// Thư viện Entity Framework
using Microsoft.EntityFrameworkCore;
// Thư viện LiveCharts (Bắt buộc cài NuGet LiveCharts.WinForms)
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.WinForms;

namespace Demo_Layout // !!! ĐỔI NAMESPACE NÀY CHO KHỚP VỚI PROJECT CỦA BẠN !!!
{
    public partial class FrmThemBaoCao : Form
    {
        // ID người dùng giả định
        private const int CURRENT_USER_ID = 1;

        public FrmThemBaoCao()
        {
            InitializeComponent();

            // Cấu hình cơ bản cho biểu đồ khi khởi động
            ConfigChart();
        }

        private void ConfigChart()
        {
            // Đặt màu nền trắng cho sạch sẽ
            cartesianChart1.BackColor = Color.White;

            // Tắt bớt các hiệu ứng zoom/pan nếu không cần thiết (tùy chọn)
            cartesianChart1.Zoom = ZoomingOptions.None;
            cartesianChart1.Pan = PanningOptions.None;
        }

        private void FrmThemBaoCao_Load(object sender, EventArgs e)
        {
            // Cài đặt ngày tháng mặc định (từ đầu tháng đến hiện tại)
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;

            // Load dữ liệu vào các ComboBox
            LoadComboboxes();

            // Vẽ biểu đồ mặc định lần đầu tiên
            btnXemBaoCao_Click(null, null);
        }

        private void LoadComboboxes()
        {
            try
            {
                using (var db = new QLTCCN_DbContext())
                {
                    // 1. Load Loại (Thu/Chi)
                 var loaiGD = db.LOAI_GIAO_DICH.ToList();
                    //Thêm mục "Tất cả" vào đầu danh sách
                   loaiGD.Insert(0, new LOAI_GIAO_DICH { MaLoaiGiaoDich = 0, TenLoaiGiaoDich = "Tất cả" });

                    cboThuChi.DataSource = loaiGD;
                    cboThuChi.DisplayMember = "TenLoaiGiaoDich";
                    cboThuChi.ValueMember = "MaLoaiGiaoDich";
                    cboThuChi.SelectedValue = 2; // Mặc định chọn 'Chi' (ID = 2)

                    // 2. Load Danh Mục
                    var danhMuc = db.DANH_MUC_CHI_TIEU.Where(d => d.MaNguoiDung == CURRENT_USER_ID).ToList();
                    danhMuc.Insert(0, new DANH_MUC_CHI_TIEU { MaDanhMuc = 0, TenDanhMuc = "Tất cả danh mục" });

                    cboDanhMuc.DataSource = danhMuc;
                    cboDanhMuc.DisplayMember = "TenDanhMuc";
                    cboDanhMuc.ValueMember = "MaDanhMuc";

                    // 3. Load Đối Tượng (Nếu có bảng này, uncomment đoạn dưới)
                    
                    var doiTuong = db.DOI_TUONG_GIAO_DICH.Where(d => d.MaNguoiDung == CURRENT_USER_ID).ToList();
                    doiTuong.Insert(0, new DOI_TUONG_GIAO_DICH { MaDoiTuongGiaoDich = 0, TenDoiTuong = "Tất cả đối tượng" });
                    cboDoiTuong.DataSource = doiTuong;
                    cboDoiTuong.DisplayMember = "TenDoiTuong";
                    cboDoiTuong.ValueMember = "MaDoiTuongGiaoDich";
                    
                }
            }
            catch (Exception ex)
            {
                // Bỏ qua lỗi kết nối nếu chưa cấu hình DB chuẩn (để form vẫn hiện lên được)
                // MessageBox.Show("Lỗi tải dữ liệu ComboBox: " + ex.Message);
            }
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fromDate = dtpTuNgay.Value.Date;
                DateTime toDate = dtpDenNgay.Value.Date;

                // Kiểm tra null an toàn trước khi convert
                int maLoai = cboThuChi.SelectedValue != null ? Convert.ToInt32(cboThuChi.SelectedValue) : 0;
                int maDanhMuc = cboDanhMuc.SelectedValue != null ? Convert.ToInt32(cboDanhMuc.SelectedValue) : 0;

                using (var db = new QLTCCN_DbContext())
                {
                    // Tạo truy vấn cơ bản
                    var query = db.GIAO_DICH
                        .Include(g => g.DanhMuc)
                        .Where(g => g.MaNguoiDung == CURRENT_USER_ID && g.NgayGiaoDich >= fromDate && g.NgayGiaoDich <= toDate);

                    // Áp dụng Filter
                    if (maLoai != 0) query = query.Where(g => g.MaLoaiGiaoDich == maLoai);
                    if (maDanhMuc != 0) query = query.Where(g => g.MaDanhMuc == maDanhMuc);

                    // Group by để lấy dữ liệu tổng hợp cho biểu đồ
                    var data = query.GroupBy(g => g.DanhMuc.TenDanhMuc)
                                    .Select(g => new { Name = g.Key, Total = (double)g.Sum(x => x.SoTien) })
                                    .OrderByDescending(x => x.Total)
                                    .ToList();

                    // Nếu không có dữ liệu, xóa biểu đồ
                    if (data.Count == 0)
                    {
                        cartesianChart1.Series.Clear();
                        cartesianChart1.AxisX.Clear();
                        cartesianChart1.AxisY.Clear();
                        return;
                    }

                    // Gọi hàm vẽ biểu đồ với dữ liệu đã lấy được
                    VeBieuDoCot(data.Select(x => x.Name).ToList(), data.Select(x => x.Total).ToList());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi vẽ biểu đồ: " + ex.Message);
            }
        }

        private void VeBieuDoCot(List<string> labels, List<double> values)
        {
            // Reset biểu đồ (quan trọng để không bị chồng lấn dữ liệu cũ)
            cartesianChart1.Series.Clear();
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisY.Clear();

            // Tạo cột dữ liệu (Column Series)
            var columnSeries = new ColumnSeries
            {
                Title = "Số tiền",
                Values = new ChartValues<double>(values),
                DataLabels = true,
                LabelPoint = point => point.Y.ToString("N0") // Định dạng số tiền (ví dụ: 1,000,000)
            };

            cartesianChart1.Series = new SeriesCollection { columnSeries };

            // Cấu hình Trục X (Tên danh mục)
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Danh mục",
                Labels = labels,
                Separator = new Separator { Step = 1 } // Đảm bảo hiện tất cả nhãn
            });

            // Cấu hình Trục Y (Số tiền)
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Giá trị (VND)",
                LabelFormatter = value => value.ToString("N0")
            });
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuuBaoCao_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng lưu báo cáo đang phát triển.");
        }
    }
}