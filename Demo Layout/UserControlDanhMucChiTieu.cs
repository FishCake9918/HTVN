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
        
        public UserControlDanhMucChiTieu(IDbContextFactory<QLTCCNContext> dbFactory, IServiceProvider _serviceProvider)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
        }

        private void UCDanhMucChiTieu_Load(object sender, EventArgs e)
        {
            KiemTraVaTaoDuLieuMau();
            LoadTreeView();
            LogHelper.GhiLog(_dbFactory, "Quản lý danh mục chi tiêu", CURRENT_USER_ID); // ghi log
        }

        // Trong file DanhMucChiTieu.cs (hoặc class Seeder)

        private void KiemTraVaTaoDuLieuMau()
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    bool daCoDuLieu = db.DanhMucChiTieus.Any(dm => dm.MaNguoiDung == CURRENT_USER_ID);

                    if (!daCoDuLieu)
                    {
                        TaoDanhMucMacDinh(db, CURRENT_USER_ID);                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private static void TaoDanhMucMacDinh(QLTCCNContext db, int userId)
        {
            var danhSachGoc = new List<DanhMucChiTieu>();

            // --- 1. ĂN UỐNG ---
            var anUong = new DanhMucChiTieu
            {
                TenDanhMuc = "Ăn uống",
                MaNguoiDung = userId,
                DanhMucCha = null,
                DanhMucCon = new List<DanhMucChiTieu> // EF Core tự động liên kết con với cha
        {
            new DanhMucChiTieu { TenDanhMuc = "Cafe", MaNguoiDung = userId },
            new DanhMucChiTieu { TenDanhMuc = "Nhà hàng", MaNguoiDung = userId },
            new DanhMucChiTieu { TenDanhMuc = "Đi chợ/Siêu thị", MaNguoiDung = userId }
        }
            };
            danhSachGoc.Add(anUong);

            // --- 2. HÓA ĐƠN ---
            var hoaDon = new DanhMucChiTieu
            {
                TenDanhMuc = "Hóa đơn",
                MaNguoiDung = userId,
                DanhMucCha = null,
                DanhMucCon = new List<DanhMucChiTieu>
        {
            new DanhMucChiTieu { TenDanhMuc = "Tiền điện", MaNguoiDung = userId },
            new DanhMucChiTieu { TenDanhMuc = "Tiền nước", MaNguoiDung = userId },
            new DanhMucChiTieu { TenDanhMuc = "Internet", MaNguoiDung = userId },
            new DanhMucChiTieu { TenDanhMuc = "Điện thoại", MaNguoiDung = userId }
        }
            };
            danhSachGoc.Add(hoaDon);

            // --- 3. GIẢI TRÍ ---
            var giaiTri = new DanhMucChiTieu
            {
                TenDanhMuc = "Giải trí",
                MaNguoiDung = userId,
                DanhMucCha = null,
                DanhMucCon = new List<DanhMucChiTieu>
        {
            new DanhMucChiTieu { TenDanhMuc = "Xem phim", MaNguoiDung = userId },
            new DanhMucChiTieu { TenDanhMuc = "Nghe nhạc (Spotify)", MaNguoiDung = userId },
            new DanhMucChiTieu { TenDanhMuc = "Du lịch", MaNguoiDung = userId }
        }
            };
            danhSachGoc.Add(giaiTri);

            // --- 4. DI CHUYỂN ---
            var diChuyen = new DanhMucChiTieu
            {
                TenDanhMuc = "Di chuyển",
                MaNguoiDung = userId,
                DanhMucCha = null,
                DanhMucCon = new List<DanhMucChiTieu>
        {
            new DanhMucChiTieu { TenDanhMuc = "Xăng xe", MaNguoiDung = userId },
            new DanhMucChiTieu { TenDanhMuc = "Gửi xe", MaNguoiDung = userId },
            new DanhMucChiTieu { TenDanhMuc = "Grab/Be", MaNguoiDung = userId }
        }
            };
            danhSachGoc.Add(diChuyen);

            // --- 5. MUA SẮM ---
            var muaSam = new DanhMucChiTieu
            {
                TenDanhMuc = "Mua sắm",
                MaNguoiDung = userId,
                DanhMucCha = null,
                DanhMucCon = new List<DanhMucChiTieu>
        {
            new DanhMucChiTieu { TenDanhMuc = "Quần áo", MaNguoiDung = userId },
            new DanhMucChiTieu { TenDanhMuc = "Đồ gia dụng", MaNguoiDung = userId },
            new DanhMucChiTieu { TenDanhMuc = "Mỹ phẩm", MaNguoiDung = userId }
        }
            };
            danhSachGoc.Add(muaSam);

            // --- 6. SỨC KHỎE ---
            var sucKhoe = new DanhMucChiTieu
            {
                TenDanhMuc = "Sức khỏe",
                MaNguoiDung = userId,
                DanhMucCha = null,
                DanhMucCon = new List<DanhMucChiTieu>
        {
            new DanhMucChiTieu { TenDanhMuc = "Khám bệnh", MaNguoiDung = userId },
            new DanhMucChiTieu { TenDanhMuc = "Thuốc", MaNguoiDung = userId },
            new DanhMucChiTieu { TenDanhMuc = "Bảo hiểm y tế", MaNguoiDung = userId }
        }
            };
            danhSachGoc.Add(sucKhoe);

            // --- 7. GIÁO DỤC ---
            var giaoDuc = new DanhMucChiTieu
            {
                TenDanhMuc = "Giáo dục",
                MaNguoiDung = userId,
                DanhMucCha = null,
                DanhMucCon = new List<DanhMucChiTieu>
        {
            new DanhMucChiTieu { TenDanhMuc = "Học phí", MaNguoiDung = userId },
            new DanhMucChiTieu { TenDanhMuc = "Mua sách", MaNguoiDung = userId },
            new DanhMucChiTieu { TenDanhMuc = "Khóa học online", MaNguoiDung = userId }
        }
            };
            danhSachGoc.Add(giaoDuc);

            // --- 8. THU NHẬP ---
            var thuNhap = new DanhMucChiTieu
            {
                TenDanhMuc = "Thu nhập",
                MaNguoiDung = userId,
                DanhMucCha = null,
                DanhMucCon = new List<DanhMucChiTieu>
        {
            new DanhMucChiTieu { TenDanhMuc = "Lương", MaNguoiDung = userId },
            new DanhMucChiTieu { TenDanhMuc = "Thưởng", MaNguoiDung = userId },
            new DanhMucChiTieu { TenDanhMuc = "Thu nhập phụ", MaNguoiDung = userId }
        }
            };
            danhSachGoc.Add(thuNhap);

            // --- LƯU TẤT CẢ VÀO DB ---
            db.DanhMucChiTieus.AddRange(danhSachGoc);
            db.SaveChanges();
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



