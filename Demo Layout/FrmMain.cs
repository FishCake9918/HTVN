using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Media;
using System.Runtime.InteropServices;
namespace Demo_Layout
{
    public partial class FrmMain : Form
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        private const int CURRENT_USER_ID = 1;

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
            LogHelper.GhiLog(_dbFactory, "Đăng nhập", CURRENT_USER_ID); // ghi log

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

            // 1. Khởi tạo UserControl qua DI
            UserControlNganSach userControlMoi = _serviceProvider.GetRequiredService<UserControlNganSach>();

            // 2. PHẢI THÊM: Đăng ký sự kiện mở form Thêm/Sửa
            userControlMoi.OnOpenEditForm += NganSachControl_OnOpenEditForm; // <-- DÒNG QUAN TRỌNG

            userControlMoi.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(userControlMoi);
        }
        private void NganSachControl_OnOpenEditForm(object sender, int nganSachId)
        {
            // Tạo Form Thêm/Sửa Ngân sách thông qua DI
            using (var frmEdit = _serviceProvider.GetRequiredService<LapNganSach>())
            {
                // Thiết lập ID (0 cho Thêm mới, >0 cho Sửa)
                frmEdit.SetId(nganSachId);

                if (frmEdit.ShowDialog() == DialogResult.OK)
                {
                    // Nếu Lưu thành công, tải lại danh sách
                    if (sender is UserControlNganSach control)
                    {
                        control.LoadDanhSach();
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HieuUngRungLac();
            pnlHienThi.Controls.Clear();

            // 1. Tạo UserControl mới thông qua DI
            UserControlDoiTuongGiaoDich userControlMoi = _serviceProvider.GetRequiredService<UserControlDoiTuongGiaoDich>();

            // 2. PHẢI THÊM: LẮNG NGHE SỰ KIỆN TỪ USER CONTROL
            // Gán Event của User Control vào phương thức xử lý của Form Main
            userControlMoi.OnOpenEditForm += DoiTuongControl_OnOpenEditForm;

            // 3. Nhúng vào Panel
            userControlMoi.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(userControlMoi);
        }

        // KHÔNG ĐƯỢC THIẾU PHƯƠNG THỨC XỬ LÝ SỰ KIỆN NÀY (Đã đúng)
        private void DoiTuongControl_OnOpenEditForm(object sender, int doiTuongId)
        {
            // Tạo form chỉnh sửa thông qua DI (khắc phục lỗi 'No service for type...')
            using (var frmEdit = _serviceProvider.GetRequiredService<FrmChinhSuaDoiTuongGiaoDich>())
            {
                frmEdit.SetId(doiTuongId);

                if (frmEdit.ShowDialog() == DialogResult.OK)
                {
                    if (sender is UserControlDoiTuongGiaoDich control)
                    {
                        // Tải lại dữ liệu sau khi Thêm/Sửa thành công
                        control.LoadDanhSach();
                    }
                }
            }
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

            // 1. Tạo UserControl mới thông qua DI
            UserControlTaiKhoanThanhToan userControlMoi = _serviceProvider.GetRequiredService<UserControlTaiKhoanThanhToan>();

            // 2. PHẢI THÊM: LẮNG NGHE SỰ KIỆN TỪ USER CONTROL
            // Gán Event Thêm tài khoản
            userControlMoi.OnOpenThemTaiKhoan += TaiKhoan_OnOpenThemTaiKhoan;
            // Gán Event Đóng tài khoản
            userControlMoi.OnOpenDongTaiKhoan += TaiKhoan_OnOpenDongTaiKhoan;

            // 3. Nhúng vào Panel
            userControlMoi.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(userControlMoi);
        }
        // Thêm 2 phương thức này vào FrmMain.cs

        private void TaiKhoan_OnOpenThemTaiKhoan(object sender, int taiKhoanId)
        {
            // Tạo Form Thêm thông qua DI
            using (var frmThem = _serviceProvider.GetRequiredService<FormThemTaiKhoanThanhToan>())
            {
                if (frmThem.ShowDialog() == DialogResult.OK)
                {
                    // Nếu thêm thành công, tải lại danh sách
                    if (sender is UserControlTaiKhoanThanhToan uc)
                    {
                        uc.LoadDanhSach();
                    }
                }
            }
        }

        private void TaiKhoan_OnOpenDongTaiKhoan(object sender, int taiKhoanId)
        {
            // Tạo Form Đóng thông qua DI
            using (var frmDong = _serviceProvider.GetRequiredService<FormDongTaiKhoan>())
            {
                // Thiết lập ID Tài khoản cần đóng
                frmDong.SetTaiKhoanId(taiKhoanId);

                if (frmDong.ShowDialog() == DialogResult.OK)
                {
                    // Nếu đóng thành công, tải lại danh sách
                    if (sender is UserControlTaiKhoanThanhToan uc)
                    {
                        uc.LoadDanhSach();
                    }
                }
            }
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("Bye");
            LogHelper.GhiLog(_dbFactory, "Đăng xuất", CURRENT_USER_ID); // ghi log
        }
        // Biến cờ để tránh việc rung bị chồng chéo nếu ấn nút quá nhanh
        private bool _isShaking = false;

        private async void HieuUngRungLac()
        {
            if (_isShaking) return;
            _isShaking = true;
            SystemSounds.Asterisk.Play();

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