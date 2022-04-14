
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCourses.Common;

namespace WebCourses.Areas.Admin.Controllers
{
    public class BlogController : BaseController
    {
        // GET: Admin/Blog
        [HasCredential(RoleID = "VIEW_BLOG")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new BlogDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        [HasCredential(RoleID = "ADD_BLOG")]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpGet]
        [HasCredential(RoleID = "EDIT_BLOG")]
        public ActionResult Edit(long id)
        {
            var dao = new BlogDao();
            var content = dao.ViewDetail(id);
            SetViewBag(content.CategoryBlogID);
            return View(content);
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "EDIT_BLOG")]
        public ActionResult Edit(Blog model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                var dao = new BlogDao();
                model.ModifiedBy = session.UserName;
                var result = dao.Update(model);
                if (result)
                {
                    SetAlert("Sửa Thành Công ", "success");
                    return RedirectToAction("Index", "Blog");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật Không thành công");
                }
            }
            SetViewBag(model.CategoryBlogID);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "ADD_BLOG")]
        public ActionResult Create(Blog content)
        {
            if (ModelState.IsValid)
            {
                SetAlert("Thêm Thành Công ", "success");
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                content.CreatedBy = session.UserName;
                content.ViewCount = 0;
                new BlogDao().Insert(content);
                return RedirectToAction("Index");
            }
            SetViewBag();
            return View(content);
        }

        [HttpDelete]
        [HasCredential(RoleID = "DELETE_BLOG")]
        public ActionResult Delete(int id)
        {
            var result = new BlogDao().Delete(id);
            if (result)
            {
                return RedirectToAction("Index", "Blog");
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật Không thành công");
            }
            return View("Index");

        }

        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryBlogDao();
            ViewBag.CategoryBlogID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }

        [HasCredential(RoleID = "CHECK_BLOG")]
        public ActionResult DuyetBlog(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new BlogDao();
            var model = dao.ListAllPagingFalse(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        public JsonResult ChangeStatus(long id)
        {
            var result = new BlogDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        [HasCredential(RoleID = "VIEW_BLOGCOMMENT")]
        public ActionResult CommentBlog(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ReviewBlogDao();
            var model = dao.ListCommentBlog(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        public ActionResult DeleteComment(int id)
        {
            var result = new ReviewBlogDao().Delete(id);
            if (result)
            {
                return RedirectToAction("Index", "CommentBlog");
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật Không thành công");
            }
            return View("Index");

        }
    }
}