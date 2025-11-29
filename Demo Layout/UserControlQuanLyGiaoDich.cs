using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection; 
using ReaLTaiizor.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Demo_Layout
{
    public partial class UserControlQuanLyGiaoDich : UserControl
    {
        // --- DI SERVICES ---
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider; 
        private readonly CurrentUserContext _userContext; 

        // Biến toàn cục
        private DataTable dtGiaoDich; 
        private bool isPlaceholderActive = true; 

        // --- CONSTRUCTOR NHẬN DI ---
        public UserControlQuanLyGiaoDich(
            IDbContextFactory<QLTCCNContext> dbFactory,
            IServiceProvider serviceProvider,
            CurrentUserContext userContext) 
        {
            InitializeComponent();

            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
            _userContext = userContext; 
            Dinhdangluoi.DinhDangLuoiNguoiDung(kryptonDataGridView1); 

            // Đăng ký các sự kiện
            this.Load += UserControlQuanLyGiaoDich_Load;
            cbTaiKhoan.SelectedIndexChanged += cbTaiKhoan_SelectedIndexChanged;

            // ⭐ 1. THÊM SỰ KIỆN DOUBLE CLICK: Dùng để Sửa nhanh giao dịch ⭐
            kryptonDataGridView1.DoubleClick += poisonDataGridView1_DoubleClick;

            txtTimKiem.Enter += txtTimKiem_Enter;
            txtTimKiem.Leave += txtTimKiem_Leave;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
        }

        private void UserControlQuanLyGiaoDich_Load(object sender, EventArgs e)
        {
            // Cấu hình DataGridView
            kryptonDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            kryptonDataGridView1.MultiSelect = false;
            kryptonDataGridView1.ReadOnly = true;

            LogHelper.GhiLog(_dbFactory, "Quản lý giao dịch", _userContext.MaNguoiDung);

            LoadComboBoxTaiKhoan(); 
            LoadData(); 
        }
        private void poisonDataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (kryptonDataGridView1.SelectedRows.Count > 0)
            {
                btnSua_Click(sender, e); // Gọi lại logic chỉnh sửa
            }
        }
        // --- 1. LOAD DANH SÁCH TÀI KHOẢN ---
        private void LoadComboBoxTaiKhoan()
        {
            try
            {
                using (var context = _dbFactory.CreateDbContext())
                {
                    // Lọc Tài khoản theo User đang đăng nhập và trạng thái "Đang hoạt động"
                    var listTK = context.TaiKhoanThanhToans
                                             .Where(t => t.MaNguoiDung == _userContext.MaNguoiDung && t.TrangThai == "Đang hoạt động")
                                             .Select(t => new { t.MaTaiKhoanThanhToan, t.TenTaiKhoan })
                                             .ToList();

                    // Thêm tùy chọn "Tất cả tài khoản"
                    listTK.Insert(0, new { MaTaiKhoanThanhToan = 0, TenTaiKhoan = "--- Tất cả tài khoản ---" });

                    cbTaiKhoan.SelectedIndexChanged -= cbTaiKhoan_SelectedIndexChanged; 

                    cbTaiKhoan.DataSource = listTK;
                    cbTaiKhoan.DisplayMember = "TenTaiKhoan";
                    cbTaiKhoan.ValueMember = "MaTaiKhoanThanhToan";
                    cbTaiKhoan.SelectedIndex = 0; // Chọn "Tất cả"

                    cbTaiKhoan.SelectedIndexChanged += cbTaiKhoan_SelectedIndexChanged;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách tài khoản: " + ex.Message);
            }
        }

        private void cbTaiKhoan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(); // Tải lại dữ liệu khi đổi Tài khoản
        }

        // --- 2. LOAD DỮ LIỆU CHÍNH & TÍNH TỔNG ---
        private void LoadData()
        {
            try
            {
                int maTaiKhoanLoc = 0;
                if (cbTaiKhoan.SelectedValue != null && int.TryParse(cbTaiKhoan.SelectedValue.ToString(), out int val))
                {
                    maTaiKhoanLoc = val;
                }

                using (var context = _dbFactory.CreateDbContext())
                {
                    // Truy vấn chính: Lấy Giao dịch của User hiện tại
                    var query = context.GiaoDichs
                        .Include(g => g.LoaiGiaoDich) // Include các bảng liên quan
                        .Include(g => g.DoiTuongGiaoDich)
                        .Include(g => g.TaiKhoanThanhToan)
                        .Include(g => g.DanhMucChiTieu)
                        .Where(g => g.MaNguoiDung == _userContext.MaNguoiDung);
                    // Lọc theo Tài khoản (nếu không phải "Tất cả")
                    if (maTaiKhoanLoc > 0)
                    {
                        query = query.Where(g => g.MaTaiKhoanThanhToan == maTaiKhoanLoc);
                    }
                    var dataList = query.Select(gd => new
                    {
                        gd.MaGiaoDich,
                        gd.MaDoiTuongGiaoDich,
                        gd.MaTaiKhoanThanhToan,
                        gd.MaDanhMuc,
                        gd.MaLoaiGiaoDich,
                        TenGiaoDich = gd.TenGiaoDich,
                        TenDoiTuong = gd.DoiTuongGiaoDich != null ? gd.DoiTuongGiaoDich.TenDoiTuong : "",
                        TenTaiKhoan = gd.TaiKhoanThanhToan != null ? gd.TaiKhoanThanhToan.TenTaiKhoan : "",
                        DanhMucChiTieu = gd.DanhMucChiTieu != null ? gd.DanhMucChiTieu.TenDanhMuc : "",
                        gd.SoTien,
                        gd.NgayGiaoDich,
                        gd.GhiChu,
                        TenLoaiGiaoDich = gd.LoaiGiaoDich != null ? gd.LoaiGiaoDich.TenLoaiGiaoDich : ""
                    })
                    .OrderByDescending(x => x.NgayGiaoDich)
                    .ToList();

                    dtGiaoDich = ConvertToDataTable(dataList); 
                    kryptonDataGridView1.DataSource = dtGiaoDich; 

                    FormatGrid(); 
                    CalculateTotal(query); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // --- 3. LOGIC TÍNH TỔNG THU & TỔNG CHI ---
        private void CalculateTotal(IQueryable<GiaoDich> filteredTransactions)
        {
            // Tính tổng Thu (MaLoaiGiaoDich == 1)
            decimal tongThu = filteredTransactions.Where(g => g.MaLoaiGiaoDich == 1).Sum(g => g.SoTien);
            // Tính tổng Chi (MaLoaiGiaoDich == 2)
            decimal tongChi = filteredTransactions.Where(g => g.MaLoaiGiaoDich == 2).Sum(g => g.SoTien);

            lblTongThuChi.Text = string.Format("💰 Tổng thu: {0:N0} đ | 💸 Tổng chi: {1:N0} đ", tongThu, tongChi);
            lblTongThuChi.ForeColor = Color.DarkSlateGray;
        }

        private void FormatGrid()
        {
            string[] hiddenColumns = { "MaGiaoDich", "MaDoiTuongGiaoDich", "MaTaiKhoanThanhToan", "MaDanhMuc", "MaLoaiGiaoDich" };
            foreach (var col in hiddenColumns)
            {
                if (kryptonDataGridView1.Columns.Contains(col))
                    kryptonDataGridView1.Columns[col].Visible = false; // Ẩn các cột ID
            }
            kryptonDataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            kryptonDataGridView1.GridColor = Color.LightGray;

            // Đặt tên tiêu đề
            if (kryptonDataGridView1.Columns.Contains("TenGiaoDich")) kryptonDataGridView1.Columns["TenGiaoDich"].HeaderText = "Giao Dịch";
            if (kryptonDataGridView1.Columns.Contains("TenDoiTuong")) kryptonDataGridView1.Columns["TenDoiTuong"].HeaderText = "Đối Tượng";
            if (kryptonDataGridView1.Columns.Contains("TenTaiKhoan")) kryptonDataGridView1.Columns["TenTaiKhoan"].HeaderText = "Tài Khoản";
            if (kryptonDataGridView1.Columns.Contains("DanhMucChiTieu")) kryptonDataGridView1.Columns["DanhMucChiTieu"].HeaderText = "Danh Mục";
            if (kryptonDataGridView1.Columns.Contains("GhiChu")) kryptonDataGridView1.Columns["GhiChu"].HeaderText = "Ghi Chú";
            if (kryptonDataGridView1.Columns.Contains("TenLoaiGiaoDich")) kryptonDataGridView1.Columns["TenLoaiGiaoDich"].HeaderText = "Loại GD";

            // Định dạng cột Số Tiền
            if (kryptonDataGridView1.Columns.Contains("SoTien"))
            {
                kryptonDataGridView1.Columns["SoTien"].HeaderText = "Số Tiền";
                kryptonDataGridView1.Columns["SoTien"].DefaultCellStyle.Format = "N0";
                kryptonDataGridView1.Columns["SoTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            // Định dạng cột Ngày Giao Dịch
            if (kryptonDataGridView1.Columns.Contains("NgayGiaoDich"))
            {
                kryptonDataGridView1.Columns["NgayGiaoDich"].HeaderText = "Ngày GD";
                kryptonDataGridView1.Columns["NgayGiaoDich"].DefaultCellStyle.Format = "dd/MM/yyyy";
                kryptonDataGridView1.Columns["NgayGiaoDich"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        // --- 5. CHỨC NĂNG THÊM / SỬA / XÓA ---
        public void btnThem_Click(object sender, EventArgs e)
        {
            // Tạo form Thêm Giao Dịch mới qua DI
            FrmThemGiaoDich frm = ActivatorUtilities.CreateInstance<FrmThemGiaoDich>(
                _serviceProvider, // Sử dụng service provider để tự Inject các DI service vào constructor
                _dbFactory,
                _serviceProvider
            );
            frm.OnDataAdded = LoadData; // Đăng ký sự kiện: sau khi thêm xong thì tải lại dữ liệu
            frm.ShowDialog();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (kryptonDataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một giao dịch để sửa.", "Thông báo");
                return;
            }

            var row = kryptonDataGridView1.SelectedRows[0];

            // Lấy dữ liệu từ dòng được chọn
            int maGiaoDich = Convert.ToInt32(row.Cells["MaGiaoDich"].Value);
            string tenGiaoDich = row.Cells["TenGiaoDich"].Value?.ToString() ?? "";
            string ghiChu = row.Cells["GhiChu"].Value?.ToString() ?? "";
            decimal soTien = 0;
            decimal.TryParse(row.Cells["SoTien"].Value?.ToString(), out soTien);
            DateTime ngayGiaoDich = DateTime.Now;
            DateTime.TryParse(row.Cells["NgayGiaoDich"].Value?.ToString(), out ngayGiaoDich);
            int maDoiTuong = row.Cells["MaDoiTuongGiaoDich"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["MaDoiTuongGiaoDich"].Value) : 0;
            int maTaiKhoan = row.Cells["MaTaiKhoanThanhToan"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["MaTaiKhoanThanhToan"].Value) : 0;

            // Tạo form Sửa Giao Dịch
            // ActivatorUtilities sẽ tự inject các service (DbFactory, ServiceProvider, UserContext)
            // và truyền các tham số còn lại (dữ liệu cần sửa)
            FrmThemGiaoDich frm = ActivatorUtilities.CreateInstance<FrmThemGiaoDich>(
                _serviceProvider,
                maGiaoDich, // Tham số constructor 2
                tenGiaoDich,
                ghiChu,
                soTien,
                ngayGiaoDich,
                maDoiTuong,
                maTaiKhoan
            );

            frm.OnDataAdded = LoadData; // Đăng ký sự kiện tải lại dữ liệu
            frm.ShowDialog();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (kryptonDataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một giao dịch để xóa.");
                return;
            }

            int maGiaoDich = Convert.ToInt32(kryptonDataGridView1.SelectedRows[0].Cells["MaGiaoDich"].Value);

            if (MessageBox.Show("Bạn có chắc muốn xóa giao dịch này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;

            try
            {
                using (var context = _dbFactory.CreateDbContext())
                {
                    var gd = context.GiaoDichs.Find(maGiaoDich);
                    if (gd != null)
                    {
                        // Kiểm tra quyền: Chỉ xóa giao dịch của chính mình
                        if (gd.MaNguoiDung != _userContext.MaNguoiDung)
                        {
                            MessageBox.Show("Bạn không có quyền xóa giao dịch này.");
                            return;
                        }

                        context.GiaoDichs.Remove(gd);
                        context.SaveChanges();
                        MessageBox.Show("Xóa thành công!");
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Giao dịch không tồn tại hoặc đã bị xóa.");
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
        }

        // --- CÁC HÀM XỬ LÝ TÌM KIẾM VÀ HELPER ---
        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            // Xử lý Placeholder khi rời khỏi ô tìm kiếm
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                isPlaceholderActive = true;
                txtTimKiem.Text = " Tìm kiếm...";
                txtTimKiem.ForeColor = Color.Gray;
            }
        }

        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            // Xử lý Placeholder khi vào ô tìm kiếm
            if (isPlaceholderActive)
            {
                isPlaceholderActive = false;
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            // Lọc dữ liệu trên DataTable khi text thay đổi
            if (dtGiaoDich == null) return;
            string filter = txtTimKiem.Text.Trim().Replace("'", "''"); // Tránh SQL Injection cơ bản

            if (!isPlaceholderActive && !string.IsNullOrEmpty(filter) && txtTimKiem.Text != " Tìm kiếm...")
            {
                try
                {
                    dtGiaoDich.DefaultView.RowFilter =
                        $"TenGiaoDich LIKE '%{filter}%' OR " +
                        $"TenDoiTuong LIKE '%{filter}%' OR " +
                        $"TenTaiKhoan LIKE '%{filter}%' OR " +
                        $"DanhMucChiTieu LIKE '%{filter}%' OR " +
                        $"TenLoaiGiaoDich LIKE '%{filter}%' OR " +
                        $"GhiChu LIKE '%{filter}%'";
                }
                catch (Exception)
                {
                    dtGiaoDich.DefaultView.RowFilter = ""; 
                }
            }
            else
            {
                dtGiaoDich.DefaultView.RowFilter = ""; 
            }
        }
        private DataTable ConvertToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            System.Reflection.PropertyInfo[] Props = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (System.Reflection.PropertyInfo prop in Props)
            {
                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++) values[i] = Props[i].GetValue(item, null);
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
}