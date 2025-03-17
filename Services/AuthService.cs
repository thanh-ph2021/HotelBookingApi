using HotelBookingApi.Common;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories;
using Microsoft.AspNetCore.Identity;

namespace HotelBookingApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        private readonly ITokenService _tokenService;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(IAuthRepository repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
            _passwordHasher = new PasswordHasher<User>();
        }

        public Task<Result<bool>> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<AuthResponseDto>> Login(LoginDto loginDto)
        {
            var userExist = await _repository.GetUserByEmailAsync(loginDto.Email);
            if (userExist == null)
            {
                return Result<AuthResponseDto>.Failure("User not found.");
            }

            var user = new User
            {
                FullName = userExist.FullName,
                Email = userExist.Email,
                UserRoleId = userExist.UserRoleId,
                PasswordHash = userExist.PasswordHash
            };

            var verifyResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
            if (verifyResult != PasswordVerificationResult.Success)
            {
                return Result<AuthResponseDto>.Failure("Invalid credentials.");
            }

            var token = _tokenService.GenerateToken(userExist);
            var authResponse = new AuthResponseDto
            {
                Token = token.Token,
                Expiration = token.Expiration
            };

            return Result<AuthResponseDto>.Success(authResponse);
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<AuthResponseDto>> Register(RegisterDto registerDto)
        {
            var existingUser = await _repository.GetUserByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return Result<AuthResponseDto>.Failure("User already exists.");
            }

            var user = new User
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                UserRoleId = registerDto.UserRoleId
            };

            var passwordHasher = new PasswordHasher<User>();
            string hashedPassword = passwordHasher.HashPassword(user, registerDto.Password);

            user.PasswordHash = hashedPassword;

            var userCreated = await _repository.CreateUserAsync(user);

            var userGenerateToken = new UserProfileDto
            {
                FullName = userCreated.FullName,
                Email = userCreated.FullName,
                Id = userCreated.Id,
                UserRole = registerDto.UserRole
            };

            var token = _tokenService.GenerateToken(userGenerateToken);
            var authResponse = new AuthResponseDto
            {
                Token = token.Token,
                Expiration = token.Expiration,
                FullName = user.FullName,
                Email = user.Email
            };

            return Result<AuthResponseDto>.Success(authResponse);
        }

        public Task<Result<string>> RequestOtp(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Task<Result<AuthResponseDto>> VerifyOtp(VerifyOtpDto otpDto)
        {
            throw new NotImplementedException();
        }
    }
}
