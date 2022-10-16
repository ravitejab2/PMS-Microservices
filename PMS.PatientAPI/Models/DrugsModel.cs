using System.ComponentModel.DataAnnotations;

namespace PMS.PatientAPI.Models
{
    public class DrugsModel
    {
        [Key]
        public int DrugID { get; set; }

        public string Drug_Name { get; set; }

        public string Drug_Generic_Name { get; set; }

        public string Drug_Manufacture_Name { get; set; }

        public string Drug_Form { get; set; }

        public string Drug_Strength { get; set; }
    }
}
