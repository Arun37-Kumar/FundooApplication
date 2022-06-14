using FundooModelLayer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManagerLayer.Interface
{
    public interface INotesManager
    {
        Task<NotesModel> AddNotes(NotesModel notes);
        List<NotesModel> GetNotes(string UserId);
        Task<string> EditNotes(NotesModel note);
        Task<string> NoteArchieve(string noteId);

        Task<NotesModel> ChangeColour(NotesModel notesId);
        Task<string> DeleteNotes(string noteId);
        List<NotesModel> GetArchievedNotes(string userId);
        Task<string> RestoreNoteFromTrash(string noteId);
        Task<string> PinningNotes(string noteId);
        Task<string> RetrieveNoteFromTrash(string notesId);
        Task<string> DeleteNotesPermanantly(string notesId);
        Task<string> AddReminder(string notesId, string remind);
        Task<string> DeleteReminder(string noteId);
        public IEnumerable<NotesModel> ShowReminderNotes(string userId);
        Task<NotesModel> PhotoUpload(string noteId, IFormFile image);
        Task<string> RemoveImage(string noteId);
    }
}
