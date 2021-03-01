using MNS.Repository;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class BlackListEntity : BaseEntity<long>
    {
        public long Ser { get; set; }

        public string Name { get; set; }

        public string Company { get; set; }

        public string Job { get; set; }

        public string Nat_Id { get; set; }

        public string Reason { get; set; }
    }
}
