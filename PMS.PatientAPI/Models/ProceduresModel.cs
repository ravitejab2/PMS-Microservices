using System.ComponentModel.DataAnnotations;

namespace PMS.PatientAPI.Models
{
    public class ProceduresModel
    {
        [Key]
        public int ProcedureID { get; set; }

        public string Procedure_Code { get; set; }

        public string Procedure_Description { get; set; }

        public bool? Procedure_Is_Depricated { get; set; }
    }
}
