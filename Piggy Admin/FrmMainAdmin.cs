using System.Runtime.InteropServices;
using Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Piggy_Admin
{
    public partial class FrmMainAdmin : Form
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly CurrentUserContext _userContext; // <-- INJECT CONTEXT

        // Code API Win32
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public FrmMainAdmin(IDbContextFactory<QLTCCNContext> dbFactory, IServiceProvider serviceProvider, CurrentUserContext userContext)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
            _userContext = userContext; // Lưu context để sử dụng
            // GỌI HÀM HIỂN THỊ THÔNG TIN NGAY KHI FORM MỞ
            LoadUserInfo();
        }
        // Inject CurrentUserContext từ project Data
        // Hàm hiển thị thông tin người dùng lên Panel 1
        private void LoadUserInfo()
        {
            if (_userContext.IsLoggedIn)
            {
                lblTenHienThi.Text = _userContext.DisplayName; // Hiện tên
                lblVaiTro.Text = _userContext.TenVaiTro;     // Hiện vai trò

            }
        }
        private void button8_Click(object sender, EventArgs e) => System.Windows.Forms.Application.Exit();
        private void button9_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;
        private void button7_Click(object sender, EventArgs e) => this.WindowState = (this.WindowState == FormWindowState.Normal) ? FormWindowState.Maximized : FormWindowState.Normal;
        private void FrmMain_Load(object sender, EventArgs e) => this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pnlHienThi.Controls.Clear();
            UserControlBaoCaoThongKe uc = _serviceProvider.GetRequiredService<UserControlBaoCaoThongKe>();
            uc.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(uc);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pnlHienThi.Controls.Clear();
            UserControlQuanLyTaiKhoan uc = _serviceProvider.GetRequiredService<UserControlQuanLyTaiKhoan>();
            uc.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(uc);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pnlHienThi.Controls.Clear();
            UserControlBaoCaoHeThong userControlMoi = _serviceProvider.GetRequiredService<UserControlBaoCaoHeThong>();
            userControlMoi.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(userControlMoi);
        }

        // --- SỬA LỖI Ở ĐÂY ---
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // 1. Lấy instance mới
            FormTaiKhoan f = _serviceProvider.GetRequiredService<FormTaiKhoan>();

            // 2. Đặt vị trí
            Point pos = picUserProfile.PointToScreen(new Point(50, picUserProfile.Height - 500));
            f.StartPosition = FormStartPosition.Manual;
            f.Location = pos;

            // 3. Đăng ký sự kiện: Nếu Form con yêu cầu Logout -> Form cha (MainAdmin) tự đóng
            f.LogoutRequested += () =>
            {
                this.Close(); // Đóng Admin Main -> Program.cs sẽ mở lại Login
            };

            // 4. Hiển thị Form Tài khoản
            f.Show();

            // ⚠️ ĐÃ XÓA SỰ KIỆN Deactivate ĐỂ TRÁNH LỖI MESSAGE BOX TẮT FORM
            // Bây giờ Form Tài khoản sẽ hoạt động như một cửa sổ bình thường.
            // Người dùng phải tự bấm nút tắt hoặc click ra ngoài (nếu muốn logic phức tạp hơn).
            // Nhưng ít nhất code này đảm bảo MessageBox hiện lên đàng hoàng.
        }
    }
}