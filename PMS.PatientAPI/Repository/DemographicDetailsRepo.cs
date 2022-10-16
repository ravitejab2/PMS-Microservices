using Microsoft.EntityFrameworkCore;
using PMS.PatientAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Repository
{
    public class DemographicDetailsRepo : IDemographicDetailsRepo

    {
        private readonly PatientDbContext _patientDbContext;

        public DemographicDetailsRepo(PatientDbContext patientDbContext)
        {
            _patientDbContext = patientDbContext;
        }

        public async Task<int> AddDemographicDetails(PatientDemographicDetailsModel model)
        {
            if (_patientDbContext != null)
            {
                model.CreatedOn = DateTime.Now;
                model.ContactNo = model.ContactNo;
                await _patientDbContext.PatientDemographics.AddAsync(model);
                return await _patientDbContext.SaveChangesAsync();


            }

            return 0;
        }

        public async Task<List<PatientDemographicDetailsModel>> GetAllDetails()
        {
            if (_patientDbContext != null)
                return await _patientDbContext.PatientDemographics.ToListAsync();
            else
                return null;
        }

        public async Task<int> UpdateDemographicDetails(int id, PatientDemographicDetailsModel model)
        {
            if (_patientDbContext != null)
            {
                var result = await _patientDbContext.PatientDemographics.FirstOrDefaultAsync(x => x.PatientId == id);

                if (result != null)
                {
                    result.UpdatedOn = DateTime.Now;
                    result.LanguagesKnow = model.LanguagesKnow;
                    result.LastName=model.LastName;
                    result.FirstName = model.FirstName;
                    result.Gender = model.Gender;
                    result.Address = model.Address;
                    result.ContactNo = model.ContactNo;
                    result.Email = model.Email;
                    result.Race = model.Race;
                    result.DOB = model.DOB;
                    result.Ethnicity = model.Ethnicity;
                    result.Age = model.Age;
                    result.Title = model.Title;
                    



                    _patientDbContext.PatientDemographics.Update(result);
                    return await _patientDbContext.SaveChangesAsync();
                }


            }
            return 0;
        }

        public async Task<PatientDemographicDetailsModel> GetDetailsById(int id)
        {
            if (_patientDbContext != null)
            {
                var result = await _patientDbContext.PatientDemographics.FirstOrDefaultAsync(x => x.PatientId == id);
                return result;
            }
            else
                return null;
        }
    }
}
