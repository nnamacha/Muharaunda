using System.ComponentModel.DataAnnotations;

namespace Munharaunda.Core.Models
{
    public class ProfileNextOfKin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProfileId { get; set; }
        [Required]
        public int NextOfKinProfileId { get; set; }

    }
}
