using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Application;
using Application.Common.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using WebUI.Services;

namespace WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration, Environment);
            services.AddApplication(Configuration);

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();

            services.AddRazorPages()
                .AddRazorPagesOptions(option =>
                {
                })
                .AddRazorRuntimeCompilation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TRA SideSlope API",
                    Description = "TRA SideSlope API",
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });
            services
                .AddHttpClient("HttpClientName", client => 
                {
                    // code to configure headers etc..
                })
                .ConfigurePrimaryHttpMessageHandler(() => 
                {
                    var handler = new HttpClientHandler();
                    if (Environment.IsDevelopment())
                    {
                        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    }
                    return handler;
                });

            services.AddCors(options =>
            {
                options.AddPolicy("cs_cors", builder =>
                {
                    builder
                        //.WithOrigins(
                        //    "https://sso.troad.gov.tw",
                        //    "https://em.troad.gov.tw",
                        //    "https://om.troad.gov.tw",
                        //    "https://qp.troad.gov.tw")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithExposedHeaders("Authorization");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
