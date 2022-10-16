using System.ComponentModel.DataAnnotations;

namespace PMS.PatientAPI.Models
{
    public class AllergyModel
    {
        [Key]
        public int Id { get; set; }
        public string AllergyId { get; set; }
        public string Allergy_Type { get; set; }
        public string Allergy_Name { get; set; }
        public string Allergy_Description { get; set; }
        public string Allergy_Clinical_Information { get; set; }
       
    }
}
