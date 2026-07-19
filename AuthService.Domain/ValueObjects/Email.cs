using AuthService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AuthService.Domain.ValueObjects
{
    public sealed class Email : IEquatable<Email>
    {
        private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        private Email()
        {
            Value = string.Empty;
        }

        public static Result<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result<Email>.Failure(DomainErrors.Email.Empty);

            email = email.Trim().ToLowerInvariant();

            if (!EmailRegex.IsMatch(email))
                return Result<Email>.Failure(DomainErrors.Email.Invalid);

            return Result<Email>.Success(new Email(email));
        }

        public override string ToString()
        {
            return Value;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Email);
        }

        public bool Equals(Email? other)
        {
            return other is not null &&
                   Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator string(Email email)
        {
            return email.Value;
        }
    }
}
