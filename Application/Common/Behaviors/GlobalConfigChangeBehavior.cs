using MediatR;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Domain.Entities;
using Application.Common.Interfaces;
using Domain.Enumeration;
using Microsoft.Extensions.Configuration;

namespace Application.GlobalConfig
{
    public class GlobalConfigChangeBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private IServiceProvider _serviceProvider;
        private HttpClient _httpClient;
        private IHttpContextAccessor _httpContext;
        private IMediator _mediator;
        private readonly IConfiguration _config;
        private readonly IApplicationDbContext _context;


        public GlobalConfigChangeBehavior(IServiceProvider serviceProvider, IHttpClientFactory client, IHttpContextAccessor httpcontext, IMediator mediator, IConfiguration config, IApplicationDbContext context)
        {
            _serviceProvider = serviceProvider;
            _httpClient = client.CreateClient("HttpClientName");
            _httpContext = httpcontext;
            _mediator = mediator;
            _config = config;
            _context = context;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is IGlobalConfigChange)
            {
                var changeRequest = (IGlobalConfigChange)request;

                // Context property injection
                var configContext = (GlobalConfigChangeContext)_serviceProvider.GetService(typeof(GlobalConfigChangeContext));
                changeRequest.ConfigContext = configContext;

                var response = await next();

                bool hasChanged = changeRequest.ConfigContext.Result != null;
                if(hasChanged)
                {
                    ChangeIntent intent = changeRequest.ConfigContext.Intent;
                    ChangedResult changed = changeRequest.ConfigContext.Result;
                    
                    if(intent != null && changed != null && !IsBySsSystem(changed.UserId))
                    {
                        var dbContext = (IApplicationDbContext)_serviceProvider.GetService(typeof(IApplicationDbContext));

                        GlobalConfigChangeLog log = BuildLog(intent, changed);                        
                        dbContext.GlobalConfigChangeLogs.Add(log);                        
                        await dbContext.SaveChangesAsync(cancellationToken);
                                              

                    }
                }

                return response;
            }
            else
            {
                return await next();
            }

            

        }

        /// <summary>
        /// 安控伺服器排程造成的 table 同步, 不能算組態異動紀錄
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool IsBySsSystem(string userId)
        {
            if(string.IsNullOrWhiteSpace(userId))
            {
                return false;
            }

            return userId.Trim() == "system";
        }
        


        
        private GlobalConfigChangeLog BuildLog(ChangeIntent intent, ChangedResult result)
        {
            var log = new GlobalConfigChangeLog();

            log.ReferenceId = intent.ReferenceId;
            log.ConfigCategory = intent.ConfigCategory;
            log.Description = intent.ChangeDescription;
            log.UserId = result.UserId;
            log.UserName = result.UserName;
            log.GcaOid = result.OrgUnitOid;
            log.SecurityServerId = result.SecurityServerId;
            log.LogTime = DateTime.Now;
            log.StateChange = result.State;

            return log;
        }

    }
}
