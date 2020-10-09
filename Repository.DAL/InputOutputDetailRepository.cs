using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Common;
using Repository.Entity.Domain;

namespace Repository.DAL
{
    public class InputOutputDetailRepository : BaseRepository<InputOutputDetail>
    {
        public List<InputOutputDetailHelper> GetByFilter(Expression<Func<InputOutputDetailHelper, bool>> filter = null)
        {
            var res = from d in DBContext.InputOutputDetails
                    .Include(a => a.InputOutput)
                    .Include(a => a.Customer)
                    .Include(a => a.Product)
                    .Include(a => a.Product.ProductCategory)
                    .Include(a => a.Product.ProductCategory.Parent)
                select new InputOutputDetailHelper()
                {
                    Id = d.InputOutputId,
                    InputOutputId = d.InputOutputId,
                    InOutType = d.InputOutput.InOutType,
                    Product = d.Product,
                    ProductCategory = d.Product.ProductCategory,
                    ParentCategory =
                        d.Product.ProductCategory.ParentId != null ? d.Product.ProductCategory.Parent : null,
                    ProductId = d.ProductId,
                    Customer = d.Customer,
                    CustomerId = d.CustomerId,
                    Count = d.Count,
                    InsertDateTimeMasterFa = d.InputOutput.InsertDateTime.ToString(),
                    InsertDateTimeDetailFa = d.InsertDateTime.ToString(),
                    InsertDateTimeMaster = d.InputOutput.InsertDateTime,
                    //InsertDateTimeDetail = d.InsertDateTime.ToString()
                    InsertDateTimeDetail = d.InsertDateTime,
                    ProductionQuality = d.ProductionQuality,
                    ReceiptId = d.InputOutput.ReceiptId,
                    InputOutput = d.InputOutput

                };

            if (filter != null)
                res = res.Where(filter);

            return res.ToList();
        }

        public List<InputOutputDetailHelper> CardexReport(Expression<Func<InputOutputDetailHelper, bool>> filter = null)
        {
            var details = GetByFilter(filter);
            long supply = 0;

            var result = details.OrderBy(a => a.InsertDateTimeDetail).ThenBy(a => a.InOutType).Select(d =>
            {
                switch (d.InputOutput.InOutType)
                {
                    case (int)InOutType.In:
                        supply += d.Count;
                        break;

                    case (int)InOutType.Out:
                        supply -= d.Count;
                        break;
                }

                return new InputOutputDetailHelper()
                {
                    Id = d.InputOutputId,
                    InputOutputId = d.InputOutputId,
                    InOutType = d.InputOutput.InOutType,
                    Product = d.Product,
                    ProductCategory = d.Product.ProductCategory,
                    ParentCategory =
                    d.Product.ProductCategory.ParentId != null ? d.Product.ProductCategory.Parent : null,
                    ProductId = d.ProductId,
                    Customer = d.Customer,
                    CustomerId = d.CustomerId,
                    Count = d.Count,
                    InsertDateTimeMasterFa = d.InputOutput.InsertDateTime.ToString(),
                    InsertDateTimeDetailFa = d.InsertDateTime.ToString(),
                    InsertDateTimeMaster = d.InputOutput.InsertDateTime,
                    //InsertDateTimeDetail = d.InsertDateTime.ToString()
                    InsertDateTimeDetail = d.InsertDateTime,
                    ProductionQuality = d.ProductionQuality,
                    ReceiptId = d.InputOutput.ReceiptId,
                    RunningSupply = supply
                };
            });

            return result.ToList();
        }

        public List<InputOutputDetail> GetAllIns()
        {
            return
                DBContext.InputOutputDetails.Include(a => a.Product)
                    .Include(a => a.Customer)
                    .Where(a => a.InputOutput.InOutType == (int) InOutType.In)
                    .OrderByDescending(a => a.Id)
                    .ToList();
        }

        public List<InputOutputDetail> GetAllOuts()
        {
            return
                DBContext.InputOutputDetails.Include(a => a.Product)
                    .Include(a => a.Customer)
                    .Where(a => a.InputOutput.InOutType == (int) InOutType.Out)
                    .OrderByDescending(a => a.Id)
                    .ToList();
        }

        public int GetProductSupply(int pid, DateTime dateTime)
        {
            var totIn =
                from d in DBContext.InputOutputDetails.Where(a => a.ProductId == pid && a.InsertDateTime <= dateTime)
                join i in DBContext.InputOutput on d.InputOutputId equals i.Id
                where i.InOutType == (int) InOutType.In
                group d by d.ProductId
                into x
                select new
                {
                    Sum = x.Sum(a => a.Count)
                };

            var totOut =
                from d in DBContext.InputOutputDetails.Where(a => a.ProductId == pid && a.InsertDateTime <= dateTime)
                join i in DBContext.InputOutput on d.InputOutputId equals i.Id
                where i.InOutType == (int) InOutType.Out
                group d by d.ProductId
                into x
                select new
                {
                    Sum = x.Sum(a => a.Count)
                };

            var ii = totIn.FirstOrDefault();
            var oo = totOut.FirstOrDefault();

            var ti = ii?.Sum ?? 0;
            var to = oo?.Sum ?? 0;

            return ti - to;
        }

