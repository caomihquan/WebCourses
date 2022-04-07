using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCourses.Areas.Admin.Controllers
{
    public class UserActiveCourseController : BaseController
    {
        // GET: Admin/UserActiveCourse
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new UserActiveCourseDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]

        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var category = new UserActiveCourseDao().ViewDetail(id);
            return View(category);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(UserActiveCourse categoryblog)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserActiveCourseDao();
                if (dao.CheckCourse(categoryblog.UserID.Value,categoryblog.CourseActiveID.Value))
                {
                    ModelState.AddModelError("", "Đã Mở Khóa Khóa Học Cho Người Dùng Này");
                }
                else
                {
                    long id = dao.Insert(categoryblog);
                    if (id > 0)
                    {
                        SetAlert("Thêm Thành Công ", "success");
                        return RedirectToAction("Index", "UserActiveCourse");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm Không thành công");
                    }
                }
                
            }
            return View(categoryblog);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(UserActiveCourse categoryblog,long id)
        {
            var category = new UserActiveCourseDao().ViewDetail(id);
            if (ModelState.IsValid)
            {
                var dao = new UserActiveCourseDao();
                if (dao.CheckCourse(categoryblog.UserID.Value, categoryblog.CourseActiveID.Value))
                {
                    ModelState.AddModelError("", "Đã Mở Khóa Khóa Học Cho Người Dùng Này");
                }
                else
                {
                    var result = dao.Update(categoryblog);
                    if (result)
                    {
                        SetAlert("Sửa Thành Công ", "success");
                        return RedirectToAction("Index", "UserActiveCourse");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Update Không thành công");
                    }
                }
                
            }
            return View(categoryblog);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserActiveCourseDao().Delete(id);
            return RedirectToAction("Index", "UserActiveCourse");
        }
    }
}