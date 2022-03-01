using Model.EF;
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
    }
}
