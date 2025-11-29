using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Data;
using System.Drawing;

namespace Demo_Layout
{
    public partial class FrmThemSuaDanhMuc : Form
    {
        // ==================================================================================
        // 1. KHAI BÁO BIẾN & TRẠNG THÁI FORM
        // ==================================================================================
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory; // Nhà máy tạo kết nối
        private readonly CurrentUserContext _userContext;             // Thông tin người dùng hiện tại

        // Biến quan trọng: 
        // - Nếu null: Form đang ở chế độ THÊM MỚI
        // - Nếu có giá trị (int): Form đang ở chế độ SỬA (lưu ID của danh mục đang sửa)
        private int? _maDanhMucCanSua = null;

        // Constructor nhận các dependency (Dependency Injection)
        public FrmThemSuaDanhMuc(IDbContextFactory<QLTCCNContext> dbFactory, CurrentUserContext userContext)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _userContext = userContext;

            // Mặc định giao diện là "THÊM MỚI"
            if (lblForm != null)
            {
                lblForm.Text = "THÊM DANH MỤC";
            }
            this.Text = "Thêm Danh mục mới";

            // Đăng ký sự kiện (Event wiring)
            this.Load += frmThemDanhMuc_Load;
            this.btnLuu.Click += btnLuu_Click;
            this.btnHuy.Click += btnHuy_Click;
        }

