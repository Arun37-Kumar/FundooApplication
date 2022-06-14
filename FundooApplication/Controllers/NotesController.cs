using FundooManagerLayer.Interface;
using FundooManagerLayer.Services;
using FundooModelLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooApplication.Controllers
{
    [ApiController]
    [Route("api/Notes")]
    public class NotesController : Controller
    {
        private readonly INotesManager manager;

        public NotesController(INotesManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("AddNotes")]
        public async Task<IActionResult> CreateNotes([FromBody] NotesModel notes)
        {
            try
            {
                var result = this.manager.AddNotes(notes);
                if(result != null)
                {
                    return this.Ok(new { Status = true, Message = "Note Created Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Note Created UnSuccessfully", Data = result });
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("GetNotes")]
        public async Task<IActionResult> GetNotes([FromBody] string userId)
        {
            try
            {
                var result = this.manager.GetNotes(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Note Received", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Note Received UnSuccessfully", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("EditNotes")]
        public async Task<IActionResult> EditNotes([FromBody] NotesModel note)
        {
            try
            {
                var result = this.manager.EditNotes(note);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Notes Edited", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Note Not Edited", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("ChangeColor")]
        public async Task<IActionResult> ChangeColor([FromBody] NotesModel color)
        {
            try
            {
                var result = await this.manager.ChangeColour(color);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Color Changed", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Color not changed", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("ArchievedNotes")]
        public async Task<IActionResult> ArchievedNotes([FromBody] string noteId)
        {
            try
            {
                var result = this.manager.NoteArchieve(noteId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Archied", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Not Archied", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPatch]
        [Route("Notes Pinned")]
        public async Task<IActionResult> Pinned([FromBody] string noteId)
        {
            try
            {
                var result = this.manager.PinningNotes(noteId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Notes Pinned", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Notes not Pinned", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("DeleteNotes")]
        public async Task<IActionResult> DeleteNotes([FromBody] string noteId)
        {
            try
            {
                var result = this.manager.DeleteNotes(noteId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Moved to trash", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Not Moved", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("GetArchievedNotes")]
        public async Task<IActionResult> GetArchievedNotes([FromBody] string noteId)
        {
            try
            {
                var result = this.manager.GetArchievedNotes(noteId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Notes Fetched", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Notes not fetched", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("Restore from trash")]
        public async Task<IActionResult> RestoreFromTrash([FromBody] string noteId)
        {
            try
            {
                var result = this.manager.RestoreNoteFromTrash(noteId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Restored", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Restored Unsuccessfull", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("Retrieve Notes From Trash")]
        public async Task<IActionResult> RetrieveNotes([FromBody] string noteId)
        {
            try
            {
                var result = this.manager.RetrieveNoteFromTrash(noteId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Unsuccessfull", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("Delete Notes From Trash")]
        public async Task<IActionResult> DeleteForever([FromBody] string noteId)
        {
            try
            {
                var result = this.manager.DeleteNotesPermanantly(noteId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Notes Deleted", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Notes not Deleted", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddReminder")]
        public async Task<IActionResult> AddReminder(string noteId,string remind)
        {
            try
            {
                var result = await this.manager.AddReminder(noteId,remind);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Notes Reminded", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Notes not Reminded", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteReminder")]
        public async Task<IActionResult> DeleteReminder([FromBody] string noteId)
        {
            try
            {
                var result = this.manager.DeleteReminder(noteId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Notes Reminder Deleted", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Notes Reminded not Deleted", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("Show Reminder")]
        public async Task<IActionResult> ShowReminderNotes([FromBody] string noteId)
        {
            try
            {
                var result = this.manager.ShowReminderNotes(noteId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Notes Reminder Show", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Notes Reminder not Show", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("Add Image")]
        public async Task<IActionResult> AddImage( string noteId,IFormFile image)
        {
            try
            {
                var result = this.manager.PhotoUpload(noteId,image);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Image Uploaded Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Image Uploaded Unsuccessful", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("Remove Image")]
        public async Task<IActionResult> RemoveImage(string noteId)
        {
            try
            {
                var result = this.manager.RemoveImage(noteId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Image Removed Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Image Removed Unsuccessful", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
