using AuthService.Application.Features.DTOs;
using AuthService.Application.Features.DTOs.Authentication;
using AuthService.Application.Features.Interfaces;
using AuthService.Domain.Common;
using AuthService.Domain.Entities;
using AuthService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Services
{
    public class AuthService: IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenProvider _jwtTokenProvider;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtTokenProvider jwtTokenProvider, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _jwtTokenProvider = jwtTokenProvider;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (user is null)
            {
                return Result<LoginResponse>.Failure(new Error("Authentication.InvalidCredentials","Invalid email or password."));
            }

            var isPasswordValid = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                return Result<LoginResponse>.Failure(new Error("Authentication.InvalidCredentials","Invalid email or password."));
            }

            var accessToken = _jwtTokenProvider.GenerateAccessToken(user);
            var refreshToken = _jwtTokenProvider.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken(refreshToken, user.Id);

            await _refreshTokenRepository.AddAsync(refreshTokenEntity, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(15)
            };

            return Result<LoginResponse>.Success(response);
        }

        public async Task<Result<RegisterResponse>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            if(await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
            {
                return Result<RegisterResponse>.Failure(new Error("User.EmailExists", "Email already exists."));
            }
            var emailResult = Email.Create(request.Email);
            if(emailResult.IsFailure)
            {
                return Result<RegisterResponse>.Failure(emailResult.Error);
            }
            var hashedPassword = _passwordHasher.HashPassword(request.Password);
            var userResult = User.Create(request.FirstName, request.LastName, emailResult.Value, hashedPassword);
            if(userResult.IsFailure)
            {
                return Result<RegisterResponse>.Failure(userResult.Error);
            }

            await _userRepository.AddAsync(userResult.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<RegisterResponse>.Success(new RegisterResponse
            {
                Id = userResult.Value.Id,
                FullName = $"{userResult.Value.FirstName} {userResult.Value.LastName}",
                Email = userResult.Value.Email.Value
            });
        }
    }
}
