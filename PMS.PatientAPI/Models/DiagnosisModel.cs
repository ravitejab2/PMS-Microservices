using System.ComponentModel.DataAnnotations;

namespace PMS.PatientAPI.Models
{
    public class DiagnosisModel
    {
        [Key]
        public int Id { get; set; }

        public string Diagnosis_Code { get; set; }

        public string Diagnosis_Description { get; set; }

        public bool? Diagnosis_Is_Depricated { get; set; }
    }
}
