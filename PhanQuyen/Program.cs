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
                        var loginForm = Services.GetRequiredService<FrmDangNhap>();
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
                    services.AddTransient<FrmDangNhap>();
                    services.AddTransient<FrmDangKy>();

                    services.AddTransient<FrmMainAdmin>();
                    services.AddTransient<UserControlQuanLyThongBao>();
                    services.AddTransient<Piggy_Admin.FrmThemSuaThongBao>();
                    services.AddTransient<Piggy_Admin.FrmTaiKhoan>();
                    services.AddTransient<Piggy_Admin.FrmDoiMatKhau>();
                    services.AddTransient<Piggy_Admin.UserControlQuanLyTaiKhoan>();
                    services.AddTransient<FrmThemSuaTaiKhoan>();           
                    services.AddTransient<UserControlBaoCaoHeThong>();

                    services.AddTransient<FrmMain>();
                    services.AddTransient<UserControlNganSach>();

                    //mới thêm full program bên piggy bank 
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

                    services.AddSingleton<CurrentUserContext>();
                    services.AddTransient<IEmailService, EmailService>();
                });
    }
}