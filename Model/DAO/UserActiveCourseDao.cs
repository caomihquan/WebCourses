using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class UserActiveCourseDao
    {
        WebDbContext db = null;
        public UserActiveCourseDao()
        {
            db = new WebDbContext();
        }

        public UserActiveCourse ViewDetail(long id)
        {
            return db.UserActiveCourses.Find(id);
        }

        public long Insert(UserActiveCourse entity)
        {
            db.UserActiveCourses.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(UserActiveCourse entity)
        {
            try
            {
                var categoryblog = db.UserActiveCourses.Find(entity.ID);
                categoryblog.CourseActiveID = entity.CourseActiveID;
                categoryblog.UserID = entity.UserID;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public IEnumerable<UserActiveCourse> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<UserActiveCourse> model = db.UserActiveCourses;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserID.ToString().Contains(searchString) || x.CourseActiveID.ToString().Contains(searchString));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }


        public bool Delete(int id)
        {
            try
            {
                var category = db.UserActiveCourses.Find(id);
                db.UserActiveCourses.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool CheckCourse(long userid, long courseid)
        {
            return db.UserActiveCourses.Count(x => x.UserID == userid && x.CourseActiveID == courseid) > 0;
        }

    }
}
