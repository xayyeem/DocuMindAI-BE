using AuthService.Application.Features.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Domain.ValueObjects;
using AuthService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await _context.Users.AddAsync(user, cancellationToken);
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var normalizedEmail = Email.Create(email);

            if (normalizedEmail.IsFailure)
                return null;

            return await _context.Users
                .FirstOrDefaultAsync(
                    x => x.Email == normalizedEmail.Value,
                    cancellationToken);
        }

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var normalizedEmail = Email.Create(email);

            if (normalizedEmail.IsFailure)
                return false;

            return await _context.Users
                .AnyAsync(
                    x => x.Email == normalizedEmail.Value,
                    cancellationToken);
        }

        public Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            _context.Users.Update(user);
            return Task.CompletedTask;
        }
        public async Task DeleteAsync(User user, CancellationToken cancellationToken = default)
        {
            _context.Users.Remove(user);

            await Task.CompletedTask;
        }
    }
}
