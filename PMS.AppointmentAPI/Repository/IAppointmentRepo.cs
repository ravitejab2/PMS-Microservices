using PMS.AppointmentAPI.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.AppointmentAPI.Repository
{
    public interface IAppointmentRepo
    {
        Task<List<AppointmentModel>> GetAllAppointments();
        Task<List<SlotTimings>> GetSlots(DateTime date, int id);
        Task<AppointmentModel> GetAppointmentById(int appointmentId);
        Task<int> DeleteAppointment(int id);
        Task<int> UpdateAppointment(int id, AppointmentModel model);
        Task<int> AcceptAppointment(int id);
        Task<int> DeclineAppointment(int id);
        Task<int> AddAppointment(AppointmentModel model);
        Task<List<ViewAppointmentModel>> GetAppointmentsLoad(int userId);
        Task<List<AppointmentModel>> GetAppointmentsByUser(int userId, string role);
        Task<List<ViewAppointmentModel>> GetAppointmentsLoad(int id, string role);


    }
}
