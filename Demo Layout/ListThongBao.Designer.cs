namespace Demo_Layout
{
    partial class ListThongBao
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
            components = new System.ComponentModel.Container();
            lblHeader = new Label();
            flowPanel = new FlowLayoutPanel();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // lblHeader
            // 
            lblHeader.BackColor = Color.FromArgb(36, 76, 60);
            lblHeader.Dock = DockStyle.Top;
            lblHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblHeader.ForeColor = Color.White;
            lblHeader.Location = new Point(0, 0);
            lblHeader.Margin = new Padding(4, 0, 4, 0);
            lblHeader.Name = "lblHeader";
            lblHeader.Padding = new Padding(19, 0, 0, 0);
            lblHeader.Size = new Size(450, 56);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "Thông báo";
            lblHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flowPanel
            // 
            flowPanel.AutoScroll = true;
            flowPanel.BackColor = Color.White;
            flowPanel.Dock = DockStyle.Fill;
            flowPanel.FlowDirection = FlowDirection.TopDown;
            flowPanel.Location = new Point(0, 56);
            flowPanel.Margin = new Padding(4, 4, 4, 4);
            flowPanel.Name = "flowPanel";
            flowPanel.Size = new Size(450, 506);
            flowPanel.TabIndex = 1;
            flowPanel.WrapContents = false;
            // 
            // ListThongBao
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(flowPanel);
            Controls.Add(lblHeader);
            Margin = new Padding(4, 4, 4, 4);
            MaximumSize = new Size(450, 562);
            Name = "ListThongBao";
            Size = new Size(450, 562);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.FlowLayoutPanel flowPanel;
        private System.Windows.Forms.Timer timer1;
    }
}