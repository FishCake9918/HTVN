namespace Demo_Layout
{
    partial class UserControlDoiTuongGiaoDich
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlDoiTuongGiaoDich));
            panel5 = new Panel();
            panel3 = new Panel();
            panel2 = new Panel();
            panel1 = new Panel();
            panel4 = new Panel();
            btnSua = new Button();
            btnXoa = new Button();
            btnThem = new Button();
            txtTimKiem = new TextBox();
            label1 = new Label();
            kryptonDataGridView1 = new Krypton.Toolkit.KryptonDataGridView();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)kryptonDataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(220, 220, 187);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(100, 69);
            panel5.Name = "panel5";
            panel5.Size = new Size(722, 50);
            panel5.TabIndex = 14;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(220, 220, 187);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(100, 561);
            panel3.Name = "panel3";
            panel3.Size = new Size(722, 50);
            panel3.TabIndex = 12;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(220, 220, 187);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(822, 69);
            panel2.Name = "panel2";
            panel2.Size = new Size(100, 542);
            panel2.TabIndex = 11;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(220, 220, 187);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 69);
            panel1.Name = "panel1";
            panel1.Size = new Size(100, 542);
            panel1.TabIndex = 10;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(82, 108, 91);
            panel4.Controls.Add(btnSua);
            panel4.Controls.Add(btnXoa);
            panel4.Controls.Add(btnThem);
            panel4.Controls.Add(txtTimKiem);
            panel4.Controls.Add(label1);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(922, 69);
            panel4.TabIndex = 13;
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
            btnSua.Location = new Point(838, 22);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(69, 29);
            btnSua.TabIndex = 2;
            btnSua.Text = "Sửa";
            btnSua.TextAlign = ContentAlignment.MiddleRight;
            btnSua.UseVisualStyleBackColor = true;
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
            btnXoa.Location = new Point(753, 21);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(68, 29);
            btnXoa.TabIndex = 3;
            btnXoa.Text = "Xoá";
            btnXoa.TextAlign = ContentAlignment.MiddleRight;
            btnXoa.UseVisualStyleBackColor = true;
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
           // btnThem.Click += btnThem_Click_1;
            // 
            // txtTimKiem
            // 
            txtTimKiem.ForeColor = SystemColors.InactiveCaption;
            txtTimKiem.Location = new Point(380, 23);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(212, 27);
            txtTimKiem.TabIndex = 1;
            txtTimKiem.Text = "  Tìm kiếm...";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.ForeColor = Color.Cyan;
            label1.Location = new Point(30, 19);
            label1.Name = "label1";
            label1.Size = new Size(319, 37);
            label1.TabIndex = 0;
            label1.Text = "ĐỐI TƯỢNG GIAO DỊCH";
            // 
            // kryptonDataGridView1
            // 
            kryptonDataGridView1.BorderStyle = BorderStyle.None;
            kryptonDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            kryptonDataGridView1.Dock = DockStyle.Fill;
            kryptonDataGridView1.Location = new Point(100, 119);
            kryptonDataGridView1.Name = "kryptonDataGridView1";
            kryptonDataGridView1.RowHeadersWidth = 51;
            kryptonDataGridView1.Size = new Size(722, 442);
            kryptonDataGridView1.TabIndex = 15;
            // 
            // UserControlDoiTuongGiaoDich
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(kryptonDataGridView1);
            Controls.Add(panel5);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(panel4);
            Name = "UserControlDoiTuongGiaoDich";
            Size = new Size(922, 611);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)kryptonDataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel5;
        private Panel panel3;
        private Panel panel2;
        private Panel panel1;
        private Panel panel4;
        private TextBox txtTimKiem;
        private Label label1;
        private Button btnSua;
        private Button btnXoa;
        private Button btnThem;
        private Krypton.Toolkit.KryptonDataGridView kryptonDataGridView1;
    }
}
