using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;

namespace InShare.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Post",
                url: "Post/{shortCode}",
                defaults: new { controller = "Post", action = "Index" }
            );

            routes.MapRoute(
                name: "User",
                url: "User/{id}",
                defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional },
                constraints: new { id = @"^\d*$" }
            );
            //routes.MapRoute(
            //    name: "User",
            //    url: "User/{action}",
            //    defaults: new { controller = "User", action = "Index" }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
