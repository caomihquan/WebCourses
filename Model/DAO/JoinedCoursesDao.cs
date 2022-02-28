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
        public List<JoinedCours> countlessson(long top)
        {
            return db.JoinedCourses.Where(x => x.CourseID == top).ToList();
        }
    }
}
