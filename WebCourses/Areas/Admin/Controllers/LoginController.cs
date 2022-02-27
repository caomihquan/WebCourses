using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebCourses.Common;
using WebCourses.Models;

namespace WebCourses.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password), true);
                if (result == 1)
                {
                    var user = dao.GetByID(model.UserName);
                    var userSession = new UserLogin();

                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    userSession.GroupID = user.GroupID;
                    var listCredentials = dao.GetListCredential(model.UserName);
                    Session.Add(CommonConstants.SESSION_CREDENTIALS, listCredentials);
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài Khoản Không Tồn Tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài Khoản Đang Bị Khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật Khẩu Không Đúng");
                }
                else if (result == -3)
                {
                    ModelState.AddModelError("", "Tài Khoản Của Bạn Không Có Quyền Đăng Nhập");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng Nhập Không Đúng");
                }
            }
            return View("Index");

        }

        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;


            FormsAuthentication.SignOut();
            // chuye view ve login
            return RedirectToAction("Index", "Home", new { Area = "Admin" });
        }
    }
}