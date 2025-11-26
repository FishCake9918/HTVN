namespace Piggy_Admin
{
    partial class UserControlBaoCaoHeThong
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            btnLoc = new Button();
            panelCards = new Panel();
            btnInLog = new Button();
            btnInDashboard = new Button();
            cboMocThoiGian = new ComboBox();
            panelCard2 = new Panel();
            lblThoiGianTrungBinh = new Label();
            label5 = new Label();
            panelCard1 = new Panel();
            lblDAU = new Label();
            label3 = new Label();
            tableLayoutPanelCharts = new TableLayoutPanel();
            chartTanSuatDangNhap = new LiveCharts.WinForms.CartesianChart();
            chartTinhNang = new LiveCharts.WinForms.PieChart();
            labelChart2 = new Label();
            labelChart1 = new Label();
            panelCards.SuspendLayout();
            panelCard2.SuspendLayout();
            panelCard1.SuspendLayout();
            tableLayoutPanelCharts.SuspendLayout();
            SuspendLayout();
            // 
            // btnLoc
            // 
            btnLoc.BackColor = Color.FromArgb(47, 67, 215);
            btnLoc.FlatStyle = FlatStyle.Popup;
            btnLoc.Font = new Font("Segoe UI Black", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLoc.ForeColor = Color.Transparent;
            btnLoc.Location = new Point(688, 75);
            btnLoc.Margin = new Padding(3, 4, 3, 4);
            btnLoc.Name = "btnLoc";
            btnLoc.Size = new Size(87, 40);
            btnLoc.TabIndex = 4;
            btnLoc.Text = "Lọc";
            btnLoc.UseVisualStyleBackColor = false;
            btnLoc.Click += btnLoc_Click;
            // 
            // panelCards
            // 
            panelCards.BackColor = Color.FromArgb(248, 150, 30);
            panelCards.Controls.Add(btnInLog);
            panelCards.Controls.Add(btnInDashboard);
            panelCards.Controls.Add(btnLoc);
            panelCards.Controls.Add(cboMocThoiGian);
            panelCards.Controls.Add(panelCard2);
            panelCards.Controls.Add(panelCard1);
            panelCards.Dock = DockStyle.Top;
            panelCards.Location = new Point(0, 0);
            panelCards.Margin = new Padding(3, 4, 3, 4);
            panelCards.Name = "panelCards";
            panelCards.Padding = new Padding(23, 27, 23, 27);
            panelCards.Size = new Size(922, 160);
            panelCards.TabIndex = 1;
            // 
            // btnInLog
            // 
            btnInLog.Location = new Point(814, 124);
            btnInLog.Name = "btnInLog";
            btnInLog.Size = new Size(94, 29);
            btnInLog.TabIndex = 6;
            btnInLog.Text = "In log";
            btnInLog.UseVisualStyleBackColor = true;
            btnInLog.Click += btnInLog_Click;
            // 
            // btnInDashboard
            // 
            btnInDashboard.Location = new Point(688, 124);
            btnInDashboard.Name = "btnInDashboard";
            btnInDashboard.Size = new Size(120, 29);
            btnInDashboard.TabIndex = 5;
            btnInDashboard.Text = "In dashboard";
            btnInDashboard.UseVisualStyleBackColor = true;
            btnInDashboard.Click += btnInDashboard_Click;
            // 
            // cboMocThoiGian
            // 
            cboMocThoiGian.ForeColor = Color.FromArgb(47, 67, 215);
            cboMocThoiGian.FormattingEnabled = true;
            cboMocThoiGian.Location = new Point(688, 30);
            cboMocThoiGian.Name = "cboMocThoiGian";
            cboMocThoiGian.Size = new Size(181, 28);
            cboMocThoiGian.TabIndex = 2;
            cboMocThoiGian.SelectedIndexChanged += cboMocThoiGian_SelectedIndexChanged;
            // 
            // panelCard2
            // 
            panelCard2.BackColor = Color.FromArgb(255, 248, 225);
            panelCard2.BorderStyle = BorderStyle.Fixed3D;
            panelCard2.Controls.Add(lblThoiGianTrungBinh);
            panelCard2.Controls.Add(label5);
            panelCard2.Location = new Point(326, 27);
            panelCard2.Margin = new Padding(3, 4, 3, 4);
            panelCard2.Name = "panelCard2";
            panelCard2.Size = new Size(278, 106);
            panelCard2.TabIndex = 1;
            // 
            // lblThoiGianTrungBinh
            // 
            lblThoiGianTrungBinh.AutoSize = true;
            lblThoiGianTrungBinh.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblThoiGianTrungBinh.ForeColor = Color.DarkOrange;
            lblThoiGianTrungBinh.Location = new Point(23, 47);
            lblThoiGianTrungBinh.Name = "lblThoiGianTrungBinh";
            lblThoiGianTrungBinh.Size = new Size(110, 41);
            lblThoiGianTrungBinh.TabIndex = 1;
            lblThoiGianTrungBinh.Text = "0 phút";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(47, 67, 215);
            label5.Location = new Point(23, 13);
            label5.Name = "label5";
            label5.Size = new Size(235, 23);
            label5.TabIndex = 0;
            label5.Text = "Thời gian sử dụng trung bình";
            // 
            // panelCard1
            // 
            panelCard1.BackColor = Color.FromArgb(227, 242, 253);
            panelCard1.BorderStyle = BorderStyle.Fixed3D;
            panelCard1.Controls.Add(lblDAU);
            panelCard1.Controls.Add(label3);
            panelCard1.Location = new Point(23, 27);
            panelCard1.Margin = new Padding(3, 4, 3, 4);
            panelCard1.Name = "panelCard1";
            panelCard1.Size = new Size(278, 106);
            panelCard1.TabIndex = 0;
            // 
            // lblDAU
            // 
            lblDAU.AutoSize = true;
            lblDAU.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblDAU.ForeColor = Color.DodgerBlue;
            lblDAU.Location = new Point(23, 47);
            lblDAU.Name = "lblDAU";
            lblDAU.Size = new Size(35, 41);
            lblDAU.TabIndex = 1;
            lblDAU.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(47, 67, 215);
            label3.Location = new Point(23, 13);
            label3.Name = "label3";
            label3.Size = new Size(222, 23);
            label3.TabIndex = 0;
            label3.Text = "Lượng người dùng truy cập";
            // 
            // tableLayoutPanelCharts
            // 
            tableLayoutPanelCharts.BackColor = Color.FromArgb(250, 237, 205);
            tableLayoutPanelCharts.ColumnCount = 2;
            tableLayoutPanelCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelCharts.Controls.Add(chartTanSuatDangNhap, 0, 1);
            tableLayoutPanelCharts.Controls.Add(chartTinhNang, 1, 1);
            tableLayoutPanelCharts.Controls.Add(labelChart2, 1, 0);
            tableLayoutPanelCharts.Controls.Add(labelChart1, 0, 0);
            tableLayoutPanelCharts.Dock = DockStyle.Fill;
            tableLayoutPanelCharts.Location = new Point(0, 160);
            tableLayoutPanelCharts.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanelCharts.Name = "tableLayoutPanelCharts";
            tableLayoutPanelCharts.Padding = new Padding(11, 13, 11, 13);
            tableLayoutPanelCharts.RowCount = 2;
            tableLayoutPanelCharts.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanelCharts.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelCharts.Size = new Size(922, 451);
            tableLayoutPanelCharts.TabIndex = 2;
            // 
            // chartTanSuatDangNhap
            // 
            chartTanSuatDangNhap.Dock = DockStyle.Fill;
            chartTanSuatDangNhap.Location = new Point(14, 57);
            chartTanSuatDangNhap.Margin = new Padding(3, 4, 3, 4);
            chartTanSuatDangNhap.Name = "chartTanSuatDangNhap";
            chartTanSuatDangNhap.Size = new Size(444, 377);
            chartTanSuatDangNhap.TabIndex = 0;
            chartTanSuatDangNhap.Text = "cartesianChart1";
            // 
            // chartTinhNang
            // 
            chartTinhNang.Dock = DockStyle.Fill;
            chartTinhNang.Location = new Point(464, 57);
            chartTinhNang.Margin = new Padding(3, 4, 3, 4);
            chartTinhNang.Name = "chartTinhNang";
            chartTinhNang.Size = new Size(444, 377);
            chartTinhNang.TabIndex = 1;
            chartTinhNang.Text = "pieChart1";
            // 
            // labelChart2
            // 
            labelChart2.Anchor = AnchorStyles.Top;
            labelChart2.AutoSize = true;
            labelChart2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            labelChart2.ForeColor = Color.FromArgb(47, 67, 215);
            labelChart2.Location = new Point(550, 13);
            labelChart2.Name = "labelChart2";
            labelChart2.Size = new Size(272, 25);
            labelChart2.TabIndex = 3;
            labelChart2.Text = "Mức độ quan tâm Chức năng";
            labelChart2.TextAlign = ContentAlignment.TopCenter;
            // 
            // labelChart1
            // 
            labelChart1.Anchor = AnchorStyles.Top;
            labelChart1.AutoSize = true;
            labelChart1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            labelChart1.ForeColor = Color.FromArgb(47, 67, 215);
            labelChart1.Location = new Point(141, 13);
            labelChart1.Name = "labelChart1";
            labelChart1.Size = new Size(190, 25);
            labelChart1.TabIndex = 2;
            labelChart1.Text = "Tần suất Đăng nhập";
            labelChart1.TextAlign = ContentAlignment.TopCenter;
            // 
            // UserControlBaoCaoHeThong
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanelCharts);
            Controls.Add(panelCards);
            Margin = new Padding(3, 4, 3, 4);
            Name = "UserControlBaoCaoHeThong";
            Size = new Size(922, 611);
            Load += UserControlBaoCaoHeThong_Load;
            panelCards.ResumeLayout(false);
            panelCard2.ResumeLayout(false);
            panelCard2.PerformLayout();
            panelCard1.ResumeLayout(false);
            panelCard1.PerformLayout();
            tableLayoutPanelCharts.ResumeLayout(false);
            tableLayoutPanelCharts.PerformLayout();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Panel panelCards;
        private System.Windows.Forms.Panel panelCard1;
        private System.Windows.Forms.Label lblDAU;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelCard2;
        private System.Windows.Forms.Label lblThoiGianTrungBinh;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelCharts;
        private LiveCharts.WinForms.CartesianChart chartTanSuatDangNhap;
        private LiveCharts.WinForms.PieChart chartTinhNang;
        private System.Windows.Forms.Label labelChart1;
        private System.Windows.Forms.Label labelChart2;
        private ComboBox cboMocThoiGian;
        private Button btnInLog;
        private Button btnInDashboard;
    }
}