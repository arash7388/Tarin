using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entity.Domain;

namespace Repository.Entity.Map
{
    public class AdvertisementMap : EntityTypeConfiguration<Advertisement>
    {
        public AdvertisementMap()
        {
              HasOptional(a => a.AdvPic).WithMany()
                .HasForeignKey(u => u.AdvPicId)
                .WillCascadeOnDelete(false);
        }

        
    }
}
