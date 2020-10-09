using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entity.Domain;
namespace Repository.Entity.ViewModels
{
    public class AdsAndCats
    {
        public AdsAndCats()
        {

        }
        public List<Advertisement> Ads { set; get; }
        public List<Category> Cats { set; get; }
    }
}
