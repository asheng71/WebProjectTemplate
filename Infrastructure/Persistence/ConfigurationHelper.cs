using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence
{
    public class ConfigurationHelper
    {
        public static void Configure<T>(EntityTypeBuilder<T> builder) where T : class
        {
            if (typeof(T).IsSubclassOf(typeof(AuditableEntity)))
            {
                
                builder.Property("CreatedBy")
                    .HasColumnName("created_by")
                    .HasMaxLength(100);

                builder.Property("CreatedAt")
                   .HasColumnName("created_at")
                   .IsRequired();

                builder.Property("UpdatedBy")
                   .HasColumnName("updated_by")
                   .HasMaxLength(100);

                builder.Property("UpdatedAt")
                   .HasColumnName("updated_at")
                   .IsRequired();
            }
        }
    }
}
