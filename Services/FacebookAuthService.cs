using HotelBookingApi.DTOs;
using Newtonsoft.Json.Linq;

namespace HotelBookingApi.Services
{
    public class FacebookAuthService : IFacebookAuthService
    {
        private readonly HttpClient _httpClient;

        public FacebookAuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<FacebookUserInfo> GetUserInfoAsync(string accessToken)
        {
            var url = $"https://graph.facebook.com/me?fields=id,name,email,picture.type(large)&access_token={accessToken}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Invalid Facebook token");

            var content = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(content);

            return new FacebookUserInfo
            {
                Id = data["id"]?.ToString(),
                Name = data["name"]?.ToString(),
                Email = data["email"]?.ToString(),
                Picture = data["picture"]?["data"]?["url"]?.ToString()
            };
        }
    }
}
