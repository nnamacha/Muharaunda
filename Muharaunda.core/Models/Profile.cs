using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Muharaunda.Domain.Models;
using Munharaunda.Domain.Contracts;
using Munharaunda.Domain.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Muharaunda.Core.Models
{


    public class Profile : ProfileBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [BsonElement("_id")]
        [JsonProperty("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid _id { get; set; }



    }
}
