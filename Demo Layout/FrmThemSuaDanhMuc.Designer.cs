namespace Demo_Layout
{
    partial class FrmThemSuaDanhMuc
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmThemSuaDanhMuc));
            lblTen = new Label();
            txtTenDanhMuc = new TextBox();
            lblCha = new Label();
            cboDanhMucCha = new ComboBox();
            btnLuu = new Button();
            btnHuy = new Button();
            button1 = new Button();
            lblForm = new Label();
            SuspendLayout();
            // 
            // lblTen
            // 
            lblTen.AutoSize = true;
            lblTen.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTen.ForeColor = Color.White;
            lblTen.Location = new Point(25, 69);
            lblTen.Name = "lblTen";
            lblTen.Size = new Size(107, 20);
            lblTen.TabIndex = 0;
            lblTen.Text = "Tên danh mục";
            // 
            // txtTenDanhMuc
            // 
            txtTenDanhMuc.Font = new Font("Segoe UI", 9F);
            txtTenDanhMuc.Location = new Point(160, 65);
            txtTenDanhMuc.Margin = new Padding(3, 4, 3, 4);
            txtTenDanhMuc.Name = "txtTenDanhMuc";
            txtTenDanhMuc.Size = new Size(300, 27);
            txtTenDanhMuc.TabIndex = 1;
            // 
            // lblCha
            // 
            lblCha.AutoSize = true;
            lblCha.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCha.ForeColor = Color.White;
            lblCha.Location = new Point(25, 125);
            lblCha.Name = "lblCha";
            lblCha.Size = new Size(108, 20);
            lblCha.TabIndex = 2;
            lblCha.Text = "Danh mục cha";
            // 
            // cboDanhMucCha
            // 
            cboDanhMucCha.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDanhMucCha.Font = new Font("Segoe UI", 9F);
            cboDanhMucCha.FormattingEnabled = true;
            cboDanhMucCha.Location = new Point(160, 121);
            cboDanhMucCha.Margin = new Padding(3, 4, 3, 4);
            cboDanhMucCha.Name = "cboDanhMucCha";
            cboDanhMucCha.Size = new Size(300, 28);
            cboDanhMucCha.TabIndex = 2;
            // 
            // btnLuu
            // 
            btnLuu.BackColor = Color.FromArgb(250, 110, 6);
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLuu.ForeColor = Color.White;
            btnLuu.Location = new Point(250, 167);
            btnLuu.Margin = new Padding(3, 4, 3, 4);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(100, 30);
            btnLuu.TabIndex = 3;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = false;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnHuy
            // 
            btnHuy.BackColor = Color.FromArgb(250, 110, 6);
            btnHuy.DialogResult = DialogResult.Cancel;
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnHuy.ForeColor = Color.White;
            btnHuy.Location = new Point(360, 167);
            btnHuy.Margin = new Padding(3, 4, 3, 4);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(100, 30);
            btnHuy.TabIndex = 4;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = false;
            btnHuy.Click += btnHuy_Click;
            // 
            // button1
            // 
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(441, 12);
            button1.Name = "button1";
            button1.Size = new Size(29, 29);
            button1.TabIndex = 14;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // lblForm
            // 
            lblForm.AutoSize = true;
            lblForm.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblForm.ForeColor = Color.White;
            lblForm.Location = new Point(25, 16);
            lblForm.Name = "lblForm";
            lblForm.Size = new Size(42, 20);
            lblForm.TabIndex = 15;
            lblForm.Text = "label";
            // 
            // FrmThemSuaDanhMuc
            // 
            AcceptButton = btnLuu;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(66, 94, 106);
            CancelButton = btnHuy;
            ClientSize = new Size(482, 210);
            Controls.Add(lblForm);
            Controls.Add(button1);
            Controls.Add(btnHuy);
            Controls.Add(btnLuu);
            Controls.Add(cboDanhMucCha);
            Controls.Add(lblCha);
            Controls.Add(txtTenDanhMuc);
            Controls.Add(lblTen);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmThemSuaDanhMuc";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thêm Danh mục Chi tiêu";
            Load += frmThemDanhMuc_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.TextBox txtTenDanhMuc;
        private System.Windows.Forms.Label lblCha;
        private System.Windows.Forms.ComboBox cboDanhMucCha;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
        private Button button1;
        private Label lblForm;
    }
}


