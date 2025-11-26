using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Krypton.Toolkit;
using System.Drawing;

namespace Demo_Layout
{
    public delegate void OpenEditFormEventHandler(object sender, int doiTuongId);

    public partial class UserControlDoiTuongGiaoDich : UserControl
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private BindingSource bsDoiTuong = new BindingSource();
        private List<DoiTuongGiaoDich> _fullList = new List<DoiTuongGiaoDich>();
        private const int MA_NGUOI_DUNG_HIEN_TAI = 1;

        public event OpenEditFormEventHandler OnOpenEditForm;

        public UserControlDoiTuongGiaoDich(IDbContextFactory<QLTCCNContext> dbFactory)
        {
            InitializeComponent();
            _dbFactory = dbFactory;

            kryptonDataGridView1.DataSource = bsDoiTuong;

            // Đăng ký sự kiện
            this.Load += UserControlDoiTuongGiaoDich_Load;
            this.txtTimKiem.TextChanged += TxtTimKiem_TextChanged;
            this.btnThem.Click += BtnThem_Click;
            this.btnSua.Click += BtnSua_Click;
            this.btnXoa.Click += BtnXoa_Click;

            // YÊU CẦU: Đúp chuột vào dòng để Sửa
            this.kryptonDataGridView1.DoubleClick += KryptonDataGridView1_DoubleClick;

            ConfigureGridView();
        }

        private void UserControlDoiTuongGiaoDich_Load(object sender, EventArgs e)
        {
            txtTimKiem.Text = string.Empty;
            txtTimKiem.ForeColor = Color.Black;
            LogHelper.GhiLog(_dbFactory, "Quản lý đối tượng giao dịch", CURRENT_USER_ID); // ghi log

            LoadDanhSach();
        }

        // --- Cấu hình DataGridView ---
        private void ConfigureGridView()
        {
            // YÊU CẦU: Xóa dòng thừa của bảng
            kryptonDataGridView1.AllowUserToAddRows = false;

            kryptonDataGridView1.AutoGenerateColumns = true;
            kryptonDataGridView1.ReadOnly = true;
            kryptonDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            kryptonDataGridView1.MultiSelect = false;
            kryptonDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // --- Xử lý sự kiện Đúp chuột để Sửa ---
        private void KryptonDataGridView1_DoubleClick(object sender, EventArgs e)
        {
            BtnSua_Click(sender, e);
        }

        // ... (ApplyGridViewHeaders, LoadDanhSach, TimKiemVaLoc giữ nguyên) ...

        private void ApplyGridViewHeaders()
        {
            // *FIX: Chỉ hiển thị 2 cột Tên Đối Tượng và Ghi Chú*
            if (kryptonDataGridView1.Columns.Count > 0)
            {
                foreach (DataGridViewColumn col in kryptonDataGridView1.Columns)
                {
                    col.Visible = false;
                }

                if (kryptonDataGridView1.Columns.Contains("TenDoiTuong"))
                {
                    kryptonDataGridView1.Columns["TenDoiTuong"].HeaderText = "Tên Đối Tượng";
                    kryptonDataGridView1.Columns["TenDoiTuong"].Visible = true;
                }
                if (kryptonDataGridView1.Columns.Contains("GhiChu"))
                {
                    kryptonDataGridView1.Columns["GhiChu"].HeaderText = "Ghi Chú";
                    kryptonDataGridView1.Columns["GhiChu"].Visible = true;
                }
            }
        }

        // --- Load dữ liệu (Read) ---
        public void LoadDanhSach()
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    _fullList = db.DoiTuongGiaoDichs
                                        .Where(d => d.MaNguoiDung == MA_NGUOI_DUNG_HIEN_TAI)
                                        .OrderBy(d => d.TenDoiTuong)
                                        .AsNoTracking()
                                        .ToList();

                    TimKiemVaLoc();
                    ApplyGridViewHeaders();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Logic Tra cứu ---
        private void TimKiemVaLoc()
        {
            string tuKhoa = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(tuKhoa))
            {
                bsDoiTuong.DataSource = _fullList;
            }
            else
            {
                var ketQuaLoc = _fullList.Where(p =>
                    (p.TenDoiTuong != null && p.TenDoiTuong.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
                    (p.GhiChu != null && p.GhiChu.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase))
                ).ToList();

                bsDoiTuong.DataSource = ketQuaLoc;
            }
            bsDoiTuong.ResetBindings(false);
        }

        private void TxtTimKiem_TextChanged(object sender, EventArgs e)
        {
            TimKiemVaLoc();
        }

        // --- Logic Nút Thêm (Create) & Sửa (Update) ---
        private void BtnThem_Click(object sender, EventArgs e)
        {
            OnOpenEditForm?.Invoke(this, 0);
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (bsDoiTuong.Current == null)
            {
                MessageBox.Show("Vui lòng chọn đối tượng cần Sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedId = ((DoiTuongGiaoDich)bsDoiTuong.Current).MaDoiTuongGiaoDich;
            OnOpenEditForm?.Invoke(this, selectedId);
        }

        // --- Logic Nút Xóa (Delete) ---
        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (bsDoiTuong.Current == null)
            {
                MessageBox.Show("Vui lòng chọn đối tượng cần Xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedObj = (DoiTuongGiaoDich)bsDoiTuong.Current;

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa đối tượng '{selectedObj.TenDoiTuong}' không? \nLưu ý: Thao tác này sẽ xóa **tất cả giao dịch và ngân sách liên quan** đến đối tượng này.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (var db = _dbFactory.CreateDbContext())
                    {
                        // 1. Tải đối tượng cần xóa cùng với các entities liên quan
                        var entityToDelete = db.DoiTuongGiaoDichs
                                                // Include Giao Dịch
                                                .Include(d => d.GiaoDichs)
                                                // Include Ngân sách liên quan (nếu có)
                                                .Include(d => d.DoiTuongGiaoDichNganSachs)
                                                .FirstOrDefault(d => d.MaDoiTuongGiaoDich == selectedObj.MaDoiTuongGiaoDich);

                        if (entityToDelete != null)
                        {
                            // 2. Xóa các entities con trước (Giao Dịch, Ngân sách)
                            if (entityToDelete.GiaoDichs != null)
                            {
                                db.GiaoDichs.RemoveRange(entityToDelete.GiaoDichs);
                            }
                            if (entityToDelete.DoiTuongGiaoDichNganSachs != null)
                            {
                                db.DoiTuongGiaoDichNganSachs.RemoveRange(entityToDelete.DoiTuongGiaoDichNganSachs);
                            }

                            // 3. Xóa đối tượng cha
                            db.DoiTuongGiaoDichs.Remove(entityToDelete);
                            db.SaveChanges();
                        }
                    }
                    LoadDanhSach();
                }
                catch (Exception ex)
                {
                    string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    MessageBox.Show($"Lỗi khi xóa dữ liệu: {errorMessage}", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}