using FundooModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositoryLayer.Interface
{
    public interface ICollaboratorRepository
    {
        Task<CollaboratorModel> AddCollab(CollaboratorModel collab);
        Task<string> DeleteCollab(string noteId, string collabMail);

        Task<CollaboratorModel> GetCollaborators(string noteId);
    }
}
