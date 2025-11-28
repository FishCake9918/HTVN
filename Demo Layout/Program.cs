using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Windows.Forms;
using Demo_Layout;

namespace Demo_Layout
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            // Bắt buộc thêm dòng này để khởi tạo tương thích WinForms/WPF (cho LiveCharts)
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            var builder = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var connectionString = hostContext.Configuration.GetConnectionString("QLTCCNConnection");

                    // 1. SỬA ĐĂNG KÝ: Dùng AddDbContextFactory thay vì AddDbContext
                    services.AddDbContextFactory<QLTCCNContext>(options =>
                        options.UseSqlServer(connectionString));

                    // 2. Đăng ký Form và UserControl là Transient (vòng đời ngắn)
                    services.AddTransient<FrmMain>();
                    services.AddTransient<UserControlBaoCao>();
                    services.AddTransient<UserControlQuanLyGiaoDich>();
                    services.AddTransient<UserControlNganSach>();
                    services.AddTransient<UserControlDoiTuongGiaoDich>();
                    services.AddTransient<UserControlDanhMucChiTieu>();
                    services.AddTransient<UserControlTaiKhoanThanhToan>();
                    services.AddTransient<FrmDongTaiKhoanThanhToan>();
                    services.AddTransient<FrmThemTaiKhoanThanhToan>();
                    services.AddTransient<UserControlDoiTuongGiaoDich>();
                    services.AddTransient<FrmThemSuaDoiTuongGiaoDich>();
                    services.AddTransient<UserControlNganSach>();
                    services.AddTransient<FrmThemSuaNganSach>();
                    services.AddTransient<FrmThemGiaoDich>();
                    services.AddTransient<FrmThemSuaDanhMuc>();
                })
                .Build();

            ApplicationConfiguration.Initialize();

            var formMain = builder.Services.GetRequiredService<FrmMain>();
            Application.Run(formMain);
        }
    }
}