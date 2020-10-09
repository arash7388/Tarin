using Repository.Entity.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Map
{
    public class WorkLineMap : EntityTypeConfiguration<WorkLine>
    {
        public WorkLineMap()
        {

           // HasRequired(a => a.Worksheet)
           //.WithMany()
           //.WillCascadeOnDelete(false);

        }

    }
}
