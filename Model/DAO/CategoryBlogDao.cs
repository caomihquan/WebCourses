using Common;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class CategoryBlogDao
    {
        WebDbContext db = null;
        public CategoryBlogDao()
        {
            db = new WebDbContext();
        }
        public CategoryBlog ViewDetail(long id)
        {
            return db.CategoryBlogs.Find(id);
        }

        public long Insert(CategoryBlog entity)
        {
            db.CategoryBlogs.Add(entity);
            if (string.IsNullOrEmpty(entity.MetaTitle))
            {
                entity.MetaTitle = StringHelper.ToUnsignString(entity.Name);
            }
            entity.CreatedDate = DateTime.Now;
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(CategoryBlog entity)
        {
            try
            {
                var categoryblog = db.CategoryBlogs.Find(entity.ID);
                categoryblog.Name = entity.Name;
                if (string.IsNullOrEmpty(entity.MetaTitle))
                {
                    categoryblog.MetaTitle = StringHelper.ToUnsignString(entity.Name);
                }
                else
                {
                categoryblog.MetaTitle = entity.MetaTitle;
                }
                categoryblog.ParentsID = entity.ParentsID;
                categoryblog.DisplayOrder = entity.DisplayOrder;
                categoryblog.SeoTitle = entity.SeoTitle;
                categoryblog.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public IEnumerable<CategoryBlog> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<CategoryBlog> model = db.CategoryBlogs;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ID.ToString().Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }


        public bool Delete(int id)
        {
            try
            {
                var category = db.CategoryBlogs.Find(id);
                db.CategoryBlogs.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<CategoryBlog> ListAll()
        {
            return db.CategoryBlogs.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }

        public CategoryBlog CategoryCourses(long? id)
        {
            return db.CategoryBlogs.Where(x => x.ID == id).SingleOrDefault();
        }

        public List<Blog> ListByCategoryBlogId(long categoryID, ref int totalRecord, int page = 1, int pageSize = 2)
        {
            totalRecord = db.Blogs.Where(x => x.CategoryBlogID == categoryID).Count();
            var model = db.Blogs.Where(x => x.CategoryBlogID == categoryID &&x.Status==true).OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return model;

        }
    }
}
