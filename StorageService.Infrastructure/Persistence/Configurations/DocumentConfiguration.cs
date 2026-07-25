using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorageService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Infrastructure.Persistence.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Documents");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.OriginalFileName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.StoredFileName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.ContentType)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.FileSize)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();
            builder.Property(x => x.UpdatedAt)
                .IsRequired();
        }
    }
}
