using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace Demo_Layout
{

    public partial class QuanLyThongBao : Form
    {
        private List<ThongBao> danh_sach_thong_bao;
        private int ma_thong_bao_tiep_theo = 4;

        public QuanLyThongBao()
        {
            InitializeComponent();
            // Tìm kiếm tự động khi gõ
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;

            KhoiTaoDuLieuMau();
            HienThiDanhSach();
        }

        private void KhoiTaoDuLieuMau()
        {
            danh_sach_thong_bao = new List<ThongBao>
            {
                new ThongBao { MaThongBao = 1, TieuDe = "Khuyến mãi lớn", NoiDung = "Giảm giá 50% toàn bộ sản phẩm.", NgayTao = DateTime.Now.AddDays(-5), RoleTao = "Super Admin" },
                new ThongBao { MaThongBao = 2, TieuDe = "Bảo trì hệ thống", NoiDung = "Hệ thống sẽ bảo trì vào 10/11.", NgayTao = DateTime.Now.AddDays(-2), RoleTao = "Moderator" },
                new ThongBao { MaThongBao = 3, TieuDe = "Thông báo mới nhất", NoiDung = "Cập nhật ứng dụng phiên bản 2.0.", NgayTao = DateTime.Now, RoleTao = "Admin" }
            };
        }

        // HÀM HIỂN THỊ HỖ TRỢ TÌM KIẾM
        private void HienThiDanhSach(string tu_khoa_tim_kiem = null)
        {
            var du_lieu_loc = danh_sach_thong_bao.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(tu_khoa_tim_kiem))
            {
                string tu_khoa = tu_khoa_tim_kiem.ToLower().Trim();
                // Lọc theo Tiêu đề HOẶC Nội dung
                du_lieu_loc = du_lieu_loc.Where(tb =>
                    tb.TieuDe.ToLower().Contains(tu_khoa) ||
                    tb.NoiDung.ToLower().Contains(tu_khoa)
                );
            }
            dgvDanhSach.DataSource = null;
            dgvDanhSach.DataSource = du_lieu_loc.OrderByDescending(tb => tb.NgayTao).ToList();
            dgvDanhSach.Columns["MaThongBao"].HeaderText = "Mã TB";
            dgvDanhSach.Columns["TieuDe"].HeaderText = "Tiêu đề";
            dgvDanhSach.Columns["NoiDung"].HeaderText = "Nội dung";
            dgvDanhSach.Columns["NgayTao"].HeaderText = "Ngày tạo";
            dgvDanhSach.Columns["RoleTao"].HeaderText = "Role";
        }

        // Tìm kiếm tự động
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            // Tự động lọc nếu người dùng gõ
            HienThiDanhSach(txtTimKiem.Text);
        }

        //CÁC PHƯƠNG THỨC CRUD
        private void dgvDanhSach_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDanhSach.SelectedRows.Count > 0)
            {
                btnCapNhat.Enabled = true;
                btnXoa.Enabled = true;
            }
            else
            {
                btnCapNhat.Enabled = false;
                btnXoa.Enabled = false;
            }
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            using (var frmTao = new TaoCapNhatThongBao())
            {
                var result = frmTao.ShowDialog();
                if (result == DialogResult.OK && frmTao.ThongBaoHienTai != null)
                {
                    ThongBao thong_bao_moi = frmTao.ThongBaoHienTai;
                    thong_bao_moi.MaThongBao = ma_thong_bao_tiep_theo++;
                    thong_bao_moi.NgayTao = DateTime.Now;

                    danh_sach_thong_bao.Add(thong_bao_moi);
                    HienThiDanhSach();
                    MessageBox.Show("Đã tạo thông báo mới thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (dgvDanhSach.SelectedRows.Count == 0) return;

            var dong_chon = dgvDanhSach.SelectedRows[0];
            var ma_tb = (int)dong_chon.Cells["MaThongBao"].Value;
            ThongBao tb_hien_tai = danh_sach_thong_bao.FirstOrDefault(tb => tb.MaThongBao == ma_tb);

            if (tb_hien_tai != null)
            {
                using (var frmCapNhat = new TaoCapNhatThongBao(tb_hien_tai))
                {
                    var result = frmCapNhat.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        HienThiDanhSach();
                        MessageBox.Show("Thông báo đã được **cập nhật** thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDanhSach.SelectedRows.Count == 0) return;

            var dong_chon = dgvDanhSach.SelectedRows[0];
            var tieu_de = dong_chon.Cells["TieuDe"].Value.ToString();
            var ma_tb_xoa = (int)dong_chon.Cells["MaThongBao"].Value;

            var xac_nhan = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa thông báo **'{tieu_de}'** (Mã TB: {ma_tb_xoa}) không?",
                "Xác nhận Xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (xac_nhan == DialogResult.Yes)
            {
                ThongBao tb_can_xoa = danh_sach_thong_bao.FirstOrDefault(tb => tb.MaThongBao == ma_tb_xoa);

                if (tb_can_xoa != null)
                {
                    danh_sach_thong_bao.Remove(tb_can_xoa);
                    HienThiDanhSach();

                    MessageBox.Show("Thông báo đã được **xóa** thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }

    public class ThongBao
    {
        public int MaThongBao { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayTao { get; set; }
        public string RoleTao { get; set; } 
    }

}