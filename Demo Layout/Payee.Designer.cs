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
            lbDSPayee = new Label();
            tbTraCuu = new TextBox();
            label1 = new Label();
            btnThemPayee = new Button();
            btnSuaPayee = new Button();
            btnXoaPayee = new Button();
            dtgPayee = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dtgPayee).BeginInit();
            SuspendLayout();
            // 
            // lbPayee
            // 
            lbPayee.AutoSize = true;
            lbPayee.Location = new Point(324, 29);
            lbPayee.Margin = new Padding(4, 0, 4, 0);
            lbPayee.Name = "lbPayee";
            lbPayee.Size = new Size(0, 28);
            lbPayee.TabIndex = 0;
            // 
            // lbTraCuu
            // 
            lbTraCuu.AutoSize = true;
            lbTraCuu.Location = new Point(13, 98);
            lbTraCuu.Margin = new Padding(4, 0, 4, 0);
            lbTraCuu.Name = "lbTraCuu";
            lbTraCuu.Size = new Size(136, 28);
            lbTraCuu.TabIndex = 1;
            lbTraCuu.Text = "Nhập từ khóa:";
            // 
            // lbDSPayee
            // 
            lbDSPayee.AutoSize = true;
            lbDSPayee.Location = new Point(13, 146);
            lbDSPayee.Margin = new Padding(4, 0, 4, 0);
            lbDSPayee.Name = "lbDSPayee";
            lbDSPayee.Size = new Size(280, 28);
            lbDSPayee.TabIndex = 10;
            lbDSPayee.Text = "Danh sách đối tượng giao dịch";
            // 
            // tbTraCuu
            // 
            tbTraCuu.Location = new Point(157, 98);
            tbTraCuu.Margin = new Padding(4);
            tbTraCuu.Name = "tbTraCuu";
            tbTraCuu.Size = new Size(553, 34);
            tbTraCuu.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Black", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(26, 29);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(629, 50);
            label1.TabIndex = 5;
            label1.Text = "QUẢN LÝ ĐỐI TƯỢNG GIAO DỊCH";
            // 
            // btnThemPayee
            // 
            btnThemPayee.Image = (Image)resources.GetObject("btnThemPayee.Image");
            btnThemPayee.ImageAlign = ContentAlignment.MiddleLeft;
            btnThemPayee.Location = new Point(739, 95);
            btnThemPayee.Margin = new Padding(4);
            btnThemPayee.Name = "btnThemPayee";
            btnThemPayee.Size = new Size(92, 40);
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
            btnSuaPayee.Location = new Point(880, 95);
            btnSuaPayee.Margin = new Padding(4);
            btnSuaPayee.Name = "btnSuaPayee";
            btnSuaPayee.Size = new Size(92, 40);
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
            btnXoaPayee.Location = new Point(1014, 95);
            btnXoaPayee.Margin = new Padding(4);
            btnXoaPayee.Name = "btnXoaPayee";
            btnXoaPayee.Size = new Size(92, 40);
            btnXoaPayee.TabIndex = 8;
            btnXoaPayee.Text = "Xóa";
            btnXoaPayee.TextAlign = ContentAlignment.MiddleRight;
            btnXoaPayee.UseVisualStyleBackColor = true;
            btnXoaPayee.Click += btnXoaPayee_Click;
            // 
            // dtgPayee
            // 
            dtgPayee.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgPayee.Location = new Point(11, 189);
            dtgPayee.Name = "dtgPayee";
            dtgPayee.RowHeadersWidth = 51;
            dtgPayee.Size = new Size(1105, 473);
            dtgPayee.TabIndex = 11;
            // 
            // Payee
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Honeydew;
            ClientSize = new Size(1129, 665);
            Controls.Add(dtgPayee);
            Controls.Add(lbDSPayee);
            Controls.Add(btnXoaPayee);
            Controls.Add(btnSuaPayee);
            Controls.Add(btnThemPayee);
            Controls.Add(label1);
            Controls.Add(tbTraCuu);
            Controls.Add(lbTraCuu);
            Controls.Add(lbPayee);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "Payee";
            Text = "Quản lý đối tượng giao dịch";
            ((System.ComponentModel.ISupportInitialize)dtgPayee).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbPayee;
        private Label lbTraCuu;
        private TextBox tbTraCuu;
        private Label label1;
        private Button btnThemPayee;
        private Button btnSuaPayee;
        private Button btnXoaPayee;
        private Label lbDSPayee;
        private DataGridView dtgPayee;
    }
}