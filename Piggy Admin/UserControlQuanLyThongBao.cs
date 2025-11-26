using Krypton.Toolkit;
using System.Windows.Forms;
using Data; // Chứa CurrentUserContext và Models
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Piggy_Admin
{
    public partial class UserControlQuanLyThongBao : UserControl
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider;
        // Inject CurrentUserContext từ project Data
        private readonly CurrentUserContext _userContext;

        public UserControlQuanLyThongBao(
            IDbContextFactory<QLTCCNContext> dbFactory,
            IServiceProvider serviceProvider,
            CurrentUserContext userContext) // <-- Inject vào đây
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
            _userContext = userContext;

            // Hookup events
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            kryptonDataGridView1.SelectionChanged += kryptonDataGridView1_SelectionChanged;
            txtTimKiem.GotFocus += txtTimKiem_GotFocus;
            txtTimKiem.LostFocus += txtTimKiem_LostFocus;
            this.Load += UserControlQuanLyThongBao_Load;
        }

        private void UserControlQuanLyThongBao_Load(object sender, EventArgs e)
        {
            HienThiDanhSach();
        }

        // --- HIỂN THỊ DANH SÁCH VỚI TÊN ADMIN ---
        private void HienThiDanhSach(string tu_khoa = null)
        {
            using (var dbContext = _dbFactory.CreateDbContext())
            {
                // Join bảng ThongBao với bảng Admin để lấy tên
                var query = dbContext.ThongBaos.Include(tb => tb.Admin).AsQueryable();

                if (!string.IsNullOrEmpty(tu_khoa) && tu_khoa != "  Tìm kiếm...")
                {
                    query = query.Where(tb => tb.TieuDe.Contains(tu_khoa) || tb.NoiDung.Contains(tu_khoa));
                }

                // Chọn các trường cần hiển thị
                var danhSach = query.OrderByDescending(tb => tb.MaThongBao)
                                    .Select(tb => new
                                    {
                                        MaThongBao = tb.MaThongBao,
                                        TieuDe = tb.TieuDe,
                                        NoiDung = tb.NoiDung,
                                        NgayTao = tb.NgayTao,
                                        // Lấy tên Admin thông qua quan hệ FK
                                        NguoiTao = tb.Admin != null ? tb.Admin.HoTenAdmin : "Không xác định",
                                        MaAdmin = tb.MaAdmin // Lấy thêm cái này ẩn đi để check quyền
                                    })
                                    .ToList();

                kryptonDataGridView1.DataSource = danhSach;

                // Cấu hình hiển thị cột
                if (kryptonDataGridView1.Columns.Contains("TieuDe")) kryptonDataGridView1.Columns["MaThongBao"].HeaderText = "Mã thông báo";
                if (kryptonDataGridView1.Columns.Contains("TieuDe")) kryptonDataGridView1.Columns["TieuDe"].HeaderText = "Tiêu đề";
                if (kryptonDataGridView1.Columns.Contains("NoiDung")) kryptonDataGridView1.Columns["NoiDung"].HeaderText = "Nội dung";
                if (kryptonDataGridView1.Columns.Contains("NgayTao")) kryptonDataGridView1.Columns["NgayTao"].HeaderText = "Ngày tạo";
                if (kryptonDataGridView1.Columns.Contains("NguoiTao")) kryptonDataGridView1.Columns["NguoiTao"].HeaderText = "Người tạo";

                // Ẩn cột MaAdmin (chỉ dùng để so sánh code)
                if (kryptonDataGridView1.Columns.Contains("MaAdmin")) kryptonDataGridView1.Columns["MaAdmin"].Visible = false;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng có phải Admin không (dựa vào Context)
            if (!_userContext.IsAdmin || _userContext.MaAdmin == null)
            {
                MessageBox.Show("Bạn không có quyền tạo thông báo (Yêu cầu quyền Admin).", "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            using (var taoCapNhatForm = _serviceProvider.GetRequiredService<TaoCapNhatThongBao>())
            {
                // Form tự lấy MaAdmin từ Context khi khởi tạo chế độ Thêm mới
                if (taoCapNhatForm.ShowDialog() == DialogResult.OK)
                {
                    using (var dbContext = _dbFactory.CreateDbContext())
                    {
                        dbContext.ThongBaos.Add(taoCapNhatForm.ThongBaoHienTai);
                        dbContext.SaveChanges();
                        HienThiDanhSach();
                        MessageBox.Show("Thêm thông báo thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (kryptonDataGridView1.SelectedRows.Count == 0) return;

            var row = kryptonDataGridView1.SelectedRows[0];
            int maTB = (int)row.Cells["MaThongBao"].Value;
            int maAdminTao = (int)row.Cells["MaAdmin"].Value; // Lấy mã người tạo từ Grid

            // === KIỂM TRA QUYỀN SỬA ===
            // 1. Phải là Admin
            // 2. MaAdmin đang đăng nhập phải TRÙNG KHỚP với MaAdmin tạo thông báo
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
                    using (var frm = _serviceProvider.GetRequiredService<TaoCapNhatThongBao>())
                    {
                        // Load dữ liệu vào form
                        frm.LoadDataForUpdate(tbCanSua);

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            // Lưu thay đổi
                            dbContext.SaveChanges();
                            HienThiDanhSach();
                            MessageBox.Show("Cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (kryptonDataGridView1.SelectedRows.Count == 0) return;

            var row = kryptonDataGridView1.SelectedRows[0];
            int maTB = (int)row.Cells["MaThongBao"].Value;
            int maAdminTao = (int)row.Cells["MaAdmin"].Value;
            string tieuDe = row.Cells["TieuDe"].Value.ToString();

            // === KIỂM TRA QUYỀN XÓA ===
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

        // Các hàm phụ trợ (Search, Focus...) giữ nguyên
        private void txtTimKiem_TextChanged(object sender, EventArgs e) => HienThiDanhSach(txtTimKiem.Text);
        private void kryptonDataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            bool coChon = kryptonDataGridView1.SelectedRows.Count > 0;
            btnSua.Enabled = coChon;
            btnXoa.Enabled = coChon;
        }
        // ... (Giữ nguyên các hàm GotFocus/LostFocus)
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