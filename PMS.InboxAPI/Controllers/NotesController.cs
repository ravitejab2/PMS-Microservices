using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.InboxAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.InboxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesRepository _notesRepository;
        public NotesController(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        [HttpPost("AddNote")]
        public async Task<object> AddNotes([FromBody] NotesModel model)
        {
            var result = await _notesRepository.AddNotes(model);
            if (result > 0)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Notes Sent Successfully", null));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error in sending notes", result));
        }

        [HttpGet("AllSentNotes/{userId}")]
        public async Task<object> GetAllsentNotes(int userId)
        {
             var result = await _notesRepository.GetSentNotes(userId);
            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "notes not found with user", result));
        }
        [HttpGet("AllReceivedNotes/{userId}")]
        public async Task<object> GetAllReceivedNotes( int userId)
        {
            var result = await _notesRepository.GetReceivedNotes(userId);
            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "notes not found with user", result));
        }

        [HttpDelete("DeleteNote/{noteId}")]
        public async Task<object> DeleteNotes(int noteId)
        {
            var result = await _notesRepository.DeleteNotes(noteId);
            if (result > 0)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Notes deleted successfully", null));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error in deleting notes", result));
        }

        [HttpGet("GetNoteById/{noteId}")]
        public async Task<object> GetNotesById(int noteId)
        {
            var result = await _notesRepository.GetNoteById(noteId);
            if (result != null)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "notes not found", result));
        }
    }
}
