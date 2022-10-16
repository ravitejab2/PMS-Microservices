using PMS.PatientAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Repository
{
    public interface IVisitProceduresRepo
    {
        Task<List<PatientVisitProceduresModel>> GetAllProceduresDetails();

        Task<int> UpdateProceduresDetails(int id, PatientVisitProceduresModel model);
        Task<int> AddProceduresDetails(PatientVisitProceduresModel model);
    }
}
