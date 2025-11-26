using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Data;
using Microsoft.Extensions.Configuration;
using Piggy_Admin;
using Demo_Layout;

namespace PhanQuyen
{
    static class Program
    {
        public static IServiceProvider Services { get; private set; }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // VÒNG LẶP CHÍNH: Giúp ứng dụng tự động mở lại Login khi Main Form đóng
            while (true)
            {
                using (var host = CreateHostBuilder().Build())
                {
                    Services = host.Services;

                    try
                    {
                        // 1. Mở Form Đăng Nhập
                        var loginForm = Services.GetRequiredService<LoginForm>();
                        Application.Run(loginForm);

                        // 2. Kiểm tra kết quả trả về từ LoginForm
                        if (loginForm.DialogResult == DialogResult.OK)
                        {
                            var userContext = Services.GetRequiredService<CurrentUserContext>();
                            Form mainForm = null;

                            if (userContext.IsAdmin)
                            {
                                mainForm = Services.GetRequiredService<FrmMainAdmin>();
                            }
                            else
                            {
                                mainForm = Services.GetRequiredService<FrmMain>();
                            }

                            // 3. Chạy Form Chính
                            if (mainForm != null)
                            {
                                Application.Run(mainForm);
                                // Khi mainForm đóng (do logout), vòng lặp while sẽ quay lại đầu -> Mở lại Login
                            }
                        }
                        else
                        {
                            // Nếu user tắt form login hoặc bấm Thoát -> Thoát hẳn vòng lặp
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
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

                    // ĐĂNG KÝ CÁC FORM (Tất cả là Transient để tạo mới mỗi lần)
                    services.AddTransient<LoginForm>();
                    services.AddTransient<DangKy>();

                    services.AddTransient<FrmMainAdmin>();
                    services.AddTransient<UserControlQuanLyThongBao>();
                    services.AddTransient<Piggy_Admin.TaoCapNhatThongBao>();
                    services.AddTransient<Piggy_Admin.FormTaiKhoan>();
                    services.AddTransient<Piggy_Admin.FormDoiMatKhau>();
                    services.AddTransient<Piggy_Admin.UserControlQuanLyTaiKhoan>();
                    services.AddTransient<FormTaoCapNhatTaiKhoan>();

                    services.AddTransient<FrmMain>();
                    services.AddTransient<UserControlNganSach>();

                    services.AddSingleton<CurrentUserContext>();
                    services.AddTransient<IEmailService, EmailService>();
                });
    }
}