        public List<int> CheckSupplyValidationBeforeDeleteInput(int receiptId)
        {
            var negativePids = new List<int>();
            var r = DBContext.InputOutput.SingleOrDefault(a => a.Id == receiptId);
            foreach (InputOutputDetail tobeRemovedDetail in r.InputOutputDetails)
            {
                var listWithRunning = GetListWithRunningSupply((DateTime) r.InsertDateTime, tobeRemovedDetail);

                foreach (var o in listWithRunning.Where(a=>a.RunningSupply<0).ToList())
                {
                    if(!negativePids.Contains(o.ProductId))
                      negativePids.Add(o.ProductId);
                }
            }

            return negativePids;
        }

        //before insert between receipts, next outputs should stay valid
        public List<int> CheckSupplyForNextOutputsBeforeInsertOutput(int toBeInsertedCount, int pid,DateTime insertDateTime)
        {
            var negativePids = new List<int>();
            var receiptsToBeChecked =
                DBContext.InputOutputDetails.Include(a => a.InputOutput).Where(
                    a => a.InsertDateTime > insertDateTime
                         && a.ProductId == pid).ToList();

            int supply = GetProductSupply(pid, (DateTime)insertDateTime) - toBeInsertedCount;

            var orderedList = receiptsToBeChecked.OrderBy(a => a.InsertDateTime).ThenBy(a => a.InputOutput.InOutType)
                .Select(i =>
                {
                    switch (i.InputOutput.InOutType)
                    {
                        case (int)InOutType.In:
                            supply += i.Count;
                            break;

                        case (int)InOutType.Out:
                            supply -= i.Count;
                            break;
                    }

                    return new DetailWithRunningSupply
                    {
                        MasterId = i.InputOutput.Id,
                        DetailId = i.Id,
                        InsertDateTime = (DateTime)i.InsertDateTime,
                        ProductId = i.ProductId,
                        Type = (InOutType)i.InputOutput.InOutType,
                        Count = i.Count,
                        RunningSupply = supply
                    };
                }).ToList();

            foreach (var o in orderedList.Where(a => a.RunningSupply < 0).ToList())
            {
                if (!negativePids.Contains(o.ProductId))
                    negativePids.Add(o.ProductId);
            }
            return negativePids;
        }
        private List<DetailWithRunningSupply> GetListWithRunningSupply(DateTime insertDateTime, InputOutputDetail tobeRemovedDetail)
        {
            var receiptsToBeChecked =
                DBContext.InputOutputDetails.Include(a => a.InputOutput).Where(
                    a => a.InsertDateTime > insertDateTime
                         && a.ProductId == tobeRemovedDetail.ProductId).ToList();

            int supply = GetProductSupply(tobeRemovedDetail.ProductId, (DateTime) tobeRemovedDetail.InsertDateTime) -
                         tobeRemovedDetail.Count;

            var orderedList = receiptsToBeChecked.OrderBy(a => a.InsertDateTime).ThenBy(a => a.InputOutput.InOutType)
                .Select(i =>
                {
                    switch (i.InputOutput.InOutType)
                    {
                        case (int) InOutType.In:
                            supply += i.Count;
                            break;

                        case (int) InOutType.Out:
                            supply -= i.Count;
                            break;
                    }

                    return new DetailWithRunningSupply()
                    {
                        MasterId = i.InputOutput.Id,
                        DetailId = i.Id,
                        InsertDateTime = (DateTime) i.InsertDateTime,
                        ProductId = i.ProductId,
                        Type = (InOutType) i.InputOutput.InOutType,
                        Count = i.Count,
                        RunningSupply = supply
                    };
                }).ToList();

            return orderedList;
        }
    }

    internal class DetailWithRunningSupply
    {
        public int MasterId { get; set; }
        public int DetailId { get; set; }
        public DateTime InsertDateTime { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public long RunningSupply { get; set; }
        public InOutType Type { get; set; }
    }

    public class InputOutputDetailHelper : InputOutputDetail
        {
            private string _insertDateTimeMasterFa;

            public string InsertDateTimeMasterFa
            {
                get { return DateTime.Parse(_insertDateTimeMasterFa).ToFaDate(); }
                set { _insertDateTimeMasterFa = value; }
            }

            private string _insertDateTimeDetailFa;

            public string InsertDateTimeDetailFa
            {
                get { return DateTime.Parse(_insertDateTimeDetailFa).ToFaDate(); }
                set { _insertDateTimeDetailFa = value; }
            }

            public DateTime? InsertDateTimeMaster { get; set; }
            public DateTime? InsertDateTimeDetail { get; set; }
            public int InOutType { get; set; }
            public Category ProductCategory { get; set; }
            public DateTime InsertDateTimeMasterEn { get; set; }
            public DateTime InsertDateTimeDetailEn { get; set; }
            public Category ParentCategory { get; set; }

            public int? ReceiptId { get; set; }

            public long RunningSupply { get; set; }
        }
    }
