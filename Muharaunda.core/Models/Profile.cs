using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Muharaunda.Core.Models
{

    [Table("Profile")]
    public class Profile : ProfileBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [BsonElement("_id")]
        [JsonProperty("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime Updated { get; set; }
     

    }
}
