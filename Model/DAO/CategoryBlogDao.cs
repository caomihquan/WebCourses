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
                categoryblog.MetaTitle = entity.MetaTitle;
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
    }
}
