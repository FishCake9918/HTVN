using System;
using System.Windows.Forms;

namespace Demo_Layout
{
    public partial class DoiMatKhau : Form
    {
        public string TenDangNhap { get; private set; }
        public string MatKhauMoi { get; private set; }

        public DoiMatKhau(string tenDangNhap)
        {
            TenDangNhap = tenDangNhap;
            InitializeComponent();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu nhập vào
            if (string.IsNullOrWhiteSpace(txtMatKhauCu.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu hiện tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauCu.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtMatKhauMoi.Text) || txtMatKhauMoi.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauMoi.Focus();
                return;
            }

            if (txtMatKhauMoi.Text != txtXacNhanMatKhau.Text)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtXacNhanMatKhau.Focus();
                return;
            }

            // Giả lập xác thực mật khẩu cũ 
            MatKhauMoi = txtMatKhauMoi.Text;
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
