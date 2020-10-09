using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Domain
{
    public class Category:BaseEntity
    {
        [DisplayName("نام")]
        public string  Name { get; set; }

        [DisplayName("کد")]
        public string Code { get; set; }

        [DisplayName("شناسه گروه والد")]
        public int? ParentId { get; set; }
        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> Children { get; set; }
        
        [DisplayName("تصویر")]
        public byte[] Image { get; set; }

        public bool? IsInOutCategory { get; set; }
    }
}
