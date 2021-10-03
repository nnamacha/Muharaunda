using System;

namespace Munharaunda.Domain.Dtos
{
    public class CreateFuneralRequest
    {
        public int ProfileId { get; set; }        
        public string Address { get; set; }
        public DateTime DateOfDeath { get; set; }
    }
}
