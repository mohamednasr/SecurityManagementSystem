using MNS.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("Contracts")]
    public class ContractsEntity : BaseEntity<long>
    {
        public DateTime Date { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long CustomerId { get; set; }
        public long? ContractContactPersonId { get; set; }
        public virtual CustomersEntity Customer { get; set; }
        public virtual List<SitesEntity> ContractSites {get;set;}
        public virtual CustomerContactsEntity ContactPerson { get; set; } 
    }
}
