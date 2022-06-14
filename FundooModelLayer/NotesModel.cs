using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModelLayer
{
    public class NotesModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string NotesId { get; set; }

        [ForeignKey("RegisterModel")]
        public RegisterModel registerModel { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Reminder { get; set; }
        public string Colour { get; set; }

        public string Image { get; set; }

        [DefaultValue(false)]
        public bool Archieve { get; set; }

        [DefaultValue(false)]
        public bool Trash { get; set; }

        [DefaultValue(false)]
        public bool Pinned { get; set; }
    }
}
