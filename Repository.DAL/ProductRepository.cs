using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Common;
using Repository.Data;
using Repository.Entity.Domain;

namespace Repository.DAL
{
    public class ProductRepository:BaseRepository<Product>
    {
        public List<Product> GetByCatId(int catId)
        {
            return DbSet.Where(a => a.ProductCategoryId == catId).ToList();
        }

        public List<ProductHelper> GetAllWithCatName()
        {
            var r = from p in DBContext.Products
                    join c in DBContext.Categories.DefaultIfEmpty() on p.ProductCategoryId equals c.Id
                    select new ProductHelper
                    {
                        Id = p.Id,
                        Code = p.Code,
                        Name = p.Name,
                        InsertDateTime = p.InsertDateTime,
                        Status = p.Status,
                        CategoryName = c.Name,
                        ProductCategoryId = p.ProductCategoryId,
                        Image = p.Image
                    };

            return r.ToList();
        }

        public List<ProductHelper> GetAllWithCatNameForInOut()
        {
            var r = from p in DBContext.Products
                    join c in DBContext.Categories.DefaultIfEmpty() on p.ProductCategoryId equals c.Id
                    where p.IsInOutProduct ?? false
                    select new ProductHelper
                    {
                        Id = p.Id,
                        Code = p.Code,
                        Name = p.Name,
                        InsertDateTime = p.InsertDateTime,
                        Status = p.Status,
                        CategoryName = c.Name,
                        ProductCategoryId = p.ProductCategoryId,
                        Image = p.Image
                    };

            return r.ToList();
        }

        public List<ProductHelper> GetAllWithCatRecursive(bool isInOutMode=false)
        {
            var result =  DBContext.Products.Select(a => new ProductHelper()
            {
                Id = a.Id,
                Code = a.Code,
                Name =  a.Name,
                IsInOutProduct = a.IsInOutProduct,
                ProductCategoryId = a.ProductCategoryId
            }).ToList();

            if (isInOutMode)
                result = result.Where(a => a.IsInOutProduct ?? false).ToList();

            foreach (ProductHelper productHelper in result)
            {
                productHelper.Name = GetCatFullPath(productHelper.ProductCategoryId) + "->" + productHelper.Name;
            }
            
            return result;
        }

        private string GetCatFullPath(int id)
        {
            if (id == -1)
                return "";

            var cat = DBContext.Categories.Find(id);
            if (cat != null && cat.ParentId == null)
                return cat.Name;
            return  GetCatFullPath((int)cat.ParentId) + "->" + cat.Name ;
        }

        public List<ProductHelper> GetByCatIdWithCatName(int? catId=null)
        {
            var r = from p in DBContext.Products
                    join c in DBContext.Categories.DefaultIfEmpty() on p.ProductCategoryId equals c.Id
                select new ProductHelper
                {
                    Id = p.Id,
                    Code = p.Code,
                    Name = p.Name,
                    InsertDateTime = p.InsertDateTime,
                    Status = p.Status,
                    CategoryName = c.Name,
                    ProductCategoryId = p.ProductCategoryId,
                    Image = p.Image
                };

            if (catId != null)
                r = r.Where(a => a.ProductCategoryId == catId);

            return r.ToList();
        }

        public string GetCodeById(int pid)
        {
            var singleOrDefault = DBContext.Products.SingleOrDefault(a => a.Id == pid);
            if (singleOrDefault != null)
                return singleOrDefault.Code;

            return "";
        }

