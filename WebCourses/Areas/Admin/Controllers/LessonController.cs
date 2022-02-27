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
    public class LessonController : BaseController
    {
        // GET: Admin/Lesson
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new LessonDao();
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
            var lesson = new LessonDao().ViewDetail(id);
            return View(lesson);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Lesson lesson, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    lesson.Video = lesson.Video;
                }
                else if (CheckFileType(file.FileName))
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("/Data/Video"), _FileName);
                    var video = _path.Substring(49);
                    file.SaveAs(_path);
                    lesson.Video = video;
                }
                var session = (UserLogin)Session[Common.CommonConstants.USER_SESSION];
                var dao = new LessonDao();
                lesson.CreatedBy = session.UserName;
                lesson.MoreFiles = "<File></File>";
                lesson.ViewCount = 0;
                long id = dao.Insert(lesson);
                if (id > 0)
                {
                    SetAlert("Thêm Thành Công ", "success");
                    return RedirectToAction("Index", "Lesson");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm Không thành công");
                }
            }
            return View(lesson);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Lesson lesson, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[Common.CommonConstants.USER_SESSION];
                if (file == null)
                {
                    lesson.Video = lesson.Video;
                }
                else if (CheckFileType(file.FileName))
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("/Data/Video"), _FileName);
                    var video = _path.Substring(49);
                    file.SaveAs(_path);
                    lesson.Video = video;
                }
                var dao = new LessonDao();
                lesson.ModifiedBy = session.UserName;
                var result = dao.Update(lesson);
                if (result)
                {
                    SetAlert("Sửa Thành Công ", "success");
                    return RedirectToAction("Index", "Lesson");
                }
                else
                {
                    ModelState.AddModelError("", "Update Không thành công");
                }
            }
            return View(lesson);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new LessonDao().Delete(id);
            return RedirectToAction("Index", "Lesson");
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
    }
}