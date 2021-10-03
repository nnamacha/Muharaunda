using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Muharaunda.Core.Constants.SystemWideConstants;

namespace Muharaunda.Core.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }
        public TransactionTypes TransactionType { get; set; }
        public decimal Amount { get; set; }

        [ForeignKey("FuneralId")]
        public Funeral Funeral { get; set; }
        public int FuneralId { get; set; }
        [Column("Description")]
        public string TransactionDescription { get; set; }
        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Created { get; set; }

        public int UpdatedBy { get; set; }
        public DateTime Updated { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime Approved { get; set; }



    }
}
