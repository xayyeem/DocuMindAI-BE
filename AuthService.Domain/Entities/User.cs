using AuthService.Domain.Common;
using AuthService.Domain.Enums;
using AuthService.Domain.ValueObjects;
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

        public Email Email { get; private set; } = null!;

        public string PasswordHash { get; private set; } = string.Empty;

        public UserRole Role { get; private set; }

        public bool IsActive { get; private set; }

        public ICollection<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();

        // Required by EF Core
        private User()
        {
        }

        private User(string firstName, string lastName, Email email, string passwordHash, UserRole role)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            IsActive = true;
        }

        public static Result<User> Create(string firstName, string lastName, Email email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return Result<User>.Failure(new Error("User.FirstName", "First name is required."));

            if (string.IsNullOrWhiteSpace(lastName))
                return Result<User>.Failure(new Error("User.LastName", "Last name is required."));

            if (string.IsNullOrWhiteSpace(passwordHash))
                return Result<User>.Failure(new Error("User.Password", "Password hash is required."));

            var emailResult = Email.Create(email);

            if (emailResult.IsFailure)
                return Result<User>.Failure(emailResult.Error);

            var user = new User(
                firstName,
                lastName,
                emailResult.Value,
                passwordHash,
                UserRole.User);

            return Result<User>.Success(user);
        }

        internal static User CreateAdmin(string firstName, string lastName, Email email, string passwordHash)
        {
            return new User(firstName,lastName,email,passwordHash,UserRole.Admin);
        }
    }
}
