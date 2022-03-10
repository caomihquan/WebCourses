
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
    public class LessonController :Controller
    {
        // GET: Lesson
        public ActionResult Index(long id)
        {
            var session = (User)Session[CommonConstants.USER_SESSION];
            var lesson = new LessonDao().ViewDetail(id);
            var course = new CourseDao().ViewDetail(lesson.CourseID.Value);
            ViewBag.Course = course;
            ViewBag.Lesson = new LessonDao().ListLessonByID(lesson.CourseID.Value);
            var check = new JoinedCoursesDao().CheckCourse(session.ID, lesson.CourseID.Value);
            var checklesson = new JoinedCoursesDao().CheckLesson(session.ID, lesson.CourseID.Value,lesson.ID);
            ViewBag.BaiHoc = lesson;
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                if (check)
                {

                }
                else
                {
                    var joinedcourse = new JoinedCours();
                    joinedcourse.CourseID = lesson.CourseID.Value;
                    joinedcourse.UserID = session.ID;
                    joinedcourse.Status = true;
                    joinedcourse.MetaTitle = course.MetaTitle;
                    joinedcourse.Image = course.Image;
                    joinedcourse.LevelCourse = course.LevelCourse;
                    joinedcourse.CategoryID = course.CategoryID;
                    joinedcourse.CreatedDate = DateTime.Now;
                    joinedcourse.CourseName = course.Name;
                    
                    new JoinedCoursesDao().Insert(joinedcourse);

                }

                if (checklesson)
                {

                }
                else
                {
                    var progresscourse = new ProgressLesson();
                    progresscourse.CourseID = lesson.CourseID;
                    progresscourse.LessonID = lesson.ID;
                    progresscourse.UserID = session.ID;
                    new JoinedCoursesDao().InsertProgress(progresscourse);
                }
                ViewBag.ProgressLesson = new JoinedCoursesDao().progress(session.ID, lesson.CourseID.Value);
                ViewBag.Review = new ReviewLessonDao().ListReview(id);
                
                
            }
            var review = new ReviewLesson()
            {
                LessonID = lesson.ID
            };
            return View(review);

        }

        [HttpPost]
        public ActionResult SendReview(ReviewLesson review)
        {

            var dao = new ReviewLessonDao();
            var session = (User)Session[CommonConstants.USER_SESSION];
            review.CreatedDate = DateTime.Now;
            review.UserID = session.ID;
            review.CreatedBy = session.UserName;
            review.Status = true;
            var result = dao.Insert(review);
            if (result > 0)
            {
                return RedirectToAction("Index", "Lesson", new { id = review.LessonID });
            }
            return View(review);


        }

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            var result = new ReviewLessonDao().Delete(id);
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