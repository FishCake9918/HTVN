namespace Demo_Layout
{
    partial class FrmThemBaoCao
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
            this.btnDong = new System.Windows.Forms.Button();
            this.btnLuuBaoCao = new System.Windows.Forms.Button();
            this.btnXemBaoCao = new System.Windows.Forms.Button();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.cboDoiTuong = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboDanhMuc = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboThuChi = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();

            // 1. Khởi tạo Chart (QUAN TRỌNG)
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();

            // Panel chứa sidebar để layout đẹp hơn
            this.panelSidebar = new System.Windows.Forms.Panel();

            // Panel chứa chart
            this.panelChart = new System.Windows.Forms.Panel();

            this.grpFilter.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.panelChart.SuspendLayout();
            this.SuspendLayout();

            // 
            // panelSidebar (Chứa các filter bên trái)
            // 
            this.panelSidebar.Controls.Add(this.lblHeader);
            this.panelSidebar.Controls.Add(this.grpFilter);
            this.panelSidebar.Controls.Add(this.btnXemBaoCao);
            this.panelSidebar.Controls.Add(this.btnLuuBaoCao);
            this.panelSidebar.Controls.Add(this.btnDong);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Padding = new System.Windows.Forms.Padding(10);
            this.panelSidebar.Size = new System.Drawing.Size(300, 674);
            this.panelSidebar.TabIndex = 0;

            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(12, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(200, 32);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Tạo Báo Cáo Mới";

            // 
            // grpFilter
            // 
            this.grpFilter.Controls.Add(this.label5);
            this.grpFilter.Controls.Add(this.dtpDenNgay);
            this.grpFilter.Controls.Add(this.label4);
            this.grpFilter.Controls.Add(this.dtpTuNgay);
            this.grpFilter.Controls.Add(this.cboDoiTuong);
            this.grpFilter.Controls.Add(this.label3);
            this.grpFilter.Controls.Add(this.cboDanhMuc);
            this.grpFilter.Controls.Add(this.label2);
            this.grpFilter.Controls.Add(this.cboThuChi);
            this.grpFilter.Controls.Add(this.label1);
            this.grpFilter.Location = new System.Drawing.Point(12, 50);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Size = new System.Drawing.Size(270, 320);
            this.grpFilter.TabIndex = 1;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "Bộ Lọc";

            // ... (Các control con trong grpFilter giữ nguyên vị trí như code cũ của bạn) ...
            // label1, cboThuChi, label2, cboDanhMuc... tôi viết gọn lại để code không quá dài
            this.label1.Location = new System.Drawing.Point(10, 25); this.label1.Text = "Thu/Chi:"; this.label1.AutoSize = true;
            this.cboThuChi.Location = new System.Drawing.Point(10, 45); this.cboThuChi.Size = new System.Drawing.Size(240, 28);

            this.label2.Location = new System.Drawing.Point(10, 80); this.label2.Text = "Danh mục:"; this.label2.AutoSize = true;
            this.cboDanhMuc.Location = new System.Drawing.Point(10, 100); this.cboDanhMuc.Size = new System.Drawing.Size(240, 28);

            this.label3.Location = new System.Drawing.Point(10, 135); this.label3.Text = "Đối tượng:"; this.label3.AutoSize = true;
            this.cboDoiTuong.Location = new System.Drawing.Point(10, 155); this.cboDoiTuong.Size = new System.Drawing.Size(240, 28);

            this.label4.Location = new System.Drawing.Point(10, 190); this.label4.Text = "Từ ngày:"; this.label4.AutoSize = true;
            this.dtpTuNgay.Location = new System.Drawing.Point(10, 210); this.dtpTuNgay.Size = new System.Drawing.Size(240, 27);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.label5.Location = new System.Drawing.Point(10, 245); this.label5.Text = "Đến ngày:"; this.label5.AutoSize = true;
            this.dtpDenNgay.Location = new System.Drawing.Point(10, 265); this.dtpDenNgay.Size = new System.Drawing.Size(240, 27);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;


            // 
            // btnXemBaoCao
            // 
            this.btnXemBaoCao.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnXemBaoCao.ForeColor = System.Drawing.Color.White;
            this.btnXemBaoCao.Location = new System.Drawing.Point(12, 380);
            this.btnXemBaoCao.Name = "btnXemBaoCao";
            this.btnXemBaoCao.Size = new System.Drawing.Size(270, 40);
            this.btnXemBaoCao.TabIndex = 2;
            this.btnXemBaoCao.Text = "Xem Báo Cáo";
            this.btnXemBaoCao.UseVisualStyleBackColor = false;
            this.btnXemBaoCao.Click += new System.EventHandler(this.btnXemBaoCao_Click);

            // 
            // btnLuuBaoCao
            // 
            this.btnLuuBaoCao.BackColor = System.Drawing.Color.SeaGreen;
            this.btnLuuBaoCao.ForeColor = System.Drawing.Color.White;
            this.btnLuuBaoCao.Location = new System.Drawing.Point(12, 430);
            this.btnLuuBaoCao.Name = "btnLuuBaoCao";
            this.btnLuuBaoCao.Size = new System.Drawing.Size(270, 40);
            this.btnLuuBaoCao.TabIndex = 3;
            this.btnLuuBaoCao.Text = "Lưu Báo Cáo";
            this.btnLuuBaoCao.UseVisualStyleBackColor = false;
            this.btnLuuBaoCao.Click += new System.EventHandler(this.btnLuuBaoCao_Click);

            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(12, 480);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(75, 30);
            this.btnDong.TabIndex = 4;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);

            // 
            // panelChart (Chứa biểu đồ bên phải)
            // 
            this.panelChart.BackColor = System.Drawing.Color.White;
            this.panelChart.Controls.Add(this.cartesianChart1); // 2. QUAN TRỌNG: Add Chart vào Panel
            this.panelChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChart.Location = new System.Drawing.Point(300, 0);
            this.panelChart.Name = "panelChart";
            this.panelChart.Padding = new System.Windows.Forms.Padding(20);
            this.panelChart.Size = new System.Drawing.Size(1186, 674);
            this.panelChart.TabIndex = 1;

            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Dock = System.Windows.Forms.DockStyle.Fill; // 3. Cho Chart tràn đầy Panel
            this.cartesianChart1.Location = new System.Drawing.Point(20, 20);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(1146, 634);
            this.cartesianChart1.TabIndex = 0;
            this.cartesianChart1.Text = "cartesianChart1";

            // 
            // FrmThemBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1486, 674);

            // 4. QUAN TRỌNG: Add các Panel chính vào Form
            this.Controls.Add(this.panelChart);
            this.Controls.Add(this.panelSidebar);

            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FrmThemBaoCao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tạo Báo Cáo Mới";
            this.Load += new System.EventHandler(this.FrmThemBaoCao_Load);

            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            this.panelChart.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Panel panelChart; // Panel chứa biểu đồ
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboThuChi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboDanhMuc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboDoiTuong;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Button btnXemBaoCao;
        private System.Windows.Forms.Button btnLuuBaoCao;
        private System.Windows.Forms.Button btnDong;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
    }
}