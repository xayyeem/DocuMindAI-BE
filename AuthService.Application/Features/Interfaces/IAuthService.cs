using AuthService.Application.Features.DTOs;
using AuthService.Application.Features.DTOs.Authentication;
using AuthService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Features.Interfaces
{
    public interface IAuthService
    {
        Task<Result<RegisterResponse>> RegisterAsync(RegisterRequest request,
            CancellationToken cancellationToken = default);
        Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    }
}
