using FundooManagerLayer.Interface;
using FundooModelLayer;
using FundooRepositoryLayer.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManagerLayer.Services
{
    public class NotesManager : INotesManager
    {
        private readonly INotesRepository repository;

        public NotesManager(INotesRepository repository)
        {
            this.repository = repository;
        }

        public async Task<NotesModel> AddNotes(NotesModel notes)
        {
            try
            {
                return await this.repository.AddNote(notes);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Retrieve Notes
        /// </summary>
        /// <param name="notes"></param>
        /// <returns></returns>
        public List<NotesModel> GetNotes(string userId)
        {
            try
            {
                return this.repository.GetNotes(userId);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> EditNotes(NotesModel note)
        {
            try
            {
                return this.repository.EditNotes(note);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<NotesModel> ChangeColour(NotesModel notesId)
        {
            try
            {
                return this.repository.ChangeColour(notesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public  Task<string> NoteArchieve(string noteId)
        {
            try
            {
                return this.repository.NoteArchieve(noteId);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> PinningNotes(string noteId)
        {
            try
            {
                return this.repository.PinningNotes(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> DeleteNotes(string noteId)
        {
            try
            {
                return this.repository.DeleteNotes(noteId);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<NotesModel> GetArchievedNotes(string userId)
        {
            try
            {
                return this.repository.GetArchievedNotes(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> RestoreNoteFromTrash(string noteId)
        {
            try
            {
                return this.repository.RestoreNoteFromTrash(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public  Task<string> RetrieveNoteFromTrash(string notesId)
        {
            try
            {
                return this.repository.RetrieveNoteFromTrash(notesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public  Task<string> DeleteNotesPermanantly(string notesId)
        {
            try
            {
                return this.repository.DeleteNotesPermanantly(notesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public  Task<string> AddReminder(string notesId, string remind)
        {
            try
            {
                return this.repository.AddReminder(notesId,remind);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public  Task<string> DeleteReminder(string noteId)
        {
            try
            {
                return this.repository.DeleteReminder(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<NotesModel> ShowReminderNotes(string userId)
        {
            try
            {
                return this.repository.ShowReminderNotes(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<NotesModel> PhotoUpload(string noteId, IFormFile image)
        {
            try
            {
                return this.repository.PhotoUpload(noteId,image);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> RemoveImage(string noteId)
        {
            try
            {
                return this.repository.RemoveImage(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
