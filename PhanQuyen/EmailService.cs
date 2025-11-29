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

        public EmailService(IConfiguration configuration)
        {
            // 1. Đọc cấu hình từ file appsettings.json
            _smtpHost = configuration["EmailSettings:SmtpHost"] ?? "smtp.gmail.com";

            // Xử lý đọc Port an toàn (tránh lỗi nếu config sai)
            string portString = configuration["EmailSettings:SmtpPort"];
            if (!int.TryParse(portString, out _smtpPort))
            {
                _smtpPort = 587; // Mặc định
            }

            _senderEmail = configuration["EmailSettings:SenderEmail"] ?? "Vyle.31231022150@st.ueh.edu.vn"; //Email gửi đi
            _senderPassword = configuration["EmailSettings:SenderPassword"] ?? "dfngglzkwhcluvon"; //Mật khẩu ứng dụng 

            // Kiểm tra debug 
            if (string.IsNullOrWhiteSpace(_senderPassword) || _smtpPort == 0)
            {
                Console.WriteLine("LỖI CẤU HÌNH: Email Host hoặc Port bị thiếu/không hợp lệ!");
            }
        }

        // 2. Hàm gửi email xác nhận đăng ký
        public async Task<bool> SendRegistrationSuccessEmail(string recipientEmail, string username)
        {
            try
            {
                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(_senderEmail, "Hệ thống Piggy Bank");
                    message.To.Add(recipientEmail);
                    message.Subject = "Chúc mừng! Đăng ký tài khoản Piggy Bank thành công";

                    // Nội dung email định dạng HTML
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
                        // Kiểm tra thông tin xác thực trước khi gửi
                        if (string.IsNullOrWhiteSpace(_senderPassword) || _smtpPort == 0)
                        {
                            throw new InvalidOperationException("Cấu hình Email Service không đầy đủ.");
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
                // Ghi log lỗi ra màn hình để debug
                Console.WriteLine($"Lỗi gửi email đến {recipientEmail}: {ex.Message}");
                return false;
            }
        }
    }
}