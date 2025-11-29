using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Data; // Data models
using Microsoft.Extensions.DependencyInjection;

namespace Demo_Layout
{
    public partial class FrmThemGiaoDich : Form
    {
        public Action OnDataAdded; // Action thông báo dữ liệu thay đổi
        private int? _maGiaoDich = null; // ID giao dịch (null = Thêm mới)
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory; // Factory tạo DbContext
        private readonly IServiceProvider _serviceProvider; // Service provider cho DI
        private readonly CurrentUserContext _userContext; // Context người dùng hiện tại

        // --- CONSTRUCTOR 1: THÊM MỚI ---
        public FrmThemGiaoDich(
            IDbContextFactory<QLTCCNContext> dbFactory,
            IServiceProvider serviceProvider,
            CurrentUserContext userContext) // DI: Tiêm các services
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
            _userContext = userContext; // Gán User Context
            _maGiaoDich = null; // Chế độ Thêm Mới
            this.Load += FrmThemGiaoDich_Load;
            this.Text = "Thêm Giao Dịch Mới";
            lblForm.Text = "THÊM GIAO DỊCH";
        }

        // --- CONSTRUCTOR 2: SỬA ---
        public FrmThemGiaoDich(
            IDbContextFactory<QLTCCNContext> dbFactory,
            IServiceProvider serviceProvider,
            CurrentUserContext userContext, // DI: Tiêm User Context
            int maGiaoDich,
            string tenGiaoDich,
            string ghiChu,
            decimal soTien,
            DateTime ngayGiaoDich,
            int maDoiTuong,
            int maTaiKhoan) // Tham số cho chế độ Sửa
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
            _userContext = userContext; // Gán User Context
            _maGiaoDich = maGiaoDich; // Chế độ Sửa
            this.Load += FrmThemGiaoDich_Load;
            this.Text = "Cập Nhật Giao Dịch";
            lblForm.Text = "SỬA GIAO DỊCH";
        }

        private void FrmThemGiaoDich_Load(object sender, EventArgs e)
        {
            // Kiểm tra bảo mật
            if (_userContext.MaNguoiDung == null)
            {
                MessageBox.Show("Lỗi: Không xác định được người dùng hiện tại.");
                this.Close();
                return;
            }
            rtbGhiChu.Multiline = true;
            rtbGhiChu.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbGhiChu.WordWrap = true;

            LoadComboBoxes(); // Tải dữ liệu cho các ComboBox

            if (_maGiaoDich != null)
            {
                LoadDataForEdit(_maGiaoDich.Value); // Tải dữ liệu để sửa
            }
            else
            {
                radChi.Checked = true; // Mặc định là Chi
                dtNgayGiaoDich.Value = DateTime.Now;
            }
        }

        // Tải dữ liệu ComboBox từ DB
        private void LoadComboBoxes()
        {
            object currentDoiTuong = cbDoiTuong.SelectedValue; // Lưu giá trị cũ
            object currentDanhMuc = cbDanhMuc.SelectedValue; // Lưu giá trị cũ
            try
            {
                using (var context = _dbFactory.CreateDbContext())
                {
                    int currentUserId = _userContext.MaNguoiDung.Value;

                    // 1. Load Đối Tượng (Lọc theo User)
                    var dsDoiTuong = context.DoiTuongGiaoDichs
                        .Where(dt => dt.MaNguoiDung == currentUserId)
                        .Select(dt => new { dt.MaDoiTuongGiaoDich, dt.TenDoiTuong })
                        .ToList();

                    cbDoiTuong.DataSource = dsDoiTuong;
                    cbDoiTuong.DisplayMember = "TenDoiTuong";
                    cbDoiTuong.ValueMember = "MaDoiTuongGiaoDich";

                    // 2. Load Tài Khoản (Lọc theo User và trạng thái)
                    var dsTaiKhoan = context.TaiKhoanThanhToans
                        .Where(tk => tk.MaNguoiDung == currentUserId && tk.TrangThai == "Đang hoạt động")
                        .Select(tk => new { tk.MaTaiKhoanThanhToan, tk.TenTaiKhoan })
                        .ToList();

                    cbTaiKhoan.DataSource = dsTaiKhoan;
                    cbTaiKhoan.DisplayMember = "TenTaiKhoan";
                    cbTaiKhoan.ValueMember = "MaTaiKhoanThanhToan";

                    // 3. Load Danh Mục (Lọc theo User, có Danh Mục Cha)
                    var dsDanhMuc = context.DanhMucChiTieus
                        .Include(dm => dm.DanhMucChaNavigation) // Lấy tên Danh Mục Cha
                        .Where(dm => dm.MaNguoiDung == currentUserId && dm.DanhMucCha != null)
                        .Select(dm => new
                        {
                            dm.MaDanhMuc,
                            TenHienThi = dm.DanhMucChaNavigation.TenDanhMuc + " - " + dm.TenDanhMuc // Format tên
                        })
                        .OrderBy(dm => dm.TenHienThi)
                        .ToList();

                    cbDanhMuc.DataSource = dsDanhMuc;
                    cbDanhMuc.DisplayMember = "TenHienThi";
                    cbDanhMuc.ValueMember = "MaDanhMuc";
                    cbDanhMuc.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách chọn: " + ex.Message);
            }

            // Gán lại giá trị cũ
            if (currentDoiTuong != null && cbDoiTuong.Items.Cast<dynamic>().Any(x => x.MaDoiTuongGiaoDich == (int)currentDoiTuong))
                cbDoiTuong.SelectedValue = currentDoiTuong;

            if (currentDanhMuc != null && cbDanhMuc.Items.Cast<dynamic>().Any(x => x.MaDanhMuc == (int)currentDanhMuc))
                cbDanhMuc.SelectedValue = currentDanhMuc;
        }

        // Tải dữ liệu cũ khi Sửa
        private void LoadDataForEdit(int maGiaoDich)
        {
            try
            {
                using (var context = _dbFactory.CreateDbContext())
                {
                    var gd = context.GiaoDichs.Find(maGiaoDich);
                    if (gd != null)
                    {
                        // Kiểm tra quyền sở hữu giao dịch
                        if (gd.MaNguoiDung != _userContext.MaNguoiDung)
                        {
                            MessageBox.Show("Bạn không có quyền sửa giao dịch này.");
                            this.Close();
                            return;
                        }

                        txtTenGiaoDich.Text = gd.TenGiaoDich;
                        rtbGhiChu.Text = gd.GhiChu;
                        txtSoTien.Text = gd.SoTien.ToString("N0"); // Format số tiền
                        dtNgayGiaoDich.Value = gd.NgayGiaoDich;

                        // Gán SelectedValue cho ComboBoxes
                        if (gd.MaDoiTuongGiaoDich.HasValue) cbDoiTuong.SelectedValue = gd.MaDoiTuongGiaoDich;
                        if (gd.MaTaiKhoanThanhToan.HasValue) cbTaiKhoan.SelectedValue = gd.MaTaiKhoanThanhToan;
                        if (gd.MaDanhMuc.HasValue) cbDanhMuc.SelectedValue = gd.MaDanhMuc;

                        // Chọn Radio Button
                        if (gd.MaLoaiGiaoDich == 1) radThu.Checked = true;
                        else radChi.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu cũ: " + ex.Message);
            }
        }

        // Sự kiện click nút Lưu (Thêm/Sửa)
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Validate Tên
            if (string.IsNullOrWhiteSpace(txtTenGiaoDich.Text))
            {
                MessageBox.Show("Vui lòng nhập tên giao dịch.");
                return;
            }

            // Validate và Parse Số Tiền
            string soTienClean = txtSoTien.Text.Replace(",", "").Replace(".", "");
            decimal soTien;
            if (!decimal.TryParse(soTienClean, out soTien))
            {
                MessageBox.Show("Số tiền không hợp lệ.");
                return;
            }

            // Validate Số Tiền âm
            if (soTien < 0)
            {
                MessageBox.Show("Số tiền không được là số âm.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoTien.Focus();
                return;
            }

            int maLoaiGD = radThu.Checked ? 1 : 2; // Xác định loại GD (1: Thu, 2: Chi)

            try
            {
                using (var context = _dbFactory.CreateDbContext())
                {
                    if (_maGiaoDich == null)
                    {
                        // --- THÊM MỚI ---
                        var gd = new GiaoDich
                        {
                            TenGiaoDich = txtTenGiaoDich.Text,
                            GhiChu = rtbGhiChu.Text,
                            SoTien = soTien,
                            NgayGiaoDich = dtNgayGiaoDich.Value,
                            MaNguoiDung = _userContext.MaNguoiDung.Value, // Gán ID người dùng
                            MaLoaiGiaoDich = maLoaiGD,
                            MaDoiTuongGiaoDich = cbDoiTuong.SelectedValue as int?, // Lấy ID Đối Tượng
                            MaTaiKhoanThanhToan = cbTaiKhoan.SelectedValue as int?, // Lấy ID Tài Khoản
                            MaDanhMuc = cbDanhMuc.SelectedValue as int? // Lấy ID Danh Mục
                        };

                        context.GiaoDichs.Add(gd);
                        context.SaveChanges();
                        MessageBox.Show("Thêm giao dịch thành công!", "Thông báo");
                    }
                    else
                    {
                        // --- CẬP NHẬT ---
                        var gd = context.GiaoDichs.Find(_maGiaoDich.Value);
                        if (gd != null)
                        {
                            if (gd.MaNguoiDung != _userContext.MaNguoiDung) // Kiểm tra quyền sở hữu
                            {
                                MessageBox.Show("Không có quyền sửa giao dịch này.");
                                return;
                            }
                            // Cập nhật các trường
                            gd.TenGiaoDich = txtTenGiaoDich.Text;
                            gd.GhiChu = rtbGhiChu.Text;
                            gd.SoTien = soTien;
                            gd.NgayGiaoDich = dtNgayGiaoDich.Value;
                            gd.MaLoaiGiaoDich = maLoaiGD;
                            gd.MaDoiTuongGiaoDich = cbDoiTuong.SelectedValue as int?;
                            gd.MaTaiKhoanThanhToan = cbTaiKhoan.SelectedValue as int?;
                            gd.MaDanhMuc = cbDanhMuc.SelectedValue as int?;

                            context.SaveChanges();
                            MessageBox.Show("Cập nhật giao dịch thành công!", "Thông báo");
                        }
                    }
                }

                OnDataAdded?.Invoke();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form
        }

        private void radChi_CheckedChanged(object sender, EventArgs e) { } 
        private void radThu_CheckedChanged(object sender, EventArgs e) { }     
    }
}