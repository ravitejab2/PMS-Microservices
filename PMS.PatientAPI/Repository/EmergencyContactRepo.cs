using Microsoft.EntityFrameworkCore;
using PMS.PatientAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Repository
{
    public class EmergencyContactRepo : IEmergencyContactRepo
    {
        private readonly PatientDbContext _patientDbContext;

        public EmergencyContactRepo(PatientDbContext patientDbContext)
        {
            _patientDbContext = patientDbContext;
        }
        public async Task<int> AddContactDetails(PatientEmergencyContactModel model)
        {
            if (_patientDbContext != null)
            {
                model.CreatedOn = DateTime.Now;
                await _patientDbContext.PatientEmergencyContacts.AddAsync(model);
                return await _patientDbContext.SaveChangesAsync();


            }

            return 0;
        }

        public async Task<List<PatientEmergencyContactModel>> GetAllContactDetails()
        {
            if (_patientDbContext != null)
                return await _patientDbContext.PatientEmergencyContacts.ToListAsync();
            else
                return null;
        }

        public async Task<int> UpdateContactDetails(int id, PatientEmergencyContactModel model)
        {
            if (_patientDbContext != null)
            {
                var result = await _patientDbContext.PatientEmergencyContacts.FirstOrDefaultAsync(x => x.PatientId== id);

                if (result != null)
                {
                    result.UpdatedOn = DateTime.Now;
                    result.FirstName = model.FirstName;
                    result.LastName = model.LastName;
                    result.Relationship = model.Relationship;
                    result.IsAllowed = model.IsAllowed;
                    result.Address = model.Address;
                    result.ContactNo = model.ContactNo;
                    result.Email = model.Email;

                    _patientDbContext.PatientEmergencyContacts.Update(result);
                    return await _patientDbContext.SaveChangesAsync();
                }


            }
            return 0;
        }

        public async Task<PatientEmergencyContactModel> GetContactsById(int contactId)
        {
            if (_patientDbContext != null)
                return await _patientDbContext.PatientEmergencyContacts.FirstOrDefaultAsync(x => x.PatientId == contactId);
            else
                return null;
        }
    }
}
