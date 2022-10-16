using Microsoft.EntityFrameworkCore;
using System;

namespace PMS.PatientAPI.Models
{
    public class PatientDbContext:DbContext
    {
        public PatientDbContext(DbContextOptions<PatientDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("DBConnection"));
            }
        }

        public DbSet<AllergyModel> Allergies { get; set; }

        public DbSet<DiagnosisModel> Diagnosis { get; set; }

        public DbSet<ProceduresModel> Procedures { get; set; }

        public DbSet<DrugsModel> Drugs { get; set; }

        public DbSet<PatientDemographicDetailsModel> PatientDemographics { get; set; }

        public DbSet<PatientAllergyDetailsModel> PatientAllergies { get; set; }

        public DbSet<PatientEmergencyContactModel> PatientEmergencyContacts { get; set; }

        public DbSet<PatientVisitVitalsModel> PatientVisitVitals { get; set; }

        public DbSet<PatientVisitDiagnosisModel> PatientVisitDiagnosis { get; set; }

        public DbSet<PatientVisitProceduresModel> PatientVisitProcedures { get; set; }

        public DbSet<PatientVisitMedicationModel> PatientVisitMedications { get; set; }

        public DbSet<Patient_Diagnosis_MedicationModel> Patient_Diagnosis_Medication { get; set; }


    }
}
