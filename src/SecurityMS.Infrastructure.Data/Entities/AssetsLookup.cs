using SecurityMS.Repository;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class AssetsLookup : BaseEntity<int>
    {
        public string Name { get; set; }

    }
}
