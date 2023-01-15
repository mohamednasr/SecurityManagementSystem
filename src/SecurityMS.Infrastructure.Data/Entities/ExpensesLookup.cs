using SecurityMS.Repository;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class ExpensesLookup : BaseEntity<int>
    {
        public string Name { get; set; }

    }
}
