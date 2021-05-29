using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
