using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices; // Import DLL cho kéo thả form
using System.Threading.Tasks; // Cho async/await (HieuUngRungLac)
using System.Windows.Forms;
using System.Media; // Phát âm thanh
using System.IO; // Thao tác file
using Data; // Context và Entity models
using Microsoft.EntityFrameworkCore; // Entity Framework Core
using Microsoft.Extensions.DependencyInjection; // Dependency Injection (DI)
using Piggy_Admin; // Để dùng ListThongBao, ItemThongBao, FrmTaiKhoan, LogHelper

namespace Demo_Layout
{
    // Form Main cho người dùng thông thường
    public partial class FrmMain : Form
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory; // Factory tạo DbContext
        private readonly IServiceProvider _serviceProvider; // Service Provider cho DI
        public event Action LogoutRequested; // Sự kiện yêu cầu đăng xuất
        private readonly CurrentUserContext _userContext; // Thông tin User đang đăng nhập

        // --- CÁC BIẾN CHO THÔNG BÁO ---
        private ToolStripDropDown _popupThongBao; // Popup hiển thị danh sách thông báo
        private ListThongBao _ucListThongBao; // UserControl chứa danh sách thông báo
        private bool _coThongBaoMoi = false; // Cờ báo có thông báo mới (chưa xem)
        private DateTime _lastCheckTime; // Thời điểm cuối cùng user xem thông báo
        private Color _dotColor = ColorTranslator.FromHtml("#FF0000"); // Màu chấm đỏ
        private string _timestampFile = "last_check.txt"; // File lưu thời điểm xem thông báo cuối
        // -------------------------------

        private bool _isShaking = false; // Cờ tránh lắc (shaking) liên tục

        // Hằng số và hàm Import DLL cho phép kéo thả form không có border
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private SoundPlayer player; // Đối tượng phát âm thanh
        private string soundFilePath = Path.Combine(Application.StartupPath, "Click.wav"); // Đường dẫn file âm thanh

        // Constructor nhận DI
        public FrmMain(IDbContextFactory<QLTCCNContext> dbFactory, IServiceProvider serviceProvider, CurrentUserContext userContext)
        {
            InitializeComponent();
            player = new SoundPlayer("Click.wav"); // Khởi tạo SoundPlayer (giả định file tồn tại)
            this.KeyPreview = true; // Cho phép form bắt sự kiện phím

            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
            _userContext = userContext;

            LoadUserInfo(); // Tải và hiển thị thông tin người dùng

            // Đăng ký sự kiện chuông
            icoBell.Click += IcoBell_Click;
            icoBell.Paint += IcoBell_Paint; // Vẽ chấm đỏ khi có thông báo mới

            // Xử lý logic Load form
            this.FrmMain_Load(this, EventArgs.Empty);

            CheckNewNotifications(); // Kiểm tra thông báo mới khi form khởi động
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size; // Giới hạn kích thước tối đa
            LogHelper.GhiLog(_dbFactory, "Đăng nhập", _userContext.MaNguoiDung); // Ghi log đăng nhập
        }

        // Tải và hiển thị tên/vai trò người dùng
        private void LoadUserInfo()
        {
            if (_userContext.IsLoggedIn)
            {
                lblTenHienThi.Text = _userContext.DisplayName;
                lblVaiTro.Text = _userContext.TenVaiTro;
            }
        }

