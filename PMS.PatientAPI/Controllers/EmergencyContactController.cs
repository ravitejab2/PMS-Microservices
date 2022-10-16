using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.PatientAPI.Models;
using PMS.PatientAPI.Repository;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmergencyContactController : ControllerBase
    {
        private readonly IEmergencyContactRepo _emergencyContactRepo;

        public EmergencyContactController(IEmergencyContactRepo emergencyContactRepo)
        {
            _emergencyContactRepo = emergencyContactRepo;
        }

        [HttpGet("allContacts")]
        public async Task<object> GetAllContactDetails()
        {
            var result = await _emergencyContactRepo.GetAllContactDetails();
            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error while retrieving ", result));
        }

        [HttpPost("addContacts")]
        public async Task<object> AddContactDetails([FromBody] PatientEmergencyContactModel model)
        {
            var result = await _emergencyContactRepo.AddContactDetails(model);
            if (result > 0)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Details Added", null));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error in adding details", result));
        }

        [HttpPut("updateContacts/{id}")]
        public async Task<object> UpdateDemographicDetails(int id, [FromBody] PatientEmergencyContactModel model)
        {
            var result = await _emergencyContactRepo.UpdateContactDetails(id, model);
            if (result > 0)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Updated Details added ", null));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error in updating details", result));
        }

        [HttpGet("Contacts/{contactId}")]
        public async Task<object> GetDetailsById(int contactId)
        {
            var result = await _emergencyContactRepo.GetContactsById(contactId);
            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error while retrieving ", result));
        }
    }
}
