namespace Demo_Layout
{
    partial class FrmDongTaiKhoanThanhToan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        public Krypton.Toolkit.KryptonLabel lblTenTaiKhoan;
        public Krypton.Toolkit.KryptonLabel lblSoDuHienTai;
        public Krypton.Toolkit.KryptonComboBox cmbTaiKhoanChuyen;
        public Krypton.Toolkit.KryptonButton btnDong;
        public Krypton.Toolkit.KryptonButton btnHuy;

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
            lblTenTaiKhoan = new Krypton.Toolkit.KryptonLabel();
            lblSoDuHienTai = new Krypton.Toolkit.KryptonLabel();
            cmbTaiKhoanChuyen = new Krypton.Toolkit.KryptonComboBox();
            btnDong = new Krypton.Toolkit.KryptonButton();
            btnHuy = new Krypton.Toolkit.KryptonButton();
            label1 = new Krypton.Toolkit.KryptonLabel();
            label2 = new Krypton.Toolkit.KryptonLabel();
            label3 = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)cmbTaiKhoanChuyen).BeginInit();
            SuspendLayout();
            // 
            // lblTenTaiKhoan
            // 
            lblTenTaiKhoan.Location = new Point(300, 20);
            lblTenTaiKhoan.Name = "lblTenTaiKhoan";
            lblTenTaiKhoan.Size = new Size(115, 24);
            lblTenTaiKhoan.StateCommon.ShortText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTenTaiKhoan.TabIndex = 6;
            lblTenTaiKhoan.Values.Text = "Tên Tài Khoản";
            // 
            // lblSoDuHienTai
            // 
            lblSoDuHienTai.Location = new Point(280, 60);
            lblSoDuHienTai.Name = "lblSoDuHienTai";
            lblSoDuHienTai.Size = new Size(34, 24);
            lblSoDuHienTai.StateCommon.ShortText.Color1 = Color.Red;
            lblSoDuHienTai.StateCommon.ShortText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSoDuHienTai.TabIndex = 4;
            lblSoDuHienTai.Values.Text = "0 đ";
            // 
            // cmbTaiKhoanChuyen
            // 
            cmbTaiKhoanChuyen.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTaiKhoanChuyen.DropDownWidth = 370;
            cmbTaiKhoanChuyen.Location = new Point(30, 130);
            cmbTaiKhoanChuyen.Name = "cmbTaiKhoanChuyen";
            cmbTaiKhoanChuyen.Size = new Size(370, 26);
            cmbTaiKhoanChuyen.TabIndex = 0;
            // 
            // btnDong
            // 
            btnDong.Location = new Point(315, 200);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(90, 30);
            btnDong.TabIndex = 2;
            btnDong.Values.DropDownArrowColor = Color.Empty;
            btnDong.Values.Text = "Xác nhận";
            // 
            // btnHuy
            // 
            btnHuy.Location = new Point(207, 200);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(90, 30);
            btnHuy.TabIndex = 1;
            btnHuy.Values.DropDownArrowColor = Color.Empty;
            btnHuy.Values.Text = "Hủy";
            // 
            // label1
            // 
            label1.Location = new Point(30, 20);
            label1.Name = "label1";
            label1.Size = new Size(284, 24);
            label1.TabIndex = 7;
            label1.Values.Text = "Bạn có chắc chắn muốn đóng tài khoản:";
            // 
            // label2
            // 
            label2.Location = new Point(30, 60);
            label2.Name = "label2";
            label2.Size = new Size(221, 24);
            label2.TabIndex = 5;
            label2.Values.Text = "Tài khoản này hiện có số dư là:";
            // 
            // label3
            // 
            label3.Location = new Point(30, 100);
            label3.Name = "label3";
            label3.Size = new Size(336, 24);
            label3.TabIndex = 3;
            label3.Values.Text = "Chọn tài khoản khác để chuyển số dư này sang:";
            // 
            // FormDongTaiKhoan
            // 
            ClientSize = new Size(434, 236);
            Controls.Add(btnDong);
            Controls.Add(btnHuy);
            Controls.Add(cmbTaiKhoanChuyen);
            Controls.Add(label3);
            Controls.Add(lblSoDuHienTai);
            Controls.Add(label2);
            Controls.Add(lblTenTaiKhoan);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 9F);
            Location = new Point(0, 0);
            Name = "FormDongTaiKhoan";
            Text = "Đóng Tài Khoản";
            ((System.ComponentModel.ISupportInitialize)cmbTaiKhoanChuyen).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonLabel label1;
        private Krypton.Toolkit.KryptonLabel label2;
        private Krypton.Toolkit.KryptonLabel label3;
    }
}