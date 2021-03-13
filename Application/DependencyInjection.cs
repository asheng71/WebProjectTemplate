using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Application.GlobalConfig;
using Application.Common.Behaviors;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestContextBehavior<,>));
            services.AddScoped<RequestContext>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(GlobalConfigChangeBehavior<,>));
            services.AddTransient<GlobalConfigChangeContext>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
