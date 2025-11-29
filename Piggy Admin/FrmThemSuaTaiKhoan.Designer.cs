using System.Drawing;
using System.Windows.Forms;

namespace Piggy_Admin
{
    partial class FrmThemSuaTaiKhoan
    {
        private System.ComponentModel.IContainer components = null;

        // --- CÁC CONTROL ---
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnCloseHeader;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblNote;

        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtHoTen;

        private System.Windows.Forms.Label lblGioiTinh;
        private System.Windows.Forms.ComboBox cboGioiTinh;

        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;

        // Nút bấm
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Panel pnlLine;

        // Các control cũ ẩn đi
        private System.Windows.Forms.Label lblVaiTro;
        private System.Windows.Forms.ComboBox cboVaiTro;
        private ReaLTaiizor.Controls.NightControlBox nightControlBox1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            btnCloseHeader = new Button();
            lblTitle = new Label();
            txtPassword = new TextBox();
            lblNote = new Label();
            lblHoTen = new Label();
            txtHoTen = new TextBox();
            lblGioiTinh = new Label();
            cboGioiTinh = new ComboBox();
            lblNgaySinh = new Label();
            dtpNgaySinh = new DateTimePicker();
            btnLuu = new Button();
            btnHuy = new Button();
            pnlLine = new Panel();
            lblVaiTro = new Label();
            cboVaiTro = new ComboBox();
            nightControlBox1 = new ReaLTaiizor.Controls.NightControlBox();
            label1 = new Label();
            txtEmail = new TextBox();
            label2 = new Label();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(11, 60, 93);
            pnlHeader.Controls.Add(btnCloseHeader);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(4);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(527, 65);
            pnlHeader.TabIndex = 0;
            // 
            // btnCloseHeader
            // 
            btnCloseHeader.Dock = DockStyle.Right;
            btnCloseHeader.FlatAppearance.BorderSize = 0;
            btnCloseHeader.FlatStyle = FlatStyle.Flat;
            btnCloseHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCloseHeader.ForeColor = Color.White;
            btnCloseHeader.Location = new Point(452, 0);
            btnCloseHeader.Margin = new Padding(4);
            btnCloseHeader.Name = "btnCloseHeader";
            btnCloseHeader.Size = new Size(75, 65);
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
            lblTitle.Location = new Point(27, 13);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(366, 38);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "THÔNG TIN NGƯỜI DÙNG";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 11F);
            txtPassword.Location = new Point(51, 201);
            txtPassword.Margin = new Padding(4);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '•';
            txtPassword.Size = new Size(434, 37);
            txtPassword.TabIndex = 4;
            // 
            // lblNote
            // 
            lblNote.Anchor = AnchorStyles.None;
            lblNote.AutoSize = true;
            lblNote.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblNote.ForeColor = Color.Gray;
            lblNote.Location = new Point(51, 250);
            lblNote.Margin = new Padding(4, 0, 4, 0);
            lblNote.Name = "lblNote";
            lblNote.Size = new Size(0, 25);
            lblNote.TabIndex = 5;
            // 
            // lblHoTen
            // 
            lblHoTen.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHoTen.ForeColor = Color.FromArgb(11, 60, 93);
            lblHoTen.Location = new Point(48, 275);
            lblHoTen.Margin = new Padding(4, 0, 4, 0);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(150, 34);
            lblHoTen.TabIndex = 6;
            lblHoTen.Text = "Họ tên:";
            // 
            // txtHoTen
            // 
            txtHoTen.Font = new Font("Segoe UI", 11F);
            txtHoTen.Location = new Point(48, 313);
            txtHoTen.Margin = new Padding(4);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(437, 37);
            txtHoTen.TabIndex = 7;
            // 
            // lblGioiTinh
            // 
            lblGioiTinh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblGioiTinh.ForeColor = Color.FromArgb(11, 60, 93);
            lblGioiTinh.Location = new Point(51, 354);
            lblGioiTinh.Margin = new Padding(4, 0, 4, 0);
            lblGioiTinh.Name = "lblGioiTinh";
            lblGioiTinh.Size = new Size(150, 34);
            lblGioiTinh.TabIndex = 13;
            lblGioiTinh.Text = "Giới tính:";
            // 
            // cboGioiTinh
            // 
            cboGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGioiTinh.Font = new Font("Segoe UI", 11F);
            cboGioiTinh.FormattingEnabled = true;
            cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            cboGioiTinh.Location = new Point(51, 391);
            cboGioiTinh.Margin = new Padding(4);
            cboGioiTinh.Name = "cboGioiTinh";
            cboGioiTinh.Size = new Size(150, 38);
            cboGioiTinh.TabIndex = 8;
            // 
            // lblNgaySinh
            // 
            lblNgaySinh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNgaySinh.ForeColor = Color.FromArgb(11, 60, 93);
            lblNgaySinh.Location = new Point(291, 354);
            lblNgaySinh.Margin = new Padding(4, 0, 4, 0);
            lblNgaySinh.Name = "lblNgaySinh";
            lblNgaySinh.Size = new Size(150, 34);
            lblNgaySinh.TabIndex = 12;
            lblNgaySinh.Text = "Ngày sinh:";
            // 
            // dtpNgaySinh
            // 
            dtpNgaySinh.Font = new Font("Segoe UI", 11F);
            dtpNgaySinh.Format = DateTimePickerFormat.Short;
            dtpNgaySinh.Location = new Point(291, 393);
            dtpNgaySinh.Margin = new Padding(4);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.Size = new Size(194, 37);
            dtpNgaySinh.TabIndex = 9;
            // 
            // btnLuu
            // 
            btnLuu.BackColor = Color.FromArgb(70, 125, 167);
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLuu.ForeColor = Color.White;
            btnLuu.Location = new Point(267, 502);
            btnLuu.Margin = new Padding(4);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(218, 68);
            btnLuu.TabIndex = 10;
            btnLuu.Text = "LƯU THÔNG TIN";
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
            btnHuy.Location = new Point(49, 502);
            btnHuy.Margin = new Padding(4);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(186, 68);
            btnHuy.TabIndex = 11;
            btnHuy.Text = "Hủy bỏ";
            btnHuy.UseVisualStyleBackColor = false;
            btnHuy.Click += btnHuy_Click;
            // 
            // pnlLine
            // 
            pnlLine.BackColor = Color.LightGray;
            pnlLine.Location = new Point(51, 459);
            pnlLine.Margin = new Padding(4);
            pnlLine.Name = "pnlLine";
            pnlLine.Size = new Size(437, 3);
            pnlLine.TabIndex = 0;
            // 
            // lblVaiTro
            // 
            lblVaiTro.Location = new Point(0, 0);
            lblVaiTro.Margin = new Padding(4, 0, 4, 0);
            lblVaiTro.Name = "lblVaiTro";
            lblVaiTro.Size = new Size(150, 34);
            lblVaiTro.TabIndex = 14;
            lblVaiTro.Visible = false;
            // 
            // cboVaiTro
            // 
            cboVaiTro.Location = new Point(0, 0);
            cboVaiTro.Margin = new Padding(4);
            cboVaiTro.Name = "cboVaiTro";
            cboVaiTro.Size = new Size(180, 33);
            cboVaiTro.TabIndex = 15;
            cboVaiTro.Visible = false;
            // 
            // nightControlBox1
            // 
            nightControlBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            nightControlBox1.BackColor = Color.Transparent;
            nightControlBox1.CloseHoverColor = Color.FromArgb(199, 80, 80);
            nightControlBox1.CloseHoverForeColor = Color.White;
            nightControlBox1.DefaultLocation = true;
            nightControlBox1.DisableMaximizeColor = Color.FromArgb(105, 105, 105);
            nightControlBox1.DisableMinimizeColor = Color.FromArgb(105, 105, 105);
            nightControlBox1.EnableCloseColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.EnableMaximizeButton = true;
            nightControlBox1.EnableMaximizeColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.EnableMinimizeButton = true;
            nightControlBox1.EnableMinimizeColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.Location = new Point(410, 0);
            nightControlBox1.MaximizeHoverColor = Color.FromArgb(15, 255, 255, 255);
            nightControlBox1.MaximizeHoverForeColor = Color.White;
            nightControlBox1.MinimizeHoverColor = Color.FromArgb(15, 255, 255, 255);
            nightControlBox1.MinimizeHoverForeColor = Color.White;
            nightControlBox1.Name = "nightControlBox1";
            nightControlBox1.Size = new Size(0, 0);
            nightControlBox1.TabIndex = 16;
            nightControlBox1.Visible = false;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(11, 60, 93);
            label1.Location = new Point(51, 74);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(150, 34);
            label1.TabIndex = 1;
            label1.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 11F);
            txtEmail.Location = new Point(51, 112);
            txtEmail.Margin = new Padding(4);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(434, 37);
            txtEmail.TabIndex = 2;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(11, 60, 93);
            label2.Location = new Point(48, 163);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(150, 34);
            label2.TabIndex = 3;
            label2.Text = "Mật khẩu:";
            // 
            // FrmThemSuaTaiKhoan
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(527, 609);
            Controls.Add(pnlLine);
            Controls.Add(btnHuy);
            Controls.Add(btnLuu);
            Controls.Add(dtpNgaySinh);
            Controls.Add(lblNgaySinh);
            Controls.Add(cboGioiTinh);
            Controls.Add(lblGioiTinh);
            Controls.Add(txtHoTen);
            Controls.Add(lblHoTen);
            Controls.Add(lblNote);
            Controls.Add(txtPassword);
            Controls.Add(label2);
            Controls.Add(txtEmail);
            Controls.Add(label1);
            Controls.Add(pnlHeader);
            Controls.Add(lblVaiTro);
            Controls.Add(cboVaiTro);
            Controls.Add(nightControlBox1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "FrmThemSuaTaiKhoan";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Thêm tài khoản";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private Label label1;
        private TextBox txtEmail;
        private Label label2;
    }
}