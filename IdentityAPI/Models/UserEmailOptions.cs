using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.Models
{
    public class UserEmailOptions
    {
        public UserEmailOptions(string emailtolist,string subject,string body)
        {
            ToEmail = emailtolist;
            Subject = subject;
            Body = body;


        }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        //public List<KeyValuePair<string, string>> PlaceHolders { get; set; }
    }
}
