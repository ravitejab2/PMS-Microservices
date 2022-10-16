using Microsoft.EntityFrameworkCore;
using PMS.PatientAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Repository
{
    public class ProcedureDetailsRepo : IProcedureDetailsRepo
    {
        private readonly PatientDbContext _patientDbContext;

        public ProcedureDetailsRepo(PatientDbContext patientDbContext)
        {
            _patientDbContext = patientDbContext;
        }

      

        public async Task<List<ProceduresModel>> GetAllProcedures()
        {
            if (_patientDbContext != null)
                return await _patientDbContext.Procedures.ToListAsync();
            else
                return null;
        }

   
    }
}
