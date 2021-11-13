using Muharaunda.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munharaunda.Domain.Models
{
    public class FuneralPayment
    {
        public Funeral Funeral { get; set; }
        public bool Paid { get; set; }
        public decimal Amount { get; set; }
    }
}
