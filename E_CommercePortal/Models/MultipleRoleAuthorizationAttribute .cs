using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;

namespace E_CommercePortal.Models
{
    public class MultipleRoleAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public string[] Roles { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Retrieve the user's role from the session
            var sobj = context.HttpContext.Session.GetString("SessionObject");
            var userRole=(JsonConvert.DeserializeObject<SessionObject>(sobj)).Role;
            // Check if the user's role matches any of the required roles
            if (userRole == null || !Roles.Contains(userRole))
            {
                
                //context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
                context.Result = new RedirectToPageResult("../Error/");
            }
        }
    }
}
