using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.PatientAPI.Models;
using PMS.PatientAPI.Repository;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllergiesController : ControllerBase
    {
        private readonly IAllergyDetailsRepo _allergyDetailsRepo;

        public AllergiesController(IAllergyDetailsRepo allergyDetailsRepo)
        {
            _allergyDetailsRepo = allergyDetailsRepo;
        }

        [HttpGet("GetAllergies")]
        public async Task<object> GetAllAllergyDetails()
        {
            var result = await _allergyDetailsRepo.GetAllAllergyDetails();
            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error while retrieving ", result));
        }

        [HttpGet("allAllergies")]
        public async Task<object> GetAllAllergies()
        {
            var result = await _allergyDetailsRepo.GetAllAllergies();
            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error while retrieving ", result));
        }

        [HttpGet("Allergies/{Id}")]
        public async Task<object> GetAllergiesById(int Id)
        {
            var result = await _allergyDetailsRepo.GetAllergiesById(Id);
            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error while retrieving ", result));
        }

        [HttpPost("addAllergies")]
        public async Task<object> AddAllergyDetails([FromBody] PatientAllergyDetailsModel model)
        {
            var result = await _allergyDetailsRepo.AddAllergyDetails(model);
            if (result > 0)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Details Added", null));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error in adding details", result));
        }

        [HttpPut("updateAllergies/{id}")]
        public async Task<object> UpdateAllergyDetails(int id, [FromBody] PatientAllergyDetailsModel model)
        {
            var result = await _allergyDetailsRepo.UpdateAllergyDetails(id, model);
            if (result > 0)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Updated Details added ", null));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error in updating details", result));
        }
    }
}
