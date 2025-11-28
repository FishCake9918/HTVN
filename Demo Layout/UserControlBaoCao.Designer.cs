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
            panel4.Margin = new Padding(4);
            panel4.Name = "panel4";
            panel4.Size = new Size(1152, 184);
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
            panelFilter.Location = new Point(0, 109);
            panelFilter.Margin = new Padding(4);
            panelFilter.Name = "panelFilter";
            panelFilter.Size = new Size(1152, 75);
            panelFilter.TabIndex = 15;
            // 
            // btnIn
            // 
            btnIn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            btnIn.BackColor = Color.CornflowerBlue;
            btnIn.FlatStyle = FlatStyle.Popup;
            btnIn.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnIn.ForeColor = Color.White;
            btnIn.Location = new Point(1028, 14);
            btnIn.Margin = new Padding(4);
            btnIn.Name = "btnIn";
            btnIn.Size = new Size(101, 40);
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
            button1.Location = new Point(919, 14);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(101, 40);
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
            label5.Location = new Point(506, 25);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(104, 25);
            label5.TabIndex = 5;
            label5.Text = "Tài khoản:";
            // 
            // cboTaiKhoan
            // 
            cboTaiKhoan.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            cboTaiKhoan.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTaiKhoan.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            cboTaiKhoan.FormattingEnabled = true;
            cboTaiKhoan.Location = new Point(621, 21);
            cboTaiKhoan.Margin = new Padding(4);
            cboTaiKhoan.Name = "cboTaiKhoan";
            cboTaiKhoan.Size = new Size(199, 33);
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
            dtpDenNgay.Location = new Point(330, 21);
            dtpDenNgay.Margin = new Padding(4);
            dtpDenNgay.Name = "dtpDenNgay";
            dtpDenNgay.Size = new Size(149, 31);
            dtpDenNgay.TabIndex = 3;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold);
            label6.ForeColor = Color.FromArgb(66, 94, 106);
            label6.Location = new Point(270, 25);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(53, 25);
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
            dtpTuNgay.Location = new Point(105, 22);
            dtpTuNgay.Margin = new Padding(4);
            dtpTuNgay.Name = "dtpTuNgay";
            dtpTuNgay.Size = new Size(149, 31);
            dtpTuNgay.TabIndex = 1;
            dtpTuNgay.Value = new DateTime(2025, 11, 28, 23, 50, 0, 0);
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold);
            label7.ForeColor = Color.FromArgb(66, 94, 106);
            label7.Location = new Point(5, 28);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(90, 25);
            label7.TabIndex = 0;
            label7.Text = "Từ ngày:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(220, 220, 187);
            label1.Location = new Point(21, 30);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(164, 45);
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
            tableLayoutPanel1.Location = new Point(0, 184);
            tableLayoutPanel1.Margin = new Padding(4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tableLayoutPanel1.Size = new Size(1152, 580);
            tableLayoutPanel1.TabIndex = 10;
            // 
            // panel5
            // 
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(cartesianChartXuHuong);
            panel5.Controls.Add(lblTitle3);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(562, 236);
            panel5.Margin = new Padding(4);
            panel5.Name = "panel5";
            panel5.Size = new Size(586, 340);
            panel5.TabIndex = 5;
            // 
            // cartesianChartXuHuong
            // 
            cartesianChartXuHuong.Dock = DockStyle.Fill;
            cartesianChartXuHuong.Location = new Point(0, 51);
            cartesianChartXuHuong.Margin = new Padding(4);
            cartesianChartXuHuong.Name = "cartesianChartXuHuong";
            cartesianChartXuHuong.Size = new Size(584, 287);
            cartesianChartXuHuong.TabIndex = 1;
            // 
            // lblTitle3
            // 
            lblTitle3.Dock = DockStyle.Top;
            lblTitle3.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            lblTitle3.Location = new Point(0, 0);
            lblTitle3.Margin = new Padding(4, 0, 4, 0);
            lblTitle3.Name = "lblTitle3";
            lblTitle3.Size = new Size(584, 51);
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
            panel2.Location = new Point(4, 4);
            panel2.Margin = new Padding(4);
            panel2.Name = "panel2";
            panel2.Size = new Size(550, 224);
            panel2.TabIndex = 2;
            // 
            // lblTongChiTieu
            // 
            lblTongChiTieu.Dock = DockStyle.Fill;
            lblTongChiTieu.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblTongChiTieu.ForeColor = Color.SeaGreen;
            lblTongChiTieu.Location = new Point(0, 52);
            lblTongChiTieu.Margin = new Padding(4, 0, 4, 0);
            lblTongChiTieu.Name = "lblTongChiTieu";
            lblTongChiTieu.Size = new Size(548, 170);
            lblTongChiTieu.TabIndex = 1;
            lblTongChiTieu.Text = "0 đ";
            lblTongChiTieu.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTitle2
            // 
            lblTitle2.Dock = DockStyle.Top;
            lblTitle2.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            lblTitle2.Location = new Point(0, 0);
            lblTitle2.Margin = new Padding(4, 0, 4, 0);
            lblTitle2.Name = "lblTitle2";
            lblTitle2.Size = new Size(548, 52);
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
            panel3.Location = new Point(562, 4);
            panel3.Margin = new Padding(4);
            panel3.Name = "panel3";
            panel3.Size = new Size(586, 224);
            panel3.TabIndex = 4;
            // 
            // cartesianChartThuChi
            // 
            cartesianChartThuChi.Dock = DockStyle.Fill;
            cartesianChartThuChi.Location = new Point(0, 51);
            cartesianChartThuChi.Margin = new Padding(4);
            cartesianChartThuChi.Name = "cartesianChartThuChi";
            cartesianChartThuChi.Size = new Size(584, 171);
            cartesianChartThuChi.TabIndex = 1;
            // 
            // lblTitle4
            // 
            lblTitle4.Dock = DockStyle.Top;
            lblTitle4.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            lblTitle4.Location = new Point(0, 0);
            lblTitle4.Margin = new Padding(4, 0, 4, 0);
            lblTitle4.Name = "lblTitle4";
            lblTitle4.Size = new Size(584, 51);
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
            panel1.Location = new Point(4, 236);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(550, 340);
            panel1.TabIndex = 3;
            // 
            // pieChartChiTieu
            // 
            pieChartChiTieu.Dock = DockStyle.Fill;
            pieChartChiTieu.Location = new Point(0, 52);
            pieChartChiTieu.Margin = new Padding(4);
            pieChartChiTieu.Name = "pieChartChiTieu";
            pieChartChiTieu.Size = new Size(548, 286);
            pieChartChiTieu.TabIndex = 1;
            // 
            // lblTitle1
            // 
            lblTitle1.Dock = DockStyle.Top;
            lblTitle1.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            lblTitle1.Location = new Point(0, 0);
            lblTitle1.Margin = new Padding(4, 0, 4, 0);
            lblTitle1.Name = "lblTitle1";
            lblTitle1.Size = new Size(548, 52);
            lblTitle1.TabIndex = 0;
            lblTitle1.Text = "CƠ CẤU CHI TIÊU";
            lblTitle1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // UserControlBaoCao
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panel4);
            Margin = new Padding(4);
            Name = "UserControlBaoCao";
            Size = new Size(1152, 764);
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
