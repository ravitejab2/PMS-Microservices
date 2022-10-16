using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Models
{
    public class UserProfileModel
    {
        public int Id { get; set; }
        public string Gender { get; set; }

        public long Contact { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        public int Age { get; set; }

       public string Address { get; set; }
    }
}
