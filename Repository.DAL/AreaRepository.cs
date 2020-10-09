using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entity.Domain;

namespace Repository.DAL
{
    public class AreaRepository:BaseRepository<Area>
    {
        public List<Area> GetAllByOrder()
        {
            var res = DBContext.Areas.Include("City").OrderBy(a => a.City.Name).ThenBy(a => a.Name);

            return res.ToList();
        }
    }
}
