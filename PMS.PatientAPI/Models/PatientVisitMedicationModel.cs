using System;

namespace PMS.PatientAPI.Models
{
    public class PatientVisitMedicationModel
    {
        public int Id { get; set; }
        public int? VisitId { get; set; }

        public int Patient_Id { get; set; }
        public int Drug_Id { get; set; }


        public string Drug_Name { get; set; }

        public string Drug_GenericName { get; set; }

        public string Drug_BrandName { get; set; }

        public string Drug_Form { get; set; }
        public string Description { get; set; }


        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
