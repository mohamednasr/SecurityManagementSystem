using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Infrastructure.Data.Entities;

namespace SecurityMS.Infrastructure.Data
{
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ContractsEntity>()
                    .HasOne(e => e.MainCustomer)
                    .WithMany()
                    .HasForeignKey(e => e.CustomerId);

            builder.Entity<ContractsEntity>()
                .HasMany(e => e.ContactPerson);

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
                .HasOne(e => e.Zone)
                .WithMany()
                .HasForeignKey(e => e.ZoneId)
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
                .WithMany()
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
        }

    }
}
