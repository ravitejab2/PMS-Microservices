using Microsoft.EntityFrameworkCore;
using PMS.PatientAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Repository
{
    public class MedicationDetailsRepo : IMedicationDetailsRepo
    {
        private readonly PatientDbContext _patientDbContext;

        public MedicationDetailsRepo(PatientDbContext patientDbContext)
        {
            _patientDbContext = patientDbContext;
        }

       

        public async Task<List<DrugsModel>> GetAllMedications()
        {
            if (_patientDbContext != null)
                return await _patientDbContext.Drugs.ToListAsync();
            else
                return null;
        }

       
    }
}
