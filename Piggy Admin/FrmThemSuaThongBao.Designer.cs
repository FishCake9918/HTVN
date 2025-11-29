using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Piggy_Admin
{
    partial class FrmThemSuaThongBao
    {
        // ----------------------------------------------------
        // KHAI BÁO BIẾN CONTROLS (ĐÃ SỬA LỖI)
        // ----------------------------------------------------
        private System.ComponentModel.IContainer components = null;
        private Label lblTieuDe;
        private Label lblTieuDeTB;
        private TextBox txtTieuDe;
        private Label lblNoiDungTB;
        private TextBox txtNoiDung;
        private Button btnLuu;
        private Button btnHuy;
        private Label lblMaTB;
        private Label lblMaThongBaoValue;
        private Label lblRole;
        private TextBox txtRole;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmThemSuaThongBao));
            lblTieuDe = new Label();
            lblTieuDeTB = new Label();
            txtTieuDe = new TextBox();
            lblNoiDungTB = new Label();
            txtNoiDung = new TextBox();
            btnLuu = new Button();
            btnHuy = new Button();
            lblMaTB = new Label();
            lblMaThongBaoValue = new Label();
            lblRole = new Label();
            txtRole = new TextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTieuDe.ForeColor = SystemColors.ControlDarkDark;
            lblTieuDe.Location = new Point(20, 39);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(298, 32);
            lblTieuDe.TabIndex = 10;
            lblTieuDe.Text = "THÔNG TIN THÔNG BÁO";
            // 
            // lblTieuDeTB
            // 
            lblTieuDeTB.AutoSize = true;
            lblTieuDeTB.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTieuDeTB.ForeColor = SystemColors.ControlDarkDark;
            lblTieuDeTB.Location = new Point(20, 120);
            lblTieuDeTB.Name = "lblTieuDeTB";
            lblTieuDeTB.Size = new Size(64, 20);
            lblTieuDeTB.TabIndex = 9;
            lblTieuDeTB.Text = "Tiêu đề:";
            // 
            // txtTieuDe
            // 
            txtTieuDe.Location = new Point(140, 117);
            txtTieuDe.Name = "txtTieuDe";
            txtTieuDe.Size = new Size(350, 27);
            txtTieuDe.TabIndex = 8;
            // 
            // lblNoiDungTB
            // 
            lblNoiDungTB.AutoSize = true;
            lblNoiDungTB.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNoiDungTB.ForeColor = SystemColors.ControlDarkDark;
            lblNoiDungTB.Location = new Point(20, 160);
            lblNoiDungTB.Name = "lblNoiDungTB";
            lblNoiDungTB.Size = new Size(78, 20);
            lblNoiDungTB.TabIndex = 7;
            lblNoiDungTB.Text = "Nội dung:";
            // 
            // txtNoiDung
            // 
            txtNoiDung.Location = new Point(140, 157);
            txtNoiDung.Multiline = true;
            txtNoiDung.Name = "txtNoiDung";
            txtNoiDung.Size = new Size(350, 200);
            txtNoiDung.TabIndex = 6;
            // 
            // btnLuu
            // 
            btnLuu.BackColor = Color.ForestGreen;
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLuu.ForeColor = Color.White;
            btnLuu.Location = new Point(258, 430);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(100, 40);
            btnLuu.TabIndex = 5;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = false;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnHuy
            // 
            btnHuy.BackColor = Color.FromArgb(250, 110, 6);
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnHuy.ForeColor = Color.White;
            btnHuy.Location = new Point(390, 430);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(100, 40);
            btnHuy.TabIndex = 4;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = false;
            btnHuy.Click += btnHuy_Click;
            // 
            // lblMaTB
            // 
            lblMaTB.AutoSize = true;
            lblMaTB.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMaTB.ForeColor = SystemColors.ControlDarkDark;
            lblMaTB.Location = new Point(20, 80);
            lblMaTB.Name = "lblMaTB";
            lblMaTB.Size = new Size(58, 20);
            lblMaTB.TabIndex = 3;
            lblMaTB.Text = "Mã TB:";
            // 
            // lblMaThongBaoValue
            // 
            lblMaThongBaoValue.AutoSize = true;
            lblMaThongBaoValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMaThongBaoValue.Location = new Point(140, 80);
            lblMaThongBaoValue.Name = "lblMaThongBaoValue";
            lblMaThongBaoValue.Size = new Size(78, 20);
            lblMaThongBaoValue.TabIndex = 2;
            lblMaThongBaoValue.Text = "(Tạo mới)";
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRole.ForeColor = SystemColors.ControlDarkDark;
            lblRole.Location = new Point(20, 380);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(84, 20);
            lblRole.TabIndex = 1;
            lblRole.Text = "Người tạo:";
            // 
            // txtRole
            // 
            txtRole.Location = new Point(140, 377);
            txtRole.Name = "txtRole";
            txtRole.Size = new Size(350, 27);
            txtRole.TabIndex = 0;
            txtRole.Text = "Admin";
            // 
            // button1
            // 
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(479, 12);
            button1.Name = "button1";
            button1.Size = new Size(29, 29);
            button1.TabIndex = 11;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FrmThemSuaThongBao
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(213, 217, 247);
            ClientSize = new Size(520, 490);
            Controls.Add(button1);
            Controls.Add(txtRole);
            Controls.Add(lblRole);
            Controls.Add(lblMaThongBaoValue);
            Controls.Add(lblMaTB);
            Controls.Add(btnHuy);
            Controls.Add(btnLuu);
            Controls.Add(txtNoiDung);
            Controls.Add(lblNoiDungTB);
            Controls.Add(txtTieuDe);
            Controls.Add(lblTieuDeTB);
            Controls.Add(lblTieuDe);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmThemSuaThongBao";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Quản lý thông báo";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
    }
}