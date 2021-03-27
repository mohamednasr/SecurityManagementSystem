using MNS.Repository;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("CustomerContacts")]
    public class CustomerContactsEntity : BaseEntity<long>
    {
        public string Name { get; set; }

        public string Job { get; set; }

        public long CustomerId { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public virtual CustomersEntity Customer { get; set; }
    }
}
