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
        private const int MA_NGUOI_DUNG_HIEN_TAI = 1;

        // Giả định controls tồn tại trong Designer: tbTenTaiKhoan, txtSoDu, cmbLoaiTaiKhoan, btnTao, btnQuayLai

        public FormThemTaiKhoanThanhToan(IDbContextFactory<QLTCCNContext> dbFactory)
        {
            InitializeComponent();
            _dbFactory = dbFactory;

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
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải loại tài khoản: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- LOGIC LƯU DỮ LIỆU ---
        private void BtnTao_Click(object sender, EventArgs e)
        {
            btnTao.Enabled = false;

            string ten = tbTenTaiKhoan.Text.Trim();
            int? maLoai = cmbLoaiTaiKhoan.SelectedValue as int?;
            decimal soDuBanDau = 0;

            // Validation 1: Tên, Loại TK
            if (string.IsNullOrEmpty(ten) || maLoai == null || maLoai == -1)
            {
                MessageBox.Show("Vui lòng nhập Tên tài khoản và chọn Loại tài khoản.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnTao.Enabled = true;
                return;
            }

            // Validation 2: Số dư
            if (!string.IsNullOrEmpty(txtSoDu.Text.Trim()))
            {
                string cleanSoDu = txtSoDu.Text.Replace(".", "").Replace(",", "");
                if (!decimal.TryParse(cleanSoDu, out soDuBanDau) || soDuBanDau < 0)
                {
                    MessageBox.Show("Số dư không hợp lệ (phải là số không âm), vui lòng nhập lại.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSoDu.Focus();
                    btnTao.Enabled = true;
                    return;
                }
            }

            using (var db = _dbFactory.CreateDbContext())
            {
                // Kiểm tra Trùng tên 
                var userAccounts = db.TaiKhoanThanhToans
                                            .Where(t => t.MaNguoiDung == MA_NGUOI_DUNG_HIEN_TAI)
                                            .ToList();
                bool isDuplicate = userAccounts.Any(t =>
                    t.TenTaiKhoan.Equals(ten, StringComparison.OrdinalIgnoreCase));

                if (isDuplicate)
                {
                    MessageBox.Show("Tên tài khoản đã tồn tại, yêu cầu nhập lại.", "Trùng tên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbTenTaiKhoan.Focus();
                    btnTao.Enabled = true;
                    return;
                }

                try
                {
                    var newTaiKhoan = new TaiKhoanThanhToan
                    {
                        TenTaiKhoan = ten,
                        MaLoaiTaiKhoan = maLoai.Value,
                        MaNguoiDung = MA_NGUOI_DUNG_HIEN_TAI,
                        TrangThai = "Đang hoạt động",
                        SoDuBanDau = soDuBanDau // GÁN SỐ DƯ BAN ĐẦU VÀO ENTITY
                    };

                    // 1. LƯU TÀI KHOẢN TRƯỚC (Quan trọng để có ID)
                    db.TaiKhoanThanhToans.Add(newTaiKhoan);
                    db.SaveChanges();

                    // 2. THÊM GIAO DỊCH GỐC (Nếu Số dư > 0)
                    if (soDuBanDau > 0)
                    {
                        var initialTransaction = new GiaoDich
                        {
                            MaTaiKhoanThanhToan = newTaiKhoan.MaTaiKhoanThanhToan,
                            SoTien = soDuBanDau,
                            NgayGiaoDich = DateTime.Now,

                            // *** DÒNG CODE SỬA LỖI GÂY LỖI: GÁN GIÁ TRỊ CHO TRƯỜNG NOT NULL ***
                            TenGiaoDich = "Số dư ban đầu",
                            GhiChu = "Số dư ban đầu",

                            MaLoaiGiaoDich = 1, // Giả định 1 là THU
                            MaNguoiDung = MA_NGUOI_DUNG_HIEN_TAI,

                            // Gán NULL an toàn cho các trường khóa ngoại không bắt buộc
                            MaDanhMuc = null,
                            MaDoiTuongGiaoDich = null
                        };
                        db.GiaoDichs.Add(initialTransaction);
                        db.SaveChanges();
                    }

                    MessageBox.Show($"Thêm tài khoản thành công! Số dư ban đầu: {soDuBanDau:N0} VND", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (DbUpdateException dbEx)
                {
                    string innerMsg = dbEx.InnerException?.Message ?? "Không rõ nguyên nhân chi tiết.";
                    MessageBox.Show($"Lỗi Database khi lưu giao dịch: {innerMsg}", "Lỗi Lưu Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi không xác định: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btnTao.Enabled = true;
                }
            }
        }

        private void TxtSoDu_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cho phép nhập số, Backspace, và dấu thập phân (dấu chấm hoặc dấu phẩy)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            // Chỉ cho phép một dấu thập phân
            if ((e.KeyChar == '.') || (e.KeyChar == ','))
            {
                if (((TextBox)sender).Text.Contains('.') || ((TextBox)sender).Text.Contains(','))
                {
                    e.Handled = true;
                }
            }
        }

    }
}