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
using static System.Net.Mime.MediaTypeNames;

namespace Demo_Layout
{
    public partial class UserControlDanhMucChiTieu : UserControl
    {
        // ==================================================================================
        // 1. KHAI BÁO BIẾN & TÀI NGUYÊN
        // ==================================================================================
        private readonly IDbContextFactory<QLTCCNContext> _dbFactory; // Nhà máy tạo kết nối CSDL
        private readonly IServiceProvider _serviceProvider;

        // Biến lưu thông tin người dùng hiện tại (để biết danh mục của ai)
        private readonly CurrentUserContext _userContext;

        // Inject các dependency vào Constructor
        public UserControlDanhMucChiTieu(IDbContextFactory<QLTCCNContext> dbFactory, IServiceProvider serviceProvider, CurrentUserContext userContext)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _serviceProvider = serviceProvider;
            _userContext = userContext;
        }

        // ==================================================================================
        // 2. SỰ KIỆN LOAD FORM
        // ==================================================================================
        private void UCDanhMucChiTieu_Load(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem User mới có dữ liệu chưa, nếu chưa thì tạo mẫu
            KiemTraVaTaoDuLieuMau();

            // 2. Tải cây danh mục lên giao diện
            LoadTreeView();

            // 3. Ghi log truy cập
            LogHelper.GhiLog(_dbFactory, "Quản lý danh mục chi tiêu", _userContext.MaNguoiDung);
        }

        // ==================================================================================
        // 3. LOGIC TẠO DỮ LIỆU MẪU (SEEDING DATA)
        // ==================================================================================
        private void KiemTraVaTaoDuLieuMau()
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    int currentUserId = _userContext.MaNguoiDung.Value;

                    // Kiểm tra xem User này đã có bất kỳ danh mục nào chưa
                    bool daCoDuLieu = db.DanhMucChiTieus.Any(dm => dm.MaNguoiDung == currentUserId);

