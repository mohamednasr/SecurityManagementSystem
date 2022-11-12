using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Infrastructure.Data.Entities;

namespace SecurityMS.Infrastructure.Data
{

    //public class ContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    //{
    //    public AppDbContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
    //        optionsBuilder.UseSqlServer();

    //        return new AppDbContext(optionsBuilder.Options);
    //    }
    //}

    public class AppDbContext : IdentityDbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }


        public virtual DbSet<BlackListEntity> BlackListEntity { get; set; }
        public virtual DbSet<ContractsEntity> ContractsEntities { get; set; }
        public virtual DbSet<CustomerContactsEntity> CustomerContactsEntities { get; set; }
        public virtual DbSet<CustomersEntity> CustomersEntities { get; set; }
        public virtual DbSet<CustomerTypesLookup> CustomerTypesLookups { get; set; }
        public virtual DbSet<GovernmentEntity> GovernmentEntities { get; set; }
        public virtual DbSet<ZonesEntity> ZonesEntities { get; set; }
        public virtual DbSet<SitesEntity> SitesEntities { get; set; }
        public virtual DbSet<DepartmentsEntity> DepartmentsEntities { get; set; }
        public virtual DbSet<JobsEntity> JobsEntities { get; set; }
        public virtual DbSet<ShiftTypesLookup> ShiftTypesLookups { get; set; }
        public virtual DbSet<EmployeesEntity> EmployeesEntities { get; set; }
        public virtual DbSet<SiteEmployeesEntity> SiteEmployeesEntities { get; set; }
        public virtual DbSet<EquipmentTypesLookup> EquipmentTypesLookups { get; set; }
        public virtual DbSet<SiteEquipmentsEntity> SiteEquipmentsEntities { get; set; }
        public virtual DbSet<CountriesLookup> CountriesLookups { get; set; }
        public virtual DbSet<EquipmentsEntity> EquipmentsEntities { get; set; }
        public virtual DbSet<SiteEmployeesAssignEntity> SiteEmployeesAssignEntities { get; set; }
        public virtual DbSet<EquipmentDetailsEntity> EquipmentDetailsEntities { get; set; }
        public virtual DbSet<SiteEquipmentsAssignEntity> SiteEquipmentsAssignEntities { get; set; }
        public virtual DbSet<SiteEmployeeAttendanceEntity> SiteEmployeeAttendanceEntities { get; set; }
        public virtual DbSet<InvoiceEntity> InvoicesEntity { get; set; }
        public virtual DbSet<InvoiceDetails> InvoiceDetails { get; set; }
        public virtual DbSet<AdvancedPaymentEntity> AdvancedPaymentsEntity { get; set; }
        public virtual DbSet<RewardEntity> RewardsEntity { get; set; }
        public virtual DbSet<PenaltyEntity> PenaltiesEntity { get; set; }
        public virtual DbSet<EndServiceReasonLookup> EndServiceReasonLookup { get; set; }
        public DbSet<ItemEntity> Items { get; set; }
        public DbSet<ItemDetailsEntity> ItemDetail { get; set; }
        public DbSet<UniformEntity> Uniform { get; set; }
        public DbSet<UniformDetailsEntity> UniformDetails { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplyTypes> SupplyTypes { get; set; }
        public DbSet<Purchases> Purchases { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<TreasuryDepositPermissionEntity> TreasuryDepositPermission { get; set; }
        public DbSet<TreasuryWithdrawPermissionEntity> TreasuryWithdrawPermission { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<SupplyItems> SupplyItems { get; set; }
        public DbSet<SalaryReportDetails> SalariesReportDetails { get; set; }
        public DbSet<EmployeesSalaryReportDetails> SalariesReportEmployeesReports { get; set; }
        public DbSet<IncomeTaxesMatrix> IncomeTaxesMatrix { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ContractsEntity>()
                    .HasOne(e => e.MainCustomer)
                    .WithMany()
                    .HasForeignKey(e => e.CustomerId);

            //builder.Entity<ContractsEntity>()
            //    .HasMany(e => e.ContactPerson);

            builder.Entity<CustomerContactsEntity>()
                    .HasOne(e => e.Customer)
                    .WithMany()
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CustomersEntity>()
                    .HasOne(e => e.ParentCustomers)
                    .WithMany()
                    .HasForeignKey(e => e.ParentCustomerId);

            builder.Entity<CustomersEntity>()
                    .HasOne(e => e.CustomerType)
                    .WithMany()
                    .HasForeignKey(e => e.CustomerTypeId);
            builder.Entity<CustomersEntity>()
                .HasOne(e => e.Government)
                .WithMany()
                .HasForeignKey(e => e.GovernmentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ZonesEntity>()
                    .HasOne(e => e.Government)
                    .WithMany()
                    .HasForeignKey(e => e.GovernmentId);

            builder.Entity<SitesEntity>()
                    .HasOne(e => e.zone)
                    .WithMany()
                    .HasForeignKey(e => e.ZoneId);

            builder.Entity<JobsEntity>()
                .HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<EmployeesEntity>()
                .HasOne(e => e.Job)
                .WithMany()
                .HasForeignKey(e => e.JobId);

            builder.Entity<SiteEmployeesEntity>()
                .HasOne(e => e.Site)
                .WithMany()
                .HasForeignKey(e => e.SiteId);

            builder.Entity<SiteEmployeesEntity>()
                .HasOne(e => e.Job)
                .WithMany()
                .HasForeignKey(e => e.JobId);

            builder.Entity<SiteEmployeesEntity>()
                .HasOne(e => e.ShiftType)
                .WithMany()
                .HasForeignKey(e => e.ShiftTypeId);

            builder.Entity<SiteEquipmentsEntity>()
                .HasOne(e => e.Site)
                .WithMany()
                .HasForeignKey(e => e.SiteId);

            builder.Entity<SiteEquipmentsEntity>()
                .HasOne(e => e.Equipment)
                .WithMany()
                .HasForeignKey(e => e.EquipmentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<EquipmentsEntity>()
                .HasOne(e => e.EquipmentType)
                .WithMany()
                .HasForeignKey(e => e.EquipmentTypeId);

            builder.Entity<EquipmentsEntity>()
                .HasOne(e => e.Manufacturing)
                .WithMany()
                .HasForeignKey(e => e.ManufactureId);

            builder.Entity<SiteEmployeesAssignEntity>()
                .HasOne(e => e.Employee)
                .WithMany()
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<SiteEmployeesAssignEntity>()
                .HasOne(e => e.SiteEmployee)
                .WithMany(s => s.AssignedEmployees)
                .HasForeignKey(e => e.SiteEmployeeId);

            builder.Entity<EquipmentDetailsEntity>()
                .HasOne(e => e.Equipment)
                .WithMany()
                .HasForeignKey(e => e.EquipmentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<SiteEquipmentsAssignEntity>()
                .HasOne(e => e.EquipmentDetails)
                .WithMany()
                .HasForeignKey(e => e.EquipmentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<SiteEquipmentsAssignEntity>()
                .HasOne(e => e.SiteEquipment)
                .WithMany()
                .HasForeignKey(e => e.SiteEquipmenteId);

            builder.Entity<SiteEmployeeAttendanceEntity>()
                .HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(e => e.EmployeeId);


            builder.Entity<SiteEmployeeAttendanceEntity>()
                .HasOne(x => x.Site)
                .WithMany()
                .HasForeignKey(e => e.SiteId);


            builder.Entity<SiteEmployeeAttendanceEntity>()
                .HasOne(x => x.ShiftType)
                .WithMany()
                .HasForeignKey(e => e.ShiftId);

            builder.Entity<InvoiceEntity>()
                .HasMany(x => x.items)
                .WithOne(x => x.invoice).HasForeignKey(x => x.InvoiceId);

            builder.Entity<SiteEmployeesAssignEntity>()
                 .HasOne(e => e.Employee)
                 .WithMany()
                 .HasForeignKey(e => e.EmployeeId)
                 .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<SiteEmployeesAssignEntity>()
                .HasOne(e => e.Employee)
                .WithMany()
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<RewardEntity>()
                .HasOne(e => e.Employee)
                .WithMany(x => x.Rewards)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<PenaltyEntity>()
                .HasOne(e => e.Employee)
                .WithMany(e => e.Penalities)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<AdvancedPaymentEntity>()
                .HasOne(e => e.Employee)
                .WithMany(e => e.AdvancedPayments)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<ItemEntity>()
            //    .HasMany(e => e.Items)
            //    .WithOne()
            //    .HasForeignKey(e => e.ItemId);
            builder.Entity<ItemDetailsEntity>()
                .HasOne(e => e.Item)
                .WithMany()
                .HasForeignKey(e => e.ItemId)
                .OnDelete(DeleteBehavior.NoAction);
            //builder.Entity<UniformEntity>()
            //    .HasMany(e => e.Uniforms)
            //    .WithOne()
            //    .HasForeignKey(e => e.UniformId);
            builder.Entity<UniformDetailsEntity>()
                .HasOne(e => e.Uniform)
                .WithMany()
                .HasForeignKey(e => e.UniformId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UniformDetailsEntity>()
               .HasOne(e => e.Employee)
               .WithMany()
               .HasForeignKey(e => e.AssignedTo)
               .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Supplier>()
                .HasOne(e => e.supplyType)
                .WithMany()
                .HasForeignKey(e => e.Type)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Purchases>()
                .HasOne(e => e.Supplier)
                .WithMany()
                .HasForeignKey(e => e.SupplierId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Purchases>()
                .HasOne(e => e.SupplyType)
                .WithMany()
                .HasForeignKey(e => e.SupplyTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Purchases>()
                .HasMany(e => e.Items)
                .WithOne()
                .HasForeignKey(e => e.PurchaseId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<PurchaseItem>()
                .HasKey(k => new { k.PurchaseId, k.ItemId });
            builder.Entity<PurchaseItem>()
                .HasOne(e => e.Item)
                .WithMany()
                .HasForeignKey(e => e.ItemId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Supply>()
                .HasOne(e => e.Purchase)
                .WithMany()
                .HasForeignKey(e => e.PurchaseId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<SupplyItems>()
                .HasOne(e => e.Supply)
                .WithMany()
                .HasForeignKey(e => e.SupplyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<SupplyItems>()
                .HasOne(e => e.Item)
                .WithMany()
                .HasForeignKey(e => e.ItemId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<SalaryReportDetails>()
                .HasMany(e => e.EmployeesSalaries)
                .WithOne(e => e.SalaryReport)
                .HasForeignKey(e => e.SalaryReportId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SalaryReportDetails>()
                .HasOne(e => e.Site)
                .WithMany()
                .HasForeignKey(e => e.SiteId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<EmployeesSalaryReportDetails>()
                .HasOne(e => e.Employee)
                .WithMany()
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<EmployeesSalaryReportDetails>()
            //    .HasOne(e => e.SalaryReport)
            //    .WithMany()
            //    .HasForeignKey(e => e.SalaryReportId)
            //    .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
