using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Đảm bảo namespace khớp với designer của bạn
namespace Demo_Layout
{
    // Lưu ý: Tên class là ChinhSuaPayee theo designer mới
    public partial class ChinhSuaPayee : Form
    {
        // Biến này để lưu đối tượng đang được chỉnh sửa
        // Nếu là 'null', có nghĩa là đang ở chế độ "Thêm mới"
        private PayeeData _editingPayee;

        public ChinhSuaPayee(PayeeData payeeToEdit)
        {
            InitializeComponent();

            _editingPayee = payeeToEdit;

            // Gán sự kiện cho các nút (vì designer của bạn chưa gán)
            this.Load += ChinhSuaPayee_Load;
            this.btnLuu.Click += btnLuu_Click;
            this.btnHuy.Click += btnHuy_Click;
        }

        private void ChinhSuaPayee_Load(object sender, EventArgs e)
        {
            // Tải danh sách các danh mục *đã có* vào ComboBox
            LoadCategories();

            if (_editingPayee == null)
            {
                // Chế độ THÊM MỚI
                this.Text = "Thêm Đối Tượng Giao Dịch";
                // Xóa trống ComboBox để người dùng có thể gõ mới
                cbDanhMuc.Text = "";
            }
            else
            {
                // Chế độ SỬA
                this.Text = "Sửa Đối Tượng Giao Dịch";
                // Điền thông tin cũ vào form
                tbPayee.Text = _editingPayee.TenDoiTuong;
                cbDanhMuc.Text = _editingPayee.DanhMuc; // Gán Text để hiển thị kể cả khi không có trong list
                tbNote.Text = _editingPayee.GhiChu;
            }
        }

        // RULE 1 (Form Edit): Tải danh mục có sẵn
        private void LoadCategories()
        {
            // Lấy tất cả các danh mục *duy nhất* và *không rỗng* từ "database"
            var existingCategories = Payee.DanhSachPayees
                .Select(p => p.DanhMuc)
                .Where(dm => !string.IsNullOrEmpty(dm))
                .Distinct()
                .ToList();

            cbDanhMuc.DataSource = existingCategories;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            // Đặt kết quả là Cancel và đóng form
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các controls
            string tenDoiTuong = tbPayee.Text.Trim();
            string danhMuc = cbDanhMuc.Text.Trim(); // Lấy Text chứ không phải SelectedItem
            string ghiChu = tbNote.Text.Trim();

            // --- Validation 1: Tên không được rỗng ---
            if (string.IsNullOrEmpty(tenDoiTuong))
            {
                MessageBox.Show("Tên đối tượng không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbPayee.Focus();
                return;
            }

            // --- RULE 2 (Form Edit): Kiểm tra trùng tên (không phân biệt hoa thường) ---
            bool isDuplicate;
            if (_editingPayee == null)
            {
                // Chế độ THÊM: Kiểm tra toàn bộ danh sách
                isDuplicate = Payee.DanhSachPayees.Any(p =>
                    p.TenDoiTuong.Equals(tenDoiTuong, StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                // Chế độ SỬA: Kiểm tra toàn bộ danh sách *ngoại trừ* chính nó
                isDuplicate = Payee.DanhSachPayees.Any(p =>
                    p.Id != _editingPayee.Id && // Đây là điểm khác biệt
                    p.TenDoiTuong.Equals(tenDoiTuong, StringComparison.OrdinalIgnoreCase));
            }

            if (isDuplicate)
            {
                MessageBox.Show("Đã tồn tại đối tượng này. Vui lòng nhập lại.", "Trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbPayee.Focus();
                return;
            }

            // --- RULE 3 (Form Edit): Kiểm tra danh mục đã tồn tại ở đối tượng khác ---
            // Yêu cầu này chỉ thực hiện nếu người dùng có nhập/chọn danh mục
            if (!string.IsNullOrEmpty(danhMuc))
            {
                // Tìm một đối tượng *khác* đang sử dụng danh mục này
                PayeeData otherPayee = null;
                if (_editingPayee == null)
                {
                    // Chế độ THÊM: Tìm bất kỳ ai dùng danh mục này
                    otherPayee = Payee.DanhSachPayees.FirstOrDefault(p =>
                        p.DanhMuc.Equals(danhMuc, StringComparison.OrdinalIgnoreCase));
                }
                else
                {
                    // Chế độ SỬA: Tìm bất kỳ ai *khác* dùng danh mục này
                    otherPayee = Payee.DanhSachPayees.FirstOrDefault(p =>
                        p.Id != _editingPayee.Id &&
                        p.DanhMuc.Equals(danhMuc, StringComparison.OrdinalIgnoreCase));
                }

                // Nếu tìm thấy một đối tượng khác...
                if (otherPayee != null)
                {
                    DialogResult confirm = MessageBox.Show(
                        $"Danh mục '{danhMuc}' đã tồn tại trong đối tượng '{otherPayee.TenDoiTuong}'.\n" +
                        $"Bạn có chắc chắn muốn thêm danh mục này vào đối tượng '{tenDoiTuong}' không?",
                        "Xác nhận danh mục",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    // Nếu người dùng chọn "Không", dừng việc lưu lại
                    if (confirm == DialogResult.No)
                    {
                        return; // Quay lại form, không đóng
                    }
                }
            }

            // --- Nếu tất cả validation đều qua, tiến hành LƯU ---
            if (_editingPayee == null)
            {
                // Chế độ THÊM: Tạo đối tượng mới và thêm vào "database"
                int newId = (Payee.DanhSachPayees.Count > 0) ? Payee.DanhSachPayees.Max(p => p.Id) + 1 : 1;

                PayeeData newPayee = new PayeeData
                {
                    Id = newId,
                    TenDoiTuong = tenDoiTuong,
                    DanhMuc = danhMuc,
                    GhiChu = ghiChu
                };
                Payee.DanhSachPayees.Add(newPayee);
            }
            else
            {
                // Chế độ SỬA: Cập nhật đối tượng *đang có* trong "database"
                _editingPayee.TenDoiTuong = tenDoiTuong;
                _editingPayee.DanhMuc = danhMuc;
                _editingPayee.GhiChu = ghiChu;
            }

            // Đặt kết quả là OK và đóng form
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}