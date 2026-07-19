using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; private set; }

        public string Token { get; private set; } = string.Empty;

        public DateTime ExpiresAt { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public bool IsRevoked { get; private set; }

        public Guid UserId { get; private set; }

        public User User { get; private set; } = null!;

        public RefreshToken()
        {
            
        }
        public RefreshToken(string token, Guid userId)
        {
            Id  = Guid.NewGuid();
            Token = token;
            ExpiresAt = DateTime.UtcNow.AddDays(7);
            CreatedAt = DateTime.UtcNow;
            UserId = userId;
        }

        public void Revoke()
        {
            IsRevoked = true;
        }
    }
}
