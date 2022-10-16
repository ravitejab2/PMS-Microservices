using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PMS.PatientAPI.Models;
using PMS.PatientAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.PatientAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddDbContext<PatientDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DBConnection"));

            });

            services.AddScoped<IDemographicDetailsRepo,DemographicDetailsRepo>();
            services.AddScoped<IEmergencyContactRepo,EmergencyContactRepo>();
            services.AddScoped<IAllergyDetailsRepo, AllergyDetailsRepo>();
            services.AddScoped<IVisitVitalsRepo, VisitVitalsRepo>();
            services.AddScoped<IDiagnosisDetailsRepo, DiagnosisDetailsRepo>();
            services.AddScoped<IMedicationDetailsRepo, MedicationDetailsRepo>();
            services.AddScoped<IProcedureDetailsRepo, ProcedureDetailsRepo>();
            services.AddScoped<IPatient_Diagnosis_Medication, Patient_Diagnosis_MeicationRepo>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
