using System;
using System.Drawing;
using System.Windows.Forms;
using Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Piggy_Admin
{
    public partial class FrmThemSuaThongBao : Form
    {
        public ThongBao ThongBaoHienTai { get; private set; }
        private bool la_cap_nhat; // Cờ đánh dấu đang Sửa hay Thêm

        private readonly CurrentUserContext _userContext;
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;

        public FrmThemSuaThongBao(CurrentUserContext userContext, IDbContextFactory<QLTCCNContext> dbFactory)
        {
            InitializeComponent();
            _userContext = userContext;
            _dbFactory = dbFactory;
            this.Paint += Vien_Paint; // Vẽ viền cho form

            // Mặc định mở lên là chế độ Thêm Mới
            KhoiTaoCheDoTaoMoi();
        }

        // Hàm vẽ khung viền thủ công (do dùng FormBorderStyle.None)
        private void Vien_Paint(object sender, PaintEventArgs e)
        {
            Color borderColor = Color.Black;
            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            using (Pen pen = new Pen(borderColor, 3))
            {
                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        private void KhoiTaoCheDoTaoMoi()
        {
            this.Text = "Tạo Thông Báo Mới";
            la_cap_nhat = false;
            lblMaThongBaoValue.Text = "Tạo mới";

            // Tự động điền tên Admin đang đăng nhập
            txtRole.Text = GetTenAdmin(_userContext.MaAdmin);
            txtRole.Enabled = false;
        }

        // Chuyển sang chế độ Sửa và điền dữ liệu cũ
        public void LoadDataForUpdate(ThongBao tb)
        {
            if (tb == null) return;

            this.Text = "Cập Nhật Thông Báo";
            ThongBaoHienTai = tb;
            la_cap_nhat = true;

            lblMaThongBaoValue.Text = tb.MaThongBao.ToString();
            txtTieuDe.Text = tb.TieuDe;
            txtNoiDung.Text = tb.NoiDung;

            // Hiển thị người tạo ban đầu
            txtRole.Text = GetTenAdmin(tb.MaAdmin);
            txtRole.Enabled = false;
        }

        // Hàm lấy tên Admin từ ID
        private string GetTenAdmin(int? maAdmin)
        {
            if (maAdmin == null) return "Hệ thống";

            using (var db = _dbFactory.CreateDbContext())
            {
                var admin = db.Admins.FirstOrDefault(a => a.MaAdmin == maAdmin);
                return admin != null ? admin.HoTenAdmin : $"Admin #{maAdmin}";
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(txtTieuDe.Text) || string.IsNullOrWhiteSpace(txtNoiDung.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ tiêu đề và nội dung.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (la_cap_nhat)
            {
                // Chế độ Sửa: Chỉ cập nhật thuộc tính
                ThongBaoHienTai.TieuDe = txtTieuDe.Text;
                ThongBaoHienTai.NoiDung = txtNoiDung.Text;
            }
            else
            {
                // Chế độ Thêm: Tạo object mới, gán MaAdmin hiện tại
                if (_userContext.MaAdmin == null)
                {
                    MessageBox.Show("Lỗi xác thực: Không tìm thấy thông tin Admin của bạn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ThongBaoHienTai = new ThongBao
                {
                    TieuDe = txtTieuDe.Text,
                    NoiDung = txtNoiDung.Text,
                    MaAdmin = _userContext.MaAdmin.Value,
                    NgayTao = DateTime.Now
                };
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e) => this.Close();
        private void button1_Click(object sender, EventArgs e) => this.Close();
    }
}