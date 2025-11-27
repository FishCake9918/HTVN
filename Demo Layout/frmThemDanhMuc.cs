using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Data;

namespace Demo_Layout
{
    public partial class frmThemDanhMuc : Form
    {
        // XÓA: public Action OnDataSaved; // Dòng này bị xóa theo yêu cầu "trước khi có nút mở Form Danh mục"

        private readonly IDbContextFactory<QLTCCNContext> _dbFactory;
        private readonly CurrentUserContext _userContext;
        private int? _maDanhMucCanSua = null;

        public frmThemDanhMuc(IDbContextFactory<QLTCCNContext> dbFactory, CurrentUserContext userContext)
        {
            InitializeComponent();
            _dbFactory = dbFactory;
            _userContext = userContext;

            this.Load += frmThemDanhMuc_Load;
            this.btnLuu.Click += btnLuu_Click;
            this.btnHuy.Click += btnHuy_Click;
        }

        public void CheDoSua(int maDanhMuc)
        {
            _maDanhMucCanSua = maDanhMuc;
            this.Text = "Cập nhật Danh mục";
            btnLuu.Text = "Cập nhật";

            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    var dm = db.DanhMucChiTieus.Find(maDanhMuc);
                    if (dm != null)
                    {
                        txtTenDanhMuc.Text = dm.TenDanhMuc;

                        LoadComboBoxDanhMucCha(dm.MaDanhMuc);

                        cboDanhMucCha.SelectedValue = dm.DanhMucCha ?? 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void frmThemDanhMuc_Load(object sender, EventArgs e)
        {
            if (_maDanhMucCanSua == null)
            {
                LoadComboBoxDanhMucCha(null);
            }
        }

        private void LoadComboBoxDanhMucCha(int? excludeId)
        {
            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    int currentUserId = _userContext.MaNguoiDung ?? 0;
                    if (currentUserId == 0)
                    {
                        MessageBox.Show("Lỗi: Không xác định được người dùng hiện tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var danhSachCha = db.DanhMucChiTieus
                                             .Where(dm => dm.MaNguoiDung == currentUserId && dm.DanhMucCha == null)
                                             .Select(dm => new { dm.MaDanhMuc, dm.TenDanhMuc })
                                             .ToList();

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
            // 1. Validate cơ bản
            string tenNhapVao = txtTenDanhMuc.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenNhapVao))
            {
                MessageBox.Show("Tên danh mục không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDanhMuc.Focus();
                return;
            }

            int currentUserId = _userContext.MaNguoiDung ?? 0;
            if (currentUserId == 0)
            {
                MessageBox.Show("Lỗi: Không xác định được người dùng hiện tại khi lưu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var db = _dbFactory.CreateDbContext())
                {
                    // KIỂM TRA TRÙNG TÊN MỚI (Case-Insensitive)
                    // (Logic kiểm tra trùng lặp đã bị lỗi ở bước trước, tôi giữ nguyên logic cũ để quay đầu)
                    var danhMucTrung = db.DanhMucChiTieus
                        .Where(dm => dm.MaNguoiDung == currentUserId)
                        .ToList()
                        .FirstOrDefault(dm =>
                            string.Equals(dm.TenDanhMuc, tenNhapVao, StringComparison.OrdinalIgnoreCase)
                        );

                    if (danhMucTrung != null)
                    {
                        if (_maDanhMucCanSua == null || danhMucTrung.MaDanhMuc != _maDanhMucCanSua)
                        {
                            MessageBox.Show($"Tên danh mục '{tenNhapVao}' đã tồn tại.",
                                "Trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtTenDanhMuc.SelectAll();
                            txtTenDanhMuc.Focus();
                            return;
                        }
                    }

                    // 3. Tiến hành Lưu
                    DanhMucChiTieu danhMuc;

                    if (_maDanhMucCanSua == null)
                    {
                        // Thêm mới
                        danhMuc = new DanhMucChiTieu();
                        danhMuc.MaNguoiDung = currentUserId;
                        db.DanhMucChiTieus.Add(danhMuc);
                    }
                    else
                    {
                        // Sửa
                        danhMuc = db.DanhMucChiTieus.Find(_maDanhMucCanSua);
                        if (danhMuc == null)
                        {
                            MessageBox.Show("Danh mục không tồn tại.");
                            return;
                        }
                    }

                    danhMuc.TenDanhMuc = tenNhapVao;

                    int maCha = 0;
                    if (cboDanhMucCha.SelectedValue != null)
                        int.TryParse(cboDanhMucCha.SelectedValue.ToString(), out maCha);

                    if (_maDanhMucCanSua != null && maCha == _maDanhMucCanSua)
                    {
                        MessageBox.Show("Một danh mục không thể là cha của chính nó.", "Lỗi Logic", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    danhMuc.DanhMucCha = (maCha == 0) ? null : (int?)maCha;

                    db.SaveChanges();
                }

                // XÓA CALLBACK: Bỏ qua OnDataSaved?.Invoke();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}