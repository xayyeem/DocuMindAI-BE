using AuthService.Application.Features.DTOs;
using AuthService.Application.Features.DTOs.Authentication;
using AuthService.Application.Features.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers
{
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.RegisterAsync(request, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(new
                {
                    Code = result.Error.Code,
                    Message = result.Error.Message
                });
            }

            return CreatedAtAction(nameof(Register), new { id = result.Value.Id }, result.Value);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.LoginAsync(request, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }


        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                Message = "You are authenticated.",
                UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value,
                Email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value,
                Role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.RefreshTokenAsync(request, cancellationToken);

            if (result.IsFailure)
            {
                return Unauthorized(result.Error);
            }

            return Ok(result.Value);
        }
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(LogoutRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.LogoutAsync(request, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(new
            {
                Message = "Logged out successfully."
            });
        }
    }
}