        public List<ProductSupplyHelper> GetProductSupply(Expression<Func<ProductSupplyHelper, bool>> filter)
        {
            var grouped = from d in DBContext.InputOutputDetails join i in DBContext.InputOutput
                          on d.InputOutputId equals  i.Id
            
            group d by  new {i.InOutType, d.ProductId}
                into x
            select new
            {
                x.Key,
                Supply = x.Key.InOutType==(int)InOutType.In ? x.Sum(a => a.Count) : x.Sum(a => a.Count*-1)
            };

            var lookup = grouped.ToLookup(g=>new { g.Key.ProductId});

            var list = new List<KeyValuePair<int,long>>();

            foreach (var a in lookup)
            {
                var supply = a.Select(b => b.Supply);
                long sumSup = 0;

                foreach (int s in supply)
                    sumSup += s;

                list.Add(new KeyValuePair<int, long>(a.Key.ProductId,sumSup));
            }

            var result = from g in list
                         join p in DBContext.Products on g.Key equals p.Id
                         join c in DBContext.Categories on p.ProductCategoryId equals c.Id
                         select new ProductSupplyHelper
                         {
                             Id = g.Key,
                             Code = p.Code,
                             Supply = g.Value,
                             ProductCategoryId = p.ProductCategoryId,
                             Name = p.Name,
                             ProductCategoryName = c.Name
                         };

            //if (filter != null)
            //    result = result.Where(filter);

            var productSupplyHelpers = result as IList<ProductSupplyHelper> ?? result.ToList();
            foreach (ProductSupplyHelper p in productSupplyHelpers.ToList())
            {
                p.Name = GetCatFullPath(p.ProductCategoryId) + "->" + p.Name;
            }

            return productSupplyHelpers.ToList();
        }

        public List<ProductSupplyHelper> GetProductSupplyWithCategory(Expression<Func<ProductSupplyHelper, bool>> filter)
        {
            var joined = from d in DBContext.InputOutputDetails
                join i in DBContext.InputOutput on d.InputOutputId equals i.Id
                join p in DBContext.Products on d.ProductId equals p.Id
                join c in DBContext.Categories on p.ProductCategoryId equals c.Id
                join c2 in DBContext.Categories on c.ParentId equals c2.Id
                into y
                from a in y.DefaultIfEmpty()
                select new Helper
                {
                    InOutType = i.InOutType,
                    CategoryId = c.Id,
                    CategoryCode = c.Code,
                    CategoryName = c.Name,
                    C2Id = a.Id ,
                    C2Name = a.Name,
                    C2Code = a.Code,
                    Count = d.Count
                };

                var grouped=  from j in joined 
                              group j by new { j.InOutType, j.CategoryId,j.CategoryCode, j.CategoryName, j.C2Id,j.C2Code, j.C2Name }
                into x
                              select new
                              {
                                  x.Key,
                                  x.Key.CategoryId,
                                  x.Key.InOutType,
                                  x.Key.C2Id,
                                  x.Key.C2Code,
                                  x.Key.C2Name,
                                  Name = x.Key.CategoryName,
                                  Supply = x.Key.InOutType == (int)InOutType.In ? x.Sum(a => a.Count) : x.Sum(a => a.Count * -1)
                              };

           var lookup = grouped.ToLookup(g => new { g.Key.CategoryId,g.Key.CategoryCode,g.Key.CategoryName,g.Key.C2Id,g.Key.C2Code, g.Key.C2Name });

            var list = new List<ProductSupplyHelper>();

            foreach (var l in lookup)
            {
                var supply = l.Select(b => b.Supply);
                long sumSup = 0;

                foreach (int s in supply)
                    sumSup += s;

                list.Add(new ProductSupplyHelper()
                {
                    Id = l.Key.C2Id??-1,
                    Code = l.Key.CategoryCode,
                    Supply = sumSup,
                    ProductCategoryId = l.Key.CategoryId,
                    Name = "",
                    ProductCategoryName = l.Key.C2Name
                });
            }

            foreach (ProductSupplyHelper p in list)
            {
                p.Name = GetCatFullPath(p.ProductCategoryId) + "->" + p.Name;
                p.Name = p.Name.Remove(p.Name.Length - 2, 2);
            }

            return list;
        }

