using PMS.PatientAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Repository
{
    public interface IVisitMedicationRepo
    {
        Task<List<PatientVisitMedicationModel>> GetAllMeicationDetails();

        Task<int> UpdateMedicationDetails(int id, PatientVisitMedicationModel model);
        Task<int> AddMedicationDetails(PatientVisitMedicationModel model);
    }
}
