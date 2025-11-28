using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection; // Cần thiết cho DI
using Data; // Namespace chứa Context và Entity

namespace Demo_Layout
{
    public partial class UserControlQuanLyGiaoDich : UserControl
    {
        // --- DI SERVICES ---
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly CurrentUserContext _userContext; // <-- 1. Thêm biến Context

        // Biến toàn cục
        private DataTable dtGiaoDich;
        private bool isPlaceholderActive = true;

        // --- CONSTRUCTOR NHẬN DI ---
        // 2. Inject CurrentUserContext vào đây
        public UserControlQuanLyGiaoDich(
            IDbContextFactory<QLTCCNContext> dbFactory, 
            IServiceProvider serviceProvider,
            CurrentUserContext userContext) 
        {
            InitializeComponent();

            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
            _userContext = userContext; // Gán giá trị

            // Đăng ký các sự kiện
            this.Load += UserControlQuanLyGiaoDich_Load;
            cbTaiKhoan.SelectedIndexChanged += cbTaiKhoan_SelectedIndexChanged;

            // ⭐ 1. THÊM SỰ KIỆN DOUBLE CLICK VÀO poisonDataGridView1 ⭐
            poisonDataGridView1.DoubleClick += poisonDataGridView1_DoubleClick;

            txtTimKiem.Enter += txtTimKiem_Enter;
            txtTimKiem.Leave += txtTimKiem_Leave;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
        }

        private void UserControlQuanLyGiaoDich_Load(object sender, EventArgs e)
        {
            // ⭐ Đã đổi từ DataGridView1 sang poisonDataGridView1 ⭐
            poisonDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            poisonDataGridView1.MultiSelect = false;
            poisonDataGridView1.ReadOnly = true;
            
            LogHelper.GhiLog(_dbFactory, "Quản lý giao dịch", _userContext.MaNguoiDung); // ghi log

            LoadComboBoxTaiKhoan();
            LoadData();
        }

        // ⭐ 2. HÀM XỬ LÝ DOUBLE CLICK: Kích hoạt nút Sửa ⭐
        private void poisonDataGridView1_DoubleClick(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không (sự kiện DoubleClick đôi khi bị kích hoạt ngay cả khi click vào header)
            if (poisonDataGridView1.SelectedRows.Count > 0)
            {
                // Gọi lại hàm btnSua_Click để thực hiện logic chỉnh sửa đã có
                btnSua_Click(sender, e);
            }
        }


