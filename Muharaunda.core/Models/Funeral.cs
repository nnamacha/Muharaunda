using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Muharaunda.Core.Contracts;
using Muharaunda.Domain.Models;
using Munharaunda.Domain.Contracts;
using Munharaunda.Domain.Dtos;
using Munharaunda.Domain.Models;
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
        public IProfileBase Profile { get; set; }
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get ; set ; }
        public DateTime Updated { get ; set ; }
        public int ApprovedBy { get ; set ; }
        public DateTime Approved { get ; set ; }
        public Audit Audit { get; set; }
    }
}
