using MNS.Repository;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("CountriesLookup")]
    public class CountriesLookup : BaseEntity<long>
    {
        public string Name { get; set; }
    }
}
