using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Domain
{
    public class EsghatDetail:BaseEntity
    {
        public Esghat Esghat { get; set; }
        public int EsghatId { get; set; }
        public string ACode { get; set; }
        public string Desc { get; set; }

        public ReworkReason ReworkReason { get; set; }
        public int ReworkReasonId { get; set; }


    }
}
