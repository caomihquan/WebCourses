using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class BlogSaveDao
    {
        WebDbContext db = null;
        public BlogSaveDao()
        {
            db = new WebDbContext();
        }

        public IEnumerable<BlogSave> ListAllPaging(string searchString, int page, int pageSize,long userid)
        {
            IQueryable<BlogSave> model = db.BlogSaves.Where(x => x.Status == true && x.UserID==userid);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ID.ToString().Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
    }
}
