using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Domain
{
    public class InputOutputDetail:BaseEntity
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }

        public Customer Customer { get; set; }
        public int? CustomerId { get; set; }

        public int Count { get; set; }

        public InputOutput InputOutput { get; set; }
        public int InputOutputId { get; set; }

        public int? ProductionQuality { get; set; }
    }
}
