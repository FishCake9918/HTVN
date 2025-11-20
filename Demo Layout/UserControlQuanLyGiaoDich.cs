using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo_Layout
{
    public partial class UserControlQuanLyGiaoDich : UserControl
    {
        // Chuỗi kết nối
        string strConnectionString = "Data Source=DESKTOP-6QOFBT9\\SQLEXPRESS;Initial Catalog=QLTCCN;Integrated Security=True;TrustServerCertificate=True";

        // Đối tượng kết nối dữ liệu
        SqlConnection conn = null;

        // Đối tượng thực hiện vận chuyển dữ liệu
        SqlDataAdapter da = null;

        // Đối tượng chứa dữ liệu trong bộ nhớ
        DataSet ds = null;
        private DataTable dtGiaoDich;
        public UserControlQuanLyGiaoDich()
        {
            InitializeComponent();
            this.Load += UserControlQuanLyGiaoDich_Load;
        }
        private bool isPlaceholderActive = true; // Biến để theo dõi trạng thái Placeholder
        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();

                    string query = @"
                SELECT gd.MaGiaoDich,
                       gd.MaDoiTuongGiaoDich,
                       gd.MaTaiKhoanThanhToan,
                       dt.TenDoiTuong,
                       gd.TenGiaoDich,
                       gd.GhiChu,
                       gd.SoTien,
                       gd.NgayGiaoDich,
                       tk.TenTaiKhoan
                FROM GIAO_DICH gd
                LEFT JOIN DOI_TUONG_GIAO_DICH dt
                    ON gd.MaDoiTuongGiaoDich = dt.MaDoiTuongGiaoDich
                LEFT JOIN TAI_KHOAN_THANH_TOAN tk
                    ON gd.MaTaiKhoanThanhToan = tk.MaTaiKhoanThanhToan
            ";

                    // TẠO DATATABLE GỐC
                    dtGiaoDich = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dtGiaoDich);

                    // GÁN VÀO DATAGRID
                    kryptonDataGridView1.DataSource = dtGiaoDich;

                    // Ẩn cột ID
                    kryptonDataGridView1.Columns["MaDoiTuongGiaoDich"].Visible = false;
                    kryptonDataGridView1.Columns["MaTaiKhoanThanhToan"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            // Sự kiện xảy ra khi click vào TextBox
            if (isPlaceholderActive)
            {
                txtTimKiem.Text = ""; // Xóa chữ "Tìm kiếm..."
                txtTimKiem.ForeColor = Color.Black; // Chuyển sang màu chữ bình thường (ví dụ: Đen)
                isPlaceholderActive = false;
            }
        }
        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            // Sự kiện xảy ra khi rời khỏi TextBox
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                // Nếu không có chữ nào được điền, khôi phục Placeholder
                txtTimKiem.Text = " Tìm kiếm...";
                txtTimKiem.ForeColor = Color.Gray; // Đặt lại màu chữ mờ
                isPlaceholderActive = true;
            }
            // Nếu có chữ, thì giữ nguyên chữ đó và màu Đen.
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            FrmThemGiaoDich frm = new FrmThemGiaoDich();
            // Truyền callback để load lại dữ liệu sau khi thêm thành công
            frm.OnDataAdded = LoadData;
            frm.ShowDialog();
        }

        private void UserControlQuanLyGiaoDich_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (kryptonDataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một giao dịch để xóa.", "Thông báo");
                return;
            }

            // Lấy MaGiaoDich của hàng đang chọn
            int maGiaoDich = Convert.ToInt32(kryptonDataGridView1.SelectedRows[0].Cells["MaGiaoDich"].Value);

            // Xác nhận trước khi xóa
            DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa giao dịch này?",
                                              "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.No) return;

            // Thực hiện xóa trong DB
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM GIAO_DICH WHERE MaGiaoDich = @MaGiaoDich";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaGiaoDich", maGiaoDich);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Xóa giao dịch thành công!", "Thông báo");

                // Load lại DataGridView
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (kryptonDataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một giao dịch để sửa.", "Thông báo");
                return;
            }

            // Lấy các giá trị cần thiết
            int maGiaoDich = Convert.ToInt32(kryptonDataGridView1.SelectedRows[0].Cells["MaGiaoDich"].Value);
            string tenGiaoDich = kryptonDataGridView1.SelectedRows[0].Cells["TenGiaoDich"].Value?.ToString() ?? "";
            string ghiChu = kryptonDataGridView1.SelectedRows[0].Cells["GhiChu"].Value?.ToString() ?? "";
            decimal soTien = 0;
            decimal.TryParse(kryptonDataGridView1.SelectedRows[0].Cells["SoTien"].Value?.ToString(), out soTien);
            DateTime ngayGiaoDich = DateTime.Now;
            DateTime.TryParse(kryptonDataGridView1.SelectedRows[0].Cells["NgayGiaoDich"].Value?.ToString(), out ngayGiaoDich);
            int maDoiTuong = Convert.ToInt32(kryptonDataGridView1.SelectedRows[0].Cells["MaDoiTuongGiaoDich"].Value);
            int maTaiKhoan = Convert.ToInt32(kryptonDataGridView1.SelectedRows[0].Cells["MaTaiKhoanThanhToan"].Value);

            // Mở form sửa với các tham số này
            FrmThemGiaoDich frm = new FrmThemGiaoDich(maGiaoDich, tenGiaoDich, ghiChu, soTien, ngayGiaoDich, maDoiTuong, maTaiKhoan);
            frm.OnDataAdded = LoadData; // callback load lại DGV sau khi sửa
            frm.ShowDialog();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (dtGiaoDich == null) return;

            string filter = txtTimKiem.Text.Trim().Replace("'", "''"); // chống lỗi ký tự '

            // Lọc theo các cột bạn muốn tìm
            dtGiaoDich.DefaultView.RowFilter =
                $"TenGiaoDich LIKE '%{filter}%' OR " +
                $"TenDoiTuong LIKE '%{filter}%' OR " +
                $"TenTaiKhoan LIKE '%{filter}%' OR " +
                $"GhiChu LIKE '%{filter}%'";
        }
    }
}
