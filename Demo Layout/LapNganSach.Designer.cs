using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace Demo_Layout
{
    partial class LapNganSach
    {
        private System.ComponentModel.IContainer components = null;

        // KHAI BÁO CONTROLS (Chỉ một lần ở đây)
        public KryptonTextBox txtSoTien;
        public KryptonButton btnLuu;
        public KryptonButton btnHuy;

        private KryptonLabel label1;
        private KryptonLabel label2;
        private KryptonLabel label3;
        private KryptonLabel label4;

        public KryptonComboBox cmbDanhMuc;
        public KryptonComboBox cmbThang;
        public KryptonTextBox txtNam;

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
            txtSoTien = new KryptonTextBox();
            btnLuu = new KryptonButton();
            btnHuy = new KryptonButton();
            label1 = new KryptonLabel();
            label2 = new KryptonLabel();
            label3 = new KryptonLabel();
            label4 = new KryptonLabel();
            cmbDanhMuc = new KryptonComboBox();
            cmbThang = new KryptonComboBox();
            txtNam = new KryptonTextBox();

            this.SuspendLayout();

            // 
            // txtSoTien
            // 
            txtSoTien.Location = new Point(180, 70);
            txtSoTien.Name = "txtSoTien";
            txtSoTien.Size = new Size(280, 27);
            txtSoTien.TabIndex = 1;

            // 
            // cmbThang
            // 
            cmbThang.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbThang.Location = new Point(180, 110);
            cmbThang.Name = "cmbThang";
            cmbThang.Size = new Size(130, 25);
            cmbThang.TabIndex = 2;

            // 
            // txtNam
            // 
            txtNam.Location = new Point(180, 150);
            txtNam.Name = "txtNam";
            txtNam.Size = new Size(130, 27);
            txtNam.TabIndex = 3;

            // 
            // btnLuu
            // 
            btnLuu.Location = new Point(360, 220);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(90, 30);
            btnLuu.TabIndex = 5;
            btnLuu.Values.Text = "Lưu";

            // 
            // btnHuy
            // 
            btnHuy.Location = new Point(260, 220);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(90, 30);
            btnHuy.TabIndex = 4;
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
            label3.Size = new Size(61, 24);
            label3.TabIndex = 7;
            label3.Values.Text = "Tháng:";

            // 
            // label4
            // 
            label4.Location = new Point(30, 150);
            label4.Name = "label4";
            label4.Size = new Size(54, 24);
            label4.TabIndex = 6;
            label4.Values.Text = "Năm:";

            // 
            // cmbDanhMuc 
            // 
            cmbDanhMuc.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDanhMuc.Location = new Point(180, 30);
            cmbDanhMuc.Name = "cmbDanhMuc";
            cmbDanhMuc.Size = new Size(280, 25);
            cmbDanhMuc.TabIndex = 10;

            // 
            // LapNganSach
            // 
            this.ClientSize = new Size(508, 283);
            this.Controls.Add(txtNam);
            this.Controls.Add(cmbThang);
            this.Controls.Add(cmbDanhMuc);
            this.Controls.Add(btnLuu);
            this.Controls.Add(btnHuy);
            this.Controls.Add(label4);
            this.Controls.Add(label3);
            this.Controls.Add(txtSoTien);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LapNganSach";
            this.Text = "Thêm/Sửa Ngân sách";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}