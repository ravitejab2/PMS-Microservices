using PMS.PatientAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Repository
{
    public interface IDemographicDetailsRepo
    {
        Task<List<PatientDemographicDetailsModel>> GetAllDetails();

        Task<int> UpdateDemographicDetails(int id, PatientDemographicDetailsModel model);
        Task<int> AddDemographicDetails(PatientDemographicDetailsModel model);

        Task<PatientDemographicDetailsModel> GetDetailsById(int demoId);
    }
}
