using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Krypton.Toolkit;
using System.Drawing;
using Microsoft.Extensions.DependencyInjection;

namespace Demo_Layout
{
    public partial class UserControlDoiTuongGiaoDich : UserControl
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider; // <-- Thêm
        private readonly CurrentUserContext _userContext;   // <-- Thêm
        private BindingSource bsDoiTuong = new BindingSource();
        private List<DoiTuongGiaoDich> _fullList = new List<DoiTuongGiaoDich>();

        public UserControlDoiTuongGiaoDich(
            IDbContextFactory<QLTCCNContext> dbFactory,
            IServiceProvider serviceProvider,
            CurrentUserContext userContext)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
            _userContext = userContext;

            kryptonDataGridView1.DataSource = bsDoiTuong;
            this.Load += UserControlDoiTuongGiaoDich_Load;
            this.txtTimKiem.TextChanged += TxtTimKiem_TextChanged;
            this.btnThem.Click += BtnThem_Click;
            this.btnSua.Click += BtnSua_Click;
            this.btnXoa.Click += BtnXoa_Click;
            this.kryptonDataGridView1.DoubleClick += KryptonDataGridView1_DoubleClick;
            ConfigureGridView();
        }

        private void UserControlDoiTuongGiaoDich_Load(object sender, EventArgs e)
        {
            if (_userContext.MaNguoiDung == null) return;
            txtTimKiem.Text = string.Empty;
            txtTimKiem.ForeColor = Color.Black;
            LoadDanhSach();
        }

        private void ConfigureGridView()
        {
            kryptonDataGridView1.AllowUserToAddRows = false;
            kryptonDataGridView1.AutoGenerateColumns = true;
            kryptonDataGridView1.ReadOnly = true;
            kryptonDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            kryptonDataGridView1.MultiSelect = false;
            kryptonDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void KryptonDataGridView1_DoubleClick(object sender, EventArgs e) => BtnSua_Click(sender, e);

        private void ApplyGridViewHeaders()
        {
            if (kryptonDataGridView1.Columns.Count > 0)
            {
                foreach (DataGridViewColumn col in kryptonDataGridView1.Columns) col.Visible = false;
                if (kryptonDataGridView1.Columns.Contains("TenDoiTuong")) { kryptonDataGridView1.Columns["TenDoiTuong"].HeaderText = "Tên Đối Tượng"; kryptonDataGridView1.Columns["TenDoiTuong"].Visible = true; }
                if (kryptonDataGridView1.Columns.Contains("GhiChu")) { kryptonDataGridView1.Columns["GhiChu"].HeaderText = "Ghi Chú"; kryptonDataGridView1.Columns["GhiChu"].Visible = true; }
            }
        }

        public void LoadDanhSach()
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    // SỬA: Lọc theo Context User
                    _fullList = db.DoiTuongGiaoDichs
                                        .Where(d => d.MaNguoiDung == _userContext.MaNguoiDung)
                                        .OrderBy(d => d.TenDoiTuong)
                                        .AsNoTracking()
                                        .ToList();
                    TimKiemVaLoc();
                    ApplyGridViewHeaders();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void TimKiemVaLoc()
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(tuKhoa)) bsDoiTuong.DataSource = _fullList;
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
        private void TxtTimKiem_TextChanged(object sender, EventArgs e) => TimKiemVaLoc();

        // --- XỬ LÝ MỞ FORM TRỰC TIẾP ---
        private void BtnThem_Click(object sender, EventArgs e)
        {
            var frm = ActivatorUtilities.CreateInstance<FrmChinhSuaDoiTuongGiaoDich>(_serviceProvider);
            frm.SetId(0);
            frm.OnDataSaved = LoadDanhSach;
            frm.ShowDialog();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (bsDoiTuong.Current == null) { MessageBox.Show("Vui lòng chọn đối tượng."); return; }
            int selectedId = ((DoiTuongGiaoDich)bsDoiTuong.Current).MaDoiTuongGiaoDich;

            var frm = ActivatorUtilities.CreateInstance<FrmChinhSuaDoiTuongGiaoDich>(_serviceProvider);
            frm.SetId(selectedId);
            frm.OnDataSaved = LoadDanhSach;
            frm.ShowDialog();
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (bsDoiTuong.Current == null) { MessageBox.Show("Vui lòng chọn đối tượng."); return; }
            var selectedObj = (DoiTuongGiaoDich)bsDoiTuong.Current;

            if (MessageBox.Show($"Xóa '{selectedObj.TenDoiTuong}'? Dữ liệu liên quan cũng sẽ mất.", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (var db = _dbFactory.CreateDbContext())
                    {
                        var entityToDelete = db.DoiTuongGiaoDichs
                                                .Include(d => d.GiaoDichs)
                                                .Include(d => d.DoiTuongGiaoDichNganSachs)
                                                .FirstOrDefault(d => d.MaDoiTuongGiaoDich == selectedObj.MaDoiTuongGiaoDich);
                        if (entityToDelete != null)
                        {
                            if (entityToDelete.GiaoDichs != null) db.GiaoDichs.RemoveRange(entityToDelete.GiaoDichs);
                            if (entityToDelete.DoiTuongGiaoDichNganSachs != null) db.DoiTuongGiaoDichNganSachs.RemoveRange(entityToDelete.DoiTuongGiaoDichNganSachs);
                            db.DoiTuongGiaoDichs.Remove(entityToDelete);
                            db.SaveChanges();
                        }
                    }
                    LoadDanhSach();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }
    }
}