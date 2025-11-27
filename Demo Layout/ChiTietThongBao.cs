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
                // FIX LỖI: Bỏ .Value vì NgayTao luôn có dữ liệu
                MessageBox.Show(
                    $"Ngày tạo: {_thongBao.NgayTao:dd/MM/yyyy HH:mm}\n\n{_thongBao.NoiDung}",
                    _thongBao.TieuDe,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }
}