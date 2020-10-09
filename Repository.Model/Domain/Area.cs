using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Domain
{
    public class Area:BaseEntity
    {
        public string Name { get; set; }
        public City City { get; set; }
        public int CityId { get; set; }
    }
}
