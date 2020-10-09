using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Domain
{
    public class ProcessCategory
    {
        public int Id { get; set; }

        public Category Category { get; set; }

        [Index("IX_CatPr", 1,IsUnique =true)]
        public int CategoryId { get; set; }

        public Process Process { get; set; }

        [Index("IX_CatPr", 2, IsUnique = true)]
        public int ProcessId { get; set; }

        public int Order { get; set; }

        public int ProcessTime { get; set; }

    }
}
