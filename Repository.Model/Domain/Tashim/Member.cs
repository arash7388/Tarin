using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Domain.Tashim
{
    public class Member : BaseEntity
    {
        [DisplayName("نام")]
        public string Name { get; set; }

        [DisplayName("کد")]
        public string Code { get; set; }

        public byte Type { get; set; }

        public long ShareAmount { get; set; }
        public long ShareCount { get; set; }

    }
}
