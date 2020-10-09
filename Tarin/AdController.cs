using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using Common;
using Controller;
using Repository.DAL;
using Repository.Entity.Domain;
using Shared;
using Repository.Entity.ViewModels;

namespace Tarin
{
    //public class AdController : ApiController
    //{
       
    //    public List<AdsAndCats> GetAds()
    //    {

    //        CommonController commonController = HttpContext.Current.Session["CommonController"] as CommonController;
    //        AdsAndCats ads = commonController.ExecuteOperation(Shared.OperationType.Read, null) as AdsAndCats;

    //        List<AdsAndCats> list = new List<AdsAndCats>();

    //        list.Add(ads);
            
    //        return list;
    //    }

    //    public List<AdsAndCats> Post([FromBody] Object category)
    //    {
    //        CommonController commonController = HttpContext.Current.Session["CommonController"] as CommonController;
    //        Dictionary<String, Object> data = new Dictionary<String, Object>();
    //        data.Add("category", category);
    //        AdsAndCats ads = commonController.ExecuteOperation(Shared.OperationType.ReadByCategory, data) as AdsAndCats;

    //        List<AdsAndCats> list = new List<AdsAndCats>();

    //        list.Add(ads);

    //        return list;
    //    }
    //}

  

    public class CategoryController : ApiController
    {
        [HttpGet]
        public IEnumerable<Category> GetByParentId(int parentId)
        {
            var res = new CategoryRepository().GetAllByParentId(parentId).ToList();
            return res;
        }
        
    }

    public class AdvertisementController : ApiController
    {
        [HttpGet]
        public IEnumerable<Advertisement> GetByCategory(int catId, int pageNumber, int pageSize)
        {
            var res = new AdvertisementRepository().GetByPageIndex(catId,pageNumber, pageSize).ToList();
            return res;
        }

    }
    //public class Test
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }

    //    public Test()
    //    {
    //    }

    //    public Test(int id, string name)
    //    {
    //        Id = id;
    //        Name = name;
    //    }
    //}
}