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


        /// Mở form Thêm (Create)
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Gọi form Thêm/Sửa ở chế độ Thêm (không truyền ID)
            //frmThemDanhMuc frm = new frmThemDanhMuc();

            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    LoadTreeView(); // Tải lại TreeView
            //}

            // Cách 1: Nếu Form này đơn giản không cần DI --> NHỚ CHỈNH LẠI 
            //frmThemDanhMuc frm = new frmThemDanhMuc(/*_dbFactory*/);

            // Cách 2: Chuẩn DI (Bạn cần đăng ký frmThemDanhMuc trong Program.cs trước)
            //using (var frm = _serviceProvider.GetRequiredService<frmThemDanhMuc>())
            //{
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        LoadTreeView();
            //    }
            //}
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            //    // 1. Kiểm tra xem có node nào được chọn không
            //    if (tvDanhMuc.SelectedNode == null)
            //    {
            //        MessageBox.Show("Vui lòng chọn một danh mục để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }

            //    try
            //    {
            //        // 2. Lấy MaDanhMuc (ID) từ Tag của Node được chọn
            //        int selectedId = (int)tvDanhMuc.SelectedNode.Tag;

            //        // 3. Gọi form Sửa
            //        frmThemSuaDanhMuc frm = new frmThemSuaDanhMuc(selectedId);
            //        if (frm.ShowDialog() == DialogResult.OK)
            //        {
            //            LoadTreeView(); // Tải lại TreeView
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Lỗi khi lấy ID: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
        }

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

                    using (var db = _dbFactory.CreateDbContext())
                    {
                        // 1. Kiểm tra ràng buộc con (Danh mục cha)
                        bool coCon = db.DanhMucChiTieus.Any(dm => dm.DanhMucCha == selectedId);
                        if (coCon)
                        {
                            MessageBox.Show("Lỗi: Không thể xóa danh mục này vì nó là danh mục cha của các mục khác. Vui lòng xóa các mục con trước.", "Lỗi Ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        //// 2. Kiểm tra ràng buộc Giao dịch (Quan trọng)
                        //bool coGiaoDich = db.GIAO_DICH.Any(gd => gd.MaDanhMuc == selectedId);
                        //if (coGiaoDich)
                        //{
                        //    MessageBox.Show("Lỗi: Không thể xóa danh mục này vì đã có giao dịch sử dụng nó.", "Lỗi Ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    return;
                        //}

                        // (Bạn cũng nên kiểm tra BẢNG NGÂN SÁCH nếu cần)

                        // 3. Tiến hành Xóa
                        var danhMucCanXoa = db.DanhMucChiTieus.Find(selectedId);
                        if (danhMucCanXoa != null)
                        {
                            db.DanhMucChiTieus.Remove(danhMucCanXoa);
                            db.SaveChanges();

                            MessageBox.Show("Xóa thành công!");
                            LoadTreeView(); // Tải lại TreeView
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Placeholder event

    }
}



