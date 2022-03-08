using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCProject.WebUI.Controllers
{
    public class BaseController : Controller
    {
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    
        public class CustomAuthorizeAttribute : AuthorizeAttribute
        {
           
            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    base.HandleUnauthorizedRequest(filterContext);
                }
                else
                {
                    var areaName = filterContext.RouteData.DataTokens["area"];
                    if (areaName == null)
                    {

                        filterContext.Result = new RedirectToRouteResult(new
                           RouteValueDictionary(new { controller = "Home", action = "Index", returnUrl = filterContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped) }));
                    }

                  
                    else if (areaName.Equals("Admin"))
                    {
                        filterContext.Result = new RedirectToRouteResult(new
                            RouteValueDictionary(new { controller = "Admin", action = "AdminLogin", area = "Admin" }));
                    }
                    else if (areaName.Equals("Supplier"))
                    {
                        filterContext.Result = new RedirectToRouteResult(new
                            RouteValueDictionary(new { controller = "Index", action = "Provider" }));
                    }
                     

                }
            }
        }
    }
}