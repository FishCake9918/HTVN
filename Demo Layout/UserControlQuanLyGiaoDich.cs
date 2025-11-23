using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Data; // Namespace chứa Context và Entity

namespace Demo_Layout
{
    public partial class UserControlQuanLyGiaoDich : UserControl
    {
        // Cấu hình
        private const string ConnectionString = "Data Source=DESKTOP-V70T5QI;Initial Catalog=QLTCCN;Integrated Security=True;TrustServerCertificate=True";
        private const int CURRENT_USER_ID = 1;

        // Biến toàn cục
        private DataTable dtGiaoDich; // Dùng để tìm kiếm (Filter)
        private bool isPlaceholderActive = true;

        public UserControlQuanLyGiaoDich()
        {
            InitializeComponent();

            // Đăng ký các sự kiện (nếu chưa làm bên giao diện Design)
            this.Load += UserControlQuanLyGiaoDich_Load;

            // Xử lý sự kiện khi chọn tài khoản
            cbTaiKhoan.SelectedIndexChanged += cbTaiKhoan_SelectedIndexChanged;

            // Xử lý sự kiện tìm kiếm
            txtTimKiem.Enter += txtTimKiem_Enter;
            txtTimKiem.Leave += txtTimKiem_Leave;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
        }

        // --- HÀM HỖ TRỢ: LẤY CONTEXT ---
        private QLTCCNContext GetContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<QLTCCNContext>();
            optionsBuilder.UseSqlServer(ConnectionString);
            return new QLTCCNContext(optionsBuilder.Options);
        }

        private void UserControlQuanLyGiaoDich_Load(object sender, EventArgs e)
        {
            // Cấu hình GridView chọn nguyên hàng
            kryptonDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            kryptonDataGridView1.MultiSelect = false;
            kryptonDataGridView1.ReadOnly = true; // Không cho sửa trực tiếp trên lưới

            LoadComboBoxTaiKhoan();
            LoadData();
        }

        // --- 1. LOAD DANH SÁCH TÀI KHOẢN ---
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

                    // Thêm tùy chọn "Tất cả" lên đầu
                    listTK.Insert(0, new { MaTaiKhoanThanhToan = 0, TenTaiKhoan = "--- Tất cả tài khoản ---" });

                    // Gán dữ liệu, ngắt sự kiện tạm thời để tránh reload không cần thiết
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

