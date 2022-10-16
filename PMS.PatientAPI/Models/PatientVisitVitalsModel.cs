using System;
using System.ComponentModel.DataAnnotations;

namespace PMS.PatientAPI.Models
{
    public class PatientVisitVitalsModel
    {
        [Key]
        public int VisitId { get; set; }

        public DateTime? Visit_Date { get; set; }

        public int Patient_Id { get; set; }

        public string Height { get; set; }

        public string Weight { get; set; }

        public string Blood_Pressure { get; set; }

        public string Body_Temperature { get; set; }

        public string Respiration_Rate { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
