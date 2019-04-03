using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ApiWithSecurity.Entity
{
    public class ApplicationUser: IdentityUser<int>
    {
        public string Name { get; set; }
    }
}