        // Kiểm tra thông báo mới so với thời điểm xem cuối cùng
        private void CheckNewNotifications()
        {
            // 1. Đọc thời điểm xem thông báo cuối từ file
            try
            {
                if (File.Exists(_timestampFile))
                {
                    string content = File.ReadAllText(_timestampFile);
                    if (DateTime.TryParse(content, out DateTime savedTime)) _lastCheckTime = savedTime;
                    else _lastCheckTime = DateTime.Now.AddDays(-7); // Mặc định 7 ngày trước nếu lỗi
                }
                else _lastCheckTime = DateTime.Now.AddDays(-7); // Mặc định 7 ngày trước nếu không có file
            }
            catch { _lastCheckTime = DateTime.Now.AddDays(-7); }


            // 2. Truy vấn DB để kiểm tra thông báo mới
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    // Đếm số lượng thông báo được tạo sau thời điểm xem cuối cùng
                    int countNew = db.ThongBaos.Count(t => t.NgayTao > _lastCheckTime);
                    _coThongBaoMoi = countNew > 0;
                    icoBell.Invalidate(); // Yêu cầu vẽ lại icon chuông
                }
            }
            catch (Exception ex) { Console.WriteLine("Lỗi check thông báo: " + ex.Message); }
        }

        // Vẽ chấm đỏ trên icon chuông nếu có thông báo mới
        private void IcoBell_Paint(object sender, PaintEventArgs e)
        {
            if (_coThongBaoMoi)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // Làm mượt đường viền
                Rectangle rect = new Rectangle(icoBell.Width - 12, 0, 10, 10); // Vị trí chấm đỏ
                using (Brush brush = new SolidBrush(_dotColor))
                {
                    e.Graphics.FillEllipse(brush, rect); // Vẽ hình tròn màu đỏ
                }
            }
        }

        // Xử lý khi click vào icon chuông (mở popup thông báo)
        private void IcoBell_Click(object sender, EventArgs e)
        {
            // Reset trạng thái thông báo mới và cập nhật thời điểm xem cuối cùng
            _coThongBaoMoi = false;
            icoBell.Invalidate();
            _lastCheckTime = DateTime.Now;
            try { File.WriteAllText(_timestampFile, _lastCheckTime.ToString()); } catch { }

            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    // Lấy 20 thông báo gần nhất
                    var listThongBao = db.ThongBaos.OrderByDescending(t => t.NgayTao).Take(20).ToList();

                    // Khởi tạo popup nếu chưa có
                    if (_popupThongBao == null)
                    {
                        _ucListThongBao = new ListThongBao(); // UserControl hiển thị list
                        ToolStripControlHost host = new ToolStripControlHost(_ucListThongBao); // Chứa UserControl trong ToolStrip
                        host.Margin = Padding.Empty;
                        host.Padding = Padding.Empty;
                        host.AutoSize = false;

                        _popupThongBao = new ToolStripDropDown(); // Popup chính
                        _popupThongBao.Items.Add(host);
                        _popupThongBao.Margin = Padding.Empty;
                        _popupThongBao.Padding = Padding.Empty;
                        _popupThongBao.BackColor = Color.White;
                    }

                    _ucListThongBao.LoadData(listThongBao, _lastCheckTime); // Đổ dữ liệu vào UserControl
                    // Hiển thị popup (vị trí gần icon chuông)
                    _popupThongBao.Show(icoBell, new Point(-310 + icoBell.Width, icoBell.Height + 5));
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải thông báo: " + ex.Message); }
        }

        // Xử lý khi Form đang đóng (luôn thoát ứng dụng)
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        // --- CÁC NÚT MENU CHÍNH ---

        // BUTTON 1: BÁO CÁO
        private void button1_Click(object sender, EventArgs e)
        {
            player.Play();
            HieuUngRungLac(); // Hiệu ứng lắc icon
            pnlHienThi.Controls.Clear();
            // Load UserControlBaoCao
            UserControlBaoCao uc = _serviceProvider.GetRequiredService<UserControlBaoCao>();
            uc.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(uc);
        }

        // BUTTON 2: QUẢN LÝ GIAO DỊCH
        private void button2_Click(object sender, EventArgs e)
        {
            player.Play();
            HieuUngRungLac();
            pnlHienThi.Controls.Clear();
            // Load UserControlQuanLyGiaoDich
            UserControlQuanLyGiaoDich uc = _serviceProvider.GetRequiredService<UserControlQuanLyGiaoDich>();
            uc.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(uc);
        }

        // BUTTON 3: NGÂN SÁCH
        private void button3_Click(object sender, EventArgs e)
        {
            player.Play();
            HieuUngRungLac();
            pnlHienThi.Controls.Clear();
            // Load UserControlNganSach
            UserControlNganSach uc = _serviceProvider.GetRequiredService<UserControlNganSach>();
            uc.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(uc);
        }

        // BUTTON 4: DANH MỤC CHI TIÊU
        private void button4_Click(object sender, EventArgs e)
        {
            player.Play();
            HieuUngRungLac();
            pnlHienThi.Controls.Clear();
            // Load UserControlDanhMucChiTieu
            UserControlDanhMucChiTieu uc = _serviceProvider.GetRequiredService<UserControlDanhMucChiTieu>();
            uc.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(uc);
        }

        // BUTTON 5: ĐỐI TƯỢNG GIAO DỊCH
        private void button5_Click(object sender, EventArgs e)
        {
            player.Play();
            HieuUngRungLac();
            pnlHienThi.Controls.Clear();
            // Load UserControlDoiTuongGiaoDich
            UserControlDoiTuongGiaoDich uc = _serviceProvider.GetRequiredService<UserControlDoiTuongGiaoDich>();
            uc.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(uc);
        }

        // BUTTON 6: TÀI KHOẢN THANH TOÁN
        private void button6_Click(object sender, EventArgs e)
        {
            player.Play();
            HieuUngRungLac();
            pnlHienThi.Controls.Clear();
            // Load UserControlTaiKhoanThanhToan
            UserControlTaiKhoanThanhToan uc = _serviceProvider.GetRequiredService<UserControlTaiKhoanThanhToan>();
            uc.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(uc);
        }

        // --- CÁC NÚT ĐIỀU KHIỂN CỬA SỔ ---
        private void button8_Click(object sender, EventArgs e) => System.Windows.Forms.Application.Exit(); // Đóng ứng dụng
        private void button9_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized; // Thu nhỏ
        private void button7_Click(object sender, EventArgs e) // Phóng to/Khôi phục
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        // Xử lý kéo thả form (dùng Import DLL)
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        // Click vào ảnh đại diện (mở form Tài khoản cá nhân)
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            player.Play();

            Piggy_Admin.FrmTaiKhoan f = _serviceProvider.GetRequiredService<Piggy_Admin.FrmTaiKhoan>();
            // Tính toán vị trí hiển thị (popup từ ảnh đại diện)
            Point pos = picUserProfile.PointToScreen(new Point(50, picUserProfile.Height - 350));
            f.StartPosition = FormStartPosition.Manual;
            f.Location = pos;
            f.LogoutRequested += () => { this.Close(); }; // Đăng ký sự kiện Logout: đóng main form
            f.Show();
        }

        // Hiệu ứng Rung lắc Async cho icon heo
        private async void HieuUngRungLac()
        {
            if (_isShaking) return;
            _isShaking = true;
            Point originalPos = icoPiggy.Location;
            Random rnd = new Random();
            try
            {
                for (int i = 0; i < 8; i++) // Lắc 8 lần
                {
                    int y = originalPos.Y + rnd.Next(-4, 5); // Random vị trí Y
                    icoPiggy.Location = new Point(originalPos.X, y);
                    await Task.Delay(50); // Delay 50ms
                }
            }
            finally
            {
                icoPiggy.Location = originalPos; // Đặt lại vị trí ban đầu
                _isShaking = false;
            }
        }

        // Ghi log khi form đóng (đăng xuất)
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            LogHelper.GhiLog(_dbFactory, "Đăng xuất", _userContext.MaNguoiDung);
        }

        // --- SHORTCUT BUTTONS (NÚT THÊM NHANH) ---

        // Thêm Tài Khoản (shortcut)
        private void scThemTaiKhoan_Click(object sender, EventArgs e)
        {
            player.Play();
            HieuUngRungLac();
            pnlHienThi.Controls.Clear();
            // Load UserControl Tài Khoản
            UserControlTaiKhoanThanhToan uc = _serviceProvider.GetRequiredService<UserControlTaiKhoanThanhToan>();
            uc.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(uc);
            uc.BtnThem_Click(sender, e); // Kích hoạt nút Thêm trên UserControl
        }

        // Thêm Giao Dịch (shortcut)
        private void button11_Click(object sender, EventArgs e)
        {
            player.Play();
            HieuUngRungLac();
            pnlHienThi.Controls.Clear();
            // Load UserControl Giao Dịch
            UserControlQuanLyGiaoDich uc = _serviceProvider.GetRequiredService<UserControlQuanLyGiaoDich>();
            uc.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(uc);
            uc.btnThem_Click(sender, e); // Kích hoạt nút Thêm trên UserControl
        }

        // Thêm Đối Tượng Giao Dịch (shortcut)
        private void button12_Click(object sender, EventArgs e)
        {
            player.Play();
            HieuUngRungLac();
            pnlHienThi.Controls.Clear();
            // Load UserControl Đối Tượng
            UserControlDoiTuongGiaoDich uc = _serviceProvider.GetRequiredService<UserControlDoiTuongGiaoDich>();
            uc.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(uc);
            uc.BtnThem_Click(sender, e); // Kích hoạt nút Thêm trên UserControl
        }

        // Thêm Ngân Sách (shortcut)
        private void button13_Click(object sender, EventArgs e)
        {
            player.Play();
            HieuUngRungLac();
            pnlHienThi.Controls.Clear();
            // Load UserControl Ngân Sách
            UserControlNganSach uc = _serviceProvider.GetRequiredService<UserControlNganSach>();
            uc.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(uc);
            uc.BtnThem_Click(sender, e); // Kích hoạt nút Thêm trên UserControl
        }
    }
}