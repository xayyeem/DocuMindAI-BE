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
        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        public static Email Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.");

            email = email.Trim().ToLowerInvariant();

            if (!IsValid(email))
                throw new ArgumentException("Invalid email address.");

            return new Email(email);
        }

        private static bool IsValid(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
        }

        public override string ToString() => Value;

        public override bool Equals(object? obj)
        {
            return Equals(obj as Email);
        }

        public bool Equals(Email? other)
        {
            return other is not null && Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator string(Email email)
            => email.Value;
    }
}
