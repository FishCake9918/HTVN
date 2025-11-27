using Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;
using Krypton.Toolkit;
using System.Data;

namespace Demo_Layout
{
    public partial class FormThemTaiKhoanThanhToan : Form
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly CurrentUserContext _userContext; // <-- Inject Context

        public FormThemTaiKhoanThanhToan(
            IDbContextFactory<QLTCCNContext> dbFactory,
            CurrentUserContext userContext) // <-- Inject
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _userContext = userContext;

            this.Load += FormThemTaiKhoan_Load;
            this.btnTao.Click += BtnTao_Click;
            this.btnQuayLai.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            this.txtSoDu.KeyPress += TxtSoDu_KeyPress;
        }

        private void FormThemTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadLoaiTaiKhoan();
        }

        private void LoadLoaiTaiKhoan()
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    var loaiTaiKhoanList = db.LoaiTaiKhoans.AsNoTracking().ToList();
                    cmbLoaiTaiKhoan.DataSource = loaiTaiKhoanList;
                    cmbLoaiTaiKhoan.DisplayMember = "TenLoaiTaiKhoan";
                    cmbLoaiTaiKhoan.ValueMember = "MaLoaiTaiKhoan";
                    cmbLoaiTaiKhoan.SelectedIndex = -1;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void BtnTao_Click(object sender, EventArgs e)
        {
            btnTao.Enabled = false;
            if (_userContext.MaNguoiDung == null) { MessageBox.Show("Lỗi xác thực user."); return; }

            string ten = tbTenTaiKhoan.Text.Trim();
            int? maLoai = cmbLoaiTaiKhoan.SelectedValue as int?;
            decimal soDuBanDau = 0;

            if (string.IsNullOrEmpty(ten) || maLoai == null || maLoai == -1)
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin.");
                btnTao.Enabled = true; return;
            }
            if (!string.IsNullOrEmpty(txtSoDu.Text.Trim()))
            {
                string cleanSoDu = txtSoDu.Text.Replace(".", "").Replace(",", "");
                if (!decimal.TryParse(cleanSoDu, out soDuBanDau) || soDuBanDau < 0)
                {
                    MessageBox.Show("Số dư không hợp lệ.");
                    btnTao.Enabled = true; return;
                }
            }

            using (var db = _dbFactory.CreateDbContext())
            {
                // SỬA: Check trùng tên theo User hiện tại
                var userAccounts = db.TaiKhoanThanhToans.Where(t => t.MaNguoiDung == _userContext.MaNguoiDung).ToList();
                if (userAccounts.Any(t => t.TenTaiKhoan.Equals(ten, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("Tên tài khoản đã tồn tại.");
                    btnTao.Enabled = true; return;
                }

                try
                {
                    var newTaiKhoan = new TaiKhoanThanhToan
                    {
                        TenTaiKhoan = ten,
                        MaLoaiTaiKhoan = maLoai.Value,
                        MaNguoiDung = _userContext.MaNguoiDung.Value, // <-- Dùng ID thật
                        TrangThai = "Đang hoạt động",
                        SoDuBanDau = soDuBanDau
                    };
                    db.TaiKhoanThanhToans.Add(newTaiKhoan);
                    db.SaveChanges();

                    if (soDuBanDau > 0)
                    {
                        var initialTransaction = new GiaoDich
                        {
                            MaTaiKhoanThanhToan = newTaiKhoan.MaTaiKhoanThanhToan,
                            SoTien = soDuBanDau,
                            NgayGiaoDich = DateTime.Now,
                            TenGiaoDich = "Số dư ban đầu",
                            GhiChu = "Số dư ban đầu",
                            MaLoaiGiaoDich = 1, // Thu
                            MaNguoiDung = _userContext.MaNguoiDung.Value, // <-- Dùng ID thật
                        };
                        db.GiaoDichs.Add(initialTransaction);
                        db.SaveChanges();
                    }

                    MessageBox.Show("Thêm tài khoản thành công!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
                finally { btnTao.Enabled = true; }
            }
        }

        private void TxtSoDu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ',')) e.Handled = true;
            if ((e.KeyChar == '.') || (e.KeyChar == ',')) if (((TextBox)sender).Text.Contains('.') || ((TextBox)sender).Text.Contains(',')) e.Handled = true;
        }
    }
}