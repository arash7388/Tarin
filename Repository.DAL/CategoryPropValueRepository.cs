using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entity.Domain;

namespace Repository.DAL
{
    public class CategoryPropValueRepository : BaseRepository<CategoryPropValue>
    {
        public List<CategoryPropValueHelper> GetAllByOrder()
        {
            var result = from v in DBContext.CategoryPropValues
                join p in DBContext.CategoryProps on v.CategoryPropId equals p.Id
                join c in DBContext.Categories on p.CategoryId equals c.Id
                orderby c.Id, p.Id
                //select v;
                select new CategoryPropValueHelper()
                {
                    Id = v.Id,
                    Category = c,
                    CategoryProp = p,
                    Value = v.Value,
                    DeleteDateTime = v.DeleteDateTime,
                    InsertDateTime =v.InsertDateTime,
                    UpdateDateTime = v.UpdateDateTime,
                    Status = v.Status
                };

            return result.ToList();
        }

        public List<CategoryPropValue> GetPropValues(int propId)
        {
            var result = from v in DBContext.CategoryPropValues where v.CategoryPropId == propId select v;
                         
            return result.ToList();
        }
    }

    public class CategoryPropValueHelper:BaseEntity
    {
        public string Value { get; set; }
        public Category Category { get; set; }
        public CategoryProp CategoryProp { get; set; }
    }
}
