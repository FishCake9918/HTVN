using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Data; // Namespace chứa QLTCCNContext và Entity

namespace Demo_Layout
{
    public partial class UserControlDanhMucChiTieu : UserControl
    {
        private const int CURRENT_USER_ID = 1; // Giả định người dùng hiện tại

        // Chuỗi kết nối (Copy từ UserControlQuanLyGiaoDich để đồng bộ)
        private const string ConnectionString = "Data Source=DESKTOP-6QOFBT9\\SQLEXPRESS;Initial Catalog=QLTCCN;Integrated Security=True;TrustServerCertificate=True";

        public UserControlDanhMucChiTieu()
        {
            InitializeComponent();
        }

        // --- HÀM HỖ TRỢ KHỞI TẠO CONTEXT ---
        private QLTCCNContext GetContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<QLTCCNContext>();
            optionsBuilder.UseSqlServer(ConnectionString);
            return new QLTCCNContext(optionsBuilder.Options);
        }

        private void UCDanhMucChiTieu_Load(object sender, EventArgs e)
        {
            LoadTreeView();
        }

        private void LoadTreeView()
        {
            tvDanhMuc.Nodes.Clear(); // Xóa cây cũ

            try
            {
                using (var context = GetContext())
                {
                    // Lấy TẤT CẢ danh mục của người dùng
                    // Sử dụng Entity: DanhMucChiTieu
                    var allCategories = context.DanhMucChiTieus
                                               .Where(dm => dm.MaNguoiDung == CURRENT_USER_ID)
                                               .ToList();

                    // Lọc ra các mục gốc (DanhMucCha == null)
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
                tvDanhMuc.ExpandAll(); // Mở rộng tất cả các node
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

        // --- CHỨC NĂNG THÊM ---
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Mở form FrmThemDanhMuc (Giả định bạn đã tạo form này giống FrmThemGiaoDich)
            frmThemDanhMuc frm = new frmThemDanhMuc();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadTreeView(); // Tải lại TreeView sau khi thêm
            }
        }

        // --- CHỨC NĂNG SỬA ---
        private void btnSua_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem có node nào được chọn không
            if (tvDanhMuc.SelectedNode == null)
            {
                MessageBox.Show("Vui lòng chọn một danh mục để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Lấy MaDanhMuc (ID) từ Tag của Node được chọn
                int selectedId = (int)tvDanhMuc.SelectedNode.Tag;

                // 3. Gọi form Sửa (Truyền ID vào constructor)
                // Giả định constructor FrmThemDanhMuc(int id) tồn tại để load dữ liệu cũ lên
                frmThemDanhMuc frm = new frmThemDanhMuc(selectedId);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadTreeView(); // Tải lại TreeView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- CHỨC NĂNG XÓA ---
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (tvDanhMuc.SelectedNode == null)
            {
                MessageBox.Show("Vui lòng chọn một danh mục để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show("Bạn có chắc muốn xóa danh mục này không?", "Xác nhận Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    int selectedId = (int)tvDanhMuc.SelectedNode.Tag;

                    using (var context = GetContext())
                    {
                        // 1. Kiểm tra ràng buộc: Có danh mục con không?
                        bool coCon = context.DanhMucChiTieus.Any(dm => dm.DanhMucCha == selectedId);
                        if (coCon)
                        {
                            MessageBox.Show("Lỗi: Không thể xóa danh mục này vì nó là danh mục cha của các mục khác. Vui lòng xóa các mục con trước.", "Lỗi Ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // 2. Kiểm tra ràng buộc: Có giao dịch nào đang dùng danh mục này không?
                        // (Quan trọng: Tránh lỗi Orphaned Records hoặc lỗi Foreign Key SQL)
                        bool coGiaoDich = context.GiaoDichs.Any(gd => gd.MaDanhMuc == selectedId);
                        if (coGiaoDich)
                        {
                            MessageBox.Show("Lỗi: Không thể xóa danh mục này vì đã có giao dịch phát sinh. Hãy xóa hoặc chuyển đổi các giao dịch liên quan trước.", "Lỗi Ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // 3. Tiến hành Xóa
                        var danhMucCanXoa = context.DanhMucChiTieus.Find(selectedId);
                        if (danhMucCanXoa != null)
                        {
                            context.DanhMucChiTieus.Remove(danhMucCanXoa);
                            context.SaveChanges();
                            LoadTreeView(); // Tải lại TreeView
                            MessageBox.Show("Xóa thành công!", "Thông báo");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvDanhSachMuc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Không dùng đến nếu dùng TreeView
        }
    }
}