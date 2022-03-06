using Facebook;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCourses.Common;
using WebCourses.Models;
using ASPSnippets.GoogleAPI;
using System.Web.Script.Serialization;
using System.Net;
using BotDetect.Web.Mvc;

namespace WebCourses.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetByID(model.UserName);
                    var userSession = new User();
                    userSession.UserName = user.UserName;
                    userSession.ID = user.ID;
                    userSession.Password = user.Password;
                    userSession.Name = user.Name;
                    userSession.Phone = user.Phone;
                    userSession.Email = user.Email;
                    userSession.Address = user.Address;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return Redirect("/");
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
                else
                {
                    ModelState.AddModelError("", "Đăng Nhập Không Đúng");
                }
            }

            return View(model);
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = /*ConfigurationManager.AppSettings["FbAppId"]*/1024793591616473,
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = /*ConfigurationManager.AppSettings["FbAppId"]*/1024793591616473,
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });


            var accessToken = result.access_token;

            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's information, like email, first name, middle name etc
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string id = me.id;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;

                var user = new User();
                user.Email = email;
                user.UserName =firstname+lastname+id;
                user.Status = true;
                user.Name = firstname + " " + middlename + " " + lastname;
                user.CreatedDate = DateTime.Now;
                var resultInsert = new UserDao().InsertForFacebook(user);
                if (resultInsert)
                {
                    var nguoidung = new UserDao().GetByID(user.UserName);
                    var userSession = new User();
                    userSession.UserName = nguoidung.UserName;
                    userSession.ID = nguoidung.ID;
                    userSession.Password = nguoidung.Password;
                    userSession.Name = nguoidung.Name;
                    userSession.Phone = nguoidung.Phone;
                    userSession.Email = nguoidung.Email;
                    userSession.Address = nguoidung.Address;
                    Session.Add(CommonConstants.USER_SESSION, userSession);                   
                }
            }

            return Redirect("/");
        }



        /*google*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void LoginWithGooglePlus()
        {
            GoogleConnect.ClientId = "336516722256-27m6dueaojj48onauf6civmsn40fkak1.apps.googleusercontent.com";
            GoogleConnect.ClientSecret = "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";
            GoogleConnect.RedirectUri = Request.Url.AbsoluteUri.Split('?')[0];
            GoogleConnect.Authorize("profile", "email");
        }

        [ActionName("LoginWithGooglePlus")]
        public ActionResult LoginWithGooglePlusConfirmed()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["code"]))
            {
                string code = Request.QueryString["code"];
                string json = GoogleConnect.Fetch("me", code);
                GoogleProfile profile = new JavaScriptSerializer().Deserialize<GoogleProfile>(json);


                User khachHang = new User()
                {
                    UserName = profile.Id,
                    Name = profile.Name,
                    Email = profile.email,
                    CreatedDate=DateTime.Now,
                    CreatedBy=profile.Name,
                    Status=true,
                    GroupID="MEMBER"
                };
                var resultInsert = new UserDao().InsertForGoogle(khachHang);
                if (resultInsert > 0)
                {
                    var nguoidung = new UserDao().GetByID(khachHang.UserName);
                    var userSession = new User();
                    userSession.UserName = nguoidung.UserName;
                    userSession.ID = nguoidung.ID;
                    userSession.Password = nguoidung.Password;
                    userSession.Name = nguoidung.Name;
                    userSession.Phone = nguoidung.Phone;
                    userSession.Email = nguoidung.Email;
                    userSession.Address = nguoidung.Address;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                }
            }
            if (Request.QueryString["error"] == "access_denied")
            {
                return Content("access_denied");
            }
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "registerCapcha", "Mã xác nhận không đúng!")]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var user = new User();
                    user.UserName = model.UserName;
                    user.Name = model.Name;
                    user.Password = Encryptor.MD5Hash(model.Password);
                    user.Phone = model.Phone;
                    user.Email = model.Email;
                    user.Address = model.Address;
                    user.CreatedDate = DateTime.Now;
                    user.Status = true;
                    var result = dao.Insert(user);
                    if (result > 0)
                    {                      
                        var userSession = new User();
                        userSession.UserName = user.UserName;
                        userSession.ID = user.ID;
                        userSession.Password = user.Password;
                        userSession.Name = user.Name;
                        userSession.Phone = user.Phone;
                        userSession.Email = user.Email;
                        userSession.Address = user.Address;
                        Session.Add(CommonConstants.USER_SESSION, userSession);
                        return Redirect("/");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công.");
                    }
                }
            }
            return View(model);
        }
    }
}