using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Piggy_Admin; // Để dùng ListThongBao, ItemThongBao

namespace Demo_Layout
{
    public partial class FrmMain : Form
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider;
        public event Action LogoutRequested;
        private readonly CurrentUserContext _userContext;

        // --- CÁC BIẾN CHO THÔNG BÁO ---
        private ToolStripDropDown _popupThongBao;
        private ListThongBao _ucListThongBao;
        private bool _coThongBaoMoi = false;
        private DateTime _lastCheckTime;
        private Color _dotColor = ColorTranslator.FromHtml("#FF0000"); // Đỏ tươi
        private string _timestampFile = "last_check.txt";
        // -------------------------------

        private bool _isShaking = false;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private SoundPlayer player;
        private string soundFilePath = Path.Combine(Application.StartupPath, "Click.wav");

        public FrmMain(IDbContextFactory<QLTCCNContext> dbFactory, IServiceProvider serviceProvider, CurrentUserContext userContext)
        {
            InitializeComponent();
            player = new SoundPlayer("Click.wav");
            this.KeyPreview = true;

            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
            _userContext = userContext;

            LoadUserInfo();

            // Đăng ký sự kiện chuông
            icoBell.Click += IcoBell_Click;
            icoBell.Paint += IcoBell_Paint;
            this.FrmMain_Load(this, EventArgs.Empty); // Gọi hàm Load

            CheckNewNotifications();
        }

