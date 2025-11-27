using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Data; // QUAN TRỌNG
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Demo_Layout
{
    public partial class FrmMain : Form
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider;
        public event Action LogoutRequested;
        private readonly CurrentUserContext _userContext;

        private ToolStripDropDown _popupThongBao;
        private ListThongBao _ucListThongBao;
        private bool _coThongBaoMoi = false;
        private DateTime _lastCheckTime;
        private Color _dotColor = ColorTranslator.FromHtml("#CC0000");
        private string _timestampFile = "last_check.txt";

        private bool _isShaking = false;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public FrmMain(IDbContextFactory<QLTCCNContext> dbFactory, IServiceProvider serviceProvider, CurrentUserContext userContext)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
            _userContext = userContext;
            LoadUserInfo();

            icoBell.Click += IcoBell_Click;
            icoBell.Paint += IcoBell_Paint;

            CheckNewNotifications();
        }

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

        private void CheckNewNotifications()
        {
            try
            {
                if (File.Exists(_timestampFile))
                {
                    string content = File.ReadAllText(_timestampFile);
                    if (DateTime.TryParse(content, out DateTime savedTime))
                    {
                        _lastCheckTime = savedTime;
                    }
                    else _lastCheckTime = DateTime.Now.AddDays(-7);
                }
                else
                {
                    _lastCheckTime = DateTime.Now.AddDays(-7);
                }
            }
            catch { _lastCheckTime = DateTime.Now.AddDays(-7); }

                LogHelper.GhiLog(_dbFactory, "Đăng nhập", _userContext.MaNguoiDung); // ghi log

                // Logic đổi hình đại diện nếu có (ví dụ)
                // if (_userContext.IsAdmin) picUserProfile.Image = ...
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    // FIX LỖI: So sánh trực tiếp, không cần HasValue
                    int countNew = db.ThongBaos.Count(t => t.NgayTao > _lastCheckTime);
                    _coThongBaoMoi = countNew > 0;
                    icoBell.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi check thông báo: " + ex.Message);
            }
        }

        private void IcoBell_Paint(object sender, PaintEventArgs e)
        {
            if (_coThongBaoMoi)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(icoBell.Width - 14, 2, 12, 12);
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
                    // Lấy dữ liệu Data.ThongBao
                    var listThongBao = db.ThongBaos
                        .OrderByDescending(t => t.NgayTao)
                        .Take(20)
                        .ToList();

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
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông báo: " + ex.Message);
            }
        }

        // --- CÁC PHẦN CÒN LẠI CỦA MAIN FORM ---

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
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


        private void button4_Click(object sender, EventArgs e)
        {
            HieuUngRungLac();
            pnlHienThi.Controls.Clear();
            UserControlDanhMucChiTieu userControlMoi = _serviceProvider.GetRequiredService<UserControlDanhMucChiTieu>();
            userControlMoi.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(userControlMoi);
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


        private void button8_Click(object sender, EventArgs e) => System.Windows.Forms.Application.Exit();
        private void button9_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;
        private void button7_Click(object sender, EventArgs e) => this.WindowState = (this.WindowState == FormWindowState.Normal) ? FormWindowState.Maximized : FormWindowState.Normal;

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
            Piggy_Admin.FormTaiKhoan f = _serviceProvider.GetRequiredService<Piggy_Admin.FormTaiKhoan>();
            Point pos = picUserProfile.PointToScreen(new Point(50, picUserProfile.Height - 500));
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