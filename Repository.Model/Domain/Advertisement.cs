using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Domain
{
    public class Advertisement : BaseEntity
    {

        public string Name { get; set; }
        public DateTime? RegDate { get; set; }
        public DateTime? ExpDate { get; set; }
        //ValidationPeriod{ get; set; }
        public string Mobile { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public virtual ICollection<AdvertisementPic> AdvertisementPics { get; set; }

        public virtual ICollection<AdvertisementPropValues> AdvertisementPropValues { get; set; }

        public string AdvMobile { get; set; }
        public string AdvTel { get; set; }
        public string AdvEmail { get; set; }
        public string AdvWebsite { get; set; }
        public string AdvCity { get; set; }

        public int AreaId { get; set; }
        public Area Area { get; set; }
        public string AdvAddress { get; set; }

        //public int? TagId { get; set; }
        //public Tag Tag { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string GPSLocation { get; set; }
        public string UserIP { get; set; }

        public bool HideEmail { get; set; }

    }
}
