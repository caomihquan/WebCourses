using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class RequireActiveCourseDao
    {
        WebDbContext db = null;
        public RequireActiveCourseDao()
        {
            db = new WebDbContext();
        }
        public IEnumerable<CourseActive> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<CourseActive> model = db.CourseActives;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserID.ToString().Contains(searchString) || x.TransactionID.ToString().Contains(searchString) || x.CourseID.ToString().Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public bool ChangeStatus(long id)
        {
            var user = db.CourseActives.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }
        public bool Delete(int id)
        {
            try
            {
                var category = db.CourseActives.Find(id);
                db.CourseActives.Remove(category);
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
