using System.Drawing;
using System.Windows.Forms;

namespace Demo_Layout
{
    partial class UserControlDanhMucChiTieu
    {
        private System.ComponentModel.IContainer components = null;

        // --- CÁC CONTROL GỐC (GIỮ NGUYÊN) ---
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnThem;

        private Krypton.Toolkit.KryptonTreeView tvDanhMuc;

        // --- CONTROL MỚI ---
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlCard;

        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlSeparator;

        // Trang trí bên phải
        private System.Windows.Forms.PictureBox pbIllustration;
        private System.Windows.Forms.Label lblSloganTitle;
        private System.Windows.Forms.Label lblSloganSub;
        // Panel chứa chữ để căn giữa dễ hơn
        private System.Windows.Forms.Panel pnlTextContainer;

        // Panel thừa
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlDanhMucChiTieu));
            panel4 = new Panel();
            btnSua = new Button();
            btnXoa = new Button();
            btnThem = new Button();
            label1 = new Label();
            pnlBody = new Panel();
            pnlCard = new Panel();
            panel6 = new Panel();
            pnlRight = new Panel();
            label3 = new Label();
            label2 = new Label();
            pnlTextContainer = new Panel();
            lblSloganSub = new Label();
            lblSloganTitle = new Label();
            pbIllustration = new PictureBox();
            pnlSeparator = new Panel();
            pnlLeft = new Panel();
            tvDanhMuc = new Krypton.Toolkit.KryptonTreeView();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel5 = new Panel();
            panel4.SuspendLayout();
            pnlCard.SuspendLayout();
            pnlRight.SuspendLayout();
            pnlTextContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbIllustration).BeginInit();
            pnlLeft.SuspendLayout();
            SuspendLayout();
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(82, 108, 91);
            panel4.Controls.Add(btnSua);
            panel4.Controls.Add(btnXoa);
            panel4.Controls.Add(btnThem);
            panel4.Controls.Add(label1);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(1152, 86);
            panel4.TabIndex = 9;
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
            btnSua.Location = new Point(940, 26);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(85, 36);
            btnSua.TabIndex = 3;
            btnSua.Text = "Sửa";
            btnSua.TextAlign = ContentAlignment.MiddleRight;
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
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
            btnXoa.Location = new Point(1046, 28);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(86, 36);
            btnXoa.TabIndex = 4;
            btnXoa.Text = "Xóa";
            btnXoa.TextAlign = ContentAlignment.MiddleRight;
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
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
            btnThem.Location = new Point(814, 26);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(99, 36);
            btnThem.TabIndex = 2;
            btnThem.Text = "Thêm";
            btnThem.TextAlign = ContentAlignment.MiddleRight;
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(220, 220, 187);
            label1.Location = new Point(38, 24);
            label1.Name = "label1";
            label1.Size = new Size(342, 45);
            label1.TabIndex = 0;
            label1.Text = "DANH MỤC CHI TIÊU";
            // 
            // pnlBody
            // 
            pnlBody.BackColor = Color.FromArgb(220, 219, 187);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Location = new Point(0, 86);
            pnlBody.Name = "pnlBody";
            pnlBody.Padding = new Padding(30);
            pnlBody.Size = new Size(1152, 678);
            pnlBody.TabIndex = 10;
            // 
            // pnlCard
            // 
            pnlCard.BackColor = Color.White;
            pnlCard.Controls.Add(panel6);
            pnlCard.Controls.Add(pnlRight);
            pnlCard.Controls.Add(pnlSeparator);
            pnlCard.Controls.Add(pnlLeft);
            pnlCard.Dock = DockStyle.Fill;
            pnlCard.Location = new Point(0, 86);
            pnlCard.Name = "pnlCard";
            pnlCard.Size = new Size(1152, 678);
            pnlCard.TabIndex = 0;
            // 
            // panel6
            // 
            panel6.BackColor = Color.FromArgb(244, 244, 238);
            panel6.Dock = DockStyle.Right;
            panel6.Location = new Point(1142, 0);
            panel6.Margin = new Padding(4);
            panel6.Name = "panel6";
            panel6.Size = new Size(10, 678);
            panel6.TabIndex = 20;
            // 
            // pnlRight
            // 
            pnlRight.BackColor = Color.FromArgb(252, 252, 252);
            pnlRight.Controls.Add(label3);
            pnlRight.Controls.Add(label2);
            pnlRight.Controls.Add(pnlTextContainer);
            pnlRight.Controls.Add(pbIllustration);
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.Location = new Point(322, 0);
            pnlRight.Name = "pnlRight";
            pnlRight.Size = new Size(830, 678);
            pnlRight.TabIndex = 3;
            // 
            // label3
            // 
            label3.BackColor = Color.FromArgb(244, 244, 238);
            label3.Dock = DockStyle.Top;
            label3.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(36, 76, 60);
            label3.Location = new Point(0, 14);
            label3.Name = "label3";
            label3.Size = new Size(830, 76);
            label3.TabIndex = 3;
            label3.Text = "SLOGAN OF PIGGY BANK";
            label3.TextAlign = ContentAlignment.BottomCenter;
            // 
            // label2
            // 
            label2.BackColor = Color.FromArgb(244, 244, 238);
            label2.Dock = DockStyle.Top;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(36, 76, 60);
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(830, 14);
            label2.TabIndex = 2;
            label2.TextAlign = ContentAlignment.BottomCenter;
            // 
            // pnlTextContainer
            // 
            pnlTextContainer.BackColor = Color.FromArgb(244, 244, 238);
            pnlTextContainer.Controls.Add(lblSloganSub);
            pnlTextContainer.Controls.Add(lblSloganTitle);
            pnlTextContainer.Dock = DockStyle.Bottom;
            pnlTextContainer.Location = new Point(0, 528);
            pnlTextContainer.Name = "pnlTextContainer";
            pnlTextContainer.Size = new Size(830, 150);
            pnlTextContainer.TabIndex = 1;
            // 
            // lblSloganSub
            // 
            lblSloganSub.Dock = DockStyle.Top;
            lblSloganSub.Font = new Font("Segoe UI", 11F, FontStyle.Italic);
            lblSloganSub.ForeColor = Color.FromArgb(66, 94, 106);
            lblSloganSub.Location = new Point(0, 60);
            lblSloganSub.Name = "lblSloganSub";
            lblSloganSub.Size = new Size(830, 80);
            lblSloganSub.TabIndex = 1;
            lblSloganSub.Text = "Phân loại rõ ràng giúp bạn kiểm soát tài chính tốt hơn.\nHãy chọn một danh mục để bắt đầu.";
            lblSloganSub.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblSloganTitle
            // 
            lblSloganTitle.Dock = DockStyle.Top;
            lblSloganTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblSloganTitle.ForeColor = Color.FromArgb(36, 76, 60);
            lblSloganTitle.Location = new Point(0, 0);
            lblSloganTitle.Name = "lblSloganTitle";
            lblSloganTitle.Size = new Size(830, 60);
            lblSloganTitle.TabIndex = 0;
            lblSloganTitle.Text = "Quản Lý Chi Tiêu Hiệu Quả";
            lblSloganTitle.TextAlign = ContentAlignment.BottomCenter;
            // 
            // pbIllustration
            // 
            pbIllustration.BackColor = Color.FromArgb(244, 244, 238);
            pbIllustration.Dock = DockStyle.Fill;
            pbIllustration.Image = Properties.Resources.chitieuhoply;
            pbIllustration.Location = new Point(0, 0);
            pbIllustration.Name = "pbIllustration";
            pbIllustration.Size = new Size(830, 678);
            pbIllustration.SizeMode = PictureBoxSizeMode.Zoom;
            pbIllustration.TabIndex = 0;
            pbIllustration.TabStop = false;
            // 
            // pnlSeparator
            // 
            pnlSeparator.BackColor = Color.WhiteSmoke;
            pnlSeparator.Dock = DockStyle.Left;
            pnlSeparator.Location = new Point(320, 0);
            pnlSeparator.Name = "pnlSeparator";
            pnlSeparator.Size = new Size(2, 678);
            pnlSeparator.TabIndex = 2;
            // 
            // pnlLeft
            // 
            pnlLeft.BackColor = Color.FromArgb(244, 244, 238);
            pnlLeft.Controls.Add(tvDanhMuc);
            pnlLeft.Dock = DockStyle.Left;
            pnlLeft.Location = new Point(0, 0);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Padding = new Padding(10);
            pnlLeft.Size = new Size(320, 678);
            pnlLeft.TabIndex = 1;
            // 
            // tvDanhMuc
            // 
            tvDanhMuc.Dock = DockStyle.Fill;
            tvDanhMuc.ItemHeight = 40;
            tvDanhMuc.Location = new Point(10, 10);
            tvDanhMuc.Name = "tvDanhMuc";
            tvDanhMuc.Size = new Size(300, 658);
            tvDanhMuc.StateCommon.Back.Color1 = Color.White;
            tvDanhMuc.StateCommon.Node.Content.ShortText.Color1 = Color.FromArgb(64, 64, 64);
            tvDanhMuc.StateCommon.Node.Content.ShortText.Font = new Font("Segoe UI", 11F);
            tvDanhMuc.TabIndex = 0;
            tvDanhMuc.NodeMouseDoubleClick += TvDanhMuc_NodeMouseDoubleClick;
            // 
            // panel1
            // 
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(0, 0);
            panel1.TabIndex = 13;
            panel1.Visible = false;
            // 
            // panel2
            // 
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(0, 0);
            panel2.TabIndex = 14;
            panel2.Visible = false;
            // 
            // panel3
            // 
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(0, 0);
            panel3.TabIndex = 12;
            panel3.Visible = false;
            // 
            // panel5
            // 
            panel5.Location = new Point(0, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(0, 0);
            panel5.TabIndex = 11;
            panel5.Visible = false;
            // 
            // UserControlDanhMucChiTieu
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlCard);
            Controls.Add(pnlBody);
            Controls.Add(panel4);
            Controls.Add(panel5);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Name = "UserControlDanhMucChiTieu";
            Size = new Size(1152, 764);
            Load += UCDanhMucChiTieu_Load;
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            pnlCard.ResumeLayout(false);
            pnlRight.ResumeLayout(false);
            pnlTextContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbIllustration).EndInit();
            pnlLeft.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label label2;
        private Label label3;
        private Panel panel6;
    }
}