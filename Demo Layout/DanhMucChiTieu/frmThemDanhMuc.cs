//using DanhMucChiTieu.Data; // Using DbContext
//using QLTCCN.Models; // Using Model
using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic; // Cần cho List

namespace Demo_Layout
{
    public partial class frmThemDanhMuc : Form
    {
        // Giả định ID người dùng hiện tại là 1 (Nguyễn Văn An)
        // Trong ứng dụng thật, bạn sẽ lấy ID này từ Form Đăng nhập
        private const int CURRENT_USER_ID = 1;

        public frmThemDanhMuc()
        {
            InitializeComponent();
        }

        private void frmThemDanhMuc_Load(object sender, EventArgs e)
        {
            LoadComboBoxDanhMucCha();
        }

        /// <summary>
        /// Tải danh sách các danh mục hiện có vào ComboBox
        /// </summary>
        private void LoadComboBoxDanhMucCha()
        {
            try
            {
                using (var db = new QLTCCN_DbContext())
                {
                    // Lấy tất cả danh mục của người dùng hiện tại
                    var danhSachCha = db.DANH_MUC_CHI_TIEU
                                        .Where(dm => dm.MaNguoiDung == CURRENT_USER_ID && dm.DanhMucCha ==null)
                                        .Select(dm => new { dm.MaDanhMuc, dm.TenDanhMuc })
                                        .ToList();

                    // Tạo một danh sách mới để thêm mục "Gốc"
                    var dataSource = new List<object>
                    {
                        // Mục này đại diện cho giá trị NULL
                        new { MaDanhMuc = 0, TenDanhMuc = "(Là danh mục gốc)" }
                    };

                    dataSource.AddRange(danhSachCha);

                    // Gán vào ComboBox
                    cboDanhMucCha.DataSource = dataSource;
                    cboDanhMucCha.DisplayMember = "TenDanhMuc"; // Hiển thị Tên
                    cboDanhMucCha.ValueMember = "MaDanhMuc";   // Lấy giá trị Mã
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh mục cha: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xử lý khi nhấn nút Lưu
        /// </summary>
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra dữ liệu (Validation)
            if (string.IsNullOrWhiteSpace(txtTenDanhMuc.Text))
            {
                MessageBox.Show("Tên danh mục không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDanhMuc.Focus();
                return;
            }

            try
            {
                // 2. Tạo đối tượng DanhMuc mới
                var danhMucMoi = new DANH_MUC_CHI_TIEU();
                danhMucMoi.TenDanhMuc = txtTenDanhMuc.Text.Trim();
                danhMucMoi.MaNguoiDung = CURRENT_USER_ID; // Gán người dùng hiện tại

                // 3. Xử lý DanhMucCha (Nullable int)
                int maChaDaChon = (int)cboDanhMucCha.SelectedValue;

                if (maChaDaChon == 0) // Nếu chọn "(Là danh mục gốc)"
                {
                    danhMucMoi.DanhMucCha = null;
                }
                else
                {
                    danhMucMoi.DanhMucCha = maChaDaChon;
                }

                // 4. Lưu vào CSDL bằng EF Core
                using (var db = new QLTCCN_DbContext())
                {
                    db.DANH_MUC_CHI_TIEU.Add(danhMucMoi);
                    db.SaveChanges(); // Lưu thay đổi
                }

                MessageBox.Show("Thêm danh mục mới thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Đặt kết quả để Form cha biết
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu vào CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}