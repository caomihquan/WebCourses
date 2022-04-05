using Model.EF;
using PagedList;
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
            return db.ReviewLessons.Where(x => x.LessonID == lessonid && x.AnswerID==null).OrderByDescending(x => x.CreatedDate).ToList();
            
        }

        public List<ReviewLesson> ListReviewAnswer(long lessonid , long reviewid)
        {
            return db.ReviewLessons.Where(x => x.LessonID == lessonid && x.AnswerID == reviewid).OrderByDescending(x => x.CreatedDate).ToList();

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

        public IEnumerable<ReviewLesson> ListCommentLesson(string searchString, int page, int pageSize)
        {
            IQueryable<ReviewLesson> model = db.ReviewLessons;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ID.ToString().Contains(searchString) || x.CreatedBy.Contains(searchString) || x.LessonID.ToString().Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
    }
}
