using AuthService.Application.Features.DTOs;
using AuthService.Application.Features.DTOs.Authentication;
using AuthService.Application.Features.Interfaces;
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
    }
}
