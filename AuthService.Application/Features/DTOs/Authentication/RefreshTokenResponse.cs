using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Features.DTOs.Authentication
{
    public sealed class RefreshTokenResponse
    {
        public string AccessToken { get; init; } = string.Empty;

        public string RefreshToken { get; init; } = string.Empty;

        public DateTime ExpiresAt { get; init; }
    }
}
