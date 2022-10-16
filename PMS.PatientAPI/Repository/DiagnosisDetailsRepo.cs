using Microsoft.EntityFrameworkCore;
using PMS.PatientAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Repository
{
    public class DiagnosisDetailsRepo : IDiagnosisDetailsRepo
    {
        private readonly PatientDbContext _patientDbContext;

        public DiagnosisDetailsRepo(PatientDbContext patientDbContext)
        {
            _patientDbContext = patientDbContext;
        }

   

        public async Task<List<DiagnosisModel>> GetAllDiagnosis()
        {
            if (_patientDbContext != null)
                return await _patientDbContext.Diagnosis.ToListAsync();
            else
                return null;
        }

    }
}
