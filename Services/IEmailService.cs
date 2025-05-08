namespace HotelBookingApi.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string otp);
    }
}
