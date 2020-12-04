using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Domain
{
    public class Rework:BaseEntity
    {
        public string ACode { get; set; }

        public User Operator { get; set; }
        public int? OperatorId { get; set; }

        public ReworkReason ReworkReason { get; set; }
        public int ReworkReasonId { get; set; }

        public string Desc { get; set; }

        public User InsertedUser { get; set; }
        public int InsertedUserId { get; set; }

    }
}