                    if (!daCoDuLieu)
                    {
                        // Nếu chưa có, hỏi người dùng xem có muốn tạo bộ mẫu không
                        if (MessageBox.Show("Bạn chưa có danh mục nào. Bạn có muốn tạo bộ danh mục mẫu không?", "Gợi ý", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            // Gọi hàm tạo danh sách danh mục mặc định
                            TaoDanhMucMacDinh(db, currentUserId);
                            MessageBox.Show("Đã tạo danh mục mẫu thành công!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra dữ liệu mẫu: " + ex.Message);
            }
        }

        // Hàm hỗ trợ tạo danh sách cứng các danh mục phổ biến
        private static void TaoDanhMucMacDinh(QLTCCNContext db, int userId)
        {
            var danhSachGoc = new List<DanhMucChiTieu>();

            // --- NHÓM 1: ĂN UỐNG (Kèm các mục con) ---
            var anUong = new DanhMucChiTieu
            {
                TenDanhMuc = "Ăn uống",
                MaNguoiDung = userId,
                DanhMucCha = null, // Là cha
                DanhMucCon = new List<DanhMucChiTieu>
                {
                    new DanhMucChiTieu { TenDanhMuc = "Cafe", MaNguoiDung = userId },
                    new DanhMucChiTieu { TenDanhMuc = "Nhà hàng", MaNguoiDung = userId },
                    new DanhMucChiTieu { TenDanhMuc = "Đi chợ/Siêu thị", MaNguoiDung = userId }
                }
            };
            danhSachGoc.Add(anUong);

            // --- NHÓM 2: HÓA ĐƠN ---
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

            // --- NHÓM 3: GIẢI TRÍ ---
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

            // --- NHÓM 4: DI CHUYỂN ---
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

            // --- NHÓM 5: MUA SẮM ---
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

            // --- NHÓM 6: SỨC KHỎE ---
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

            // --- NHÓM 7: GIÁO DỤC ---
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

            // --- NHÓM 8: THU NHẬP ---
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

        // ==================================================================================
        // 4. LOGIC HIỂN THỊ CÂY DANH MỤC (TreeView)
        // ==================================================================================
        private void LoadTreeView()
        {
            tvDanhMuc.Nodes.Clear(); // Xóa cây cũ trước khi load lại

            int currentUserId = _userContext.MaNguoiDung.Value;

            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    // Bước 1: Lấy TẤT CẢ danh mục của người dùng về RAM
                    var allCategories = db.DanhMucChiTieus
                                            .Where(dm => dm.MaNguoiDung == currentUserId)
                                            .ToList();

                    // Bước 2: Lọc ra các mục gốc (Root - không có cha)
                    var rootCategories = allCategories
                                            .Where(dm => dm.DanhMucCha == null)
                                            .ToList();

                    // Bước 3: Duyệt qua từng mục gốc để tạo Node và tìm con của nó
                    foreach (var rootCat in rootCategories)
                    {
                        TreeNode rootNode = new TreeNode(rootCat.TenDanhMuc);
                        rootNode.Tag = rootCat.MaDanhMuc; // Lưu ID vào Tag để dùng sau này (Sửa/Xóa)

                        // Gọi hàm đệ quy để tìm và thêm các Node con
                        AddChildNodes(rootNode, rootCat.MaDanhMuc, allCategories);

                        tvDanhMuc.Nodes.Add(rootNode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu TreeView: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm đệ quy để thêm Node con
        private void AddChildNodes(TreeNode parentNode, int parentId, List<DanhMucChiTieu> allCategories)
        {
            // Tìm các mục con của parentId trong danh sách tổng
            var childCategories = allCategories
                                    .Where(dm => dm.DanhMucCha == parentId)
                                    .ToList();

            foreach (var childCat in childCategories)
            {
                TreeNode childNode = new TreeNode(childCat.TenDanhMuc);
                childNode.Tag = childCat.MaDanhMuc; // Lưu ID

                // Tiếp tục đệ quy: Tìm cháu chắt của nó (nếu có)
                AddChildNodes(childNode, childCat.MaDanhMuc, allCategories);

                parentNode.Nodes.Add(childNode);
            }
        }

        // ==================================================================================
        // 5. CHỨC NĂNG THÊM - SỬA - XÓA
        // ==================================================================================

        // --- Nút THÊM ---
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Mở form con FrmThemSuaDanhMuc để thêm mới
            FrmThemSuaDanhMuc frm = new FrmThemSuaDanhMuc(_dbFactory, _userContext);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadTreeView(); // Nếu thêm thành công thì tải lại cây
            }
        }

        // --- Nút SỬA ---
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (tvDanhMuc.SelectedNode == null)
            {
                MessageBox.Show("Vui lòng chọn danh mục cần sửa.");
                return;
            }
            ThucHienSua();
        }

        // Sự kiện Double Click vào Node cũng mở form Sửa
        private void TvDanhMuc_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ThucHienSua();
        }

        private void ThucHienSua()
        {
            if (tvDanhMuc.SelectedNode == null) return;

            int maDanhMuc = (int)tvDanhMuc.SelectedNode.Tag;

            // Khởi tạo form con và truyền ID cần sửa sang
            FrmThemSuaDanhMuc frm = new FrmThemSuaDanhMuc(_dbFactory, _userContext);
            frm.CheDoSua(maDanhMuc);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadTreeView();
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // --- Nút XÓA ---
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (tvDanhMuc.SelectedNode == null)
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa.");
                return;
            }

            string tenDanhMuc = tvDanhMuc.SelectedNode.Text;
            int maDanhMuc = (int)tvDanhMuc.SelectedNode.Tag;

            // Cảnh báo người dùng: Xóa danh mục sẽ mất luôn giao dịch
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
                        // 1. Kiểm tra ràng buộc: Không được xóa danh mục cha đang có con
                        bool coCon = db.DanhMucChiTieus.Any(dm => dm.DanhMucCha == maDanhMuc);
                        if (coCon)
                        {
                            MessageBox.Show("Danh mục này đang chứa các danh mục con. Vui lòng xóa các danh mục con trước.");
                            return;
                        }

                        // 2. XÓA CÁC GIAO DỊCH LIÊN QUAN TRƯỚC (Cascading Delete thủ công)
                        var giaoDichLienQuan = db.GiaoDichs.Where(gd => gd.MaDanhMuc == maDanhMuc).ToList();
                        if (giaoDichLienQuan.Count > 0)
                        {
                            db.GiaoDichs.RemoveRange(giaoDichLienQuan);
                        }

                        // 3. SAU ĐÓ MỚI XÓA DANH MỤC
                        var danhMuc = db.DanhMucChiTieus.Find(maDanhMuc);
                        if (danhMuc != null)
                        {
                            db.DanhMucChiTieus.Remove(danhMuc);
                            db.SaveChanges(); // Lưu thay đổi vào DB

                            LoadTreeView(); // Refresh lại cây
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
    }
}