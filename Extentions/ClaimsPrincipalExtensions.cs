using System.Security.Claims;

namespace HotelBookingApi.Extentions
{
    
    public static class ClaimsPrincipalExtensions
    {
        private static readonly String DefaultRole = "Customer";

        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? user.FindFirst("id")?.Value;

            return Guid.TryParse(userIdClaim, out var userId) ? userId : Guid.Empty;
        }

        public static string GetUserRole(this ClaimsPrincipal user)
        {
            var userRoleClaim = user.FindFirst(ClaimTypes.Role)?.Value;

            return !string.IsNullOrEmpty(userRoleClaim) ? userRoleClaim : "Customer";
        }
    }
}
