using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence
{
    //public class ApplicationDbContext : IdentityDbContext
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ReplaceService<IMigrationsAssembly, ContextAwareMigrationsAssembly>();
        }


        public DbSet<GlobalConfigChangeLog> GlobalConfigChangeLogs { get; set; }

       
        public DbSet<ApplicationLog> ApplicationLogs { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var now = DateTime.Now;
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.CreatedAt = now;
                        entry.Entity.UpdatedAt = now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = _currentUserService.UserId;
                        entry.Entity.UpdatedAt = now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }

}
