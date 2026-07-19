using AuthService.Domain.Entities;
using AuthService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Persistence.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .IsRequired();

            builder.Property(x => x.Role)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasConversion(
                    email => email.Value,
                    value => Email.Create(value).Value);
        }
    }
}
