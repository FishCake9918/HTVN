using System;
using System.Windows.Forms;

namespace Demo_Layout
{
    public partial class ChinhSuaPayee : Form
    {
        public ChinhSuaPayee()
        {
            InitializeComponent();
        }

        // --- LOGIC NÚT LƯU ---
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // 1. Ràng buộc (Validation)
            if (string.IsNullOrWhiteSpace(tbPayee.Text))
            {
                MessageBox.Show("Tên đối tượng giao dịch không được để trống.",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                tbPayee.Focus(); // Di chuyển con trỏ tới ô này
                return; // Dừng, không cho lưu
            }

            // (Thêm các ràng buộc khác nếu cần, ví dụ: ComboBox phải được chọn)
            // ...

            // 2. Báo cho form Payee biết là đã "Lưu" thành công
            this.DialogResult = DialogResult.OK;

            // 3. Đóng form ChinhSuaPayee
            this.Close();
        }

        // --- LOGIC NÚT HỦY ---
        private void btnHuy_Click(object sender, EventArgs e)
        {
            // 1. Báo cho form Payee biết là đã "Hủy"
            this.DialogResult = DialogResult.Cancel;

            // 2. Đóng form ChinhSuaPayee
            this.Close();
        }
    }
}