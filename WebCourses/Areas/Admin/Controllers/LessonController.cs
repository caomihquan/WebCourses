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
        [HasCredential(RoleID = "VIEW_LESSON")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new LessonDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        [HasCredential(RoleID = "ADD_LESSON")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        [HasCredential(RoleID = "EDIT_LESSON")]

        public ActionResult Edit(long id)
        {
            var lesson = new LessonDao().ViewDetail(id);
            return View(lesson);
        }


        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "ADD_LESSON")]

        public ActionResult Create(Lesson lesson, HttpPostedFileBase filevideo, List<HttpPostedFileBase> file)
        {
            if (ModelState.IsValid)
            {
                
                if (filevideo == null)
                {
                    lesson.Video = lesson.Video;
                }
                else if (CheckFileTypeVideo(filevideo.FileName))
                {
                    string _FileName = Path.GetFileName(filevideo.FileName);
                    string _path = Path.Combine(Server.MapPath("/Data/Video"), _FileName);
                    var video = _path.Substring(49+9);
                    filevideo.SaveAs(_path);
                    lesson.Video = video;
                }
                var session = (UserLogin)Session[Common.CommonConstants.USER_SESSION];
                var dao = new LessonDao();
                lesson.CreatedBy = session.UserName;
                lesson.ViewCount = 0;
                int byteCount = 0;
                string chuoi = "";
                if (file[0] == null)
                {
                    lesson.MoreFiles = null;
                }
                else
                {
                    foreach (HttpPostedFileBase f in file)
                    {
                        int sizefile = f.ContentLength;
                        byteCount = sizefile + byteCount;
                        if (byteCount < 26214400)
                        {
                            string files = Path.GetFileName(f.FileName);
                            string _path = Path.Combine(Server.MapPath("/Data/File"), files);
                            var video = _path.Substring(49+9);
                            f.SaveAs(_path);
                            chuoi = chuoi + "," + video;
                        }
                        else
                        {
                            chuoi = null;
                            ViewBag.Message = "Tổng Dung Lượng File Không Vượt Quá 25MB";
                            return View(lesson);
                        }

                    }
                    lesson.MoreFiles = chuoi;
                }
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
        [HasCredential(RoleID = "EDIT_LESSON")]

        public ActionResult Edit(Lesson lesson, HttpPostedFileBase filevideo, List<HttpPostedFileBase> file)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[Common.CommonConstants.USER_SESSION];
                if (filevideo == null)
                {
                    lesson.Video = lesson.Video;
                }
                else if (CheckFileTypeVideo(filevideo.FileName))
                {
                    string _FileName = Path.GetFileName(filevideo.FileName);
                    string _path = Path.Combine(Server.MapPath("/Data/Video"), _FileName);
                    var video = _path.Substring(49+9);
                    filevideo.SaveAs(_path);
                    lesson.Video = video;
                }
                var dao = new LessonDao();
                lesson.ModifiedBy = session.UserName;
                string chuoi = "";
                int byteCount = 0;
                var list = new LessonDao().ViewDetail(lesson.ID);
                
                if (file[0] == null)
                {
                    lesson.MoreFiles = list.MoreFiles;
                }
                else
                {
                    foreach (HttpPostedFileBase f in file)
                    {
                        int sizefile = f.ContentLength;
                        byteCount = sizefile + byteCount;
                        if (byteCount < 26214400)
                        {
                            string files = Path.GetFileName(f.FileName);
                            string _path = Path.Combine(Server.MapPath("/Data/File"), files);
                            var video = _path.Substring(49+9);
                            f.SaveAs(_path);
                            chuoi = chuoi + "," + video;
                        }
                        else
                        {
                            chuoi = null;
                            ViewBag.Message = "Tổng Dung Lượng File Không Vượt Quá 25MB";
                            return View(lesson);
                        }
                    }
                    lesson.MoreFiles = chuoi;
                }
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
        [HasCredential(RoleID = "DELETE_LESSON")]
        public ActionResult Delete(int id)
        {
            new LessonDao().Delete(id);
            return RedirectToAction("Index", "Lesson");
        }
        bool CheckFileTypeVideo(string fileName)
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

        [HttpPost]
        public JsonResult ClearFilePath(long id)
        {
            var result = new LessonDao().ClearAllFile(id);
            return Json(new
            {
                status = result
            });
        }

        [HasCredential(RoleID = "VIEW_COMMENTLESSON")]
        public ActionResult CommentLesson(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ReviewLessonDao();
            var model = dao.ListCommentLesson(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        public ActionResult DeleteComment(int id)
        {
            var result = new ReviewLessonDao().Delete(id);
            if (result)
            {
                return RedirectToAction("Index", "CommentLesson");
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật Không thành công");
            }
            return View("Index");

        }
    }
}