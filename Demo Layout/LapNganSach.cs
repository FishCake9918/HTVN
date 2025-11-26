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
    public partial class LapNganSach : KryptonForm
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private int _maNganSach = 0;
        private const int MA_NGUOI_DUNG_HIEN_TAI = 1;

        // KHÔNG KHAI BÁO CÁC CONTROLS TẠI ĐÂY! (Chúng đã có trong LapNganSach.Designer.cs)

        // Constructor
        public LapNganSach(IDbContextFactory<QLTCCNContext> dbFactory)
        {
            InitializeComponent();
            _dbFactory = dbFactory;

            this.Load += FrmThemSuaNganSach_Load;
            this.btnLuu.Click += BtnLuu_Click;
            this.btnHuy.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            this.txtSoTien.KeyPress += TxtSoTien_KeyPress;
            this.txtSoTien.KeyPress += TxtSoTien_KeyPress;
            this.txtNam.KeyPress += TxtNam_KeyPress; // <-- Đăng ký sự kiện
        }

        public void SetId(int id)
        {
            _maNganSach = id;
            this.Text = (id == 0) ? "Thêm Ngân sách mới" : "Sửa Ngân sách";

            if (_maNganSach == 0)
            {
                txtSoTien.Text = string.Empty;
                if (cmbThang.DataSource != null)
                    cmbThang.SelectedValue = DateTime.Today.Month;
                txtNam.Text = DateTime.Today.Year.ToString();
            }
        }
        private void TxtNam_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số và phím điều khiển (backspace, delete)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void FrmThemSuaNganSach_Load(object sender, EventArgs e)
        {
            LoadThangComboBox();
            LoadDanhMuc();

            if (_maNganSach > 0)
            {
                LoadDataForEdit(_maNganSach);
            }
        }

        private void LoadDanhMuc()
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    var danhMucList = db.DanhMucChiTieus
                       .Where(d => d.MaNguoiDung == MA_NGUOI_DUNG_HIEN_TAI && d.DanhMucCha == null)
                       .AsNoTracking()
                       .ToList();

                    cmbDanhMuc.DataSource = danhMucList;
                    cmbDanhMuc.DisplayMember = "TenDanhMuc";
                    cmbDanhMuc.ValueMember = "MaDanhMuc";
                    cmbDanhMuc.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh mục: {ex.Message}", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadThangComboBox()
        {
            var months = Enumerable.Range(1, 12)
                .Select(m => new { MonthValue = m, MonthName = $"Tháng {m}" })
                .ToList();

            cmbThang.DataSource = months;
            cmbThang.DisplayMember = "MonthName";
            cmbThang.ValueMember = "MonthValue";

            cmbThang.SelectedValue = DateTime.Today.Month;
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
                        txtSoTien.Text = ns.SoTien.ToString("N0", CultureInfo.CurrentCulture);
                        cmbDanhMuc.SelectedValue = ns.MaDanhMuc;
                        cmbDanhMuc.Enabled = false;

                        cmbThang.SelectedValue = ns.NgayBatDau?.Month ?? DateTime.Today.Month;
                        txtNam.Text = (ns.NgayBatDau?.Year ?? DateTime.Today.Year).ToString();

                        cmbThang.Enabled = false;
                        txtNam.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu ngân sách: {ex.Message}", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            int? maDanhMuc = cmbDanhMuc.SelectedValue as int?;
            int? thang = cmbThang.SelectedValue as int?;
            int? nam = int.TryParse(txtNam.Text.Trim(), out int n) ? (int?)n : null;
            decimal soTien;

            if (maDanhMuc == null || maDanhMuc.Value <= 0)
            {
                MessageBox.Show("Vui lòng chọn Danh mục cần lập ngân sách.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!thang.HasValue || !nam.HasValue)
            {
                MessageBox.Show("Vui lòng chọn Tháng và nhập Năm hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!decimal.TryParse(txtSoTien.Text.Replace(",", "").Replace(".", ""), out soTien) || soTien <= 0)
            {
                MessageBox.Show("Số tiền ngân sách phải là số dương.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoTien.Focus();
                return;
            }

            DateTime ngayBatDau;

            try
            {
                ngayBatDau = new DateTime(nam.Value, thang.Value, 1);
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Tháng hoặc Năm không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_maNganSach == 0)
            {
                DateTime thoiDiemHienTai = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

                if (ngayBatDau.Date < thoiDiemHienTai)
                {
                    MessageBox.Show("Không thể lập ngân sách cho tháng/năm đã qua.", "Lỗi Thời gian", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            using (var db = _dbFactory.CreateDbContext())
            {
                try
                {
                    if (_maNganSach == 0)
                    {
                        bool isOverlap = db.BangNganSachs.Any(n =>
                            n.MaNguoiDung == MA_NGUOI_DUNG_HIEN_TAI &&
                            n.MaDanhMuc == maDanhMuc.Value &&
                            n.NgayBatDau == ngayBatDau
                        );

                        if (isOverlap)
                        {
                            MessageBox.Show($"Đã tồn tại ngân sách cho danh mục này trong Tháng {thang.Value}/{nam.Value}.", "Lỗi Trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        var newNs = new BangNganSach
                        {
                            SoTien = soTien,
                            NgayBatDau = ngayBatDau,
                            NgayKetThuc = ngayBatDau.AddMonths(1).AddDays(-1),
                            MaNguoiDung = MA_NGUOI_DUNG_HIEN_TAI,
                            MaDanhMuc = maDanhMuc.Value
                        };
                        db.BangNganSachs.Add(newNs);
                    }
                    else
                    {
                        var nsToUpdate = db.BangNganSachs.FirstOrDefault(n => n.MaNganSach == _maNganSach);
                        if (nsToUpdate != null)
                        {
                            nsToUpdate.SoTien = soTien;
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
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') || (e.KeyChar == ','))
            {
                if (txtSoTien.Text.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
    }
}