using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.AppointmentAPI.DataModels
{
    public class AddAppointmentModel
    {
          public DateTime SelctedDate { get; set; }
            public int PhyisicanId { get; set; }
            public int PatientId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string CreatedBy { get; set; }
            public int SlotId { get; set; }   
    }

}

