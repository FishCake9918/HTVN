using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace Demo_Layout
{
    public delegate void OpenNganSachFormHandler(object sender, int nganSachId);

    public partial class UserControlNganSach : UserControl
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private BindingSource bsNganSach = new BindingSource();
        private List<NganSachDisplayModel> _fullList = new List<NganSachDisplayModel>();
        private const int MA_NGUOI_DUNG_HIEN_TAI = 1;

        public event OpenNganSachFormHandler OnOpenEditForm;

        // Constructor
        public UserControlNganSach(IDbContextFactory<QLTCCNContext> dbFactory)
        {
            InitializeComponent();
            _dbFactory = dbFactory;

            kryptonDataGridView1.DataSource = bsNganSach;

            // Đăng ký sự kiện
            this.Load += (s, e) => LoadDanhSach();
            this.txtTimKiem.TextChanged += TxtTimKiem_TextChanged;
            this.btnThem.Click += BtnThem_Click;
            this.btnSua.Click += BtnSua_Click;
            this.btnXoa.Click += BtnXoa_Click;

            ConfigureGridView();
        }
        private void UserControlNganSach_Load(object sender, EventArgs e)
        {
            txtTimKiem.Text = string.Empty;
            txtTimKiem.ForeColor = Color.Black;
            LogHelper.GhiLog(_dbFactory, "Quản lý ngân sách", CURRENT_USER_ID); // ghi log

            LoadDanhSach();
        }
        // --- Cấu hình DataGridView ---

        private void ConfigureGridView()
        {
            kryptonDataGridView1.AutoGenerateColumns = false;
            kryptonDataGridView1.ReadOnly = true;
            kryptonDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            kryptonDataGridView1.MultiSelect = false;
            kryptonDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            kryptonDataGridView1.Columns.Clear();

            // Thêm các cột hiển thị
            kryptonDataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaNganSach", HeaderText = "ID", DataPropertyName = "MaNganSach", Visible = false });
            kryptonDataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "DanhMuc", HeaderText = "Danh mục", DataPropertyName = "TenDanhMuc" });
            kryptonDataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoTien", HeaderText = "Số tiền NS", DataPropertyName = "SoTien", DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            kryptonDataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayBatDau", HeaderText = "Bắt đầu", DataPropertyName = "NgayBatDau", DefaultCellStyle = { Format = "dd/MM/yyyy" } });
            kryptonDataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayKetThuc", HeaderText = "Kết thúc", DataPropertyName = "NgayKetThuc", DefaultCellStyle = { Format = "dd/MM/yyyy" } });
        }

        // --- Load dữ liệu (Read) ---
        public void LoadDanhSach()
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    // SỬA LỖI: Dùng tên DbSet 'BangNganSachs' và tên Navigation Property 'DanhMucChiTieu'
                    _fullList = db.BangNganSachs
                                    .Include(n => n.DanhMucChiTieu)
                                    .Where(n => n.MaNguoiDung == MA_NGUOI_DUNG_HIEN_TAI)
                                    .OrderByDescending(n => n.NgayBatDau)
                                    .Select(n => new NganSachDisplayModel
                                    {
                                        MaNganSach = n.MaNganSach,
                                        SoTien = n.SoTien,
                                        NgayBatDau = n.NgayBatDau,
                                        NgayKetThuc = n.NgayKetThuc,
                                        TenDanhMuc = n.DanhMucChiTieu.TenDanhMuc // Lấy tên danh mục
                                    })
                                    .ToList();

                    TimKiemVaLoc();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu ngân sách: {ex.Message}", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Logic Tra cứu (Search) ---
        private void TimKiemVaLoc()
        {
            string tuKhoa = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(tuKhoa))
            {
                bsNganSach.DataSource = _fullList;
            }
            else
            {
                // Tra cứu theo Tên Danh mục
                var ketQuaLoc = _fullList.Where(p =>
                    (p.TenDanhMuc != null && p.TenDanhMuc.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase))
                ).ToList();

                bsNganSach.DataSource = ketQuaLoc;
            }
            bsNganSach.ResetBindings(false);
        }

        private void TxtTimKiem_TextChanged(object sender, EventArgs e)
        {
            TimKiemVaLoc();
        }

        // --- Nút Thêm (Create) ---
        private void BtnThem_Click(object sender, EventArgs e)
        {
            OnOpenEditForm?.Invoke(this, 0); // Mở form với ID = 0 (Thêm mới)
        }

        // --- Nút Sửa (Update) ---
        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (bsNganSach.Current == null)
            {
                MessageBox.Show("Vui lòng chọn ngân sách cần Sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy MaNganSach của đối tượng đang chọn
            int selectedId = ((NganSachDisplayModel)bsNganSach.Current).MaNganSach;
            OnOpenEditForm?.Invoke(this, selectedId); // Mở form với ID đã chọn
        }

        // --- Nút Xóa (Delete) ---
        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (bsNganSach.Current == null)
            {
                MessageBox.Show("Vui lòng chọn ngân sách cần Xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedNs = (NganSachDisplayModel)bsNganSach.Current;

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa ngân sách '{selectedNs.TenDanhMuc}' ({selectedNs.NgayBatDau:dd/MM/yyyy} - {selectedNs.NgayKetThuc:dd/MM/yyyy}) không?\nThao tác này sẽ xóa vĩnh viễn dữ liệu này khỏi hệ thống.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (var db = _dbFactory.CreateDbContext())
                    {
                        var entityToDelete = db.BangNganSachs.FirstOrDefault(n => n.MaNganSach == selectedNs.MaNganSach);

                        if (entityToDelete != null)
                        {
                            db.BangNganSachs.Remove(entityToDelete);
                            db.SaveChanges();
                        }
                    }
                    LoadDanhSach(); // Tải lại danh sách sau khi xóa thành công
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa dữ liệu: {ex.Message}", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    // --- MODEL DÙNG ĐỂ HIỂN THỊ TRÊN DATA GRID VIEW ---
    public class NganSachDisplayModel
    {
        public int MaNganSach { get; set; }
        public decimal SoTien { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string TenDanhMuc { get; set; }
    }
}