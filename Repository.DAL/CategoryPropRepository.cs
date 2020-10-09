using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entity.Domain;

namespace Repository.DAL
{
    public class CategoryPropRepository:BaseRepository<CategoryProp>
    {
        //public List<KeyValuePair<int, string>> GetAllIdName()
        //{
        //    var catProps = from c in DBContext.CategoryProps
        //               select new
        //               {
        //                   Key = c.Id,
        //                   Value = c.Name
        //               };

        //    var result = new List<KeyValuePair<int, string>>();

        //    foreach (var item in catProps)
        //        result.Add(new KeyValuePair<int, string>(item.Key, item.Value));

        //    return result;
        //}

        public List<CategoryProp> GetByCatId(int catId)
        {
            var result = from p in DBContext.CategoryProps where p.CategoryId == catId select p;

            return result.ToList();
        }

        public List<CategoryProp> GetPropHaveDatasourceByCatId(int catId)
        {
            var result = from p in DBContext.CategoryProps where p.CategoryId == catId && p.HasDatasource select p;

            return result.ToList();
        }
    }
}
