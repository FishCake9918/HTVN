using System.Runtime.InteropServices;
using Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms; 
using System.Media;        
using System;               
using System.IO;            
using System.Drawing;       

namespace Piggy_Admin
{
    public partial class FrmMainAdmin : Form
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly CurrentUserContext _userContext; // <-- INJECT CONTEXT

        // KHAI BÁO BIẾN ÂM THANH
        private SoundPlayer player;
        private string soundFilePath = Path.Combine(Application.StartupPath, "Click.wav");
        // ------------------------

        // Code API Win32 (Giữ nguyên)
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
            _userContext = userContext;

            // KHỞI TẠO SoundPlayer
            if (File.Exists(soundFilePath))
            {
                player = new SoundPlayer(soundFilePath);
            }
            else
            {
                // Xử lý nếu file không tồn tại (ví dụ: gán null hoặc tạo SoundPlayer rỗng)
                player = new SoundPlayer();
            }
            // ------------------------

            LoadUserInfo();
        }

        private void PlayClickSound()
        {
            try
            {
                player.Play();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu không thể phát (ví dụ: file bị khóa hoặc bị hỏng)
                System.Diagnostics.Debug.WriteLine($"Lỗi phát âm thanh: {ex.Message}");
            }
        }

        private void LoadUserInfo()
        {
            if (_userContext.IsLoggedIn)
            {
                lblTenHienThi.Text = _userContext.DisplayName; // Hiện tên
                lblVaiTro.Text = _userContext.TenVaiTro;      // Hiện vai trò

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

        // --- NÚT BÁO CÁO HỆ THỐNG ---
        private void button1_Click(object sender, EventArgs e)
        {
            PlayClickSound(); // ⭐ PHÁT ÂM THANH ⭐
            pnlHienThi.Controls.Clear();
            UserControlBaoCaoHeThong uc = _serviceProvider.GetRequiredService<UserControlBaoCaoHeThong>();
            uc.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(uc);
        }

        // --- NÚT QUẢN LÝ TÀI KHOẢN ---
        private void button2_Click(object sender, EventArgs e)
        {
            PlayClickSound(); // ⭐ PHÁT ÂM THANH ⭐
            pnlHienThi.Controls.Clear();
            UserControlQuanLyTaiKhoan uc = _serviceProvider.GetRequiredService<UserControlQuanLyTaiKhoan>();
            uc.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(uc);
        }

        // --- NÚT QUẢN LÝ THÔNG BÁO ---
        private void button5_Click(object sender, EventArgs e)
        {
            PlayClickSound(); // ⭐ PHÁT ÂM THANH ⭐
            pnlHienThi.Controls.Clear();
            UserControlQuanLyThongBao userControlMoi = _serviceProvider.GetRequiredService<UserControlQuanLyThongBao>();
            userControlMoi.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(userControlMoi);
        }

        // --- NÚT HỒ SƠ (PIC BOX) ---
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PlayClickSound(); // ⭐ PHÁT ÂM THANH ⭐

            // Đảm bảo PicUserProfile được khai báo trong Designer để dùng PointToScreen
            // Giả định picUserProfile là tên của PictureBox/Control
            Control picUserProfile = (Control)sender;

            FrmTaiKhoan f = _serviceProvider.GetRequiredService<FrmTaiKhoan>();

            // Điều chỉnh vị trí popup cho phù hợp
            Point pos = picUserProfile.PointToScreen(new Point(50, picUserProfile.Height));
            f.StartPosition = FormStartPosition.Manual;
            f.Location = pos;

            f.LogoutRequested += () =>
            {
                this.Close(); // Đóng Admin Main -> Program.cs sẽ mở lại Login
            };
            f.Show();
        }
    }
}