        // --- 1. LOAD DANH SÁCH TÀI KHOẢN ---
        private void LoadComboBoxTaiKhoan()
        {
            try
            {
                using (var context = _dbFactory.CreateDbContext())
                {
                    // 3. Thay CURRENT_USER_ID bằng _userContext.MaNguoiDung
                    var listTK = context.TaiKhoanThanhToans
                                         .Where(t => t.MaNguoiDung == _userContext.MaNguoiDung && t.TrangThai == "Đang hoạt động")
                                         .Select(t => new { t.MaTaiKhoanThanhToan, t.TenTaiKhoan })
                                         .ToList();

                    listTK.Insert(0, new { MaTaiKhoanThanhToan = 0, TenTaiKhoan = "--- Tất cả tài khoản ---" });

                    cbTaiKhoan.SelectedIndexChanged -= cbTaiKhoan_SelectedIndexChanged;

                    cbTaiKhoan.DataSource = listTK;
                    cbTaiKhoan.DisplayMember = "TenTaiKhoan";
                    cbTaiKhoan.ValueMember = "MaTaiKhoanThanhToan";
                    cbTaiKhoan.SelectedIndex = 0;

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
            LoadData();
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
                    // 4. Lọc theo User đang đăng nhập
                    var query = context.GiaoDichs
                        .Include(g => g.LoaiGiaoDich)
                        .Include(g => g.DoiTuongGiaoDich)
                        .Include(g => g.TaiKhoanThanhToan)
                        .Include(g => g.DanhMucChiTieu)
                        .Where(g => g.MaNguoiDung == _userContext.MaNguoiDung); 

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
                    // ⭐ Đã đổi từ DataGridView1 sang poisonDataGridView1 ⭐
                    poisonDataGridView1.DataSource = dtGiaoDich;

                    FormatGrid();
                    CalculateTotal(query);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // --- 3. LOGIC TÍNH TỔNG THU & TỔNG CHI MỚI ---
        private void CalculateTotal(IQueryable<GiaoDich> filteredTransactions)
        {
            decimal tongThu = filteredTransactions.Where(g => g.MaLoaiGiaoDich == 1).Sum(g => g.SoTien);
            decimal tongChi = filteredTransactions.Where(g => g.MaLoaiGiaoDich == 2).Sum(g => g.SoTien);

            lblTongThuChi.Text = string.Format("💰 Tổng thu: {0:N0} đ | 💸 Tổng chi: {1:N0} đ", tongThu, tongChi);
            lblTongThuChi.ForeColor = Color.DarkSlateGray;
        }

        private void FormatGrid()
        {
            string[] hiddenColumns = { "MaGiaoDich", "MaDoiTuongGiaoDich", "MaTaiKhoanThanhToan", "MaDanhMuc", "MaLoaiGiaoDich" };
            foreach (var col in hiddenColumns)
            {
                if (poisonDataGridView1.Columns.Contains(col))
                    poisonDataGridView1.Columns[col].Visible = false;
            }
            poisonDataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            poisonDataGridView1.GridColor = Color.LightGray;

            if (poisonDataGridView1.Columns.Contains("TenGiaoDich")) poisonDataGridView1.Columns["TenGiaoDich"].HeaderText = "Giao Dịch";
            if (poisonDataGridView1.Columns.Contains("TenDoiTuong")) poisonDataGridView1.Columns["TenDoiTuong"].HeaderText = "Đối Tượng";
            if (poisonDataGridView1.Columns.Contains("TenTaiKhoan")) poisonDataGridView1.Columns["TenTaiKhoan"].HeaderText = "Tài Khoản";
            if (poisonDataGridView1.Columns.Contains("DanhMucChiTieu")) poisonDataGridView1.Columns["DanhMucChiTieu"].HeaderText = "Danh Mục";
            if (poisonDataGridView1.Columns.Contains("GhiChu")) poisonDataGridView1.Columns["GhiChu"].HeaderText = "Ghi Chú";
            if (poisonDataGridView1.Columns.Contains("TenLoaiGiaoDich")) poisonDataGridView1.Columns["TenLoaiGiaoDich"].HeaderText = "Loại GD";

            if (poisonDataGridView1.Columns.Contains("SoTien"))
            {
                poisonDataGridView1.Columns["SoTien"].HeaderText = "Số Tiền";
                poisonDataGridView1.Columns["SoTien"].DefaultCellStyle.Format = "N0";
                poisonDataGridView1.Columns["SoTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (poisonDataGridView1.Columns.Contains("NgayGiaoDich"))
            {
                poisonDataGridView1.Columns["NgayGiaoDich"].HeaderText = "Ngày GD";
                poisonDataGridView1.Columns["NgayGiaoDich"].DefaultCellStyle.Format = "dd/MM/yyyy";
                poisonDataGridView1.Columns["NgayGiaoDich"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        // --- 5. CHỨC NĂNG THÊM / SỬA / XÓA ---
        private void btnThem_Click(object sender, EventArgs e)
        {
            FrmThemGiaoDich frm = ActivatorUtilities.CreateInstance<FrmThemGiaoDich>(
                _serviceProvider,
                _dbFactory,
                _serviceProvider
            );
            frm.OnDataAdded = LoadData;
            frm.ShowDialog();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // ⭐ Đã đổi từ DataGridView1 sang poisonDataGridView1 ⭐
            if (poisonDataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một giao dịch để sửa.", "Thông báo");
                return;
            }

            var row = poisonDataGridView1.SelectedRows[0];

            int maGiaoDich = Convert.ToInt32(row.Cells["MaGiaoDich"].Value);
            string tenGiaoDich = row.Cells["TenGiaoDich"].Value?.ToString() ?? "";
            string ghiChu = row.Cells["GhiChu"].Value?.ToString() ?? "";
            decimal soTien = 0;
            decimal.TryParse(row.Cells["SoTien"].Value?.ToString(), out soTien);
            DateTime ngayGiaoDich = DateTime.Now;
            DateTime.TryParse(row.Cells["NgayGiaoDich"].Value?.ToString(), out ngayGiaoDich);
            int maDoiTuong = row.Cells["MaDoiTuongGiaoDich"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["MaDoiTuongGiaoDich"].Value) : 0;
            int maTaiKhoan = row.Cells["MaTaiKhoanThanhToan"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["MaTaiKhoanThanhToan"].Value) : 0;

            // 6. Truyền các tham số KHÔNG phải là Service (Dữ liệu cần sửa)
            // ActivatorUtilities sẽ tự lấy DbContext, ServiceProvider và CurrentUserContext từ DI
            FrmThemGiaoDich frm = ActivatorUtilities.CreateInstance<FrmThemGiaoDich>(
                _serviceProvider,
                maGiaoDich,           
                tenGiaoDich,          
                ghiChu,
                soTien,
                ngayGiaoDich,
                maDoiTuong,
                maTaiKhoan
            );

            frm.OnDataAdded = LoadData;
            frm.ShowDialog();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // ⭐ Đã đổi từ DataGridView1 sang poisonDataGridView1 ⭐
            if (poisonDataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một giao dịch để xóa.");
                return;
            }

            int maGiaoDich = Convert.ToInt32(poisonDataGridView1.SelectedRows[0].Cells["MaGiaoDich"].Value);

            if (MessageBox.Show("Bạn có chắc muốn xóa giao dịch này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;

            try
            {
                using (var context = _dbFactory.CreateDbContext())
                {
                    var gd = context.GiaoDichs.Find(maGiaoDich);
                    if (gd != null)
                    {
                        // Kiểm tra quyền (optional): Chỉ xóa của chính mình
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

        // ... (Các hàm tìm kiếm và helper giữ nguyên) ...
        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                isPlaceholderActive = true;
                txtTimKiem.Text = " Tìm kiếm...";
                txtTimKiem.ForeColor = Color.Gray;
            }
        }

        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (isPlaceholderActive)
            {
                isPlaceholderActive = false;
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (dtGiaoDich == null) return;
            string filter = txtTimKiem.Text.Trim().Replace("'", "''");

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