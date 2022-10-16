using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.AppointmentAPI.DataModels
{
    public class SlotTimings
    {
        [Key]
        public int SlotId { get; set; }
        public string SlotTiming { get; set; }
        public TimeSpan SlotStart { get; set; }
        public TimeSpan SlotEnd { get; set; }

    }
}
