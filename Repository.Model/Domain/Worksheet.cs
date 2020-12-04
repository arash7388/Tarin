using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Domain
{
    public class Worksheet : BaseEntity
    {
        public User User { get; set; }
        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public string PartNo { get; set; }
        public string WaxNo { get; set; }

        public Color Color { get; set; }
        public int ColorId { get; set; }

        public User Operator { get; set; }
        public int? OperatorId { get; set; }

        public virtual ICollection<WorksheetDetail> WorksheetDetails { get; set; }
        public string Desc { get; set; }
    }
}
