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
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private int _idDoiTuong = 0;
        private const int MA_NGUOI_DUNG_HIEN_TAI = 1;

        public FrmChinhSuaDoiTuongGiaoDich(IDbContextFactory<QLTCCNContext> dbFactory)
        {
            InitializeComponent();
            _dbFactory = dbFactory;

            this.Load += FrmChinhSuaDoiTuongGiaoDich_Load;
            this.btnLuu.Click += btnLuu_Click;
            this.btnHuy.Click += btnHuy_Click;
        }

        public void SetId(int id)
        {
            _idDoiTuong = id;
            this.Text = (id == 0) ? "Thêm Đối Tượng Giao Dịch Mới" : "Chỉnh Sửa Đối Tượng Giao Dịch";

            if (_idDoiTuong > 0)
            {
                LoadDataForEdit(_idDoiTuong);
            }
            else
            {
                txtTen.Text = string.Empty;
                txtGhiChu.Text = string.Empty;
            }
        }

        private void FrmChinhSuaDoiTuongGiaoDich_Load(object sender, EventArgs e)
        {
            // Không cần logic ở đây
        }

        private void LoadDataForEdit(int id)
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    var obj = db.DoiTuongGiaoDichs.AsNoTracking().FirstOrDefault(d => d.MaDoiTuongGiaoDich == id);
                    if (obj != null)
                    {
                        txtTen.Text = obj.TenDoiTuong;
                        txtGhiChu.Text = obj.GhiChu;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu chỉnh sửa: {ex.Message}", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string tenDoiTuong = txtTen.Text.Trim();
            string ghiChu = txtGhiChu.Text.Trim();

            if (string.IsNullOrEmpty(tenDoiTuong))
            {
                MessageBox.Show("Tên đối tượng không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTen.Focus();
                return;
            }

            using (var db = _dbFactory.CreateDbContext())
            {
                bool isDuplicate = db.DoiTuongGiaoDichs.Any(p =>
                    p.TenDoiTuong.Equals(tenDoiTuong) &&
                    p.MaNguoiDung == MA_NGUOI_DUNG_HIEN_TAI &&
                    p.MaDoiTuongGiaoDich != _idDoiTuong);

                if (isDuplicate)
                {
                    MessageBox.Show("Đã tồn tại đối tượng này.", "Trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTen.Focus();
                    return;
                }

                try
                {
                    if (_idDoiTuong == 0) // THÊM MỚI
                    {
                        var newObj = new DoiTuongGiaoDich
                        {
                            TenDoiTuong = tenDoiTuong,
                            GhiChu = ghiChu,
                            MaNguoiDung = MA_NGUOI_DUNG_HIEN_TAI
                        };
                        db.DoiTuongGiaoDichs.Add(newObj);
                    }
                    else // CẬP NHẬT
                    {
                        var objToUpdate = db.DoiTuongGiaoDichs.FirstOrDefault(d => d.MaDoiTuongGiaoDich == _idDoiTuong);
                        if (objToUpdate != null)
                        {
                            objToUpdate.TenDoiTuong = tenDoiTuong;
                            objToUpdate.GhiChu = ghiChu;
                        }
                    }

                    db.SaveChanges();

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}