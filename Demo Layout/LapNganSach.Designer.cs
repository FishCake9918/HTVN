namespace Demo_Layout
{
    partial class LapNganSach
    {
        private System.ComponentModel.IContainer components = null;

        // Khai báo các Controls công cộng
        public Krypton.Toolkit.KryptonTextBox txtSoTien;
        public Krypton.Toolkit.KryptonDateTimePicker dtpNgayBatDau;
        public Krypton.Toolkit.KryptonDateTimePicker dtpNgayKetThuc;
        public Krypton.Toolkit.KryptonComboBox cmbDanhMucCha;
        public Krypton.Toolkit.KryptonButton btnLuu;
        public Krypton.Toolkit.KryptonButton btnHuy;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            txtSoTien = new Krypton.Toolkit.KryptonTextBox();
            dtpNgayBatDau = new Krypton.Toolkit.KryptonDateTimePicker();
            dtpNgayKetThuc = new Krypton.Toolkit.KryptonDateTimePicker();
            cmbDanhMucCha = new Krypton.Toolkit.KryptonComboBox();
            btnLuu = new Krypton.Toolkit.KryptonButton();
            btnHuy = new Krypton.Toolkit.KryptonButton();
            label1 = new Krypton.Toolkit.KryptonLabel();
            label2 = new Krypton.Toolkit.KryptonLabel();
            label3 = new Krypton.Toolkit.KryptonLabel();
            label4 = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)cmbDanhMucCha).BeginInit();
            SuspendLayout();
            // 
            // txtSoTien
            // 
            txtSoTien.Location = new Point(180, 70);
            txtSoTien.Name = "txtSoTien";
            txtSoTien.Size = new Size(280, 27);
            txtSoTien.TabIndex = 1;
            // 
            // dtpNgayBatDau
            // 
            dtpNgayBatDau.Location = new Point(180, 110);
            dtpNgayBatDau.Name = "dtpNgayBatDau";
            dtpNgayBatDau.Size = new Size(280, 25);
            dtpNgayBatDau.TabIndex = 2;
            // 
            // dtpNgayKetThuc
            // 
            dtpNgayKetThuc.Location = new Point(180, 150);
            dtpNgayKetThuc.Name = "dtpNgayKetThuc";
            dtpNgayKetThuc.Size = new Size(280, 25);
            dtpNgayKetThuc.TabIndex = 3;
            // 
            // cmbDanhMucCha
            // 
            cmbDanhMucCha.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDanhMucCha.DropDownWidth = 280;
            cmbDanhMucCha.Location = new Point(180, 30);
            cmbDanhMucCha.Name = "cmbDanhMucCha";
            cmbDanhMucCha.Size = new Size(280, 26);
            cmbDanhMucCha.TabIndex = 0;
            // 
            // btnLuu
            // 
            btnLuu.Location = new Point(360, 220);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(90, 30);
            btnLuu.TabIndex = 5;
            btnLuu.Values.DropDownArrowColor = Color.Empty;
            btnLuu.Values.Text = "Lưu";
            // 
            // btnHuy
            // 
            btnHuy.Location = new Point(260, 220);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(90, 30);
            btnHuy.TabIndex = 4;
            btnHuy.Values.DropDownArrowColor = Color.Empty;
            btnHuy.Values.Text = "Hủy";
            // 
            // label1
            // 
            label1.Location = new Point(30, 30);
            label1.Name = "label1";
            label1.Size = new Size(125, 24);
            label1.TabIndex = 9;
            label1.Values.Text = "Chọn Danh mục:";
            // 
            // label2
            // 
            label2.Location = new Point(30, 70);
            label2.Name = "label2";
            label2.Size = new Size(138, 24);
            label2.TabIndex = 8;
            label2.Values.Text = "Số tiền Ngân sách:";
            // 
            // label3
            // 
            label3.Location = new Point(30, 110);
            label3.Name = "label3";
            label3.Size = new Size(107, 24);
            label3.TabIndex = 7;
            label3.Values.Text = "Ngày Bắt đầu:";
            // 
            // label4
            // 
            label4.Location = new Point(30, 150);
            label4.Name = "label4";
            label4.Size = new Size(111, 24);
            label4.TabIndex = 6;
            label4.Values.Text = "Ngày Kết thúc:";
            // 
            // LapNganSach
            // 
            ClientSize = new Size(504, 256);
            Controls.Add(btnLuu);
            Controls.Add(btnHuy);
            Controls.Add(dtpNgayKetThuc);
            Controls.Add(label4);
            Controls.Add(dtpNgayBatDau);
            Controls.Add(label3);
            Controls.Add(txtSoTien);
            Controls.Add(label2);
            Controls.Add(cmbDanhMucCha);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Location = new Point(0, 0);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LapNganSach";
            Text = "Thêm/Sửa Ngân sách";
            Load += FrmThemSuaNganSach_Load;
            ((System.ComponentModel.ISupportInitialize)cmbDanhMucCha).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Krypton.Toolkit.KryptonLabel label1;
        private Krypton.Toolkit.KryptonLabel label2;
        private Krypton.Toolkit.KryptonLabel label3;
        private Krypton.Toolkit.KryptonLabel label4;
    }
}