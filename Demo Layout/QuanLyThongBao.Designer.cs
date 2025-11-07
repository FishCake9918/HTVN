using System.Drawing;
using System.Windows.Forms;

namespace Demo_Layout
{
    partial class QuanLyThongBao
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlNoiDung;
        private DataGridView dgvDanhSach;
        private Button btnCapNhat;
        private Button btnXoa;
        private TextBox txtTimKiem; 
        private Button btnTaoMoi; 

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuanLyThongBao));
            pnlNoiDung = new Panel();
            txtTimKiem = new TextBox();
            btnTaoMoi = new Button();
            btnCapNhat = new Button();
            btnXoa = new Button();
            dgvDanhSach = new DataGridView();
            pnlNoiDung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDanhSach).BeginInit();
            SuspendLayout();
            // 
            // pnlNoiDung
            // 
            pnlNoiDung.BackColor = Color.FromArgb(227, 242, 253);
            pnlNoiDung.Controls.Add(txtTimKiem);
            pnlNoiDung.Controls.Add(btnTaoMoi);
            pnlNoiDung.Controls.Add(btnCapNhat);
            pnlNoiDung.Controls.Add(btnXoa);
            pnlNoiDung.Controls.Add(dgvDanhSach);
            pnlNoiDung.Dock = DockStyle.Fill;
            pnlNoiDung.Location = new Point(0, 0);
            pnlNoiDung.Margin = new Padding(4);
            pnlNoiDung.Name = "pnlNoiDung";
            pnlNoiDung.Padding = new Padding(30);
            pnlNoiDung.Size = new Size(1153, 659);
            pnlNoiDung.TabIndex = 0;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtTimKiem.ForeColor = Color.Navy;
            txtTimKiem.Location = new Point(30, 33);
            txtTimKiem.Multiline = true;
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(579, 40);
            txtTimKiem.TabIndex = 5;
            // 
            // btnTaoMoi
            // 
            btnTaoMoi.BackColor = Color.White;
            btnTaoMoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTaoMoi.ForeColor = Color.Navy;
            btnTaoMoi.Image = (Image)resources.GetObject("btnTaoMoi.Image");
            btnTaoMoi.ImageAlign = ContentAlignment.MiddleLeft;
            btnTaoMoi.Location = new Point(804, 33);
            btnTaoMoi.Name = "btnTaoMoi";
            btnTaoMoi.Size = new Size(82, 40);
            btnTaoMoi.TabIndex = 2;
            btnTaoMoi.Text = "Tạo";
            btnTaoMoi.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnTaoMoi.UseVisualStyleBackColor = false;
            btnTaoMoi.Click += btnTaoMoi_Click;
            // 
            // btnCapNhat
            // 
            btnCapNhat.BackColor = Color.White;
            btnCapNhat.Enabled = false;
            btnCapNhat.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCapNhat.ForeColor = Color.Navy;
            btnCapNhat.Image = (Image)resources.GetObject("btnCapNhat.Image");
            btnCapNhat.ImageAlign = ContentAlignment.MiddleLeft;
            btnCapNhat.Location = new Point(892, 33);
            btnCapNhat.Name = "btnCapNhat";
            btnCapNhat.Size = new Size(136, 40);
            btnCapNhat.TabIndex = 3;
            btnCapNhat.Text = "Cập Nhật";
            btnCapNhat.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCapNhat.UseVisualStyleBackColor = false;
            btnCapNhat.Click += btnCapNhat_Click;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = Color.White;
            btnXoa.Enabled = false;
            btnXoa.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnXoa.ForeColor = Color.Navy;
            btnXoa.Image = (Image)resources.GetObject("btnXoa.Image");
            btnXoa.ImageAlign = ContentAlignment.MiddleLeft;
            btnXoa.Location = new Point(1034, 33);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(89, 40);
            btnXoa.TabIndex = 4;
            btnXoa.Text = "Xóa";
            btnXoa.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnXoa.UseVisualStyleBackColor = false;
            btnXoa.Click += btnXoa_Click;
            // 
            // dgvDanhSach
            // 
            dgvDanhSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDanhSach.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDanhSach.Location = new Point(30, 80);
            dgvDanhSach.Margin = new Padding(4);
            dgvDanhSach.MultiSelect = false;
            dgvDanhSach.Name = "dgvDanhSach";
            dgvDanhSach.ReadOnly = true;
            dgvDanhSach.RowHeadersWidth = 62;
            dgvDanhSach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhSach.Size = new Size(1093, 549);
            dgvDanhSach.TabIndex = 1;
            dgvDanhSach.SelectionChanged += dgvDanhSach_SelectionChanged;
            // 
            // QuanLyThongBao
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1153, 659);
            Controls.Add(pnlNoiDung);
            MaximumSize = new Size(1175, 715);
            MinimumSize = new Size(1175, 715);
            Name = "QuanLyThongBao";
            Text = "Quản lý thông báo Admin";
            pnlNoiDung.ResumeLayout(false);
            pnlNoiDung.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDanhSach).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}