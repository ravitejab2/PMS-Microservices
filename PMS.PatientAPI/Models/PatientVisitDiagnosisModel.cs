using System;

namespace PMS.PatientAPI.Models
{
    public class PatientVisitDiagnosisModel
    {

        public int Id { get; set; }

        public int? Visit_Id { get; set; }

        public int Patient_Id { get; set; }

        public int Diagnosis_Code { get; set; }

        public string Description { get; set; }

        public bool Diagnosis_Is_Depricated { get; set; }


        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
