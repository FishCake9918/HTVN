namespace Demo_Layout
{
    partial class UserControlBaoCao
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
            panel4 = new Panel();
            panelFilter = new Panel();
            btnIn = new Button();
            button1 = new Button();
            label5 = new Label();
            cboTaiKhoan = new ComboBox();
            dtpDenNgay = new DateTimePicker();
            label6 = new Label();
            dtpTuNgay = new DateTimePicker();
            label7 = new Label();
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel5 = new Panel();
            cartesianChartXuHuong = new LiveCharts.WinForms.CartesianChart();
            lblTitle3 = new Label();
            panel2 = new Panel();
            lblTongChiTieu = new Label();
            lblTitle2 = new Label();
            panel3 = new Panel();
            cartesianChartThuChi = new LiveCharts.WinForms.CartesianChart();
            lblTitle4 = new Label();
            panel1 = new Panel();
            pieChartChiTieu = new LiveCharts.WinForms.PieChart();
            lblTitle1 = new Label();
            panel4.SuspendLayout();
            panelFilter.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel5.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(82, 108, 91);
            panel4.Controls.Add(panelFilter);
            panel4.Controls.Add(label1);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(922, 147);
            panel4.TabIndex = 9;
            // 
            // panelFilter
            // 
            panelFilter.BackColor = Color.FromArgb(220, 220, 187);
            panelFilter.Controls.Add(btnIn);
            panelFilter.Controls.Add(button1);
            panelFilter.Controls.Add(label5);
            panelFilter.Controls.Add(cboTaiKhoan);
            panelFilter.Controls.Add(dtpDenNgay);
            panelFilter.Controls.Add(label6);
            panelFilter.Controls.Add(dtpTuNgay);
            panelFilter.Controls.Add(label7);
            panelFilter.Dock = DockStyle.Bottom;
            panelFilter.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            panelFilter.Location = new Point(0, 87);
            panelFilter.Name = "panelFilter";
            panelFilter.Size = new Size(922, 60);
            panelFilter.TabIndex = 15;
            // 
            // btnIn
            // 
            btnIn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            btnIn.BackColor = Color.CornflowerBlue;
            btnIn.FlatStyle = FlatStyle.Popup;
            btnIn.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnIn.ForeColor = Color.White;
            btnIn.Location = new Point(837, 15);
            btnIn.Name = "btnIn";
            btnIn.Size = new Size(81, 32);
            btnIn.TabIndex = 7;
            btnIn.Text = "In";
            btnIn.UseVisualStyleBackColor = false;
            btnIn.Click += btnIn_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            button1.BackColor = Color.FromArgb(250, 110, 6);
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(686, 15);
            button1.Name = "button1";
            button1.Size = new Size(81, 32);
            button1.TabIndex = 6;
            button1.Text = "Xem";
            button1.UseVisualStyleBackColor = false;
            button1.Click += btnLoc_Click;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(66, 94, 106);
            label5.Location = new Point(405, 20);
            label5.Name = "label5";
            label5.Size = new Size(86, 20);
            label5.TabIndex = 5;
            label5.Text = "Tài khoản:";
            // 
            // cboTaiKhoan
            // 
            cboTaiKhoan.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            cboTaiKhoan.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTaiKhoan.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            cboTaiKhoan.FormattingEnabled = true;
            cboTaiKhoan.Location = new Point(497, 17);
            cboTaiKhoan.Name = "cboTaiKhoan";
            cboTaiKhoan.Size = new Size(160, 28);
            cboTaiKhoan.TabIndex = 4;
            cboTaiKhoan.SelectedIndexChanged += cboTaiKhoan_SelectedIndexChanged;
            // 
            // dtpDenNgay
            // 
            dtpDenNgay.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dtpDenNgay.CalendarFont = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dtpDenNgay.CalendarForeColor = Color.FromArgb(66, 94, 106);
            dtpDenNgay.CalendarMonthBackground = Color.FromArgb(220, 220, 187);
            dtpDenNgay.CalendarTitleBackColor = Color.FromArgb(82, 108, 91);
            dtpDenNgay.CalendarTitleForeColor = Color.FromArgb(220, 220, 187);
            dtpDenNgay.CalendarTrailingForeColor = Color.Gray;
            dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            dtpDenNgay.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dtpDenNgay.Format = DateTimePickerFormat.Custom;
            dtpDenNgay.Location = new Point(264, 17);
            dtpDenNgay.Name = "dtpDenNgay";
            dtpDenNgay.Size = new Size(120, 27);
            dtpDenNgay.TabIndex = 3;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold);
            label6.ForeColor = Color.FromArgb(66, 94, 106);
            label6.Location = new Point(216, 20);
            label6.Name = "label6";
            label6.Size = new Size(42, 20);
            label6.TabIndex = 2;
            label6.Text = "Đến:";
            // 
            // dtpTuNgay
            // 
            dtpTuNgay.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dtpTuNgay.CalendarFont = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dtpTuNgay.CalendarForeColor = Color.FromArgb(66, 94, 106);
            dtpTuNgay.CalendarMonthBackground = Color.FromArgb(220, 220, 187);
            dtpTuNgay.CalendarTitleBackColor = Color.FromArgb(82, 108, 91);
            dtpTuNgay.CalendarTitleForeColor = Color.FromArgb(220, 220, 187);
            dtpTuNgay.CalendarTrailingForeColor = Color.Gray;
            dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            dtpTuNgay.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dtpTuNgay.Format = DateTimePickerFormat.Custom;
            dtpTuNgay.Location = new Point(84, 18);
            dtpTuNgay.Name = "dtpTuNgay";
            dtpTuNgay.Size = new Size(120, 27);
            dtpTuNgay.TabIndex = 1;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold);
            label7.ForeColor = Color.FromArgb(66, 94, 106);
            label7.Location = new Point(4, 22);
            label7.Name = "label7";
            label7.Size = new Size(75, 20);
            label7.TabIndex = 0;
            label7.Text = "Từ ngày:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.ForeColor = Color.Cyan;
            label1.Location = new Point(30, 19);
            label1.Name = "label1";
            label1.Size = new Size(136, 37);
            label1.TabIndex = 0;
            label1.Text = "BÁO CÁO";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.48156F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.5184364F));
            tableLayoutPanel1.Controls.Add(panel5, 1, 1);
            tableLayoutPanel1.Controls.Add(panel2, 0, 0);
            tableLayoutPanel1.Controls.Add(panel3, 1, 0);
            tableLayoutPanel1.Controls.Add(panel1, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 147);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tableLayoutPanel1.Size = new Size(922, 464);
            tableLayoutPanel1.TabIndex = 10;
            // 
            // panel5
            // 
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(cartesianChartXuHuong);
            panel5.Controls.Add(lblTitle3);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(450, 188);
            panel5.Name = "panel5";
            panel5.Size = new Size(469, 273);
            panel5.TabIndex = 5;
            // 
            // cartesianChartXuHuong
            // 
            cartesianChartXuHuong.Dock = DockStyle.Fill;
            cartesianChartXuHuong.Location = new Point(0, 41);
            cartesianChartXuHuong.Name = "cartesianChartXuHuong";
            cartesianChartXuHuong.Size = new Size(467, 230);
            cartesianChartXuHuong.TabIndex = 1;
            // 
            // lblTitle3
            // 
            lblTitle3.Dock = DockStyle.Top;
            lblTitle3.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            lblTitle3.Location = new Point(0, 0);
            lblTitle3.Name = "lblTitle3";
            lblTitle3.Size = new Size(467, 41);
            lblTitle3.TabIndex = 0;
            lblTitle3.Text = "XU HƯỚNG CHI TIÊU";
            lblTitle3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(lblTongChiTieu);
            panel2.Controls.Add(lblTitle2);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(441, 179);
            panel2.TabIndex = 2;
            // 
            // lblTongChiTieu
            // 
            lblTongChiTieu.Dock = DockStyle.Fill;
            lblTongChiTieu.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblTongChiTieu.ForeColor = Color.SeaGreen;
            lblTongChiTieu.Location = new Point(0, 42);
            lblTongChiTieu.Name = "lblTongChiTieu";
            lblTongChiTieu.Size = new Size(439, 135);
            lblTongChiTieu.TabIndex = 1;
            lblTongChiTieu.Text = "0 đ";
            lblTongChiTieu.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTitle2
            // 
            lblTitle2.Dock = DockStyle.Top;
            lblTitle2.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            lblTitle2.Location = new Point(0, 0);
            lblTitle2.Name = "lblTitle2";
            lblTitle2.Size = new Size(439, 42);
            lblTitle2.TabIndex = 0;
            lblTitle2.Text = "TỔNG CHI TIÊU";
            lblTitle2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(cartesianChartThuChi);
            panel3.Controls.Add(lblTitle4);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(450, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(469, 179);
            panel3.TabIndex = 4;
            // 
            // cartesianChartThuChi
            // 
            cartesianChartThuChi.Dock = DockStyle.Fill;
            cartesianChartThuChi.Location = new Point(0, 41);
            cartesianChartThuChi.Name = "cartesianChartThuChi";
            cartesianChartThuChi.Size = new Size(467, 136);
            cartesianChartThuChi.TabIndex = 1;
            // 
            // lblTitle4
            // 
            lblTitle4.Dock = DockStyle.Top;
            lblTitle4.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            lblTitle4.Location = new Point(0, 0);
            lblTitle4.Name = "lblTitle4";
            lblTitle4.Size = new Size(467, 41);
            lblTitle4.TabIndex = 0;
            lblTitle4.Text = "TỔNG QUAN";
            lblTitle4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(pieChartChiTieu);
            panel1.Controls.Add(lblTitle1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 188);
            panel1.Name = "panel1";
            panel1.Size = new Size(441, 273);
            panel1.TabIndex = 3;
            // 
            // pieChartChiTieu
            // 
            pieChartChiTieu.Dock = DockStyle.Fill;
            pieChartChiTieu.Location = new Point(0, 42);
            pieChartChiTieu.Name = "pieChartChiTieu";
            pieChartChiTieu.Size = new Size(439, 229);
            pieChartChiTieu.TabIndex = 1;
            // 
            // lblTitle1
            // 
            lblTitle1.Dock = DockStyle.Top;
            lblTitle1.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            lblTitle1.Location = new Point(0, 0);
            lblTitle1.Name = "lblTitle1";
            lblTitle1.Size = new Size(439, 42);
            lblTitle1.TabIndex = 0;
            lblTitle1.Text = "CƠ CẤU CHI TIÊU";
            lblTitle1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // UserControlBaoCao
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panel4);
            Name = "UserControlBaoCao";
            Size = new Size(922, 611);
            Load += UserControlBaoCao_Load;
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panelFilter.ResumeLayout(false);
            panelFilter.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel4;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel2;
        private Label lblTongChiTieu;
        private Label lblTitle2;
        private Panel panel1;
        private LiveCharts.WinForms.PieChart pieChartChiTieu;
        private Label lblTitle1;
        private Panel panel3;
        private LiveCharts.WinForms.CartesianChart cartesianChartThuChi;
        private Label lblTitle4;
        private Panel panel5;
        private LiveCharts.WinForms.CartesianChart cartesianChartXuHuong;
        private Label lblTitle3;
        private Panel panelFilter;
        private Button button1;
        private Label label5;
        private ComboBox cboTaiKhoan;
        private DateTimePicker dtpDenNgay;
        private Label label6;
        private DateTimePicker dtpTuNgay;
        private Label label7;
        private Button btnIn;
    }
}
