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
            lbHidden = new Label();
            checkBox1 = new CheckBox();
            lbDanhMuc = new Label();
            cbDanhMuc = new ComboBox();
            btnLuu = new Button();
            btnHuy = new Button();
            lbNote = new Label();
            tbNote = new TextBox();
            tbURLweb = new TextBox();
            lbURLweb = new Label();
            SuspendLayout();
            // 
            // lbPayee
            // 
            lbPayee.AutoSize = true;
            lbPayee.Location = new Point(46, 39);
            lbPayee.Margin = new Padding(5, 0, 5, 0);
            lbPayee.Name = "lbPayee";
            lbPayee.Size = new Size(197, 26);
            lbPayee.TabIndex = 0;
            lbPayee.Text = "Đối tượng giao dịch";
            // 
            // tbPayee
            // 
            tbPayee.Location = new Point(253, 31);
            tbPayee.Margin = new Padding(5, 4, 5, 4);
            tbPayee.Name = "tbPayee";
            tbPayee.Size = new Size(339, 34);
            tbPayee.TabIndex = 1;
            // 
            // lbHidden
            // 
            lbHidden.AutoSize = true;
            lbHidden.Location = new Point(46, 98);
            lbHidden.Margin = new Padding(5, 0, 5, 0);
            lbHidden.Name = "lbHidden";
            lbHidden.Size = new Size(79, 26);
            lbHidden.TabIndex = 2;
            lbHidden.Text = "Hidden";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(253, 94);
            checkBox1.Margin = new Padding(5, 4, 5, 4);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(248, 30);
            checkBox1.TabIndex = 3;
            checkBox1.Text = "Ẩn đối tượng giao dịch";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // lbDanhMuc
            // 
            lbDanhMuc.AutoSize = true;
            lbDanhMuc.Location = new Point(46, 157);
            lbDanhMuc.Margin = new Padding(5, 0, 5, 0);
            lbDanhMuc.Name = "lbDanhMuc";
            lbDanhMuc.Size = new Size(108, 26);
            lbDanhMuc.TabIndex = 4;
            lbDanhMuc.Text = "Danh mục";
            // 
            // cbDanhMuc
            // 
            cbDanhMuc.FormattingEnabled = true;
            cbDanhMuc.Location = new Point(253, 157);
            cbDanhMuc.Margin = new Padding(5, 4, 5, 4);
            cbDanhMuc.Name = "cbDanhMuc";
            cbDanhMuc.Size = new Size(339, 34);
            cbDanhMuc.TabIndex = 5;
            // 
            // btnLuu
            // 
            btnLuu.BackColor = Color.LightSkyBlue;
            btnLuu.Location = new Point(141, 344);
            btnLuu.Margin = new Padding(5, 4, 5, 4);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(153, 52);
            btnLuu.TabIndex = 6;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = false;
            // 
            // btnHuy
            // 
            btnHuy.BackColor = Color.Red;
            btnHuy.Location = new Point(407, 344);
            btnHuy.Margin = new Padding(5, 4, 5, 4);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(153, 52);
            btnHuy.TabIndex = 7;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = false;
            // 
            // lbNote
            // 
            lbNote.AutoSize = true;
            lbNote.Location = new Point(46, 282);
            lbNote.Margin = new Padding(5, 0, 5, 0);
            lbNote.Name = "lbNote";
            lbNote.Size = new Size(86, 26);
            lbNote.TabIndex = 8;
            lbNote.Text = "Ghi chú";
            // 
            // tbNote
            // 
            tbNote.Location = new Point(253, 274);
            tbNote.Margin = new Padding(5, 4, 5, 4);
            tbNote.Name = "tbNote";
            tbNote.Size = new Size(339, 34);
            tbNote.TabIndex = 9;
            // 
            // tbURLweb
            // 
            tbURLweb.Location = new Point(253, 211);
            tbURLweb.Margin = new Padding(5, 4, 5, 4);
            tbURLweb.Name = "tbURLweb";
            tbURLweb.Size = new Size(339, 34);
            tbURLweb.TabIndex = 10;
            // 
            // lbURLweb
            // 
            lbURLweb.AutoSize = true;
            lbURLweb.Location = new Point(46, 219);
            lbURLweb.Margin = new Padding(5, 0, 5, 0);
            lbURLweb.Name = "lbURLweb";
            lbURLweb.Size = new Size(111, 26);
            lbURLweb.TabIndex = 11;
            lbURLweb.Text = "Trang web";
            // 
            // ThemPayee
            // 
            AutoScaleDimensions = new SizeF(13F, 26F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(678, 419);
            Controls.Add(lbURLweb);
            Controls.Add(tbURLweb);
            Controls.Add(tbNote);
            Controls.Add(lbNote);
            Controls.Add(btnHuy);
            Controls.Add(btnLuu);
            Controls.Add(cbDanhMuc);
            Controls.Add(lbDanhMuc);
            Controls.Add(checkBox1);
            Controls.Add(lbHidden);
            Controls.Add(tbPayee);
            Controls.Add(lbPayee);
            Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 4, 5, 4);
            Name = "ThemPayee";
            Text = "Chỉnh sửa đối tượng giao dịch";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbPayee;
        private TextBox tbPayee;
        private Label lbHidden;
        private CheckBox checkBox1;
        private Label lbDanhMuc;
        private ComboBox cbDanhMuc;
        private Button btnLuu;
        private Button btnHuy;
        private Label lbNote;
        private TextBox tbNote;
        private TextBox tbURLweb;
        private Label lbURLweb;
    }
}