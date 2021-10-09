using Muharaunda.Core.Models;
using Newtonsoft.Json;
using System;

namespace Munharaunda.Core.Dtos
{
    public class CreateProfileRequest : ProfileBase
    {

        [JsonProperty(PropertyName = "id")]
        public Guid id { get; set; }

    }
}
