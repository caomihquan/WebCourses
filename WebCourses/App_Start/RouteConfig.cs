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

            routes.IgnoreRoute("{*botdetect}",
   new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "Blog",
                url: "blog",
                defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebCourses.Controllers" }
            );
            routes.MapRoute(
                name: "Search",
                url: "tim-kiem",
                defaults: new { controller = "KhoaHoc", action = "Search", id = UrlParameter.Optional },
                namespaces: new[] { "WebCourses.Controllers" }
            );
            routes.MapRoute(
               name: "Joined Course",
               url: "khoa-hoc-da-tham-gia",
               defaults: new { controller = "KhoaHoc", action = "JoinedCourse", id = UrlParameter.Optional },
               namespaces: new[] { "WebCourses.Controllers" }
           );


            routes.MapRoute(
                name: "Subject category",
                url: "chu-de/{metatitle}-{cateId}",
                defaults: new { controller = "Category", action = "Category", id = UrlParameter.Optional },
                namespaces: new[] { "WebCourses.Controllers" }
            );
            routes.MapRoute(
                name: "Subject",
                url: "chu-de",
                defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebCourses.Controllers" }
            );

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
                name: "Blog Category",
                url: "blog/{metatitle}-{cateId}",
                defaults: new { controller = "Blog", action = "Category", id = UrlParameter.Optional },
                namespaces: new[] { "WebCourses.Controllers" }
            );
            routes.MapRoute(
            name: "Tags",
            url: "tag/{tagId}",
            defaults: new { controller = "Blog", action = "Tag", id = UrlParameter.Optional },
            namespaces: new[] { "WebCourses.Controllers" }
        );
            routes.MapRoute(
            name: "Create Blog",
            url: "tao-bai-viet",
            defaults: new { controller = "Blog", action = "Create", id = UrlParameter.Optional },
            namespaces: new[] { "WebCourses.Controllers" }
        );

            routes.MapRoute(
            name: "Blog Detail",
            url: "blog-chi-tiet/{metatitle}-{id}",
            defaults: new { controller = "Blog", action = "Detail", id = UrlParameter.Optional },
            namespaces: new[] { "WebCourses.Controllers" }
        );
            routes.MapRoute(
                name: "Lessons",
                url: "bai-hoc/{metatitle}-{id}",
                defaults: new { controller = "Lesson", action = "Index", id = UrlParameter.Optional },
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
                name: "Register",
                url: "dang-ky",
                defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
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
