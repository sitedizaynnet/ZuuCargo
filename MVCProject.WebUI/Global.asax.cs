using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCProject.DAL;

using MVCProject.Entities;

namespace MVCProject.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

           
        }

        //void Application_BeginRequest(Object sender, EventArgs e)
        //{
        //    if (ConfigurationManager.AppSettings["HttpsServer"].ToString() == "prod")
        //        if (ConfigurationManager.AppSettings["HttpsServer"].ToString() == "stage")
        //        {
        //            if (!HttpContext.Current.Request.IsSecureConnection)
        //            {
        //                if (!Request.Url.GetLeftPart(UriPartial.Authority).Contains("www"))
        //                {
        //                    HttpContext.Current.Response.Redirect(
        //                        Request.Url.GetLeftPart(UriPartial.Authority).Replace("http://", "https://www."), true);
        //                }
        //                else
        //                {
        //                    HttpContext.Current.Response.Redirect(
        //                        Request.Url.GetLeftPart(UriPartial.Authority).Replace("http://", "https://"), true);
        //                }
        //            }
        //        }
        //}

    }
}
