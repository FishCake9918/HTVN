using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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

            // Chọn mặc định giới tính
            if (cboGioiTinh.Items.Count > 0) cboGioiTinh.SelectedIndex = 0;

            // Đăng ký sự kiện vẽ viền
            this.Paint += Vien_Paint;
        }

        // Hàm vẽ viền thủ công
        private void Vien_Paint(object sender, PaintEventArgs e)
        {
            Color borderColor = Color.Black;
            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            using (Pen pen = new Pen(borderColor, 3))
            {
                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        // Hàm load dữ liệu khi ở chế độ Sửa
        public void LoadDataForEdit(int maTaiKhoan)
        {
            _maTaiKhoanHienTai = maTaiKhoan;
            lblTitle.Text = "CẬP NHẬT TÀI KHOẢN";
            lblNote.Text = "(Để trống nếu không đổi mật khẩu)";
            this.Text = "Cập nhật tài khoản";
            txtEmail.Enabled = false; // Không được sửa Email

            using (var db = _dbFactory.CreateDbContext())
            {
                var tk = db.Set<TaiKhoan>()
                    .Include(t => t.NguoiDung)
                    .FirstOrDefault(t => t.MaTaiKhoan == maTaiKhoan);

                if (tk != null)
                {
                    txtEmail.Text = tk.Email;
                    if (tk.NguoiDung != null)
                    {
                        txtHoTen.Text = tk.NguoiDung.HoTen;
                        if (!string.IsNullOrEmpty(tk.NguoiDung.GioiTinh))
                            cboGioiTinh.SelectedItem = tk.NguoiDung.GioiTinh;
                        if (tk.NguoiDung.NgaySinh.HasValue)
                            dtpNgaySinh.Value = tk.NguoiDung.NgaySinh.Value;
                    }
                }
            }
        }

        // Xử lý sự kiện nút Lưu
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // 1. Lấy dữ liệu
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string gioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "Khác";
            DateTime ngaySinh = dtpNgaySinh.Value;

            // 2. Kiểm tra dữ liệu bắt buộc
            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Vui lòng nhập Họ tên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            // 3. Kiểm tra độ tuổi (>= 16)
            DateTime today = DateTime.Today;
            int age = today.Year - ngaySinh.Year;
            if (ngaySinh.Date > today.AddYears(-age)) age--; // Trừ 1 nếu chưa tới sinh nhật

            if (age < 16)
            {
                MessageBox.Show($"Người dùng mới {age} tuổi. Quy định phải từ 16 tuổi trở lên.", "Chưa đủ tuổi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 4. Xử lý lưu vào cơ sở dữ liệu
            using (var db = _dbFactory.CreateDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var userRole = db.Set<VaiTro>().FirstOrDefault(r => r.TenVaiTro != "Admin");
                        if (userRole == null)
                        {
                            MessageBox.Show("Lỗi: Không tìm thấy vai trò người dùng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        TaiKhoan taiKhoan;

                        // --- TRƯỜNG HỢP THÊM MỚI ---
                        if (_maTaiKhoanHienTai == null)
                        {
                            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                            {
                                MessageBox.Show("Email và Mật khẩu không được để trống.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (db.Set<TaiKhoan>().Any(t => t.Email == email))
                            {
                                MessageBox.Show("Email đã tồn tại.", "Trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (password.Length < 6)
                            {
                                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự.", "Bảo mật", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            taiKhoan = new TaiKhoan
                            {
                                Email = email,
                                MatKhau = password,
                                MaVaiTro = userRole.MaVaiTro
                            };
                            db.Set<TaiKhoan>().Add(taiKhoan);
                            db.SaveChanges();
                        }
                        // --- TRƯỜNG HỢP CẬP NHẬT ---
                        else
                        {
                            taiKhoan = db.Set<TaiKhoan>().Find(_maTaiKhoanHienTai);
                            if (taiKhoan == null) return;

                            // Chỉ cập nhật mật khẩu nếu có nhập mới
                            if (!string.IsNullOrEmpty(password))
                            {
                                if (password.Length < 6)
                                {
                                    MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự.", "Bảo mật", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                taiKhoan.MatKhau = password;
                            }
                            db.SaveChanges();
                        }

                        // --- CẬP NHẬT BẢNG NGƯỜI DÙNG ---
                        var nguoiDung = db.Set<NguoiDung>().FirstOrDefault(n => n.MaTaiKhoan == taiKhoan.MaTaiKhoan);
                        if (nguoiDung == null)
                        {
                            nguoiDung = new NguoiDung { MaTaiKhoan = taiKhoan.MaTaiKhoan };
                            db.Set<NguoiDung>().Add(nguoiDung);
                        }

                        nguoiDung.HoTen = hoTen;
                        nguoiDung.GioiTinh = gioiTinh;
                        nguoiDung.NgaySinh = ngaySinh;

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