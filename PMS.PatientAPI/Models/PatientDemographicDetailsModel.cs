using System;
using System.ComponentModel.DataAnnotations;

namespace PMS.PatientAPI.Models
{
    public class PatientDemographicDetailsModel
    {
        [Key]  
        public int Id { get; set; }
        
        public int PatientId { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset DOB { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public long ContactNo { get; set; }

        public string LanguagesKnow { get; set; }

        public string Race { get; set; }

        public string Ethnicity { get; set; }

        public DateTimeOffset? CreatedOn { get; set; }

        public DateTimeOffset? UpdatedOn { get; set; }
    }
}
