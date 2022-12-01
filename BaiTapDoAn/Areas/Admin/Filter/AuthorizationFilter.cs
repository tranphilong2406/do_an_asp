using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;

namespace BaiTapDoAn.Areas.Admin.Filter
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            int? role = context.HttpContext.Session.GetInt32("role");

            
                if (role == 0)
                {
                    context.Result = new RedirectResult("/Admin/Denied");
                }
            
        }
    }
}
