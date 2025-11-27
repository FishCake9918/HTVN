using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Data; // QUAN TRỌNG: Để hiểu List<ThongBao> là của Data

namespace Demo_Layout
{
    public partial class ListThongBao : UserControl
    {
        public ListThongBao()
        {
            InitializeComponent();
        }

        // FIX LỖI: Chỉ định rõ Data.ThongBao để tránh nhầm lẫn namespace
        public void LoadData(List<Data.ThongBao> listData, DateTime lastCheckTime)
        {
            flowPanel.Controls.Clear();

            if (listData == null || listData.Count == 0)
            {
                Label lblNull = new Label();
                lblNull.Text = "Không có thông báo mới.";
                lblNull.AutoSize = false;
                lblNull.Size = new Size(350, 50);
                lblNull.TextAlign = ContentAlignment.MiddleCenter;
                lblNull.ForeColor = Color.Gray;
                lblNull.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
                flowPanel.Controls.Add(lblNull);
                return;
            }

            foreach (var tb in listData)
            {
                // FIX LỖI: Bỏ .HasValue và .Value
                // So sánh trực tiếp DateTime > DateTime
                bool isNew = tb.NgayTao > lastCheckTime;

                var item = new Demo_Layout.ChiTietThongBao(tb, isNew);
                flowPanel.Controls.Add(item);
            }
        }
    }
}