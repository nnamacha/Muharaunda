using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muharaunda.Core.Models
{
    public  class ProfileDependents
    {
        [Key]
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public int DependentId { get; set; }
    }
}
