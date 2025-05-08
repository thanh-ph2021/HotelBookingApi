
using HotelBookingApi.Common;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories;
using System.Runtime.CompilerServices;

namespace HotelBookingApi.Services
{
    public class OtpService : IOtpService
    {
        private readonly IOtpRepository _otpRepo;
        private readonly IEmailService _emailService;
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;
        public OtpService(IOtpRepository otpRepo, IEmailService emailService, IAuthRepository authRepository, ITokenService tokenService)
        {
            _otpRepo = otpRepo;
            _emailService = emailService;
            _authRepository = authRepository;
            _tokenService = tokenService;
        }

        public async Task<Result<string>> GenerateOtpAsync(string email)
        {
            try
            {
                var otp = OtpUtils.GenerateOtp();
                var hashed = OtpUtils.HashOtp(otp);

                var otpEntity = new OtpRequest
                {
                    Email = email.Trim(),
                    OtpCode = hashed,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(5)
                };

                await _otpRepo.AddOtpAsync(otpEntity);
                await _otpRepo.SaveChangesAsync();

                await _emailService.SendEmailAsync(
                    toEmail: email,
                    otp
                );
                return Result<string>.Success("OTP has been sent successfully.");
            } 
            catch(Exception ex)
            {
                return Result<string>.Failure(ex.Message);
            }
        }

        public async Task<Result<AuthResponseDto>> VerifyOtpAndLoginAsync(string email, string otp)
        {
            var hashedOtp = OtpUtils.HashOtp(otp);
            var record = await _otpRepo.GetValidOtpAsync(email.Trim(), hashedOtp);

            if (record == null) 
            {
                return Result<AuthResponseDto>.Failure("OTP has expired. Please request a new one.");
            }

            record.IsUsed = true;
            await _otpRepo.SaveChangesAsync();

            var user = await _authRepository.GetUserByEmailAsync(email);
            var token = new AuthResponseDto();
            if (user == null)
            {
                var newUser = new User
                {
                    Email = email.Trim(),
                    FullName = email,
                    PasswordHash = "",
                    UserRoleId = new Guid("7E4E81D0-8163-4EA8-96C6-86B042B9D050")
                };
                await _authRepository.CreateUserAsync(newUser);
                user = new UserProfileDto
                {
                    FullName = newUser.FullName,
                    Email = newUser.Email,
                    Id = newUser.Id,
                    UserRole = "Customer"
                };
                token = _tokenService.GenerateToken(user);
            } 
            else
            {
                token = _tokenService.GenerateToken(user);
            }

            var authResponse = new AuthResponseDto
            {
                Token = token.Token,
                Expiration = token.Expiration,
                userProfile = new UserProfileDto2
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    Phone = user.Phone ?? "",
                    UserRole = user.UserRole,
                    UserRoleId = user.UserRoleId
                }
            };

            return Result<AuthResponseDto>.Success(authResponse);
        }
    }
}
