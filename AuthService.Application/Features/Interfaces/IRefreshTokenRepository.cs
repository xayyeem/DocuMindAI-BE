using AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Features.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken refreshtoken, CancellationToken cancellationToken = default);
        Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
    }
}
