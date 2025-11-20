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
    public partial class FrmThemGiaoDich : Form
    {
        public Action OnDataAdded; // Callback để UserControl load lại DGV
        private int? _maGiaoDich = null; // null = thêm mới, có giá trị = sửa
        string strConnectionString = "Data Source=DESKTOP-6QOFBT9\\SQLEXPRESS;Initial Catalog=QLTCCN;Integrated Security=True;TrustServerCertificate=True";
        public FrmThemGiaoDich()
        {
            InitializeComponent();
            this.Load += FrmThemGiaoDich_Load;
        }
        public FrmThemGiaoDich(int maGiaoDich, string tenGiaoDich, string ghiChu, decimal soTien, DateTime ngayGiaoDich, int maDoiTuong, int maTaiKhoan)
        {
            InitializeComponent();
            _maGiaoDich = maGiaoDich;

            txtTenGiaoDich.Text = tenGiaoDich;
            rtbGhiChu.Text = ghiChu;
            txtSoTien.Text = soTien.ToString();
            dtNgayGiaoDich.Value = ngayGiaoDich;

            // Load combobox trước, sau đó set SelectedValue
            LoadComboBoxes();
            cbDoiTuong.SelectedValue = maDoiTuong;
            cbTaiKhoan.SelectedValue = maTaiKhoan;
        }
        private void FrmThemGiaoDich_Load(object sender, EventArgs e)
        {
            // Setup RichTextBox
            rtbGhiChu.Multiline = true;
            rtbGhiChu.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbGhiChu.WordWrap = true;
            LoadComboBoxes();
        }
        private void LoadComboBoxes()
        {
            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                conn.Open();

                // Load đối tượng giao dịch
                SqlDataAdapter daDT = new SqlDataAdapter(
                    "SELECT MaDoiTuongGiaoDich, TenDoiTuong FROM DOI_TUONG_GIAO_DICH", conn);
                DataTable dtDT = new DataTable();
                daDT.Fill(dtDT);

                cbDoiTuong.DataSource = dtDT;
                cbDoiTuong.DisplayMember = "TenDoiTuong";
                cbDoiTuong.ValueMember = "MaDoiTuongGiaoDich";

                // Load tài khoản thanh toán
                SqlDataAdapter daTK = new SqlDataAdapter(
                    "SELECT MaTaiKhoanThanhToan, TenTaiKhoan FROM TAI_KHOAN_THANH_TOAN", conn);
                DataTable dtTK = new DataTable();
                daTK.Fill(dtTK);

                cbTaiKhoan.DataSource = dtTK;
                cbTaiKhoan.DisplayMember = "TenTaiKhoan";
                cbTaiKhoan.ValueMember = "MaTaiKhoanThanhToan";
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();

                    string query;

                    if (_maGiaoDich == null)
                    {
                        // Thêm mới
                        query = @"
                    INSERT INTO GIAO_DICH
                    (TenGiaoDich, GhiChu, SoTien, NgayGiaoDich, MaDoiTuongGiaoDich, MaTaiKhoanThanhToan)
                    VALUES
                    (@TenGiaoDich, @GhiChu, @SoTien, @NgayGiaoDich, @MaDoiTuongGiaoDich, @MaTaiKhoanThanhToan)";
                    }
                    else
                    {
                        // Cập nhật
                        query = @"
                    UPDATE GIAO_DICH
                    SET TenGiaoDich = @TenGiaoDich,
                        GhiChu = @GhiChu,
                        SoTien = @SoTien,
                        NgayGiaoDich = @NgayGiaoDich,
                        MaDoiTuongGiaoDich = @MaDoiTuongGiaoDich,
                        MaTaiKhoanThanhToan = @MaTaiKhoanThanhToan
                    WHERE MaGiaoDich = @MaGiaoDich";
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TenGiaoDich", txtTenGiaoDich.Text);
                    cmd.Parameters.AddWithValue("@GhiChu", rtbGhiChu.Text);
                    cmd.Parameters.AddWithValue("@SoTien", decimal.Parse(txtSoTien.Text));
                    cmd.Parameters.AddWithValue("@NgayGiaoDich", dtNgayGiaoDich.Value.Date);
                    cmd.Parameters.AddWithValue("@MaDoiTuongGiaoDich", cbDoiTuong.SelectedValue);
                    cmd.Parameters.AddWithValue("@MaTaiKhoanThanhToan", cbTaiKhoan.SelectedValue);

                    if (_maGiaoDich != null)
                    {
                        cmd.Parameters.AddWithValue("@MaGiaoDich", _maGiaoDich.Value);
                    }

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show(_maGiaoDich == null ? "Thêm giao dịch thành công!" : "Cập nhật giao dịch thành công!", "Thông báo");

                OnDataAdded?.Invoke();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form hiện tại
        }
    }
}
