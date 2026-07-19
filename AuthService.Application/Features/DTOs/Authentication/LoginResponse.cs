using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Features.DTOs.Authentication
{
    public sealed class LoginResponse
    {
        public string AccessToken { get; init; } = string.Empty;

        public DateTime ExpiresAt { get; init; }
        public string RefreshToken { get; set; }
    }
}
