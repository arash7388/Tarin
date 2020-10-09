using Repository.Entity.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAL
{
    public class WorksheetRepository : BaseRepository<Worksheet>
    {
        public Worksheet GetByIdWithDetails(int id)
        {
            var det = DBContext.WorksheetDetails.Where(d => d.WorksheetId == id)
                    .Include(b => b.Product).ToList();

            var w = DBContext.Worksheets.SingleOrDefault(a => a.Id == id);
            w.WorksheetDetails = det;
            return w;
        }

        public int GetMaxId()
        {
            if (!DBContext.Worksheets.Any())
                return 0;

            var id = DBContext.Worksheets.Select(a => a.Id).Max();
            return id;
        }
        public object GetWorksheetForPrint(int id)
        {
            throw new NotImplementedException();
        }

        public List<int> GetWorksheetProcesses(int worksheetId)
        {
            var result = new List<int>();

            var w = DBContext.Worksheets.Include(a => a.WorksheetDetails)
                                .FirstOrDefault(a => a.Id == worksheetId);

            if (w == null)
            {
                return null;
            }

            foreach (WorksheetDetail d in w.WorksheetDetails)
            {
                var product = DBContext.Products.Include(a => a.ProductCategory).FirstOrDefault(a => a.Id == d.ProductId);
                var processesOfThisCat = DBContext.ProcessCategories.Where(a => a.CategoryId == product.ProductCategoryId);

                foreach (var p in processesOfThisCat)
                {
                    if (!result.Contains(p.ProcessId))
                        result.Add(p.ProcessId);
                }
            }

            result.Sort();

            return result;
        }

        public List<WorksheetReportHelper> GetAllForReport(Expression<Func<WorksheetReportHelper, bool>> whereClause = null)
        {

            var result = from w in DBContext.Worksheets
                         join o in DBContext.Users on w.OperatorId equals o.Id
                         join c in DBContext.Colors on w.ColorId equals c.Id
                         join d in DBContext.WorksheetDetails on w.Id equals d.WorksheetId
                         join p in DBContext.Products on d.ProductId equals p.Id
                         join cat in DBContext.Categories on p.ProductCategoryId equals cat.Id
                         select new WorksheetReportHelper
                         {
                             OperatorId = w.OperatorId,
                             InsertDateTime = w.InsertDateTime,
                             WorksheetId = w.Id,
                             Date = w.Date,
                             PartNo = w.PartNo,
                             WaxNo = w.WaxNo,
                             ProductCode = p.Code,
                             ProductName = cat.Name + "->" + p.Name,
                             CategoryName = cat.Name,
                             ACode = d.ACode,
                             ColorId = c.Id,
                             ColorName = c.Name,
                             OperatorName = o.FriendlyName,
                             //ACodes = w.WorksheetDetails.Select(a => a.ACode).Aggregate((m, n) => m + "," + n)
                         };

            if (whereClause != null)
                result = result.Where(whereClause);

            var resultList = result.ToList();
            resultList.ForEach(a => a.PersianDate = Common.Utility.CastToFaDate(a.InsertDateTime));

            return resultList;
        }

        public List<WorkLineSummary> GetWorksheetAllowedPrTimes(int wid)
        {
            var worksheetsDetails = from ws in DBContext.Worksheets
                                    join u in DBContext.Users on ws.OperatorId equals u.Id
                                    join wd in DBContext.WorksheetDetails on ws.Id equals wd.WorksheetId
                                    join p in DBContext.Products on wd.ProductId equals p.Id
                                    join cat in DBContext.Categories on p.ProductCategoryId equals cat.Id
                                    join pcat in DBContext.ProcessCategories on cat.Id equals pcat.CategoryId
                                    join pro in DBContext.Processes on pcat.ProcessId equals pro.Id
                                    where ws.Id==wid
                                    select new WorkLineHelper
                                    {
                                        WorksheetId = ws.Id,
                                        OperatorId = (int)ws.OperatorId,
                                        OperatorName = u.FriendlyName,
                                        ProductId = wd.ProductId,
                                        ProcessId = pro.Id,
                                        ProductCode = p.Code,
                                        ProductName = cat.Name + " " + p.Name,
                                        ProcessName = pro.Name,
                                        ProcessTime = pcat.ProcessTime,
                                        InsertDateTime = ws.InsertDateTime
                                    };

            var worksheetsDetailsList = worksheetsDetails.ToList();

            var operatorProcessAllowedTimeResult = from r in worksheetsDetailsList
                                                   group r by new { r.InsertDateTime, r.ProcessId, r.ProcessName, r.OperatorId, r.OperatorName } into g
                                                   select new WorkLineSummary
                                                   {
                                                       InsertDateTime = g.Key.InsertDateTime ?? DateTime.MinValue,
                                                       ProcessId = g.Key.ProcessId,
                                                       ProcessName = g.Key.ProcessName,
                                                       OperatorId = g.Key.OperatorId,
                                                       FriendlyName = g.Key.OperatorName,
                                                       ProcessTime = g.Sum(a => a.ProcessTime)
                                                   };

            return operatorProcessAllowedTimeResult.ToList();
        }

    }

    public class WorksheetReportHelper : WorksheetDetail
    {
        public DateTime Date { get; set; }
        public string PersianDate { get; set; }
        public string PartNo { get; set; }
        public string WaxNo { get; set; }
        public int ColorId { get; set; }
        public string OperatorName { get; set; }
        public int? OperatorId { get; set; }
        public string ColorName { get; set; }
        public string CategoryName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ACodes { get; set; }

    }

}
