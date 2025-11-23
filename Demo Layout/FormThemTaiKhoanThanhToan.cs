using Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;
using Krypton.Toolkit;
using Microsoft.Data.SqlClient;
using System.Net.Http; // Cần thiết cho API
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Demo_Layout
{
    // Class để chứa thông tin đơn vị tiền tệ cho cmbDonViTienTe


    public partial class FormThemTaiKhoanThanhToan : Form
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private const int MA_NGUOI_DUNG_HIEN_TAI = 1;

        // Khởi tạo HttpClient một lần cho toàn bộ Form
        private readonly HttpClient _httpClient = new HttpClient();
        private const string BASE_CURRENCY = "VND";

        public FormThemTaiKhoanThanhToan(IDbContextFactory<QLTCCNContext> dbFactory)
        {
            InitializeComponent();
            _dbFactory = dbFactory;

            this.Load += FormThemTaiKhoan_Load;
            this.btnTao.Click += BtnTao_Click; // Sự kiện này là async
            this.btnQuayLai.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            this.txtSoDu.KeyPress += TxtSoDu_KeyPress;
        }

        private void FormThemTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadLoaiTaiKhoan();
            LoadCurrencies(); // Tải danh sách tiền tệ
        }

        // --- LOGIC API: TẢI DANH SÁCH TIỀN TỆ VÀO COMBOBOX ---
        private void LoadCurrencies()
        {
            // Danh sách tiền tệ cơ bản
            var currencyList = new List<CurrencyModel>
            {
                new CurrencyModel { Code = "VND", Name = "Việt Nam Đồng (VND)" },
                new CurrencyModel { Code = "USD", Name = "Đô la Mỹ (USD)" },
                new CurrencyModel { Code = "EUR", Name = "Euro (EUR)" },
                new CurrencyModel { Code = "JPY", Name = "Yên Nhật (JPY)" }
            };

            // Giả định tên ComboBox là cmbDonViTienTe
            cmbDonViTienTe.DataSource = currencyList;
            cmbDonViTienTe.DisplayMember = "Name";
            cmbDonViTienTe.ValueMember = "Code";
            cmbDonViTienTe.SelectedValue = BASE_CURRENCY; // Chọn mặc định là VND
        }

        /// <summary>
        /// Lấy tỉ giá chuyển đổi từ ngoại tệ (fromCurrency) sang VND (BASE_CURRENCY).
        /// </summary>
        private async Task<decimal> GetExchangeRate(string fromCurrency)
        {
            if (fromCurrency == BASE_CURRENCY) return 1m;

            try
            {
                string url = $"https://api.exchangerate.host/latest?base={fromCurrency}&symbols={BASE_CURRENCY}";

                var res = await _httpClient.GetAsync(url);

                if (res.IsSuccessStatusCode)
                {
                    string result = await res.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(result);

                    if (data?.rates?.VND != null)
                    {
                        return (decimal)data.rates.VND;
                    }
                }
                MessageBox.Show("Không thể lấy tỉ giá từ API. Sử dụng tỉ giá 1:1.", "Cảnh báo API", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 1m;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối API tỉ giá: {ex.Message}. Sử dụng tỉ giá 1:1.", "Lỗi API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1m;
            }
        }

        private void LoadLoaiTaiKhoan()
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    var loaiTaiKhoanList = db.LoaiTaiKhoans.AsNoTracking().ToList();

                    cmbLoaiTaiKhoan.DataSource = loaiTaiKhoanList;
                    cmbLoaiTaiKhoan.DisplayMember = "TenLoaiTaiKhoan";
                    cmbLoaiTaiKhoan.ValueMember = "MaLoaiTaiKhoan";
                    cmbLoaiTaiKhoan.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải loại tài khoản: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- SỬA LOGIC LƯU DỮ LIỆU ĐỂ TÍCH HỢP CHUYỂN ĐỔI TIỀN TỆ ---
        private async void BtnTao_Click(object sender, EventArgs e)
        {
            btnTao.Enabled = false;

            string ten = tbTenTaiKhoan.Text.Trim();
            int? maLoai = cmbLoaiTaiKhoan.SelectedValue as int?;
            decimal soDuNgoaiTe = 0;
            string maTienTe = cmbDonViTienTe.SelectedValue?.ToString() ?? BASE_CURRENCY;

            // Validation 1: Tên, Loại TK
            if (string.IsNullOrEmpty(ten) || maLoai == null || maLoai == -1)
            {
                MessageBox.Show("Vui lòng nhập Tên tài khoản và chọn Loại tài khoản.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnTao.Enabled = true;
                return;
            }

            // Validation 2: Số dư
            if (!string.IsNullOrEmpty(txtSoDu.Text.Trim()))
            {
                // Xử lý chuỗi số dư nhập vào
                string cleanSoDu = txtSoDu.Text.Replace(".", "").Replace(",", "");
                if (!decimal.TryParse(cleanSoDu, out soDuNgoaiTe) || soDuNgoaiTe < 0)
                {
                    MessageBox.Show("Số dư không hợp lệ (phải là số không âm), vui lòng nhập lại.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSoDu.Focus();
                    btnTao.Enabled = true;
                    return;
                }
            }

            // 1. TÍCH HỢP API: Gọi hàm lấy tỉ giá (ASYNC AWAIT)
            decimal tiGia = await GetExchangeRate(maTienTe);
            // 2. CHUYỂN ĐỔI: Số dư ban đầu (VND) = Số dư ngoại tệ * Tỉ giá
            decimal soDuBanDauVND = soDuNgoaiTe * tiGia;

            using (var db = _dbFactory.CreateDbContext())
            {
                // Kiểm tra Trùng tên 
                var userAccounts = db.TaiKhoanThanhToans
                                     .Where(t => t.MaNguoiDung == MA_NGUOI_DUNG_HIEN_TAI)
                                     .ToList();
                bool isDuplicate = userAccounts.Any(t =>
                    t.TenTaiKhoan.Equals(ten, StringComparison.OrdinalIgnoreCase));

                if (isDuplicate)
                {
                    MessageBox.Show("Tên tài khoản đã tồn tại, yêu cầu nhập lại.", "Trùng tên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbTenTaiKhoan.Focus();
                    btnTao.Enabled = true;
                    return;
                }

                try
                {
                    var newTaiKhoan = new TaiKhoanThanhToan
                    {
                        TenTaiKhoan = ten,
                        MaLoaiTaiKhoan = maLoai.Value,
                        SoDuBanDau = soDuBanDauVND, // <-- LƯU SỐ DƯ ĐÃ CHUYỂN ĐỔI
                        MaNguoiDung = MA_NGUOI_DUNG_HIEN_TAI,
                        TrangThai = "Đang hoạt động"
                    };
                    db.TaiKhoanThanhToans.Add(newTaiKhoan);
                    db.SaveChanges();

                    MessageBox.Show($"Thêm tài khoản thành công! Số dư ban đầu: {soDuBanDauVND:N0} VND (Tỉ giá: {tiGia:N0})", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (DbUpdateException dbEx)
                {
                    // Logic xử lý lỗi Database
                    MessageBox.Show($"Lỗi Database: {dbEx.Message}", "Lỗi Lưu Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi không xác định: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btnTao.Enabled = true;
                }
            }
        }

        private void TxtSoDu_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cho phép nhập số, Backspace, và dấu thập phân (dấu chấm hoặc dấu phẩy)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            // Chỉ cho phép một dấu thập phân
            if ((e.KeyChar == '.') || (e.KeyChar == ','))
            {
                if (((TextBox)sender).Text.Contains('.') || ((TextBox)sender).Text.Contains(','))
                {
                    e.Handled = true;
                }
            }
        }

    }
    public class CurrencyModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}