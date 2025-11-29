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
        // ==================================================================================
        // 1. KHAI BÁO BIẾN & DI SERVICE
        // ==================================================================================
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly CurrentUserContext _userContext;

        public UserControlQuanLyTaiKhoan(
            IDbContextFactory<QLTCCNContext> dbFactory,
            IServiceProvider serviceProvider,
            CurrentUserContext userContext)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
            _userContext = userContext;

            this.Load += UserControlQuanLyTaiKhoan_Load;

            // Áp dụng giao diện lưới 
            Dinhdangluoi.DinhDangLuoiAdmin(kryptonDataGridView1);

            // Đăng ký các sự kiện
            kryptonDataGridView1.CellDoubleClick += KryptonDataGridView1_CellDoubleClick;
            txtTimKiem.TextChanged += TxtTimKiem_TextChanged;
            txtTimKiem.GotFocus += TxtTimKiem_GotFocus;
            txtTimKiem.LostFocus += TxtTimKiem_LostFocus;

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
        }

        // ==================================================================================
        // 2. TẢI DỮ LIỆU
        // ==================================================================================
        private void UserControlQuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void KryptonDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnSua.PerformClick(); // Mở form sửa khi click đúp
            }
        }

        private void LoadData(string keyword = "")
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                // Chỉ lấy tài khoản người dùng  
                var query = db.Set<TaiKhoan>()
                    .Include(t => t.VaiTro)
                    .Include(t => t.NguoiDung)
                    .Where(t => t.VaiTro.TenVaiTro != "Admin")
                    .AsQueryable();

                // Lọc theo từ khóa (Email hoặc Họ tên)
                if (!string.IsNullOrEmpty(keyword) && keyword != "  Tìm kiếm...")
                {
                    string k = keyword.ToLower();
                    query = query.Where(t => t.Email.ToLower().Contains(k) ||
                                             (t.NguoiDung != null && t.NguoiDung.HoTen.ToLower().Contains(k)));
                }

                // Chuyển đổi dữ liệu hiển thị
                var list = query.Select(t => new
                {
                    MaTaiKhoan = t.MaTaiKhoan,
                    Email = t.Email,
                    VaiTro = t.VaiTro.TenVaiTro,
                    HoTen = t.NguoiDung != null ? t.NguoiDung.HoTen : "Chưa cập nhật"
                }).ToList();

                kryptonDataGridView1.DataSource = list;

                // Cấu hình cột hiển thị
                if (kryptonDataGridView1.Columns["MaTaiKhoan"] != null)
                    kryptonDataGridView1.Columns["MaTaiKhoan"].Visible = false; // Ẩn cột ID

                if (kryptonDataGridView1.Columns["HoTen"] != null) kryptonDataGridView1.Columns["HoTen"].HeaderText = "Họ Tên";
                if (kryptonDataGridView1.Columns["VaiTro"] != null) kryptonDataGridView1.Columns["VaiTro"].HeaderText = "Vai Trò";
                if (kryptonDataGridView1.Columns["Email"] != null) kryptonDataGridView1.Columns["Email"].HeaderText = "Email";

                kryptonDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        // ==================================================================================
        // 3. CHỨC NĂNG THÊM - SỬA - XÓA
        // ==================================================================================

        //Chức năng Thêm
        public void btnThem_Click(object sender, EventArgs e)
        {
            if (!_userContext.IsAdmin) return;

            using (var frm = _serviceProvider.GetRequiredService<FrmThemSuaTaiKhoan>())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData(); // Refresh sau khi thêm
                    MessageBox.Show("Thêm người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //Chức năng Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!_userContext.IsAdmin) return;

            if (kryptonDataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maTK = (int)kryptonDataGridView1.SelectedRows[0].Cells["MaTaiKhoan"].Value;

            using (var frm = _serviceProvider.GetRequiredService<FrmThemSuaTaiKhoan>())
            {
                frm.LoadDataForEdit(maTK); // Load dữ liệu cũ
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //Chức năng Xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!_userContext.IsAdmin) return;

            if (kryptonDataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maTK = (int)kryptonDataGridView1.SelectedRows[0].Cells["MaTaiKhoan"].Value;
            string email = kryptonDataGridView1.SelectedRows[0].Cells["Email"].Value.ToString();

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa tài khoản '{email}'?\nHành động này sẽ xóa toàn bộ dữ liệu liên quan.",
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

        // ==================================================================================
        // 4. TÌM KIẾM và SỰ KIỆN TIỆN ÍCH 
        // ==================================================================================
        private void TxtTimKiem_TextChanged(object sender, EventArgs e) => LoadData(txtTimKiem.Text);

        // Xử lý "Tìm kiếm..."
        private void TxtTimKiem_GotFocus(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "  Tìm kiếm...")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
            }
        }

        private void TxtTimKiem_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                txtTimKiem.Text = "  Tìm kiếm...";
                txtTimKiem.ForeColor = SystemColors.InactiveCaption;
                LoadData();
            }
        }
    }
}