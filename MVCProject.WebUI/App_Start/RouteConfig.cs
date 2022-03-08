using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCProject.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 namespaces: new[] { "MVCProject.WebUI.Controllers" } 
            );

            routes.MapRoute(
       name: "Category",
       url: "{controller}/{ action}/{ id}",
        defaults: new { controller = "Order", action = "Category", id = UrlParameter.Optional }
    );
            routes.MapRoute(
                  name: "DeleteBid",
                  url: "{controller}/{ action}/{ id}",
                   defaults: new { controller = "Bid", action = "DeleteBid", Id = UrlParameter.Optional }
               );
            routes.MapRoute(
                 name: "CompleteOrder",
                 url: "{controller}/{ action}/{ id}",
                  defaults: new { controller = "Order", action = "CompleteOrder", Id = UrlParameter.Optional }
              );
            routes.MapRoute(
                   name: "ProviderDetail",
                   url: "{controller}/{ action}/{id}/{seo_text}",
                    defaults: new { controller = "Provider", action = "ProviderDetail", id = UrlParameter.Optional }
                );
            routes.MapRoute(
                  name: "ProviderDetailId",
                  url: "{controller}/{ action}/{id}",
                   defaults: new { controller = "Provider", action = "ProviderDetailId", id = UrlParameter.Optional }
               );
            routes.MapRoute(
       name: "OrderDetails",
       url: "{controller}/{ action}/{ id}",
        defaults: new { controller = "Account", action = "OrderDetails", id = UrlParameter.Optional }
    );
            
            routes.MapRoute(
                  name: "EditProvider2",
                  url: "{controller}/{ action}/{ id}",
                   defaults: new { controller = "Provider", action = "EditProvider", id = UrlParameter.Optional }
               );
            routes.MapRoute(
                  name: "EditLogo",
                  url: "{controller}/{ action}/{ id}",
                   defaults: new { controller = "Provider", action = "EditLogo", id = UrlParameter.Optional }
               );
            routes.MapRoute(
                  name: "UploadPhoto",
                  url: "{controller}/{ action}/{ id}",
                   defaults: new { controller = "Provider", action = "UploadPhoto", id = UrlParameter.Optional }
               );
            routes.MapRoute(
                 name: "ImageUpload",
                 url: "{controller}/{ action}/{ id}",
                  defaults: new { controller = "Provider", action = "ImageUpload", id = UrlParameter.Optional }
              );
            routes.MapRoute(
                name: "Providers",
                url: "{controller}/{ action}/{ id}",
                 defaults: new { controller = "Provider", action = "Providers", id = UrlParameter.Optional }
             );
            routes.MapRoute(
                name: "ProvidersBySubCatId",
                url: "{controller}/{ action}/{ id}",
                 defaults: new { controller = "Provider", action = "ProvidersBySubCatId", id = UrlParameter.Optional }
             );
            routes.MapRoute(
               name: "MakeBid",
               url: "{controller}/{ action}/{ id}",
                defaults: new { controller = "Bid", action = "MakeBid", id = UrlParameter.Optional }
            ); 
            routes.MapRoute(
                name: "SelectBid",
                url: "{controller}/{ action}/{ id}",
                 defaults: new { controller = "Order", action = "SelectBid", id = UrlParameter.Optional }
             );
            routes.MapRoute(
    name: "BidChosed",
    url: "{controller}/{ action}/{ BidId}/{ OrderId}",
     defaults: new { controller = "Bid", action = "BidChosed", BidId = UrlParameter.Optional, OrderId= UrlParameter.Optional }
 );
            routes.MapRoute(
name: "Edit",
url: "{controller}/{ action}/{ id}",
defaults: new { controller = "Account", action = "Edit", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "Create",
url: "{controller}/{ action}/{ id}",
defaults: new { controller = "Message", action = "Create", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "CreateOrder",
url: "{controller}/{ action}/{ id}",
defaults: new { controller = "Order", action = "CreateOrder", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "IlanBasvurulari",
url: "{controller}/{ action}/{ id}",
defaults: new { controller = "ElemanIlan", action = "IlanBasvurulari", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "BasvuruDetayGetir",
url: "{controller}/{ action}/{ id}",
defaults: new { controller = "ElemanIlan", action = "BasvuruDetayGetir", id = UrlParameter.Optional }
);
            routes.MapRoute(
                "TrackShipment", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Track", action = "TrackShipment", id = UrlParameter.Optional } // Parameter defaults
            );

        }
    }
}
