using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.Data
{
    public class UserDTO
    {
        public UserDTO(int id,string firstName, string lastName, string email,bool isChanged, List<string> roles)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            IsPasswordChanged = isChanged;
            Roles = roles;
        }

        public int Id { get;  set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public bool IsPasswordChanged { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }
    }
}
