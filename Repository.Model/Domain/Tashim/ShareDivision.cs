using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Domain.Tashim
{
    public class ShareDivision : BaseEntity
    {
        public byte Type { get; set; }
        public long Amount { get; set; }

        public int SharePercent { get; set; }
        public int EqualPercent { get; set; }
        public int PriorityPercent { get; set; }
        public ICollection<ShareDivisionDetail> ShareDivisionDetails { get; set; }

    }
}
