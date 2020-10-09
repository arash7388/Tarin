using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entity.Domain;

namespace Repository.Entity.Map
{
    public class AdvertisementPropValuesMap:EntityTypeConfiguration<AdvertisementPropValues>
    {
        public AdvertisementPropValuesMap()
        {
            //HasRequired(a => a.Advertisement).WithMany()
            //    .HasForeignKey(u => u.AdvertisementId)
            //    .WillCascadeOnDelete(false);

            HasRequired(a => a.CategoryProp).WithMany()
                .HasForeignKey(u => u.CategoryPropId)
                .WillCascadeOnDelete(false);
        }
    }
}
