namespace Demo_Layout
{
    partial class UserControlQuanLyGiaoDich
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlQuanLyGiaoDich));
            panel5 = new Panel();
            kryptonDataGridView1 = new Krypton.Toolkit.KryptonDataGridView();
            panel3 = new Panel();
            lblTongThuChi = new Label();
            cbTaiKhoan = new ComboBox();
            panel1 = new Panel();
            panel2 = new Panel();
            panel6 = new Panel();
            label1 = new Label();
            txtTimKiem = new TextBox();
            btnXoa = new Button();
            btnSua = new Button();
            btnThem = new Button();
            panel4 = new Panel();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)kryptonDataGridView1).BeginInit();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // panel5
            // 
            panel5.Controls.Add(kryptonDataGridView1);
            panel5.Controls.Add(panel3);
            panel5.Controls.Add(panel1);
            panel5.Controls.Add(panel2);
            panel5.Controls.Add(panel6);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(0, 86);
            panel5.Margin = new Padding(4);
            panel5.Name = "panel5";
            panel5.Size = new Size(1152, 678);
            panel5.TabIndex = 4;
            // 
            // kryptonDataGridView1
            // 
            kryptonDataGridView1.AllowUserToAddRows = false;
            kryptonDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            kryptonDataGridView1.BorderStyle = BorderStyle.None;
            kryptonDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            kryptonDataGridView1.Dock = DockStyle.Fill;
            kryptonDataGridView1.Location = new Point(30, 30);
            kryptonDataGridView1.Margin = new Padding(4);
            kryptonDataGridView1.Name = "kryptonDataGridView1";
            kryptonDataGridView1.RowHeadersWidth = 51;
            kryptonDataGridView1.Size = new Size(1092, 594);
            kryptonDataGridView1.TabIndex = 24;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(220, 220, 187);
            panel3.Controls.Add(lblTongThuChi);
            panel3.Controls.Add(cbTaiKhoan);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(30, 624);
            panel3.Margin = new Padding(4);
            panel3.Name = "panel3";
            panel3.Size = new Size(1092, 54);
            panel3.TabIndex = 23;
            // 
            // lblTongThuChi
            // 
            lblTongThuChi.AutoSize = true;
            lblTongThuChi.Location = new Point(309, 11);
            lblTongThuChi.Name = "lblTongThuChi";
            lblTongThuChi.Size = new Size(59, 25);
            lblTongThuChi.TabIndex = 3;
            lblTongThuChi.Text = "label2";
            // 
            // cbTaiKhoan
            // 
            cbTaiKhoan.FormattingEnabled = true;
            cbTaiKhoan.Location = new Point(4, 11);
            cbTaiKhoan.Margin = new Padding(4);
            cbTaiKhoan.Name = "cbTaiKhoan";
            cbTaiKhoan.Size = new Size(284, 33);
            cbTaiKhoan.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(220, 220, 187);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(30, 0);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1092, 30);
            panel1.TabIndex = 22;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(220, 220, 187);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(1122, 0);
            panel2.Margin = new Padding(4);
            panel2.Name = "panel2";
            panel2.Size = new Size(30, 678);
            panel2.TabIndex = 21;
            // 
            // panel6
            // 
            panel6.BackColor = Color.FromArgb(220, 220, 187);
            panel6.Dock = DockStyle.Left;
            panel6.Location = new Point(0, 0);
            panel6.Margin = new Padding(4);
            panel6.Name = "panel6";
            panel6.Size = new Size(30, 678);
            panel6.TabIndex = 20;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(220, 220, 187);
            label1.Location = new Point(38, 24);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(334, 45);
            label1.TabIndex = 0;
            label1.Text = "QUẢN LÝ GIAO DỊCH";
            // 
            // txtTimKiem
            // 
            txtTimKiem.ForeColor = SystemColors.InactiveCaption;
            txtTimKiem.Location = new Point(396, 29);
            txtTimKiem.Margin = new Padding(4);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(264, 31);
            txtTimKiem.TabIndex = 1;
            txtTimKiem.Text = "  Tìm kiếm...";
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            txtTimKiem.Enter += txtTimKiem_Enter;
            txtTimKiem.Leave += txtTimKiem_Leave;
            // 
            // btnXoa
            // 
            btnXoa.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnXoa.FlatAppearance.BorderSize = 0;
            btnXoa.FlatStyle = FlatStyle.Flat;
            btnXoa.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnXoa.ForeColor = Color.White;
            btnXoa.Image = (Image)resources.GetObject("btnXoa.Image");
            btnXoa.ImageAlign = ContentAlignment.MiddleLeft;
            btnXoa.Location = new Point(940, 26);
            btnXoa.Margin = new Padding(4);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(85, 36);
            btnXoa.TabIndex = 3;
            btnXoa.Text = "Xoá";
            btnXoa.TextAlign = ContentAlignment.MiddleRight;
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnSua
            // 
            btnSua.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSua.FlatAppearance.BorderSize = 0;
            btnSua.FlatStyle = FlatStyle.Flat;
            btnSua.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSua.ForeColor = Color.White;
            btnSua.Image = (Image)resources.GetObject("btnSua.Image");
            btnSua.ImageAlign = ContentAlignment.MiddleLeft;
            btnSua.Location = new Point(1046, 28);
            btnSua.Margin = new Padding(4);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(86, 36);
            btnSua.TabIndex = 2;
            btnSua.Text = "Sửa";
            btnSua.TextAlign = ContentAlignment.MiddleRight;
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnThem
            // 
            btnThem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnThem.FlatAppearance.BorderSize = 0;
            btnThem.FlatStyle = FlatStyle.Flat;
            btnThem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnThem.ForeColor = Color.White;
            btnThem.Image = (Image)resources.GetObject("btnThem.Image");
            btnThem.ImageAlign = ContentAlignment.MiddleLeft;
            btnThem.Location = new Point(814, 26);
            btnThem.Margin = new Padding(4);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(99, 36);
            btnThem.TabIndex = 4;
            btnThem.Text = "Thêm";
            btnThem.TextAlign = ContentAlignment.MiddleRight;
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(82, 108, 91);
            panel4.Controls.Add(btnThem);
            panel4.Controls.Add(btnSua);
            panel4.Controls.Add(btnXoa);
            panel4.Controls.Add(txtTimKiem);
            panel4.Controls.Add(label1);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 0);
            panel4.Margin = new Padding(4);
            panel4.Name = "panel4";
            panel4.Size = new Size(1152, 86);
            panel4.TabIndex = 3;
            // 
            // UserControlQuanLyGiaoDich
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel5);
            Controls.Add(panel4);
            Margin = new Padding(4);
            Name = "UserControlQuanLyGiaoDich";
            Size = new Size(1152, 764);
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)kryptonDataGridView1).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel5;
        private Label label1;
        private TextBox txtTimKiem;
        private Button btnXoa;
        private Button btnSua;
        private Button btnThem;
        private ComboBox cbTaiKhoan;
        private Panel panel4;
        private Krypton.Toolkit.KryptonDataGridView kryptonDataGridView1;
        private Panel panel3;
        private Panel panel1;
        private Panel panel2;
        private Panel panel6;
        private Label lblTongThuChi;
    }
}
