using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ProgressLessonDao
    {
        WebDbContext db = null;
        public ProgressLessonDao()
        {
            db = new WebDbContext();
        }

        public List<ProgressLesson> LessonByUser(long userid,long courseid)
        {
            return db.ProgressLessons.Where(x => x.UserID == userid && x.CourseID == courseid).ToList();
        }
        public Certificate CertificatebyCourse(long courseid)
        {
            return db.Certificates.Where(x => x.IDCourse == courseid).SingleOrDefault();
        }

        public long InsertCertificateOwned(CertificateOwned entity)
        {
            db.CertificateOwneds.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
    }
}
