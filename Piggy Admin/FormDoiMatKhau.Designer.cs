namespace Piggy_Admin
{
    partial class FormDoiMatKhau
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblOldPass;
        private System.Windows.Forms.TextBox txtOldPass;
        private System.Windows.Forms.Label lblNewPass;
        private System.Windows.Forms.TextBox txtNewPass;
        private System.Windows.Forms.Label lblConfirmPass;
        private System.Windows.Forms.TextBox txtConfirmPass;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Label lblTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblOldPass = new System.Windows.Forms.Label();
            this.txtOldPass = new System.Windows.Forms.TextBox();
            this.lblNewPass = new System.Windows.Forms.Label();
            this.txtNewPass = new System.Windows.Forms.TextBox();
            this.lblConfirmPass = new System.Windows.Forms.Label();
            this.txtConfirmPass = new System.Windows.Forms.TextBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(67)))), ((int)(((byte)(215)))));
            this.lblTitle.Location = new System.Drawing.Point(100, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(195, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ĐỔI MẬT KHẨU";

            // lblOldPass
            this.lblOldPass.AutoSize = true;
            this.lblOldPass.Location = new System.Drawing.Point(30, 70);
            this.lblOldPass.Name = "lblOldPass";
            this.lblOldPass.Size = new System.Drawing.Size(92, 20);
            this.lblOldPass.TabIndex = 1;
            this.lblOldPass.Text = "Mật khẩu cũ:";

            // txtOldPass
            this.txtOldPass.Location = new System.Drawing.Point(30, 95);
            this.txtOldPass.Name = "txtOldPass";
            this.txtOldPass.PasswordChar = '•'; // Che mật khẩu
            this.txtOldPass.Size = new System.Drawing.Size(320, 27);
            this.txtOldPass.TabIndex = 1;

            // lblNewPass
            this.lblNewPass.AutoSize = true;
            this.lblNewPass.Location = new System.Drawing.Point(30, 140);
            this.lblNewPass.Name = "lblNewPass";
            this.lblNewPass.Size = new System.Drawing.Size(103, 20);
            this.lblNewPass.TabIndex = 3;
            this.lblNewPass.Text = "Mật khẩu mới:";

            // txtNewPass
            this.txtNewPass.Location = new System.Drawing.Point(30, 165);
            this.txtNewPass.Name = "txtNewPass";
            this.txtNewPass.PasswordChar = '•';
            this.txtNewPass.Size = new System.Drawing.Size(320, 27);
            this.txtNewPass.TabIndex = 2;

            // lblConfirmPass
            this.lblConfirmPass.AutoSize = true;
            this.lblConfirmPass.Location = new System.Drawing.Point(30, 210);
            this.lblConfirmPass.Name = "lblConfirmPass";
            this.lblConfirmPass.Size = new System.Drawing.Size(163, 20);
            this.lblConfirmPass.TabIndex = 5;
            this.lblConfirmPass.Text = "Xác nhận mật khẩu mới:";

            // txtConfirmPass
            this.txtConfirmPass.Location = new System.Drawing.Point(30, 235);
            this.txtConfirmPass.Name = "txtConfirmPass";
            this.txtConfirmPass.PasswordChar = '•';
            this.txtConfirmPass.Size = new System.Drawing.Size(320, 27);
            this.txtConfirmPass.TabIndex = 3;

            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(105)))), ((int)(((byte)(223)))));
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(200, 290);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(150, 40);
            this.btnLuu.TabIndex = 4;
            this.btnLuu.Text = "Lưu Thay Đổi";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);

            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.LightGray;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnHuy.Location = new System.Drawing.Point(30, 290);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(150, 40);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);

            // FormDoiMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 360);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.txtConfirmPass);
            this.Controls.Add(this.lblConfirmPass);
            this.Controls.Add(this.txtNewPass);
            this.Controls.Add(this.lblNewPass);
            this.Controls.Add(this.txtOldPass);
            this.Controls.Add(this.lblOldPass);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDoiMatKhau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Đổi Mật Khẩu";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}