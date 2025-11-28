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
            this.lblTen = new System.Windows.Forms.Label();
            this.txtTenDanhMuc = new System.Windows.Forms.TextBox();
            this.lblCha = new System.Windows.Forms.Label();
            this.cboDanhMucCha = new System.Windows.Forms.ComboBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.lblTen.Location = new System.Drawing.Point(25, 30);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(123, 23);
            this.lblTen.TabIndex = 0;
            this.lblTen.Text = "Tên danh mục:";
            // 
            // txtTenDanhMuc
            // 
            this.txtTenDanhMuc.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.txtTenDanhMuc.Location = new System.Drawing.Point(160, 27);
            this.txtTenDanhMuc.Name = "txtTenDanhMuc";
            this.txtTenDanhMuc.Size = new System.Drawing.Size(300, 30);
            this.txtTenDanhMuc.TabIndex = 1;
            // 
            // lblCha
            // 
            this.lblCha.AutoSize = true;
            this.lblCha.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.lblCha.Location = new System.Drawing.Point(25, 75);
            this.lblCha.Name = "lblCha";
            this.lblCha.Size = new System.Drawing.Size(129, 23);
            this.lblCha.TabIndex = 2;
            this.lblCha.Text = "Danh mục cha:";
            // 
            // cboDanhMucCha
            // 
            this.cboDanhMucCha.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDanhMucCha.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cboDanhMucCha.FormattingEnabled = true;
            this.cboDanhMucCha.Location = new System.Drawing.Point(160, 72);
            this.cboDanhMucCha.Name = "cboDanhMucCha";
            this.cboDanhMucCha.Size = new System.Drawing.Size(300, 31);
            this.cboDanhMucCha.TabIndex = 2;
            // 
            // btnLuu
            // 
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnLuu.Location = new System.Drawing.Point(250, 130);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(100, 35);
            this.btnLuu.TabIndex = 3;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnHuy.Location = new System.Drawing.Point(360, 130);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(100, 35);
            this.btnHuy.TabIndex = 4;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // frmThemDanhMuc
            // 
            this.AcceptButton = this.btnLuu;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(482, 183);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.cboDanhMucCha);
            this.Controls.Add(this.lblCha);
            this.Controls.Add(this.txtTenDanhMuc);
            this.Controls.Add(this.lblTen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmThemDanhMuc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm Danh mục Chi tiêu";
            this.Load += new System.EventHandler(this.frmThemDanhMuc_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.TextBox txtTenDanhMuc;
        private System.Windows.Forms.Label lblCha;
        private System.Windows.Forms.ComboBox cboDanhMucCha;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
    }
}


