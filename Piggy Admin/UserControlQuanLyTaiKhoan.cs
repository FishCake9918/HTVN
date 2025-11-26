using System;
using System.Windows.Forms;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

namespace Piggy_Admin
{
    public partial class UserControlQuanLyTaiKhoan : UserControl
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly CurrentUserContext _userContext;

        public UserControlQuanLyTaiKhoan(IDbContextFactory<QLTCCNContext> dbFactory, IServiceProvider serviceProvider, CurrentUserContext userContext)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
            _userContext = userContext;

            this.Load += UserControlQuanLyTaiKhoan_Load;

            txtTimKiem.TextChanged += TxtTimKiem_TextChanged;
            txtTimKiem.GotFocus += TxtTimKiem_GotFocus;
            txtTimKiem.LostFocus += TxtTimKiem_LostFocus;

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
        }

        private void UserControlQuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
           
            LoadData();
        }

        // HÀM QUAN TRỌNG: CHỈ LOAD USER
        private void LoadData(string keyword = "")
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                // Lọc: Chỉ lấy tài khoản có vai trò KHÁC "Admin"
                var query = db.Set<TaiKhoan>()
                    .Include(t => t.VaiTro)
                    .Include(t => t.NguoiDung)
                    // Giả định tên vai trò Admin là "Admin" (không phân biệt hoa thường trong SQL)
                    .Where(t => t.VaiTro.TenVaiTro != "Admin")
                    .AsQueryable();

                if (!string.IsNullOrEmpty(keyword) && keyword != "  Tìm kiếm...")
                {
                    string k = keyword.ToLower();
                    query = query.Where(t => t.Email.ToLower().Contains(k) ||
                                             (t.NguoiDung != null && t.NguoiDung.HoTen.ToLower().Contains(k)));
                }

                var list = query.Select(t => new
                {
                    MaTaiKhoan = t.MaTaiKhoan,
                    Email = t.Email,
                    VaiTro = t.VaiTro.TenVaiTro, // Sẽ luôn là User
                    HoTen = t.NguoiDung != null ? t.NguoiDung.HoTen : "Chưa cập nhật"
                }).ToList();

                kryptonDataGridView1.DataSource = list;

                if (kryptonDataGridView1.Columns["MaTaiKhoan"] != null) kryptonDataGridView1.Columns["MaTaiKhoan"].HeaderText = "ID";
                if (kryptonDataGridView1.Columns["HoTen"] != null) kryptonDataGridView1.Columns["HoTen"].HeaderText = "Họ Tên";
                if (kryptonDataGridView1.Columns["VaiTro"] != null) kryptonDataGridView1.Columns["VaiTro"].HeaderText = "Vai Trò";
            }
        }

        // ... (Các hàm tìm kiếm giữ nguyên)
        private void TxtTimKiem_TextChanged(object sender, EventArgs e) => LoadData(txtTimKiem.Text);
        private void TxtTimKiem_GotFocus(object sender, EventArgs e) { if (txtTimKiem.Text == "  Tìm kiếm...") { txtTimKiem.Text = ""; txtTimKiem.ForeColor = Color.Black; } }
        private void TxtTimKiem_LostFocus(object sender, EventArgs e) { if (string.IsNullOrWhiteSpace(txtTimKiem.Text)) { txtTimKiem.Text = "  Tìm kiếm..."; txtTimKiem.ForeColor = SystemColors.InactiveCaption; LoadData(); } }

        // --- CRUD ---

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!_userContext.IsAdmin) return;

            using (var frm = _serviceProvider.GetRequiredService<FormTaoCapNhatTaiKhoan>())
            {
                // Form này đã được cấu hình chỉ tạo User
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    MessageBox.Show("Thêm người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!_userContext.IsAdmin) return;

            if (kryptonDataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maTK = (int)kryptonDataGridView1.SelectedRows[0].Cells["MaTaiKhoan"].Value;

            // Không cần check vai trò Admin nữa vì danh sách không có Admin
            using (var frm = _serviceProvider.GetRequiredService<FormTaoCapNhatTaiKhoan>())
            {
                frm.LoadDataForEdit(maTK);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!_userContext.IsAdmin) return;

            if (kryptonDataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maTK = (int)kryptonDataGridView1.SelectedRows[0].Cells["MaTaiKhoan"].Value;
            string email = kryptonDataGridView1.SelectedRows[0].Cells["Email"].Value.ToString();

            // Không cần check tự xóa mình vì mình (Admin) không có trong danh sách này
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa tài khoản người dùng '{email}'?\nHành động này sẽ xóa toàn bộ dữ liệu liên quan.",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (var db = _dbFactory.CreateDbContext())
                    {
                        var tk = db.Set<TaiKhoan>().Find(maTK);
                        if (tk != null)
                        {
                            db.Set<TaiKhoan>().Remove(tk);
                            db.SaveChanges();
                            LoadData();
                            MessageBox.Show("Đã xóa tài khoản thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}