        // ==================================================================================
        // 2. CHUYỂN ĐỔI CHẾ ĐỘ: TỪ THÊM -> SỬA
        // ==================================================================================
        public void CheDoSua(int maDanhMuc)
        {
            _maDanhMucCanSua = maDanhMuc; // Gán ID để hệ thống biết đang sửa ai

            // Cập nhật lại giao diện (Tiêu đề, Tên nút)
            if (lblForm != null)
            {
                lblForm.Text = "SỬA DANH MỤC";
            }
            this.Text = "Cập nhật Danh mục";
            btnLuu.Text = "Cập nhật";

            // Tải dữ liệu cũ lên Form để người dùng nhìn thấy
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    var dm = db.DanhMucChiTieus.Find(maDanhMuc);
                    if (dm != null)
                    {
                        txtTenDanhMuc.Text = dm.TenDanhMuc;

                        // Load ComboBox danh mục cha
                        // Quan trọng: Truyền ID hiện tại vào để loại trừ (tránh chọn chính mình làm cha)
                        LoadComboBoxDanhMucCha(dm.MaDanhMuc);

                        // Chọn đúng cha hiện tại của nó (nếu không có cha thì chọn 0 - Gốc)
                        cboDanhMucCha.SelectedValue = dm.DanhMucCha ?? 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // ==================================================================================
        // 3. LOAD FORM & DỮ LIỆU COMBOBOX
        // ==================================================================================
        private void frmThemDanhMuc_Load(object sender, EventArgs e)
        {
            // Nếu đang là Thêm mới thì load ComboBox bình thường (không cần loại trừ ai)
            if (_maDanhMucCanSua == null)
            {
                LoadComboBoxDanhMucCha(null);
            }
        }

        /// <summary>
        /// Tải danh sách các danh mục có thể làm Cha vào ComboBox
        /// </summary>
        /// <param name="excludeId">ID cần loại bỏ (dùng khi Sửa để tránh vòng lặp cha-con)</param>
        private void LoadComboBoxDanhMucCha(int? excludeId)
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    if (_userContext.MaNguoiDung == null)
                    {
                        MessageBox.Show("Lỗi: Không xác định được người dùng hiện tại.");
                        return;
                    }
                    var currentUserId = _userContext.MaNguoiDung.Value;

                    // Logic lọc:
                    // 1. Phải là của User hiện tại
                    // 2. Phải là danh mục Gốc (Cha == null) -> Chỉ cho phép 2 cấp (Cha -> Con)
                    // 3. Không được là chính nó (excludeId)
                    var danhSachCha = db.DanhMucChiTieus
                                         .Where(dm => dm.MaNguoiDung == currentUserId && dm.DanhMucCha == null && dm.MaDanhMuc != excludeId)
                                         .Select(dm => new { dm.MaDanhMuc, dm.TenDanhMuc })
                                         .ToList();

                    // Thêm tùy chọn "Là danh mục gốc" vào đầu danh sách
                    var dataSource = new List<object>
                    {
                        new { MaDanhMuc = 0, TenDanhMuc = "(Là danh mục gốc)" }
                    };

                    dataSource.AddRange(danhSachCha);

                    // Đổ dữ liệu vào ComboBox
                    cboDanhMucCha.DataSource = dataSource;
                    cboDanhMucCha.DisplayMember = "TenDanhMuc"; // Hiển thị tên
                    cboDanhMucCha.ValueMember = "MaDanhMuc";    // Giá trị ngầm là ID
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh mục cha: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================================================================================
        // 4. XỬ LÝ LƯU (THÊM HOẶC CẬP NHẬT)
        // ==================================================================================

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // BƯỚC 1: Validate dữ liệu đầu vào (Không được để trống tên)
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

                    // BƯỚC 2: KIỂM TRA TRÙNG TÊN (QUAN TRỌNG)
                    // Logic: Tìm xem User này đã có danh mục nào tên giống vậy chưa?
                    var danhMucTrung = db.DanhMucChiTieus
                        .Where(dm => dm.MaNguoiDung == currentUserId)
                        .ToList() 
                        .FirstOrDefault(dm =>
                            string.Equals(dm.TenDanhMuc, tenNhapVao, StringComparison.OrdinalIgnoreCase) // Bỏ qua hoa/thường
                        );

                    // Nếu tìm thấy tên trùng
                    if (danhMucTrung != null)
                    {
                        // TH1: Đang Thêm mới -> Chặn luôn.
                        // TH2: Đang Sửa -> Nếu trùng với tên của người khác thì chặn. (Trùng với chính nó thì OK)
                        if (_maDanhMucCanSua == null || (_maDanhMucCanSua != null && danhMucTrung.MaDanhMuc != _maDanhMucCanSua))
                        {
                            MessageBox.Show($"Tên danh mục '{tenNhapVao}' đã tồn tại.\nVui lòng chọn tên khác.",
                                "Trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtTenDanhMuc.SelectAll();
                            txtTenDanhMuc.Focus();
                            return; // Dừng, không lưu
                        }
                    }

                    // BƯỚC 3: TIẾN HÀNH LƯU
                    DanhMucChiTieu danhMuc;

                    if (_maDanhMucCanSua == null)
                    {
                        // --- Logic THÊM MỚI ---
                        danhMuc = new DanhMucChiTieu();
                        danhMuc.MaNguoiDung = currentUserId;
                        db.DanhMucChiTieus.Add(danhMuc); // Đánh dấu là Add
                    }
                    else
                    {
                        // --- Logic CẬP NHẬT ---
                        danhMuc = db.DanhMucChiTieus.Find(_maDanhMucCanSua);
                        if (danhMuc == null)
                        {
                            MessageBox.Show("Danh mục không tồn tại.");
                            return;
                        }
                    }

                    // Gán các giá trị mới
                    danhMuc.TenDanhMuc = tenNhapVao;

                    // Xử lý Danh mục cha
                    int maCha = 0;
                    if (cboDanhMucCha.SelectedValue != null)
                        int.TryParse(cboDanhMucCha.SelectedValue.ToString(), out maCha);

                    // Kiểm tra an toàn: Không cho phép chọn chính nó làm cha
                    if (_maDanhMucCanSua.HasValue && maCha == _maDanhMucCanSua.Value)
                    {
                        MessageBox.Show("Một danh mục không thể là cha của chính nó.", "Lỗi Logic", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Nếu maCha = 0 tức là chọn "Gốc" -> Lưu null vào DB
                    danhMuc.DanhMucCha = (maCha == 0) ? null : maCha;

                    db.SaveChanges(); // Thực thi xuống SQL
                }

                // Đóng form và báo OK để form cha cập nhật lại cây
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message);
            }
        }

        // Nút Hủy: Đóng form không làm gì cả
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