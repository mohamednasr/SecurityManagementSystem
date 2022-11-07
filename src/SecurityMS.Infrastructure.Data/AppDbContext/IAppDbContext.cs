using Microsoft.EntityFrameworkCore;
using SecurityMS.Infrastructure.Data.Entities;

namespace SecurityMS.Infrastructure.Data
{
    public interface IAppDbContext
    {
        DbSet<BlackListEntity> BlackListEntity { get; set; }
        DbSet<ContractsEntity> ContractsEntities { get; set; }
        DbSet<CustomerContactsEntity> CustomerContactsEntities { get; set; }
        DbSet<CustomersEntity> CustomersEntities { get; set; }
        DbSet<CustomerTypesLookup> CustomerTypesLookups { get; set; }
        DbSet<GovernmentEntity> GovernmentEntities { get; set; }
        DbSet<ZonesEntity> ZonesEntities { get; set; }
        DbSet<SitesEntity> SitesEntities { get; set; }
        DbSet<DepartmentsEntity> DepartmentsEntities { get; set; }
        DbSet<JobsEntity> JobsEntities { get; set; }
        DbSet<ShiftTypesLookup> ShiftTypesLookups { get; set; }
        DbSet<EmployeesEntity> EmployeesEntities { get; set; }
        DbSet<SiteEmployeesEntity> SiteEmployeesEntities { get; set; }
        DbSet<EquipmentTypesLookup> EquipmentTypesLookups { get; set; }
        DbSet<SiteEquipmentsEntity> SiteEquipmentsEntities { get; set; }
        DbSet<CountriesLookup> CountriesLookups { get; set; }
        DbSet<EquipmentsEntity> EquipmentsEntities { get; set; }
        DbSet<SiteEmployeesAssignEntity> SiteEmployeesAssignEntities { get; set; }
        DbSet<EquipmentDetailsEntity> EquipmentDetailsEntities { get; set; }
        DbSet<SiteEquipmentsAssignEntity> SiteEquipmentsAssignEntities { get; set; }
        DbSet<InvoiceEntity> InvoicesEntity { get; set; }
        DbSet<InvoiceDetails> InvoiceDetails { get; set; }
        DbSet<AdvancedPaymentEntity> AdvancedPaymentsEntity { get; set; }
        DbSet<RewardEntity> RewardsEntity { get; set; }
        DbSet<PenaltyEntity> PenaltiesEntity { get; set; }
        DbSet<EndServiceReasonLookup> EndServiceReasonLookup { get; set; }
        DbSet<ItemEntity> Items { get; set; }
        DbSet<ItemDetailsEntity> ItemDetail { get; set; }
        DbSet<UniformEntity> Uniform { get; set; }
        DbSet<UniformDetailsEntity> UniformDetails { get; set; }
        DbSet<Supplier> Suppliers { get; set; }
        DbSet<SupplyTypes> SupplyTypes { get; set; }
        DbSet<Purchases> Purchases { get; set; }
        DbSet<PurchaseItem> PurchaseItems { get; set; }
        DbSet<TreasuryDepositPermissionEntity> TreasuryDepositPermission { get; set; }
        DbSet<TreasuryWithdrawPermissionEntity> TreasuryWithdrawPermission { get; set; }

        DbSet<Supply> Supplies { get; set; }
        DbSet<SupplyItems> SupplyItems { get; set; }
    }
}
