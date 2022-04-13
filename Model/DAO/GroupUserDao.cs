using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class GroupUserDao
    {
        WebDbContext db = null;
        public GroupUserDao()
        {
            db = new WebDbContext();
        }

        public List<UserGroup> ListAll()
        {
            return db.UserGroups.ToList();
        }

        public bool Insert(UserGroup entity)
        {
            try
            {
                db.UserGroups.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
            
        }

        public bool Update(UserGroup entity)
        {
            try
            {
                var usergroup = db.UserGroups.Find(entity.ID);
                usergroup.Name = entity.Name;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


        public UserGroup ViewDetail(string id)
        {
            return db.UserGroups.Find(id);
        }
        public IEnumerable<UserGroup> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<UserGroup> model = db.UserGroups;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ID.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }

        public bool Delete(string id)
        {
            try
            {
                var usergroup = db.UserGroups.Find(id);
                db.UserGroups.Remove(usergroup);
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
