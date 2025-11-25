using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Demo_Layout
{

    public partial class UserControlDanhMucChiTieu : UserControl
    {
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly IServiceProvider _serviceProvider;
        private const int CURRENT_USER_ID = 1;
        
        public UserControlDanhMucChiTieu(IDbContextFactory<QLTCCNContext> dbFactory)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
        }

        private void UCDanhMucChiTieu_Load(object sender, EventArgs e)
        {
            LoadTreeView();
            LogHelper.GhiLog(_dbFactory, "Quản lý danh mục chi tiêu", CURRENT_USER_ID); // ghi log
        }
        private void LoadTreeView()
        {
            tvDanhMuc.Nodes.Clear(); // Xóa cây cũ

            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    // Lấy TẤT CẢ danh mục của người dùng
                    var allCategories = db.DanhMucChiTieus
                                          .Where(dm => dm.MaNguoiDung == CURRENT_USER_ID)
                                          .ToList();

                    // Lọc ra các mục gốc (Cha = null)
                    var rootCategories = allCategories
                                         .Where(dm => dm.DanhMucCha == null)
                                         .ToList();

                    foreach (var rootCat in rootCategories)
                    {
                        // Tạo Node gốc
                        TreeNode rootNode = new TreeNode(rootCat.TenDanhMuc);
                        rootNode.Tag = rootCat.MaDanhMuc; // Lưu ID vào Tag

                        // Gọi đệ quy để thêm các Node con
                        AddChildNodes(rootNode, rootCat.MaDanhMuc, allCategories);

                        tvDanhMuc.Nodes.Add(rootNode);
                    }
                }
                //tvDanhMuc.ExpandAll(); // Mở rộng tất cả các node
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu TreeView: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddChildNodes(TreeNode parentNode, int parentId, List<DanhMucChiTieu> allCategories)
        {
            // Lấy các mục con của mục cha hiện tại
            var childCategories = allCategories
                                  .Where(dm => dm.DanhMucCha == parentId)
                                  .ToList();

            foreach (var childCat in childCategories)
            {
                TreeNode childNode = new TreeNode(childCat.TenDanhMuc);
                childNode.Tag = childCat.MaDanhMuc; // Lưu ID

                // Tiếp tục đệ quy cho chính nó
                AddChildNodes(childNode, childCat.MaDanhMuc, allCategories);

                parentNode.Nodes.Add(childNode);
            }
        }


        /// Mở form Thêm (Create)
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            frmThemDanhMuc frm = new frmThemDanhMuc(_dbFactory);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadTreeView(); // Tải lại TreeView
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            ThucHienSua();
        }
        private void TvDanhMuc_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ThucHienSua();
        }

        private void ThucHienSua()
        {
            if (tvDanhMuc.SelectedNode == null)
            {
                MessageBox.Show("Vui lòng chọn danh mục cần sửa.");
                return;
            }

            int maDanhMuc = (int)tvDanhMuc.SelectedNode.Tag;

            // Khởi tạo form
            frmThemDanhMuc frm = new frmThemDanhMuc(_dbFactory);

            // Kích hoạt chế độ Sửa
            frm.CheDoSua(maDanhMuc);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadTreeView();
                MessageBox.Show("Cập nhật thành công!");
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (tvDanhMuc.SelectedNode == null)
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa.");
                return;
            }

            // Lấy thông tin để hiển thị confirm
            string tenDanhMuc = tvDanhMuc.SelectedNode.Text;
            int maDanhMuc = (int)tvDanhMuc.SelectedNode.Tag;

            // Cảnh báo mạnh mẽ vì xóa sẽ mất hết giao dịch
            var result = MessageBox.Show(
                $"CẢNH BÁO: Bạn đang muốn xóa danh mục '{tenDanhMuc}'.\n\n" +
                $"Hành động này sẽ XÓA TOÀN BỘ CÁC GIAO DỊCH thuộc danh mục này.\n" +
                "Bạn có chắc chắn muốn tiếp tục không?",
                "Xác nhận xóa nguy hiểm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var db = _dbFactory.CreateDbContext())
                    {
                        // 1. Kiểm tra xem có danh mục con không? (Nếu có con thì không cho xóa, bắt xóa con trước)
                        bool coCon = db.DanhMucChiTieus.Any(dm => dm.DanhMucCha == maDanhMuc);
                        if (coCon)
                        {
                            MessageBox.Show("Danh mục này đang chứa các danh mục con. Vui lòng xóa các danh mục con trước.");
                            return;
                        }

                        // 2. TÌM VÀ XÓA CÁC GIAO DỊCH LIÊN QUAN TRƯỚC (Theo yêu cầu của bạn)
                        var giaoDichLienQuan = db.GiaoDichs.Where(gd => gd.MaDanhMuc == maDanhMuc).ToList();
                        if (giaoDichLienQuan.Count > 0)
                        {
                            db.GiaoDichs.RemoveRange(giaoDichLienQuan);
                        }

                        // 3. XÓA DANH MỤC
                        var danhMuc = db.DanhMucChiTieus.Find(maDanhMuc);
                        if (danhMuc != null)
                        {
                            db.DanhMucChiTieus.Remove(danhMuc);
                            db.SaveChanges(); // Commit transaction (xóa cả GD và DM cùng lúc)

                            LoadTreeView();
                            MessageBox.Show($"Đã xóa danh mục và {giaoDichLienQuan.Count} giao dịch liên quan.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                }
            }
        }


        // Placeholder event

    }
}



