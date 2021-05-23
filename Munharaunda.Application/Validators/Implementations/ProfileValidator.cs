using FluentValidation;
using Muharaunda.Core.Contracts;
using Muharaunda.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static Muharaunda.Core.Constants.SystemWideConstants;

namespace Munharaunda.Application.Validators.Implementations
{
    public class ProfileValidator : AbstractValidator<Profile>
    {
        private readonly IAppSettings _appSettings;

        public ProfileValidator(IAppSettings appSettings)
        {
            RuleFor(x => x.ProfileType).IsInEnum();

            RuleFor(x => x.Email)
                .Must(IsValidEmailAddress)
                .WithMessage("Invalid Email Address");
            
            RuleFor(x => x.DateOfBirth)
                .Must(IsValidDateOfBirth)
                .WithMessage($"Invalid Dependent or Member must be old than {_appSettings.MinAgeInMonths} months") ;

            _appSettings = appSettings;
        }



        private bool IsValidEmailAddress(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }

        }
        private bool IsValidDateOfBirth(string dob)
        {
            try
            {
                var dateOfBirth = Convert.ToDateTime(dob);

                return (_appSettings.MinAgeInMonths <= dateOfBirth.Subtract(DateTime.Now).Days / (365.2425 / 12));

                
            }
            catch (Exception)
            {

                return false;
            }
        }

        private bool IsValidMobileNumber(string mobileNumber)
        {
            if (mobileNumber.Substring(0, 1) == "+")
                return mobileNumber.Length == (_appSettings.LengthForMobileNumber + 2);
            else if (mobileNumber.Substring(0, 1) == "00")
                return mobileNumber.Length == (_appSettings.LengthForMobileNumber + 2);
            else
               return mobileNumber.Length == _appSettings.LengthForMobileNumber;
        }

        
    }
}
