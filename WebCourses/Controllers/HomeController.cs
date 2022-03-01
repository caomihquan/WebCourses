using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace WebCourses.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           
            ViewBag.PopularSubject = new CategoryDao().ListPopularSubjects(3);
            ViewBag.TotalCourses = new CourseDao().ListAll().Count();
            ViewBag.FeaterCourses = new CourseDao().ListFeatureCourses(2);
            ViewBag.TotalSubjects = new CategoryDao().ListAll().Count();
            ViewBag.TotalMember = new UserDao().ListAll().Count();
            return View();
        }


        [ChildActionOnly]
        //[OutputCache(Duration =3600*24)]
        public ActionResult header()
        {
            var model = new MenuDao().ListByGroupId(1);
            ViewBag.Login = new MenuDao().ListByGroupId(2);
            return PartialView(model);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600 * 24)]
        public ActionResult footer()
        {
            var model = new MenuDao().GetFooter();
            return PartialView(model);
        }
    }
}