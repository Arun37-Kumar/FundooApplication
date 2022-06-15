using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModelLayer
{
    public class CollaboratorModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CollaborationId { get; set; } // Automatically created

        [ForeignKey("NotesModel")]
        public string NotesId { get; set; }
        public virtual NotesModel noteModel { get; set; }
        public string collabEmail { get; set; } // Shared Email

       // public string DeleteMail { get; set; }
    }
}
