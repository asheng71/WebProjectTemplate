using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{

    public delegate IApplicationDbContext DbConnectionDelegate(string name);

    public interface IApplicationDbContext
    {
        

        public DbSet<GlobalConfigChangeLog> GlobalConfigChangeLogs { get; set; }

        
        public DbSet<ApplicationLog> ApplicationLogs { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        EntityEntry Entry([NotNullAttribute] object entity);
    }

    
}
