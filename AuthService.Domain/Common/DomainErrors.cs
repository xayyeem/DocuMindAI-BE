using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Common
{
    public static class DomainErrors
    {
        public static class Email
        {
            public static readonly Error Empty =
                new("Email.Empty", "Email is required.");

            public static readonly Error Invalid =
                new("Email.Invalid", "Email format is invalid.");
        }
    }
}
