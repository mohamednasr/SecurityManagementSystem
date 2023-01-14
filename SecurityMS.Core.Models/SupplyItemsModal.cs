using SecurityMS.Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace SecurityMS.Core.Models
{
    public class SupplyItemsModal
    {
        public long SupplyId { get; set; }
        public List<SupplyItems> Items { get; set; }
    }
}
