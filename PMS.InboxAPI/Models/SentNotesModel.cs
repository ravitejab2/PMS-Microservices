using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.InboxAPI.Models
{
    public class SentNotesModel
    {
        public int NoteId { get; set; }
        public DateTime SentDate { get; set; }
        public int ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverDesignation { get; set; }
        public string Message { get; set; }
        public string IsUrgent { get; set; }
        public bool Response { get; set; }
    }

}
