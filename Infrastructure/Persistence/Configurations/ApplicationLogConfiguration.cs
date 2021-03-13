using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class ApplicationLogConfiguration : IEntityTypeConfiguration<ApplicationLog>
    {
        public void Configure(EntityTypeBuilder<ApplicationLog> builder)
        {
            builder.ToTable("ss_tb_application_log");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("id");

            builder.Property(t => t.Message)
                .HasColumnName("message");

            builder.Property(t => t.LogTime)
                .HasColumnName("log_time");

            builder.Property(t => t.Description)
                .HasColumnName("description");

            ConfigurationHelper.Configure(builder);
        }
    }
}
