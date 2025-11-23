using Krypton.Toolkit;

namespace Demo_Layout
{
    partial class FrmChinhSuaDoiTuongGiaoDich
    {
        private System.ComponentModel.IContainer components = null;

        // Khai báo controls (sử dụng từ khóa private/public partial class FrmChinhSuaDoiTuongGiaoDich trong file .cs chính)
        public KryptonTextBox txtTen;
        public KryptonTextBox txtGhiChu;
        private KryptonLabel labelTen;
        private KryptonButton btnLuu;
        private KryptonButton btnHuy;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        // PHƯƠNG THỨC KHỞI TẠO CÁC CONTROLS CỦA FORM (CHỈ CÓ 1 LẦN)
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChinhSuaDoiTuongGiaoDich));
            txtTen = new KryptonTextBox();
            txtGhiChu = new KryptonTextBox();
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            label2 = new Label();
            btnThoat = new Button();
            SuspendLayout();
            // 
            // txtTen
            // 
            txtTen.Location = new Point(152, 56);
            txtTen.Name = "txtTen";
            txtTen.Size = new Size(300, 27);
            txtTen.TabIndex = 4;
            // 
            // txtGhiChu
            // 
            txtGhiChu.Location = new Point(152, 96);
            txtGhiChu.Name = "txtGhiChu";
            txtGhiChu.Size = new Size(300, 27);
            txtGhiChu.TabIndex = 2;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(250, 110, 6);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Location = new Point(358, 162);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 5;
            button1.Text = "Huỷ";
            button1.UseVisualStyleBackColor = false;
            button1.Click += btnHuy_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(250, 110, 6);
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button2.ForeColor = Color.White;
            button2.Location = new Point(246, 162);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 6;
            button2.Text = "Lưu";
            button2.UseVisualStyleBackColor = false;
            button2.Click += btnLuu_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(22, 63);
            label1.Name = "label1";
            label1.Size = new Size(115, 20);
            label1.TabIndex = 7;
            label1.Text = "Tên đối tượng: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(22, 103);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 8;
            label2.Text = "Ghi chú: ";
            // 
            // btnThoat
            // 
            btnThoat.FlatAppearance.BorderSize = 0;
            btnThoat.FlatStyle = FlatStyle.Flat;
            btnThoat.Image = (Image)resources.GetObject("btnThoat.Image");
            btnThoat.Location = new Point(423, 12);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(29, 29);
            btnThoat.TabIndex = 14;
            btnThoat.UseVisualStyleBackColor = true;
            btnThoat.Click += btnThoat_Click;
            // 
            // FrmChinhSuaDoiTuongGiaoDich
            // 
            BackColor = Color.FromArgb(66, 94, 106);
            ClientSize = new Size(467, 203);
            Controls.Add(btnThoat);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(txtGhiChu);
            Controls.Add(txtTen);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmChinhSuaDoiTuongGiaoDich";
            Text = "Chỉnh Sửa Đối Tượng Giao Dịch";
            Load += FrmChinhSuaDoiTuongGiaoDich_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Label label1;
        private Label label2;
        private Button btnThoat;
    }
}