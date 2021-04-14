using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Domain
{
    public class Rework:BaseEntity
    {
        public User Operator { get; set; }
        public int? OperatorId { get; set; }

        public string Desc { get; set; }

        public User InsertedUser { get; set; }
        public int InsertedUserId { get; set; }

        public ICollection<ReworkDetail> ReworkDetails { get; set; }

        public Worksheet Worksheet { get; set; }
        public int? WorksheetId { get; set; }

        public int PrevProcessId { get; set; }

    }
}
