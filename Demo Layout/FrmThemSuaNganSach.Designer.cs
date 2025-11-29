using System.Drawing;
using System.Windows.Forms;

namespace Demo_Layout
{
    partial class FrmThemSuaNganSach
    {
        private System.ComponentModel.IContainer components = null;

        // KHAI BÁO CONTROLS (Chuyển sang WinForms tiêu chuẩn)
        public TextBox txtSoTien;
        public Button btnLuu;
        public Button btnHuy;

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;

        public ComboBox cmbDanhMuc;
        public ComboBox cmbThang;
        public TextBox txtNam;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmThemSuaNganSach));
            txtSoTien = new TextBox();
            btnLuu = new Button();
            btnHuy = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            cmbDanhMuc = new ComboBox();
            cmbThang = new ComboBox();
            txtNam = new TextBox();
            button1 = new Button();
            lblForm = new Label();
            SuspendLayout();
            // 
            // txtSoTien
            // 
            txtSoTien.Location = new Point(185, 92);
            txtSoTien.Name = "txtSoTien";
            txtSoTien.Size = new Size(280, 27);
            txtSoTien.TabIndex = 1;
            // 
            // btnLuu
            // 
            btnLuu.BackColor = Color.FromArgb(250, 110, 6);
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.ForeColor = Color.White;
            btnLuu.Location = new Point(261, 224);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(90, 30);
            btnLuu.TabIndex = 5;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = false;
            // 
            // btnHuy
            // 
            btnHuy.BackColor = Color.FromArgb(250, 110, 6);
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.ForeColor = Color.White;
            btnHuy.Location = new Point(375, 224);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(90, 30);
            btnHuy.TabIndex = 4;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(35, 52);
            label1.Name = "label1";
            label1.Size = new Size(125, 24);
            label1.TabIndex = 9;
            label1.Text = "Chọn Danh mục";
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(35, 92);
            label2.Name = "label2";
            label2.Size = new Size(138, 24);
            label2.TabIndex = 8;
            label2.Text = "Số tiền Ngân sách";
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.ForeColor = Color.White;
            label3.Location = new Point(35, 132);
            label3.Name = "label3";
            label3.Size = new Size(61, 24);
            label3.TabIndex = 7;
            label3.Text = "Tháng";
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.ForeColor = Color.White;
            label4.Location = new Point(35, 172);
            label4.Name = "label4";
            label4.Size = new Size(54, 24);
            label4.TabIndex = 6;
            label4.Text = "Năm";
            // 
            // cmbDanhMuc
            // 
            cmbDanhMuc.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDanhMuc.Location = new Point(185, 52);
            cmbDanhMuc.Name = "cmbDanhMuc";
            cmbDanhMuc.Size = new Size(280, 28);
            cmbDanhMuc.TabIndex = 10;
            // 
            // cmbThang
            // 
            cmbThang.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbThang.Location = new Point(185, 132);
            cmbThang.Name = "cmbThang";
            cmbThang.Size = new Size(130, 28);
            cmbThang.TabIndex = 2;
            // 
            // txtNam
            // 
            txtNam.Location = new Point(185, 172);
            txtNam.Name = "txtNam";
            txtNam.Size = new Size(130, 27);
            txtNam.TabIndex = 3;
            // 
            // button1
            // 
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(449, 12);
            button1.Name = "button1";
            button1.Size = new Size(29, 29);
            button1.TabIndex = 15;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // lblForm
            // 
            lblForm.AutoSize = true;
            lblForm.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblForm.ForeColor = Color.White;
            lblForm.Location = new Point(39, 16);
            lblForm.Name = "lblForm";
            lblForm.Size = new Size(42, 20);
            lblForm.TabIndex = 16;
            lblForm.Text = "label";
            // 
            // FrmThemSuaNganSach
            // 
            BackColor = Color.FromArgb(66, 94, 106);
            ClientSize = new Size(490, 268);
            Controls.Add(lblForm);
            Controls.Add(button1);
            Controls.Add(txtNam);
            Controls.Add(cmbThang);
            Controls.Add(cmbDanhMuc);
            Controls.Add(btnLuu);
            Controls.Add(btnHuy);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(txtSoTien);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmThemSuaNganSach";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thêm/Sửa Ngân sách";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label lblForm;
    }
}