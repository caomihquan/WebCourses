using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebCourses
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Course",
                url: "khoa-hoc",
                defaults: new { controller = "KhoaHoc", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebCourses.Controllers" }
            );
            routes.MapRoute(
                name: "Course Overview",
                url: "tong-quan/{metatitle}-{id}",
                defaults: new { controller = "KhoaHoc", action = "OverView", id = UrlParameter.Optional },
                namespaces: new[] { "WebCourses.Controllers" }
            );
            routes.MapRoute(
                name: "Login",
                url: "dang-nhap",
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "WebCourses.Controllers" }
            );
            routes.MapRoute(
                name: "Logout",
                url: "dang-xuat",
                defaults: new { controller = "User", action = "Logout", id = UrlParameter.Optional },
                namespaces: new[] { "WebCourses.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebCourses.Controllers" }
            );
        }
    }
}
