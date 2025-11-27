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
        private const int CURRENT_USER_ID = 1;

        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider;

        // --- CONSTRUCTOR 1: THÊM MỚI (NHẬN DI) ---
        public FrmThemGiaoDich(IDbContextFactory<QLTCCNContext> dbFactory, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
            _maGiaoDich = null;
            this.Load += FrmThemGiaoDich_Load;
            this.Text = "Thêm Giao Dịch Mới";
        }

        // --- CONSTRUCTOR 2: SỬA (NHẬN DI + THAM SỐ DỮ LIỆU) ---
        public FrmThemGiaoDich(
            IDbContextFactory<QLTCCNContext> dbFactory,
            IServiceProvider serviceProvider,
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
            _maGiaoDich = maGiaoDich;
            this.Load += FrmThemGiaoDich_Load;
            this.Text = "Cập Nhật Giao Dịch";
        }

        private void FrmThemGiaoDich_Load(object sender, EventArgs e)
        {
            // Setup giao diện
            rtbGhiChu.Multiline = true;
            rtbGhiChu.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbGhiChu.WordWrap = true;

            LoadComboBoxes();

            if (_maGiaoDich != null)
            {
                // --- CHẾ ĐỘ SỬA ---
                LoadDataForEdit(_maGiaoDich.Value);
            }
            else
            {
                // --- CHẾ ĐỘ THÊM MỚI ---
                radChi.Checked = true; // Mặc định là Chi
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
                    // 1. Load Đối Tượng
                    var dsDoiTuong = context.DoiTuongGiaoDichs
                        .Where(dt => dt.MaNguoiDung == CURRENT_USER_ID)
                        .Select(dt => new { dt.MaDoiTuongGiaoDich, dt.TenDoiTuong })
                        .ToList();

                    cbDoiTuong.DataSource = dsDoiTuong;
                    cbDoiTuong.DisplayMember = "TenDoiTuong";
                    cbDoiTuong.ValueMember = "MaDoiTuongGiaoDich";

                    // 2. Load Tài Khoản (Đã lọc theo "Đang hoạt động")
                    var dsTaiKhoan = context.TaiKhoanThanhToans
                        .Where(tk => tk.MaNguoiDung == CURRENT_USER_ID && tk.TrangThai == "Đang hoạt động")
                        .Select(tk => new { tk.MaTaiKhoanThanhToan, tk.TenTaiKhoan })
                        .ToList();

                    cbTaiKhoan.DataSource = dsTaiKhoan;
                    cbTaiKhoan.DisplayMember = "TenTaiKhoan";
                    cbTaiKhoan.ValueMember = "MaTaiKhoanThanhToan";

                    // 3. Load Danh Mục (HIỂN THỊ DẠNG: CHA - CON)
                    var dsDanhMuc = context.DanhMucChiTieus
                        .Include(dm => dm.DanhMucChaNavigation)
                        .Where(dm => dm.MaNguoiDung == CURRENT_USER_ID && dm.DanhMucCha != null)
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

            // Gán lại giá trị đã chọn (nếu có)
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
                        txtTenGiaoDich.Text = gd.TenGiaoDich;
                        rtbGhiChu.Text = gd.GhiChu;
                        txtSoTien.Text = gd.SoTien.ToString("N0");
                        dtNgayGiaoDich.Value = gd.NgayGiaoDich;

                        if (gd.MaDoiTuongGiaoDich.HasValue) cbDoiTuong.SelectedValue = gd.MaDoiTuongGiaoDich;

                        if (gd.MaTaiKhoanThanhToan.HasValue)
                        {
                            var selectedTK = context.TaiKhoanThanhToans
                                .Any(tk => tk.MaTaiKhoanThanhToan == gd.MaTaiKhoanThanhToan && tk.TrangThai == "Đang hoạt động");

                            if (selectedTK)
                                cbTaiKhoan.SelectedValue = gd.MaTaiKhoanThanhToan;
                            else
                                cbTaiKhoan.SelectedIndex = -1;
                        }

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

        // Trong FrmThemGiaoDich.cs

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

            // ⭐ THÊM KIỂM TRA SỐ ÂM NGAY TẠI ĐÂY ⭐
            if (soTien < 0)
            {
                MessageBox.Show("Số tiền không được là số âm.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoTien.Focus();
                return;
            }
            // ⭐ KẾT THÚC KIỂM TRA SỐ ÂM ⭐

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
                            SoTien = soTien, // Giá trị đã được kiểm tra không âm
                            NgayGiaoDich = dtNgayGiaoDich.Value,
                            MaNguoiDung = CURRENT_USER_ID,
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
                            gd.TenGiaoDich = txtTenGiaoDich.Text;
                            gd.GhiChu = rtbGhiChu.Text;
                            gd.SoTien = soTien; // Giá trị đã được kiểm tra không âm
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

        // --- HÀM CHO NÚT THÊM ĐỐI TƯỢNG GIAO DỊCH ---
        private void btnThemDoiTuongGiaoDich_Click(object sender, EventArgs e)
        {
            if (_serviceProvider == null)
            {
                MessageBox.Show("Lỗi cấu hình hệ thống (IServiceProvider).", "Lỗi DI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var frm = _serviceProvider.GetRequiredService<FrmChinhSuaDoiTuongGiaoDich>();
                frm.OnDataSaved = LoadComboBoxes;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở form Thêm Đối tượng Giao dịch: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- HÀM NÀY ĐANG BỎ TRỐNG (TRƯỚC KHI BẠN HỎI) ---
        private void btnThemDanhMucChiTieu_Click(object sender, EventArgs e)
        {
            if (_serviceProvider == null)
            {
                MessageBox.Show("Lỗi cấu hình hệ thống (IServiceProvider).", "Lỗi DI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 1. Lấy instance của form con từ DI container.
                var frm = _serviceProvider.GetRequiredService<frmThemDanhMuc>();

                // 2. Gọi hàm để thiết lập form ở chế độ Thêm mới (ID = 0).
                // Hàm này cũng tự động tải ComboBoxes.
                frm.CheDoSua(0);

                // 3. Hiển thị form con.
                frm.ShowDialog();

                // 4. KHÔNG CẦN CALLBACK, TẢI LẠI THỦ CÔNG sau khi đóng form con.
                // Dữ liệu sẽ được cập nhật lại sau khi form con đóng.
                LoadComboBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở form Thêm Danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}