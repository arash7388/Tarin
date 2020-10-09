using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Repository.Entity.Domain;

namespace Repository.DAL
{
    public class InputOutputRepository : BaseRepository<InputOutput>
    {
        public InputOutput GetByIdWithDetails(int id)
        {
            var det =
                DBContext.InputOutputDetails.Where(d => d.InputOutputId == id)
                    .Include(b => b.Product).ToList();

            var i = DBContext.InputOutput.SingleOrDefault(a => a.Id == id);
            i.InputOutputDetails = det;
            return i;
        }

        public List<InputOutput> GetAllIns()
        {
            return DBContext.InputOutput.Where(a => a.InOutType == (int) InOutType.In).ToList();
        }

        public List<InputOutput> GetAllOuts()
        {
            return DBContext.InputOutput.Where(a => a.InOutType == (int)InOutType.Out).ToList();
        }
    }
}
