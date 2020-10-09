using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Domain
{
    public class CategoryProp:BaseEntity
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public int Type { get; set; }
        public bool HasDatasource { get; set; }
    }

    public enum CategoryPropType
    {
        Int,
        String,
        Decimal,
        Radio,
        Checkbox,
        Combo
    }
}
