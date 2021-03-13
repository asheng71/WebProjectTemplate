using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviors
{
    public class RequestContextBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RequestContext _ctx;
        private readonly DbConnectionDelegate _dbconn;

        public RequestContextBehavior(IHttpContextAccessor httpContextAccessor, RequestContext ctx, DbConnectionDelegate dbconn)
        {
            _httpContextAccessor = httpContextAccessor;
            _ctx = ctx;
            _dbconn = dbconn;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _ctx.Items["CurrentUser"] = _httpContextAccessor.HttpContext.User;
            
            if(_httpContextAccessor.HttpContext.Request.Path == "/Privacy")
            {
                _ctx.Items["IApplicationDbContext"] = _dbconn("source");
            } 
            else
            {
                _ctx.Items["IApplicationDbContext"] = _dbconn("default");
            }

            
            return next();
        }
    }
}
