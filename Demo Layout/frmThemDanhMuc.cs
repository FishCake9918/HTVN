using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Data; // Namespace chứa QLTCCNContext và Entity

namespace Demo_Layout
{
    public partial class frmThemDanhMuc : Form
    {
        private const int CURRENT_USER_ID = 1;

        // Chuỗi kết nối
        private const string ConnectionString = "Data Source=DESKTOP-6QOFBT9\\SQLEXPRESS;Initial Catalog=QLTCCN;Integrated Security=True;TrustServerCertificate=True";

        // Biến lưu ID danh mục đang sửa (nếu có). Null nghĩa là đang Thêm mới.
        private int? _maDanhMucEdited = null;

        // --- CONSTRUCTOR 1: Dùng cho Thêm Mới ---
        public frmThemDanhMuc()
        {
            InitializeComponent();
            _maDanhMucEdited = null;
            this.Text = "Thêm Danh Mục Mới";
        }

        // --- CONSTRUCTOR 2: Dùng cho Sửa ---
        public frmThemDanhMuc(int maDanhMuc)
        {
            InitializeComponent();
            _maDanhMucEdited = maDanhMuc;
            this.Text = "Cập Nhật Danh Mục";
        }

        // Helper tạo Context
        private QLTCCNContext GetContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<QLTCCNContext>();
            optionsBuilder.UseSqlServer(ConnectionString);
            return new QLTCCNContext(optionsBuilder.Options);
        }

        private void frmThemDanhMuc_Load(object sender, EventArgs e)
        {
            LoadComboBoxDanhMucCha();

            // Nếu đang ở chế độ Sửa, load dữ liệu cũ lên form
            if (_maDanhMucEdited.HasValue)
            {
                LoadDataForEdit();
            }
        }

        private void LoadDataForEdit()
        {
            try
            {
                using (var context = GetContext())
                {
                    var entity = context.DanhMucChiTieus.Find(_maDanhMucEdited.Value);
                    if (entity != null)
                    {
                        txtTenDanhMuc.Text = entity.TenDanhMuc;

                        // Set giá trị cho ComboBox (Nếu null thì về 0 - Gốc)
                        cboDanhMucCha.SelectedValue = entity.DanhMucCha ?? 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu cũ: " + ex.Message);
            }
        }

        private void LoadComboBoxDanhMucCha()
        {
            try
            {
                using (var context = GetContext())
                {
                    // Lấy danh sách danh mục cha (Chỉ lấy các mục Gốc để làm cha)
                    var query = context.DanhMucChiTieus
                                       .Where(dm => dm.MaNguoiDung == CURRENT_USER_ID && dm.DanhMucCha == null);

                    // LOGIC QUAN TRỌNG: Nếu đang Sửa, loại bỏ chính nó khỏi danh sách cha
                    // (Không thể chọn chính mình làm cha của mình)
                    if (_maDanhMucEdited.HasValue)
                    {
                        int idToExclude = _maDanhMucEdited.Value;
                        query = query.Where(dm => dm.MaDanhMuc != idToExclude);
                    }

                    var danhSachCha = query.Select(dm => new { dm.MaDanhMuc, dm.TenDanhMuc })
                                           .ToList();

                    // Tạo mục "Gốc"
                    var dataSource = new List<object>
                    {
                        new { MaDanhMuc = 0, TenDanhMuc = "(Là danh mục gốc)" }
                    };

                    dataSource.AddRange(danhSachCha);

                    cboDanhMucCha.DataSource = dataSource;
                    cboDanhMucCha.DisplayMember = "TenDanhMuc";
                    cboDanhMucCha.ValueMember = "MaDanhMuc";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh mục cha: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // 1. Validation
            if (string.IsNullOrWhiteSpace(txtTenDanhMuc.Text))
            {
                MessageBox.Show("Tên danh mục không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDanhMuc.Focus();
                return;
            }

            try
            {
                using (var context = GetContext())
                {
                    // Lấy ID cha từ ComboBox
                    int maChaDaChon = (int)cboDanhMucCha.SelectedValue;
                    int? idChaLuuDb = (maChaDaChon == 0) ? (int?)null : maChaDaChon;

                    if (_maDanhMucEdited == null)
                    {
                        // --- TRƯỜNG HỢP THÊM MỚI ---
                        var danhMucMoi = new DanhMucChiTieu
                        {
                            TenDanhMuc = txtTenDanhMuc.Text.Trim(),
                            MaNguoiDung = CURRENT_USER_ID,
                            DanhMucCha = idChaLuuDb
                        };

                        context.DanhMucChiTieus.Add(danhMucMoi);
                        context.SaveChanges();
                        MessageBox.Show("Thêm danh mục mới thành công!", "Thành công");
                    }
                    else
                    {
                        // --- TRƯỜNG HỢP SỬA (UPDATE) ---
                        var danhMucCanSua = context.DanhMucChiTieus.Find(_maDanhMucEdited.Value);
                        if (danhMucCanSua != null)
                        {
                            danhMucCanSua.TenDanhMuc = txtTenDanhMuc.Text.Trim();
                            danhMucCanSua.DanhMucCha = idChaLuuDb;

                            // (Không cần sửa MaNguoiDung)

                            context.SaveChanges();
                            MessageBox.Show("Cập nhật danh mục thành công!", "Thành công");
                        }
                        else
                        {
                            MessageBox.Show("Danh mục này không còn tồn tại.", "Lỗi");
                        }
                    }
                }

                this.DialogResult = DialogResult.OK; // Báo cho form cha biết để reload cây
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu vào CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}