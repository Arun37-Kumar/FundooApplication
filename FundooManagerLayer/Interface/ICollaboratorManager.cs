using FundooModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManagerLayer.Interface
{
    public interface ICollaboratorManager
    {
        Task<CollaboratorModel> AddCollab(CollaboratorModel collab);

        Task<string> DeleteCollab(string noteId, string collabMail);

        Task<CollaboratorModel> GetCollaborators(string noteId);


    }
}
