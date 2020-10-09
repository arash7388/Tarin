using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Domain
{
    public class AdvertisementPropValues:BaseEntity
    {
        public int AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }

        public int CategoryPropId { get; set; }
        public CategoryProp CategoryProp { get; set; }

        public string Value { get; set; }
    }
}
