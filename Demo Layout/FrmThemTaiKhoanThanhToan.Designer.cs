namespace Demo_Layout
{
    partial class FrmThemTaiKhoanThanhToan
    {
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmThemTaiKhoanThanhToan));
            lbTenTaiKhoan = new Label();
            lbSoDu = new Label();
            tbTenTaiKhoan = new TextBox();
            txtSoDu = new TextBox();
            btnTao = new Button();
            btnQuayLai = new Button();
            lbLoaiTaiKhoan = new Label();
            cmbLoaiTaiKhoan = new ComboBox();
            button1 = new Button();
            lblForm = new Label();
            SuspendLayout();
            // 
            // lbTenTaiKhoan
            // 
            lbTenTaiKhoan.AutoSize = true;
            lbTenTaiKhoan.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbTenTaiKhoan.ForeColor = Color.White;
            lbTenTaiKhoan.Location = new Point(29, 69);
            lbTenTaiKhoan.Name = "lbTenTaiKhoan";
            lbTenTaiKhoan.Size = new Size(103, 20);
            lbTenTaiKhoan.TabIndex = 0;
            lbTenTaiKhoan.Text = "Tên tài khoản";
            // 
            // lbSoDu
            // 
            lbSoDu.AutoSize = true;
            lbSoDu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbSoDu.ForeColor = Color.White;
            lbSoDu.Location = new Point(29, 163);
            lbSoDu.Name = "lbSoDu";
            lbSoDu.Size = new Size(49, 20);
            lbSoDu.TabIndex = 1;
            lbSoDu.Text = "Số dư";
            // 
            // tbTenTaiKhoan
            // 
            tbTenTaiKhoan.Font = new Font("Segoe UI", 9F);
            tbTenTaiKhoan.Location = new Point(163, 62);
            tbTenTaiKhoan.Name = "tbTenTaiKhoan";
            tbTenTaiKhoan.Size = new Size(321, 27);
            tbTenTaiKhoan.TabIndex = 2;
            // 
            // txtSoDu
            // 
            txtSoDu.Font = new Font("Segoe UI", 9F);
            txtSoDu.Location = new Point(163, 160);
            txtSoDu.Name = "txtSoDu";
            txtSoDu.Size = new Size(180, 27);
            txtSoDu.TabIndex = 3;
            // 
            // btnTao
            // 
            btnTao.BackColor = Color.FromArgb(250, 110, 6);
            btnTao.FlatAppearance.BorderSize = 0;
            btnTao.FlatStyle = FlatStyle.Flat;
            btnTao.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTao.ForeColor = Color.White;
            btnTao.Location = new Point(269, 207);
            btnTao.Name = "btnTao";
            btnTao.Size = new Size(100, 30);
            btnTao.TabIndex = 5;
            btnTao.Text = "Tạo";
            btnTao.UseVisualStyleBackColor = false;
            // 
            // btnQuayLai
            // 
            btnQuayLai.BackColor = Color.FromArgb(250, 110, 6);
            btnQuayLai.FlatAppearance.BorderSize = 0;
            btnQuayLai.FlatStyle = FlatStyle.Flat;
            btnQuayLai.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnQuayLai.ForeColor = Color.White;
            btnQuayLai.Location = new Point(377, 207);
            btnQuayLai.Name = "btnQuayLai";
            btnQuayLai.Size = new Size(100, 30);
            btnQuayLai.TabIndex = 6;
            btnQuayLai.Text = "Huỷ";
            btnQuayLai.UseVisualStyleBackColor = false;
            // 
            // lbLoaiTaiKhoan
            // 
            lbLoaiTaiKhoan.AutoSize = true;
            lbLoaiTaiKhoan.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbLoaiTaiKhoan.ForeColor = Color.White;
            lbLoaiTaiKhoan.Location = new Point(29, 117);
            lbLoaiTaiKhoan.Name = "lbLoaiTaiKhoan";
            lbLoaiTaiKhoan.Size = new Size(107, 20);
            lbLoaiTaiKhoan.TabIndex = 7;
            lbLoaiTaiKhoan.Text = "Loại tài khoản";
            // 
            // cmbLoaiTaiKhoan
            // 
            cmbLoaiTaiKhoan.Font = new Font("Segoe UI", 9F);
            cmbLoaiTaiKhoan.FormattingEnabled = true;
            cmbLoaiTaiKhoan.Location = new Point(163, 109);
            cmbLoaiTaiKhoan.Name = "cmbLoaiTaiKhoan";
            cmbLoaiTaiKhoan.Size = new Size(321, 28);
            cmbLoaiTaiKhoan.TabIndex = 8;
            // 
            // button1
            // 
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(467, 12);
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
            lblForm.Location = new Point(29, 16);
            lblForm.Name = "lblForm";
            lblForm.Size = new Size(42, 20);
            lblForm.TabIndex = 16;
            lblForm.Text = "label";
            // 
            // FrmThemTaiKhoanThanhToan
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(66, 94, 106);
            ClientSize = new Size(513, 258);
            Controls.Add(lblForm);
            Controls.Add(button1);
            Controls.Add(cmbLoaiTaiKhoan);
            Controls.Add(lbLoaiTaiKhoan);
            Controls.Add(btnQuayLai);
            Controls.Add(btnTao);
            Controls.Add(txtSoDu);
            Controls.Add(tbTenTaiKhoan);
            Controls.Add(lbSoDu);
            Controls.Add(lbTenTaiKhoan);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmThemTaiKhoanThanhToan";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thêm tài khoản";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        // Khai báo biến thành viên
        private Label lbTenTaiKhoan;
        private Label lbSoDu;
        private TextBox tbTenTaiKhoan;
        private TextBox txtSoDu;
        private Button btnTao;
        private Button btnQuayLai;
        private Label lbLoaiTaiKhoan;
        private ComboBox cmbLoaiTaiKhoan;
        private Button button1;
        private Label lblForm;
    }
}