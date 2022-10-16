using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.InboxAPI.Models
{
    public class NotesModel
    {
        public NotesModel()
        {
            CreatedDate = DateTime.Now;
        }
        [Key]
        public int NoteId { get; set; }

        [Required]
        public int SenderId { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string SenderDesignation { get; set; }
        public string Message { get; set; }
        public string ReceiverName { get; set; }
        public int ReceiverId { get; set; }
        public string ReceiverDesignation { get; set; }
        public string ReceiverEmail { get; set; }
        public int? ReplyId { get; set; }
        public bool IsRepsonded { get; set; }
        public string IsUrgent { get; set; }
        public DateTime CreatedDate { get; set; }       


    }
}
