using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Piggy_Admin
{
    public partial class FrmThemSuaTaiKhoan : Form
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private int? _maTaiKhoanHienTai = null;

        public FrmThemSuaTaiKhoan(IDbContextFactory<QLTCCNContext> dbFactory)
        {
            InitializeComponent();
            _dbFactory = dbFactory;

            // Yêu cầu 2: Không cho chỉnh quyền (ẩn UI)
            lblVaiTro.Visible = false;
            cboVaiTro.Visible = false;
        }

        public void LoadDataForEdit(int maTaiKhoan)
        {
            _maTaiKhoanHienTai = maTaiKhoan;
            lblTitle.Text = "CẬP NHẬT NGƯỜI DÙNG";
            lblNote.Text = "(Để trống nếu không đổi mật khẩu)";
            this.Text= "Cập nhật tài khoản";
            txtEmail.Enabled = false; // Email không được sửa

            using (var db = _dbFactory.CreateDbContext())
            {
                var tk = db.Set<TaiKhoan>()
                    .Include(t => t.NguoiDung)
                    .FirstOrDefault(t => t.MaTaiKhoan == maTaiKhoan);

                if (tk != null)
                {
                    txtEmail.Text = tk.Email;
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
                        // Luôn lấy vai trò User (chặn việc set thành Admin)
                        var userRole = db.Set<VaiTro>().FirstOrDefault(r => r.TenVaiTro != "Admin");

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
                                MaVaiTro = userRole.MaVaiTro // Yêu cầu 2: Luôn gán là User
                            };
                            db.Set<TaiKhoan>().Add(taiKhoan);
                            db.SaveChanges();
                        }
                        // --- CẬP NHẬT ---
                        else
                        {
                            taiKhoan = db.Set<TaiKhoan>().Find(_maTaiKhoanHienTai);
                            if (taiKhoan == null) return;

                            // Chỉ cập nhật mật khẩu nếu có nhập
                            if (!string.IsNullOrEmpty(password))
                            {
                                if (password.Length < 6)
                                {
                                    MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                taiKhoan.MatKhau = password;
                            }
                            // Không cập nhật MaVaiTro -> Đảm bảo không bị chuyển sang Admin
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