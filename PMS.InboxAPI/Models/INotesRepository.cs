using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.InboxAPI.Models
{
    public interface INotesRepository
    {
        Task<List<SentNotesModel>> GetSentNotes(int userid);
        Task<List<ReceivedNotesModel>> GetReceivedNotes(int userId);
        Task<int> AddNotes(NotesModel model);
        Task<int> DeleteNotes(int noteId);
        Task<NotesModel> GetNoteById(int noteId);
    }
}
