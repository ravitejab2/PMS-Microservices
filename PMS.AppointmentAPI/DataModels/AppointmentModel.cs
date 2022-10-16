using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.AppointmentAPI.DataModels
{
    public class AppointmentModel
    {
        public AppointmentModel()
        {
            CreatedDate = DateTime.Now;
        }
        [Key]
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string Meetingtitle { get; set; }

        public string Description { get; set; }
        public int PhysicianId { get; set; }
        public DateTime Appointmentdate { get; set; }
        public DateTime? AppointmentStartdate { get; set; }
        public DateTime? AppointmentEnddate { get; set; }
        public int SlotId { get; set; }
        public string Status { get; set; }       
        public DateTime? CreatedDate { get; set; } 

        
    }
}
