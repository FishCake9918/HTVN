using System;
using System.Windows.Forms;

namespace Demo_Layout
{
    public partial class QuanLyTaiKhoan : Form
    {
        private string tenDangNhap = "bichtramxinhdep";
        private string email = "bichtramxinhdep@gmail.com";

        public QuanLyTaiKhoan()
        {
            InitializeComponent();
            NapThongTinTaiKhoan();
        }

        private void NapThongTinTaiKhoan()
        {
            lblTenNguoiDung.Text = "Trần Bích Trâm";
            lblTenDangNhap.Text = "Tên đăng nhập: " + tenDangNhap;
            lblEmail.Text = "Email: " + email;
        }

        private void btnCapNhatMatKhau_Click(object sender, EventArgs e)
        {
            try
            {
                using (DoiMatKhau frm = new DoiMatKhau(tenDangNhap))
                {
                    var kq = frm.ShowDialog(this);
                    if (kq == DialogResult.OK)
                    {
                        string matKhauMoi = frm.MatKhauMoi;
                        MessageBox.Show("Mật khẩu đã được thay đổi thành công!",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form đổi mật khẩu: " + ex.Message);
            }
        }

        private void btnXoaTaiKhoan_Click(object sender, EventArgs e)
        {
            var dr = MessageBox.Show("Bạn chắc chắn muốn xóa tài khoản này?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                MessageBox.Show("Tài khoản đã bị xóa (demo).",
                    "Đã xóa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            var dr = MessageBox.Show("Bạn có chắc muốn đăng xuất?",
                "Đăng xuất", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                MessageBox.Show("Đã đăng xuất (demo).",
                    "Đăng xuất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }
    }
}
