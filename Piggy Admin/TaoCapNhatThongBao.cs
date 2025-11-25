using System;
using System.Drawing;
using System.Windows.Forms;
using Data; // Chứa CurrentUserContext
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Piggy_Admin
{
    public partial class TaoCapNhatThongBao : Form
    {
        public ThongBao ThongBaoHienTai { get; private set; }
        private bool la_cap_nhat;

        private readonly CurrentUserContext _userContext;
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;

        // Constructor DUY NHẤT dùng cho DI
        public TaoCapNhatThongBao(CurrentUserContext userContext, IDbContextFactory<QLTCCNContext> dbFactory)
        {
            InitializeComponent();
            _userContext = userContext;
            _dbFactory = dbFactory;

            // Mặc định là chế độ tạo mới
            KhoiTaoCheDoTaoMoi();
        }

        private void KhoiTaoCheDoTaoMoi()
        {
            this.Text = "Tạo Thông Báo Mới";
            la_cap_nhat = false;
            lblMaThongBaoValue.Text = "Tạo mới";

            // Hiển thị tên Admin đang đăng nhập
            txtRole.Text = GetTenAdmin(_userContext.MaAdmin);
            txtRole.Enabled = false;
        }

        // Hàm này được gọi từ bên ngoài khi muốn Sửa
        public void LoadDataForUpdate(ThongBao tb)
        {
            if (tb == null) return;

            this.Text = "Cập Nhật Thông Báo";
            ThongBaoHienTai = tb;
            la_cap_nhat = true;

            lblMaThongBaoValue.Text = tb.MaThongBao.ToString();
            txtTieuDe.Text = tb.TieuDe;
            txtNoiDung.Text = tb.NoiDung;

            // Hiển thị tên người đã tạo thông báo này
            txtRole.Text = GetTenAdmin(tb.MaAdmin);
            txtRole.Enabled = false;
        }

        // Hàm phụ trợ lấy tên Admin từ DB
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
            if (string.IsNullOrWhiteSpace(txtTieuDe.Text) || string.IsNullOrWhiteSpace(txtNoiDung.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ tiêu đề và nội dung.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (la_cap_nhat)
            {
                // Cập nhật thông báo đang có (EF Core Tracking sẽ lo phần lưu sau này)
                // Lưu ý: Việc check quyền đã làm ở UserControl rồi, vào đây chỉ việc gán.
                ThongBaoHienTai.TieuDe = txtTieuDe.Text;
                ThongBaoHienTai.NoiDung = txtNoiDung.Text;
            }
            else
            {
                // Tạo mới: Phải gán MaAdmin của người đang đăng nhập
                if (_userContext.MaAdmin == null)
                {
                    MessageBox.Show("Lỗi xác thực: Không tìm thấy thông tin Admin của bạn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ThongBaoHienTai = new ThongBao
                {
                    TieuDe = txtTieuDe.Text,
                    NoiDung = txtNoiDung.Text,
                    MaAdmin = _userContext.MaAdmin.Value, // <-- LẤY ID THẬT
                    NgayTao = DateTime.Now
                };
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}