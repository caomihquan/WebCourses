using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCourses.Areas.Admin.Controllers
{
    public class RequireCertificateController : BaseController
    {
        // GET: Admin/RequireCertificate
        [HasCredential(RoleID = "VIEW_REQUIRECERTIFICATE")]

        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new CertificateDao();
            var model = dao.ListAllPagingRequireCertificate(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }


        [HttpDelete]
        [HasCredential(RoleID = "DELETE_REQUIRECERTIFICATE")]
        public ActionResult Delete(int id)
        {
            new CertificateDao().DeleteRequireCertificate(id);
            return RedirectToAction("Index", "RequireCertificate");
        }
    }
}