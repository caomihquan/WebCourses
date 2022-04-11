using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCourses.Areas.Admin.Controllers
{
    public class CredentialController : BaseController
    {
        // GET: Admin/Credential
        [HasCredential(RoleID = "VIEW_QUYEN")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new CredentialDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        [HasCredential(RoleID = "ADD_QUYEN")]
        public ActionResult Create()
        {
            SetViewBag();
            SetViewBagCreDential();
            return View();
        }
        [HttpGet]
        [HasCredential(RoleID = "EDIT_QUYEN")]
        public ActionResult Edit(string roleid,string usergroup)
        {
            
            var credential = new CredentialDao().GetByID(roleid,usergroup);
            SetViewBag(usergroup);
            SetViewBagCreDential(usergroup);
            return View(credential);
        }


        [HttpPost]
        [HasCredential(RoleID = "ADD_QUYEN")]
        public ActionResult Create(Credential credential)
        {
            if (ModelState.IsValid)
            {
                var dao = new CredentialDao();
                if (dao.CheckQuyen(credential.RoleID, credential.UserGroupID))
                {
                    ModelState.AddModelError("", "đã tồn tại");
                }
                else
                {
                    var id = dao.Insert(credential);
                    if (id)
                    {
                        SetAlert("Thêm Thành Công ", "success");
                        return RedirectToAction("Index", "Credential");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm Không thành công");
                    }
                }              
            }
            SetViewBag();
            SetViewBagCreDential();
            return View(credential);
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_QUYEN")]
        public ActionResult Edit(Credential credential)
        {
            if (ModelState.IsValid)
            {
                var dao = new CredentialDao();
                if (dao.CheckQuyen(credential.RoleID, credential.UserGroupID))
                {
                    ModelState.AddModelError("", "đã tồn tại");
                }
                else
                {
                    bool result = dao.InsertUpdateCrendential(credential);
                    if (result)
                    {
                        SetAlert("Sửa Thành Công ", "success");
                        return RedirectToAction("Index", "Credential");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhật Sản Phẩm Không thành công");
                    }

                }

            }
            SetViewBag(credential.UserGroupID);
            SetViewBagCreDential(credential.RoleID);
            return View(credential);
        }

        [HttpDelete]
        [HasCredential(RoleID = "DELETE_QUYEN")]
        public ActionResult Delete(string id, string usergroup)
        {
            new CredentialDao().Delete(id,usergroup);
            return RedirectToAction("Index", "Credential");
        }


        public void SetViewBag(string selectedId = null)
        {
            var dao = new GroupUserDao();
            ViewBag.UserGroupID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
        public void SetViewBagCreDential(string selectedId = null)
        {
            var dao = new CredentialDao();
            ViewBag.RoleID = new SelectList(dao.ListAllRole(), "ID", "Name", selectedId);
        }
    }
}