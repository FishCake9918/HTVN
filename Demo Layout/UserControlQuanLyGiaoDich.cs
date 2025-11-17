using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo_Layout
{
    public partial class UserControlQuanLyGiaoDich : UserControl
    {
        public UserControlQuanLyGiaoDich()
        {
            InitializeComponent();
        }
        private bool isPlaceholderActive = true; // Biến để theo dõi trạng thái Placeholder

        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            // Sự kiện xảy ra khi click vào TextBox
            if (isPlaceholderActive)
            {
                txtTimKiem.Text = ""; // Xóa chữ "Tìm kiếm..."
                txtTimKiem.ForeColor = Color.Black; // Chuyển sang màu chữ bình thường (ví dụ: Đen)
                isPlaceholderActive = false;
            }
        }
        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            // Sự kiện xảy ra khi rời khỏi TextBox
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                // Nếu không có chữ nào được điền, khôi phục Placeholder
                txtTimKiem.Text = " Tìm kiếm...";
                txtTimKiem.ForeColor = Color.Gray; // Đặt lại màu chữ mờ
                isPlaceholderActive = true;
            }
            // Nếu có chữ, thì giữ nguyên chữ đó và màu Đen.
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ChinhSuaPayee chinhSuaPayee = new ChinhSuaPayee();
            chinhSuaPayee.Show();
        }
    }
}
