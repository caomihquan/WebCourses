
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCourses.Common;

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
            ViewBag.Lessons = new LessonDao().ListLessonByID(id);
            ViewBag.Course = course;
            ViewBag.Review = new ReviewCourseDao().ListReview(id);
            var review = new ReviewCourse()
            {
                CourseID = course.ID
            };
            return View(review);
        }

        [HttpPost]
        public ActionResult SendReview(ReviewCourse review, float rating)
        {

            var dao = new ReviewCourseDao();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            review.CreatedDate = DateTime.Now;
            review.Rating = rating;
            /*            review.UserID = session.ID;
                        review.CreatedBy = session.UserName;*/
            review.Status = true;
            dao.Insert(review);
            return RedirectToAction("OverView", "KhoaHoc", new { id = review.CourseID });
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