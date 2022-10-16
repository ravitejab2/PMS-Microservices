using Microsoft.EntityFrameworkCore;
using PMS.InboxAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.InboxAPI
{
    public class NotesRepository : INotesRepository
    {
        private readonly NotesDbContext _context;
        public NotesRepository(NotesDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddNotes(NotesModel model)
        {
            if (_context != null)
            {                
                
                if (model.ReplyId != null)
                {
                   var notes= await _context.Notes.FirstOrDefaultAsync(x => x.NoteId == model.ReplyId);
                    notes.IsRepsonded = true;
                     _context.Notes.Update(notes);
                    model.ReplyId = null;
                }
                await _context.Notes.AddAsync(model);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> DeleteNotes(int noteId)
        {
            if (_context != null)
            {
               var note= await _context.Notes.FirstOrDefaultAsync(x => x.NoteId == noteId);
                if (note != null)
                {
                    _context.Notes.Remove(note);
                    return await _context.SaveChangesAsync();
                }
            }
            return 0;
        }

        public async Task<NotesModel> GetNoteById(int noteId)
        {
            if (_context != null)
            {
                var notes= await _context.Notes.FirstOrDefaultAsync(x => x.NoteId == noteId);
                return notes;
            }
            return null;
        }

        public async Task<List<ReceivedNotesModel>> GetReceivedNotes(int userId)
        {
            if (_context != null)
            {
                var notes= await _context.Notes.Where(c => c.ReceiverId == userId).ToListAsync();
                List<ReceivedNotesModel> listReceived = new List<ReceivedNotesModel>();
                
                foreach (var received in notes)
                {
                    ReceivedNotesModel model = new ReceivedNotesModel();
                    model.NoteId = received.NoteId;
                    model.ReceivedDate = received.CreatedDate;

                    model.SenderName = received.SenderName;
                    model.SenderId = received.SenderId;
                    model.SenderDesignation = received.SenderDesignation;
                    model.MessageReceived = received.Message;
                    model.IsUrgent = received.IsUrgent;
                    model.ReceivedDate.ToShortDateString();
                    listReceived.Add(model);
                }
                return listReceived;
            }
            return null;
        }

        public async Task<List<SentNotesModel>> GetSentNotes(int userid)
        {
            if (_context != null)
            {
                var notes = await _context.Notes.Where(c => c.SenderId == userid).ToListAsync();
                List<SentNotesModel> listReceived = new List<SentNotesModel>();

                foreach (var received in notes)
                {
                    SentNotesModel model = new SentNotesModel();
                    model.NoteId = received.NoteId;
                    model.SentDate = received.CreatedDate;
                    model.ReceiverId = received.ReceiverId;
                    model.ReceiverDesignation = received.ReceiverDesignation;
                    model.ReceiverName = received.ReceiverName;
                    model.Message = received.Message;
                    model.Response = received.IsRepsonded;
                    model.IsUrgent = received.IsUrgent;
                    listReceived.Add(model);
                }
                return listReceived;
            }
            return null;
        }
    }
}
