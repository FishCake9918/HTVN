namespace Demo_Layout
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FormThemTaiKhoan form = new FormThemTaiKhoan();
            form.ShowDialog();
        }
    }
}
