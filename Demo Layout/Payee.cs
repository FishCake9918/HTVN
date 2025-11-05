using System;
using System.Windows.Forms;

namespace Demo_Layout
{
    public partial class Payee : Form
    {
        public Payee()
        {
            InitializeComponent();

            // Cài đặt này giúp người dùng dễ chọn hàng
            dtgPayee.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgPayee.MultiSelect = false;
        }

        // --- LOGIC TRA CỨU ---
        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            string keyword = tbTraCuu.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập từ khóa",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Logic giả định khi không tìm thấy
            MessageBox.Show("Không tìm thấy đối tượng giao dịch, vui lòng thử lại.",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            // Nếu tìm thấy, bạn sẽ đổ data vào dtgPayee ở đây
            // dtgPayee.DataSource = ...
        }

        // --- LOGIC NÚT THÊM ---
        private void btnThemPayee_Click(object sender, EventArgs e)
        {
            // 1. Tạo mới form ChinhSuaPayee
            ChinhSuaPayee formThem = new ChinhSuaPayee();

            // 2. Hiển thị form đó và chờ cho đến khi nó đóng
            // ShowDialog() sẽ khóa form Payee lại
            DialogResult result = formThem.ShowDialog();

            // 3. Kiểm tra xem người dùng đã bấm "Lưu" trên form đó hay không
            if (result == DialogResult.OK)
            {
                MessageBox.Show("Đã thêm đối tượng mới thành công!", "Thông báo");
                // Gọi hàm tải lại DataGridView của bạn ở đây
                // LoadData(); 
            }
        }

        // --- LOGIC NÚT SỬA ---
        private void btnSuaPayee_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem người dùng đã chọn hàng nào trong DataGridView chưa
            if (dtgPayee.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đối tượng cần chỉnh sửa",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return; // Dừng lại
            }

            // 2. Nếu đã chọn, mở form ChinhSuaPayee
            ChinhSuaPayee formSua = new ChinhSuaPayee();

            // (Bạn sẽ truyền dữ liệu của hàng đã chọn vào formSua ở đây)
            // vi_du: formSua.LoadDataForEdit(...);

            // 3. Hiển thị form và chờ
            DialogResult result = formSua.ShowDialog();

            // 4. Nếu người dùng bấm "Lưu"
            if (result == DialogResult.OK)
            {
                MessageBox.Show("Đã cập nhật đối tượng thành công!", "Thông báo");
                // Gọi hàm tải lại DataGridView của bạn ở đây
                // LoadData();
            }
        }

        // --- LOGIC NÚT XÓA ---
        private void btnXoaPayee_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem người dùng đã chọn hàng nào chưa
            if (dtgPayee.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đối tượng cần xóa",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // 2. Hiển thị hộp thoại xác nhận
            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa đối tượng này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            // 3. Nếu người dùng chọn "Yes"
            if (confirm == DialogResult.Yes)
            {
                // Thực hiện logic xóa ở đây
                // ...
                MessageBox.Show("Đã xóa thành công!", "Thông báo");
                // Gọi hàm tải lại DataGridView của bạn ở đây
                // LoadData();
            }
            // Nếu người dùng chọn "No" thì không làm gì cả
        }
    }
}