using FundooManagerLayer.Interface;
using FundooModelLayer;
using FundooRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManagerLayer.Services
{
    public class CollaboratorManager : ICollaboratorManager
    {
        private readonly ICollaboratorRepository repository;

        public CollaboratorManager(ICollaboratorRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Add collab
        /// </summary>
        /// <param name="collab"></param>
        /// <returns></returns>
        public async Task<CollaboratorModel> AddCollab(CollaboratorModel collab)
        {
            try
            {
                return await this.repository.AddCollab(collab);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete Collab
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="collabMail"></param>
        /// <returns></returns>
        public async Task<string> DeleteCollab(string noteId, string collabMail)
        {
            try
            {
                return await this.repository.DeleteCollab(noteId,collabMail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CollaboratorModel> GetCollaborators(string noteId)
        {
            try
            {
                return await this.repository.GetCollaborators(noteId);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}
