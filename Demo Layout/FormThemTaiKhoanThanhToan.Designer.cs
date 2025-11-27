namespace Demo_Layout
{
    partial class FormThemTaiKhoanThanhToan
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
            lbTenTaiKhoan = new Label();
            lbSoDu = new Label();
            tbTenTaiKhoan = new TextBox();
            txtSoDu = new TextBox();
            btnTao = new Button();
            btnQuayLai = new Button();
            lbLoaiTaiKhoan = new Label();
            cmbLoaiTaiKhoan = new ComboBox();
            SuspendLayout();
            // 
            // lbTenTaiKhoan
            // 
            lbTenTaiKhoan.AutoSize = true;
            lbTenTaiKhoan.Font = new Font("Segoe UI", 12F);
            lbTenTaiKhoan.Location = new Point(7, 37);
            lbTenTaiKhoan.Name = "lbTenTaiKhoan";
            lbTenTaiKhoan.Size = new Size(127, 28);
            lbTenTaiKhoan.TabIndex = 0;
            lbTenTaiKhoan.Text = "Tên tài khoản";
            // 
            // lbSoDu
            // 
            lbSoDu.AutoSize = true;
            lbSoDu.Font = new Font("Segoe UI", 12F);
            lbSoDu.Location = new Point(12, 141);
            lbSoDu.Name = "lbSoDu";
            lbSoDu.Size = new Size(64, 28);
            lbSoDu.TabIndex = 1;
            lbSoDu.Text = "Số dư";
            // 
            // tbTenTaiKhoan
            // 
            tbTenTaiKhoan.Font = new Font("Segoe UI", 12F);
            tbTenTaiKhoan.Location = new Point(163, 37);
            tbTenTaiKhoan.Name = "tbTenTaiKhoan";
            tbTenTaiKhoan.Size = new Size(321, 34);
            tbTenTaiKhoan.TabIndex = 2;
            // 
            // txtSoDu
            // 
            txtSoDu.Font = new Font("Segoe UI", 12F);
            txtSoDu.Location = new Point(163, 135);
            txtSoDu.Name = "txtSoDu";
            txtSoDu.Size = new Size(180, 34);
            txtSoDu.TabIndex = 3;
            // 
            // btnTao
            // 
            btnTao.Location = new Point(390, 195);
            btnTao.Name = "btnTao";
            btnTao.Size = new Size(94, 40);
            btnTao.TabIndex = 5;
            btnTao.Text = "Tạo";
            btnTao.UseVisualStyleBackColor = true;
            // 
            // btnQuayLai
            // 
            btnQuayLai.Location = new Point(254, 195);
            btnQuayLai.Name = "btnQuayLai";
            btnQuayLai.Size = new Size(107, 40);
            btnQuayLai.TabIndex = 6;
            btnQuayLai.Text = "Quay trở lại";
            btnQuayLai.UseVisualStyleBackColor = true;
            // 
            // lbLoaiTaiKhoan
            // 
            lbLoaiTaiKhoan.AutoSize = true;
            lbLoaiTaiKhoan.Font = new Font("Segoe UI", 12F);
            lbLoaiTaiKhoan.Location = new Point(12, 92);
            lbLoaiTaiKhoan.Name = "lbLoaiTaiKhoan";
            lbLoaiTaiKhoan.Size = new Size(134, 28);
            lbLoaiTaiKhoan.TabIndex = 7;
            lbLoaiTaiKhoan.Text = "Loại tài khoản";
            // 
            // cmbLoaiTaiKhoan
            // 
            cmbLoaiTaiKhoan.Font = new Font("Segoe UI", 12F);
            cmbLoaiTaiKhoan.FormattingEnabled = true;
            cmbLoaiTaiKhoan.Location = new Point(163, 84);
            cmbLoaiTaiKhoan.Name = "cmbLoaiTaiKhoan";
            cmbLoaiTaiKhoan.Size = new Size(321, 36);
            cmbLoaiTaiKhoan.TabIndex = 8;
            // 
            // FormThemTaiKhoanThanhToan
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Honeydew;
            ClientSize = new Size(527, 258);
            Controls.Add(cmbLoaiTaiKhoan);
            Controls.Add(lbLoaiTaiKhoan);
            Controls.Add(btnQuayLai);
            Controls.Add(btnTao);
            Controls.Add(txtSoDu);
            Controls.Add(tbTenTaiKhoan);
            Controls.Add(lbSoDu);
            Controls.Add(lbTenTaiKhoan);
            Name = "FormThemTaiKhoanThanhToan";
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
    }
}