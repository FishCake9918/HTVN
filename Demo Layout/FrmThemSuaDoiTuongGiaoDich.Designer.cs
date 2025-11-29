using System.Drawing; // Cần thiết cho Point, Size, Color, Font
using System.Windows.Forms; // Cần thiết cho các control cơ bản và Form

namespace Demo_Layout
{
    partial class FrmThemSuaDoiTuongGiaoDich
    {
        private System.ComponentModel.IContainer components = null;

        // Khai báo controls (Đã chuyển đổi sang WinForms tiêu chuẩn)
        public TextBox txtTen;
        public TextBox txtGhiChu;
        private Label labelTen;
        private Label labelGhiChu;
        private Button btnLuu;
        private Button btnHuy;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        // PHƯƠNG THỨC KHỞI TẠO CÁC CONTROLS CỦA FORM (Đã chuyển đổi)
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmThemSuaDoiTuongGiaoDich));
            txtTen = new TextBox();
            txtGhiChu = new TextBox();
            labelTen = new Label();
            labelGhiChu = new Label();
            btnLuu = new Button();
            btnHuy = new Button();
            button1 = new Button();
            lblForm = new Label();
            SuspendLayout();
            // 
            // txtTen
            // 
            txtTen.Location = new Point(150, 61);
            txtTen.Name = "txtTen";
            txtTen.Size = new Size(300, 27);
            txtTen.TabIndex = 4;
            // 
            // txtGhiChu
            // 
            txtGhiChu.Location = new Point(150, 101);
            txtGhiChu.Name = "txtGhiChu";
            txtGhiChu.Size = new Size(300, 27);
            txtGhiChu.TabIndex = 2;
            // 
            // labelTen
            // 
            labelTen.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelTen.ForeColor = Color.White;
            labelTen.Location = new Point(20, 61);
            labelTen.Name = "labelTen";
            labelTen.Size = new Size(116, 24);
            labelTen.TabIndex = 5;
            labelTen.Text = "Tên Đối Tượng";
            // 
            // labelGhiChu
            // 
            labelGhiChu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelGhiChu.ForeColor = Color.White;
            labelGhiChu.Location = new Point(20, 101);
            labelGhiChu.Name = "labelGhiChu";
            labelGhiChu.Size = new Size(69, 24);
            labelGhiChu.TabIndex = 3;
            labelGhiChu.Text = "Ghi Chú";
            // 
            // btnLuu
            // 
            btnLuu.BackColor = Color.FromArgb(250, 110, 6);
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLuu.ForeColor = Color.White;
            btnLuu.Location = new Point(221, 150);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(100, 30);
            btnLuu.TabIndex = 1;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = false;
            // 
            // btnHuy
            // 
            btnHuy.BackColor = Color.FromArgb(250, 110, 6);
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnHuy.ForeColor = Color.White;
            btnHuy.Location = new Point(350, 150);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(100, 30);
            btnHuy.TabIndex = 0;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(430, 12);
            button1.Name = "button1";
            button1.Size = new Size(29, 29);
            button1.TabIndex = 14;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // lblForm
            // 
            lblForm.AutoSize = true;
            lblForm.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblForm.ForeColor = Color.White;
            lblForm.Location = new Point(20, 16);
            lblForm.Name = "lblForm";
            lblForm.Size = new Size(42, 20);
            lblForm.TabIndex = 15;
            lblForm.Text = "label";
            // 
            // FrmThemSuaDoiTuongGiaoDich
            // 
            BackColor = Color.FromArgb(66, 94, 106);
            ClientSize = new Size(471, 200);
            Controls.Add(lblForm);
            Controls.Add(button1);
            Controls.Add(btnHuy);
            Controls.Add(btnLuu);
            Controls.Add(txtGhiChu);
            Controls.Add(labelGhiChu);
            Controls.Add(txtTen);
            Controls.Add(labelTen);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmThemSuaDoiTuongGiaoDich";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Chỉnh Sửa Đối Tượng Giao Dịch";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private Button button1;
        private Label lblForm;
    }
}