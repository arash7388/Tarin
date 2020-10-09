using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Domain
{
    public class WorksheetDetail:BaseEntity
    {
        public Worksheet Worksheet { get; set; }
        public int WorksheetId { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }
        public string ACode { get; set; }
    }
}
