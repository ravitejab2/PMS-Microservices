using Microsoft.EntityFrameworkCore;
using PMS.AppointmentAPI.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.AppointmentAPI.Repository
{
    public class AppointmentRepo : IAppointmentRepo
    {
        private readonly AppointmentDbContext _appointmentDbContext;
        public AppointmentRepo(AppointmentDbContext appointmentDbContext)
        {
            _appointmentDbContext = appointmentDbContext;
        }
        public async Task<int> AddAppointment(AppointmentModel model)
        {
            if (_appointmentDbContext != null)
            {
                SlotTimings slottimes = await _appointmentDbContext.Slots.FirstOrDefaultAsync(x => x.SlotId == model.SlotId);
                if (slottimes != null)
                {
                    model.AppointmentStartdate = model.Appointmentdate.Add(slottimes.SlotStart);
                    model.AppointmentEnddate = model.Appointmentdate.Add(slottimes.SlotEnd);
                    model.Status = "Created";
                }
                
                await  _appointmentDbContext.Appointments.AddAsync(model);
               return  await _appointmentDbContext.SaveChangesAsync();

                
            }

            return 0;
            
        }

        public async Task<int> DeleteAppointment(int id)
        {
            if(_appointmentDbContext != null)
            {
                var appointment=await _appointmentDbContext.Appointments.FirstOrDefaultAsync(x=>x.AppointmentId==id);
                if (appointment != null)
                {
                     _appointmentDbContext.Appointments.Remove(appointment);
                   return await _appointmentDbContext.SaveChangesAsync();
                }
            }
            return 0;
        }

        public async Task<List<AppointmentModel>> GetAllAppointments()
        {
            if (_appointmentDbContext != null)
                return await _appointmentDbContext.Appointments.ToListAsync();
            else
                return null;
        }

        public async Task<List<SlotTimings>> GetSlots(DateTime date,int id)
        {
            if (_appointmentDbContext != null)
            {
                List<AppointmentModel> slots = await _appointmentDbContext.Appointments.Where(x => x.Appointmentdate == date && x.PhysicianId == id).ToListAsync();
                List<SlotTimings> availableSlots = new List<SlotTimings>();
                if (slots != null)
                {
                    var slotsIds = slots.Select(x => x.SlotId).ToList();
                     availableSlots = await _appointmentDbContext.Slots.Where(x => !slotsIds.Contains(x.SlotId)).ToListAsync();
                }
                else
                {
                     availableSlots = await _appointmentDbContext.Slots.ToListAsync();
                }
                return availableSlots;
            }
            return null;
                
           
        }

        public async Task<AppointmentModel> GetAppointmentById(int appointmentId)
        {
            if (_appointmentDbContext != null)
                return await _appointmentDbContext.Appointments.FirstOrDefaultAsync(x => x.AppointmentId == appointmentId); 
            else
                return null;
        }

        public async Task<List<AppointmentModel>> GetAppointmentsByUser(int userId, string role)
        {
            if (_appointmentDbContext != null)
            {
                if (role == "nurse")
                {
                    return await _appointmentDbContext.Appointments.ToListAsync();
                }
                else
                {
                    return await _appointmentDbContext.Appointments.Where(x => x.PatientId == userId || x.PhysicianId == userId).ToListAsync();
                }

            }

            else
                return null;
        }

        public async Task<List<AppointmentModel>> GetAppointmentsByUser(int userId)
        {
            if (_appointmentDbContext != null)
                return await _appointmentDbContext.Appointments.Where(x => x.PatientId == userId || x.PhysicianId == userId).ToListAsync();
            else
                return null;
        }

        public async Task<List<ViewAppointmentModel>> GetAppointmentsLoad(int id, string role)
        {
            if (_appointmentDbContext != null)
            {
                List<AppointmentModel> appointments = new List<AppointmentModel>();
                List<ViewAppointmentModel> viewAppointments = new List<ViewAppointmentModel>();
                if (role == "nurse")
                {
                    appointments = await _appointmentDbContext.Appointments.ToListAsync();
                }
                else
                {
                    appointments = await _appointmentDbContext.Appointments.Where(x => x.PhysicianId == id || x.PatientId == id).ToListAsync();
                }
                foreach (var item in appointments)
                {
                    ViewAppointmentModel model = new ViewAppointmentModel();
                    model.Id = item.AppointmentId;
                    model.Start = (DateTime)item.AppointmentStartdate;
                    model.End = (DateTime)item.AppointmentEnddate;
                    model.Text = item.Meetingtitle;
                    if (item.Status == "Declined")
                    {
                        model.BackColor = "#db403b";
                    }
                    else if (item.Status == "Accepted")
                    {
                        model.BackColor = "#ADD8E6";
                    }
                    else
                    {
                        model.BackColor = "##bf00ff";
                    }
                    viewAppointments.Add(model);
                }
                return viewAppointments;
            }
            else
                return null;
        }

        public async Task<List<ViewAppointmentModel>> GetAppointmentsLoad(int id)
        {
            if (_appointmentDbContext != null)
            {
                List<AppointmentModel> appointments = new List<AppointmentModel>();
                List<ViewAppointmentModel> viewAppointments = new List<ViewAppointmentModel>();
               
                     appointments = await _appointmentDbContext.Appointments.Where(x=>x.PhysicianId==id || x.PatientId==id).ToListAsync();
                    
                
                     
                
                foreach (var item in appointments)
                {
                    ViewAppointmentModel model = new ViewAppointmentModel();
                    model.Id = item.AppointmentId;
                    model.Start = (DateTime)item.AppointmentStartdate;
                    model.End = (DateTime)item.AppointmentEnddate;
                    model.Text = item.Meetingtitle;
                    if (item.Status == "Declined")
                    {
                        model.BackColor = "#db403b";
                    }
                    else if(item.Status=="Accepted")
                    {
                        model.BackColor = "#ADD8E6";
                    }
                    else
                    {
                        model.BackColor = "##bf00ff";
                    }
                    
                    viewAppointments.Add(model);

                }
                return viewAppointments;

            }  
            else
                return null;
        }

       

        public async Task<int> UpdateAppointment(int id, AppointmentModel model)
        {
            if (_appointmentDbContext != null)
            {
                var result = await _appointmentDbContext.Appointments.FirstOrDefaultAsync(x => x.AppointmentId == id);
                
                if (result != null)
                {
                    SlotTimings slottimes = await _appointmentDbContext.Slots.FirstOrDefaultAsync(x => x.SlotId == model.SlotId);
                    if (slottimes != null)
                    {
                        result.Appointmentdate = model.Appointmentdate;
                        result.Meetingtitle = model.Meetingtitle;
                        result.Description = model.Description;
                        result.AppointmentStartdate = model.Appointmentdate.Add(slottimes.SlotStart);
                        result.AppointmentEnddate = model.Appointmentdate.Add(slottimes.SlotEnd);
                        
                        result.SlotId = model.SlotId;
                    }
                    if (model.Status != null)
                    {
                        result.Status = model.Status;
                    }
                    
                    _appointmentDbContext.Appointments.Update(result);
                    return await _appointmentDbContext.SaveChangesAsync();
                }
                
               
            }
            return 0;
        }

        public async Task<int> AcceptAppointment(int id)
        {
            if (_appointmentDbContext != null)
            {
                var result = await _appointmentDbContext.Appointments.FirstOrDefaultAsync(x => x.AppointmentId == id);

                if (result != null)
                {
                    result.Status = "Accepted";                   
                   
                    _appointmentDbContext.Appointments.Update(result);
                    return await _appointmentDbContext.SaveChangesAsync();
                }


            }
            return 0;
        }

        public async Task<int> DeclineAppointment(int id)
        {
            if (_appointmentDbContext != null)
            {
                var result = await _appointmentDbContext.Appointments.FirstOrDefaultAsync(x => x.AppointmentId == id);

                if (result != null)
                {
                    result.Status = "Declined";

                    _appointmentDbContext.Appointments.Update(result);
                    return await _appointmentDbContext.SaveChangesAsync();
                }


            }
            return 0;
        }


        
    }
}
