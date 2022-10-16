using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.Models
{
    public class EmployeeDetailsModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        public string Role { get; set; }

        public DateTime DateOfJoining
        {
            get { return DateTime.Now.Date; }
        }
       
        public DateTimeOffset? Lockout { get; set; }
      
        public string Status { get; set; }

    }
}
