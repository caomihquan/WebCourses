using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCourses.Common;
namespace WebCourses.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new BlogDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            ViewBag.Tag = dao.ListAllTag();
            return View(model);
        }

        public ActionResult BlogSave(string searchString, int page = 1, int pageSize = 10)
        {
            var session = (User)Session[CommonConstants.USER_SESSION];
            var dao = new BlogSaveDao();
            var model = dao.ListAllPaging(searchString, page, pageSize,session.ID);
            ViewBag.SearchString = searchString;
            ViewBag.Tag = new BlogDao().ListAllTag();
            return View(model);
        }

        [ChildActionOnly]
        public PartialViewResult CategoryBlog()
        {
            var model = new CategoryBlogDao().ListAll();
            return PartialView(model);
        }

        public ActionResult Category(long cateId, int page = 1, int pageSize = 8)
        {
            var categoryblog = new CategoryBlogDao().ViewDetail(cateId);
            ViewBag.Category = categoryblog;
            int totalRecord = 0;
            var model = new CategoryBlogDao().ListByCategoryBlogId(cateId, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            ViewBag.Tag = new BlogDao().ListAllTag();
            return View(model);
        }

        public ActionResult Tag(string tagId, int page = 1, int pageSize = 10)
        {
            var model = new BlogDao().ListAllByTag(tagId, page, pageSize);
            ViewBag.Tags = new BlogDao().ListAllTag();
            int totalRecord = 0;

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            ViewBag.Tag = new BlogDao().GetTag(tagId);
            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Tags = new BlogDao().ListAllTag();
            SetViewBag();
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Blog content, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {

                if (img == null)
                {
                    content.Image = "/Data/images/Blog/blogcmeducation.png";
                }
                else if (CheckFileType(img.FileName))
                {
                    string _FileName = Path.GetFileName(img.FileName);
                    string _path = Path.Combine(Server.MapPath("/Data/Blogs"), _FileName);
                    var video = _path.Substring(49);
                    img.SaveAs(_path);
                    content.Image = video;
                }
                var session = (User)Session[CommonConstants.USER_SESSION];
                content.CreatedBy = session.UserName;
                content.ViewCount = 0;
                content.Status = false;
                new BlogDao().Insert(content);
                return RedirectToAction("SucceedCreate");
            }
            SetViewBag();
            return View("Index");
        }
        bool CheckFileType(string fileName)
        {

            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".png":
                    return true;
                case ".jpg":
                    return true;
                case ".svg":
                    return true;
                default:
                    return false;
            }
        }

        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryBlogDao();
            ViewBag.CategoryBlogID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }

        public ActionResult SucceedCreate()
        {
            ViewBag.Tags = new BlogDao().ListAllTag();
            return View();
        }

        public ActionResult Detail(long id)
        {
            var model = new BlogDao().GetByID(id);
            ViewBag.RelatedBlog = new BlogDao().ListRelatedContents(id, 1);
            ViewBag.Tags = new BlogDao().ListAllTag();
            ViewBag.Blog = model;
            ViewBag.Tag = new BlogDao().ListTag(id);
            var review = new ReviewBlog()
            {
                BlogID = model.ID
            };
            ViewBag.Review = new ReviewBlogDao().ListReview(id);
            return View(review);
        }

        [HttpPost]
        public ActionResult SendReview(ReviewBlog review)
        {

            var dao = new ReviewBlogDao();
            var session = (User)Session[CommonConstants.USER_SESSION];
            review.CreatedDate = DateTime.Now;
            review.UserID = session.ID;
            review.CreatedBy = session.Name;
            review.Status = true;
            var result = dao.Insert(review);
            if (result > 0)
            {
                return RedirectToAction("Detail", "Blog", new { id = review.BlogID });
            }
            
            return View(review);


        }

        [HttpPost]
        public JsonResult AddChangeBlogSaveStatus(long id)
        {
            var model = new BlogDao().ViewDetail(id);
            var session = (User)Session[CommonConstants.USER_SESSION];
            var saveblog = new BlogDao().ViewDetailBlogSave(id, session.ID);
            long? userid = 0;
            if (saveblog == null)
            {
                userid = 0;
            }
            else
            {
                userid = saveblog.UserID;
            }
            var result = true;

            if (session.ID == userid && model.ID==id)
            {
                result = new BlogDao().ChangeBlogSave(model.ID, session.ID);
            }
            else
            {
                var blogsave = new BlogSave();
                blogsave.BlogID = model.ID;
                blogsave.UserID = session.ID;
                blogsave.MetaTitle = model.MetaTitle;
                blogsave.Name = model.Name;
                blogsave.Image = model.Image;
                blogsave.Description = model.Description;
                blogsave.CategoryBlogID = model.CategoryBlogID;
                blogsave.CreatedDate = model.CreatedDate;
                blogsave.CreatedBy = model.CreatedBy;
                blogsave.Status = true;
                result = new BlogDao().AddBlogSave(blogsave);
               
            }
            return Json(new
            {
                status = result
            });

        }
        

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            var result = new ReviewBlogDao().Delete(id);
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