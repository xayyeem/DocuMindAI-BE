using AuthService.Application.Features.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Repositories
{
    public sealed class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RefreshToken refreshToken,CancellationToken cancellationToken = default)
        {
            await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token,CancellationToken cancellationToken = default)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == token, cancellationToken);
        }
    }
}
