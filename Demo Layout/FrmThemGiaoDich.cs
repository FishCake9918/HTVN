using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Data; // Namespace chứa QLTCCNContext và Entity

namespace Demo_Layout
{
    public partial class FrmThemGiaoDich : Form
    {
        public Action OnDataAdded; // Callback load lại grid
        private int? _maGiaoDich = null; // null = thêm mới, có giá trị = sửa
        private const int CURRENT_USER_ID = 1; // Giả định user ID

        // Chuỗi kết nối
        private const string ConnectionString = "Data Source=DESKTOP-V70T5QI;Initial Catalog=QLTCCN;Integrated Security=True;TrustServerCertificate=True";

        // --- CONSTRUCTOR 1: THÊM MỚI ---
        public FrmThemGiaoDich()
        {
            InitializeComponent();
            _maGiaoDich = null;
            this.Load += FrmThemGiaoDich_Load;
        }

        // --- CONSTRUCTOR 2: SỬA ---
        public FrmThemGiaoDich(int maGiaoDich, string tenGiaoDich, string ghiChu, decimal soTien, DateTime ngayGiaoDich, int maDoiTuong, int maTaiKhoan)
        {
            InitializeComponent();
            _maGiaoDich = maGiaoDich;
            this.Load += FrmThemGiaoDich_Load;
        }

        private QLTCCNContext GetContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<QLTCCNContext>();
            optionsBuilder.UseSqlServer(ConnectionString);
            return new QLTCCNContext(optionsBuilder.Options);
        }

        private void FrmThemGiaoDich_Load(object sender, EventArgs e)
        {
            // Setup giao diện
            rtbGhiChu.Multiline = true;
            rtbGhiChu.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbGhiChu.WordWrap = true;

            LoadComboBoxes(); // <--- Load dữ liệu cho 3 ComboBox: Đối tượng, Tài khoản, Danh mục

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
            try
            {
                using (var context = GetContext())
                {
                    // 1. Load Đối Tượng
                    var dsDoiTuong = context.DoiTuongGiaoDichs
                        .Where(dt => dt.MaNguoiDung == CURRENT_USER_ID)
                        .Select(dt => new { dt.MaDoiTuongGiaoDich, dt.TenDoiTuong })
                        .ToList();

                    cbDoiTuong.DataSource = dsDoiTuong;
                    cbDoiTuong.DisplayMember = "TenDoiTuong";
                    cbDoiTuong.ValueMember = "MaDoiTuongGiaoDich";

                    // 2. Load Tài Khoản
                    var dsTaiKhoan = context.TaiKhoanThanhToans
                        .Where(tk => tk.MaNguoiDung == CURRENT_USER_ID)
                        .Select(tk => new { tk.MaTaiKhoanThanhToan, tk.TenTaiKhoan })
                        .ToList();

                    cbTaiKhoan.DataSource = dsTaiKhoan;
                    cbTaiKhoan.DisplayMember = "TenTaiKhoan";
                    cbTaiKhoan.ValueMember = "MaTaiKhoanThanhToan";

                    // 3. Load Danh Mục (MỚI THÊM)
                    var dsDanhMuc = context.DanhMucChiTieus
                        .Where(dm => dm.MaNguoiDung == CURRENT_USER_ID)
                        .Select(dm => new { dm.MaDanhMuc, dm.TenDanhMuc }) // Lấy ID và Tên
                        .ToList();

                    cbDanhMuc.DataSource = dsDanhMuc;
                    cbDanhMuc.DisplayMember = "TenDanhMuc";
                    cbDanhMuc.ValueMember = "MaDanhMuc";

                    // Reset selection để tránh chọn nhầm cái đầu tiên khi mới mở
                    cbDanhMuc.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách chọn: " + ex.Message);
            }
        }

        private void LoadDataForEdit(int maGiaoDich)
        {
            try
            {
                using (var context = GetContext())
                {
                    var gd = context.GiaoDichs.Find(maGiaoDich);
                    if (gd != null)
                    {
                        txtTenGiaoDich.Text = gd.TenGiaoDich;
                        rtbGhiChu.Text = gd.GhiChu;
                        txtSoTien.Text = gd.SoTien.ToString("N0");
                        dtNgayGiaoDich.Value = gd.NgayGiaoDich;

                        // Gán giá trị cũ vào ComboBox
                        if (gd.MaDoiTuongGiaoDich.HasValue) cbDoiTuong.SelectedValue = gd.MaDoiTuongGiaoDich;
                        if (gd.MaTaiKhoanThanhToan.HasValue) cbTaiKhoan.SelectedValue = gd.MaTaiKhoanThanhToan;

                        // Gán giá trị cũ vào ComboBox Danh Mục (MỚI THÊM)
                        if (gd.MaDanhMuc.HasValue) cbDanhMuc.SelectedValue = gd.MaDanhMuc;

                        // Xử lý RadioButton
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

            int maLoaiGD = radThu.Checked ? 1 : 2;

            try
            {
                using (var context = GetContext())
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
                            MaNguoiDung = CURRENT_USER_ID,
                            MaLoaiGiaoDich = maLoaiGD,

                            // Lấy giá trị từ 3 ComboBox
                            MaDoiTuongGiaoDich = cbDoiTuong.SelectedValue as int?,
                            MaTaiKhoanThanhToan = cbTaiKhoan.SelectedValue as int?,
                            MaDanhMuc = cbDanhMuc.SelectedValue as int? // (MỚI THÊM)
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
                            gd.SoTien = soTien;
                            gd.NgayGiaoDich = dtNgayGiaoDich.Value;
                            gd.MaLoaiGiaoDich = maLoaiGD;

                            // Cập nhật 3 ComboBox
                            gd.MaDoiTuongGiaoDich = cbDoiTuong.SelectedValue as int?;
                            gd.MaTaiKhoanThanhToan = cbTaiKhoan.SelectedValue as int?;
                            gd.MaDanhMuc = cbDanhMuc.SelectedValue as int?; // (MỚI THÊM)

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
    }
}