using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FundooModelLayer;
using FundooRepositoryLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositoryLayer.Service
{
    public class NotesRepository : INotesRepository
    {
        private readonly IMongoCollection<NotesModel> Note;
        private readonly IConfiguration configuration;

        public NotesRepository(IFundooDatabaseSettings settings, IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            Note = database.GetCollection<NotesModel>("Note");
        }

        /// <summary>
        /// Adding Notes DONE
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public async Task<NotesModel> AddNote(NotesModel note)
        {
            try
            {
                if (note.Title != null || note.Description != null)
                {
                    await this.Note.InsertOneAsync(note);
                    return note;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// GetNotes DONE
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<NotesModel> GetNotes(string userId)
        {
            try
            {
                List<NotesModel> notes = this.Note.AsQueryable().Where(x => x.UserId == userId && x.Archieve == false && x.Trash == false).ToList();
                if (notes != null)
                {
                    return notes;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        /// <summary>
        /// Edit Notes - DONE
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public async Task<string> EditNotes(NotesModel note)
        {
            try
            {
                var currentNoteId = this.Note.AsQueryable().Where(x => x.NotesId == note.NotesId).FirstOrDefault();
                if (currentNoteId != null)
                {
                    if (note != null)
                    {
                        await this.Note.UpdateOneAsync(x => x.NotesId == note.NotesId,
                            Builders<NotesModel>.Update.Set(a => a.Description,note.Description));
                        await this.Note.UpdateOneAsync(x => x.NotesId == note.NotesId,
                            Builders<NotesModel>.Update.Set(a => a.Title, note.Title));
                        return "Edited Notes";
                    }
                    else
                    {
                        return "Note Edited";
                    }
                }
                else
                {
                    return "Note is null";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Change Colour - Incomplete
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public async Task<NotesModel> ChangeColour(NotesModel notesId)
        {
            try
            {
                var currentNote = this.Note.AsQueryable().Where(x => x.NotesId == notesId.NotesId).FirstOrDefault();
                if (currentNote != null)
                {
                        await this.Note.UpdateOneAsync(x => x.NotesId == notesId.NotesId,
                            Builders<NotesModel>.Update.Set(x => x.Colour, notesId.Colour));
                        return notesId;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Notes Archieve DONE
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns></returns>
        public async Task<string> NoteArchieve(string noteId)
        {
            try
            {
                string message;
                var availableNotes = this.Note.AsQueryable().Where(x => x.NotesId == noteId).SingleOrDefault();
                if (availableNotes != null)
                {
                    if (availableNotes.Archieve.Equals(false))
                    {
                        await this.Note.UpdateOneAsync(x => x.NotesId == noteId,
                            Builders<NotesModel>.Update.Set(x => x.Pinned, false));
                        await this.Note.UpdateOneAsync(x => x.NotesId == noteId,
                            Builders<NotesModel>.Update.Set(x => x.Archieve, true));
                         message = "Notes Archieved";
                        return message;
                    }

                    if (availableNotes.Archieve.Equals(true))
                    {
                        await this.Note.UpdateOneAsync(x => x.NotesId == noteId,
                            Builders<NotesModel>.Update.Set(x => x.Archieve, false));
                        message = "Notes Unarchieved";
                        return message;
                    }
                }
                message = "Notes Id do not exist";
                return message;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Pinning Notes DONE
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns></returns>
        public async Task<string> PinningNotes(string noteId)
        {
            try
            {
                string res;
                var isNoteId = this.Note.AsQueryable().Where(x => x.NotesId == noteId).FirstOrDefault();
                if (isNoteId != null)
                {
                    if (isNoteId.Pinned.Equals(false))
                    {
                        await this.Note.UpdateOneAsync(x => x.NotesId == noteId,
                            Builders<NotesModel>.Update.Set(x => x.Pinned, true));
                        if (isNoteId.Archieve.Equals(true))
                        {
                            await this.Note.UpdateOneAsync(x => x.NotesId == noteId,
                            Builders<NotesModel>.Update.Set(x => x.Archieve, false));
                            res = "Note unarchived and pinned";
                        }
                        else
                        {
                            res = "Note pinned";
                        }
                    }
                    else
                    {
                        await this.Note.UpdateOneAsync(x => x.NotesId == noteId,
                            Builders<NotesModel>.Update.Set(x => x.Pinned, false));
                        res = "Note unpinned";
                    }
                }
                else
                {
                    res = "Note does not exist";
                }
                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Delete Notes - DONE
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns></returns>
        public async Task<string> DeleteNotes(string noteId)
        {
            try
            {
                var currentNoteId =  this.Note.AsQueryable().Where(a => a.NotesId == noteId).FirstOrDefault();
                if (currentNoteId != null)
                {
                    if (currentNoteId.Pinned.Equals(true))
                    {
                        await this.Note.UpdateOneAsync(x => x.NotesId == noteId,
                            Builders<NotesModel>.Update.Set(x => x.Pinned, false)); // pinned set to false

                        await this.Note.UpdateOneAsync(x => x.NotesId == noteId,
                            Builders<NotesModel>.Update.Set(x => x.Trash, true)); // trash set to true


                        return "Note unpinned and Trashed";
                    }
                    else
                    {
                        await this.Note.UpdateOneAsync(x => x.NotesId == noteId,
                        Builders<NotesModel>.Update.Set(x => x.Trash, true));
                        return noteId; 
                    }
                }
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get Archieved Notes - DONE
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public  List<NotesModel> GetArchievedNotes(string userId)
        {
            try
            {
                var savedNotes =  this.Note.AsQueryable().Where(a => a.UserId == userId && a.Trash.Equals(false) && a.Archieve.Equals(true)).ToList();
                if(savedNotes != null)
                {
                    return savedNotes;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Restore Notes from trash
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns></returns>
        public async Task<string> RestoreNoteFromTrash(string noteId)
        {
            try
            {
                var isNoteId = this.Note.AsQueryable().Where(x => x.NotesId == noteId && x.Trash.Equals(true)).FirstOrDefault();
                if (isNoteId != null)
                {
                    await this.Note.UpdateOneAsync(t => t.NotesId == noteId,
                        Builders<NotesModel>.Update.Set(t => t.Trash,false));
                    return "Note Restored from trash";
                }
                else
                {
                    return "Note does not exist in trash";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Retrieve Notes From Trash - DONE 
        /// </summary>
        /// <param name="notesId"></param>
        /// <returns></returns>
        public async Task<string> RetrieveNoteFromTrash(string notesId)
        {
            try
            {
                var currentNotes = this.Note.AsQueryable().Where(x => x.NotesId == notesId && x.Trash.Equals(true)).FirstOrDefault();
                if (currentNotes != null)
                {
                    await this.Note.UpdateOneAsync(x => x.NotesId == notesId,
                        Builders<NotesModel>.Update.Set(a => a.Trash, false));
                    return "Note Retrieved";
                }
                else
                {
                    return "Note not Retrieved";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete Notes Permanatly - DONE
        /// </summary>
        /// <param name="notesId"></param>
        /// <returns></returns>
        public async Task<string> DeleteNotesPermanantly(string notesId)
        {
            try
            {
                var currentNotes = this.Note.AsQueryable().Where(x => x.NotesId == notesId).FirstOrDefault();
                if (currentNotes != null)
                {
                    await this.Note.DeleteOneAsync(b => b.NotesId == notesId);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Add Reminder -  DONE
        /// </summary>
        /// <param name="notesId"></param>
        /// <param name="remind"></param>
        /// <returns></returns>
        public async Task<string> AddReminder(string notesId, string remind)
        {
            try
            {
                var availableNotes = this.Note.AsQueryable().Where(x => x.NotesId == notesId).SingleOrDefault();
                if (availableNotes != null)
                {
                    await this.Note.UpdateOneAsync(g => g.NotesId == notesId,
                        Builders<NotesModel>.Update.Set(g => g.Reminder, remind));
                    return "Remind me";
                }
                else
                {
                    return "This note does not exist";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete Reminder - DONE
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns></returns>
        public async Task<string> DeleteReminder(string noteId)
        {
            try
            {
                var availableNotes = this.Note.AsQueryable().Where(x => x.NotesId == noteId).FirstOrDefault();
                if (availableNotes != null)
                {
                    await this.Note.UpdateOneAsync(g => g.NotesId == noteId,
                         Builders<NotesModel>.Update.Set(g => g.Reminder, null));
                    return "Reminder Deleted";
                }
                return "This note does not exists";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Show Reminder Notes - DONE
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<NotesModel> ShowReminderNotes(string userId)
        {
            try
            {
                IEnumerable<NotesModel> currentNotes = this.Note.AsQueryable().Where(x => x.UserId == userId);
                if (currentNotes != null)
                {
                    return currentNotes;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Image Upload - DONE
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public async Task<NotesModel> PhotoUpload(string noteId, IFormFile image)
        {
            try
            {
                Account account = new Account(this.configuration["CloudinaryAccount:Name"], this.configuration["CloudinaryAccount:ApiKey"], this.configuration["CloudinaryAccount:ApiSecret"]);

                var cloudinary = new Cloudinary(account);
                var uploadParameters = new ImageUploadParams()
                {
                   File = new FileDescription(image.FileName, image.OpenReadStream()),
                };
                var uploadResult = cloudinary.Upload(uploadParameters);
                string imagePath = uploadResult.Url.ToString();
                var noteCheck = this.Note.AsQueryable().Where(x => x.NotesId == noteId).SingleOrDefault();
                if(noteCheck != null)
                {
                    noteCheck.Image = imagePath;
                    await this.Note.UpdateOneAsync(x => x.NotesId == noteId, Builders<NotesModel>.Update.Set(x => x.Image, noteCheck.Image));
                    return noteCheck;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> RemoveImage(string noteId)
        {
            try
            {
                var checkNote = this.Note.AsQueryable().Where(c => c.NotesId == noteId).SingleOrDefault();
                if(checkNote != null)
                {
                    //checkNote.Image = null;
                    await this.Note.UpdateOneAsync(c => c.NotesId == noteId,
                        Builders<NotesModel>.Update.Set(c => c.Image, null));
                    return "Removed Successfully";
                }
                return "The note does not exist";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
