using Dapper;
using HotelBookingApi.Data;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models;
using HotelBookingApi.Models.Requests;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace HotelBookingApi.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly HotelBookingDbContext _context;
        private readonly IConfiguration _configuration;
        public PaymentRepository(HotelBookingDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<PaymentStatusDto> GetPaymentStatusAsync(Guid bookingId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@BookingId", bookingId, DbType.Guid);

                var result = await connection.QueryFirstOrDefaultAsync<PaymentStatusDto>(
                    "PP0001",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
        }

        public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request)
        {
            var payment = new Payment
            {
                BookingId = request.BookingId,
                UserId = request.UserId,
                Amount = request.Amount,
                PaymentMethodId = request.PaymentMethodId,
                StatusId = Guid.Parse("5BA86915-5ECA-463D-B9B5-3D9A1D7360D1"),
                PaidAt = DateTime.UtcNow
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return new PaymentResult
            {
                Success = true,
                Message = "Payment processed successfully",
                BookingId = request.BookingId
            };
        }
    }
}
