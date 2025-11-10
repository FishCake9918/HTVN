namespace Demo_Layout
{
    partial class FormThemTaiKhoan
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
            lbTenTaiKhoan = new Label();
            lbSoDu = new Label();
            tbTenTaiKhoan = new TextBox();
            txSoDu = new TextBox();
            btTaoTaiKhoan = new Button();
            btQuayLai = new Button();
            lbLoaiTaiKhoan = new Label();
            cbLoaiTaiKhoan = new ComboBox();
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
            // txSoDu
            // 
            txSoDu.Font = new Font("Segoe UI", 12F);
            txSoDu.Location = new Point(163, 135);
            txSoDu.Name = "txSoDu";
            txSoDu.Size = new Size(321, 34);
            txSoDu.TabIndex = 3;
            // 
            // btTaoTaiKhoan
            // 
            btTaoTaiKhoan.Location = new Point(367, 186);
            btTaoTaiKhoan.Name = "btTaoTaiKhoan";
            btTaoTaiKhoan.Size = new Size(94, 40);
            btTaoTaiKhoan.TabIndex = 5;
            btTaoTaiKhoan.Text = "Tạo";
            btTaoTaiKhoan.UseVisualStyleBackColor = true;
            // 
            // btQuayLai
            // 
            btQuayLai.Location = new Point(238, 186);
            btQuayLai.Name = "btQuayLai";
            btQuayLai.Size = new Size(107, 40);
            btQuayLai.TabIndex = 6;
            btQuayLai.Text = "Quay trở lại";
            btQuayLai.UseVisualStyleBackColor = true;
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
            // cbLoaiTaiKhoan
            // 
            cbLoaiTaiKhoan.Font = new Font("Segoe UI", 12F);
            cbLoaiTaiKhoan.FormattingEnabled = true;
            cbLoaiTaiKhoan.Location = new Point(163, 84);
            cbLoaiTaiKhoan.Name = "cbLoaiTaiKhoan";
            cbLoaiTaiKhoan.Size = new Size(321, 36);
            cbLoaiTaiKhoan.TabIndex = 8;
            // 
            // FormThemTaiKhoan
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Honeydew;
            ClientSize = new Size(515, 258);
            Controls.Add(cbLoaiTaiKhoan);
            Controls.Add(lbLoaiTaiKhoan);
            Controls.Add(btQuayLai);
            Controls.Add(btTaoTaiKhoan);
            Controls.Add(txSoDu);
            Controls.Add(tbTenTaiKhoan);
            Controls.Add(lbSoDu);
            Controls.Add(lbTenTaiKhoan);
            Name = "FormThemTaiKhoan";
            Text = "Thêm tài khoản";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbTenTaiKhoan;
        private Label lbSoDu;
        private TextBox tbTenTaiKhoan;
        private TextBox txSoDu;
        private Button btTaoTaiKhoan;
        private Button btQuayLai;
        private Label lbLoaiTaiKhoan;
        private ComboBox cbLoaiTaiKhoan;
    }
}