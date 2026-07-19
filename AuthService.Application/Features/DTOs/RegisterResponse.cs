using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Features.DTOs
{
    public sealed class RegisterResponse
    {
        public Guid Id { get; init; }

        public string FullName { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;
    }
}
