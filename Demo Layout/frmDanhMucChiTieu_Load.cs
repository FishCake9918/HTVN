using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Demo_Layout
{
    public partial class frmDanhMucChiTieu : Form
    {
        public frmDanhMucChiTieu()
        {
            InitializeComponent();
        }

        public Panel ThemNhom
        {
            get { return pnlThemNhom; }
        }

        public System.Windows.Forms.TreeView DanhSachNhom
        {
            get { return trvDanhSachNhom; }
        }

        private void AddDataToTreeView() // Recommend
        {
            // Xóa các node cũ trước khi thêm mới
            trvDanhSachNhom.Nodes.Clear();

            // Tạo nhóm Ăn uống
            TreeNode foodNode = new TreeNode("Ăn uống");
            foodNode.Nodes.Add("Cà phê");
            foodNode.Nodes.Add("Nhà hàng");
            foodNode.Nodes.Add("Mua đồ ăn vặt");

            // Tạo nhóm Di chuyển
            TreeNode transportNode = new TreeNode("Di chuyển");
            transportNode.Nodes.Add("Xăng");
            transportNode.Nodes.Add("Vé xe buýt");
            transportNode.Nodes.Add("Bảo dưỡng xe");

            // Tạo nhóm Giải trí
            TreeNode entertainmentNode = new TreeNode("Giải trí");
            entertainmentNode.Nodes.Add("Phim ảnh");
            entertainmentNode.Nodes.Add("Game");
            entertainmentNode.Nodes.Add("Du lịch");

            // Tạo nhóm Hóa đơn & Dịch vụ
            TreeNode billsNode = new TreeNode("Hóa đơn & Dịch vụ");
            billsNode.Nodes.Add("Điện");
            billsNode.Nodes.Add("Nước");
            billsNode.Nodes.Add("Internet");
            billsNode.Nodes.Add("Điện thoại");

            // Tạo nhóm Tiết kiệm & Đầu tư
            TreeNode investNode = new TreeNode("Tiết kiệm & Đầu tư");
            investNode.Nodes.Add("Gửi tiết kiệm");
            investNode.Nodes.Add("Mua cổ phiếu");
            investNode.Nodes.Add("Crypto");

            // Thêm tất cả vào TreeView
            trvDanhSachNhom.Nodes.Add(foodNode);
            trvDanhSachNhom.Nodes.Add(transportNode);
            trvDanhSachNhom.Nodes.Add(entertainmentNode);
            trvDanhSachNhom.Nodes.Add(billsNode);
            trvDanhSachNhom.Nodes.Add(investNode);

            // Mở rộng tất cả node
            trvDanhSachNhom.ExpandAll();
        }

        private void frmDanhMucChiTieu_Load(object sender, EventArgs e)
        {
            AddDataToTreeView();
        }
    }
}


