using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Domain
{
    public class WorkLine: BaseEntity
    {
        public Worksheet Worksheet { get; set; }
        [Index("IX_Workline", 1,IsUnique =true)]
        public int? WorksheetId { get; set; }

        public Process Process { get; set; }
        [Index("IX_Workline", 2, IsUnique = true)]
        public int ProcessId { get; set; }

        public User Operator { get; set; }
        [Index("IX_Workline", 3, IsUnique = true)]
        public int OperatorId { get; set; }
        public bool? Manual { get; set; }

    }
}
