using System.Drawing;
using System.Windows.Forms;
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
            panelLeftContainer = new Panel();
            pieChartNganSach = new PieChart();
            lblValueTongConLai = new Label();
            lblValueTongDaChi = new Label();
            lblValueTongNS = new Label();
            labelTongNS = new Label();
            tableLayoutPanelMain = new TableLayoutPanel();
            panelRightContainer = new Panel();
            panel2 = new Panel();
            dataGridView1 = new Krypton.Toolkit.KryptonDataGridView();
            panel4.SuspendLayout();
            panelLeftContainer.SuspendLayout();
            tableLayoutPanelMain.SuspendLayout();
            panelRightContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
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
            panel4.Margin = new Padding(4);
            panel4.Name = "panel4";
            panel4.Size = new Size(1152, 86);
            panel4.TabIndex = 8;
            // 
            // cmbLocThang
            // 
            cmbLocThang.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLocThang.Location = new Point(389, 25);
            cmbLocThang.Margin = new Padding(4);
            cmbLocThang.Name = "cmbLocThang";
            cmbLocThang.Size = new Size(159, 33);
            cmbLocThang.TabIndex = 1;
            // 
            // txtLocNam
            // 
            txtLocNam.ForeColor = SystemColors.InactiveCaption;
            txtLocNam.Location = new Point(556, 25);
            txtLocNam.Margin = new Padding(4);
            txtLocNam.Name = "txtLocNam";
            txtLocNam.Size = new Size(124, 31);
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
            btnSua.Location = new Point(1055, 18);
            btnSua.Margin = new Padding(4);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(94, 54);
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
            btnXoa.Location = new Point(954, 19);
            btnXoa.Margin = new Padding(4);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(94, 51);
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
            btnThem.Location = new Point(852, 18);
            btnThem.Margin = new Padding(4);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(94, 54);
            btnThem.TabIndex = 4;
            btnThem.Text = "Thêm";
            btnThem.TextAlign = ContentAlignment.MiddleRight;
            btnThem.UseVisualStyleBackColor = true;
            // 
            // txtTimKiem
            // 
            txtTimKiem.ForeColor = SystemColors.InactiveCaption;
            txtTimKiem.Location = new Point(689, 25);
            txtTimKiem.Margin = new Padding(4);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(124, 31);
            txtTimKiem.TabIndex = 3;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(220, 220, 187);
            label1.Location = new Point(4, 14);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(382, 46);
            label1.TabIndex = 0;
            label1.Text = "QUẢN LÝ NGÂN SÁCH";
            // 
            // panelLeftContainer
            // 
            panelLeftContainer.Controls.Add(pieChartNganSach);
            panelLeftContainer.Controls.Add(lblValueTongConLai);
            panelLeftContainer.Controls.Add(lblValueTongDaChi);
            panelLeftContainer.Controls.Add(lblValueTongNS);
            panelLeftContainer.Controls.Add(labelTongNS);
            panelLeftContainer.Dock = DockStyle.Fill;
            panelLeftContainer.Location = new Point(4, 4);
            panelLeftContainer.Margin = new Padding(4);
            panelLeftContainer.Name = "panelLeftContainer";
            panelLeftContainer.Size = new Size(510, 670);
            panelLeftContainer.TabIndex = 0;
            // 
            // pieChartNganSach
            // 
            pieChartNganSach.BackgroundImage = Properties.Resources.blank_default_pfp_wue0zko1dfxs9z2c;
            pieChartNganSach.ForeColor = SystemColors.ButtonHighlight;
            pieChartNganSach.Location = new Point(6, 231);
            pieChartNganSach.Margin = new Padding(4);
            pieChartNganSach.Name = "pieChartNganSach";
            pieChartNganSach.Size = new Size(500, 435);
            pieChartNganSach.TabIndex = 1;
            // 
            // lblValueTongConLai
            // 
            lblValueTongConLai.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblValueTongConLai.ForeColor = Color.DarkGreen;
            lblValueTongConLai.Location = new Point(31, 170);
            lblValueTongConLai.Margin = new Padding(4, 0, 4, 0);
            lblValueTongConLai.Name = "lblValueTongConLai";
            lblValueTongConLai.Size = new Size(454, 58);
            lblValueTongConLai.TabIndex = 2;
            lblValueTongConLai.Text = "TỔNG CÒN LẠI: {0} VNĐ";
            // 
            // lblValueTongDaChi
            // 
            lblValueTongDaChi.Font = new Font("Segoe UI", 12F);
            lblValueTongDaChi.ForeColor = Color.Firebrick;
            lblValueTongDaChi.Location = new Point(6, 111);
            lblValueTongDaChi.Margin = new Padding(4, 0, 4, 0);
            lblValueTongDaChi.Name = "lblValueTongDaChi";
            lblValueTongDaChi.Size = new Size(251, 40);
            lblValueTongDaChi.TabIndex = 3;
            lblValueTongDaChi.Text = "Tổng Đã chi: {0} VNĐ";
            // 
            // lblValueTongNS
            // 
            lblValueTongNS.Font = new Font("Segoe UI", 12F);
            lblValueTongNS.Location = new Point(6, 71);
            lblValueTongNS.Margin = new Padding(4, 0, 4, 0);
            lblValueTongNS.Name = "lblValueTongNS";
            lblValueTongNS.Size = new Size(304, 40);
            lblValueTongNS.TabIndex = 4;
            lblValueTongNS.Text = "Tổng Ngân sách: {0} VNĐ";
            // 
            // labelTongNS
            // 
            labelTongNS.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelTongNS.ForeColor = Color.FromArgb(82, 108, 91);
            labelTongNS.Location = new Point(0, 14);
            labelTongNS.Margin = new Padding(4, 0, 4, 0);
            labelTongNS.Name = "labelTongNS";
            labelTongNS.Size = new Size(506, 46);
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
            tableLayoutPanelMain.Location = new Point(0, 86);
            tableLayoutPanelMain.Margin = new Padding(4);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 1;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanelMain.Size = new Size(1152, 678);
            tableLayoutPanelMain.TabIndex = 9;
            // 
            // panelRightContainer
            // 
            panelRightContainer.Controls.Add(dataGridView1);
            panelRightContainer.Controls.Add(panel2);
            panelRightContainer.Dock = DockStyle.Fill;
            panelRightContainer.Location = new Point(522, 4);
            panelRightContainer.Margin = new Padding(4);
            panelRightContainer.Name = "panelRightContainer";
            panelRightContainer.Size = new Size(626, 670);
            panelRightContainer.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(220, 220, 187);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(596, 0);
            panel2.Margin = new Padding(4);
            panel2.Name = "panel2";
            panel2.Size = new Size(30, 670);
            panel2.TabIndex = 17;
            // 
            // dataGridView1
            // 
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersHeight = 36;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Margin = new Padding(4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(596, 670);
            dataGridView1.TabIndex = 18;
            // 
            // UserControlNganSach
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanelMain);
            Controls.Add(panel4);
            Margin = new Padding(4);
            Name = "UserControlNganSach";
            Size = new Size(1152, 764);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panelLeftContainer.ResumeLayout(false);
            tableLayoutPanelMain.ResumeLayout(false);
            panelRightContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel2;
        private Krypton.Toolkit.KryptonDataGridView dataGridView1;
    }
}