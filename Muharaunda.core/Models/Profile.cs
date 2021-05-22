using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Muharaunda.Core.Constants.SystemWideConstants;

namespace Muharaunda.Core.Models
{

    [Table("Profiles")]
    public class Profile
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfileId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string DateOfBirth { get; set; }
        [Required]
        public IdentificationTypes IdentificationType { get; set; }
        [Required]
        public string IdentificationNumber { get; set; }
        [Required]
        public ProfileTypes ProfileType { get; set; }
        [Required]
        public ProfileStatuses ProfileStatus { get; set; }
        [Required]
        public string Address { get; set; }
        public string Image { get; set; }

    }
}
