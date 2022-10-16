using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.Data
{
    public class IdentityModel
    {
        public class ApplicationRole : IdentityRole<int> { }
        public class UserRoles : IdentityUserRole<int> { }


    }
}
