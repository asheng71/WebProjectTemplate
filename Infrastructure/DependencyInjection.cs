using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace Infrastructure
{
    public static class DependencyInjection
    {
        //public delegate IApplicationDbContext DbConnectionDelegate(string name);

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            string connStr = configuration.GetConnectionString("DefaultConnection");
            string slopeSourceStr = configuration.GetConnectionString("SlopeSourceConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connStr,
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.AddDbContext<SlopeSourceDbContext>(options =>
                    options.UseSqlServer(slopeSourceStr,
                    b => b.MigrationsAssembly(typeof(SlopeSourceDbContext).Assembly.FullName)));

            //services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IApplicationDbContext>(provider => 
            {
                var connDelegate = provider.GetService<DbConnectionDelegate>();
                return connDelegate("slope");                
            });

            services.AddScoped<DbConnectionDelegate>(provider => name => 
            {
                if(name == "source")
                {
                    return provider.GetService<SlopeSourceDbContext>();
                }
                else if(name == "slope")
                {
                    return provider.GetService<ApplicationDbContext>();
                }

                return provider.GetService<ApplicationDbContext>();
            });

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();


            return services;
        }
    }
}





