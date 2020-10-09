using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Common;
using Repository.Data.Migrations;
using Repository.Entity.Domain;

namespace Repository.DAL
{
    public class ProcessCategoryRepository:BaseRepository<ProcessCategory>
    {
        public List<ProcessCategoryHelper> GetByCatIdWithDetails(int catId)
        {
            var result = (from pc in DBContext.ProcessCategories
                          join p in DBContext.Processes on pc.ProcessId equals p.Id
                          join c in DBContext.Categories on pc.CategoryId equals c.Id
                          where pc.CategoryId==catId
                         
                          select new ProcessCategoryHelper
                          {
                              Id = pc.Id,
                              CategoryId = pc.CategoryId,
                              CategoryName = c.Name,
                              ProcessId = pc.ProcessId,
                              ProcessName = p.Name,
                              Order = pc.Order,
                              ProcessTime = pc.ProcessTime
                          }).ToList();

            return result;
        }

        public List<ProCatHelper> GetGroupedList()
        {
            var catRepo = new CategoryRepository();

            var result = (from pc in DBContext.ProcessCategories
                         join p in DBContext.Processes on pc.ProcessId equals p.Id
                         join c in DBContext.Categories on pc.CategoryId equals c.Id
                         group pc by new { pc.CategoryId, c.Name } into g
                         select new ProCatHelper
                         {
                             CategoryId = g.Key.CategoryId,
                             CategoryName =  g.Key.Name,
                             //ProcessId=p.Id,
                             //ProcessNames= string.Join(",", DBContext.ProcessCategories.Include("Process").Where(a=>a.CategoryId== g.Key.CategoryId).Select(a=>a.Process.Name).ToArray())
                             //ProcessNames = DBContext.ProcessCategories.Select(a => a.ProcessId.ToString()).FirstOrDefault()
                         }).ToList();

            foreach(var r in result)
            {
                r.ProcessNames = string.Join("," , DBContext.ProcessCategories.Include("Process").Where(a => a.CategoryId == r.CategoryId).Select(a => a.Process.Name).ToArray());
                r.CategoryName = catRepo.GetFullName(r.CategoryId);
            }

            return result.ToList();
        }

        public List<ProcessCategoryHelper> GetCatProcessTimingList()
        {
            var catRepo = new CategoryRepository();

            var result = (from pc in DBContext.ProcessCategories
                          join p in DBContext.Processes on pc.ProcessId equals p.Id
                          join c in DBContext.Categories on pc.CategoryId equals c.Id
                          select new ProcessCategoryHelper
                          {
                              Id = pc.Id,
                              CategoryId = c.Id,
                              CategoryName = c.Name,
                              ProcessId= p.Id,
                              ProcessName= p.Name,
                              ProcessTime=pc.ProcessTime
                          }).ToList();

            return result.ToList();
        }
    }

    public class ProCatHelper
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string ProcessNames { get; set; }
    }

    public class ProcessCategoryHelper
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int ProcessId { get; set; }
        public string ProcessName { get; set; }

        public int Order { get; set; }
        public int ProcessTime { get; set; }
    }
}
