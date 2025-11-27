using System.Drawing;
using System.Windows.Forms;
// Đã loại bỏ using Krypton.Toolkit
using LiveCharts.WinForms;

namespace Demo_Layout
{
    partial class UserControlNganSach
    {
        private System.ComponentModel.IContainer components = null;

        // --- KHAI BÁO CONTROLS (Đã thay đổi loại) ---
        public ComboBox cmbLocThang; // KryptonComboBox -> ComboBox
        public TextBox txtLocNam; // KryptonTextBox -> TextBox
        public Label labelTongNS; // KryptonLabel -> Label
        public Panel panelLeftContainer; // KryptonPanel -> Panel
        
        // Labels giá trị chi tiết
        public Label lblValueTongNS; // KryptonLabel -> Label
        public Label lblValueTongDaChi; // KryptonLabel -> Label
        public Label lblValueTongConLai; // KryptonLabel -> Label
        public PieChart pieChartNganSach;

        // Cấu trúc layout và controls WinForms
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Panel panelRightContainer;
        private System.Windows.Forms.DataGridView dataGridView1; // KryptonDataGridView -> DataGridView

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlNganSach));
            panel4 = new Panel();
            cmbLocThang = new ComboBox();
            txtLocNam = new TextBox();
            btnSua = new Button();
            btnXoa = new Button();
            btnThem = new Button();
            txtTimKiem = new TextBox();
            label1 = new Label();
            dataGridView1 = new DataGridView();
            panelLeftContainer = new Panel();
            pieChartNganSach = new PieChart();
            lblValueTongConLai = new Label();
            lblValueTongDaChi = new Label();
            lblValueTongNS = new Label();
            labelTongNS = new Label();
            tableLayoutPanelMain = new TableLayoutPanel();
            panelRightContainer = new Panel();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panelLeftContainer.SuspendLayout();
            tableLayoutPanelMain.SuspendLayout();
            panelRightContainer.SuspendLayout();
            SuspendLayout();
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(82, 108, 91);
            panel4.Controls.Add(cmbLocThang);
            panel4.Controls.Add(txtLocNam);
            panel4.Controls.Add(btnSua);
            panel4.Controls.Add(btnXoa);
            panel4.Controls.Add(btnThem);
            panel4.Controls.Add(txtTimKiem);
            panel4.Controls.Add(label1);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(922, 69);
            panel4.TabIndex = 8;
            // 
            // cmbLocThang
            // 
            cmbLocThang.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLocThang.Location = new Point(311, 20);
            cmbLocThang.Name = "cmbLocThang";
            cmbLocThang.Size = new Size(128, 28);
            cmbLocThang.TabIndex = 1;
            // 
            // txtLocNam
            // 
            txtLocNam.ForeColor = SystemColors.InactiveCaption;
            txtLocNam.Location = new Point(445, 20);
            txtLocNam.Name = "txtLocNam";
            txtLocNam.Size = new Size(100, 27);
            txtLocNam.TabIndex = 2;
            txtLocNam.Text = "Năm (YYYY)";
            // 
            // btnSua
            // 
            btnSua.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSua.FlatAppearance.BorderSize = 0;
            btnSua.FlatStyle = FlatStyle.Flat;
            btnSua.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSua.ForeColor = Color.White;
            btnSua.Image = (Image)resources.GetObject("btnSua.Image");
            btnSua.ImageAlign = ContentAlignment.MiddleLeft;
            btnSua.Location = new Point(835, 22);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(75, 23);
            btnSua.TabIndex = 6;
            btnSua.Text = "Sửa";
            btnSua.TextAlign = ContentAlignment.MiddleRight;
            btnSua.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            btnXoa.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnXoa.FlatAppearance.BorderSize = 0;
            btnXoa.FlatStyle = FlatStyle.Flat;
            btnXoa.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnXoa.ForeColor = Color.White;
            btnXoa.Image = (Image)resources.GetObject("btnXoa.Image");
            btnXoa.ImageAlign = ContentAlignment.MiddleLeft;
            btnXoa.Location = new Point(750, 21);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(75, 23);
            btnXoa.TabIndex = 5;
            btnXoa.Text = "Xoá";
            btnXoa.TextAlign = ContentAlignment.MiddleRight;
            btnXoa.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            btnThem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnThem.FlatAppearance.BorderSize = 0;
            btnThem.FlatStyle = FlatStyle.Flat;
            btnThem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnThem.ForeColor = Color.White;
            btnThem.Image = (Image)resources.GetObject("btnThem.Image");
            btnThem.ImageAlign = ContentAlignment.MiddleLeft;
            btnThem.Location = new Point(649, 21);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(75, 23);
            btnThem.TabIndex = 4;
            btnThem.Text = "Thêm";
            btnThem.TextAlign = ContentAlignment.MiddleRight;
            btnThem.UseVisualStyleBackColor = true;
            // 
            // txtTimKiem
            // 
            txtTimKiem.ForeColor = SystemColors.InactiveCaption;
            txtTimKiem.Location = new Point(551, 20);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(100, 27);
            txtTimKiem.TabIndex = 3;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.ForeColor = Color.Cyan;
            label1.Location = new Point(13, 13);
            label1.Name = "label1";
            label1.Size = new Size(306, 37);
            label1.TabIndex = 0;
            label1.Text = "QUẢN LÝ NGÂN SÁCH";
            // 
            // dataGridView1
            // 
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersHeight = 36;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(502, 536);
            dataGridView1.TabIndex = 0;
            // 
            // panelLeftContainer
            // 
            panelLeftContainer.Controls.Add(pieChartNganSach);
            panelLeftContainer.Controls.Add(lblValueTongConLai);
            panelLeftContainer.Controls.Add(lblValueTongDaChi);
            panelLeftContainer.Controls.Add(lblValueTongNS);
            panelLeftContainer.Controls.Add(labelTongNS);
            panelLeftContainer.Dock = DockStyle.Fill;
            panelLeftContainer.Location = new Point(3, 3);
            panelLeftContainer.Name = "panelLeftContainer";
            panelLeftContainer.Size = new Size(408, 536);
            panelLeftContainer.TabIndex = 0;
            // 
            // pieChartNganSach
            // 
            pieChartNganSach.BackgroundImage = Properties.Resources.blank_default_pfp_wue0zko1dfxs9z2c;
            pieChartNganSach.ForeColor = SystemColors.ButtonHighlight;
            pieChartNganSach.Location = new Point(5, 185);
            pieChartNganSach.Name = "pieChartNganSach";
            pieChartNganSach.Size = new Size(400, 348);
            pieChartNganSach.TabIndex = 1;
            // 
            // lblValueTongConLai
            // 
            lblValueTongConLai.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblValueTongConLai.ForeColor = Color.DarkGreen;
            lblValueTongConLai.Location = new Point(10, 124);
            lblValueTongConLai.Name = "lblValueTongConLai";
            lblValueTongConLai.Size = new Size(370, 46);
            lblValueTongConLai.TabIndex = 2;
            lblValueTongConLai.Text = "TỔNG CÒN LẠI: {0} VNĐ";
            // 
            // lblValueTongDaChi
            // 
            lblValueTongDaChi.Font = new Font("Segoe UI", 12F);
            lblValueTongDaChi.ForeColor = Color.Firebrick;
            lblValueTongDaChi.Location = new Point(10, 95);
            lblValueTongDaChi.Name = "lblValueTongDaChi";
            lblValueTongDaChi.Size = new Size(204, 32);
            lblValueTongDaChi.TabIndex = 3;
            lblValueTongDaChi.Text = "Tổng Đã chi: {0} VNĐ";
            // 
            // lblValueTongNS
            // 
            lblValueTongNS.Font = new Font("Segoe UI", 12F);
            lblValueTongNS.Location = new Point(5, 57);
            lblValueTongNS.Name = "lblValueTongNS";
            lblValueTongNS.Size = new Size(243, 32);
            lblValueTongNS.TabIndex = 4;
            lblValueTongNS.Text = "Tổng Ngân sách: {0} VNĐ";
            // 
            // labelTongNS
            // 
            labelTongNS.ForeColor = Color.FromArgb(82, 108, 91);
            labelTongNS.Location = new Point(76, 16);
            labelTongNS.Name = "labelTongNS";
            labelTongNS.Size = new Size(250, 24);
            labelTongNS.TabIndex = 0;
            labelTongNS.Text = "TỔNG QUAN NGÂN SÁCH ĐÃ LỌC";
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 2;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tableLayoutPanelMain.Controls.Add(panelLeftContainer, 0, 0);
            tableLayoutPanelMain.Controls.Add(panelRightContainer, 1, 0);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 69);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 1;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Size = new Size(922, 542);
            tableLayoutPanelMain.TabIndex = 9;
            // 
            // panelRightContainer
            // 
            panelRightContainer.Controls.Add(dataGridView1);
            panelRightContainer.Dock = DockStyle.Fill;
            panelRightContainer.Location = new Point(417, 3);
            panelRightContainer.Name = "panelRightContainer";
            panelRightContainer.Size = new Size(502, 536);
            panelRightContainer.TabIndex = 1;
            // 
            // UserControlNganSach
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanelMain);
            Controls.Add(panel4);
            Name = "UserControlNganSach";
            Size = new Size(922, 611);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panelLeftContainer.ResumeLayout(false);
            tableLayoutPanelMain.ResumeLayout(false);
            panelRightContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}