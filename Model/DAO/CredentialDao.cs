using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class CredentialDao
    {
        WebDbContext db = null;
        public CredentialDao()
        {
            db = new WebDbContext();
        }
        public Credential ViewDetail(string roleid, string usergroup)
        {
            return db.Credentials.Find(roleid,usergroup);
        }
        public Credential GetByID(string roleid,string usergroup)
        {
            return db.Credentials.SingleOrDefault(x => x.RoleID == roleid && x.UserGroupID==usergroup);
        }
        public List<Role> ListAllRole()
        {
            return db.Roles.ToList();
        }

        public bool Insert(Credential entity)
        {
            try { 
            db.Credentials.Add(entity);
            db.SaveChanges();
            return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool CheckQuyen(string roleid, string group)
        {
            return db.Credentials.Count(x => x.RoleID == roleid && x.UserGroupID == group) > 0;
        }

        public bool InsertUpdateCrendential(Credential credential)
        {

            Object[] param ={
                new SqlParameter("@RoleID",credential.RoleID),
                new SqlParameter("@UserGroupID",credential.UserGroupID)
            };

            int result = db.Database.ExecuteSqlCommand("PSP_InsertUpdateCredential @RoleID,@UserGroupID", param);
            if (result >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public IEnumerable<Credential> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Credential> model = db.Credentials;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.RoleID.Contains(searchString) || x.UserGroupID.Contains(searchString));
            }
            return model.OrderByDescending(x => x.RoleID).ToPagedList(page, pageSize);
        }


        public bool Delete(string roleid,string usergroup)
        {
            try
            {

                var credential = db.Credentials.SingleOrDefault(x => x.RoleID == roleid && x.UserGroupID ==usergroup);
                db.Credentials.Remove(credential);
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
