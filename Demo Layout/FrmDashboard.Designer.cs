namespace Demo_Layout
{
    partial class FrmDashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelFilter = new Panel();
            btnLoc = new Button();
            label3 = new Label();
            cboTaiKhoan = new ComboBox();
            dtpDenNgay = new DateTimePicker();
            label2 = new Label();
            dtpTuNgay = new DateTimePicker();
            label1 = new Label();
            tlpMain = new TableLayoutPanel();
            panel2 = new Panel();
            lblTongThuNhap = new Label();
            lblTitle2 = new Label();
            panel1 = new Panel();
            pieChartChiTieu = new LiveCharts.WinForms.PieChart();
            lblTitle1 = new Label();
            panel4 = new Panel();
            cartesianChartThuChi = new LiveCharts.WinForms.CartesianChart();
            lblTitle4 = new Label();
            panel3 = new Panel();
            cartesianChartXuHuong = new LiveCharts.WinForms.CartesianChart();
            lblTitle3 = new Label();
            panelFilter.SuspendLayout();
            tlpMain.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panelFilter
            // 
            panelFilter.BackColor = Color.WhiteSmoke;
            panelFilter.Controls.Add(btnLoc);
            panelFilter.Controls.Add(label3);
            panelFilter.Controls.Add(cboTaiKhoan);
            panelFilter.Controls.Add(dtpDenNgay);
            panelFilter.Controls.Add(label2);
            panelFilter.Controls.Add(dtpTuNgay);
            panelFilter.Controls.Add(label1);
            panelFilter.Dock = DockStyle.Top;
            panelFilter.Location = new Point(0, 0);
            panelFilter.Name = "panelFilter";
            panelFilter.Size = new Size(922, 60);
            panelFilter.TabIndex = 0;
            // 
            // btnLoc
            // 
            btnLoc.BackColor = Color.DodgerBlue;
            btnLoc.ForeColor = Color.White;
            btnLoc.Location = new Point(780, 14);
            btnLoc.Name = "btnLoc";
            btnLoc.Size = new Size(100, 32);
            btnLoc.TabIndex = 6;
            btnLoc.Text = "Lọc Dữ Liệu";
            btnLoc.UseVisualStyleBackColor = false;
            btnLoc.Click += btnLoc_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(520, 20);
            label3.Name = "label3";
            label3.Size = new Size(74, 20);
            label3.TabIndex = 5;
            label3.Text = "Tài khoản:";
            // 
            // cboTaiKhoan
            // 
            cboTaiKhoan.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTaiKhoan.FormattingEnabled = true;
            cboTaiKhoan.Location = new Point(600, 17);
            cboTaiKhoan.Name = "cboTaiKhoan";
            cboTaiKhoan.Size = new Size(160, 28);
            cboTaiKhoan.TabIndex = 4;
            // 
            // dtpDenNgay
            // 
            dtpDenNgay.Format = DateTimePickerFormat.Short;
            dtpDenNgay.Location = new Point(300, 17);
            dtpDenNgay.Name = "dtpDenNgay";
            dtpDenNgay.Size = new Size(120, 27);
            dtpDenNgay.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(250, 20);
            label2.Name = "label2";
            label2.Size = new Size(39, 20);
            label2.TabIndex = 2;
            label2.Text = "Đến:";
            // 
            // dtpTuNgay
            // 
            dtpTuNgay.Format = DateTimePickerFormat.Short;
            dtpTuNgay.Location = new Point(90, 17);
            dtpTuNgay.Name = "dtpTuNgay";
            dtpTuNgay.Size = new Size(120, 27);
            dtpTuNgay.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 20);
            label1.Name = "label1";
            label1.Size = new Size(65, 20);
            label1.TabIndex = 0;
            label1.Text = "Từ ngày:";
            // 
            // tlpMain
            // 
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.04772F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.95228F));
            tlpMain.Controls.Add(panel2, 1, 0);
            tlpMain.Controls.Add(panel1, 0, 0);
            tlpMain.Controls.Add(panel4, 1, 1);
            tlpMain.Controls.Add(panel3, 0, 1);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(0, 60);
            tlpMain.Name = "tlpMain";
            tlpMain.RowCount = 2;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpMain.Size = new Size(922, 551);
            tlpMain.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(lblTongThuNhap);
            panel2.Controls.Add(lblTitle2);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(446, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(473, 269);
            panel2.TabIndex = 1;
            // 
            // lblTongThuNhap
            // 
            lblTongThuNhap.Dock = DockStyle.Fill;
            lblTongThuNhap.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblTongThuNhap.ForeColor = Color.SeaGreen;
            lblTongThuNhap.Location = new Point(0, 30);
            lblTongThuNhap.Name = "lblTongThuNhap";
            lblTongThuNhap.Size = new Size(471, 237);
            lblTongThuNhap.TabIndex = 1;
            lblTongThuNhap.Text = "0 đ";
            lblTongThuNhap.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTitle2
            // 
            lblTitle2.BackColor = Color.AliceBlue;
            lblTitle2.Dock = DockStyle.Top;
            lblTitle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTitle2.Location = new Point(0, 0);
            lblTitle2.Name = "lblTitle2";
            lblTitle2.Size = new Size(471, 30);
            lblTitle2.TabIndex = 0;
            lblTitle2.Text = "2. Tổng Thu Nhập";
            lblTitle2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(pieChartChiTieu);
            panel1.Controls.Add(lblTitle1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(437, 269);
            panel1.TabIndex = 0;
            // 
            // pieChartChiTieu
            // 
            pieChartChiTieu.Dock = DockStyle.Fill;
            pieChartChiTieu.Location = new Point(0, 30);
            pieChartChiTieu.Name = "pieChartChiTieu";
            pieChartChiTieu.Size = new Size(435, 237);
            pieChartChiTieu.TabIndex = 1;
            // 
            // lblTitle1
            // 
            lblTitle1.BackColor = Color.AliceBlue;
            lblTitle1.Dock = DockStyle.Top;
            lblTitle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTitle1.Location = new Point(0, 0);
            lblTitle1.Name = "lblTitle1";
            lblTitle1.Size = new Size(435, 30);
            lblTitle1.TabIndex = 0;
            lblTitle1.Text = "1. Cơ cấu Chi tiêu (Top 5)";
            lblTitle1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(cartesianChartThuChi);
            panel4.Controls.Add(lblTitle4);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(446, 278);
            panel4.Name = "panel4";
            panel4.Size = new Size(473, 270);
            panel4.TabIndex = 3;
            // 
            // cartesianChartThuChi
            // 
            cartesianChartThuChi.Dock = DockStyle.Fill;
            cartesianChartThuChi.Location = new Point(0, 30);
            cartesianChartThuChi.Name = "cartesianChartThuChi";
            cartesianChartThuChi.Size = new Size(471, 238);
            cartesianChartThuChi.TabIndex = 1;
            // 
            // lblTitle4
            // 
            lblTitle4.BackColor = Color.AliceBlue;
            lblTitle4.Dock = DockStyle.Top;
            lblTitle4.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTitle4.Location = new Point(0, 0);
            lblTitle4.Name = "lblTitle4";
            lblTitle4.Size = new Size(471, 30);
            lblTitle4.TabIndex = 0;
            lblTitle4.Text = "4. Tổng quan Thu - Chi";
            lblTitle4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(cartesianChartXuHuong);
            panel3.Controls.Add(lblTitle3);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(3, 278);
            panel3.Name = "panel3";
            panel3.Size = new Size(437, 270);
            panel3.TabIndex = 2;
            // 
            // cartesianChartXuHuong
            // 
            cartesianChartXuHuong.Dock = DockStyle.Fill;
            cartesianChartXuHuong.Location = new Point(0, 30);
            cartesianChartXuHuong.Name = "cartesianChartXuHuong";
            cartesianChartXuHuong.Size = new Size(435, 238);
            cartesianChartXuHuong.TabIndex = 1;
            // 
            // lblTitle3
            // 
            lblTitle3.BackColor = Color.AliceBlue;
            lblTitle3.Dock = DockStyle.Top;
            lblTitle3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTitle3.Location = new Point(0, 0);
            lblTitle3.Name = "lblTitle3";
            lblTitle3.Size = new Size(435, 30);
            lblTitle3.TabIndex = 0;
            lblTitle3.Text = "3. Xu hướng Chi tiêu (Theo ngày)";
            lblTitle3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FrmDashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(922, 611);
            Controls.Add(tlpMain);
            Controls.Add(panelFilter);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(922, 611);
            Name = "FrmDashboard";
            Text = "Dashboard Tài chính";
            WindowState = FormWindowState.Maximized;
            Load += frmDashboard_Load;
            panelFilter.ResumeLayout(false);
            panelFilter.PerformLayout();
            tlpMain.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboTaiKhoan;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel panel1;
        private LiveCharts.WinForms.PieChart pieChartChiTieu;
        private System.Windows.Forms.Label lblTitle1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTongThuNhap;
        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.Panel panel3;
        private LiveCharts.WinForms.CartesianChart cartesianChartXuHuong;
        private System.Windows.Forms.Label lblTitle3;
        private System.Windows.Forms.Panel panel4;
        private LiveCharts.WinForms.CartesianChart cartesianChartThuChi;
        private System.Windows.Forms.Label lblTitle4;
    }
}