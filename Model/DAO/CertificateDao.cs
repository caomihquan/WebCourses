using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class CertificateDao
    {
        WebDbContext db = null;
        public CertificateDao()
        {
            db = new WebDbContext();
        }

        public bool CheckCertificate(long userid, long certificate)
        {
            return db.CertificateOwneds.Count(x => x.UserID == userid && x.CertificateID == certificate) > 0;
        }
        public bool CheckChungchi(long certificateid)
        {
            return db.Certificates.Count(x => x.IDCourse == certificateid) > 0;
        }

        public long Insert(Certificate entity)
        {
            db.Certificates.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Certificate entity)
        {
            try
            {
                var categoryblog = db.Certificates.Find(entity.ID);
                categoryblog.Name = entity.Name;
                categoryblog.IDCourse = entity.IDCourse;
                categoryblog.Image = entity.Image;
                categoryblog.Logo = entity.Logo;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public IEnumerable<Certificate> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Certificate> model = db.Certificates;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ID.ToString().Contains(searchString) || x.Name.Contains(searchString) || x.IDCourse.ToString().Contains(searchString));
            }
            return model.OrderByDescending(x => x.IDCourse).ToPagedList(page, pageSize);
        }


        public bool Delete(int id)
        {
            try
            {
                var category = db.Certificates.Find(id);
                db.Certificates.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<CertificateOwned> ListAll(long userid)
        {
            return db.CertificateOwneds.Where(x=>x.UserID==userid).ToList();
        }
        public Certificate ViewDetail(long id)
        {
            return db.Certificates.Find(id);
        }
    }
}
