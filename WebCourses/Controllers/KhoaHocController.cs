using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCourses.Controllers
{
    public class KhoaHocController : Controller
    {
        // GET: KhoaHoc
        public ActionResult Index(string searchString, int page = 1, int pageSize = 6)
        {
            var dao = new CourseDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }


        public ActionResult OverView(long id)
        {
            var course = new CourseDao().ViewDetail(id);
            ViewBag.Cateogry = new CategoryDao().ViewDetail(course.CategoryID.Value);
            return View(course);
        }

        [ChildActionOnly]
        public PartialViewResult Category()
        {
            var model = new CategoryDao().ListAll();
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult RecentBlog()
        {
            var model = new BlogDao().RecentBlog(3);
            return PartialView(model);
        }
    }
}