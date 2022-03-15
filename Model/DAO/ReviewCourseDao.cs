using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ReviewCourseDao
    {
        WebDbContext db = null;
        public ReviewCourseDao()
        {
            db = new WebDbContext();
        }

        public List<ReviewCourse> ListReview(long productID)
        {
            return db.ReviewCourses.Where(x => x.CourseID == productID).OrderByDescending(x => x.CreatedDate).ToList();
        }

        public long Insert(ReviewCourse entity)
        {
            db.ReviewCourses.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Delete(long id)
        {
            try
            {
                var reviewcourse = db.ReviewCourses.Find(id);
                db.ReviewCourses.Remove(reviewcourse);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public IEnumerable<ReviewCourse> ListCommentCourse(string searchString, int page, int pageSize)
        {
            IQueryable<ReviewCourse> model = db.ReviewCourses;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ID.ToString().Contains(searchString) || x.CreatedBy.Contains(searchString) || x.CourseID.ToString().Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
    }
}
