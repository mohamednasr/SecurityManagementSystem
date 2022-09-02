using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SecurityMS.Core.Models
{
    public class AppUserModel
    {
        public IdentityUser User { get; set; }
        public List<string> AppliedRoles { get; set; }
        public List<IdentityRole> Roles { get; set; }
    }
}
