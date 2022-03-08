using Model.EF;
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

    }
}
