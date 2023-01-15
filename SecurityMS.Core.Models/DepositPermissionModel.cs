using SecurityMS.Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace SecurityMS.Core.Models
{
    public class DepositPermissionModel
    {
        public TreasuryDepositPermissionEntity permission { get; set; }
        public List<TreasuryDepositPermissionTypesLookup> TypesList { get; set; }
        public List<Supplier> SuppliersList { get; set; }
        public List<ExpensesLookup> ExpensesList { get; set; }
        public List<AssetsLookup> AssetsList { get; set; }
        public List<EmployeesEntity> EmployeesList { get; set; }
    }
}
