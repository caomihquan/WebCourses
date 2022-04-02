using Common;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class CategoryDao
    {
        WebDbContext db = null;
        public CategoryDao()
        {
            db = new WebDbContext();
        }
        public Category CategoryCourses(long? id)
        {
            return db.Categories.Where(x=>x.ID == id).SingleOrDefault();
        }

        public Category ViewDetail(long id)
        {
            var model = db.Categories.Find(id);
            return model;
        }

        public Category ViewDetailout(long id)
        {
            var model = db.Categories.Find(id);
            model.ViewCount++;
            db.SaveChanges();
            return model;
        }
        public List<Category> ListPopularSubjects(int top)
        {
            return db.Categories.Where(x => x.Status == true && x.ParentsID!=null).OrderByDescending(x => x.ViewCount).Take(top).ToList();
        }
        public List<Category> ListAll()
        {
            return db.Categories.ToList();
        }

        public long Insert(Category entity)
        {
            db.Categories.Add(entity);
            if (string.IsNullOrEmpty(entity.MetaTitle))
            {
                entity.MetaTitle = StringHelper.ToUnsignString(entity.Name);
            }
            entity.CreatedDate = DateTime.Now;
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Category entity)
        {
            try
            {
                var category = db.Categories.Find(entity.ID);
                category.Name = entity.Name;
                if (string.IsNullOrEmpty(entity.MetaTitle))
                {
                    category.MetaTitle = StringHelper.ToUnsignString(entity.Name);
                }
                else
                {
                category.MetaTitle= entity.MetaTitle;
                }
                category.Image = entity.Image;
                category.ParentsID = entity.ParentsID;
                category.DisplayOrder = entity.DisplayOrder;
                category.Overview = entity.Overview;
                category.ModifiedBy = entity.ModifiedBy;
                category.ModifiedDate = DateTime.Now;
                category.Description = entity.Description;
                category.SeoTitle = entity.SeoTitle;
                category.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories;
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
                var category = db.Categories.Find(id);
                db.Categories.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


        public List<Cours> ListByCategoryId(long categoryID, ref int totalRecord, int page = 1, int pageSize = 2)
        {
            totalRecord = db.Courses.Where(x => x.CategoryID == categoryID).Count();
            var model = db.Courses.Where(x => x.CategoryID == categoryID).OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return model;

        }
    }
}
