using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Domain
{
    public class ReworkDetail:BaseEntity
    {
        public Rework Rework { get; set; }
        public int ReworkId { get; set; }
        public string ACode { get; set; }
        public string Desc { get; set; }
        public ReworkReason ReworkReason { get; set; }
        public int ReworkReasonId { get; set; }


    }
}
