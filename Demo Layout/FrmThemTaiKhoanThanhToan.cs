using Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;
using Krypton.Toolkit; // Giữ lại vì control vẫn được sử dụng trong designer (chưa thấy chuyển đổi hết)
using System.Data;

namespace Demo_Layout
{
    public partial class FrmThemTaiKhoanThanhToan : Form
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly CurrentUserContext _userContext; // <-- Inject Context
        public FrmThemTaiKhoanThanhToan(
            IDbContextFactory<QLTCCNContext> dbFactory,
            CurrentUserContext userContext) // <-- Inject
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _userContext = userContext;

            // CẬP NHẬT: Đặt tiêu đề cho lblForm
            if (lblForm != null)
            {
                lblForm.Text = "THÊM TÀI KHOẢN THANH TOÁN";
            }
            this.Text = "Thêm Tài Khoản"; // Tiêu đề Form

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
            if (_userContext.MaNguoiDung == null) { MessageBox.Show("Lỗi xác thực user."); btnTao.Enabled = true; return; }

            string ten = tbTenTaiKhoan.Text.Trim();
            // MaLoai có thể là int? nếu cmbLoaiTaiKhoan là KryptonComboBox
            int? maLoai = cmbLoaiTaiKhoan.SelectedValue as int?;
            decimal soDuBanDau = 0;

            if (string.IsNullOrEmpty(ten) || maLoai == null || maLoai.Value <= 0) // Kiểm tra maLoai
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin.");
                btnTao.Enabled = true; return;
            }
            if (!string.IsNullOrEmpty(txtSoDu.Text.Trim()))
            {
                string cleanSoDu = txtSoDu.Text.Replace(".", "").Replace(",", "");
                if (!decimal.TryParse(cleanSoDu, NumberStyles.Currency, CultureInfo.CurrentCulture, out soDuBanDau) || soDuBanDau < 0)
                {
                    MessageBox.Show("Số dư không hợp lệ.");
                    btnTao.Enabled = true; return;
                }
            }

            using (var db = _dbFactory.CreateDbContext())
            {
                try
                {
                    // SỬA: Check trùng tên theo User hiện tại (Case-Insensitive)
                    var userAccounts = db.TaiKhoanThanhToans.Where(t => t.MaNguoiDung == _userContext.MaNguoiDung).ToList();
                    if (userAccounts.Any(t => t.TenTaiKhoan.Equals(ten, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show($"Tên tài khoản '{ten}' đã tồn tại. Vui lòng chọn tên khác.");
                        btnTao.Enabled = true; return;
                    }


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
            // Logic cho phép nhập số, dấu thập phân (dấu chấm hoặc dấu phẩy) và chỉ cho phép một dấu thập phân.
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ',')) e.Handled = true;

            if ((e.KeyChar == '.') || (e.KeyChar == ','))
            {
                // Cho phép chỉ một dấu thập phân
                if (((TextBox)sender).Text.Contains('.') || ((TextBox)sender).Text.Contains(','))
                {
                    e.Handled = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}