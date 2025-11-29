namespace Demo_Layout
{
    partial class FrmDongTaiKhoanThanhToan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        // KHAI BÁO CÁC CONTROL CƠ BẢN CỦA WINFORMS
        public Label lblTenTaiKhoan;
        public Label lblSoDuHienTai;
        public ComboBox cmbTaiKhoanChuyen;
        public Button btnDong;
        public Button btnHuy;

        // Các Label phụ trợ
        private Label label1;
        private Label label2;
        private Label label3;


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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDongTaiKhoanThanhToan));
            lblTenTaiKhoan = new Label();
            lblSoDuHienTai = new Label();
            cmbTaiKhoanChuyen = new ComboBox();
            btnDong = new Button();
            btnHuy = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            button1 = new Button();
            lblForm = new Label();
            SuspendLayout();
            // 
            // lblTenTaiKhoan
            // 
            lblTenTaiKhoan.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTenTaiKhoan.ForeColor = Color.White;
            lblTenTaiKhoan.Location = new Point(279, 46);
            lblTenTaiKhoan.Name = "lblTenTaiKhoan";
            lblTenTaiKhoan.Size = new Size(115, 24);
            lblTenTaiKhoan.TabIndex = 6;
            lblTenTaiKhoan.Text = "Tên Tài Khoản";
            // 
            // lblSoDuHienTai
            // 
            lblSoDuHienTai.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSoDuHienTai.ForeColor = Color.Snow;
            lblSoDuHienTai.Location = new Point(256, 86);
            lblSoDuHienTai.Name = "lblSoDuHienTai";
            lblSoDuHienTai.Size = new Size(120, 24);
            lblSoDuHienTai.TabIndex = 4;
            lblSoDuHienTai.Text = "0 đ";
            lblSoDuHienTai.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbTaiKhoanChuyen
            // 
            cmbTaiKhoanChuyen.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTaiKhoanChuyen.Location = new Point(29, 156);
            cmbTaiKhoanChuyen.Name = "cmbTaiKhoanChuyen";
            cmbTaiKhoanChuyen.Size = new Size(370, 28);
            cmbTaiKhoanChuyen.TabIndex = 0;
            // 
            // btnDong
            // 
            btnDong.BackColor = Color.FromArgb(250, 110, 6);
            btnDong.FlatAppearance.BorderSize = 0;
            btnDong.FlatStyle = FlatStyle.Flat;
            btnDong.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDong.ForeColor = Color.White;
            btnDong.Location = new Point(309, 203);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(90, 30);
            btnDong.TabIndex = 2;
            btnDong.Text = "Xác nhận";
            btnDong.UseVisualStyleBackColor = false;
            // 
            // btnHuy
            // 
            btnHuy.BackColor = Color.FromArgb(250, 110, 6);
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnHuy.ForeColor = Color.White;
            btnHuy.Location = new Point(208, 203);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(90, 30);
            btnHuy.TabIndex = 1;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(29, 46);
            label1.Name = "label1";
            label1.Size = new Size(284, 24);
            label1.TabIndex = 7;
            label1.Text = "Bạn có chắc chắn muốn đóng tài khoản:";
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(29, 86);
            label2.Name = "label2";
            label2.Size = new Size(221, 24);
            label2.TabIndex = 5;
            label2.Text = "Tài khoản này hiện có số dư là:";
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.ForeColor = Color.White;
            label3.Location = new Point(29, 126);
            label3.Name = "label3";
            label3.Size = new Size(370, 24);
            label3.TabIndex = 3;
            label3.Text = "Chọn tài khoản khác để chuyển số dư này sang:";
            // 
            // button1
            // 
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(400, 12);
            button1.Name = "button1";
            button1.Size = new Size(29, 29);
            button1.TabIndex = 8;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // lblForm
            // 
            lblForm.AutoSize = true;
            lblForm.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblForm.ForeColor = Color.White;
            lblForm.Location = new Point(29, 12);
            lblForm.Name = "lblForm";
            lblForm.Size = new Size(141, 20);
            lblForm.TabIndex = 9;
            lblForm.Text = "ĐÓNG TÀI KHOẢN";
            // 
            // FrmDongTaiKhoanThanhToan
            // 
            BackColor = Color.FromArgb(66, 94, 106);
            ClientSize = new Size(442, 250);
            Controls.Add(lblForm);
            Controls.Add(button1);
            Controls.Add(btnDong);
            Controls.Add(btnHuy);
            Controls.Add(cmbTaiKhoanChuyen);
            Controls.Add(label3);
            Controls.Add(lblSoDuHienTai);
            Controls.Add(label2);
            Controls.Add(lblTenTaiKhoan);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmDongTaiKhoanThanhToan";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đóng Tài Khoản";
            ResumeLayout(false);
            PerformLayout();

        }
        private Button button1;
        private Label lblForm;

        #endregion

        // Khai báo lại các control phụ trợ
        // Đã được khai báo phía trên
    }
}