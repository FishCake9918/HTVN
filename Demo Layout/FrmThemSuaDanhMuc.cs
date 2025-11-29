using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic; // Cần cho List
using Microsoft.EntityFrameworkCore;
using Data; // Cần thiết cho IDbContextFactory
using System.Drawing; // Cần thiết cho Label

namespace Demo_Layout
{
    public partial class FrmThemSuaDanhMuc : Form
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly CurrentUserContext _userContext;
        private int? _maDanhMucCanSua = null;


        // 2. Sửa Constructor nhận thêm userContext
        public FrmThemSuaDanhMuc(IDbContextFactory<QLTCCNContext> dbFactory, CurrentUserContext userContext)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _userContext = userContext;

            // THÊM: Đặt tiêu đề mặc định cho lblForm khi MỚI
            if (lblForm != null)
            {
                lblForm.Text = "THÊM DANH MỤC";
            }
            this.Text = "Thêm Danh mục mới";

            // Đăng ký sự kiện Load cho Form
            this.Load += frmThemDanhMuc_Load;
            // Đăng ký sự kiện cho các nút
            this.btnLuu.Click += btnLuu_Click;
            this.btnHuy.Click += btnHuy_Click;
        }

        public void CheDoSua(int maDanhMuc)
        {
            _maDanhMucCanSua = maDanhMuc;

            // CẬP NHẬT: Đặt tiêu đề cho lblForm khi SỬA
            if (lblForm != null)
            {
                lblForm.Text = "SỬA DANH MỤC";
            }
            this.Text = "Cập nhật Danh mục"; // Đổi tên Form
            btnLuu.Text = "Cập nhật";       // Đổi tên nút

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
                    // Đảm bảo MaNguoiDung không null trước khi sử dụng
                    if (_userContext.MaNguoiDung == null)
                    {
                        MessageBox.Show("Lỗi: Không xác định được người dùng hiện tại.");
                        return;
                    }
                    var currentUserId = _userContext.MaNguoiDung.Value;

                    // Lấy tất cả danh mục GỐC (DanhMucCha == null) của người dùng hiện tại
                    var danhSachCha = db.DanhMucChiTieus
                                             .Where(dm => dm.MaNguoiDung == currentUserId && dm.DanhMucCha == null && dm.MaDanhMuc != excludeId)
                                             .Select(dm => new { dm.MaDanhMuc, dm.TenDanhMuc })
                                             .ToList();

                    // Tạo một danh sách mới để thêm mục "Gốc"
                    var dataSource = new List<object>
                    {
                        // Mục này đại diện cho giá trị NULL (hoặc 0)
                        new { MaDanhMuc = 0, TenDanhMuc = "(Là danh mục gốc)" }
                    };

                    dataSource.AddRange(danhSachCha);

                    // Gán vào ComboBox
                    cboDanhMucCha.DataSource = dataSource;
                    cboDanhMucCha.DisplayMember = "TenDanhMuc"; // Hiển thị Tên
                    cboDanhMucCha.ValueMember = "MaDanhMuc";   // Lấy giá trị Mã
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
            // 1. Validate cơ bản
            string tenNhapVao = txtTenDanhMuc.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenNhapVao))
            {
                MessageBox.Show("Tên danh mục không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDanhMuc.Focus();
                return;
            }

            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    int currentUserId = _userContext.MaNguoiDung.Value;

                    // [QUAN TRỌNG] 2. KIỂM TRA TRÙNG TÊN (Case-Insensitive)
                    // Logic: Tìm xem có danh mục nào của User này có tên giống tên nhập vào không
                    var danhMucTrung = db.DanhMucChiTieus
                        .Where(dm => dm.MaNguoiDung == currentUserId)
                        .ToList() // Tải về bộ nhớ để so sánh chuỗi chính xác nhất
                        .FirstOrDefault(dm =>
                            string.Equals(dm.TenDanhMuc, tenNhapVao, StringComparison.OrdinalIgnoreCase) // So sánh bỏ qua hoa thường
                        );

                    // Nếu tìm thấy danh mục trùng tên
                    if (danhMucTrung != null)
                    {
                        // Kiểm tra kỹ hơn:
                        // - Nếu đang THÊM MỚI: Cứ trùng là chặn.
                        // - Nếu đang SỬA: Trùng với chính nó thì OK, trùng với đứa khác thì chặn.
                        if (_maDanhMucCanSua == null || (_maDanhMucCanSua != null && danhMucTrung.MaDanhMuc != _maDanhMucCanSua))
                        {
                            MessageBox.Show($"Tên danh mục '{tenNhapVao}' đã tồn tại.\nVui lòng chọn tên khác.",
                                "Trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtTenDanhMuc.SelectAll();
                            txtTenDanhMuc.Focus();
                            return; // Dừng lại, không lưu
                        }
                    }

                    // 3. Tiến hành Lưu nếu không trùng
                    DanhMucChiTieu danhMuc;

                    if (_maDanhMucCanSua == null)
                    {
                        // Thêm mới
                        danhMuc = new DanhMucChiTieu();
                        danhMuc.MaNguoiDung = currentUserId;
                        db.DanhMucChiTieus.Add(danhMuc);
                    }
                    else
                    {
                        // Sửa
                        danhMuc = db.DanhMucChiTieus.Find(_maDanhMucCanSua);
                        if (danhMuc == null)
                        {
                            MessageBox.Show("Danh mục không tồn tại.");
                            return;
                        }
                    }

                    danhMuc.TenDanhMuc = tenNhapVao; // Lưu tên đã trim

                    // Xử lý Danh mục cha
                    int maCha = 0;
                    if (cboDanhMucCha.SelectedValue != null)
                        int.TryParse(cboDanhMucCha.SelectedValue.ToString(), out maCha);

                    // Ngăn chặn việc chọn chính nó làm cha của nó (Logic vòng lặp)
                    if (_maDanhMucCanSua.HasValue && maCha == _maDanhMucCanSua.Value)
                    {
                        MessageBox.Show("Một danh mục không thể là cha của chính nó.", "Lỗi Logic", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    danhMuc.DanhMucCha = (maCha == 0) ? null : maCha;

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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}