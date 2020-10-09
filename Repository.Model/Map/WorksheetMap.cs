using Repository.Entity.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Map
{
    public class WorksheetMap: EntityTypeConfiguration<Worksheet>
    {
        public WorksheetMap()
        {
            //HasRequired(a => a.Operator).WithMany()
            //   .HasForeignKey(u => u.OperatorId)
            //   .WillCascadeOnDelete(false);
        }
       
    }
}
