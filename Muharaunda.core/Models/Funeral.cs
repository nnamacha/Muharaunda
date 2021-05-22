using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muharaunda.Core.Models
{
    [Table("Funerals")]
    public class Funeral
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FuneralId { get; set; }
        public DateTime DateOfDeath { get; set; }
        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }
        public int ProfileId { get; set; }
        
    }
}
