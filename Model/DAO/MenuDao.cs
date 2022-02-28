using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class MenuDao
    {
        WebDbContext db = null;
        public MenuDao()
        {
            db = new WebDbContext();
        }
        public Footer GetFooter()
        {
            return db.Footers.SingleOrDefault(x => x.Status == true);
        }
        public List<Menu> ListByGroupId(int groupid)
        {
            return db.Menus.Where(x =>x.TypeID==groupid && x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }

    }
}
