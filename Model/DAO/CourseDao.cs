using Common;
using Model.EF;
using Model.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class CourseDao
    {
        WebDbContext db = null;
        public CourseDao()
        {
            db = new WebDbContext();
        }
        public List<Cours> ListAll()
        {
            return db.Courses.ToList();
        }
        public List<Cours> countcourse(long top)
        {
            return db.Courses.Where(x => x.CategoryID == top).ToList();
        }

        public List<Cours> ListFeatureCourses(int top)
        {
            return db.Courses.Where(x => x.Status == true).OrderByDescending(x => x.ViewCount).Take(top).ToList();
        }
        public Cours ViewDetail(long id)
        {
            return db.Courses.Find(id);
        }



        public long Insert(Cours entity)
        {
            db.Courses.Add(entity);
            if (string.IsNullOrEmpty(entity.MetaTitle))
            {
                entity.MetaTitle = StringHelper.ToUnsignString(entity.Name);
            }
            entity.CreatedDate = DateTime.Now;
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Cours entity)
        {
            try
            {
                var courses = db.Courses.Find(entity.ID);
                courses.Name = entity.Name;
                if (string.IsNullOrEmpty(entity.MetaTitle))
                {
                    courses.MetaTitle = StringHelper.ToUnsignString(entity.Name);
                }
                else
                {
                courses.MetaTitle = entity.MetaTitle;

                }
                courses.Image = entity.Image;
                courses.Description = entity.Description;
                courses.CategoryID = entity.CategoryID;
                courses.VideoOverview = entity.VideoOverview;
                courses.LevelCourse = entity.LevelCourse;
                courses.Overview = entity.Overview;
                courses.Price = entity.Price;
                courses.SeoTitle = entity.SeoTitle;
                courses.ModifiedBy = entity.ModifiedBy;
                courses.ModifiedDate = DateTime.Now;
                courses.ViewCount = entity.ViewCount;
                courses.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public IEnumerable<Cours> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Cours> model = db.Courses;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ID.ToString().Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }


        public bool Delete(int id)
        {
            try
            {
                var category = db.Courses.Find(id);
                db.Courses.Remove(category);
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
