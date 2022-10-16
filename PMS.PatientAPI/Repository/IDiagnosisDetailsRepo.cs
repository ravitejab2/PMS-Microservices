using PMS.PatientAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Repository
{
    public interface IDiagnosisDetailsRepo
    {


        Task<List<DiagnosisModel>> GetAllDiagnosis();

  
    }
}
