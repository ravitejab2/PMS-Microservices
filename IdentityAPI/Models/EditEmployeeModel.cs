using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace PMS.UserAPI.Models
{
    public class EditEmployeeModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public long ContactNumber { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}