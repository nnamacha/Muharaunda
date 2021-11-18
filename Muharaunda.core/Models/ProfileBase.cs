using Muharaunda.Core.Contracts;
using Munharaunda.Domain.Models;
using System;
using System.Collections.Generic;
using static Muharaunda.Core.Constants.SystemWideConstants;

namespace Muharaunda.Domain.Models
{
    public interface IProfileBase
    {
        public Guid id { get; set; }
        DateTime ActivationDate { get; set; }
        string Address { get; set; }
        Audit Audit { get; set; }
        DateTime DateOfBirth { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        List<FuneralPayment> FuneralPayments { get; set; }
        string IdentificationNumber { get; set; }
        IdentificationTypes IdentificationType { get; set; }
        string Image { get; set; }
        string MobileNumber { get; set; }
        int NextOfKin { get; set; }
        int ProfileId { get; set; }
        Statuses ProfileStatus { get; set; }
        ProfileTypes ProfileType { get; set; }
        string Surname { get; set; }
    }

    public class ProfileBase: IProfileBase
    {
        public Guid id { get ; set ; }
        public int ProfileId { get; set; }
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public IdentificationTypes IdentificationType { get; set; }

        public string IdentificationNumber { get; set; }

        public string MobileNumber { get; set; }


        public string Email { get; set; }


        public int NextOfKin { get; set; }

        public ProfileTypes ProfileType { get; set; }

        public Statuses ProfileStatus { get; set; }

        public DateTime ActivationDate { get; set; }

        public string Address { get; set; }
        public string Image { get; set; }

        public List<FuneralPayment> FuneralPayments { get; set; }
        public Audit Audit { get; set; }
        
    }
}
