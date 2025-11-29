using Krypton.Toolkit;
using System.Windows.Forms;
using Data;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Piggy_Admin
{
    public partial class UserControlQuanLyThongBao : UserControl
    {
        // ==================================================================================
        // 1. KHAI BÁO BIẾN và DI SERVICE
        // ==================================================================================
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly CurrentUserContext _userContext;

        public UserControlQuanLyThongBao(
            IDbContextFactory<QLTCCNContext> dbFactory,
            IServiceProvider serviceProvider,
            CurrentUserContext userContext)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
            _userContext = userContext;

            // Trang trí giao diện lưới
            Dinhdangluoi.DinhDangLuoiAdmin(kryptonDataGridView1);

            // Đăng ký các sự kiện 
            kryptonDataGridView1.CellDoubleClick += KryptonDataGridView1_CellDoubleClick;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            kryptonDataGridView1.SelectionChanged += kryptonDataGridView1_SelectionChanged;
            txtTimKiem.GotFocus += txtTimKiem_GotFocus;
            txtTimKiem.LostFocus += txtTimKiem_LostFocus;

            this.Load += UserControlQuanLyThongBao_Load;
        }

        // ==================================================================================
        // 2. TẢI DỮ LIỆU
        // ==================================================================================
        private void UserControlQuanLyThongBao_Load(object sender, EventArgs e)
        {
            HienThiDanhSach();
        }

        private void HienThiDanhSach(string tu_khoa = null)
        {
            using (var dbContext = _dbFactory.CreateDbContext())
            {
                // Lấy thông báo kèm thông tin người tạo
                var query = dbContext.ThongBaos.Include(tb => tb.Admin).AsQueryable();

                // Lọc theo từ khóa nếu có
                if (!string.IsNullOrEmpty(tu_khoa) && tu_khoa != "  Tìm kiếm...")
                {
                    query = query.Where(tb => tb.TieuDe.Contains(tu_khoa) || tb.NoiDung.Contains(tu_khoa));
                }

                // Chọn các cột cần hiển thị
                var danhSach = query.OrderByDescending(tb => tb.MaThongBao)
                                    .Select(tb => new
                                    {
                                        MaThongBao = tb.MaThongBao,
                                        TieuDe = tb.TieuDe,
                                        NoiDung = tb.NoiDung,
                                        NgayTao = tb.NgayTao,
                                        NguoiTao = tb.Admin != null ? tb.Admin.HoTenAdmin : "Không xác định",
                                        MaAdmin = tb.MaAdmin
                                    })
                                    .ToList();

                kryptonDataGridView1.DataSource = danhSach;

                // Cấu hình tiêu đề cột và ẩn các cột ID
                if (kryptonDataGridView1.Columns.Contains("MaThongBao")) kryptonDataGridView1.Columns["MaThongBao"].HeaderText = "Mã thông báo";
                kryptonDataGridView1.Columns["MaThongBao"].Visible = false;
                if (kryptonDataGridView1.Columns.Contains("TieuDe")) kryptonDataGridView1.Columns["TieuDe"].HeaderText = "Tiêu đề";
                if (kryptonDataGridView1.Columns.Contains("NoiDung")) kryptonDataGridView1.Columns["NoiDung"].HeaderText = "Nội dung";
                if (kryptonDataGridView1.Columns.Contains("NgayTao")) kryptonDataGridView1.Columns["NgayTao"].HeaderText = "Ngày tạo";
                if (kryptonDataGridView1.Columns.Contains("NguoiTao")) kryptonDataGridView1.Columns["NguoiTao"].HeaderText = "Người tạo";
                if (kryptonDataGridView1.Columns.Contains("MaAdmin")) kryptonDataGridView1.Columns["MaAdmin"].Visible = false;
            }
        }

        // ==================================================================================
        // 3. CHỨC NĂNG THÊM - SỬA - XÓA
        // ==================================================================================

        // Chức năng Thêm
        public void btnThem_Click(object sender, EventArgs e)
        {
            // Mở form thêm mới
            using (var taoCapNhatForm = _serviceProvider.GetRequiredService<FrmThemSuaThongBao>())
            {
                if (taoCapNhatForm.ShowDialog() == DialogResult.OK)
                {
                    using (var dbContext = _dbFactory.CreateDbContext())
                    {
                        dbContext.ThongBaos.Add(taoCapNhatForm.ThongBaoHienTai);
                        dbContext.SaveChanges();
                        HienThiDanhSach(); // Refresh lại lưới
                        MessageBox.Show("Thêm thông báo thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        // Chức năng Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (kryptonDataGridView1.SelectedRows.Count == 0) return;

            var row = kryptonDataGridView1.SelectedRows[0];
            int maTB = (int)row.Cells["MaThongBao"].Value;
            int maAdminTao = (int)row.Cells["MaAdmin"].Value;

            // Kiểm tra quyền: Phải là chính chủ (người tạo) mới được sửa
            if (!_userContext.IsAdmin || _userContext.MaAdmin != maAdminTao)
            {
                MessageBox.Show("Bạn không phải là người tạo thông báo này nên không thể chỉnh sửa.", "Không đủ quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var dbContext = _dbFactory.CreateDbContext())
            {
                var tbCanSua = dbContext.ThongBaos.FirstOrDefault(tb => tb.MaThongBao == maTB);
                if (tbCanSua != null)
                {
                    using (var frm = _serviceProvider.GetRequiredService<FrmThemSuaThongBao>())
                    {
                        frm.LoadDataForUpdate(tbCanSua); // Load dữ liệu cũ lên form

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            dbContext.SaveChanges();
                            HienThiDanhSach();
                            MessageBox.Show("Cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        // Chức năng Xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (kryptonDataGridView1.SelectedRows.Count == 0) return;

            var row = kryptonDataGridView1.SelectedRows[0];
            int maTB = (int)row.Cells["MaThongBao"].Value;
            int maAdminTao = (int)row.Cells["MaAdmin"].Value;
            string tieuDe = row.Cells["TieuDe"].Value.ToString();

            // Kiểm tra quyền: Chỉ chính chủ mới được xóa
            if (!_userContext.IsAdmin || _userContext.MaAdmin != maAdminTao)
            {
                MessageBox.Show("Bạn không phải là người tạo thông báo này nên không thể xóa.", "Không đủ quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa thông báo '{tieuDe}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var dbContext = _dbFactory.CreateDbContext())
                {
                    var tbXoa = dbContext.ThongBaos.FirstOrDefault(tb => tb.MaThongBao == maTB);
                    if (tbXoa != null)
                    {
                        dbContext.ThongBaos.Remove(tbXoa);
                        dbContext.SaveChanges();
                        HienThiDanhSach();
                        MessageBox.Show("Đã xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        // ==================================================================================
        // 4. CÁC SỰ KIỆN 
        // ==================================================================================
        private void KryptonDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Double click vào dòng thì mở chức năng Sửa
            if (e.RowIndex >= 0) btnSua.PerformClick();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e) => HienThiDanhSach(txtTimKiem.Text);

        private void kryptonDataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Chỉ bật nút Sửa/Xóa khi có dòng được chọn
            bool coChon = kryptonDataGridView1.SelectedRows.Count > 0;
            btnSua.Enabled = coChon;
            btnXoa.Enabled = coChon;
        }

        // Xử lý Placeholder "Tìm kiếm..."
        private void txtTimKiem_GotFocus(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "  Tìm kiếm...")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
            }
        }

        private void txtTimKiem_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                txtTimKiem.Text = "  Tìm kiếm...";
                txtTimKiem.ForeColor = System.Drawing.SystemColors.InactiveCaption;
                HienThiDanhSach();
            }
        }
    }
}