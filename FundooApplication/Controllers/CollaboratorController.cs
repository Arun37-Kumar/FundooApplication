using FundooManagerLayer.Interface;
using FundooModelLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooApplication.Controllers
{
    [ApiController]
    [Route("api/Collaborator")]
    public class CollaboratorController : Controller
    {
        private readonly ICollaboratorManager manager;

        public CollaboratorController(ICollaboratorManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("Add Collaborator")]
        public async Task<IActionResult> AddCollab( CollaboratorModel email)
        {
            try
            {
                var result = this.manager.AddCollab(email);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Email added  Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Error!", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("Delete Collaborator")]
        public async Task<IActionResult> DeleteCollab(string noteId, string collabMail)
        {
            try
            {
                var result = this.manager.DeleteCollab(noteId,collabMail);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Email Delete  Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Error!", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("Fetch Collaborator")]
        public async Task<IActionResult> FetchCollaborator(string noteId)
        {
            try
            {
                var result = this.manager.GetCollaborators(noteId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Email Fetched", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Error!", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