        // --- SỰ KIỆN CHỌN TÀI KHOẢN ---
        private void cbTaiKhoan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(); // Load lại dữ liệu và tính lại tiền khi đổi tài khoản
        }

        // --- 2. LOAD DỮ LIỆU CHÍNH & TÍNH TỔNG ---
        private void LoadData()
        {
            try
            {
                // Lấy ID tài khoản đang chọn
                int maTaiKhoanLoc = 0;
                if (cbTaiKhoan.SelectedValue != null && int.TryParse(cbTaiKhoan.SelectedValue.ToString(), out int val))
                {
                    maTaiKhoanLoc = val;
                }

                using (var context = GetContext())
                {
                    // A. Tạo Query cơ bản (Chưa chạy xuống DB)
                    var query = context.GiaoDichs
                        .Include(g => g.LoaiGiaoDich)
                        .Include(g => g.DoiTuongGiaoDich)
                        .Include(g => g.TaiKhoanThanhToan)
                        .Include(g => g.DanhMucChiTieu)
                        .Where(g => g.MaNguoiDung == CURRENT_USER_ID);

                    // B. Lọc theo tài khoản (nếu không chọn Tất cả)
                    if (maTaiKhoanLoc > 0)
                    {
                        query = query.Where(g => g.MaTaiKhoanThanhToan == maTaiKhoanLoc);
                    }

                    // C. Lấy dữ liệu hiển thị (Projection)
                    // LƯU Ý: Thứ tự các dòng ở đây quyết định thứ tự cột trên Grid
                    var dataList = query.Select(gd => new
                    {
                        // 1. Các cột ID (Sẽ ẩn đi)
                        gd.MaGiaoDich,
                        gd.MaDoiTuongGiaoDich,
                        gd.MaTaiKhoanThanhToan,
                        gd.MaDanhMuc,
                        gd.MaLoaiGiaoDich,

                        // 2. Các cột hiển thị
                        TenGiaoDich = gd.TenGiaoDich,
                        TenDoiTuong = gd.DoiTuongGiaoDich != null ? gd.DoiTuongGiaoDich.TenDoiTuong : "",
                        TenTaiKhoan = gd.TaiKhoanThanhToan != null ? gd.TaiKhoanThanhToan.TenTaiKhoan : "",
                        DanhMucChiTieu = gd.DanhMucChiTieu != null ? gd.DanhMucChiTieu.TenDanhMuc : "",

                        gd.SoTien,
                        gd.NgayGiaoDich,
                        gd.GhiChu,

                        // 3. Cột Loại Giao Dịch (ĐỂ CUỐI CÙNG theo yêu cầu)
                        TenLoaiGiaoDich = gd.LoaiGiaoDich != null ? gd.LoaiGiaoDich.TenLoaiGiaoDich : ""
                    })
                    .OrderByDescending(x => x.NgayGiaoDich)
                    .ToList();

                    // D. Đổ dữ liệu vào Grid thông qua DataTable (để hỗ trợ tìm kiếm text)
                    dtGiaoDich = ConvertToDataTable(dataList);
                    kryptonDataGridView1.DataSource = dtGiaoDich;

                    // E. Định dạng cột (Ẩn ID, Đổi tên Header, Format tiền)
                    FormatGrid();

                    // F. Tính toán tổng số dư
                    CalculateTotalBalance(context, maTaiKhoanLoc, query);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // --- 3. LOGIC TÍNH TỔNG SỐ DƯ ---
        private void CalculateTotalBalance(QLTCCNContext context, int maTaiKhoanLoc, IQueryable<GiaoDich> filteredTransactions)
        {
            // Bước 1: Lấy Số Dư Đầu Kỳ
            decimal soDuDau = 0;
            if (maTaiKhoanLoc > 0)
            {
                // Nếu chọn 1 tài khoản -> Lấy số dư đầu của tài khoản đó
                var tk = context.TaiKhoanThanhToans.Find(maTaiKhoanLoc);
                soDuDau = tk != null ? tk.SoDuBanDau : 0;
            }
            else
            {
                // Nếu chọn "Tất cả" -> Tổng số dư đầu của tất cả các ví
                soDuDau = context.TaiKhoanThanhToans
                                 .Where(t => t.MaNguoiDung == CURRENT_USER_ID)
                                 .Sum(t => t.SoDuBanDau);
            }

            // Bước 2: Tính Tổng Thu - Chi phát sinh
            // MaLoaiGiaoDich: 1 = Thu, 2 = Chi
            decimal tongThu = filteredTransactions.Where(g => g.MaLoaiGiaoDich == 1).Sum(g => g.SoTien);
            decimal tongChi = filteredTransactions.Where(g => g.MaLoaiGiaoDich == 2).Sum(g => g.SoTien);

            // Bước 3: Tính kết quả
            decimal tongSoDu = soDuDau + tongThu - tongChi;

            // Bước 4: Hiển thị
            lblTongGiaoDich.Text = string.Format("Tổng số dư: {0:N0} đ", tongSoDu);

            // Đổi màu cảnh báo nếu âm
            lblTongGiaoDich.ForeColor = tongSoDu < 0 ? Color.Red : Color.SeaGreen;
        }

        // --- 4. ĐỊNH DẠNG GRIDVIEW ---
        private void FormatGrid()
        {
            // Ẩn các cột ID
            string[] hiddenColumns = { "MaGiaoDich", "MaDoiTuongGiaoDich", "MaTaiKhoanThanhToan", "MaDanhMuc", "MaLoaiGiaoDich" };
            foreach (var col in hiddenColumns)
            {
                if (kryptonDataGridView1.Columns.Contains(col))
                    kryptonDataGridView1.Columns[col].Visible = false;
            }

            // Đặt tên tiêu đề tiếng Việt
            if (kryptonDataGridView1.Columns.Contains("TenGiaoDich")) kryptonDataGridView1.Columns["TenGiaoDich"].HeaderText = "Giao Dịch";
            if (kryptonDataGridView1.Columns.Contains("TenDoiTuong")) kryptonDataGridView1.Columns["TenDoiTuong"].HeaderText = "Đối Tượng";
            if (kryptonDataGridView1.Columns.Contains("TenTaiKhoan")) kryptonDataGridView1.Columns["TenTaiKhoan"].HeaderText = "Tài Khoản";
            if (kryptonDataGridView1.Columns.Contains("DanhMucChiTieu")) kryptonDataGridView1.Columns["DanhMucChiTieu"].HeaderText = "Danh Mục";
            if (kryptonDataGridView1.Columns.Contains("GhiChu")) kryptonDataGridView1.Columns["GhiChu"].HeaderText = "Ghi Chú";
            if (kryptonDataGridView1.Columns.Contains("TenLoaiGiaoDich")) kryptonDataGridView1.Columns["TenLoaiGiaoDich"].HeaderText = "Loại GD";

            // Format Số tiền
            if (kryptonDataGridView1.Columns.Contains("SoTien"))
            {
                kryptonDataGridView1.Columns["SoTien"].HeaderText = "Số Tiền";
                kryptonDataGridView1.Columns["SoTien"].DefaultCellStyle.Format = "N0"; // VD: 1,000,000
                kryptonDataGridView1.Columns["SoTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            // Format Ngày
            if (kryptonDataGridView1.Columns.Contains("NgayGiaoDich"))
            {
                kryptonDataGridView1.Columns["NgayGiaoDich"].HeaderText = "Ngày GD";
                kryptonDataGridView1.Columns["NgayGiaoDich"].DefaultCellStyle.Format = "dd/MM/yyyy";
                kryptonDataGridView1.Columns["NgayGiaoDich"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        // --- 5. CHỨC NĂNG THÊM / SỬA / XÓA ---
        private void btnThem_Click(object sender, EventArgs e)
        {
            FrmThemGiaoDich frm = new FrmThemGiaoDich();
            frm.OnDataAdded = LoadData; // Callback reload
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

            // Lấy dữ liệu từ dòng chọn
            int maGiaoDich = Convert.ToInt32(row.Cells["MaGiaoDich"].Value);
            string tenGiaoDich = row.Cells["TenGiaoDich"].Value?.ToString() ?? "";
            string ghiChu = row.Cells["GhiChu"].Value?.ToString() ?? "";

            decimal soTien = 0;
            decimal.TryParse(row.Cells["SoTien"].Value?.ToString(), out soTien);

            DateTime ngayGiaoDich = DateTime.Now;
            DateTime.TryParse(row.Cells["NgayGiaoDich"].Value?.ToString(), out ngayGiaoDich);

            // Xử lý null an toàn
            int maDoiTuong = row.Cells["MaDoiTuongGiaoDich"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["MaDoiTuongGiaoDich"].Value) : 0;
            int maTaiKhoan = row.Cells["MaTaiKhoanThanhToan"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["MaTaiKhoanThanhToan"].Value) : 0;

            FrmThemGiaoDich frm = new FrmThemGiaoDich(maGiaoDich, tenGiaoDich, ghiChu, soTien, ngayGiaoDich, maDoiTuong, maTaiKhoan);
            frm.OnDataAdded = LoadData;
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
                using (var context = GetContext())
                {
                    var gd = context.GiaoDichs.Find(maGiaoDich);
                    if (gd != null)
                    {
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

        // --- SỰ KIỆN KHI RỜI KHỎI Ô TÌM KIẾM (LEAVE) ---
        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            // Kiểm tra nếu người dùng không nhập gì hoặc chỉ nhập khoảng trắng
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                // 1. QUAN TRỌNG: Phải gán cờ này thành TRUE trước!
                // Để khi dòng code gán Text bên dưới chạy, hàm TextChanged biết đây là Placeholder
                isPlaceholderActive = true;

                // 2. Sau đó mới gán lại chữ "Tìm kiếm..."
                txtTimKiem.Text = " Tìm kiếm...";
                txtTimKiem.ForeColor = Color.Gray;
            }
        }

        // --- SỰ KIỆN KHI CLICK VÀO Ô TÌM KIẾM (ENTER) ---
        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (isPlaceholderActive)
            {
                // Tương tự, gán cờ False trước cho chắc ăn
                isPlaceholderActive = false;

                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
            }
        }

        // --- SỰ KIỆN TEXT CHANGED (Cập nhật logic một chút cho an toàn) ---
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (dtGiaoDich == null) return;

            // Lấy text và xử lý ký tự đặc biệt
            string filter = txtTimKiem.Text.Trim().Replace("'", "''");

            // Thêm điều kiện: Text không được trùng với chữ Placeholder "Tìm kiếm..."
            // Điều này giúp chặn lỗi triệt để hơn
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
                    // Tránh crash nếu người dùng nhập ký tự quá đặc biệt mà Replace chưa xử lý hết
                    dtGiaoDich.DefaultView.RowFilter = "";
                }
            }
            else
            {
                dtGiaoDich.DefaultView.RowFilter = ""; // Bỏ lọc, hiện tất cả
            }
        }

        // --- HELPER: CHUYỂN LIST -> DATATABLE ---
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