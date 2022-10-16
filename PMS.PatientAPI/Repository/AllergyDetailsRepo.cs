using Microsoft.EntityFrameworkCore;
using PMS.PatientAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Repository
{
    public class AllergyDetailsRepo : IAllergyDetailsRepo
    {

        private readonly PatientDbContext _patientDbContext;

        public AllergyDetailsRepo(PatientDbContext patientDbContext)
        {
            _patientDbContext = patientDbContext;
        }
      
        public async Task<int> AddAllergyDetails(PatientAllergyDetailsModel model)
        {
            if (_patientDbContext != null)
            {
                model.CreatedOn = DateTime.Now;
                await _patientDbContext.PatientAllergies.AddAsync(model);
                return await _patientDbContext.SaveChangesAsync();


            }

            return 0;
        }

        public async Task<List<PatientAllergyDetailsModel>> GetAllAllergyDetails()
        {
            if (_patientDbContext != null)
                return await _patientDbContext.PatientAllergies.ToListAsync();
            else
                return null;
        }

        public async Task<int> UpdateAllergyDetails(int id, PatientAllergyDetailsModel model)
        {
            if (_patientDbContext != null)
            {
                var result = await _patientDbContext.PatientAllergies.FirstOrDefaultAsync(x => x.PatientAllergyId == model.PatientAllergyId);

                if (result != null)
                {
                    _patientDbContext.PatientAllergies.Update(model);
                    return await _patientDbContext.SaveChangesAsync();
                }


            }
            return 0;
        }

        public async Task<PatientAllergyDetailsModel> GetAllergiesById(int allergyId)
        {
            if (_patientDbContext != null) { 
                var users =   _patientDbContext.PatientAllergies.Where(x => x.PatientId == allergyId).OrderByDescending(x=>x.PatientAllergyId);
           
                 return await users.FirstOrDefaultAsync();
            }
            // return await _patientDbContext.PatientAllergies.FirstOrDefaultAsync(x => x.PatientId == allergyId);
            else
                return null;
        }

        public async Task<List<AllergyModel>> GetAllAllergies()
        {
            if (_patientDbContext != null)
                return await _patientDbContext.Allergies.ToListAsync();
            else
                return null;
        }
    }
}
