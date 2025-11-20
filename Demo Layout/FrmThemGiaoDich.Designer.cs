namespace Demo_Layout
{
    partial class FrmThemGiaoDich
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmThemGiaoDich));
            txtTenGiaoDich = new TextBox();
            rtbGhiChu = new RichTextBox();
            label1 = new Label();
            label2 = new Label();
            txtSoTien = new TextBox();
            label3 = new Label();
            dtNgayGiaoDich = new DateTimePicker();
            label4 = new Label();
            cbDoiTuong = new ComboBox();
            label5 = new Label();
            cbTaiKhoan = new ComboBox();
            label6 = new Label();
            btnLuu = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // txtTenGiaoDich
            // 
            txtTenGiaoDich.Location = new Point(113, 43);
            txtTenGiaoDich.Name = "txtTenGiaoDich";
            txtTenGiaoDich.Size = new Size(125, 27);
            txtTenGiaoDich.TabIndex = 0;
            // 
            // rtbGhiChu
            // 
            rtbGhiChu.Location = new Point(113, 88);
            rtbGhiChu.Name = "rtbGhiChu";
            rtbGhiChu.Size = new Size(125, 120);
            rtbGhiChu.TabIndex = 1;
            rtbGhiChu.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(9, 46);
            label1.Name = "label1";
            label1.Size = new Size(101, 20);
            label1.TabIndex = 2;
            label1.Text = "Tên giao dịch";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(49, 88);
            label2.Name = "label2";
            label2.Size = new Size(62, 20);
            label2.TabIndex = 3;
            label2.Text = "Ghi chú";
            // 
            // txtSoTien
            // 
            txtSoTien.Location = new Point(113, 226);
            txtSoTien.Name = "txtSoTien";
            txtSoTien.Size = new Size(125, 27);
            txtSoTien.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.ForeColor = Color.White;
            label3.Location = new Point(52, 229);
            label3.Name = "label3";
            label3.Size = new Size(57, 20);
            label3.TabIndex = 5;
            label3.Text = "Số tiền";
            // 
            // dtNgayGiaoDich
            // 
            dtNgayGiaoDich.Location = new Point(400, 136);
            dtNgayGiaoDich.Name = "dtNgayGiaoDich";
            dtNgayGiaoDich.Size = new Size(250, 27);
            dtNgayGiaoDich.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.ForeColor = Color.White;
            label4.Location = new Point(257, 141);
            label4.Name = "label4";
            label4.Size = new Size(141, 20);
            label4.TabIndex = 7;
            label4.Text = "Thời gian giao dịch";
            // 
            // cbDoiTuong
            // 
            cbDoiTuong.FormattingEnabled = true;
            cbDoiTuong.Location = new Point(499, 46);
            cbDoiTuong.Name = "cbDoiTuong";
            cbDoiTuong.Size = new Size(151, 28);
            cbDoiTuong.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.ForeColor = Color.White;
            label5.Location = new Point(350, 50);
            label5.Name = "label5";
            label5.Size = new Size(147, 20);
            label5.TabIndex = 9;
            label5.Text = "Đối tượng giao dịch";
            // 
            // cbTaiKhoan
            // 
            cbTaiKhoan.FormattingEnabled = true;
            cbTaiKhoan.Location = new Point(499, 85);
            cbTaiKhoan.Name = "cbTaiKhoan";
            cbTaiKhoan.Size = new Size(151, 28);
            cbTaiKhoan.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label6.ForeColor = Color.White;
            label6.Location = new Point(422, 91);
            label6.Name = "label6";
            label6.Size = new Size(76, 20);
            label6.TabIndex = 11;
            label6.Text = "Tài khoản";
            // 
            // btnLuu
            // 
            btnLuu.BackColor = Color.FromArgb(250, 110, 6);
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLuu.ForeColor = Color.White;
            btnLuu.Location = new Point(556, 225);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(94, 33);
            btnLuu.TabIndex = 12;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = false;
            btnLuu.Click += btnLuu_Click;
            // 
            // button1
            // 
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(636, 7);
            button1.Name = "button1";
            button1.Size = new Size(29, 29);
            button1.TabIndex = 13;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FrmThemGiaoDich
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(66, 94, 106);
            ClientSize = new Size(675, 270);
            Controls.Add(button1);
            Controls.Add(btnLuu);
            Controls.Add(label6);
            Controls.Add(cbTaiKhoan);
            Controls.Add(label5);
            Controls.Add(cbDoiTuong);
            Controls.Add(label4);
            Controls.Add(dtNgayGiaoDich);
            Controls.Add(label3);
            Controls.Add(txtSoTien);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(rtbGhiChu);
            Controls.Add(txtTenGiaoDich);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmThemGiaoDich";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FrmThemGiaoDich";
            Load += FrmThemGiaoDich_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtTenGiaoDich;
        private RichTextBox rtbGhiChu;
        private Label label1;
        private Label label2;
        private TextBox txtSoTien;
        private Label label3;
        private DateTimePicker dtNgayGiaoDich;
        private Label label4;
        private ComboBox cbDoiTuong;
        private Label label5;
        private ComboBox cbTaiKhoan;
        private Label label6;
        private Button btnLuu;
        private Button button1;
    }
}