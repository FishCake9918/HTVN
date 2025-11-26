using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;
using LiveCharts.WinForms;

namespace Demo_Layout
{
    partial class UserControlNganSach
    {
        private System.ComponentModel.IContainer components = null;

        // --- KHAI BÁO CONTROLS ---
        public KryptonComboBox cmbLocThang;
        public KryptonTextBox txtLocNam;
        public KryptonLabel labelTongNS;
        public KryptonPanel kryptonPanelLeft;

        // Labels giá trị chi tiết
        public KryptonLabel lblValueTongNS;
        public KryptonLabel lblValueTongDaChi;
        public KryptonLabel lblValueTongConLai;
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
        private KryptonDataGridView kryptonDataGridView1;

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
            cmbLocThang = new KryptonComboBox();
            txtLocNam = new KryptonTextBox();
            btnSua = new Button();
            btnXoa = new Button();
            btnThem = new Button();
            txtTimKiem = new TextBox();
            label1 = new Label();
            kryptonDataGridView1 = new KryptonDataGridView();
            kryptonPanelLeft = new KryptonPanel();
            pieChartNganSach = new PieChart();
            lblValueTongConLai = new KryptonLabel();
            lblValueTongDaChi = new KryptonLabel();
            lblValueTongNS = new KryptonLabel();
            labelTongNS = new KryptonLabel();
            tableLayoutPanelMain = new TableLayoutPanel();
            panelRightContainer = new Panel();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cmbLocThang).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonDataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonPanelLeft).BeginInit();
            kryptonPanelLeft.SuspendLayout();
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
            cmbLocThang.DropDownWidth = 128;
            cmbLocThang.Location = new Point(311, 20);
            cmbLocThang.Name = "cmbLocThang";
            cmbLocThang.Size = new Size(128, 26);
            cmbLocThang.TabIndex = 1;
            // 
            // txtLocNam
            // 
            txtLocNam.CueHint.CueHintText = "Năm (YYYY)";
            txtLocNam.Location = new Point(445, 20);
            txtLocNam.Name = "txtLocNam";
            txtLocNam.Size = new Size(100, 27);
            txtLocNam.TabIndex = 2;
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
            // kryptonDataGridView1
            // 
            kryptonDataGridView1.BorderStyle = BorderStyle.None;
            kryptonDataGridView1.ColumnHeadersHeight = 36;
            kryptonDataGridView1.Dock = DockStyle.Fill;
            kryptonDataGridView1.Location = new Point(0, 0);
            kryptonDataGridView1.Name = "kryptonDataGridView1";
            kryptonDataGridView1.RowHeadersWidth = 51;
            kryptonDataGridView1.Size = new Size(502, 536);
            kryptonDataGridView1.TabIndex = 0;
            // 
            // kryptonPanelLeft
            // 
            kryptonPanelLeft.Controls.Add(pieChartNganSach);
            kryptonPanelLeft.Controls.Add(lblValueTongConLai);
            kryptonPanelLeft.Controls.Add(lblValueTongDaChi);
            kryptonPanelLeft.Controls.Add(lblValueTongNS);
            kryptonPanelLeft.Controls.Add(labelTongNS);
            kryptonPanelLeft.Dock = DockStyle.Fill;
            kryptonPanelLeft.Location = new Point(3, 3);
            kryptonPanelLeft.Name = "kryptonPanelLeft";
            kryptonPanelLeft.Size = new Size(408, 536);
            kryptonPanelLeft.TabIndex = 0;
            // 
            // pieChartNganSach
            // 
            pieChartNganSach.BackgroundImage = Properties.Resources.blank_default_pfp_wue0zko1dfxs9z2c;
            pieChartNganSach.ForeColor = SystemColors.ButtonHighlight;
            pieChartNganSach.Location = new Point(5, 185);
            pieChartNganSach.Name = "pieChartNganSach";
            pieChartNganSach.Size = new Size(398, 350);
            pieChartNganSach.TabIndex = 1;
            // 
            // lblValueTongConLai
            // 
            lblValueTongConLai.Location = new Point(10, 124);
            lblValueTongConLai.Name = "lblValueTongConLai";
            lblValueTongConLai.Size = new Size(370, 46);
            lblValueTongConLai.StateCommon.ShortText.Color1 = Color.DarkGreen;
            lblValueTongConLai.StateCommon.ShortText.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblValueTongConLai.TabIndex = 2;
            lblValueTongConLai.Values.Text = "TỔNG CÒN LẠI: {0} VNĐ";
            // 
            // lblValueTongDaChi
            // 
            lblValueTongDaChi.Location = new Point(10, 95);
            lblValueTongDaChi.Name = "lblValueTongDaChi";
            lblValueTongDaChi.Size = new Size(204, 32);
            lblValueTongDaChi.StateCommon.ShortText.Color1 = Color.Firebrick;
            lblValueTongDaChi.StateCommon.ShortText.Font = new Font("Segoe UI", 12F);
            lblValueTongDaChi.TabIndex = 3;
            lblValueTongDaChi.Values.Text = "Tổng Đã chi: {0} VNĐ";
            // 
            // lblValueTongNS
            // 
            lblValueTongNS.Location = new Point(5, 57);
            lblValueTongNS.Name = "lblValueTongNS";
            lblValueTongNS.Size = new Size(243, 32);
            lblValueTongNS.StateCommon.ShortText.Font = new Font("Segoe UI", 12F);
            lblValueTongNS.TabIndex = 4;
            lblValueTongNS.Values.Text = "Tổng Ngân sách: {0} VNĐ";
            // 
            // labelTongNS
            // 
            labelTongNS.Location = new Point(76, 16);
            labelTongNS.Name = "labelTongNS";
            labelTongNS.Size = new Size(250, 24);
            labelTongNS.StateCommon.ShortText.Color1 = Color.FromArgb(82, 108, 91);
            labelTongNS.TabIndex = 0;
            labelTongNS.Values.Text = "TỔNG QUAN NGÂN SÁCH ĐÃ LỌC";
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 2;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tableLayoutPanelMain.Controls.Add(kryptonPanelLeft, 0, 0);
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
            panelRightContainer.Controls.Add(kryptonDataGridView1);
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
            ((System.ComponentModel.ISupportInitialize)cmbLocThang).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonDataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonPanelLeft).EndInit();
            kryptonPanelLeft.ResumeLayout(false);
            kryptonPanelLeft.PerformLayout();
            tableLayoutPanelMain.ResumeLayout(false);
            panelRightContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}