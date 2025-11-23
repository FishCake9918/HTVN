using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PhanQuyen
{
    public interface IEmailService
    {
        Task<bool> SendRegistrationSuccessEmail(string recipientEmail, string username);
    }

    public class EmailService : IEmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _senderEmail;
        private readonly string _senderPassword;

        // Constructor: Lấy cấu hình từ appsettings.json qua IConfiguration
        public EmailService(IConfiguration configuration)
        {
            // Đọc các giá trị cấu hình từ phần "EmailSettings"
            _smtpHost = configuration["EmailSettings:SmtpHost"] ?? "smtp.gmail.com";

            // =========================================================================
            // ✅ FIX LỖI: Đọc Port an toàn hơn
            // =========================================================================
            string portString = configuration["EmailSettings:SmtpPort"];
            if (!int.TryParse(portString, out _smtpPort))
            {
                _smtpPort = 587; // Giá trị mặc định an toàn nếu Parse thất bại
            }
            // =========================================================================

            _senderEmail = configuration["EmailSettings:SenderEmail"] ?? "Vyle.31231022150@st.ueh.edu.vn";
            _senderPassword = configuration["EmailSettings:SenderPassword"] ?? "dfngglzkwhcluvon";

            // Thêm kiểm tra cuối cùng (Chỉ để debug)
            if (string.IsNullOrWhiteSpace(_senderPassword) || _smtpPort == 0)
            {
                Console.WriteLine("LỖI CẤU HÌNH: Email Host hoặc Port bị thiếu/không hợp lệ!");
            }
        }

        public async Task<bool> SendRegistrationSuccessEmail(string recipientEmail, string username)
        {
            try
            {
                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(_senderEmail, "Hệ thống Piggy Bank");
                    message.To.Add(recipientEmail);
                    message.Subject = "Chúc mừng! Đăng ký tài khoản Piggy Bank thành công";

                    message.Body = $@"
                        Xin chào {username},<br><br>
                        Tài khoản của bạn đã được đăng ký thành công tại hệ thống Piggy Bank.<br>
                        Thông tin tài khoản:<br>
                        - Tên đăng nhập (Email): <b>{recipientEmail}</b><br><br>
                        Bạn có thể đăng nhập ngay để bắt đầu quản lý tài chính cá nhân.<br><br>
                        Trân trọng,<br>
                        Đội ngũ Piggy Bank.
                    ";
                    message.IsBodyHtml = true;

                    using (var smtpClient = new SmtpClient(_smtpHost, _smtpPort))
                    {
                        // Kiểm tra lại nếu mật khẩu hoặc port không đúng
                        if (string.IsNullOrWhiteSpace(_senderPassword) || _smtpPort == 0)
                        {
                            throw new InvalidOperationException("Cấu hình Email Service không đầy đủ (Thiếu mật khẩu hoặc Port).");
                        }

                        smtpClient.Credentials = new NetworkCredential(_senderEmail, _senderPassword);
                        smtpClient.EnableSsl = true;
                        smtpClient.Timeout = 20000;

                        await smtpClient.SendMailAsync(message);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // In ra thông báo lỗi chi tiết để bạn kiểm tra cửa sổ Output/Console
                Console.WriteLine($"Lỗi gửi email đến {recipientEmail}: {ex.Message}");
                // Không hiển thị MessageBox, chỉ trả về false để hiển thị cảnh báo trên Form Đăng ký
                return false;
            }
        }
    }
}