using System;
using static Muharaunda.Core.Constants.SystemWideConstants;

namespace Muharaunda.Core.Models
{
    public class ProfileBase
    {
        public int ProfileId { get; set; }
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string DateOfBirth { get; set; }

        public IdentificationTypes IdentificationType { get; set; }

        public string IdentificationNumber { get; set; }

        public string MobileNumber { get; set; }


        public string Email { get; set; }


        public int NextOfKin { get; set; }

        public ProfileTypes ProfileType { get; set; }

        public ProfileStatuses ProfileStatus { get; set; }

        public DateTime ActivationDate { get; set; }

        public string Address { get; set; }
        public string Image { get; set; }

        public int CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime Approved { get; set; }
    }
}
