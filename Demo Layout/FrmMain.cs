using System.Runtime.InteropServices;
using Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
namespace Demo_Layout
{
    public partial class FrmMain : Form
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        // HÀM KHỞI TẠO CHỈ DÙNG CHO DESIGNER (Đã sửa lỗi)
        public FrmMain()
        {
            InitializeComponent();
            this.FormClosing += FrmMain_FormClosing;
        }

        // HÀM KHỞI TẠO CHÍNH (ĐƯỢC GỌI BỞI DI)
        public FrmMain(IDbContextFactory<QLTCCNContext> dbFactory, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
            this.FormClosing += FrmMain_FormClosing;
        }

        // FIX LỖI ỨNG DỤNG KHÔNG TẮT (Nút X)
        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
         
            System.Drawing.Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            this.MaximumSize = workingArea.Size;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HieuUngRungLac();
            pnlHienThi.Controls.Clear();
            UserControlBaoCao userControlMoi = _serviceProvider.GetRequiredService<UserControlBaoCao>();
            userControlMoi.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(userControlMoi);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HieuUngRungLac();
            pnlHienThi.Controls.Clear();
            UserControlQuanLyGiaoDich userControlMoi = _serviceProvider.GetRequiredService<UserControlQuanLyGiaoDich>();
            userControlMoi.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(userControlMoi);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FormTaiKhoan f = _serviceProvider.GetRequiredService<FormTaiKhoan>();

            Point pos = picUserProfile.PointToScreen(new Point(50, picUserProfile.Height - 500));
            f.StartPosition = FormStartPosition.Manual;
            f.Location = pos;

            f.Show();
            f.Deactivate += (s, ev) => f.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HieuUngRungLac();
            pnlHienThi.Controls.Clear();
            UserControlNganSach userControlMoi = _serviceProvider.GetRequiredService<UserControlNganSach>();
            userControlMoi.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(userControlMoi);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HieuUngRungLac();
            pnlHienThi.Controls.Clear();
            UserControlDoiTuongGiaoDich userControlMoi = _serviceProvider.GetRequiredService<UserControlDoiTuongGiaoDich>();
            userControlMoi.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(userControlMoi);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HieuUngRungLac();
            pnlHienThi.Controls.Clear();
            UserControlDanhMucChiTieu userControlMoi = _serviceProvider.GetRequiredService<UserControlDanhMucChiTieu>();
            userControlMoi.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(userControlMoi);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            HieuUngRungLac();
            pnlHienThi.Controls.Clear();
            UserControlTaiKhoanThanhToan userControlMoi = _serviceProvider.GetRequiredService<UserControlTaiKhoanThanhToan>();
            userControlMoi.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(userControlMoi);
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        // Biến cờ để tránh việc rung bị chồng chéo nếu ấn nút quá nhanh
        private bool _isShaking = false;

        private async void HieuUngRungLac()
        {
            if (_isShaking) return;
            _isShaking = true;

            Point originalPos = icoPiggy.Location;
            Random rnd = new Random();

            try
            {
                // Lặp 8 lần thôi cho gọn (nhanh hơn chút)
                for (int i = 0; i < 8; i++)
                {
                    // X giữ nguyên (originalPos.X) -> Không lắc ngang
                    // Y chỉ thay đổi trong khoảng rất nhỏ (-2 đến +2) -> Rung nhẹ
                    int y = originalPos.Y + rnd.Next(-4, 5);

                    icoPiggy.Location = new Point(originalPos.X, y);

                    // Tăng delay lên một chút (40ms) để nhịp rung chậm rãi, nhẹ nhàng hơn
                    await Task.Delay(50);
                }
            }
            finally
            {
                // Trả về vị trí cũ
                icoPiggy.Location = originalPos;
                _isShaking = false;
            }
        }

    }
}