using Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage; // Cần thêm namespace này
using System;
using System.Linq;
using System.Windows.Forms;
using Krypton.Toolkit;
using Microsoft.Data.SqlClient;

namespace Demo_Layout
{
    public partial class FormThemTaiKhoanThanhToan : Form
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private const int MA_NGUOI_DUNG_HIEN_TAI = 1;

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

        private void BtnTao_Click(object sender, EventArgs e)
        {
            string ten = tbTenTaiKhoan.Text.Trim();
            int? maLoai = cmbLoaiTaiKhoan.SelectedValue as int?;
            decimal soDu = 0;

            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Tên tài khoản không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbTenTaiKhoan.Focus();
                return;
            }
            if (maLoai == null || maLoai == -1)
            {
                MessageBox.Show("Vui lòng chọn Loại tài khoản.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbLoaiTaiKhoan.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(txtSoDu.Text.Trim()))
            {
                if (!decimal.TryParse(txtSoDu.Text.Replace(",", "").Replace(".", ""), out soDu) || soDu < 0)
                {
                    MessageBox.Show("Số dư không hợp lệ (phải là số không âm), vui lòng nhập lại.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSoDu.Focus();
                    return;
                }
            }

            using (var db = _dbFactory.CreateDbContext())
            {
                // Kiểm tra Trùng tên (Đã fix lỗi LINQ)
                var userAccounts = db.TaiKhoanThanhToans
                                     .Where(t => t.MaNguoiDung == MA_NGUOI_DUNG_HIEN_TAI)
                                     .ToList();
                bool isDuplicate = userAccounts.Any(t =>
                    t.TenTaiKhoan.Equals(ten, StringComparison.OrdinalIgnoreCase));

                if (isDuplicate)
                {
                    MessageBox.Show("Tên tài khoản đã tồn tại, yêu cầu nhập lại.", "Trùng tên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbTenTaiKhoan.Focus();
                    return;
                }

                try
                {
                    var newTaiKhoan = new TaiKhoanThanhToan
                    {
                        TenTaiKhoan = ten,
                        MaLoaiTaiKhoan = maLoai.Value,
                        SoDuBanDau = soDu,
                        MaNguoiDung = MA_NGUOI_DUNG_HIEN_TAI,
                        TrangThai = "Đang hoạt động"
                    };
                    db.TaiKhoanThanhToans.Add(newTaiKhoan);
                    db.SaveChanges(); // <-- Dòng gây lỗi

                    MessageBox.Show("Thêm tài khoản thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (DbUpdateException dbEx) // <-- BẮT LỖI CẬP NHẬT DATABASE
                {
                    string errorMessage = "Lỗi Database: Vi phạm ràng buộc Khóa ngoại (FK) hoặc ràng buộc NOT NULL.";

                    // Lấy lỗi chi tiết từ SQL Server (InnerException)
                    var innerException = dbEx.InnerException;
                    while (innerException != null)
                    {
                        // SỬA TẠI ĐÂY: Dùng Microsoft.Data.SqlClient.SqlException
                        if (innerException is Microsoft.Data.SqlClient.SqlException sqlEx)
                        {
                            errorMessage += $"\n\n[LỖI SQL] Số lỗi: {sqlEx.Number}\nThông báo: {sqlEx.Message}";
                            if (sqlEx.Number == 547)
                            {
                                errorMessage += "\n\n-> HƯỚNG GIẢI QUYẾT: Khóa ngoại bị vi phạm. Kiểm tra ID người dùng (MaNguoiDung = 1) có tồn tại trong bảng NGUOI_DUNG không?";
                            }
                            break;
                        }
                        innerException = innerException.InnerException;
                    }
                    MessageBox.Show(errorMessage, "Lỗi Lưu Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi không xác định: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TxtSoDu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') || (e.KeyChar == ','))
            {
                if (((TextBox)sender).Text.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
    }
}