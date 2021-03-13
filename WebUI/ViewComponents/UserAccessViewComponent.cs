using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebUI.ViewComponents
{
    [ViewComponent]
    public class UserAccessViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = new UserAccessComponentModel();
            if (this.User.Identity.IsAuthenticated)
            {
                model.IsAuthenticated = true;
                model.UserName = this.UserClaimsPrincipal.Claims.FirstOrDefault(c => c.Type == "trname")?.Value;
                model.UserOrgName = this.UserClaimsPrincipal.Claims.FirstOrDefault(c => c.Type == "trservername")?.Value;
            }
            
            return View(model);
        }
    }


    public class UserAccessComponentModel
    {
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }

        public string UserOrgName { get; set; }
    }
}
