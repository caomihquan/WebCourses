using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCourses.Areas.Admin.Controllers
{
    public class CategoryBlogController : BaseController
    {
        // GET: Admin/CategoryBlog
        [HasCredential(RoleID = "VIEW_CATEGORYBLOG")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new CategoryBlogDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        [HasCredential(RoleID = "ADD_CATEOGRYBLOG")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        [HasCredential(RoleID = "EDIT_CATEGORYBLOG")]
        public ActionResult Edit(long id)
        {
            var category = new CategoryBlogDao().ViewDetail(id);
            return View(category);
        }


        [HttpPost]
        [HasCredential(RoleID = "ADD_CATEOGRYBLOG")]
        [ValidateInput(false)]
        public ActionResult Create(CategoryBlog categoryblog)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryBlogDao();
                long id = dao.Insert(categoryblog);
                if (id > 0)
                {
                    SetAlert("Thêm Thành Công ", "success");
                    return RedirectToAction("Index", "CategoryBlog");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm Không thành công");
                }
            }
            return View("Index");
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_CATEGORYBLOG")]
        [ValidateInput(false)]
        public ActionResult Edit(CategoryBlog categoryblog)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryBlogDao();
                var result = dao.Update(categoryblog);
                if (result)
                {
                    SetAlert("Sửa Thành Công ", "success");
                    return RedirectToAction("Index", "CategoryBlog");
                }
                else
                {
                    ModelState.AddModelError("", "Update Không thành công");
                }
            }
            return View("Index");
        }

        [HttpDelete]
        [HasCredential(RoleID = "DELETE_CATEGORYBLOG")]
        public ActionResult Delete(int id)
        {
            new CategoryBlogDao().Delete(id);
            return RedirectToAction("Index", "CategoryBlog");
        }
    }
}