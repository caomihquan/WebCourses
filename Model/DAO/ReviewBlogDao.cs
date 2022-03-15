using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ReviewBlogDao
    {
        WebDbContext db = null;
        public ReviewBlogDao()
        {
            db = new WebDbContext();
        }

        public long Insert(ReviewBlog entity)
        {
            db.ReviewBlogs.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Delete(long id)
        {
            try
            {
                var reviewcourse = db.ReviewBlogs.Find(id);
                db.ReviewBlogs.Remove(reviewcourse);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<ReviewBlog> ListReview(long blogid)
        {
            return db.ReviewBlogs.Where(x => x.BlogID == blogid).OrderByDescending(x => x.CreatedDate).ToList();
        }

        public IEnumerable<ReviewBlog> ListCommentBlog(string searchString, int page, int pageSize)
        {
            IQueryable<ReviewBlog> model = db.ReviewBlogs;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ID.ToString().Contains(searchString) || x.CreatedBy.Contains(searchString) || x.BlogID.ToString().Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

    }
}
