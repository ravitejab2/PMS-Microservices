using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.UserAPI.Models
{
    public class UserProfileModel
    {
        public int Id { get; set; }
        public string Role { get; set; }

        public long Contact { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        public string Title { get; set; }

       public DateTime DateOfBirth { get; set; }


  
    }
}
