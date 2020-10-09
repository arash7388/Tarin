using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Repository.Entity.Domain;
using Repository.Entity.ViewModels;

namespace Repository.DAL
{
    public class AdvertisementRepository : BaseRepository<Advertisement>
    {
        public static short pageSize = 24;
        public AdsAndCats ReadAdvertisementsData()
        {
            AdsAndCats result = new AdsAndCats();
            result.Ads = (from c in MTOContext.Advertisements orderby c.Id select c).Take(pageSize).ToList();
            result.Cats = (from c in MTOContext.Categories orderby c.Name where c.ParentId == null select c).ToList();


            Category backItem = new Category();
            backItem.Name = "همه آگهی ها";
            backItem.Id = -1;
            result.Cats.Insert(0, backItem);

            return result;


        }

        public List<Advertisement> ReadAdvertisementById(Dictionary<String, Object> data)
        {

            List<Advertisement> returnData = new List<Advertisement>();

            Int32 id = Convert.ToInt32(data["id"]);

            returnData = MTOContext.Advertisements.Where(ad => ad.Id == id).ToList();
            return returnData;
        }

        public AdsAndCats ReadBookByCategory(Dictionary<String, Object> data)
        {
            AdsAndCats result = new AdsAndCats();
            
            Category deserializedCategory = JsonConvert.DeserializeObject<Category>(data["category"].ToString());
            short pageIndex;

            int? category = deserializedCategory.Id;
            if (category == 0)
            {
                category = null;
            }
            
            try
            {
                pageIndex = deserializedCategory.Status.Value;

                if (pageIndex > 0)
                {
                    if (category == null)
                    {
                        result.Ads = (from c in MTOContext.Advertisements orderby c.Id select c).Skip(pageIndex).Take(pageSize).ToList();
                        return result;
                    }
                    Category selectedCat = (from c in MTOContext.Categories where c.Id == category.Value select c).FirstOrDefault();
                    var catIds = FindAllSubCats(selectedCat);
                    result.Ads = (from c in MTOContext.Advertisements orderby c.Id where catIds.Contains(c.CategoryId) select c).Skip(pageIndex).Take(pageSize).ToList();
                    return result;
                }
            }
            catch
            {
                pageIndex = 0;
            }


            result.Cats = (from c in MTOContext.Categories orderby c.Id where c.ParentId == category select c).ToList();

            if (category != null && result.Cats.Count > 0)
            {

                Category backItem = new Category();
                backItem.Name = "بازگشت";
                backItem.Id = deserializedCategory.ParentId != null ? deserializedCategory.ParentId.Value : 0;

                result.Cats.Insert(0, backItem);
                
                Category selectedCat = (from c in MTOContext.Categories where c.Id == category.Value select c).FirstOrDefault();
                var catIds = FindAllSubCats(selectedCat);
                result.Ads = (from c in MTOContext.Advertisements orderby c.Id where catIds.Contains(c.CategoryId) select c).Take(pageSize).ToList();


            }
            else
            {
                if (category == null)
                {
                    Category backItem = new Category();
                    backItem.Name = "همه آگهی ها";
                    backItem.Id = -1;
                    result.Cats.Insert(0, backItem);
                    result.Ads = (from c in MTOContext.Advertisements orderby c.Id select c).Take(pageSize).ToList();
                }
                else
                {
                    result.Ads = (from c in MTOContext.Advertisements orderby c.Id where c.CategoryId == category select c).Take(pageSize).ToList();
                }
            }


            return result;
        }

        public static IEnumerable<int> FindAllSubCats(Category node)
        {
            yield return node.Id;

            if (node.Children != null)
            {
                foreach (var child in node.Children)
                    foreach (var descendant in FindAllSubCats(child))
                        yield return descendant;
            }
        }

        public List<Category> ReadMenus()
        {
            List<Category> returnData = new List<Category>();
            
            returnData = MTOContext.Categories.Where(menu => menu.Parent == null).ToList();

            return returnData;
        }

        public List<Advertisement> Get()
        {
            var res = MTOContext.Advertisements.OrderBy(a => a.Category);

            return res.ToList();
        }

        public List<Advertisement> GetByPageIndex(int catId,int pageIndex,int pageSize)
        {
            if(catId!=0)
                return MTOContext.Advertisements.OrderByDescending(i => i.Id).Where(a=>a.CategoryId==catId).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            
                return MTOContext.Advertisements.OrderByDescending(i => i.Id).Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        public List<int> GetTestData()
        {
           List<int> res = new List<int>();

            res.Add(10);
            res.Add(11);
            res.Add(12);
            res.Add(13);
            res.Add(14);


            return res;
        }
    }
}
