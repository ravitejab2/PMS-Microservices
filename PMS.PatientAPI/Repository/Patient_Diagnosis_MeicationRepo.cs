using Microsoft.EntityFrameworkCore;
using PMS.PatientAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Repository
{
    public class Patient_Diagnosis_MeicationRepo:IPatient_Diagnosis_Medication
    {
        private readonly PatientDbContext _patientDbContext;

        public Patient_Diagnosis_MeicationRepo(PatientDbContext patientDbContext)
        {
            _patientDbContext = patientDbContext;
        }
       
      
        public async Task<int> AddDiagnosisDetails(Patient_Diagnosis_MedicationModel model)
        {
            if (_patientDbContext != null)
            {
                model.CreatedOn = DateTime.Now;
                await _patientDbContext.Patient_Diagnosis_Medication.AddAsync(model);
                
                return await _patientDbContext.SaveChangesAsync();


            }

            return 0;
        }

        public async Task<List<Patient_Diagnosis_MedicationModel>> GetAllDiagnosisDetails()
        {
            if (_patientDbContext != null)
                
                return await _patientDbContext.Patient_Diagnosis_Medication.ToListAsync();
            else
                return null;
        }

        public async Task<List<Patient_Diagnosis_MedicationModel>> GetDiagnosisById(int id)
        {
            if (_patientDbContext != null)
            {
                var result = await  _patientDbContext.Patient_Diagnosis_Medication.Where(x => x.PatientId == id).ToListAsync();
                return result;
            }
            else
                return null;
        }

       
        public Task<int> UpdateDiagnosisDetails(int id, Patient_Diagnosis_MedicationModel model)
        {
            throw new NotImplementedException();
        }
    }
}
