using Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;
using Krypton.Toolkit;
using System.Globalization;

namespace Demo_Layout
{
    // Cần đảm bảo Form này được đăng ký trong Program.cs
    public partial class LapNganSach : KryptonForm
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private int _maNganSach = 0; // 0 là Thêm mới
        private const int MA_NGUOI_DUNG_HIEN_TAI = 1;

        // Khai báo Constructor cho DI
        public LapNganSach(IDbContextFactory<QLTCCNContext> dbFactory)
        {
            InitializeComponent();
            _dbFactory = dbFactory;

            this.Load += FrmThemSuaNganSach_Load;
            this.btnLuu.Click += BtnLuu_Click;
            this.btnHuy.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            // Ràng buộc nhập số cho Số tiền
            this.txtSoTien.KeyPress += TxtSoTien_KeyPress;
        }

        // Phương thức để thiết lập ID (0: Thêm, >0: Sửa)
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
                // Reset form cho Thêm mới
                txtSoTien.Text = string.Empty;
                dtpNgayBatDau.Value = DateTime.Today.Date;
                dtpNgayKetThuc.Value = DateTime.Today.Date.AddMonths(1).AddDays(-1);
            }
        }

        private void FrmThemSuaNganSach_Load(object sender, EventArgs e)
        {
            LoadDanhMucCha();
        }

        private void LoadDanhMucCha()
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    // SỬA LỖI: Dùng tên DbSet phổ biến nhất (thêm 's')
                    var danhMucList = db.DanhMucChiTieus // <--- SỬA TẠI ĐÂY (Giả định tên Entity là DanhMucChiTieu)
                     .Where(d => d.MaNguoiDung == MA_NGUOI_DUNG_HIEN_TAI && d.DanhMucCha == null)
                     .AsNoTracking()
                     .ToList();

                    cmbDanhMucCha.DataSource = danhMucList;
                    // ...
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh mục: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataForEdit(int id)
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    var ns = db.BangNganSachs.AsNoTracking().FirstOrDefault(n => n.MaNganSach == id);
                    if (ns != null)
                    {
                        // Load dữ liệu vào controls
                        txtSoTien.Text = ns.SoTien.ToString("N0", CultureInfo.CurrentCulture);
                        dtpNgayBatDau.Value = ns.NgayBatDau ?? DateTime.Today; // Xử lý NULL
                        dtpNgayKetThuc.Value = ns.NgayKetThuc ?? DateTime.Today.AddMonths(1); // Xử lý NULL
                        cmbDanhMucCha.SelectedValue = ns.MaDanhMuc;

                        // Không cho phép sửa Danh mục khi Sửa Ngân sách
                        cmbDanhMucCha.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu ngân sách: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- RÀNG BUỘC VÀ LƯU DỮ LIỆU ---
        private void BtnLuu_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra nhập liệu và định dạng
            string soTienText = txtSoTien.Text.Replace(",", "").Replace(".", "");
            if (!decimal.TryParse(soTienText, out decimal soTien) || soTien <= 0)
            {
                MessageBox.Show("Số tiền ngân sách phải là số dương.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoTien.Focus();
                return;
            }

            int? maDanhMuc = cmbDanhMucCha.SelectedValue as int?;
            if (maDanhMuc == null)
            {
                MessageBox.Show("Vui lòng chọn Danh mục cần lập ngân sách.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime ngayBatDau = dtpNgayBatDau.Value.Date;
            DateTime ngayKetThuc = dtpNgayKetThuc.Value.Date;

            // 2. RÀNG BUỘC: Ngày kết thúc phải sau ngày bắt đầu
            if (ngayKetThuc <= ngayBatDau)
            {
                MessageBox.Show("Ngày kết thúc phải sau ngày bắt đầu.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var db = _dbFactory.CreateDbContext())
            {
                try
                {
                    if (_maNganSach == 0)
                    {
                        // Ràng buộc trùng lặp
                        bool isOverlap = db.BangNganSachs.Any(n =>
                            n.MaNguoiDung == MA_NGUOI_DUNG_HIEN_TAI &&
                            n.MaDanhMuc == maDanhMuc.Value &&
                            ngayBatDau < n.NgayKetThuc && ngayKetThuc > n.NgayBatDau
                        );

                        if (isOverlap)
                        {
                            MessageBox.Show("Đã tồn tại ngân sách cho danh mục này...", "Lỗi Trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // SỬA LỖI: Dùng tên Entity đúng (BangNganSach)
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
                        // ... (logic cập nhật giữ nguyên) ...
                    }

                    db.SaveChanges();
                    // ... (thông báo thành công) ...
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lưu dữ liệu: {ex.Message}", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- RÀNG BUỘC: KeyPress cho ô Số tiền ---
        private void TxtSoTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cho phép số, dấu backspace, và dấu thập phân/phân cách
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
            // Chỉ cho phép một dấu thập phân/phân cách
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