using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Windows.Forms;

namespace Piggy_Admin
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            var builder = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var connectionString = hostContext.Configuration.GetConnectionString("QLTCCNConnection");

                    // 1. Đăng ký DbContextFactory (Vòng đời an toàn)
                    services.AddDbContextFactory<QLTCCNContext>(options =>
                        options.UseSqlServer(connectionString));

                    // 2. Đăng ký Form chính và UserControl/Form phụ
                    services.AddTransient<FrmMainAdmin>();
                    services.AddTransient<FormTaiKhoan>();
                    services.AddTransient<UserControlBaoCaoHeThong>();
                    services.AddTransient<UserControlQuanLyTaiKhoan>();
                    services.AddTransient<UserControlQuanLyThongBao>();
                    services.AddTransient<TaoCapNhatThongBao>();
                    services.AddTransient<FormTaiKhoan>();

                })
                .Build();

            ApplicationConfiguration.Initialize();

            // 3. LẤY FORM TỪ DI CONTAINER để gọi Constructor có tham số
            var formMain = builder.Services.GetRequiredService<FrmMainAdmin>();

            Application.Run(formMain);
        }
    }
}