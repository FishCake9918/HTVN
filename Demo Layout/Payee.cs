using System;
using System.Windows.Forms;

namespace Demo_Layout
{
    public partial class Payee : Form
    {
        // Đây là "cơ sở dữ liệu" demo trong bộ nhớ của chúng ta.
        // Dùng 'static' để nó có thể được truy cập từ form ChinhSuaPayee
        public static List<PayeeData> DanhSachPayees = new List<PayeeData>();

        // BindingSource giúp quản lý dữ liệu giữa List và DataGridView
        private BindingSource bsPayees = new BindingSource();

        public Payee()
        {
            InitializeComponent();

            // Gán các sự kiện
            // Các sự kiện Click cho 4 nút (Thêm, Sửa, Xóa, Tra Cứu) 
            // đã được gán tự động trong file Payee.Designer.cs của bạn.

            // Chúng ta cần gán thủ công sự kiện Load và TextChanged (để tra cứu tự động)
            this.Load += Payee_Load;
            this.tbTraCuu.TextChanged += tbTraCuu_TextChanged;
        }

        private void Payee_Load(object sender, EventArgs e)
        {
            // Tải 5 dòng dữ liệu demo
            LoadDemoData();

            // Thiết lập BindingSource
            bsPayees.DataSource = DanhSachPayees;

            // Gán DataGridView vào BindingSource
            dtgPayee.DataSource = bsPayees;

            // Cấu hình cột cho DataGridView
            ConfigureGridView();
        }

        private void LoadDemoData()
        {
            // Chỉ tải demo data nếu danh sách đang trống
            if (DanhSachPayees.Count == 0)
            {
                DanhSachPayees.Add(new PayeeData { Id = 1, TenDoiTuong = "Công ty Điện lực", DanhMuc = "Hóa đơn", GhiChu = "Tiền điện tháng 10" });
                DanhSachPayees.Add(new PayeeData { Id = 2, TenDoiTuong = "Công ty Nước sạch", DanhMuc = "Hóa đơn", GhiChu = "Tiền nước" });
                DanhSachPayees.Add(new PayeeData { Id = 3, TenDoiTuong = "Siêu thị Co.op", DanhMuc = "Ăn uống", GhiChu = "" });
                DanhSachPayees.Add(new PayeeData { Id = 4, TenDoiTuong = "Netflix", DanhMuc = "Giải trí", GhiChu = "Phí hàng tháng" });
                DanhSachPayees.Add(new PayeeData { Id = 5, TenDoiTuong = "Grab", DanhMuc = "Đi lại", GhiChu = "Di chuyển" });
            }
        }

        private void ConfigureGridView()
        {
            // Ẩn cột ID
            dtgPayee.Columns["Id"].Visible = false;

            // Đặt lại tên tiêu đề cột
            dtgPayee.Columns["TenDoiTuong"].HeaderText = "Tên Đối Tượng";
            dtgPayee.Columns["DanhMuc"].HeaderText = "Danh Mục";
            dtgPayee.Columns["GhiChu"].HeaderText = "Ghi Chú";

            // Tự động dãn cột
            dtgPayee.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Chỉ cho phép chọn nguyên dòng
            dtgPayee.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // Không cho phép sửa trực tiếp trên grid
            dtgPayee.ReadOnly = true;
            // Không cho phép chọn nhiều dòng
            dtgPayee.MultiSelect = false;
        }

        // Hàm làm mới lưới (đồng thời áp dụng bộ lọc tra cứu)
        private void RefreshGridData()
        {
            // Gọi lại hàm lọc để cập nhật danh sách
            TimKiemVaLoc();
            // Đảm bảo BindingSource cập nhật giao diện
            bsPayees.ResetBindings(false);
        }

        // Hàm xử lý logic tìm kiếm (dùng cho cả TextChanged và Nút bấm)
        private void TimKiemVaLoc()
        {
            string tuKhoa = tbTraCuu.Text.Trim();

            if (string.IsNullOrEmpty(tuKhoa))
            {
                // Nếu ô tra cứu rỗng, hiển thị toàn bộ danh sách
                bsPayees.DataSource = DanhSachPayees;
            }
            else
            {
                // Nếu có từ khóa, lọc danh sách (không phân biệt hoa thường)
                var ketQuaLoc = DanhSachPayees.Where(p =>
                    (p.TenDoiTuong != null && p.TenDoiTuong.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
                    (p.DanhMuc != null && p.DanhMuc.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
                    (p.GhiChu != null && p.GhiChu.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase))
                ).ToList();

                // Cập nhật BindingSource với kết quả đã lọc
                bsPayees.DataSource = ketQuaLoc;
            }

            // Đảm bảo DataGridView vẽ lại
            bsPayees.ResetBindings(false);
        }

        // RULE 3: Tra cứu tự động khi gõ
        private void tbTraCuu_TextChanged(object sender, EventArgs e)
        {
            TimKiemVaLoc();
        }

        // Nút Tra Cứu (nếu người dùng muốn bấm)
        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            TimKiemVaLoc();
        }


        // RULE 1: Nút Thêm
        private void btnThemPayee_Click(object sender, EventArgs e)
        {
            // Mở form ChinhSuaPayee ở chế độ "Thêm" (truyền vào 'null')
            // Lưu ý: Tên form là ChinhSuaPayee theo designer mới
            using (ChinhSuaPayee formThem = new ChinhSuaPayee(null))
            {
                if (formThem.ShowDialog() == DialogResult.OK)
                {
                    // Nếu nhấn Lưu, làm mới lại lưới
                    RefreshGridData();
                }
            }
        }

        // RULE 2: Nút Sửa
        private void btnSuaPayee_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
            if (bsPayees.Current == null)
            {
                MessageBox.Show("Vui lòng chọn đối tượng cần Sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy đối tượng đang được chọn
            PayeeData selectedPayee = (PayeeData)bsPayees.Current;

            // Mở form ChinhSuaPayee ở chế độ "Sửa" (truyền đối tượng được chọn)
            using (ChinhSuaPayee formSua = new ChinhSuaPayee(selectedPayee))
            {
                if (formSua.ShowDialog() == DialogResult.OK)
                {
                    // Nếu nhấn Lưu, làm mới lại lưới
                    RefreshGridData();
                }
            }
        }

        // RULE 2: Nút Xóa
        private void btnXoaPayee_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
            if (bsPayees.Current == null)
            {
                MessageBox.Show("Vui lòng chọn đối tượng cần Xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy đối tượng đang được chọn
            PayeeData selectedPayee = (PayeeData)bsPayees.Current;

            // Hiển thị hộp thoại xác nhận
            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa đối tượng '{selectedPayee.TenDoiTuong}' không?",
                                                   "Xác nhận xóa",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                // Xóa khỏi danh sách "database"
                DanhSachPayees.Remove(selectedPayee);
                // Làm mới lại lưới
                RefreshGridData();
            }
        }
    }
    public class PayeeData
    {
        public int Id { get; set; }
        public string TenDoiTuong { get; set; }
        public string DanhMuc { get; set; }
        public string GhiChu { get; set; }
    }
}