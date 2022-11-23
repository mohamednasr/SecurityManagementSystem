using SecurityMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class AssetsLookup : BaseEntity<int>
    {
        public string Name { get; set; }

    }
}
