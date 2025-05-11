using Google.Apis.Auth;
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
        private readonly IFacebookAuthService _facebookAuthService;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(IAuthRepository repository, ITokenService tokenService, IFacebookAuthService facebookAuthService)
        {
            _repository = repository;
            _tokenService = tokenService;
            _passwordHasher = new PasswordHasher<User>();
            _facebookAuthService = facebookAuthService;
        }

        public Task<Result<bool>> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            throw new NotImplementedException();
        }

        private async Task<GoogleJsonWebSignature.Payload> ValidateGoogleTokenAsync(string idToken)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);
            return payload;
        }

        public async Task<Result<AuthResponseDto>> GoogleSignInAsync(string idToken)
        {
            try
            {
                var payload = await ValidateGoogleTokenAsync(idToken);

                var userExist = await _repository.GetUserByEmailAsync(payload.Email);
                var token = new AuthResponseDto();
                if (userExist == null)
                {
                    var user = await _repository.CreateUserAsync(new User
                    {
                        Email = payload.Email,
                        PasswordHash = "",
                        UserRoleId = new Guid("7E4E81D0-8163-4EA8-96C6-86B042B9D050"),
                        FullName = payload.Name,
                        Avatar = payload.Picture,
                    });

                    userExist = new UserProfileDto
                    {
                        FullName = user.FullName,
                        Email = user.Email,
                        Id = user.Id,
                        UserRole = "Customer",
                        UserRoleId = user.UserRoleId,
                        Avatar = payload.Picture
                    };
                }

                token = _tokenService.GenerateToken(userExist);

                var authResponse = new AuthResponseDto
                {
                    Token = token.Token,
                    Expiration = token.Expiration,
                    userProfile = new UserProfileDto2
                    {
                        Id = userExist.Id,
                        Email = userExist.Email,
                        FullName = userExist.FullName,
                        Phone = userExist.Phone ?? "",
                        UserRole = userExist.UserRole,
                        UserRoleId = userExist.UserRoleId,
                        Avatar = userExist.Avatar,
                    }
                };

                return Result<AuthResponseDto>.Success(authResponse);
            }
            catch (Exception ex)
            {
                return Result<AuthResponseDto>.Failure("Google sign-in failed.");
            }
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
                Expiration = token.Expiration,
                userProfile = new UserProfileDto2
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    Phone = user.Phone ?? "",
                    UserRole = userExist.UserRole,
                    UserRoleId = user.UserRoleId
                }
            };

            return Result<AuthResponseDto>.Success(authResponse);
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
                userProfile = new UserProfileDto2
                {
                    Id = userCreated.Id,
                    Email = userCreated.Email,
                    FullName = userCreated.FullName,
                    Phone = userCreated.Phone ?? "",
                    UserRole = registerDto.UserRole,
                    UserRoleId = userCreated.UserRoleId
                }
            };

            return Result<AuthResponseDto>.Success(authResponse);
        }

        public async Task<Result<AuthResponseDto>> FacebookSignInAsync(string accessToken)
        {
            try
            {
                var fbUser = await _facebookAuthService.GetUserInfoAsync(accessToken);

                var userExist = await _repository.GetUserByFacebookIdAsync(fbUser.Id);
                var token = new AuthResponseDto();

                if (userExist == null)
                {
                    var email = fbUser.Email ?? $"{fbUser.Id}@facebook.local";
                    var user = await _repository.CreateUserAsync(new User
                    {
                        Email = email,
                        PasswordHash = "",
                        UserRoleId = new Guid("7E4E81D0-8163-4EA8-96C6-86B042B9D050"),
                        FullName = fbUser.Name,
                        Avatar = fbUser.Picture,
                        FacebookId = fbUser.Id
                    });

                    userExist = new UserProfileDto
                    {
                        FullName = user.FullName,
                        Email = user.Email,
                        Id = user.Id,
                        UserRole = "Customer",
                        UserRoleId = user.UserRoleId,
                        Avatar = user.Avatar,
                        FacebookId = user.FacebookId
                    };
                }

                token = _tokenService.GenerateToken(userExist);

                var authResponse = new AuthResponseDto
                {
                    Token = token.Token,
                    Expiration = token.Expiration,
                    userProfile = new UserProfileDto2
                    {
                        Id = userExist.Id,
                        Email = userExist.Email,
                        FullName = userExist.FullName,
                        Phone = userExist.Phone ?? "",
                        UserRole = userExist.UserRole,
                        UserRoleId = userExist.UserRoleId,
                        Avatar = userExist.Avatar,
                        FacebookId = userExist.FacebookId,
                    }
                };

                return Result<AuthResponseDto>.Success(authResponse);
            }
            catch (Exception ex)
            {
                return Result<AuthResponseDto>.Failure("Facebook sign-in failed.");
            }
        }
    }
}
