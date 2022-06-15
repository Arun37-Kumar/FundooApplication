using FundooModelLayer;
using FundooRepositoryLayer.Interface;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositoryLayer.Service
{
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private readonly IMongoCollection<CollaboratorModel> Collab;
        private readonly IConfiguration configuration;

        public CollaboratorRepository(IFundooDatabaseSettings settings, IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            Collab = database.GetCollection<CollaboratorModel>("Collab");
        }

        /// <summary>
        /// Adding Email - DONE
        /// </summary>
        /// <param name="collab"></param>
        /// <returns></returns>
        public async Task<CollaboratorModel> AddCollab(CollaboratorModel collab)
        {
            try
            {
                    await this.Collab.InsertOneAsync(collab);
                    return collab;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message) ;
            }
        }

        /// <summary>
        /// Delete Collab - DONE
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="collabMail"></param>
        /// <returns></returns>
        public async Task<string> DeleteCollab(string noteId,string collabMail)
        {
            try
            {
                var result = this.Collab.AsQueryable().Where(t => t.CollaborationId == noteId && t.collabEmail == collabMail).FirstOrDefault();
                if(result != null)
                {
                   await this.Collab.DeleteOneAsync(c => c.NotesId == noteId);
                    return "Removed Succefully";
                }
                else
                {
                    return "Mail does not exist";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get Collaborators - DONE
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns></returns>
        public async Task<CollaboratorModel> GetCollaborators(string noteId)
        {
            try
            {
                var fetchCollab = await this.Collab.AsQueryable().Where(t => t.NotesId == noteId).FirstOrDefaultAsync();
                if( fetchCollab != null )
                {
                    return fetchCollab;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}
