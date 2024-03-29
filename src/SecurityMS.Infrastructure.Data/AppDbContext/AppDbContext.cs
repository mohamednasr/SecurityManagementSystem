﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        public DbSet<BankAccountsEntity> BankAccounts { get; set; }
        public DbSet<BankTransactions> BankTransactions { get; set; }
        public DbSet<CompanyInfo> CompanyInfo { get; set; }

        public DbSet<Supply> Supplies { get; set; }
        public DbSet<SupplierTypesLookups> SuppliersTypes { get; set; }
        public DbSet<SupplyItems> SupplyItems { get; set; }
        public DbSet<SalaryReportDetails> SalariesReportDetails { get; set; }
        public DbSet<EmployeesSalaryReportDetails> SalariesReportEmployeesReports { get; set; }
        public DbSet<IncomeTaxesMatrix> IncomeTaxesMatrix { get; set; }
        public DbSet<ExchangeTypesLookups> ExchangeTypesLookup { get; set; }
        public DbSet<ExchangeEntity> ExchangeEntity { get; set; }
        public DbSet<ExchangeItems> ExhangeItems { get; set; }

        public DbSet<TreasuryWithdrawPermissionTypesLookup> TreasuryWithdrawPermissionTypesLookup { get; set; }
        public DbSet<TreasuryDepositPermissionTypesLookup> TreasuryDepositPermissionTypesLookup { get; set; }
        public DbSet<ExpensesLookup> ExpensesLookup { get; set; }

        public DbSet<AssetsLookup> AssetsLookup { get; set; }
        public DbSet<BankCashDepositTransaction> BankCashDepositTransaction { get; set; }
        public DbSet<BankCashWithdrawTransaction> BankCashWithdrawTransaction { get; set; }
        public DbSet<BankChequeDepositTransaction> BankChequeDepositTransaction { get; set; }
        public DbSet<BankChequeWithdrawTransaction> BankChequeWithdrawTransaction { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AssetsLookup>().HasData(
                new AssetsLookup { Id = 1, Name = "أثاث" },
                new AssetsLookup { Id = 2, Name = "أجهزة كمبيوتر و نظم حسابيه" },
                new AssetsLookup { Id = 3, Name = "اجهزة كهربائية" },
                new AssetsLookup { Id = 4, Name = "تشطيبات و ديكورات" },
                new AssetsLookup { Id = 5, Name = "أجهزة تكييف" },
                new AssetsLookup { Id = 6, Name = "وسائل نقل و انتقال" },
                new AssetsLookup { Id = 7, Name = "أسلحة" },
                new AssetsLookup { Id = 8, Name = "أجهزة لاسلكي" }
                );

            builder.Entity<ExpensesLookup>().HasData(
                new ExpensesLookup { Id = 1, Name = "بوفيه و ضيافة" },
                new ExpensesLookup { Id = 2, Name = "اجور و مرتبات" },
                new ExpensesLookup { Id = 3, Name = "م.بنكية" },
                new ExpensesLookup { Id = 4, Name = "رسوم" },
                new ExpensesLookup { Id = 5, Name = "نثريات" },
                new ExpensesLookup { Id = 6, Name = "انتقالات" },
                new ExpensesLookup { Id = 7, Name = "اعانة وفاة" },
                new ExpensesLookup { Id = 8, Name = "اصلاح و صيانة" },
                new ExpensesLookup { Id = 9, Name = "تليفونات" },
                new ExpensesLookup { Id = 10, Name = "ادوات مكتبية" },
                new ExpensesLookup { Id = 11, Name = "ايجار" },
                new ExpensesLookup { Id = 12, Name = "اكراميات" },
                new ExpensesLookup { Id = 13, Name = "اتعاب مهنية" },
                new ExpensesLookup { Id = 14, Name = "تامينات اجتماعية" },
                new ExpensesLookup { Id = 15, Name = "تامينات مقاولات" },
                new ExpensesLookup { Id = 16, Name = "تحصيل عملاء" },
                new ExpensesLookup { Id = 17, Name = "نت مقر الشركة" },
                new ExpensesLookup { Id = 18, Name = "ادوات كهربائية" },
                new ExpensesLookup { Id = 19, Name = "بنزين" },
                new ExpensesLookup { Id = 20, Name = "غاز و كهرباءومياه" },
                new ExpensesLookup { Id = 21, Name = "غرامات" },
                new ExpensesLookup { Id = 22, Name = "غرامات موقع" },
                new ExpensesLookup { Id = 23, Name = "مساهمة تكافلية" },
                new ExpensesLookup { Id = 24, Name = "ايجار سيارات" },
                new ExpensesLookup { Id = 25, Name = "غسيل و مكوى" },
                new ExpensesLookup { Id = 26, Name = "زي امن" },
                new ExpensesLookup { Id = 27, Name = "وثائق تامين" },
                new ExpensesLookup { Id = 28, Name = "تبرعات" },
                new ExpensesLookup { Id = 29, Name = "علاج" }
                );


            builder.Entity<TreasuryDepositPermissionTypesLookup>().HasData(
                new ExpensesLookup { Id = 1, Name = "جاري شريك" },
                new ExpensesLookup { Id = 2, Name = "عهد" },
                new ExpensesLookup { Id = 3, Name = "بنك" },
                new ExpensesLookup { Id = 4, Name = "سلف" },
                new ExpensesLookup { Id = 5, Name = "عملاء" }
                );

            builder.Entity<TreasuryWithdrawPermissionTypesLookup>().HasData(
                new ExpensesLookup { Id = 1, Name = "مصاريف تشغيل" },
                new ExpensesLookup { Id = 2, Name = "مصاريف عمومية" },
                new ExpensesLookup { Id = 3, Name = "موردين" },
                new ExpensesLookup { Id = 4, Name = "الاصول" },
                new ExpensesLookup { Id = 5, Name = "العهد" },
                new ExpensesLookup { Id = 6, Name = "سلف" },
                new ExpensesLookup { Id = 7, Name = "جاري شريك" }
                );


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
                .WithOne(e => e.Purchase)
                .HasForeignKey(e => e.PurchaseId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Purchases>()
                .HasIndex(e => e.PurchaseCode)
                .IsUnique();

            builder.Entity<PurchaseItem>()
                .HasKey(k => new { k.PurchaseId, k.ItemId });

            builder.Entity<PurchaseItem>()
                .HasOne(e => e.Item)
                .WithMany()
                .HasForeignKey(e => e.ItemId)
                .OnDelete(DeleteBehavior.NoAction);



            builder.Entity<Supply>()
                .HasOne(e => e.SupplierType)
                .WithMany()
                .HasForeignKey(e => e.SupplierTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<SupplyItems>()
                .HasOne(e => e.Supply)
                .WithMany(e => e.SupplyItems)
                .HasForeignKey(e => e.SupplyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<SupplyItems>()
                .HasOne(e => e.Item)
                .WithMany()
                .HasForeignKey(e => e.ItemId)
                .OnDelete(DeleteBehavior.NoAction);



            builder.Entity<BankCashDepositTransaction>()
                .HasOne(e => e.BankAccount)
                .WithMany()
                .HasForeignKey(e => e.BankId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<BankCashWithdrawTransaction>()
                .HasOne(e => e.BankAccount)
                .WithMany()
                .HasForeignKey(e => e.BankId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<BankChequeDepositTransaction>()
                .HasOne(e => e.BankAccount)
                .WithMany()
                .HasForeignKey(e => e.BankId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<BankChequeWithdrawTransaction>()
                .HasOne(e => e.BankAccount)
                .WithMany()
                .HasForeignKey(e => e.BankId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TreasuryWithdrawPermissionEntity>()
                .HasOne(e => e.Type)
                .WithMany()
                .HasForeignKey(e => e.TypeId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<TreasuryDepositPermissionEntity>()
                .HasOne(e => e.Type)
                .WithMany()
                .HasForeignKey(e => e.TypeId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<SalaryReportDetails>()
                .HasMany(e => e.EmployeesSalaries)
                .WithOne(e => e.SalaryReport)
                .HasForeignKey(e => e.SalaryReportId)
                .OnDelete(DeleteBehavior.Cascade);


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

            builder.Entity<ExchangeEntity>()
                .HasOne(e => e.ExchangeType)
                .WithMany()
                .HasForeignKey(e => e.ExchangeTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ExchangeItems>()
                .HasOne(e => e.Exchange)
                .WithMany(e => e.ExchangeItems)
                .HasForeignKey(e => e.ExchangeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ExchangeItems>()
                .HasOne(e => e.Item)
                .WithMany()
                .HasForeignKey(e => e.ItemId)
                .OnDelete(DeleteBehavior.NoAction);

        }

    }
}
