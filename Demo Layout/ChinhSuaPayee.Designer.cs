namespace Demo_Layout
{
    partial class ChinhSuaPayee
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChinhSuaPayee));
            lbPayee = new Label();
            tbPayee = new TextBox();
            lbDanhMuc = new Label();
            cbDanhMuc = new ComboBox();
            btnLuu = new Button();
            btnHuy = new Button();
            lbNote = new Label();
            tbNote = new TextBox();
            SuspendLayout();
            // 
            // lbPayee
            // 
            lbPayee.AutoSize = true;
            lbPayee.Location = new Point(14, 40);
            lbPayee.Margin = new Padding(5, 0, 5, 0);
            lbPayee.Name = "lbPayee";
            lbPayee.Size = new Size(219, 31);
            lbPayee.TabIndex = 0;
            lbPayee.Text = "Đối tượng giao dịch";
            // 
            // tbPayee
            // 
            tbPayee.Location = new Point(253, 37);
            tbPayee.Margin = new Padding(5, 5, 5, 5);
            tbPayee.Name = "tbPayee";
            tbPayee.Size = new Size(339, 38);
            tbPayee.TabIndex = 1;
            // 
            // lbDanhMuc
            // 
            lbDanhMuc.AutoSize = true;
            lbDanhMuc.Location = new Point(14, 107);
            lbDanhMuc.Margin = new Padding(5, 0, 5, 0);
            lbDanhMuc.Name = "lbDanhMuc";
            lbDanhMuc.Size = new Size(118, 31);
            lbDanhMuc.TabIndex = 4;
            lbDanhMuc.Text = "Danh mục";
            // 
            // cbDanhMuc
            // 
            cbDanhMuc.FormattingEnabled = true;
            cbDanhMuc.Location = new Point(141, 107);
            cbDanhMuc.Margin = new Padding(5, 5, 5, 5);
            cbDanhMuc.Name = "cbDanhMuc";
            cbDanhMuc.Size = new Size(451, 39);
            cbDanhMuc.TabIndex = 5;
            // 
            // btnLuu
            // 
            btnLuu.BackColor = Color.LightSkyBlue;
            btnLuu.Location = new Point(179, 236);
            btnLuu.Margin = new Padding(5, 5, 5, 5);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(153, 62);
            btnLuu.TabIndex = 6;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = false;
            // 
            // btnHuy
            // 
            btnHuy.BackColor = Color.Red;
            btnHuy.Location = new Point(397, 236);
            btnHuy.Margin = new Padding(5, 5, 5, 5);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(153, 62);
            btnHuy.TabIndex = 7;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = false;
            // 
            // lbNote
            // 
            lbNote.AutoSize = true;
            lbNote.Location = new Point(14, 165);
            lbNote.Margin = new Padding(5, 0, 5, 0);
            lbNote.Name = "lbNote";
            lbNote.Size = new Size(92, 31);
            lbNote.TabIndex = 8;
            lbNote.Text = "Ghi chú";
            // 
            // tbNote
            // 
            tbNote.Location = new Point(141, 165);
            tbNote.Margin = new Padding(5, 5, 5, 5);
            tbNote.Name = "tbNote";
            tbNote.Size = new Size(451, 38);
            tbNote.TabIndex = 9;
            // 
            // ChinhSuaPayee
            // 
            AutoScaleDimensions = new SizeF(13F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Honeydew;
            ClientSize = new Size(625, 330);
            Controls.Add(tbNote);
            Controls.Add(lbNote);
            Controls.Add(btnHuy);
            Controls.Add(btnLuu);
            Controls.Add(cbDanhMuc);
            Controls.Add(lbDanhMuc);
            Controls.Add(tbPayee);
            Controls.Add(lbPayee);
            Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 5, 5, 5);
            Name = "ChinhSuaPayee";
            Text = "Chỉnh sửa đối tượng giao dịch";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbPayee;
        private TextBox tbPayee;
        private Label lbDanhMuc;
        private ComboBox cbDanhMuc;
        private Button btnLuu;
        private Button btnHuy;
        private Label lbNote;
        private TextBox tbNote;
    }
}