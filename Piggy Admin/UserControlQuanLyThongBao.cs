using Krypton.Toolkit;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Data;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Drawing; // Cần thiết cho Color

namespace Piggy_Admin
{
    public partial class UserControlQuanLyThongBao : UserControl
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider;

        // Constructor mới nhận Factory và ServiceProvider
        public UserControlQuanLyThongBao(IDbContextFactory<QLTCCNContext> dbFactory, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;

            // HOOKUP SỰ KIỆN TÌM KIẾM
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            kryptonDataGridView1.SelectionChanged += kryptonDataGridView1_SelectionChanged;

            // FIX LỖI TÌM KIẾM: Thêm GotFocus và LostFocus
            txtTimKiem.GotFocus += txtTimKiem_GotFocus;
            txtTimKiem.LostFocus += txtTimKiem_LostFocus;

            this.Load += UserControlQuanLyThongBao_Load;
        }

        private void UserControlQuanLyThongBao_Load(object sender, EventArgs e)
        {
            HienThiDanhSach();
        }

        // --- HÀM TÌM KIẾM PLACEHOLDER (FIX LỖI) ---
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
                HienThiDanhSach(); // Load lại toàn bộ danh sách khi ô tìm kiếm trống
            }
        }

        // --- READ: HÀM HIỂN THỊ VÀ TÌM KIẾM DỮ LIỆU TỪ DATABASE ---
        private void HienThiDanhSach(string tu_khoa_tim_kiem = null)
        {
            // Bỏ qua nếu giá trị là placeholder
            if (tu_khoa_tim_kiem != null && tu_khoa_tim_kiem.Trim() == "Tìm kiếm...")
            {
                tu_khoa_tim_kiem = "";
            }

            using (var dbContext = _dbFactory.CreateDbContext())
            {
                // Lỗi: Using Data; 2 lần, cần sửa lại
                IQueryable<ThongBao> query = dbContext.Set<ThongBao>();

                if (!string.IsNullOrWhiteSpace(tu_khoa_tim_kiem))
                {
                    string tu_khoa = tu_khoa_tim_kiem.ToLower().Trim();
                    // Lọc theo Tiêu đề HOẶC Nội dung
                    query = query.Where(tb =>
                        tb.TieuDe.ToLower().Contains(tu_khoa) ||
                        (tb.NoiDung != null && tb.NoiDung.ToLower().Contains(tu_khoa))
                    );
                }

                kryptonDataGridView1.DataSource = query.OrderByDescending(tb => tb.NgayTao).ToList();

                // Cấu hình Header Text
                if (kryptonDataGridView1.Columns.Contains("MaThongBao"))
                    kryptonDataGridView1.Columns["MaThongBao"].HeaderText = "Mã TB";
                if (kryptonDataGridView1.Columns.Contains("TieuDe"))
                    kryptonDataGridView1.Columns["TieuDe"].HeaderText = "Tiêu đề";
                if (kryptonDataGridView1.Columns.Contains("NoiDung"))
                    kryptonDataGridView1.Columns["NoiDung"].HeaderText = "Nội dung";
                if (kryptonDataGridView1.Columns.Contains("NgayTao"))
                    kryptonDataGridView1.Columns["NgayTao"].HeaderText = "Ngày tạo";
                // Ẩn các cột không cần thiết (như Foreign Key)
                if (kryptonDataGridView1.Columns.Contains("MaAdmin"))
                    kryptonDataGridView1.Columns["MaAdmin"].Visible = false;
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            HienThiDanhSach(txtTimKiem.Text);
        }

        // --- CRUD ACTIONS ---

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var frmTao = _serviceProvider.GetRequiredService<TaoCapNhatThongBao>())
            {
                var result = frmTao.ShowDialog();
                if (result == DialogResult.OK && frmTao.ThongBaoHienTai != null)
                {
                    using (var dbContext = _dbFactory.CreateDbContext())
                    {
                        ThongBao thong_bao_moi = frmTao.ThongBaoHienTai;
                        thong_bao_moi.NgayTao = DateTime.Now;

                        dbContext.ThongBaos.Add(thong_bao_moi);
                        dbContext.SaveChanges(); // Lệnh COMMIT

                        HienThiDanhSach();
                        MessageBox.Show("Đã tạo thông báo mới thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (kryptonDataGridView1.SelectedRows.Count == 0) return;

            var ma_tb = (int)kryptonDataGridView1.SelectedRows[0].Cells["MaThongBao"].Value;

            using (var dbContext = _dbFactory.CreateDbContext())
            {
                // Lấy đối tượng thật từ DB để theo dõi thay đổi
                ThongBao tb_hien_tai = dbContext.ThongBaos.FirstOrDefault(tb => tb.MaThongBao == ma_tb);

                if (tb_hien_tai != null)
                {
                    using (var frmCapNhat = _serviceProvider.GetRequiredService<TaoCapNhatThongBao>())
                    {
                        frmCapNhat.NapDuLieu(tb_hien_tai); // Điền dữ liệu vào form

                        var result = frmCapNhat.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            // FIX LỖI 2: Buộc EF Core nhận ra sự thay đổi
                            dbContext.Entry(tb_hien_tai).State = EntityState.Modified;

                            dbContext.SaveChanges(); // Lệnh COMMIT
                            HienThiDanhSach();
                            MessageBox.Show("Thông báo đã được cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (kryptonDataGridView1.SelectedRows.Count == 0) return;

            var dong_chon = kryptonDataGridView1.SelectedRows[0];
            var tieu_de = dong_chon.Cells["TieuDe"].Value.ToString();
            var ma_tb_xoa = (int)dong_chon.Cells["MaThongBao"].Value;

            var xac_nhan = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa thông báo '{tieu_de}' (Mã TB: {ma_tb_xoa}) không?",
                "Xác nhận Xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (xac_nhan == DialogResult.Yes)
            {
                using (var dbContext = _dbFactory.CreateDbContext())
                {
                    ThongBao tb_can_xoa = dbContext.ThongBaos.FirstOrDefault(tb => tb.MaThongBao == ma_tb_xoa);

                    if (tb_can_xoa != null)
                    {
                        dbContext.ThongBaos.Remove(tb_can_xoa);
                        dbContext.SaveChanges(); // Lệnh COMMIT

                        HienThiDanhSach();
                        MessageBox.Show("Thông báo đã được xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void kryptonDataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (kryptonDataGridView1.SelectedRows.Count > 0)
            {
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
            else
            {
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }
    }
}