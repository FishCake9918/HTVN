using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Piggy_Admin
{
    partial class TaoCapNhatThongBao
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
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTieuDe.Location = new Point(25, 25);
            lblTieuDe.Margin = new Padding(4, 0, 4, 0);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(347, 38);
            lblTieuDe.TabIndex = 10;
            lblTieuDe.Text = "THÔNG TIN THÔNG BÁO";
            // 
            // lblTieuDeTB
            // 
            lblTieuDeTB.AutoSize = true;
            lblTieuDeTB.Location = new Point(25, 150);
            lblTieuDeTB.Margin = new Padding(4, 0, 4, 0);
            lblTieuDeTB.Name = "lblTieuDeTB";
            lblTieuDeTB.Size = new Size(73, 25);
            lblTieuDeTB.TabIndex = 9;
            lblTieuDeTB.Text = "Tiêu đề:";
            // 
            // txtTieuDe
            // 
            txtTieuDe.Location = new Point(175, 146);
            txtTieuDe.Margin = new Padding(4);
            txtTieuDe.Name = "txtTieuDe";
            txtTieuDe.Size = new Size(436, 31);
            txtTieuDe.TabIndex = 8;
            // 
            // lblNoiDungTB
            // 
            lblNoiDungTB.AutoSize = true;
            lblNoiDungTB.Location = new Point(25, 200);
            lblNoiDungTB.Margin = new Padding(4, 0, 4, 0);
            lblNoiDungTB.Name = "lblNoiDungTB";
            lblNoiDungTB.Size = new Size(91, 25);
            lblNoiDungTB.TabIndex = 7;
            lblNoiDungTB.Text = "Nội dung:";
            // 
            // txtNoiDung
            // 
            txtNoiDung.Location = new Point(175, 196);
            txtNoiDung.Margin = new Padding(4);
            txtNoiDung.Multiline = true;
            txtNoiDung.Name = "txtNoiDung";
            txtNoiDung.Size = new Size(436, 249);
            txtNoiDung.TabIndex = 6;
            // 
            // btnLuu
            // 
            btnLuu.BackColor = Color.ForestGreen;
            btnLuu.ForeColor = Color.White;
            btnLuu.Location = new Point(350, 538);
            btnLuu.Margin = new Padding(4);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(125, 50);
            btnLuu.TabIndex = 5;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = false;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnHuy
            // 
            btnHuy.Location = new Point(488, 538);
            btnHuy.Margin = new Padding(4);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(125, 50);
            btnHuy.TabIndex = 4;
            btnHuy.Text = "Hủy";
            btnHuy.Click += btnHuy_Click;
            // 
            // lblMaTB
            // 
            lblMaTB.AutoSize = true;
            lblMaTB.Location = new Point(25, 100);
            lblMaTB.Margin = new Padding(4, 0, 4, 0);
            lblMaTB.Name = "lblMaTB";
            lblMaTB.Size = new Size(65, 25);
            lblMaTB.TabIndex = 3;
            lblMaTB.Text = "Mã TB:";
            // 
            // lblMaThongBaoValue
            // 
            lblMaThongBaoValue.AutoSize = true;
            lblMaThongBaoValue.Location = new Point(175, 100);
            lblMaThongBaoValue.Margin = new Padding(4, 0, 4, 0);
            lblMaThongBaoValue.Name = "lblMaThongBaoValue";
            lblMaThongBaoValue.Size = new Size(87, 25);
            lblMaThongBaoValue.TabIndex = 2;
            lblMaThongBaoValue.Text = "(Tạo mới)";
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Location = new Point(25, 475);
            lblRole.Margin = new Padding(4, 0, 4, 0);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(97, 25);
            lblRole.TabIndex = 1;
            lblRole.Text = "Người tạo:";
            // 
            // txtRole
            // 
            txtRole.Location = new Point(175, 471);
            txtRole.Margin = new Padding(4);
            txtRole.Name = "txtRole";
            txtRole.Size = new Size(436, 31);
            txtRole.TabIndex = 0;
            txtRole.Text = "Admin";
            // 
            // TaoCapNhatThongBao
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(650, 612);
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
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TaoCapNhatThongBao";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Quản lý thông báo";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}