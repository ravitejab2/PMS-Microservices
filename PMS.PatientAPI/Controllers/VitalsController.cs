using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.PatientAPI.Models;
using PMS.PatientAPI.Repository;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VitalsController : ControllerBase
    {
        private readonly IVisitVitalsRepo _visitVitalsRepo;

        public VitalsController(IVisitVitalsRepo visitVitalsRepo)
        {
            _visitVitalsRepo = visitVitalsRepo;
        }

        [HttpGet("allVitals")]
        public async Task<object> GetAllVitalsDetails()
        {
            var result = await _visitVitalsRepo.GetAllVitalsDetails();
            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error while retrieving ", result));
        }

        [HttpGet("allVitals/{patientId}")]
        public async Task<object> GetAllVitalsDetails(int patientId)
        {
            var result = await _visitVitalsRepo.GetAllVitalsDetails(patientId);
            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error while retrieving ", result));
        }



        [HttpGet("Vitals/{vitalsId}")]
        public async Task<object> GetVitalsById(int vitalsId)
        {
            var result = await _visitVitalsRepo.GetVitalsById(vitalsId);
            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error while retrieving ", result));
        }


        [HttpPost("addVitals")]
        public async Task<object> AddVitalsDetails([FromBody] PatientVisitVitalsModel model)
        {
            var result = await _visitVitalsRepo.AddVitalsDetails(model);
            if (result > 0)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Details Added", null));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error in adding details", result));
        }

        [HttpPut("updateVitals/{id}")]
        public async Task<object> UpdateVitalsDetails(int id, [FromBody] PatientVisitVitalsModel model)
        {
            var result = await _visitVitalsRepo.UpdateVitalsDetails(id, model);
            if (result > 0)
            {
                model.UpdatedOn = System.DateTime.Now;
                model.CreatedOn = model.Visit_Date;
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Updated Details added ", null));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error in updating details", result));
        }
    }
}
