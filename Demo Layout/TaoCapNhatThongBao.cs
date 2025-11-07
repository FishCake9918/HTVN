using System;
using System.Drawing; 
using System.Windows.Forms;

namespace Demo_Layout
{
    public partial class TaoCapNhatThongBao : Form
    {
        public ThongBao ThongBaoHienTai { get; private set; }
        private bool la_cap_nhat;
        public TaoCapNhatThongBao()
        {
            InitializeComponent();
            this.Text = "Tạo Thông Báo Mới";
            la_cap_nhat = false;
            lblMaThongBaoValue.Text = "Tạo mới";
        }
        public TaoCapNhatThongBao(ThongBao tb)
        {
            InitializeComponent();
            this.Text = "Cập Nhật Thông Báo";
            ThongBaoHienTai = tb;
            la_cap_nhat = true;
            NapDuLieu(tb);
        }

        private void NapDuLieu(ThongBao tb)
        {
            lblMaThongBaoValue.Text = tb.MaThongBao.ToString();
            txtTieuDe.Text = tb.TieuDe;
            txtNoiDung.Text = tb.NoiDung;
            txtRole.Text = tb.RoleTao; 
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTieuDe.Text) || string.IsNullOrWhiteSpace(txtNoiDung.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ Tiêu đề và Nội dung.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo/Cập nhật đối tượng ThongBao
            if (!la_cap_nhat)
            {
                // Tạo mới
                ThongBaoHienTai = new ThongBao
                {
                    TieuDe = txtTieuDe.Text,
                    NoiDung = txtNoiDung.Text,
                    RoleTao = txtRole.Text 
                };
            }
            else
            {
                // Cập nhật 
                ThongBaoHienTai.TieuDe = txtTieuDe.Text;
                ThongBaoHienTai.NoiDung = txtNoiDung.Text;
                ThongBaoHienTai.RoleTao = txtRole.Text; 
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