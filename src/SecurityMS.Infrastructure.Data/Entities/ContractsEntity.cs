﻿using MNS.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("Contracts")]
    public class ContractsEntity : BaseEntity<long>
    {
        [Display(Name = "تاريخ التعاقد")]
        public DateTime Date { get; set; }

        [Display(Name = "تاريخ بدء التنفيذ")]
        public DateTime StartDate { get; set; }
        [Display(Name = "تاريخ انتهاء التنفيذ")]
        public DateTime EndDate { get; set; }
        [Display(Name = "العميل")]
        public long CustomerId { get; set; }
        [Display(Name = "جهة التواصل")]
        public long? ContractContactPersonId { get; set; }
        [Display(Name = "العميل")]
        public virtual CustomersEntity MainCustomer { get; set; }
        [Display(Name = "المواقع")]
        public virtual List<SitesEntity> ContractSites {get;set;}
        [Display(Name = "جهات التواصل")]
        public virtual List<CustomerContactsEntity> ContactPerson { get; set; } 
    }
}