using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using System;
using Data;
using Microsoft.Extensions.Configuration;

namespace PhanQuyen
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1. Khởi tạo Host
            using (var host = CreateHostBuilder().Build())
            {
                var serviceProvider = host.Services;

                try
                {
                    // Lấy Form Đăng nhập từ DI
                    var loginForm = serviceProvider.GetRequiredService<PhanQuyen.LoginForm>();

                    // SỬA LỖI LƯỢNG CHÍNH: Thay vì Application.Run(loginForm), chúng ta dùng ApplicationContext
                    // và gán Form Đăng nhập là Form đầu tiên trong Context.
                    Application.Run(new ApplicationContext(loginForm));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khởi tạo ứng dụng: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var connectionString = hostContext.Configuration.GetConnectionString("QLTCCNConnection");

                    services.AddDbContextFactory<QLTCCNContext>(options =>
                    {
                        options.UseSqlServer(connectionString);
                    });

                    // Đăng ký tất cả các Form để DI có thể tạo
                    services.AddTransient<LoginForm>();
                    services.AddTransient<Piggy_Admin.FrmMainAdmin>();
                    services.AddTransient<Demo_Layout.FrmMain>();
                    services.AddTransient<Piggy_Admin.UserControlQuanLyThongBao>();
                    services.AddTransient<Piggy_Admin.TaoCapNhatThongBao>();

                });
    }
}