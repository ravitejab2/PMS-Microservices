using PMS.PatientAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Repository
{
    public interface IAllergyDetailsRepo
    {
        Task<List<PatientAllergyDetailsModel>> GetAllAllergyDetails();

        Task<int> UpdateAllergyDetails(int id, PatientAllergyDetailsModel model);
        Task<int> AddAllergyDetails(PatientAllergyDetailsModel model);

        Task<PatientAllergyDetailsModel> GetAllergiesById(int allergyId);
        Task<List<AllergyModel>> GetAllAllergies();
    }
}
