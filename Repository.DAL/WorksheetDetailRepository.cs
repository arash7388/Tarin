using Repository.Entity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAL
{
    public class WorksheetDetailRepository : BaseRepository<WorksheetDetail>
    {
        public List<WorksheetDetailHelper> GetAllDetails(int wid)
        {
            return DBContext.WorksheetDetails.Include("Product").Where(a => a.WorksheetId == wid).Select(a => new WorksheetDetailHelper
            {
                Id = a.Id,
                ProductId = a.ProductId,
                ProductCode = a.Product.Code,
                ProductName = a.Product.Name,
                ACode=a.ACode
                
            }).ToList();
        }
    }

    public class WorksheetDetailHelper
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ACode { get; set; }


    }

}
