using Krypton.Toolkit;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Piggy_Admin
{
    public partial class FrmMainAdmin : Form
    {
        // Cần 2 hằng số (constants) để gọi WinAPI
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        // ... (Các constructor và phương thức khác)
        public FrmMainAdmin()
        {
            InitializeComponent();

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
            // Lấy kích thước của màn hình làm việc (Working Area) hiện tại.
            // Working Area là kích thước màn hình trừ đi Taskbar và các dock bar khác.
            System.Drawing.Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;

            // Đặt thuộc tính MaximumSize của Form bằng với kích thước màn hình làm việc.
            this.MaximumSize = workingArea.Size;

            // Nếu bạn muốn bao gồm cả Taskbar (kích thước đầy đủ của màn hình),
            // bạn có thể dùng thuộc tính Bounds thay thế:
            // this.MaximumSize = Screen.PrimaryScreen.Bounds.Size;
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    pnlHienThi.Controls.Clear();
        //    UserControlBaoCao userControlMoi = new UserControlBaoCao();
        //    userControlMoi.Dock = DockStyle.Fill;
        //    pnlHienThi.Controls.Add(userControlMoi);
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    pnlHienThi.Controls.Clear();
        //    UserControlQuanLyGiaoDich userControlMoi = new UserControlQuanLyGiaoDich();
        //    userControlMoi.Dock = DockStyle.Fill;
        //    pnlHienThi.Controls.Add(userControlMoi);
        //}

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            // Kiểm tra nếu là chuột trái được nhấn
            if (e.Button == MouseButtons.Left)
            {
                // 1. Giải phóng sự chiếm giữ chuột của Panel
                ReleaseCapture();

                // 2. Gửi thông báo đến Windows rằng Form này đã nhận được lệnh kéo
                // 'this.Handle' là tay nắm (handle) của cửa sổ Form hiện tại
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private FormTaiKhoan formTaiKhoan;

        private void button1_Click(object sender, EventArgs e)
        {
            pnlHienThi.Controls.Clear();
            UserControlBaoCaoThongKe userControlMoi = new UserControlBaoCaoThongKe();
            userControlMoi.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(userControlMoi);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FormTaiKhoan f = new FormTaiKhoan();
            Point pos = picUserProfile.PointToScreen(new Point(50, picUserProfile.Height - 500));
            f.StartPosition = FormStartPosition.Manual;
            f.Location = pos;

            f.Show();

            // Tự tắt khi click ra ngoài
            f.Deactivate += (s, ev) => f.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pnlHienThi.Controls.Clear();
            UserControlQuanLyTaiKhoan userControlMoi = new UserControlQuanLyTaiKhoan();
            userControlMoi.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(userControlMoi);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pnlHienThi.Controls.Clear();
            UserControlQuanLyThongBao userControlMoi = new UserControlQuanLyThongBao();
            userControlMoi.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(userControlMoi);
        }
    }
}
