using AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Features.Interfaces
{
    public interface IJwtTokenProvider
    {
        string GenerateAccessToken(User user);

        string GenerateRefreshToken();
    }
}
