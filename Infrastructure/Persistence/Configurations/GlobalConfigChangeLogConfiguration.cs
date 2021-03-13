using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class GlobalConfigChangeLogConfiguration : IEntityTypeConfiguration<GlobalConfigChangeLog>
    {
        public void Configure(EntityTypeBuilder<GlobalConfigChangeLog> builder)
        {
            builder.ToTable("tr_tb_global_config_change_log");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(t => t.ConfigCategory)
                .HasColumnName("config_category")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(t => t.Description)
                .HasColumnName("description")
                .IsRequired();

            builder.Property(t => t.ReferenceId)
                .HasColumnName("reference_id")
                .IsRequired();

            builder.Property(t => t.StateChange)
                .HasColumnName("state_change")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(t => t.GcaOid)
                .HasColumnName("gca_oid");

            builder.Property(t => t.SecurityServerId)
                .HasColumnName("security_server_id");

            builder.Property(t => t.UserId)
                .HasColumnName("user_id");

            builder.Property(t => t.UserName)
                .HasColumnName("user_name");

            builder.Property(t => t.LogTime)
                .HasColumnName("log_time");

            builder.Property(t => t.OMSerialId)
                .HasColumnName("om_serial_id");

            builder.Property(t => t.GlobalConfigJson)
                .HasColumnName("global_config_json");


            builder.HasIndex(x => new { x.ReferenceId, x.ConfigCategory, x.StateChange });
        }
    }
}
