using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entity.Domain;

namespace Repository.DAL
{
    public class CityRepository:BaseRepository<City>
    {
        public List<City> GetAllByOrder()
        {
            var result = from c in DBContext.Cities orderby c.Name  select c ;
            return result.ToList();
        }
    }
}
