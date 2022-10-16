using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.PatientAPI.Models;
using PMS.PatientAPI.Repository;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientDetailsController : ControllerBase
    {
        private readonly IDemographicDetailsRepo _demographicDetailsRepo;

        public PatientDetailsController(IDemographicDetailsRepo demographicDetailsRepo)
        {
            _demographicDetailsRepo = demographicDetailsRepo;
        }

        [HttpGet("allDetails")]
        public async Task<object> GetAllDetails()
        {
            var result = await _demographicDetailsRepo.GetAllDetails();
            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error while retrieving ", result));
        }
      
        [HttpGet("profile-user/{id}")]
        public async Task<object> GetUserProfileById(int id)
        {
            var result = await _demographicDetailsRepo.GetDetailsById(id);

            if (result != null)
            {
                UserProfileModel employees = new UserProfileModel();
                employees.Id = result.Id;
                employees.Name = result.FirstName + " " + result.LastName;
                employees.Address = result.Address;
                employees.Gender = result.Gender;
                employees.Contact = result.ContactNo;
                employees.Age = result.Age;
                employees.Email = result.Email;
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "User Deatils", employees));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "User Not Found", null));
        }

        [HttpGet("Get-Details/{id}")]
        public async Task<object> GetDetailsbyId(int id)
        {
            var result = await _demographicDetailsRepo.GetDetailsById(id);

            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "User Deatils", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "User Not Found", null));
        }


        [HttpPost("addDetails")]
        public async Task<object> AddDemographicDetails([FromBody] PatientDemographicDetailsModel model)
        {
            var result = await _demographicDetailsRepo.AddDemographicDetails(model);
            if (result > 0)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Details Added", null));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error in adding details", result));
        }

      
        [HttpPut("updateDetails/{id}")]
        public async Task<object> UpdateDemographicDetails(int id, [FromBody] PatientDemographicDetailsModel model)
        {
            var result = await _demographicDetailsRepo.UpdateDemographicDetails(id, model);
            if (result > 0)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Updated Details added ", null));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error in updating details", result));
        }

     
        
        [HttpGet("Details/{demoId}")]
        public async Task<object> GetDetailsById(int demoId)
        {
            var result = await _demographicDetailsRepo.GetDetailsById(demoId);
            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error while retrieving ", result));
        }
    }
}
