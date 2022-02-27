using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCourses.Areas.Admin.Controllers
{
    public class UserGroupController : BaseController
    {
        // GET: Admin/UserGroup
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new GroupUserDao();
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
        public ActionResult Edit(string id)
        {
            var category = new GroupUserDao().ViewDetail(id);
            return View(category);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(UserGroup usergroup)
        {
            if (ModelState.IsValid)
            {
                var dao = new GroupUserDao();
                bool id = dao.Insert(usergroup);
                if (id)
                {
                    SetAlert("Thêm Thành Công ", "success");
                    return RedirectToAction("Index", "UserGroup");
                }
                else
                {
                    ModelState.AddModelError("", "Trùng ID");
                }
            }
            return View(usergroup);
        }

        [HttpPost]
        public ActionResult Edit(UserGroup userGroup)
        {
            if (ModelState.IsValid)
            {
                var dao = new GroupUserDao();
                var result = dao.Update(userGroup);
                if (result)
                {
                    SetAlert("Sửa Thành Công ", "success");
                    return RedirectToAction("Index", "UserGroup");
                }
                else
                {
                    ModelState.AddModelError("", "Update Không thành công");
                }
            }
            return View(userGroup);
        }

        [HttpDelete]
        public ActionResult Delete(string id)
        {
            new GroupUserDao().Delete(id);
            return RedirectToAction("Index", "UserGroup");
        }
    }
}