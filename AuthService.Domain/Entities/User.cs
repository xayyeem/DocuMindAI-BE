using AuthService.Domain.Common;
using AuthService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Entities
{
    public class User : AuditableEntity
    {
        public string FirstName { get; private set; } = string.Empty;

        public string LastName { get; private set; } = string.Empty;

        public string Email { get; private set; } = string.Empty;

        public string PasswordHash { get; private set; } = string.Empty;

        public UserRole Role { get; private set; }

        public bool IsActive { get; private set; }

        // Required by EF Core
        private User()
        {
        }

        private User(string firstName, string lastName, string email, string passwordHash, UserRole role)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email.Trim().ToLowerInvariant();
            PasswordHash = passwordHash;
            Role = role;
            IsActive = true;
        }

        public static User Create(string firstName, string lastName, string email, string passwordHash)
        {
            return new User(firstName, lastName, email, passwordHash, UserRole.User);
        }

        internal static User CreateAdmin(string firstName, string lastName, string email, string passwordHash)
        {
            return new User(firstName,lastName,email,passwordHash,UserRole.Admin);
        }
    }
}
