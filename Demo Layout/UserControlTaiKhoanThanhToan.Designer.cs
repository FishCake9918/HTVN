namespace Demo_Layout
{
    partial class UserControlTaiKhoanThanhToan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlTaiKhoanThanhToan));
            panel4 = new Panel();
            btnDong = new Button();
            btnThem = new Button();
            txtTimKiem = new TextBox();
            label1 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            panel5 = new Panel();
            panel3 = new Panel();
            kryptonDataGridView1 = new Krypton.Toolkit.KryptonDataGridView();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)kryptonDataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(82, 108, 91);
            panel4.Controls.Add(btnDong);
            panel4.Controls.Add(btnThem);
            panel4.Controls.Add(txtTimKiem);
            panel4.Controls.Add(label1);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 0);
            panel4.Margin = new Padding(4);
            panel4.Name = "panel4";
            panel4.Size = new Size(1152, 86);
            panel4.TabIndex = 9;
            // 
            // btnDong
            // 
            btnDong.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDong.FlatAppearance.BorderSize = 0;
            btnDong.FlatStyle = FlatStyle.Flat;
            btnDong.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDong.ForeColor = Color.White;
            btnDong.Image = (Image)resources.GetObject("btnDong.Image");
            btnDong.ImageAlign = ContentAlignment.MiddleLeft;
            btnDong.Location = new Point(1000, 26);
            btnDong.Margin = new Padding(4);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(101, 36);
            btnDong.TabIndex = 4;
            btnDong.Text = "Đóng";
            btnDong.TextAlign = ContentAlignment.MiddleRight;
            btnDong.UseVisualStyleBackColor = true;
            btnDong.Click += BtnDong_Click;
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
            btnThem.Location = new Point(875, 26);
            btnThem.Margin = new Padding(4);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(99, 36);
            btnThem.TabIndex = 2;
            btnThem.Text = "Thêm";
            btnThem.TextAlign = ContentAlignment.MiddleRight;
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += BtnThem_Click;
            // 
            // txtTimKiem
            // 
            txtTimKiem.ForeColor = SystemColors.InactiveCaption;
            txtTimKiem.Location = new Point(500, 29);
            txtTimKiem.Margin = new Padding(4);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(264, 31);
            txtTimKiem.TabIndex = 1;
            txtTimKiem.Text = "  Tìm kiếm...";
            txtTimKiem.TextChanged += TxtTimKiem_TextChanged;
            txtTimKiem.KeyPress += TxtTimKiem_KeyPress;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(82, 108, 91);
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(220, 220, 187);
            label1.Location = new Point(38, 24);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(422, 45);
            label1.TabIndex = 0;
            label1.Text = "TÀI KHOẢN THANH TOÁN";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(220, 220, 187);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 86);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(30, 678);
            panel1.TabIndex = 15;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(220, 220, 187);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(1122, 86);
            panel2.Margin = new Padding(4);
            panel2.Name = "panel2";
            panel2.Size = new Size(30, 678);
            panel2.TabIndex = 16;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(220, 220, 187);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(30, 86);
            panel5.Margin = new Padding(4);
            panel5.Name = "panel5";
            panel5.Size = new Size(1092, 30);
            panel5.TabIndex = 17;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(220, 220, 187);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(30, 734);
            panel3.Margin = new Padding(4);
            panel3.Name = "panel3";
            panel3.Size = new Size(1092, 30);
            panel3.TabIndex = 18;
            // 
            // kryptonDataGridView1
            // 
            kryptonDataGridView1.BorderStyle = BorderStyle.None;
            kryptonDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            kryptonDataGridView1.Dock = DockStyle.Fill;
            kryptonDataGridView1.Location = new Point(30, 116);
            kryptonDataGridView1.Margin = new Padding(4);
            kryptonDataGridView1.Name = "kryptonDataGridView1";
            kryptonDataGridView1.RowHeadersWidth = 51;
            kryptonDataGridView1.Size = new Size(1092, 618);
            kryptonDataGridView1.TabIndex = 19;
            // 
            // UserControlTaiKhoanThanhToan
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(kryptonDataGridView1);
            Controls.Add(panel3);
            Controls.Add(panel5);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(panel4);
            Margin = new Padding(4);
            Name = "UserControlTaiKhoanThanhToan";
            Size = new Size(1152, 764);
            Load += UserControlTaiKhoanThanhToan_Load;
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)kryptonDataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel4;
        private TextBox txtTimKiem;
        private Label label1;
        private Button btnThem;
        private Button btnDong;
        private Krypton.Toolkit.KryptonDataGridView kryptonDataGridView1;
        private Panel panel1;
        private Panel panel2;
        private Panel panel5;
        private Panel panel3;
    }
}
