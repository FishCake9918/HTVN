namespace Demo_Layout.DanhMucChiTieu
{
    partial class frmDanhMucChiTieu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            trvDanhSachNhom = new TreeView();
            pnlThemNhom = new Panel();
            btnHuy = new Button();
            lblTitleThem = new Label();
            btnLuu = new Button();
            cboNhomCha = new ComboBox();
            label3 = new Label();
            cboKhoan = new ComboBox();
            label2 = new Label();
            txtTenNhom = new TextBox();
            label1 = new Label();
            panel1 = new Panel();
            button1 = new Button();
            lblChiTietNhom = new Label();
            button2 = new Button();
            comboBox1 = new ComboBox();
            label5 = new Label();
            comboBox2 = new ComboBox();
            label6 = new Label();
            textBox1 = new TextBox();
            label7 = new Label();
            pnlThemNhom.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // trvDanhSachNhom
            // 
            trvDanhSachNhom.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            trvDanhSachNhom.Location = new Point(12, 12);
            trvDanhSachNhom.Name = "trvDanhSachNhom";
            trvDanhSachNhom.Size = new Size(532, 466);
            trvDanhSachNhom.TabIndex = 0;
            // 
            // pnlThemNhom
            // 
            pnlThemNhom.BackColor = Color.Gainsboro;
            pnlThemNhom.Controls.Add(btnHuy);
            pnlThemNhom.Controls.Add(lblTitleThem);
            pnlThemNhom.Controls.Add(btnLuu);
            pnlThemNhom.Controls.Add(cboNhomCha);
            pnlThemNhom.Controls.Add(label3);
            pnlThemNhom.Controls.Add(cboKhoan);
            pnlThemNhom.Controls.Add(label2);
            pnlThemNhom.Controls.Add(txtTenNhom);
            pnlThemNhom.Controls.Add(label1);
            pnlThemNhom.Location = new Point(633, 12);
            pnlThemNhom.Name = "pnlThemNhom";
            pnlThemNhom.Size = new Size(513, 285);
            pnlThemNhom.TabIndex = 1;
            // 
            // btnHuy
            // 
            btnHuy.Location = new Point(295, 237);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(94, 29);
            btnHuy.TabIndex = 7;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = true;
            // 
            // lblTitleThem
            // 
            lblTitleThem.Anchor = AnchorStyles.Top;
            lblTitleThem.BackColor = Color.Gray;
            lblTitleThem.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitleThem.ForeColor = SystemColors.ButtonHighlight;
            lblTitleThem.Location = new Point(0, 0);
            lblTitleThem.Name = "lblTitleThem";
            lblTitleThem.Size = new Size(513, 41);
            lblTitleThem.TabIndex = 2;
            lblTitleThem.Text = "Thêm nhóm mới";
            lblTitleThem.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnLuu
            // 
            btnLuu.Location = new Point(180, 237);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(94, 29);
            btnLuu.TabIndex = 6;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = true;
            // 
            // cboNhomCha
            // 
            cboNhomCha.Location = new Point(180, 177);
            cboNhomCha.Name = "cboNhomCha";
            cboNhomCha.Size = new Size(261, 28);
            cboNhomCha.TabIndex = 5;
            cboNhomCha.Text = "Chọn nhóm";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(97, 180);
            label3.Name = "label3";
            label3.Size = new Size(77, 20);
            label3.TabIndex = 4;
            label3.Text = "Nhóm cha";
            // 
            // cboKhoan
            // 
            cboKhoan.FormattingEnabled = true;
            cboKhoan.Location = new Point(180, 125);
            cboKhoan.Name = "cboKhoan";
            cboKhoan.Size = new Size(173, 28);
            cboKhoan.TabIndex = 3;
            cboKhoan.Text = "Thu/Chi";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(123, 128);
            label2.Name = "label2";
            label2.Size = new Size(51, 20);
            label2.TabIndex = 2;
            label2.Text = "Khoản";
            // 
            // txtTenNhom
            // 
            txtTenNhom.Location = new Point(180, 74);
            txtTenNhom.Name = "txtTenNhom";
            txtTenNhom.Size = new Size(261, 27);
            txtTenNhom.TabIndex = 1;
            txtTenNhom.Text = "Nhập tên nhóm";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(100, 77);
            label1.Name = "label1";
            label1.Size = new Size(74, 20);
            label1.TabIndex = 0;
            label1.Text = "Tên nhóm";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Gainsboro;
            panel1.Controls.Add(button1);
            panel1.Controls.Add(lblChiTietNhom);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(comboBox2);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label7);
            panel1.Location = new Point(633, 303);
            panel1.Name = "panel1";
            panel1.Size = new Size(513, 433);
            panel1.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(295, 237);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 7;
            button1.Text = "Xóa";
            button1.UseVisualStyleBackColor = true;
            // 
            // lblChiTietNhom
            // 
            lblChiTietNhom.Anchor = AnchorStyles.Top;
            lblChiTietNhom.BackColor = Color.Gray;
            lblChiTietNhom.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblChiTietNhom.ForeColor = SystemColors.ButtonHighlight;
            lblChiTietNhom.Location = new Point(0, -2);
            lblChiTietNhom.Name = "lblChiTietNhom";
            lblChiTietNhom.Size = new Size(513, 41);
            lblChiTietNhom.TabIndex = 2;
            lblChiTietNhom.Text = "Chi tiết nhóm";
            lblChiTietNhom.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button2
            // 
            button2.Location = new Point(180, 237);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 6;
            button2.Text = "Chỉnh sửa";
            button2.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            comboBox1.Location = new Point(180, 177);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(261, 28);
            comboBox1.TabIndex = 5;
            comboBox1.Text = "Chọn nhóm";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(97, 180);
            label5.Name = "label5";
            label5.Size = new Size(77, 20);
            label5.TabIndex = 4;
            label5.Text = "Nhóm cha";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(180, 125);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(173, 28);
            comboBox2.TabIndex = 3;
            comboBox2.Text = "Thu/Chi";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(123, 128);
            label6.Name = "label6";
            label6.Size = new Size(51, 20);
            label6.TabIndex = 2;
            label6.Text = "Khoản";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(180, 74);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(261, 27);
            textBox1.TabIndex = 1;
            textBox1.Text = "Nhập tên nhóm";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(100, 77);
            label7.Name = "label7";
            label7.Size = new Size(74, 20);
            label7.TabIndex = 0;
            label7.Text = "Tên nhóm";
            // 
            // frmDanhMucChiTieu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1225, 783);
            Controls.Add(panel1);
            Controls.Add(pnlThemNhom);
            Controls.Add(trvDanhSachNhom);
            Name = "frmDanhMucChiTieu";
            Text = "Danh mục chi tiêu";
            Load += frmDanhMucChiTieu_Load;
            pnlThemNhom.ResumeLayout(false);
            pnlThemNhom.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TreeView trvDanhSachNhom;
        private Panel pnlThemNhom;
        private ComboBox cboNhomCha;
        private Label label3;
        private ComboBox cboKhoan;
        private Label label2;
        private TextBox txtTenNhom;
        private Label label1;
        private Label lblTitleThem;
        private Button btnLuu;
        private Button btnHuy;
        private Panel panel1;
        private Button button1;
        private Label lblChiTietNhom;
        private Button button2;
        private ComboBox comboBox1;
        private Label label5;
        private ComboBox comboBox2;
        private Label label6;
        private TextBox textBox1;
        private Label label7;
    }
}

