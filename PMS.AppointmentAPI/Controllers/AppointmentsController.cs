using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.AppointmentAPI.DataModels;
using PMS.AppointmentAPI.Repository;
using System;
using System.Threading.Tasks;

namespace PMS.AppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Roles ="user,nurse,physician")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentRepo _appointmentRepo;
        public AppointmentsController(IAppointmentRepo appointmentRepo)
        {
            _appointmentRepo = appointmentRepo;
        }

        [HttpGet("allAppointments")]
        public async Task<object> GetAllAppointments()
        {
            var result =await _appointmentRepo.GetAllAppointments();
            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error while retrieving ", result));
        } 

        [HttpPost("addAppointment")]
        
        public async Task<object> AddAppointment([FromBody] AppointmentModel model)
        {
            var result =await _appointmentRepo.AddAppointment(model);
            if (result > 0)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Appointment Scheduled", null));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error in adding appointment", result));
        }

        [HttpPost("updateAppointment/{id}")]
        public async Task<object> UpdateAppointment(int id, [FromBody] AppointmentModel model)
        {
            var result = await _appointmentRepo.UpdateAppointment(id,model);
            if (result > 0)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Updated Appointment ", null));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error in updating appointment", result));
        }

        [HttpDelete("deleteAppointment/{id}")]
        public async Task<object> DeleteAppointment(int appointmentId)
        {
            var result = await _appointmentRepo.DeleteAppointment(appointmentId);
            if (result > 0)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Appointment Deleted", null));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error in deleting appointment", result));
        }

        [HttpGet("userAppointments/{userId}")]
        public async Task<object> GetAppointmentsByUserLoad(int userId)
        {
            var result = await _appointmentRepo.GetAppointmentsLoad(userId);
            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error while retrieving ", result));
        }

        [HttpGet("appointmentDetails/{id}")]
        public async Task<object> GetAppointmentsById(int id)
        {

            var result = await _appointmentRepo.GetAppointmentById(id);
            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error while retrieving ", result));
        }

        [HttpGet("getAvailableslots/{date}/{id}")]
       
        public async Task<object> GetSlots(DateTime date, int id)
        {
            var result = await _appointmentRepo.GetSlots(date,id);
            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error while retrieving ", result));
        }

        [HttpGet("appointmentsByUserId/{id}/{role}")]
        public async Task<object> GetAppointmentsByUser(int id, string role)
        {



            var result = await _appointmentRepo.GetAppointmentsByUser(id, role);
            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error while retrieving ", result));
        }

        [HttpPut("accept")]
        public async Task<object> AcceptAppointment([FromBody]int id)
        {

            var result = await _appointmentRepo.AcceptAppointment(id);
            if (result >0)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error while retrieving ", result));
        }

        [HttpPut("decline")]
        public async Task<object> DeclineAppointment([FromBody]int id)
        { 

            var result = await _appointmentRepo.DeclineAppointment(id);
            if (result > 0)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error while retrieving ", result));
        }
        [HttpGet("userAppointments/{userId}/{role}")]
        public async Task<object> GetAppointmentsByUserLoad(int userId, string role)
        {
            var result = await _appointmentRepo.GetAppointmentsLoad(userId, role);
            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error while retrieving ", result));
        }



    }
}
