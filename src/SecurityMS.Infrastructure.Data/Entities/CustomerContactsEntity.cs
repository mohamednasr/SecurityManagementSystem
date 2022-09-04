using MNS.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("CustomerContacts")]
    public class CustomerContactsEntity : BaseEntity<long>
    {
        [Display(Name = "الأسم")]
        public string Name { get; set; }

        [Display(Name = "الوظيفة")]
        public string Job { get; set; }

        [Display(Name = "الشركة")]
        public long CustomerId { get; set; }

        [Display(Name = "الإيميل")]
        public string Email { get; set; }

        [Display(Name = "رقم التليفون")]
        public string Phone { get; set; }

        [Display(Name = "الشركة")]
        public virtual CustomersEntity Customer { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                if (Phone == null)
                    return string.Format("{0}", Name);

                return string.Format("{0} - {1}", Name, Customer.Name);
            }
        }

        [NotMapped]
        public string NameContact
        {
            get
            {
                if (Phone == null)
                    return string.Format("{0}", Name);

                return string.Format("{0} - {1}", Name, Phone);
            }
        }
    }
}
