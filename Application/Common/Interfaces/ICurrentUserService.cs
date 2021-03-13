using Domain.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        public bool IsAuthenticated { get; }

        public string UserId { get; }

        public string UserName { get; }

        public string OrgOid { get; }

        public string OrgName { get; }

        public string ServerName { get; }

        public string Mobile { get; }

        public string Email { get; }

        public string Token { get; }
        public IEnumerable<TRoadManagementRoles> Roles { get; }
    }
}
