using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.PatientAPI.Models
{
    public class Patient_Diagnosis_MedicationModel
    {
        [Key]
        public int Id { get; set; }

        public int PatientId { get; set; }  
       
        [ForeignKey("PatientVisitVitalsModel")]
        public int? VisitId { get; set; }

        public PatientVisitVitalsModel PatientVisitVitalsModel { get; set; }
        public string Diagnosis_Code { get; set; }

        public string Diagnosis_Description { get; set; }

        public bool Diagnosis_Is_Depricated { get; set; }

        public string Procedure_Code { get; set; }

        public string Procedure_Description { get; set; }

        public bool Procedure_Is_Depricated { get; set; }

        public string Drug_Name { get; set; }

        public string Drug_GenericName { get; set; }

        public string Drug_Form { get; set; }
        public string Description { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string Drug_Strength { get; set; }

    }
}
