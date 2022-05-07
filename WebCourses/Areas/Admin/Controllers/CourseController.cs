using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCourses.Areas.Admin.Controllers
{
    public class CourseController : BaseController
    {
        // GET: Admin/Course
        [HasCredential(RoleID = "VIEW_COURSE")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new CourseDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        [HasCredential(RoleID = "ADD_COURSE")]

        public ActionResult Create()
        {
            SetCourseLevel();
            SetViewBag();
            return View();
        }
        [HttpGet]
        [HasCredential(RoleID = "EDIT_COURSE")]

        public ActionResult Edit(long id)
        {
            var course = new CourseDao().ViewDetail(id);
            SetCourseLevel(course.LevelCourse);
            SetViewBag(course.CategoryID);
            return View(course);
        }


        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "ADD_COURSE")]

        public ActionResult Create(Cours course, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                
                var session = (UserLogin)Session[Common.CommonConstants.USER_SESSION];
                var dao = new CourseDao();
                if (file == null)
                {
                    course.VideoOverview = course.VideoOverview;
                }
                else if (CheckFileType(file.FileName))
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("/Data/Video"), _FileName);
                    var video = _path.Substring(49+9);
                    file.SaveAs(_path);
                    course.VideoOverview = video;
                }
                course.CreatedBy = session.UserName;
                course.ViewCount = 0;
                long id = dao.Insert(course);
                if (id > 0)
                {
                    SetAlert("Thêm Thành Công ", "success");
                    return RedirectToAction("Index", "Course");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm Không thành công");
                }
            }
            SetCourseLevel();
            SetViewBag();
            return View(course);
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "EDIT_COURSE")]

        public ActionResult Edit(Cours course, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
        
                if (file==null)
                {
                    course.VideoOverview = course.VideoOverview;                 
                }             
                else if(CheckFileType(file.FileName))
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("/Data/Video"), _FileName);
                    var video = _path.Substring(49+9);
                    file.SaveAs(_path);
                    course.VideoOverview = video;
                }
                var session = (UserLogin)Session[Common.CommonConstants.USER_SESSION];
                var dao = new CourseDao();
                course.ModifiedBy = session.UserName;
                var result = dao.Update(course);
                if (result)
                {
                    SetAlert("Sửa Thành Công ", "success");
                    return RedirectToAction("Index", "Course");
                }
                else
                {
                    ModelState.AddModelError("", "Update Không thành công");
                }
            }
            SetCourseLevel(course.LevelCourse);
            SetViewBag(course.CategoryID);
            return View(course);
        }

        [HttpDelete]
        [HasCredential(RoleID = "DELETE_COURSE")]

        public ActionResult Delete(int id)
        {
            new CourseDao().Delete(id);
            return RedirectToAction("Index", "Course");
        }

        bool CheckFileType(string fileName)
        {

            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".mp4":
                    return true;
                default:
                    return false;
            }
        }

        [HasCredential(RoleID = "DELETE_COURSE")]
        public ActionResult CommentCourse(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ReviewCourseDao();
            var model = dao.ListCommentCourse(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        public ActionResult DeleteComment(int id)
        {
            var result = new ReviewCourseDao().Delete(id);
            if (result)
            {
                return RedirectToAction("Index", "CommentCourse");
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật Không thành công");
            }
            return View("Index");

        }

        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CourseDao();
            ViewBag.CategoryID = new SelectList(dao.ListAllCategoryforcourse(), "ID", "Name", selectedId);
        }
        public void SetCourseLevel(string select=null)
        {
            var weekDays = new string[3] { "Cơ Bản", "Thông Thường", "Nâng Cao" };
            var items = weekDays.Select((day, index) =>
            {
                return new SelectListItem
                {
                    Value = day,
                    Text = day
                };
            }).ToList();
            ViewBag.LevelCourse = new SelectList(items, "Text", "Value",select);
        }
    }
}