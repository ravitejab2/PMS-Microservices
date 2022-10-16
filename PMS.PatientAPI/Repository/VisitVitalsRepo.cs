using Microsoft.EntityFrameworkCore;
using PMS.PatientAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Repository
{
    public class VisitVitalsRepo : IVisitVitalsRepo
    {
        private readonly PatientDbContext _patientDbContext;

        public VisitVitalsRepo(PatientDbContext patientDbContext)
        {
            _patientDbContext = patientDbContext;
        }
        public async Task<int> AddVitalsDetails(PatientVisitVitalsModel model)
        {
            if (_patientDbContext != null)
            {
                model.CreatedOn = DateTime.Now;
                model.Visit_Date = DateTime.Now;
                await _patientDbContext.PatientVisitVitals.AddAsync(model);
                return await _patientDbContext.SaveChangesAsync();


            }

            return 0;
        }

        public async Task<List<PatientVisitVitalsModel>> GetAllVitalsDetails()
        {
            if (_patientDbContext != null)
                return await _patientDbContext.PatientVisitVitals.ToListAsync();
            else
                return null;
        }

        public async Task<int> UpdateVitalsDetails(int id, PatientVisitVitalsModel model)
        {
            if (_patientDbContext != null)
            {
                var result = await _patientDbContext.PatientVisitVitals.FirstOrDefaultAsync(x => x.VisitId == model.VisitId);

                if (result != null)
                {
                    model.UpdatedOn = DateTime.Now;
                    model.CreatedOn = model.Visit_Date;
                    _patientDbContext.PatientVisitVitals.Update(model);
                    return await _patientDbContext.SaveChangesAsync();
                }


            }
            return 0;
        }

        public async Task<PatientVisitVitalsModel> GetVitalsById(int vitalsId)
        {
            if (_patientDbContext != null)
            {
                //  var user= await _patientDbContext.PatientVisitVitals.ord
                var users = _patientDbContext.PatientVisitVitals.Where(x=>x.Patient_Id==vitalsId).OrderByDescending(x=>x.VisitId);
                return await users.FirstOrDefaultAsync();
            }
            else
                return null;
        }

        public async Task<List<PatientVisitVitalsModel>> GetAllVitalsDetails(int patientId)
        {
            if (_patientDbContext != null)
                return await _patientDbContext.PatientVisitVitals.Where(x => x.Patient_Id == patientId).ToListAsync();
            else
                return null;
        }
    }
}
