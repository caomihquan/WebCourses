using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCourses.Common;
using System.Linq.Dynamic.Core;

namespace WebCourses.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index(string searchString, string sortProperty, string sortOrder, int page = 1, int pageSize = 10)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HasCredential(RoleID = "EDIT_USER")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = new UserDao();
            var user = dao.ViewDetail(id);
            SetViewBag(user.GroupID);
            return View(user);
        }

        [HttpPost]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckUserName(user.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (dao.CheckEmail(user.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    if (user.Password != null)
                    {
                        var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                        user.Password = encryptedMd5Pas;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Vui Lòng Nhập Mật Khẩu");
                    }

                    long id = dao.Insert(user);
                    if (id > 0)
                    {
                        SetAlert("Thêm Thành Công ", "success");
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm user Không thành công");
                    }
                }
               
            }
            SetViewBag();
            return View(user);
        }
        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(User user,long id)
        {
            if (ModelState.IsValid)
            {
                var userdetail = new UserDao().ViewDetail(id);
                var dao = new UserDao();
                if (userdetail.UserName == user.UserName && userdetail.Email==user.Email)
                {
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                        user.Password = encryptedMd5Pas;
                    }
                    else
                    {
                        user.Password = userdetail.Password;
                    }
                    var result = dao.Update(user);
                    if (result)
                    {
                        SetAlert("Sửa Thành Công ", "success");
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhật user Không thành công");
                    }
                }
                else
                {
                    if (userdetail.UserName != user.UserName)
                    {
                        if (dao.CheckUserName(user.UserName))
                        {
                            ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(user.Password))
                            {
                                var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                                user.Password = encryptedMd5Pas;

                            }
                            else
                            {
                                user.Password = userdetail.Password;

                            }
                            var result = dao.Update(user);
                            if (result)
                            {
                                SetAlert("Sửa Thành Công ", "success");
                                return RedirectToAction("Index", "User");
                            }
                            else
                            {
                                ModelState.AddModelError("", "Cập nhật user Không thành công");
                            }
                        }
                    }
                    else if (userdetail.Email != user.Email)
                    {
                        if (dao.CheckEmail(user.Email))
                        {
                            ModelState.AddModelError("", "Email đã tồn tại");
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(user.Password))
                            {
                                var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                                user.Password = encryptedMd5Pas;

                            }
                            else
                            {
                                user.Password = userdetail.Password;

                            }
                            var result = dao.Update(user);
                            if (result)
                            {
                                SetAlert("Sửa Thành Công ", "success");
                                return RedirectToAction("Index", "User");
                            }
                            else
                            {
                                ModelState.AddModelError("", "Cập nhật user Không thành công");
                            }
                        }
                    }
                    
                }
                
                
                
            }
            SetViewBag(user.GroupID);
            return View(user);
        }
        [HttpDelete]
        [HasCredential(RoleID = "DELETE_USER")]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index", "User");
        }
        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        public void SetViewBag(string selectedId = null)
        {
            var dao = new GroupUserDao();
            ViewBag.GroupID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }


    }
}