using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityAPI.Data
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string Email { get; set; }
        public long ContactNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsPwdChanged { get; set; }
        public string Role { get; set; }    
        public string CreatedDate { get; set; }
    }
}
