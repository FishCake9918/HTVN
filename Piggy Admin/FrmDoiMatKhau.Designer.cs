using System.Drawing;
using System.Windows.Forms;

namespace Piggy_Admin
{
    partial class FrmDoiMatKhau
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

        // Control Mới
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnCloseHeader;
        private System.Windows.Forms.Panel pnlLine;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            btnCloseHeader = new Button();
            lblTitle = new Label();
            lblOldPass = new Label();
            txtOldPass = new TextBox();
            lblNewPass = new Label();
            txtNewPass = new TextBox();
            lblConfirmPass = new Label();
            txtConfirmPass = new TextBox();
            btnLuu = new Button();
            btnHuy = new Button();
            pnlLine = new Panel();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(11, 60, 93);
            pnlHeader.Controls.Add(btnCloseHeader);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(2, 2);
            pnlHeader.Margin = new Padding(4);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(528, 70);
            pnlHeader.TabIndex = 0;
            // 
            // btnCloseHeader
            // 
            btnCloseHeader.Dock = DockStyle.Right;
            btnCloseHeader.FlatAppearance.BorderSize = 0;
            btnCloseHeader.FlatStyle = FlatStyle.Flat;
            btnCloseHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCloseHeader.ForeColor = Color.White;
            btnCloseHeader.Location = new Point(453, 0);
            btnCloseHeader.Margin = new Padding(4);
            btnCloseHeader.Name = "btnCloseHeader";
            btnCloseHeader.Size = new Size(75, 70);
            btnCloseHeader.TabIndex = 1;
            btnCloseHeader.Text = "✕";
            btnCloseHeader.UseVisualStyleBackColor = true;
            btnCloseHeader.Click += btnHuy_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(37, 14);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(226, 38);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "ĐỔI MẬT KHẨU";
            // 
            // lblOldPass
            // 
            lblOldPass.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblOldPass.ForeColor = Color.FromArgb(11, 60, 93);
            lblOldPass.Location = new Point(45, 88);
            lblOldPass.Margin = new Padding(4, 0, 4, 0);
            lblOldPass.Name = "lblOldPass";
            lblOldPass.Size = new Size(225, 34);
            lblOldPass.TabIndex = 8;
            lblOldPass.Text = "Mật khẩu cũ:";
            // 
            // txtOldPass
            // 
            txtOldPass.Font = new Font("Segoe UI", 11F);
            txtOldPass.Location = new Point(45, 126);
            txtOldPass.Margin = new Padding(4);
            txtOldPass.Name = "txtOldPass";
            txtOldPass.PasswordChar = '•';
            txtOldPass.Size = new Size(433, 37);
            txtOldPass.TabIndex = 1;
            // 
            // lblNewPass
            // 
            lblNewPass.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNewPass.ForeColor = Color.FromArgb(11, 60, 93);
            lblNewPass.Location = new Point(45, 182);
            lblNewPass.Margin = new Padding(4, 0, 4, 0);
            lblNewPass.Name = "lblNewPass";
            lblNewPass.Size = new Size(225, 34);
            lblNewPass.TabIndex = 7;
            lblNewPass.Text = "Mật khẩu mới:";
            // 
            // txtNewPass
            // 
            txtNewPass.Font = new Font("Segoe UI", 11F);
            txtNewPass.Location = new Point(45, 220);
            txtNewPass.Margin = new Padding(4);
            txtNewPass.Name = "txtNewPass";
            txtNewPass.PasswordChar = '•';
            txtNewPass.Size = new Size(433, 37);
            txtNewPass.TabIndex = 2;
            // 
            // lblConfirmPass
            // 
            lblConfirmPass.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblConfirmPass.ForeColor = Color.FromArgb(11, 60, 93);
            lblConfirmPass.Location = new Point(45, 287);
            lblConfirmPass.Margin = new Padding(4, 0, 4, 0);
            lblConfirmPass.Name = "lblConfirmPass";
            lblConfirmPass.Size = new Size(300, 34);
            lblConfirmPass.TabIndex = 6;
            lblConfirmPass.Text = "Xác nhận mật khẩu:";
            // 
            // txtConfirmPass
            // 
            txtConfirmPass.Font = new Font("Segoe UI", 11F);
            txtConfirmPass.Location = new Point(45, 324);
            txtConfirmPass.Margin = new Padding(4);
            txtConfirmPass.Name = "txtConfirmPass";
            txtConfirmPass.PasswordChar = '•';
            txtConfirmPass.Size = new Size(433, 37);
            txtConfirmPass.TabIndex = 3;
            // 
            // btnLuu
            // 
            btnLuu.BackColor = Color.FromArgb(70, 125, 167);
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLuu.ForeColor = Color.White;
            btnLuu.Location = new Point(273, 405);
            btnLuu.Margin = new Padding(4);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(205, 68);
            btnLuu.TabIndex = 4;
            btnLuu.Text = "LƯU THAY ĐỔI";
            btnLuu.UseVisualStyleBackColor = false;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnHuy
            // 
            btnHuy.BackColor = Color.FromArgb(240, 240, 240);
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnHuy.ForeColor = Color.FromArgb(64, 64, 64);
            btnHuy.Location = new Point(39, 405);
            btnHuy.Margin = new Padding(4);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(125, 68);
            btnHuy.TabIndex = 5;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = false;
            btnHuy.Click += btnHuy_Click;
            // 
            // pnlLine
            // 
            pnlLine.BackColor = Color.LightGray;
            pnlLine.Location = new Point(45, 385);
            pnlLine.Margin = new Padding(4);
            pnlLine.Name = "pnlLine";
            pnlLine.Size = new Size(433, 2);
            pnlLine.TabIndex = 0;
            // 
            // FrmDoiMatKhau
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(532, 519);
            Controls.Add(pnlLine);
            Controls.Add(btnHuy);
            Controls.Add(btnLuu);
            Controls.Add(txtConfirmPass);
            Controls.Add(lblConfirmPass);
            Controls.Add(txtNewPass);
            Controls.Add(lblNewPass);
            Controls.Add(txtOldPass);
            Controls.Add(lblOldPass);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "FrmDoiMatKhau";
            Padding = new Padding(2);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Đổi Mật Khẩu";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}