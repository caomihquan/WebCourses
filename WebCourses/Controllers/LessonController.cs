
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebCourses.Common;

namespace WebCourses.Controllers
{
    public class LessonController :Controller
    {
        // GET: Lesson
        public ActionResult Index(long id)
        {
            var session = (User)Session[CommonConstants.USER_SESSION];
            var lesson = new LessonDao().ViewDetailOut(id);
            var course = new CourseDao().ViewDetail(lesson.CourseID.Value);
            var checkactivecourse = new LessonDao().CheckActive(session.ID, course.ID);
            var chungchi = new CertificateDao().listbycourseid(lesson.CourseID.Value);
            if (chungchi != null)
            {
                var requirecertificate = new CertificateDao().ViewDetailOwned(chungchi.ID, session.ID);
                ViewBag.ChungChi = requirecertificate;
            }

            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
            else if (course.Price != null && course.Price != 0)
            {
                if (checkactivecourse || session.GroupID=="ADMIN")
                {
                    ViewBag.Course = course;
                    var lessonAll = new LessonDao().ListLessonByID(lesson.CourseID.Value);
                    ViewBag.Lesson = lessonAll;
                    var check = new JoinedCoursesDao().CheckCourse(session.ID, lesson.CourseID.Value);
                    var checklesson = new JoinedCoursesDao().CheckLesson(session.ID, lesson.CourseID.Value, lesson.ID);
                    ViewBag.BaiHoc = lesson;

                    if (check)
                    {

                    }
                    else
                    {
                        var joinedcourse = new JoinedCours();
                        joinedcourse.CourseID = lesson.CourseID.Value;
                        joinedcourse.UserID = session.ID;
                        joinedcourse.Status = true;
                        joinedcourse.MetaTitle = course.MetaTitle;
                        joinedcourse.Image = course.Image;
                        joinedcourse.LevelCourse = course.LevelCourse;
                        joinedcourse.CategoryID = course.CategoryID;
                        joinedcourse.CreatedDate = DateTime.Now;
                        joinedcourse.CourseName = course.Name;

                        new JoinedCoursesDao().Insert(joinedcourse);

                    }

                    if (checklesson)
                    {

                    }
                    else
                    {
                        var progresscourse = new ProgressLesson();
                        progresscourse.CourseID = lesson.CourseID;
                        progresscourse.LessonID = lesson.ID;
                        progresscourse.UserID = session.ID;
                        progresscourse.CreatedDate = DateTime.Now;
                        new JoinedCoursesDao().InsertProgress(progresscourse);
                    }
                    ViewBag.ProgressLesson = new ProgressLessonDao().LessonByUser(session.ID, lesson.CourseID.Value);
                    ViewBag.Review = new ReviewLessonDao().ListReview(id);



                    var progresslessonbyuser = new ProgressLessonDao().LessonByUser(session.ID, lesson.CourseID.Value);
                    var childs = 0;
                    foreach (var li in lessonAll)
                    {

                        var child = lessonAll.Where(x => x.ParentsID != null && x.ParentsID == li.ID).Count();
                        var countnull = lessonAll.Where(x => x.ParentsID == null && x.ID != li.ParentsID).Count();
                        if (child > 0 && countnull > 0)
                        {
                            childs++;
                        }

                    }
                    var sobaidahoc = (float)progresslessonbyuser.Count();
                    var tongso = (lessonAll.Count() - childs);
                    var tiendo = (sobaidahoc / tongso) * 100;
                    var progresstiendo = (int)tiendo;

                    if (progresstiendo == 100)
                    {
                        var certificate = new ProgressLessonDao().CertificatebyCourse(lesson.CourseID.Value);
                        if (certificate != null)
                        {
                            var certificateown = new CertificateOwned();
                            var checkcertificate = new CertificateDao().CheckCertificate(session.ID, certificate.ID);
                            certificateown.UserID = session.ID;
                            certificateown.CertificateID = certificate.ID;
                            certificateown.FileCertificate = certificate.Image;
                            certificateown.Logo = certificate.Logo;
                            certificateown.CertificateName = certificate.Name;
                            if (!checkcertificate)
                            {
                                new ProgressLessonDao().InsertCertificateOwned(certificateown);
                            }
                        }

                    }
                    var review = new ReviewLesson()
                    {
                        LessonID = lesson.ID
                    };
                    return View(review);
                }
                else
                {
                    return RedirectToAction("Payment", "Lesson", new { id = course.ID });
                }
            }
            else
            {
                ViewBag.Course = course;
                var lessonAll = new LessonDao().ListLessonByID(lesson.CourseID.Value);
                ViewBag.Lesson = lessonAll;
                var check = new JoinedCoursesDao().CheckCourse(session.ID, lesson.CourseID.Value);
                var checklesson = new JoinedCoursesDao().CheckLesson(session.ID, lesson.CourseID.Value, lesson.ID);
                ViewBag.BaiHoc = lesson;

                if (check)
                {

                }
                else
                {
                    var joinedcourse = new JoinedCours();
                    joinedcourse.CourseID = lesson.CourseID.Value;
                    joinedcourse.UserID = session.ID;
                    joinedcourse.Status = true;
                    joinedcourse.MetaTitle = course.MetaTitle;
                    joinedcourse.Image = course.Image;
                    joinedcourse.LevelCourse = course.LevelCourse;
                    joinedcourse.CategoryID = course.CategoryID;
                    joinedcourse.CreatedDate = DateTime.Now;
                    joinedcourse.CourseName = course.Name;

                    new JoinedCoursesDao().Insert(joinedcourse);

                }

                if (checklesson)
                {

                }
                else
                {
                    var progresscourse = new ProgressLesson();
                    progresscourse.CourseID = lesson.CourseID;
                    progresscourse.LessonID = lesson.ID;
                    progresscourse.UserID = session.ID;
                    progresscourse.CreatedDate = DateTime.Now;
                    new JoinedCoursesDao().InsertProgress(progresscourse);
                }
                ViewBag.ProgressLesson = new ProgressLessonDao().LessonByUser(session.ID, lesson.CourseID.Value);
                ViewBag.Review = new ReviewLessonDao().ListReview(id);



                var progresslessonbyuser = new ProgressLessonDao().LessonByUser(session.ID, lesson.CourseID.Value);
                var childs = 0;
                foreach (var li in lessonAll)
                {

                    var child = lessonAll.Where(x => x.ParentsID != null && x.ParentsID == li.ID).Count();
                    var countnull = lessonAll.Where(x => x.ParentsID == null && x.ID != li.ParentsID).Count();
                    if (child > 0 && countnull > 0)
                    {
                        childs++;
                    }

                }
                var sobaidahoc = (float)progresslessonbyuser.Count();
                var tongso = (lessonAll.Count() - childs);
                var tiendo = (sobaidahoc / tongso) * 100;
                var progresstiendo = (int)tiendo;

                if (progresstiendo == 100)
                {
                    var certificate = new ProgressLessonDao().CertificatebyCourse(lesson.CourseID.Value);
                    if (certificate != null)
                    {
                        var certificateown = new CertificateOwned();
                        var checkcertificate = new CertificateDao().CheckCertificate(session.ID, certificate.ID);
                        certificateown.UserID = session.ID;
                        certificateown.CertificateID = certificate.ID;
                        certificateown.FileCertificate = certificate.Image;
                        certificateown.Logo = certificate.Logo;
                        certificateown.CertificateName = certificate.Name;
                        if (!checkcertificate)
                        {
                            new ProgressLessonDao().InsertCertificateOwned(certificateown);
                        }
                    }

                }
                var review = new ReviewLesson()
                {
                    LessonID = lesson.ID
                };
                return View(review);
            }
            
            

        }
        public ActionResult ActiveSuccess()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Payment(long id)
        {
            ViewBag.CourseID = id;
            return View();
        }
        [HttpPost]
        public ActionResult Payment(CourseActive model,long id)
        {
            var session = (User)Session[CommonConstants.USER_SESSION];
            if (ModelState.IsValid)
            {
                
                var dao = new CourseDao();
                model.CourseID = id;
                model.CreatedDate = DateTime.Now;
                model.Status = true;
                model.TransactionID = model.TransactionID;
                model.UserID = session.ID;
                if (!dao.CheckActiveCourse(model.TransactionID))
                {
                    string a = System.IO.File.ReadAllText(Server.MapPath("/Assets/Outsite/Template/Payment.html"));
                    a = a.Replace("{{CustomerName}}", session.UserName);
                    a = a.Replace("{{UserID}}", session.ID.ToString());
                    a = a.Replace("{{MaGiaoDich}}", model.TransactionID.ToString());
                    a = a.Replace("{{MaKhoaHoc}}", model.CourseID.ToString());
                    SendMail("caominhquan0512@gmail.com", "Email Đăng Ký Khóa Học", a);
                    long result = dao.InsertUserActiveCourse(model);
                    if (result > 0)
                    {
                        return RedirectToAction("ActiveSuccess", "Lesson");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Không thành công");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Mã Giao Dịch Đã Tồn Tại");
                }
                
            }
            return View(model);
        }
        public void SendMail(string toEmailAddress, string subject, string content)
        {
            var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

            bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

            string body = content;
            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmailAddress));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = enabledSsl;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.Send(message);
        }

        [HttpPost]
        public ActionResult SendReview(ReviewLesson review)
        {

            var dao = new ReviewLessonDao();
            var session = (User)Session[CommonConstants.USER_SESSION];
            review.CreatedDate = DateTime.Now;
            review.UserID = session.ID;
            review.CreatedBy = session.Name;
            review.Status = true;
            var result = dao.Insert(review);
            if (result > 0)
            {
                return RedirectToAction("Index", "Lesson", new { id = review.LessonID });
            }
            return View(review);


        }

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            var result = new ReviewLessonDao().Delete(id);
            if (result)
            {
                return RedirectToAction("Detail", "Product");
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật Sản Phẩm Không thành công");
            }
            return View("Index");
        }

      

    }

}