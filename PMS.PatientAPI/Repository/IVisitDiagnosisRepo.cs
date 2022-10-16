using PMS.PatientAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Repository
{
    public interface IVisitDiagnosisRepo
    {
        Task<List<PatientVisitDiagnosisModel>> GetAllDiagnosisDetails();

        Task<int> UpdateDiagnosisDetails(int id, PatientVisitDiagnosisModel model);
        Task<int> AddDiagnosisDetails(PatientVisitDiagnosisModel model);
    }
}
