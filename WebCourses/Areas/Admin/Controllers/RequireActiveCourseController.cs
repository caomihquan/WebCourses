using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCourses.Areas.Admin.Controllers
{
    public class RequireActiveCourseController : BaseController
    {
        // GET: Admin/RequireActiveCourse
        [HasCredential(RoleID = "VIEW_REQUIREACTIVECOURSE")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new RequireActiveCourseDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        public JsonResult ChangeStatus(long id)
        {
            var result = new RequireActiveCourseDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        [HttpDelete]
        
        public ActionResult Delete(int id)
        {
            var result = new RequireActiveCourseDao().Delete(id);
            if (result)
            {
                return RedirectToAction("Index", "RequireActiveCourse");
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật Không thành công");
            }
            return View("Index");

        }

    }
}