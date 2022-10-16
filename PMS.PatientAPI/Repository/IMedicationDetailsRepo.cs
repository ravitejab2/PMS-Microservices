using PMS.PatientAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Repository
{
    public interface IMedicationDetailsRepo
    {

        Task<List<DrugsModel>> GetAllMedications();

      
    }
}
