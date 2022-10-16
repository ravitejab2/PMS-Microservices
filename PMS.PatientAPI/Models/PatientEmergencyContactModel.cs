using System;
using System.ComponentModel.DataAnnotations;

namespace PMS.PatientAPI.Models
{
    public class PatientEmergencyContactModel


    {
        [Key]
        public int EmergencyContactId { get; set; }
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }
        public string Email { get; set; }

        public long ContactNo { get; set; }

        public string Address { get; set; }

        public bool IsAllowed { get; set; }

        public DateTimeOffset? CreatedOn { get; set; }

        public DateTimeOffset? UpdatedOn { get; set; }
    }
}
