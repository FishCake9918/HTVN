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
        private KryptonLabel labelGhiChu;
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
            txtTen = new KryptonTextBox();
            txtGhiChu = new KryptonTextBox();
            labelTen = new KryptonLabel();
            labelGhiChu = new KryptonLabel();
            btnLuu = new KryptonButton();
            btnHuy = new KryptonButton();
            SuspendLayout();
            // 
            // txtTen
            // 
            txtTen.Location = new Point(150, 20);
            txtTen.Name = "txtTen";
            txtTen.Size = new Size(300, 27);
            txtTen.TabIndex = 4;
            // 
            // txtGhiChu
            // 
            txtGhiChu.Location = new Point(150, 60);
            txtGhiChu.Name = "txtGhiChu";
            txtGhiChu.Size = new Size(300, 27);
            txtGhiChu.TabIndex = 2;
            // 
            // labelTen
            // 
            labelTen.Location = new Point(20, 20);
            labelTen.Name = "labelTen";
            labelTen.Size = new Size(116, 24);
            labelTen.TabIndex = 5;
            labelTen.Values.Text = "Tên Đối Tượng:";
            // 
            // labelGhiChu
            // 
            labelGhiChu.Location = new Point(20, 60);
            labelGhiChu.Name = "labelGhiChu";
            labelGhiChu.Size = new Size(69, 24);
            labelGhiChu.TabIndex = 3;
            labelGhiChu.Values.Text = "Ghi Chú:";
            // 
            // btnLuu
            // 
            btnLuu.Location = new Point(221, 150);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(100, 30);
            btnLuu.TabIndex = 1;
            btnLuu.Values.DropDownArrowColor = Color.Empty;
            btnLuu.Values.Text = "Lưu";
            // 
            // btnHuy
            // 
            btnHuy.Location = new Point(350, 150);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(100, 30);
            btnHuy.TabIndex = 0;
            btnHuy.Values.DropDownArrowColor = Color.Empty;
            btnHuy.Values.Text = "Hủy";
            // 
            // FrmChinhSuaDoiTuongGiaoDich
            // 
            ClientSize = new Size(480, 200);
            Controls.Add(btnHuy);
            Controls.Add(btnLuu);
            Controls.Add(txtGhiChu);
            Controls.Add(labelGhiChu);
            Controls.Add(txtTen);
            Controls.Add(labelTen);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmChinhSuaDoiTuongGiaoDich";
            Text = "Chỉnh Sửa Đối Tượng Giao Dịch";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}