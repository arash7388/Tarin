using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Domain.Tashim
{
    public class ShareDivisionDetail : BaseEntity
    {
        public int MemberId { get; set; }
        public Member Member { get; set; }

        public long ShareAmount { get; set; }

        public int ShareDivisionId { get; set; }
        public ShareDivision ShareDivision { get; set; }

    }
}
