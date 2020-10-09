namespace Repository.Entity.Domain
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    
    public class Project : BaseEntity
    {
        [DisplayName("کد")]
        public int Code { get; set; }
      
        [DisplayName("نام")]
        [MaxLength(100) ]
        public string Name { get; set; }
    }
}
