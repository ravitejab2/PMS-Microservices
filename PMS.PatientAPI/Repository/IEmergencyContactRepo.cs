using PMS.PatientAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Repository
{
    public interface IEmergencyContactRepo
    {
        Task<List<PatientEmergencyContactModel>> GetAllContactDetails();

        Task<int> UpdateContactDetails(int id, PatientEmergencyContactModel model);
        Task<int> AddContactDetails(PatientEmergencyContactModel model);

        Task<PatientEmergencyContactModel> GetContactsById(int contactId);
    }
}
