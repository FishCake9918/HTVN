namespace Demo_Layout
{
    partial class FormDongTaiKhoan
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
            label1 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            textBox2 = new TextBox();
            label5 = new Label();
            comboBox1 = new ComboBox();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 28);
            label1.Name = "label1";
            label1.Size = new Size(211, 20);
            label1.TabIndex = 0;
            label1.Text = "Bạn có chắc chắn muốn đóng: ";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(231, 25);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 55);
            label2.Name = "label2";
            label2.Size = new Size(504, 20);
            label2.TabIndex = 2;
            label2.Text = "Tài khoản này có các giao dịch, vì vậy chúng tôi không thể xóa vĩnh viễn nó.";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(362, 32);
            label3.Name = "label3";
            label3.Size = new Size(16, 20);
            label3.TabIndex = 3;
            label3.Text = "?";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(24, 85);
            label4.Name = "label4";
            label4.Size = new Size(214, 20);
            label4.TabIndex = 4;
            label4.Text = "Tài khoản này hiện có số dư là: ";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(231, 82);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(125, 27);
            textBox2.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(24, 115);
            label5.Name = "label5";
            label5.Size = new Size(532, 20);
            label5.TabIndex = 6;
            label5.Text = "Để đóng tài khoản này, hãy chọn một tài khoản khác để chuyển số dư này sang:";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(24, 150);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(532, 28);
            comboBox1.TabIndex = 7;
            // 
            // button1
            // 
            button1.Location = new Point(462, 198);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 8;
            button1.Text = "Đóng";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(362, 198);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 9;
            button2.Text = "Huỷ";
            button2.UseVisualStyleBackColor = true;
            // 
            // FormDongTaiKhoan
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(569, 239);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(comboBox1);
            Controls.Add(label5);
            Controls.Add(textBox2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Name = "FormDongTaiKhoan";
            Text = "Đóng tài khoản";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox2;
        private Label label5;
        private ComboBox comboBox1;
        private Button button1;
        private Button button2;
    }
}