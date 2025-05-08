
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using static System.Net.WebRequestMethods;

namespace HotelBookingApi.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmailAsync(string toEmail, string otp)
        {
            var apiKey = _config["SendGrid:ApiKey"];
            var senderEmail = _config["SendGrid:SenderEmail"];
            var senderName = _config["SendGrid:SenderName"];
            var templateId = _config["SendGrid:TemplateId"];  // Lấy Template ID từ cấu hình

            // Kiểm tra xem API Key có hợp lệ không
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("API Key is missing from configuration.");
            }

            // Kiểm tra xem TemplateId có hợp lệ không
            if (string.IsNullOrEmpty(templateId))
            {
                throw new Exception("Template ID is missing from configuration.");
            }

            // Tạo client SendGrid
            var client = new SendGridClient(apiKey);

            // Tạo đối tượng từ (người gửi)
            var from = new EmailAddress(senderEmail, senderName);

            // Tạo đối tượng đến (người nhận)
            var to = new EmailAddress(toEmail);

            // Tạo thông điệp email với Template ID
            var msg = new SendGridMessage()
            {
                From = from,
                TemplateId = templateId
            };

            // Thêm các dynamic template data vào
            msg.AddTo(to);
            msg.SetTemplateData(new
            {
                email = toEmail,
                otp = otp
            });

            // Gửi email và nhận phản hồi
            var response = await client.SendEmailAsync(msg);

            // Kiểm tra phản hồi và thông báo lỗi nếu có
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Body.ReadAsStringAsync();
                throw new Exception($"SendGrid failed with status code: {response.StatusCode}. Error: {errorMessage}");
            }
        }
    }
}
