using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class JoinedCoursesDao
    {
        WebDbContext db = null;
        public JoinedCoursesDao()
        {
            db = new WebDbContext();
        }
        public List<JoinedCours> countUserJoined(long top)
        {
            return db.JoinedCourses.Where(x => x.CourseID == top).ToList();
        }

        public bool CheckCourse(long userid,long courseid)
        {
            return db.JoinedCourses.Count(x => x.UserID == userid && x.CourseID == courseid) > 0;
        }

        public bool CheckLesson(long userid, long courseid,long lesson)
        {
            return db.ProgressLessons.Count(x => x.UserID == userid && x.CourseID == courseid && x.LessonID==lesson) > 0;
        }

        public long Insert(JoinedCours entity)
        {
            db.JoinedCourses.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public long InsertProgress(ProgressLesson entity)
        {
            db.ProgressLessons.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public List<ProgressLesson> progress(long userid,long courseid)
        {
            return db.ProgressLessons.Where(x => x.UserID == userid && x.CourseID == courseid).ToList();
        }
    }
}
