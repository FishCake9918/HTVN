using Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;
using Krypton.Toolkit;
using System.Globalization;
using System.Collections.Generic;

namespace Demo_Layout
{
    public partial class LapNganSach : KryptonForm
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private int _maNganSach = 0;
        private const int MA_NGUOI_DUNG_HIEN_TAI = 1;

        // Khai báo Constructor cho DI
        public LapNganSach(IDbContextFactory<QLTCCNContext> dbFactory)
        {
            InitializeComponent();
            _dbFactory = dbFactory;

            this.Load += FrmThemSuaNganSach_Load;
            this.btnLuu.Click += BtnLuu_Click;
            this.btnHuy.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            this.txtSoTien.KeyPress += TxtSoTien_KeyPress;
        }

        // Phương thức để thiết lập ID (giữ nguyên)
        public void SetId(int id)
        {
            _maNganSach = id;
            this.Text = (id == 0) ? "Thêm Ngân sách mới" : "Sửa Ngân sách";

            if (_maNganSach > 0)
            {
                LoadDataForEdit(id);
            }
            else
            {
                txtSoTien.Text = string.Empty;
                dtpNgayBatDau.Value = DateTime.Today.Date;
                dtpNgayKetThuc.Value = DateTime.Today.Date.AddMonths(1).AddDays(-1);
            }
        }

        private void FrmThemSuaNganSach_Load(object sender, EventArgs e)
        {
            LoadDanhMucCha(); // Tải cấu trúc cây danh mục
        }

        // --- TẢI DỮ LIỆU DANH MỤC VÀO COMBOBOX ---
        private void LoadDanhMucCha()
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    // Chỉ tải các Danh mục Gốc (DanhMucCha == null) cho Ngân sách
                    var danhMucList = db.DanhMucChiTieus
                       .Where(d => d.MaNguoiDung == MA_NGUOI_DUNG_HIEN_TAI && d.DanhMucCha == null)
                       .AsNoTracking()
                       .ToList();

                    // Gán Data Source cho ComboBox
                    cmbDanhMuc.DataSource = danhMucList;
                    cmbDanhMuc.DisplayMember = "TenDanhMuc";
                    cmbDanhMuc.ValueMember = "MaDanhMuc";

                    // SỬA LỖI: Đặt giá trị mặc định nếu danh sách có dữ liệu
                    if (danhMucList.Any())
                    {
                        cmbDanhMuc.SelectedIndex = 0; // Chọn mục đầu tiên mặc định
                    }
                    else
                    {
                        cmbDanhMuc.SelectedIndex = -1; // Không có gì để chọn
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh mục: {ex.Message}", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- LOAD DATA FOR EDIT (Đã sửa cho ComboBox) ---
        private void LoadDataForEdit(int id)
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    var ns = db.BangNganSachs.AsNoTracking().FirstOrDefault(n => n.MaNganSach == id);
                    if (ns != null)
                    {
                        txtSoTien.Text = ns.SoTien.ToString("N0", CultureInfo.CurrentCulture);
                        dtpNgayBatDau.Value = ns.NgayBatDau ?? DateTime.Today;
                        dtpNgayKetThuc.Value = ns.NgayKetThuc ?? DateTime.Today.AddMonths(1);

                        // LỌC: Chọn mục tương ứng trong ComboBox
                        cmbDanhMuc.SelectedValue = ns.MaDanhMuc; // ns.MaDanhMuc là int?, SelectedValue tự xử lý

                        cmbDanhMuc.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu ngân sách: {ex.Message}", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- RÀNG BUỘC VÀ LƯU DỮ LIỆU ---
        private void BtnLuu_Click(object sender, EventArgs e)
        {
            // 1. Lấy MaDanhMuc từ ComboBox
            // Sử dụng as int? để lấy giá trị int an toàn, nếu SelectedValue là null thì maDanhMuc là null
            int? maDanhMuc = cmbDanhMuc.SelectedValue as int?;

            // SỬA LỖI: Kiểm tra maDanhMuc có giá trị hợp lệ (khác null và ID > 0)
            if (maDanhMuc == null || maDanhMuc.Value <= 0)
            {
                MessageBox.Show("Vui lòng chọn Danh mục cần lập ngân sách.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Validation số tiền và ngày tháng
            string soTienText = txtSoTien.Text.Replace(",", "").Replace(".", "");
            if (!decimal.TryParse(soTienText, out decimal soTien) || soTien <= 0)
            {
                MessageBox.Show("Số tiền ngân sách phải là số dương.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoTien.Focus();
                return;
            }

            DateTime ngayBatDau = dtpNgayBatDau.Value.Date;
            DateTime ngayKetThuc = dtpNgayKetThuc.Value.Date;

            if (ngayKetThuc <= ngayBatDau)
            {
                MessageBox.Show("Ngày kết thúc phải sau ngày bắt đầu.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // RÀNG BUỘC THỜI GIAN: Ngân sách không được lập cho quá khứ (Trừ chế độ Sửa)
            if (_maNganSach == 0 && ngayBatDau < DateTime.Today.Date)
            {
                MessageBox.Show("Không thể lập ngân sách cho ngày trong quá khứ.", "Lỗi Thời gian", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var db = _dbFactory.CreateDbContext())
            {
                try
                {
                    if (_maNganSach == 0) // THÊM MỚI
                    {
                        // Ràng buộc trùng lặp
                        bool isOverlap = db.BangNganSachs.Any(n =>
                            n.MaNguoiDung == MA_NGUOI_DUNG_HIEN_TAI &&
                            n.MaDanhMuc == maDanhMuc.Value &&
                            ngayBatDau < n.NgayKetThuc && ngayKetThuc > n.NgayBatDau
                        );

                        if (isOverlap)
                        {
                            MessageBox.Show("Đã tồn tại ngân sách cho danh mục này trong khoảng thời gian trùng lặp.", "Lỗi Trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // THÊM MỚI
                        var newNs = new BangNganSach
                        {
                            SoTien = soTien,
                            NgayBatDau = ngayBatDau,
                            NgayKetThuc = ngayKetThuc,
                            MaNguoiDung = MA_NGUOI_DUNG_HIEN_TAI,
                            MaDanhMuc = maDanhMuc.Value
                        };
                        db.BangNganSachs.Add(newNs);
                    }
                    else // CẬP NHẬT
                    {
                        var nsToUpdate = db.BangNganSachs.FirstOrDefault(n => n.MaNganSach == _maNganSach);
                        if (nsToUpdate != null)
                        {
                            nsToUpdate.SoTien = soTien;
                            nsToUpdate.NgayBatDau = ngayBatDau;
                            nsToUpdate.NgayKetThuc = ngayKetThuc;
                        }
                    }

                    db.SaveChanges();
                    MessageBox.Show("Lưu ngân sách thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lưu dữ liệu: {ex.Message}", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TxtSoTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Logic ràng buộc nhập số (giữ nguyên)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') || (e.KeyChar == ','))
            {
                if (((KryptonTextBox)sender).Text.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
    }
}