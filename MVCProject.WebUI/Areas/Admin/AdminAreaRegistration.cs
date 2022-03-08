using System.Web.Mvc;

namespace MVCProject.WebUI.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "EditProvider",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Provider", action = "EditProvider", id = UrlParameter.Optional }
            );
            context.MapRoute(
              "DeleteProvider",
              "Admin/{controller}/{action}/{id}",
              new { controller = "Provider", action = "DeleteProvider", id = UrlParameter.Optional }
          );
            context.MapRoute(
  "EditOrder",
  "Admin/{controller}/{action}/{id}",
  new { controller = "Order", action = "EditOrder", id = UrlParameter.Optional }
);

            context.MapRoute(
"DeleteOrder",
"Admin/{controller}/{action}/{id}",
new { controller = "Order", action = "DeleteOrder", id = UrlParameter.Optional }
);

            context.MapRoute(
name: "GetReport",
url: "Admin/{controller}/{action}/{id}",
defaults: new { controller = "Accounting", action = "GetReport", id = UrlParameter.Optional }
);
             context.MapRoute(
name: "GetProducts",
url: "Admin/{controller}/{action}/{id}",
defaults: new { controller = "Accounting", action = "GetProducts", id = UrlParameter.Optional }
);
        }
    }
}