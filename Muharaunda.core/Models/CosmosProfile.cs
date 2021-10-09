using Muharaunda.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munharaunda.Domain.Models
{
    public class CosmosProfile: ProfileBase
    {
        [JsonProperty(PropertyName = "id")]
        public Guid id { get; set; }
    }
}
