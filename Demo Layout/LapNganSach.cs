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
        private readonly CurrentUserContext _userContext; // <-- Inject
        private int _maNganSach = 0;

        public LapNganSach(
            IDbContextFactory<QLTCCNContext> dbFactory,
            CurrentUserContext userContext) // <-- Inject
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _userContext = userContext;

            this.Load += FrmThemSuaNganSach_Load;
            this.btnLuu.Click += BtnLuu_Click;
            this.btnHuy.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            this.txtSoTien.KeyPress += TxtSoTien_KeyPress;
            this.txtNam.KeyPress += TxtNam_KeyPress;
        }

        public void SetId(int id)
        {
            _maNganSach = id;
            this.Text = (id == 0) ? "Thêm Ngân sách mới" : "Sửa Ngân sách";
            if (_maNganSach == 0)
            {
                txtSoTien.Text = string.Empty;
                if (cmbThang.DataSource != null) cmbThang.SelectedValue = DateTime.Today.Month;
                txtNam.Text = DateTime.Today.Year.ToString();
            }
        }
        private void TxtNam_KeyPress(object sender, KeyPressEventArgs e) { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true; }
        private void FrmThemSuaNganSach_Load(object sender, EventArgs e) { LoadThangComboBox(); LoadDanhMuc(); if (_maNganSach > 0) LoadDataForEdit(_maNganSach); }

        private void LoadDanhMuc()
        {
            if (_userContext.MaNguoiDung == null) return;
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    // SỬA: Lọc theo Context User
                    var danhMucList = db.DanhMucChiTieus
                       .Where(d => d.MaNguoiDung == _userContext.MaNguoiDung && d.DanhMucCha == null)
                       .AsNoTracking().ToList();
                    cmbDanhMuc.DataSource = danhMucList; cmbDanhMuc.DisplayMember = "TenDanhMuc"; cmbDanhMuc.ValueMember = "MaDanhMuc"; cmbDanhMuc.SelectedIndex = -1;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void LoadThangComboBox()
        {
            var months = Enumerable.Range(1, 12).Select(m => new { MonthValue = m, MonthName = $"Tháng {m}" }).ToList();
            cmbThang.DataSource = months; cmbThang.DisplayMember = "MonthName"; cmbThang.ValueMember = "MonthValue"; cmbThang.SelectedValue = DateTime.Today.Month;
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
                        cmbThang.Enabled = false; txtNam.Enabled = false;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (_userContext.MaNguoiDung == null) return;
            int? maDanhMuc = cmbDanhMuc.SelectedValue as int?;
            int? thang = cmbThang.SelectedValue as int?;
            int? nam = int.TryParse(txtNam.Text.Trim(), out int n) ? (int?)n : null;
            decimal soTien;

            if (maDanhMuc == null || maDanhMuc.Value <= 0) { MessageBox.Show("Chọn Danh mục."); return; }
            if (!thang.HasValue || !nam.HasValue) { MessageBox.Show("Chọn Tháng/Năm."); return; }
            if (!decimal.TryParse(txtSoTien.Text.Replace(",", "").Replace(".", ""), out soTien) || soTien <= 0) { MessageBox.Show("Tiền phải > 0."); return; }

            DateTime ngayBatDau;
            try { ngayBatDau = new DateTime(nam.Value, thang.Value, 1); } catch { MessageBox.Show("Ngày sai."); return; }

            if (_maNganSach == 0)
            {
                if (ngayBatDau.Date < new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)) { MessageBox.Show("Không lập ngân sách quá khứ."); return; }
            }

            using (var db = _dbFactory.CreateDbContext())
            {
                try
                {
                    if (_maNganSach == 0)
                    {
                        // SỬA: Check trùng theo User thật
                        bool isOverlap = db.BangNganSachs.Any(n => n.MaNguoiDung == _userContext.MaNguoiDung && n.MaDanhMuc == maDanhMuc.Value && n.NgayBatDau == ngayBatDau);
                        if (isOverlap) { MessageBox.Show("Đã có ngân sách này."); return; }

                        var newNs = new BangNganSach
                        {
                            SoTien = soTien,
                            NgayBatDau = ngayBatDau,
                            NgayKetThuc = ngayBatDau.AddMonths(1).AddDays(-1),
                            MaNguoiDung = _userContext.MaNguoiDung.Value, // <-- ID thật
                            MaDanhMuc = maDanhMuc.Value
                        };
                        db.BangNganSachs.Add(newNs);
                    }
                    else
                    {
                        var nsToUpdate = db.BangNganSachs.FirstOrDefault(n => n.MaNganSach == _maNganSach);
                        if (nsToUpdate != null) nsToUpdate.SoTien = soTien;
                    }
                    db.SaveChanges();
                    MessageBox.Show("Thành công!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }
        private void TxtSoTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',') e.Handled = true;
            if ((e.KeyChar == '.' || e.KeyChar == ',') && txtSoTien.Text.Contains(e.KeyChar)) e.Handled = true;
        }
    }
}