using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Data; // Namespace chứa QLTCCNContext và Entity
using Microsoft.Extensions.DependencyInjection;

namespace Demo_Layout
{
    public partial class FrmThemGiaoDich : Form
    {
        public Action OnDataAdded;
        private int? _maGiaoDich = null;
        
        // 1. Khai báo biến Inject
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly CurrentUserContext _userContext; 

        // --- CONSTRUCTOR 1: THÊM MỚI (NHẬN DI) ---
        public FrmThemGiaoDich(
            IDbContextFactory<QLTCCNContext> dbFactory, 
            IServiceProvider serviceProvider,
            CurrentUserContext userContext) // <-- Inject vào đây
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
            _userContext = userContext; // Gán giá trị
            _maGiaoDich = null;
            this.Load += FrmThemGiaoDich_Load;
            this.Text = "Thêm Giao Dịch Mới";
        }

        // --- CONSTRUCTOR 2: SỬA (NHẬN DI + THAM SỐ DỮ LIỆU) ---
        public FrmThemGiaoDich(
            IDbContextFactory<QLTCCNContext> dbFactory,
            IServiceProvider serviceProvider,
            CurrentUserContext userContext, // <-- Inject vào đây
            int maGiaoDich,
            string tenGiaoDich,
            string ghiChu,
            decimal soTien,
            DateTime ngayGiaoDich,
            int maDoiTuong,
            int maTaiKhoan)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
            _userContext = userContext; // Gán giá trị
            _maGiaoDich = maGiaoDich;
            this.Load += FrmThemGiaoDich_Load;
            this.Text = "Cập Nhật Giao Dịch";

            // Có thể gán dữ liệu vào control ngay tại đây hoặc trong LoadDataForEdit
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

            LoadComboBoxes();

            if (_maGiaoDich != null)
            {
                LoadDataForEdit(_maGiaoDich.Value);
            }
            else
            {
                radChi.Checked = true; 
                dtNgayGiaoDich.Value = DateTime.Now;
            }
        }

        private void LoadComboBoxes()
        {
            object currentDoiTuong = cbDoiTuong.SelectedValue;
            object currentDanhMuc = cbDanhMuc.SelectedValue;

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

                    // 2. Load Tài Khoản (Lọc theo User)
                    var dsTaiKhoan = context.TaiKhoanThanhToans
                        .Where(tk => tk.MaNguoiDung == currentUserId && tk.TrangThai == "Đang hoạt động")
                        .Select(tk => new { tk.MaTaiKhoanThanhToan, tk.TenTaiKhoan })
                        .ToList();

                    cbTaiKhoan.DataSource = dsTaiKhoan;
                    cbTaiKhoan.DisplayMember = "TenTaiKhoan";
                    cbTaiKhoan.ValueMember = "MaTaiKhoanThanhToan";

                    // 3. Load Danh Mục (Lọc theo User)
                    var dsDanhMuc = context.DanhMucChiTieus
                        .Include(dm => dm.DanhMucChaNavigation)
                        .Where(dm => dm.MaNguoiDung == currentUserId && dm.DanhMucCha != null)
                        .Select(dm => new
                        {
                            dm.MaDanhMuc,
                            TenHienThi = dm.DanhMucChaNavigation.TenDanhMuc + " - " + dm.TenDanhMuc
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

        private void LoadDataForEdit(int maGiaoDich)
        {
            try
            {
                using (var context = _dbFactory.CreateDbContext())
                {
                    var gd = context.GiaoDichs.Find(maGiaoDich);
                    if (gd != null)
                    {
                        // Kiểm tra xem giao dịch này có thuộc về user hiện tại không
                        if (gd.MaNguoiDung != _userContext.MaNguoiDung)
                        {
                            MessageBox.Show("Bạn không có quyền sửa giao dịch này.");
                            this.Close();
                            return;
                        }

                        txtTenGiaoDich.Text = gd.TenGiaoDich;
                        rtbGhiChu.Text = gd.GhiChu;
                        txtSoTien.Text = gd.SoTien.ToString("N0");
                        dtNgayGiaoDich.Value = gd.NgayGiaoDich;

                        if (gd.MaDoiTuongGiaoDich.HasValue) cbDoiTuong.SelectedValue = gd.MaDoiTuongGiaoDich;
                        if (gd.MaTaiKhoanThanhToan.HasValue) cbTaiKhoan.SelectedValue = gd.MaTaiKhoanThanhToan;
                        if (gd.MaDanhMuc.HasValue) cbDanhMuc.SelectedValue = gd.MaDanhMuc;

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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenGiaoDich.Text))
            {
                MessageBox.Show("Vui lòng nhập tên giao dịch.");
                return;
            }

            string soTienClean = txtSoTien.Text.Replace(",", "").Replace(".", "");
            if (!decimal.TryParse(soTienClean, out decimal soTien))
            {
                MessageBox.Show("Số tiền không hợp lệ.");
                return;
            }

            if (soTien < 0)
            {
                MessageBox.Show("Số tiền không được là số âm.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoTien.Focus();
                return;
            }

            int maLoaiGD = radThu.Checked ? 1 : 2;

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
                            
                            // 3. Gán MaNguoiDung từ Context
                            MaNguoiDung = _userContext.MaNguoiDung.Value,
                            
                            MaLoaiGiaoDich = maLoaiGD,
                            MaDoiTuongGiaoDich = cbDoiTuong.SelectedValue as int?,
                            MaTaiKhoanThanhToan = cbTaiKhoan.SelectedValue as int?,
                            MaDanhMuc = cbDanhMuc.SelectedValue as int?
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
                            // Kiểm tra quyền sở hữu
                            if (gd.MaNguoiDung != _userContext.MaNguoiDung)
                            {
                                MessageBox.Show("Không có quyền sửa giao dịch này.");
                                return;
                            }

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
            this.Close();
        }

        private void radChi_CheckedChanged(object sender, EventArgs e) { }
        private void radThu_CheckedChanged(object sender, EventArgs e) { }

        private void btnThemDoiTuongGiaoDich_Click(object sender, EventArgs e)
        {
            if (_serviceProvider == null) return;
            try
            {
                var frm = _serviceProvider.GetRequiredService<FrmThemSuaDoiTuongGiaoDich>();
                frm.SetId(0);
                frm.OnDataSaved = LoadComboBoxes;

                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnThemDanhMucChiTieu_Click(object sender, EventArgs e)
        {
            if (_serviceProvider == null) return;
            try
            {
                var frm = _serviceProvider.GetRequiredService<FrmThemSuaDanhMuc>();
                frm.ShowDialog();
                LoadComboBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}