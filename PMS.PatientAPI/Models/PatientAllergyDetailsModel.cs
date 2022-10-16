using System;
using System.ComponentModel.DataAnnotations;

namespace PMS.PatientAPI.Models
{
    public class PatientAllergyDetailsModel
    {

        [Key]

        public int PatientAllergyId { get; set; }
        public string AllergyId { get; set; }
        public int PatientId { get; set; }
        public string Allergy_Name { get; set; }

        public string Allergy_Type { get; set; }

        public string Allergy_Desc { get; set; }

        public string Allergy_Clinical { get; set; }

        public bool Is_Allergy_Fatal { get; set; }

        public DateTimeOffset? CreatedOn { get; set; }

        public DateTimeOffset? UpdatedOn { get; set; }
    }
}
