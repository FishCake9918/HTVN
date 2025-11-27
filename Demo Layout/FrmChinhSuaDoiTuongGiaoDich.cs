using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Data;

namespace Demo_Layout
{
    public partial class FrmChinhSuaDoiTuongGiaoDich : Form
    {
        public Action OnDataSaved;
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly CurrentUserContext _userContext; // <-- Inject
        private int _idDoiTuong = 0;

        public FrmChinhSuaDoiTuongGiaoDich(
            IDbContextFactory<QLTCCNContext> dbFactory,
            CurrentUserContext userContext) // <-- Inject
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _userContext = userContext;

            this.btnLuu.Click += btnLuu_Click;
            this.btnHuy.Click += btnHuy_Click;
        }

        public void SetId(int id)
        {
            _idDoiTuong = id;
            this.Text = (id == 0) ? "Thêm Đối Tượng" : "Sửa Đối Tượng";
            if (_idDoiTuong > 0) LoadDataForEdit(_idDoiTuong);
            else { txtTen.Text = ""; txtGhiChu.Text = ""; }
        }

        private void LoadDataForEdit(int id)
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    var obj = db.DoiTuongGiaoDichs.AsNoTracking().FirstOrDefault(d => d.MaDoiTuongGiaoDich == id);
                    if (obj != null) { txtTen.Text = obj.TenDoiTuong; txtGhiChu.Text = obj.GhiChu; }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnHuy_Click(object sender, EventArgs e) { this.DialogResult = DialogResult.Cancel; this.Close(); }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_userContext.MaNguoiDung == null) return;
            string tenDoiTuong = txtTen.Text.Trim();
            string ghiChu = txtGhiChu.Text.Trim();

            if (string.IsNullOrEmpty(tenDoiTuong)) { MessageBox.Show("Tên trống."); return; }

            using (var db = _dbFactory.CreateDbContext())
            {
                // SỬA: Check trùng theo User hiện tại
                bool isDuplicate = db.DoiTuongGiaoDichs.Any(p => p.TenDoiTuong.Equals(tenDoiTuong) && p.MaNguoiDung == _userContext.MaNguoiDung && p.MaDoiTuongGiaoDich != _idDoiTuong);
                if (isDuplicate) { MessageBox.Show("Trùng tên."); return; }

                try
                {
                    if (_idDoiTuong == 0)
                    {
                        var newObj = new DoiTuongGiaoDich
                        {
                            TenDoiTuong = tenDoiTuong,
                            GhiChu = ghiChu,
                            MaNguoiDung = _userContext.MaNguoiDung.Value // <-- ID thật
                        };
                        db.DoiTuongGiaoDichs.Add(newObj);
                    }
                    else
                    {
                        var objToUpdate = db.DoiTuongGiaoDichs.FirstOrDefault(d => d.MaDoiTuongGiaoDich == _idDoiTuong);
                        if (objToUpdate != null) { objToUpdate.TenDoiTuong = tenDoiTuong; objToUpdate.GhiChu = ghiChu; }
                    }
                    db.SaveChanges();
                    OnDataSaved?.Invoke();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }
    }
}