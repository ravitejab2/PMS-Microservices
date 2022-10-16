using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.PatientAPI.Models;
using PMS.PatientAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.PatientAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientDiagnosisMedicationController : ControllerBase
    {
        private readonly IPatient_Diagnosis_Medication _diagnosis_Medication;
       
        private readonly IDiagnosisDetailsRepo _diagnosisDetailsRepo;
        private readonly IMedicationDetailsRepo _medicationDetailsRepo;
        private readonly IProcedureDetailsRepo _procedureDetailsRepo;


        public PatientDiagnosisMedicationController(IProcedureDetailsRepo procedureDetailsRepo, IPatient_Diagnosis_Medication diagnosis_Medication,IMedicationDetailsRepo medicationDetailsRepo, IDiagnosisDetailsRepo diagnosisDetailsRepo)
        {
            _diagnosis_Medication = diagnosis_Medication;
            _diagnosisDetailsRepo = diagnosisDetailsRepo;
            _procedureDetailsRepo = procedureDetailsRepo;
            _medicationDetailsRepo = medicationDetailsRepo;


        }

        [HttpGet("GetDiagnosis")]
        public async Task<object> GetAllDiagnosisDetails()
        {
            var result = await _diagnosis_Medication.GetAllDiagnosisDetails();
           
            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error while retrieving ", result));
        }


        [HttpPost("PostDiagnosis")]
        public async Task<object> AddDiagnosis_MedicationDetails([FromBody] Patient_Diagnosis_MedicationModel model)
        {
            var result = await _diagnosis_Medication.AddDiagnosisDetails(model);
            if (result > 0)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Details Added", null));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error in adding details", result));
        }

       
        
        
        [HttpGet("GetDiagnosis/{id}")]
    
        public async Task<object> GetAllDiagnosibyId(int id)
        {
            var result = await _diagnosis_Medication.GetDiagnosisById(id);

            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error while retrieving ", result));
        }












        [HttpGet("allProcedures")]
        public async Task<object> GetAllProcedures()
        {
            var result = await _procedureDetailsRepo.GetAllProcedures();
            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error while retrieving ", result));
        }

        [HttpGet("allMedications")]
        public async Task<object> GetAllMedications()
        {
            var result = await _medicationDetailsRepo.GetAllMedications();
            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error while retrieving ", result));
        }

        [HttpGet("allDiagnosis")]
        public async Task<object> GetAllDiagnosis()
        {
            var result = await _diagnosisDetailsRepo.GetAllDiagnosis();
            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error while retrieving ", result));
        }

    }
}
