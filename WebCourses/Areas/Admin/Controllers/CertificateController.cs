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
    public class CertificateController : BaseController
    {
        // GET: Admin/Certificate
        [HasCredential(RoleID = "VIEW_CERTIFICATE")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new CertificateDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }



        [HttpGet]
        [HasCredential(RoleID = "ADD_CERTIFICATE")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        [HasCredential(RoleID = "EDIT_CERTIFICATE")]
        public ActionResult Edit(long id)
        {
            var category = new CertificateDao().ViewDetail(id);
            return View(category);
        }


        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "ADD_CERTIFICATE")]
        public ActionResult Create(Certificate categoryblog, HttpPostedFileBase file)
        {
            
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    categoryblog.Image = null;
                }
                else if (CheckFileType(file.FileName))
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("/Data/Certificate"), _FileName);
                    var video = _path.Substring(49 +9);
                    file.SaveAs(_path);
                    categoryblog.Image = video;
                }
                var dao = new CertificateDao();
                if (dao.CheckChungchi(categoryblog.IDCourse.Value))
                {
                    ModelState.AddModelError("", "Đã Có Chứng Chỉ Cho ID Khóa Học Này");
                }
                else
                {
                    long id = dao.Insert(categoryblog);
                    if (id > 0)
                    {
                        SetAlert("Thêm Thành Công ", "success");
                        return RedirectToAction("Index", "Certificate");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm Không thành công");
                    }
                }
                
            }
            return View(categoryblog);
        }
        bool CheckFileType(string fileName)
        {

            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".doc":
                    return true;
                case ".docx":
                    return true;
                case ".pdf":
                    return true;
                default:
                    return false;
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "EDIT_CERTIFICATE")]
        public ActionResult Edit(Certificate categoryblog,long id, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    categoryblog.Image = categoryblog.Image;
                }
                else if (CheckFileType(file.FileName))
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("/Data/Certificate"), _FileName);
                    var video = _path.Substring(49+9);
                    file.SaveAs(_path);
                    categoryblog.Image = video;
                }
                var dao = new CertificateDao();
                var category = new CertificateDao().ViewDetail(id);
                if (categoryblog.IDCourse == category.IDCourse)
                {
                    var result = dao.Update(categoryblog);
                    if (result)
                    {
                        SetAlert("Sửa Thành Công ", "success");
                        return RedirectToAction("Index", "Certificate");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Update Không thành công");
                    }
                }
                else if (dao.CheckChungchi(categoryblog.IDCourse.Value))
                {
                    ModelState.AddModelError("", "Đã Có Chứng Chỉ Cho ID Khóa Học Này");
                }

            }
            return View(categoryblog);
        }

        [HttpDelete]
        [HasCredential(RoleID = "DELETE_CERTIFICATE")]
        public ActionResult Delete(int id)
        {
            new CertificateDao().Delete(id);
            return RedirectToAction("Index", "Certificate");
        }
    }
}