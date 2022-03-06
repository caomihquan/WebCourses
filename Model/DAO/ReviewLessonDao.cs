using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ReviewLessonDao
    {
        WebDbContext db = null;
        public ReviewLessonDao()
        {
            db = new WebDbContext();
        }

        public List<ReviewLesson> ListReview(long lessonid)
        {
            return db.ReviewLessons.Where(x => x.LessonID == lessonid).OrderByDescending(x => x.CreatedDate).ToList();
        }

        public long Insert(ReviewLesson entity)
        {
            db.ReviewLessons.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Delete(long id)
        {
            try
            {
                var reviewlesson = db.ReviewLessons.Find(id);
                db.ReviewLessons.Remove(reviewlesson);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
