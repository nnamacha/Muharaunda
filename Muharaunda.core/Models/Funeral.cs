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
    public class Funeral
    {
        public Guid id { get; set; }
        public Guid FuneralId { get; set; }
        public ProfileBase Profile { get; set; }
        public string Address { get; set; }
        public DateTime DateOfDeath { get; set; }        
        public Audit Audit { get; set; }
        public string Pk { get; set; }
    }
}
