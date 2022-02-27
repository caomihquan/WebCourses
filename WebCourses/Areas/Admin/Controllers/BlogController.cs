
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
        
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new BlogDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new BlogDao();
            var content = dao.ViewDetail(id);
            SetViewBag(content.CategoryBlogID);
            return View(content);
        }

        [HttpPost]
        [ValidateInput(false)]
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
            return View("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
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
            return View("Index");
        }

        [HttpDelete]
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
    }
}