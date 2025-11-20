using System;
using System.Drawing;
using System.Windows.Forms;
using Data; // <-- ĐÃ SỬA: Sửa lại using để trỏ đến Model thật
using System.Linq;

namespace Piggy_Admin
{
    public partial class TaoCapNhatThongBao : Form
    {
        // Sử dụng Model ThongBao thật
        public ThongBao ThongBaoHienTai { get; private set; }
        private bool la_cap_nhat;

        // Constructor tạo mới
        public TaoCapNhatThongBao()
        {
            InitializeComponent();
            this.Text = "Tạo Thông Báo Mới";
            la_cap_nhat = false;
            lblMaThongBaoValue.Text = "Tạo mới";
        }

        // Constructor cập nhật (nhận Model thật)
        public TaoCapNhatThongBao(ThongBao tb)
        {
            InitializeComponent();
            this.Text = "Cập Nhật Thông Báo";
            ThongBaoHienTai = tb;
            la_cap_nhat = true;
            NapDuLieu(tb);
        }

        // PHƯƠNG THỨC SỬA LỖI: Đã chuyển thành PUBLIC
       public void NapDuLieu(ThongBao tb)
        {
            if (tb == null) return;

            // Gán entity hiện tại để form sửa trực tiếp lên object đang được theo dõi
            this.ThongBaoHienTai = tb;
            this.la_cap_nhat = true;

            lblMaThongBaoValue.Text = tb.MaThongBao.ToString();
            txtTieuDe.Text = tb.TieuDe;
            txtNoiDung.Text = tb.NoiDung;

            // Hiển thị MaAdmin (người tạo) và khóa trường này
            txtRole.Text = tb.MaAdmin.HasValue ? tb.MaAdmin.ToString() : "N/A";
            txtRole.Enabled = false;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTieuDe.Text) || string.IsNullOrWhiteSpace(txtNoiDung.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ Tiêu đề và Nội dung.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (la_cap_nhat && ThongBaoHienTai != null)
            {
                // cập nhật chính object đã được gán bởi NapDuLieu
                ThongBaoHienTai.TieuDe = txtTieuDe.Text;
                ThongBaoHienTai.NoiDung = txtNoiDung.Text;
            }
            else
            {
                // Tạo mới
                ThongBaoHienTai = new ThongBao
                {
                    TieuDe = txtTieuDe.Text,
                    NoiDung = txtNoiDung.Text,
                    MaAdmin = 1 // hoặc lấy ID admin hiện tại
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