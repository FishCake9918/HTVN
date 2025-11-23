using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic; // Cần cho List
using Microsoft.EntityFrameworkCore;
using Data; // Cần thiết cho IDbContextFactory

namespace Demo_Layout
{
    public partial class frmThemDanhMuc : Form
    {
        // Giả định ID người dùng hiện tại là 1 (Nguyễn Văn An)
        // Trong ứng dụng thật, bạn sẽ lấy ID này từ Form Đăng nhập
        private const int CURRENT_USER_ID = 1;
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider;

        // Biến lưu trạng thái: Nếu null => Thêm mới, Nếu có số => Sửa
        private int? _maDanhMucCanSua = null;

        public frmThemDanhMuc(IDbContextFactory<QLTCCNContext> dbFactory)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
        }

        public void CheDoSua(int maDanhMuc)
        {
            _maDanhMucCanSua = maDanhMuc;
            this.Text = "Cập nhật Danh mục"; // Đổi tên Form
            btnLuu.Text = "Cập nhật";       // Đổi tên nút

            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    var dm = db.DanhMucChiTieus.Find(maDanhMuc);
                    if (dm != null)
                    {
                        txtTenDanhMuc.Text = dm.TenDanhMuc;

                        // Load ComboBox trước để gán giá trị
                        LoadComboBoxDanhMucCha(dm.MaDanhMuc); // Truyền ID hiện tại để tránh chọn chính nó làm cha

                        // Gán giá trị cha hiện tại
                        cboDanhMucCha.SelectedValue = dm.DanhMucCha ?? 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void frmThemDanhMuc_Load(object sender, EventArgs e)
        {
            if (_maDanhMucCanSua == null) // Chỉ load mặc định nếu đang là Thêm mới
            {
                LoadComboBoxDanhMucCha(null);
            }
        }

        /// <summary>
        /// Tải danh sách các danh mục hiện có vào ComboBox
        /// </summary>
        private void LoadComboBoxDanhMucCha(int? excludeId)
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    // Lấy tất cả danh mục của người dùng hiện tại
                    var danhSachCha = db.DanhMucChiTieus
                                        .Where(dm => dm.MaNguoiDung == CURRENT_USER_ID && dm.DanhMucCha == null)
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
            if (string.IsNullOrWhiteSpace(txtTenDanhMuc.Text))
            {
                MessageBox.Show("Tên danh mục không được để trống.");
                return;
            }

            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    DanhMucChiTieu danhMuc;

                    // 1. LOGIC PHÂN BIỆT THÊM / SỬA
                    if (_maDanhMucCanSua == null)
                    {
                        // Chế độ THÊM
                        danhMuc = new DanhMucChiTieu();
                        danhMuc.MaNguoiDung = CURRENT_USER_ID;
                        db.DanhMucChiTieus.Add(danhMuc);
                    }
                    else
                    {
                        // Chế độ SỬA
                        danhMuc = db.DanhMucChiTieus.Find(_maDanhMucCanSua);
                        if (danhMuc == null)
                        {
                            MessageBox.Show("Danh mục này không còn tồn tại.");
                            return;
                        }
                    }

                    // 2. Cập nhật thông tin chung
                    danhMuc.TenDanhMuc = txtTenDanhMuc.Text.Trim();

                    int maCha = 0;
                    if (cboDanhMucCha.SelectedValue != null)
                        int.TryParse(cboDanhMucCha.SelectedValue.ToString(), out maCha);

                    danhMuc.DanhMucCha = (maCha == 0) ? null : maCha;

                    // 3. Lưu
                    db.SaveChanges();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}