        // --------------------------------------------------------

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
        }

        private void LoadUserInfo()
        {
            if (_userContext.IsLoggedIn)
            {
                lblTenHienThi.Text = _userContext.DisplayName;
                lblVaiTro.Text = _userContext.TenVaiTro;
            }
        }

        // =================================================================================
        // PHẦN 1: LOGIC THÔNG BÁO (GIỮ NGUYÊN)
        // =================================================================================
        private void CheckNewNotifications()
        {
            try
            {
                if (File.Exists(_timestampFile))
                {
                    string content = File.ReadAllText(_timestampFile);
                    if (DateTime.TryParse(content, out DateTime savedTime)) _lastCheckTime = savedTime;
                    else _lastCheckTime = DateTime.Now.AddDays(-7);
                }
                else _lastCheckTime = DateTime.Now.AddDays(-7);
            }
            catch { _lastCheckTime = DateTime.Now.AddDays(-7); }

            // LogHelper.GhiLog(_dbFactory, "Đăng nhập", _userContext.MaNguoiDung); // Giả định LogHelper tồn tại

            try
            {
                // Ghi log đăng nhập (nếu cần)
                // LogHelper.GhiLog(_dbFactory, "Đăng nhập", _userContext.MaNguoiDung ?? 0);

                using (var db = _dbFactory.CreateDbContext())
                {
                    int countNew = db.ThongBaos.Count(t => t.NgayTao > _lastCheckTime);
                    _coThongBaoMoi = countNew > 0;
                    icoBell.Invalidate();
                }
            }
            catch (Exception ex) { Console.WriteLine("Lỗi check thông báo: " + ex.Message); }
        }

        private void IcoBell_Paint(object sender, PaintEventArgs e)
        {
            if (_coThongBaoMoi)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(icoBell.Width - 12, 0, 10, 10);
                using (Brush brush = new SolidBrush(_dotColor))
                {
                    e.Graphics.FillEllipse(brush, rect);
                }
            }
        }

        private void IcoBell_Click(object sender, EventArgs e)
        {
            _coThongBaoMoi = false;
            icoBell.Invalidate();
            _lastCheckTime = DateTime.Now;
            try { File.WriteAllText(_timestampFile, _lastCheckTime.ToString()); } catch { }

            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    var listThongBao = db.ThongBaos.OrderByDescending(t => t.NgayTao).Take(20).ToList();

                    if (_popupThongBao == null)
                    {
                        _ucListThongBao = new ListThongBao();
                        ToolStripControlHost host = new ToolStripControlHost(_ucListThongBao);
                        host.Margin = Padding.Empty;
                        host.Padding = Padding.Empty;
                        host.AutoSize = false;

                        _popupThongBao = new ToolStripDropDown();
                        _popupThongBao.Items.Add(host);
                        _popupThongBao.Margin = Padding.Empty;
                        _popupThongBao.Padding = Padding.Empty;
                        _popupThongBao.BackColor = Color.White;
                    }

                    _ucListThongBao.LoadData(listThongBao, _lastCheckTime);
                    _popupThongBao.Show(icoBell, new Point(-310 + icoBell.Width, icoBell.Height + 5));
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải thông báo: " + ex.Message); }
        }

        // =================================================================================
        // PHẦN 2: LOGIC ĐIỀU HƯỚNG MENU (ĐÃ LÀM SẠCH)
        // =================================================================================

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        // --- BUTTON 1: BÁO CÁO ---
        private void button1_Click(object sender, EventArgs e)
        {
            player.Play(); // ⭐ PHÁT ÂM THANH ⭐
            HieuUngRungLac();
            pnlHienThi.Controls.Clear();
            UserControlBaoCao uc = _serviceProvider.GetRequiredService<UserControlBaoCao>();
            uc.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(uc);
        }

        // --- BUTTON 2: QUẢN LÝ GIAO DỊCH ---
        private void button2_Click(object sender, EventArgs e)
        {
            player.Play(); // ⭐ PHÁT ÂM THANH ⭐
            HieuUngRungLac();
            pnlHienThi.Controls.Clear();
            UserControlQuanLyGiaoDich uc = _serviceProvider.GetRequiredService<UserControlQuanLyGiaoDich>();
            uc.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(uc);
        }

        // --- BUTTON 3: NGÂN SÁCH (ĐÃ SỬA GỌN) ---
        private void button3_Click(object sender, EventArgs e)
        {
            player.Play(); // ⭐ PHÁT ÂM THANH ⭐
            HieuUngRungLac();
            pnlHienThi.Controls.Clear();
            // UserControl tự lo việc mở form, Main không cần đăng ký event nữa
            UserControlNganSach uc = _serviceProvider.GetRequiredService<UserControlNganSach>();
            uc.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(uc);
        }

        // --- BUTTON 4: DANH MỤC CHI TIÊU ---
        private void button4_Click(object sender, EventArgs e)
        {
            player.Play(); // ⭐ PHÁT ÂM THANH ⭐                                         
            HieuUngRungLac();
            pnlHienThi.Controls.Clear();
            UserControlDanhMucChiTieu uc = _serviceProvider.GetRequiredService<UserControlDanhMucChiTieu>();
            uc.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(uc);
        }

        // --- BUTTON 5: ĐỐI TƯỢNG GIAO DỊCH (ĐÃ SỬA GỌN) ---
        private void button5_Click(object sender, EventArgs e)
        {
            player.Play(); // ⭐ PHÁT ÂM THANH ⭐
            HieuUngRungLac();
            pnlHienThi.Controls.Clear();
            // Code gọn gàng, logic xử lý form nằm trong UserControl
            UserControlDoiTuongGiaoDich uc = _serviceProvider.GetRequiredService<UserControlDoiTuongGiaoDich>();
            uc.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(uc);
        }

        // --- BUTTON 6: TÀI KHOẢN THANH TOÁN (ĐÃ SỬA GỌN) ---
        private void button6_Click(object sender, EventArgs e)
        {
            player.Play(); // ⭐ PHÁT ÂM THANH ⭐
            HieuUngRungLac();
            pnlHienThi.Controls.Clear();
            // Code gọn gàng
            UserControlTaiKhoanThanhToan uc = _serviceProvider.GetRequiredService<UserControlTaiKhoanThanhToan>();
            uc.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(uc);
        }

        // --- CÁC NÚT ĐIỀU KHIỂN CỬA SỔ ---
        private void button8_Click(object sender, EventArgs e) => System.Windows.Forms.Application.Exit();
        private void button9_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;
        private void button7_Click(object sender, EventArgs e)
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

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            player.Play(); // ⭐ PHÁT ÂM THANH ⭐
            Piggy_Admin.FormTaiKhoan f = _serviceProvider.GetRequiredService<Piggy_Admin.FormTaiKhoan>();
            Point pos = picUserProfile.PointToScreen(new Point(50, picUserProfile.Height - 350));
            f.StartPosition = FormStartPosition.Manual;
            f.Location = pos;
            f.LogoutRequested += () => { this.Close(); };
            f.Show();
        }

        private async void HieuUngRungLac()
        {
            if (_isShaking) return;
            _isShaking = true;
            Point originalPos = icoPiggy.Location;
            Random rnd = new Random();
            try
            {
                for (int i = 0; i < 8; i++)
                {
                    int y = originalPos.Y + rnd.Next(-4, 5);
                    icoPiggy.Location = new Point(originalPos.X, y);
                    await Task.Delay(50);
                }
            }
            finally
            {
                icoPiggy.Location = originalPos;
                _isShaking = false;
            }
        }
    }
}