namespace Demo_Layout
{
    partial class FormDongTaiKhoan
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
            this.lblTenTaiKhoan = new Krypton.Toolkit.KryptonLabel();
            this.lblSoDuHienTai = new Krypton.Toolkit.KryptonLabel();
            this.cmbTaiKhoanChuyen = new Krypton.Toolkit.KryptonComboBox();
            this.btnDong = new Krypton.Toolkit.KryptonButton();
            this.btnHuy = new Krypton.Toolkit.KryptonButton();
            Krypton.Toolkit.KryptonLabel label1;
            Krypton.Toolkit.KryptonLabel label2;
            Krypton.Toolkit.KryptonLabel label3;
            ((System.ComponentModel.ISupportInitialize)(this.cmbTaiKhoanChuyen)).BeginInit();
            this.SuspendLayout();

            // 
            // label1
            // 
            label1 = new Krypton.Toolkit.KryptonLabel();
            label1.Location = new System.Drawing.Point(30, 20);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(280, 20);
            label1.Text = "Bạn có chắc chắn muốn đóng tài khoản:";
            // 
            // lblTenTaiKhoan
            // 
            this.lblTenTaiKhoan.Location = new System.Drawing.Point(300, 20);
            this.lblTenTaiKhoan.Name = "lblTenTaiKhoan";
            this.lblTenTaiKhoan.Size = new System.Drawing.Size(10, 20);
            this.lblTenTaiKhoan.Text = "Tên Tài Khoản"; // Placeholder
            this.lblTenTaiKhoan.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            // 
            // label2
            // 
            label2 = new Krypton.Toolkit.KryptonLabel();
            label2.Location = new System.Drawing.Point(30, 60);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(250, 20);
            label2.Text = "Tài khoản này hiện có số dư là:";
            // 
            // lblSoDuHienTai
            // 
            this.lblSoDuHienTai.Location = new System.Drawing.Point(280, 60);
            this.lblSoDuHienTai.Name = "lblSoDuHienTai";
            this.lblSoDuHienTai.Size = new System.Drawing.Size(10, 20);
            this.lblSoDuHienTai.Text = "0 đ"; // Placeholder
            this.lblSoDuHienTai.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSoDuHienTai.StateCommon.ShortText.Color1 = System.Drawing.Color.Red;
            // 
            // label3
            // 
            label3 = new Krypton.Toolkit.KryptonLabel();
            label3.Location = new System.Drawing.Point(30, 100);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(350, 20);
            label3.Text = "Chọn tài khoản khác để chuyển số dư này sang:";
            // 
            // cmbTaiKhoanChuyen
            // 
            this.cmbTaiKhoanChuyen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTaiKhoanChuyen.DropDownWidth = 370;
            this.cmbTaiKhoanChuyen.Location = new System.Drawing.Point(30, 130);
            this.cmbTaiKhoanChuyen.Name = "cmbTaiKhoanChuyen";
            this.cmbTaiKhoanChuyen.Size = new System.Drawing.Size(370, 21);
            this.cmbTaiKhoanChuyen.TabIndex = 0;
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(220, 200);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(90, 30);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Values.Text = "Hủy";
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(315, 200);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(90, 30);
            this.btnDong.TabIndex = 2;
            this.btnDong.Values.Text = "Đóng";
            // 
            // FormDongTaiKhoan
            // 
            this.ClientSize = new System.Drawing.Size(430, 250);
            this.ControlBox = true;
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.cmbTaiKhoanChuyen);
            this.Controls.Add(label3);
            this.Controls.Add(this.lblSoDuHienTai);
            this.Controls.Add(label2);
            this.Controls.Add(this.lblTenTaiKhoan);
            this.Controls.Add(label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "FormDongTaiKhoan";
            this.Text = "Đóng Tài Khoản";
            ((System.ComponentModel.ISupportInitialize)(this.cmbTaiKhoanChuyen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}