using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class LessonDao
    {
        WebDbContext db = null;
        public LessonDao()
        {
            db = new WebDbContext();
        }
        public Lesson ViewDetail(long id)
        {
            return db.Lessons.Find(id);
        }

        public long Insert(Lesson entity)
        {
            db.Lessons.Add(entity);
            entity.CreatedDate = DateTime.Now;
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Lesson entity)
        {
            try
            {
                var lesson = db.Lessons.Find(entity.ID);
                lesson.Name = entity.Name;
                lesson.MetaTitle = entity.MetaTitle;
                lesson.ParentsID = entity.ParentsID;
                lesson.Video = entity.Video;
                lesson.CourseID = entity.CourseID;
                lesson.HomeWork = entity.HomeWork;
                lesson.ModifiedDate = DateTime.Now;
                lesson.Content = entity.Content;
                lesson.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public IEnumerable<Lesson> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Lesson> model = db.Lessons;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ID.ToString().Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public List<Lesson>countlessson(long top)
        {
            return db.Lessons.Where(x => x.CourseID == top).ToList();
        }
        public bool Delete(int id)
        {
            try
            {
                var lesson = db.Lessons.Find(id);
                db.Lessons.Remove(lesson);
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
