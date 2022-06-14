using FundooModelLayer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositoryLayer.Interface
{
    public interface INotesRepository
    {
        Task<NotesModel> AddNote(NotesModel note);
        List<NotesModel> GetNotes(string userId);
        Task<string> EditNotes(NotesModel note);
        Task<NotesModel> ChangeColour(NotesModel notesId);
        Task<string> NoteArchieve(string noteId);
        Task<string> PinningNotes(string noteId);
        Task<string> DeleteNotes(string noteId);
        List<NotesModel> GetArchievedNotes(string userId);
        Task<string> RestoreNoteFromTrash(string noteId);
        Task<string> RetrieveNoteFromTrash(string notesId);
        Task<string> DeleteNotesPermanantly(string notesId);
        Task<string> AddReminder(string notesId, string remind);
        Task<string> DeleteReminder(string noteId);
        IEnumerable<NotesModel> ShowReminderNotes(string userId);

        Task<NotesModel> PhotoUpload(string noteId, IFormFile image);
        Task<string> RemoveImage(string noteId);
    }
}
