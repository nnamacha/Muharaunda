using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Munharaunda.Domain.Dtos;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Muharaunda.Core.Models
{
    [Table("Funerals")]
    public class Funeral: CreateFuneralRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [BsonElement("_id")]
        [JsonProperty("FuneralId")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FuneralId { get; set; }
        
        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }

    }
}
