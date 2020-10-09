using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Domain
{
    public class CategoryPropValue:BaseEntity
    {
        [NotMapped]
        public virtual Category Category { get; set; }
        [NotMapped]
        public int CategoryId { get; set; }
        public CategoryProp CategoryProp { get; set; }
        public int CategoryPropId { get; set; }
        public string Value { get; set; }
    }
}
