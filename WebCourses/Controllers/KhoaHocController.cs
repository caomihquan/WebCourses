﻿
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
            var session = (User)Session[CommonConstants.USER_SESSION];
            var course = new CourseDao().ViewDetail(id);
            ViewBag.Cateogry = new CategoryDao().ViewDetail(course.CategoryID.Value);
            ViewBag.Lessons = new LessonDao().ListLessonByID(id);
            ViewBag.Course = course;
            ViewBag.CourseJoined = new JoinedCoursesDao().countUserJoined(id);
            ViewBag.Review = new ReviewCourseDao().ListReview(id);
            if (session == null)
            {

            }
            else
            {
            ViewBag.CheckCourse = new JoinedCoursesDao().CheckCourse(session.ID,course.ID);
                
            }
            var review = new ReviewCourse()
            {
                CourseID = course.ID
            };
            return View(review);
        }

        [HttpPost]
        public ActionResult SendReview(ReviewCourse review, float rating=0)
        {

            var dao = new ReviewCourseDao();
            var session = (User)Session[CommonConstants.USER_SESSION];
            review.CreatedDate = DateTime.Now;
            review.Rating = rating;
            review.UserID = session.ID;
            review.CreatedBy = session.UserName;
            review.Status = true;
            var result=dao.Insert(review);
            if(result > 0)
            {
                return RedirectToAction("OverView", "KhoaHoc", new { id = review.CourseID });
            }
            return View(review);

            
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

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            var result = new ReviewCourseDao().Delete(id);
            if (result)
                {
                    return RedirectToAction("Detail", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật Sản Phẩm Không thành công");
                }
            return View("Index");
        }

    }
}