        public List<ProductSupplyHelper> GetProductSupplyWithCategory2(Expression<Func<ProductSupplyHelper, bool>> filter)
        {
            var joined2 = from d in DBContext.InputOutputDetails
                         join i in DBContext.InputOutput on d.InputOutputId equals i.Id
                         join p in DBContext.Products on d.ProductId equals p.Id
                         join c in DBContext.Categories on p.ProductCategoryId equals c.Id
                         join c2 in DBContext.Categories on c.ParentId equals c2.Id
                         //join c3 in DBContext.Categories on c2.ParentId equals c3.Id
                         into y
                         from a in y.DefaultIfEmpty()
                         select new Helper
                         {
                             InOutType = i.InOutType,
                             CategoryId = c.Id,
                             CategoryCode = c.Code,
                             CategoryName = c.Name,
                             C2Id = a.Id,
                             C2Name = a.Name,
                             C2Code = a.Code,
                             C3Id = null,
                             C3Name = "",
                             C3Code = "",
                             Count = d.Count,
                             ParentId= a.ParentId
                         };

            var joined = from j in joined2
                          join c3 in DBContext.Categories on j.ParentId equals c3.Id
                          into y from a in y.DefaultIfEmpty()
                          select new Helper
                          {
                              InOutType = j.InOutType,
                              CategoryId = j.CategoryId,
                              CategoryCode = j.CategoryCode,
                              CategoryName = j.CategoryName,
                              C2Id = j.C2Id,
                              C2Name = j.C2Name,
                              C2Code = j.C2Code,
                              C3Id = a.Id,
                              C3Name = a.Name,
                              C3Code = a.Code,
                              Count = j.Count,
                              ParentId = a.ParentId
                          };

            var grouped = from j in joined
                          group j by new { j.InOutType,
                              j.C2Id,
                              j.C2Code,
                              j.C2Name,
                              j.C3Id, j.C3Code, j.C3Name,j.ParentId
                          }
            into x
                          select new
                          {
                              x.Key,
                              x.Key.InOutType,
                              x.Key.C2Id,
                              x.Key.C2Code,
                              x.Key.C2Name,
                              x.Key.C3Id,
                              x.Key.C3Code,
                              x.Key.C3Name,
                              x.Key.ParentId,
                              Supply = x.Key.InOutType == (int)InOutType.In ? x.Sum(a => a.Count) : x.Sum(a => a.Count * -1)
                          };

            var lookup = grouped.ToLookup(g => new { g.Key.C2Id, g.Key.C2Code, g.Key.C2Name,g.Key.C3Id, g.Key.C3Code, g.Key.C3Name,g.Key.ParentId });

            var list = new List<ProductSupplyHelper>();

            foreach (var l in lookup)
            {
                var supply = l.Select(b => b.Supply);
                long sumSup = 0;

                foreach (int s in supply)
                    sumSup += s;

                list.Add(new ProductSupplyHelper()
                {
                    Id = l.Key.C3Id ?? -1,
                    Code = l.Key.C3Code,
                    Supply = sumSup,
                    ProductCategoryId = l.Key.C2Id??-1,
                    Name = "",
                    //ProductCategoryName = l.Key.C3Name
                });
            }

            foreach (ProductSupplyHelper p in list)
            {
                p.Name = GetCatFullPath(p.ProductCategoryId) + "->" + p.Name;
                p.Name = p.Name.Remove(p.Name.Length - 2, 2);
            }

            return list;
        }
    }

    public class Helper
    {
        public int CategoryId { get; set; }
        public string CategoryCode { get; set; }
        public int InOutType { get; set; }
        public string CategoryName { get; set; }
        public int? C2Id { get; set; }
        public string C2Name { get; set; }
        public int Count { get; set; }
        public string C2Code { get; set; }
        public int? ParentId { get; set; }
        public int? C3Id { get; set; }
        public string C3Name { get; set; }
        public string C3Code { get; set; }
    }

    public class ProductHelper : Product
    {
        public string CategoryName { get; set; }
    }

    public class ProductSupplyHelper : Product
    {
        public long Supply { get; set; }

        public string ProductCategoryName { get; set; }
    }

    //public class KeySupply
    //{
    //    public Key Key { get; set; }
    //    public long Supply { get; set; }
    //}

    //public class Key
    //{
    //    public int ProductId { get; set; }
    //    public int InOut { get; set; }
    //}
}
