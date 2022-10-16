using System;

namespace PMS.PatientAPI.Models
{
    public class PatientVisitProceduresModel
    {
        public int Id { get; set; }

        public int? VisitId { get; set; }

        public int Patient_Id { get; set; }

        public int Procedure_Code { get; set; }

        public string Description { get; set; }

        public bool Diagnosis_Is_Depricated { get; set; }


        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
