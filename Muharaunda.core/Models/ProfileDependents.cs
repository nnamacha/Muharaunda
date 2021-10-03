using System.ComponentModel.DataAnnotations;

namespace Muharaunda.Core.Models
{
    public class ProfileDependents
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProfileId { get; set; }
        [Required]
        public int DependentId { get; set; }
    }
}
