using System;
using System.Windows.Forms;
using Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace Piggy_Admin
{
    public partial class FormTaoCapNhatTaiKhoan : Form
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private int? _maTaiKhoanHienTai = null;

        public FormTaoCapNhatTaiKhoan(IDbContextFactory<QLTCCNContext> dbFactory)
        {
            InitializeComponent();
            _dbFactory = dbFactory;

            // Ẩn phần chọn vai trò đi vì mặc định là User
            lblVaiTro.Visible = false;
            cboVaiTro.Visible = false;
        }

        // Không cần LoadRoles nữa

        public void LoadDataForEdit(int maTaiKhoan)
        {
            _maTaiKhoanHienTai = maTaiKhoan;
            lblTitle.Text = "CẬP NHẬT NGƯỜI DÙNG";
            txtEmail.Enabled = false;

            using (var db = _dbFactory.CreateDbContext())
            {
                var tk = db.Set<TaiKhoan>()
                    .Include(t => t.NguoiDung)
                    .FirstOrDefault(t => t.MaTaiKhoan == maTaiKhoan);

                if (tk != null)
                {
                    txtEmail.Text = tk.Email;
                    // Chỉ load thông tin người dùng
                    if (tk.NguoiDung != null)
                        txtHoTen.Text = tk.NguoiDung.HoTen;
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();

            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Vui lòng nhập Họ tên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var db = _dbFactory.CreateDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        // Lấy ID vai trò không phải Admin (Giả sử tên trong DB là "người dùng")
                        var userRole = db.Set<VaiTro>()
                                         .FirstOrDefault(r => r.TenVaiTro != "Admin");

                        if (userRole == null)
                        {
                            MessageBox.Show("Lỗi: Không tìm thấy vai trò người dùng trong DB.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        TaiKhoan taiKhoan;

                        // --- THÊM MỚI ---
                        if (_maTaiKhoanHienTai == null)
                        {
                            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                            {
                                MessageBox.Show("Email và Mật khẩu không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (db.Set<TaiKhoan>().Any(t => t.Email == email))
                            {
                                MessageBox.Show("Email đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            if (password.Length < 6)
                            {
                                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            taiKhoan = new TaiKhoan
                            {
                                Email = email,
                                MatKhau = password,
                                MaVaiTro = userRole.MaVaiTro // LUÔN LÀ USER
                            };
                            db.Set<TaiKhoan>().Add(taiKhoan);
                            db.SaveChanges();
                        }
                        // --- CẬP NHẬT ---
                        else
                        {
                            taiKhoan = db.Set<TaiKhoan>().Find(_maTaiKhoanHienTai);
                            if (taiKhoan == null) return;

                            if (!string.IsNullOrEmpty(password))
                            {
                                taiKhoan.MatKhau = password;
                            }
                            if (password.Length < 6)
                            {
                                MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            db.SaveChanges();
                        }

                        // --- CẬP NHẬT BẢNG NGUOI_DUNG ---
                        var nguoiDung = db.Set<NguoiDung>().FirstOrDefault(n => n.MaTaiKhoan == taiKhoan.MaTaiKhoan);
                        if (nguoiDung == null)
                        {
                            nguoiDung = new NguoiDung
                            {
                                MaTaiKhoan = taiKhoan.MaTaiKhoan,
                                GioiTinh = "Khác",
                                NgaySinh = DateTime.Now
                            };
                            db.Set<NguoiDung>().Add(nguoiDung);
                        }
                        nguoiDung.HoTen = hoTen;

                        db.SaveChanges();
                        transaction.Commit();

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e) => this.Close();
    }
}