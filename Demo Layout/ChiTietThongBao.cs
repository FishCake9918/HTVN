using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Data; // Namespace chứa Model ThongBao

namespace Demo_Layout
{
    public partial class ChiTietThongBao : UserControl
    {
        private Data.ThongBao _thongBao;
        private bool _isNew;

        public ChiTietThongBao()
        {
            InitializeComponent();
        }

        public ChiTietThongBao(Data.ThongBao tb, bool isNew) : this()
        {
            _thongBao = tb;
            _isNew = isNew;

            BoTronChamDo();
            LoadData();
            UpdateStyle();
            GanSuKien();
        }

        private void BoTronChamDo()
        {
            if (pnlStatus != null)
            {
                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(0, 0, pnlStatus.Width, pnlStatus.Height);
                pnlStatus.Region = new Region(path);
            }
        }

        private void LoadData()
        {
            if (_thongBao != null)
            {
                lblTieuDe.Text = _thongBao.TieuDe;
                // FIX LỖI: Bỏ .Value và .HasValue vì NgayTao không null
                lblThoiGian.Text = _thongBao.NgayTao.ToString("dd/MM/yyyy HH:mm");
            }
        }

        private void GanSuKien()
        {
            this.MouseEnter += (s, e) => this.BackColor = Color.FromArgb(240, 242, 245);
            this.MouseLeave += (s, e) => UpdateStyle();

            this.DoubleClick += Event_XemChiTiet;
            lblTieuDe.DoubleClick += Event_XemChiTiet;
            lblThoiGian.DoubleClick += Event_XemChiTiet;
            pnlStatus.DoubleClick += Event_XemChiTiet;
        }

        private void UpdateStyle()
        {
            if (_isNew)
            {
                this.BackColor = Color.FromArgb(237, 247, 242);
                pnlStatus.Visible = true;
            }
            else
            {
                this.BackColor = Color.White;
                pnlStatus.Visible = false;
            }
        }

        private void Event_XemChiTiet(object sender, EventArgs e)
        {
            _isNew = false;
            UpdateStyle();

            if (_thongBao != null)
            {
                // GỌI HÀM MỚI Ở ĐÂY:
                PiggyMessageBox.Show(
                    _thongBao.TieuDe,                                        // Tiêu đề
                    _thongBao.NoiDung,                                       // Nội dung
                    $"Ngày đăng: {_thongBao.NgayTao:dd/MM/yyyy HH:mm}"      // Ngày tháng
                );
            }
        }
        public static class PiggyMessageBox
        {
            private static Color colorPrimary = ColorTranslator.FromHtml("#425e6a");
            private static Color colorAccent = ColorTranslator.FromHtml("#f07055");

            public static void Show(string title, string content, string dateInfo)
            {
                Form frm = new Form();
                frm.Size = new Size(350, 250);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.BackColor = Color.White;
                frm.ShowInTaskbar = false;

                // Vẽ viền
                frm.Paint += (s, e) => {
                    ControlPaint.DrawBorder(e.Graphics, frm.ClientRectangle,
                        colorPrimary, 2, ButtonBorderStyle.Solid,
                        colorPrimary, 2, ButtonBorderStyle.Solid,
                        colorPrimary, 2, ButtonBorderStyle.Solid,
                        colorPrimary, 2, ButtonBorderStyle.Solid);
                };

                // --- Header (Giữ nguyên) ---
                Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 45, BackColor = colorPrimary, Padding = new Padding(10, 0, 10, 0) };

                Label lblCaption = new Label
                {
                    Text = "CHI TIẾT THÔNG BÁO",
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Dock = DockStyle.Fill
                };

                Label btnX = new Label
                {
                    Text = "✕",
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    Dock = DockStyle.Right,
                    AutoSize = true,
                    Cursor = Cursors.Hand
                };
                btnX.Click += (s, e) => frm.Close();
                btnX.MouseEnter += (s, e) => btnX.ForeColor = colorAccent;
                btnX.MouseLeave += (s, e) => btnX.ForeColor = Color.White;

                pnlHeader.Controls.Add(lblCaption);
                pnlHeader.Controls.Add(btnX);

                // --- Body (THAY ĐỔI LỚN Ở ĐÂY) ---
                // Dùng FlowLayoutPanel để các thành phần tự đẩy nhau xuống
                FlowLayoutPanel pnlBody = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    Padding = new Padding(10),
                    BackColor = Color.White,
                    AutoScroll = true,             // <--- QUAN TRỌNG: Cho phép cuộn
                    FlowDirection = FlowDirection.TopDown, // Xếp theo chiều dọc
                    WrapContents = false           // Không cho nhảy sang cột bên cạnh
                };

                // 1. Tiêu đề (Cho phép dài vô tư)
                Label lblTitle = new Label
                {
                    Text = title.ToUpper(),
                    ForeColor = colorPrimary,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    AutoSize = true,               // Tự giãn chiều cao
                    MaximumSize = new Size(300, 0),// Giới hạn chiều rộng để tự xuống dòng, chiều cao (0) là vô tận
                    Margin = new Padding(0, 0, 0, 5)
                };

                // 2. Ngày tháng
                Label lblDate = new Label
                {
                    Text = dateInfo,
                    ForeColor = Color.Gray,
                    Font = new Font("Segoe UI", 9, FontStyle.Italic),
                    AutoSize = true,
                    Margin = new Padding(0, 0, 0, 10)
                };

                // 3. Đường kẻ
                Panel line = new Panel
                {
                    Size = new Size(300, 2),
                    BackColor = Color.LightGray,
                    Margin = new Padding(0, 0, 0, 15)
                };

                // 4. Nội dung (Dùng Label thay RichTextBox để cuộn mượt hơn)
                Label lblContent = new Label
                {
                    Text = content,
                    Font = new Font("Segoe UI", 11),
                    ForeColor = Color.Black,
                    AutoSize = true,               // Tự giãn theo nội dung
                    MaximumSize = new Size(300, 0),// Tự xuống dòng khi hết chiều rộng
                    UseMnemonic = false            // Để hiển thị được ký tự & nếu có
                };

                // Add các control vào FlowLayoutPanel theo thứ tự
                pnlBody.Controls.Add(lblTitle);
                pnlBody.Controls.Add(lblDate);
                pnlBody.Controls.Add(line);
                pnlBody.Controls.Add(lblContent);

                // --- Footer ---
                Panel pnlFooter = new Panel { Dock = DockStyle.Bottom, Height = 50, Padding = new Padding(0, 5, 20, 10) };
                Button btnClose = new Button
                {
                    Text = "Đóng",
                    Size = new Size(80, 35),
                    Dock = DockStyle.Right,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = colorPrimary,
                    ForeColor = Color.White,
                    Cursor = Cursors.Hand,
                    TabIndex = 0
                };
                btnClose.FlatAppearance.BorderSize = 0;
                btnClose.Click += (s, e) => frm.Close();
                pnlFooter.Controls.Add(btnClose);

                frm.Controls.Add(pnlBody); // Body ở giữa
                frm.Controls.Add(pnlFooter);
                frm.Controls.Add(pnlHeader);

                // Fix lỗi focus
                frm.Shown += (s, e) => {
                    btnClose.Focus();
                    // Cuộn lên đầu trang (đôi khi focus làm nó trôi xuống dưới)
                    pnlBody.AutoScrollPosition = new Point(0, 0);
                };

                frm.ShowDialog();
            }
        }
    }
}