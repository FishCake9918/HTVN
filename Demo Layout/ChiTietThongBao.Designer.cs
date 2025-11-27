namespace Demo_Layout
{

    partial class ChiTietThongBao
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
            pnlStatus = new Panel();
            lblTieuDe = new Label();
            lblThoiGian = new Label();
            SuspendLayout();
            // 
            // pnlStatus
            // 
            pnlStatus.BackColor = Color.FromArgb(240, 112, 85);
            pnlStatus.Location = new Point(12, 40);
            pnlStatus.Margin = new Padding(4, 4, 4, 4);
            pnlStatus.Name = "pnlStatus";
            pnlStatus.Size = new Size(12, 12);
            pnlStatus.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoEllipsis = true;
            lblTieuDe.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.FromArgb(36, 76, 60);
            lblTieuDe.Location = new Point(38, 12);
            lblTieuDe.Margin = new Padding(4, 0, 4, 0);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(388, 31);
            lblTieuDe.TabIndex = 1;
            lblTieuDe.Text = "Tiêu đề thông báo";
            // 
            // lblThoiGian
            // 
            lblThoiGian.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            lblThoiGian.ForeColor = Color.Gray;
            lblThoiGian.Location = new Point(38, 48);
            lblThoiGian.Margin = new Padding(4, 0, 4, 0);
            lblThoiGian.Name = "lblThoiGian";
            lblThoiGian.Size = new Size(375, 25);
            lblThoiGian.TabIndex = 2;
            lblThoiGian.Text = "12/12/2024 10:00";
            // 
            // ChiTietThongBao
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(lblThoiGian);
            Controls.Add(lblTieuDe);
            Controls.Add(pnlStatus);
            Cursor = Cursors.Hand;
            Margin = new Padding(4, 4, 4, 4);
            MaximumSize = new Size(438, 94);
            Name = "ChiTietThongBao";
            Padding = new Padding(6, 6, 6, 6);
            Size = new Size(438, 94);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblThoiGian;
    }
}