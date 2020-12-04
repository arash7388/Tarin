using System.ComponentModel;

namespace Repository.Entity.Domain
{

    public class User:BaseEntity
    {
        [DisplayName("نام کاربری")]
        public string Username { get; set; }
        
        [DisplayName("کلمه عبور")]
        public string Password { get; set; }

        [DisplayName("نام مستعار")]
        public string FriendlyName { get; set; }

        [DisplayName("نوع")]
        public int Type { get; set; }

        [DisplayName("رمز دوباره کاری/اسقاط")]
        public string ReworkPassword { get; set; }
    }
}
