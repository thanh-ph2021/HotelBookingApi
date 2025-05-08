using HotelBookingApi.Data;
using HotelBookingApi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace HotelBookingApi.Repositories
{
    public class OtpRepository : IOtpRepository
    {
        private readonly HotelBookingDbContext _context;
        public OtpRepository(HotelBookingDbContext context)
        {
            _context = context;
        }
        public async Task AddOtpAsync(OtpRequest otp)
        {
            _context.OtpRequests.Add(otp);
        }

        public async Task<OtpRequest?> GetValidOtpAsync(string email, string otpCode)
        {
            return await _context.OtpRequests
            .Where(x => x.Email == email && x.OtpCode == otpCode && x.IsUsed == false && x.ExpiresAt > DateTime.UtcNow)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
