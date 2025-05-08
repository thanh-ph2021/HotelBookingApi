using System.Security.Cryptography;
using System.Text;
using static System.Net.WebRequestMethods;

namespace HotelBookingApi.Common
{
    public class OtpUtils
    {
        public static string GenerateOtp()
        {
            return new Random().Next(1000, 10000).ToString();
        }
        public static string HashOtp(string otp)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(otp);
            var hashBytes = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
