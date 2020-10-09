using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Domain
{
    public class InputOutput:BaseEntity
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<InputOutputDetail> InputOutputDetails  { get; set; }
        public int InOutType { get; set; }
        public int? ReceiptId { get; set; }
    }
}
