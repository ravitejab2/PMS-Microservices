using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PMS.UserAPI.Models
{
    public class LocationModel
    {
        public LocationModel()
        {
            Email = "CTEmergencyContact@gmail.com";
        }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Email { get; set; }
    }
}