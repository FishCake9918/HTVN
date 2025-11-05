namespace Demo_Layout
{
    partial class Payee
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Payee));
            lbPayee = new Label();
            lbTraCuu = new Label();
            panel1 = new Panel();
            label2 = new Label();
            dtgPayee = new DataGridView();
            tbTraCuu = new TextBox();
            label1 = new Label();
            btnThemPayee = new Button();
            btnSuaPayee = new Button();
            btnXoaPayee = new Button();
            btnTraCuu = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgPayee).BeginInit();
            SuspendLayout();
            // 
            // lbPayee
            // 
            lbPayee.AutoSize = true;
            lbPayee.Location = new Point(236, 21);
            lbPayee.Name = "lbPayee";
            lbPayee.Size = new Size(0, 20);
            lbPayee.TabIndex = 0;
            // 
            // lbTraCuu
            // 
            lbTraCuu.AutoSize = true;
            lbTraCuu.Location = new Point(67, 153);
            lbTraCuu.Name = "lbTraCuu";
            lbTraCuu.Size = new Size(102, 20);
            lbTraCuu.TabIndex = 1;
            lbTraCuu.Text = "Nhập từ khóa:";
            // 
            // panel1
            // 
            panel1.Controls.Add(label2);
            panel1.Controls.Add(dtgPayee);
            panel1.Location = new Point(0, 189);
            panel1.Name = "panel1";
            panel1.Size = new Size(1518, 630);
            panel1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(34, 34);
            label2.Name = "label2";
            label2.Size = new Size(213, 20);
            label2.TabIndex = 10;
            label2.Text = "Danh sách đối tượng giao dịch";
            // 
            // dtgPayee
            // 
            dtgPayee.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgPayee.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgPayee.Location = new Point(34, 74);
            dtgPayee.Name = "dtgPayee";
            dtgPayee.RowHeadersWidth = 51;
            dtgPayee.Size = new Size(1451, 542);
            dtgPayee.TabIndex = 4;
            // 
            // tbTraCuu
            // 
            tbTraCuu.Location = new Point(194, 150);
            tbTraCuu.Name = "tbTraCuu";
            tbTraCuu.Size = new Size(793, 27);
            tbTraCuu.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Cambria", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(49, 21);
            label1.Name = "label1";
            label1.Size = new Size(565, 43);
            label1.TabIndex = 5;
            label1.Text = "QUẢN LÝ ĐỐI TƯỢNG GIAO DỊCH";
            // 
            // btnThemPayee
            // 
            btnThemPayee.Image = (Image)resources.GetObject("btnThemPayee.Image");
            btnThemPayee.ImageAlign = ContentAlignment.MiddleLeft;
            btnThemPayee.Location = new Point(1141, 143);
            btnThemPayee.Name = "btnThemPayee";
            btnThemPayee.Size = new Size(79, 40);
            btnThemPayee.TabIndex = 6;
            btnThemPayee.Text = "Thêm";
            btnThemPayee.TextAlign = ContentAlignment.MiddleRight;
            btnThemPayee.UseVisualStyleBackColor = true;
            btnThemPayee.Click += btnThemPayee_Click;
            // 
            // btnSuaPayee
            // 
            btnSuaPayee.Image = (Image)resources.GetObject("btnSuaPayee.Image");
            btnSuaPayee.ImageAlign = ContentAlignment.MiddleLeft;
            btnSuaPayee.Location = new Point(1264, 143);
            btnSuaPayee.Name = "btnSuaPayee";
            btnSuaPayee.Size = new Size(79, 40);
            btnSuaPayee.TabIndex = 7;
            btnSuaPayee.Text = "Sửa";
            btnSuaPayee.TextAlign = ContentAlignment.MiddleRight;
            btnSuaPayee.UseVisualStyleBackColor = true;
            btnSuaPayee.Click += btnSuaPayee_Click;
            // 
            // btnXoaPayee
            // 
            btnXoaPayee.Image = (Image)resources.GetObject("btnXoaPayee.Image");
            btnXoaPayee.ImageAlign = ContentAlignment.MiddleLeft;
            btnXoaPayee.Location = new Point(1381, 143);
            btnXoaPayee.Name = "btnXoaPayee";
            btnXoaPayee.Size = new Size(79, 40);
            btnXoaPayee.TabIndex = 8;
            btnXoaPayee.Text = "Xóa";
            btnXoaPayee.TextAlign = ContentAlignment.MiddleRight;
            btnXoaPayee.UseVisualStyleBackColor = true;
            btnXoaPayee.Click += btnXoaPayee_Click;
            // 
            // btnTraCuu
            // 
            btnTraCuu.Image = (Image)resources.GetObject("btnTraCuu.Image");
            btnTraCuu.ImageAlign = ContentAlignment.MiddleLeft;
            btnTraCuu.Location = new Point(1005, 143);
            btnTraCuu.Name = "btnTraCuu";
            btnTraCuu.Size = new Size(89, 40);
            btnTraCuu.TabIndex = 9;
            btnTraCuu.Text = "Tra cứu";
            btnTraCuu.TextAlign = ContentAlignment.MiddleRight;
            btnTraCuu.UseVisualStyleBackColor = true;
            btnTraCuu.Click += btnTraCuu_Click;
            // 
            // Payee
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1518, 817);
            Controls.Add(btnXoaPayee);
            Controls.Add(btnTraCuu);
            Controls.Add(btnSuaPayee);
            Controls.Add(btnThemPayee);
            Controls.Add(label1);
            Controls.Add(tbTraCuu);
            Controls.Add(panel1);
            Controls.Add(lbTraCuu);
            Controls.Add(lbPayee);
            Name = "Payee";
            Text = "Quản lý đối tượng giao dịch";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgPayee).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbPayee;
        private Label lbTraCuu;
        private Panel panel1;
        private TextBox tbTraCuu;
        private DataGridView dtgPayee;
        private Label label1;
        private Button btnThemPayee;
        private Button btnSuaPayee;
        private Button btnXoaPayee;
        private Button btnTraCuu;
        private Label label2;
    }
}