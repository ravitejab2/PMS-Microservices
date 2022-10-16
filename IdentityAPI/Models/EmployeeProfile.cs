using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.UserAPI.Models
{
    public class EmployeeProfile
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        public string DateOfBirth { get; set; }

        public DateTime DateOfJoining
        {
            get { return DateTime.Now.Date; }
        }

    }
}
