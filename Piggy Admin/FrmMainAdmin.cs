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

        // Cần 2 hằng số (constants) để gọi WinAPI
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        // THAY THẾ CONSTRUCTOR CŨ BẰNG CONSTRUCTOR DÙNG DI:
        public FrmMainAdmin(IDbContextFactory<QLTCCNContext> dbFactory, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
        }

        private void button8_Click(object sender, EventArgs e)
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

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        // Đã xóa FormTaiKhoan formTaiKhoan; nếu là khai báo cũ

        private void button1_Click(object sender, EventArgs e)
        {
            pnlHienThi.Controls.Clear();
            UserControlBaoCaoHeThong userControlMoi = _serviceProvider.GetRequiredService<UserControlBaoCaoHeThong>();
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

        private void button2_Click(object sender, EventArgs e)
        {
            pnlHienThi.Controls.Clear();
            UserControlQuanLyTaiKhoan userControlMoi = _serviceProvider.GetRequiredService<UserControlQuanLyTaiKhoan>();
            userControlMoi.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(userControlMoi);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pnlHienThi.Controls.Clear();
            UserControlQuanLyThongBao userControlMoi = _serviceProvider.GetRequiredService<UserControlQuanLyThongBao>();
            userControlMoi.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(userControlMoi);
        }
    }
}