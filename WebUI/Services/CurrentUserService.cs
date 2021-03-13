using Application.Common.Interfaces;
using Domain.Enumeration;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            IsAuthenticated = httpContextAccessor.HttpContext?.User.Identity.IsAuthenticated ?? false;

            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("sub");
            UserName = httpContextAccessor.HttpContext?.User?.FindFirstValue("trname");
            OrgOid = httpContextAccessor.HttpContext?.User?.FindFirstValue("orgoid");
            OrgName = httpContextAccessor.HttpContext?.User?.FindFirstValue("orgname");
            ServerName = httpContextAccessor.HttpContext?.User?.FindFirstValue("trservername");            
            Mobile = httpContextAccessor.HttpContext?.User?.FindFirstValue("trmobile");
            Email = httpContextAccessor.HttpContext?.User?.FindFirstValue("tremail");

            Token = httpContextAccessor.HttpContext?.Request.Cookies["troadtoken"];

            var roles = new List<TRoadManagementRoles>();
            if(httpContextAccessor.HttpContext?.User?.IsInRole("CS_System_MA") ?? false)
            {
                roles.Add(TRoadManagementRoles.CS_System_MA);
            }

            if (httpContextAccessor.HttpContext?.User?.IsInRole("CS_EMOM_User") ?? false)
            {
                roles.Add(TRoadManagementRoles.CS_EMOM_User);
            }

            if (httpContextAccessor.HttpContext?.User?.IsInRole("CS_QP_User") ?? false)
            {
                roles.Add(TRoadManagementRoles.CS_QP_User);
            }
            this.Roles = roles;
        }

        public IEnumerable<TRoadManagementRoles> Roles { get; private set; }

        public bool IsAuthenticated { get; }

        public string UserId { get; }

        public string UserName { get; }

        public string OrgOid { get; }

        public string OrgName { get; }

        public string ServerName { get; }

        public string Mobile { get; }

        public string Email { get; }

        public string Token { get; }
    }
}
