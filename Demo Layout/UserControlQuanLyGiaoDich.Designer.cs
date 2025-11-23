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
            panel2 = new Panel();
            panel3 = new Panel();
            cbTaiKhoan = new ComboBox();
            lblTongGiaoDich = new Label();
            panel4 = new Panel();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            txtTimKiem = new TextBox();
            label1 = new Label();
            panel5 = new Panel();
            kryptonDataGridView1 = new DataGridView();
            panel6 = new Panel();
            panel1 = new Panel();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)kryptonDataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(220, 220, 187);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(822, 69);
            panel2.Name = "panel2";
            panel2.Size = new Size(100, 542);
            panel2.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(220, 220, 187);
            panel3.Controls.Add(cbTaiKhoan);
            panel3.Controls.Add(lblTongGiaoDich);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(100, 561);
            panel3.Name = "panel3";
            panel3.Size = new Size(722, 50);
            panel3.TabIndex = 2;
            // 
            // cbTaiKhoan
            // 
            cbTaiKhoan.FormattingEnabled = true;
            cbTaiKhoan.Location = new Point(0, 15);
            cbTaiKhoan.Name = "cbTaiKhoan";
            cbTaiKhoan.Size = new Size(228, 28);
            cbTaiKhoan.TabIndex = 2;
            // 
            // lblTongGiaoDich
            // 
            lblTongGiaoDich.AutoSize = true;
            lblTongGiaoDich.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTongGiaoDich.ForeColor = Color.FromArgb(36, 76, 60);
            lblTongGiaoDich.Location = new Point(234, 20);
            lblTongGiaoDich.Name = "lblTongGiaoDich";
            lblTongGiaoDich.Size = new Size(0, 23);
            lblTongGiaoDich.TabIndex = 1;
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
            panel4.Name = "panel4";
            panel4.Size = new Size(922, 69);
            panel4.TabIndex = 3;
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
            btnThem.Location = new Point(651, 21);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(79, 29);
            btnThem.TabIndex = 4;
            btnThem.Text = "Thêm";
            btnThem.TextAlign = ContentAlignment.MiddleRight;
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
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
            btnSua.Location = new Point(837, 22);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(69, 29);
            btnSua.TabIndex = 2;
            btnSua.Text = "Sửa";
            btnSua.TextAlign = ContentAlignment.MiddleRight;
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
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
            btnXoa.Location = new Point(752, 21);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(68, 29);
            btnXoa.TabIndex = 3;
            btnXoa.Text = "Xoá";
            btnXoa.TextAlign = ContentAlignment.MiddleRight;
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // txtTimKiem
            // 
            txtTimKiem.ForeColor = SystemColors.InactiveCaption;
            txtTimKiem.Location = new Point(317, 23);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(212, 27);
            txtTimKiem.TabIndex = 1;
            txtTimKiem.Text = "  Tìm kiếm...";
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            txtTimKiem.Enter += txtTimKiem_Enter;
            txtTimKiem.Leave += txtTimKiem_Leave;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.ForeColor = Color.Cyan;
            label1.Location = new Point(30, 19);
            label1.Name = "label1";
            label1.Size = new Size(281, 37);
            label1.TabIndex = 0;
            label1.Text = "QUẢN LÝ GIAO DỊCH";
            // 
            // panel5
            // 
            panel5.Controls.Add(kryptonDataGridView1);
            panel5.Controls.Add(panel6);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(100, 69);
            panel5.Name = "panel5";
            panel5.Size = new Size(722, 492);
            panel5.TabIndex = 4;
            // 
            // kryptonDataGridView1
            // 
            kryptonDataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            kryptonDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            kryptonDataGridView1.BorderStyle = BorderStyle.None;
            kryptonDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            kryptonDataGridView1.Location = new Point(0, 50);
            kryptonDataGridView1.Name = "kryptonDataGridView1";
            kryptonDataGridView1.RowHeadersWidth = 51;
            kryptonDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            kryptonDataGridView1.Size = new Size(722, 442);
            kryptonDataGridView1.TabIndex = 2;
            // 
            // panel6
            // 
            panel6.BackColor = Color.FromArgb(220, 220, 187);
            panel6.Dock = DockStyle.Top;
            panel6.Location = new Point(0, 0);
            panel6.Name = "panel6";
            panel6.Size = new Size(722, 50);
            panel6.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(220, 220, 187);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 69);
            panel1.Name = "panel1";
            panel1.Size = new Size(100, 542);
            panel1.TabIndex = 0;
            // 
            // UserControlQuanLyGiaoDich
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel5);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(panel4);
            Name = "UserControlQuanLyGiaoDich";
            Size = new Size(922, 611);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)kryptonDataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Label label1;
        private Panel panel5;
        private Panel panel1;
        private TextBox txtTimKiem;
        private Panel panel6;
        private Button btnSua;
        private Button btnXoa;
        private Button btnThem;
        private DataGridView kryptonDataGridView1;
        private Label lblTongGiaoDich;
        private ComboBox cbTaiKhoan;
    }
}
