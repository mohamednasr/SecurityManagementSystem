using MNS.Repository;
using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class BlackListEntity : BaseEntity<long>
    {
        [Display(Name = "#")]
        public long? Ser { get; set; }

        [Display(Name = "الأسم")]
        public string Name { get; set; }

        [Display(Name = "الشركة")]
        public string Company { get; set; }

        [Display(Name = "الوظيفة")]
        public string Job { get; set; }

        [Display(Name = "الرقم القومي")]
        public string Nat_Id { get; set; }

        [Display(Name = "السبب")]
        public string Reason { get; set; }
    }
}
