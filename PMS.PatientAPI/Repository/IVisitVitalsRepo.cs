using PMS.PatientAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Repository
{
    public interface IVisitVitalsRepo
    {
        Task<List<PatientVisitVitalsModel>> GetAllVitalsDetails();
        Task<List<PatientVisitVitalsModel>> GetAllVitalsDetails(int patientId);

        Task<int> UpdateVitalsDetails(int id, PatientVisitVitalsModel model);
        Task<int> AddVitalsDetails(PatientVisitVitalsModel model);

        Task<PatientVisitVitalsModel> GetVitalsById(int vitalsId);
    }
}
