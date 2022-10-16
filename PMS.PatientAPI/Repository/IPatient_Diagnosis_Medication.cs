using PMS.PatientAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Repository
{
     public interface IPatient_Diagnosis_Medication
    {
        Task<List<Patient_Diagnosis_MedicationModel>> GetAllDiagnosisDetails();

        Task<int> UpdateDiagnosisDetails(int id, Patient_Diagnosis_MedicationModel model);
        Task<int> AddDiagnosisDetails(Patient_Diagnosis_MedicationModel model);

        Task <List<Patient_Diagnosis_MedicationModel>> GetDiagnosisById(int id);

    }
}
