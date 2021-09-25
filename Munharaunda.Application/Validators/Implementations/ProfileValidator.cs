using FluentValidation;
using Muharaunda.Core.Contracts;
using Muharaunda.Core.Models;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Munharaunda.Application.Validators.Implementations
{
    public class ProfileValidator : AbstractValidator<Profile>
    {
        private readonly IAppSettings _appSettings;
        private readonly IProfileRespository _profileRepository;

        public ProfileValidator(IAppSettings appSettings, IProfileRespository profileRepository)
        {
            _appSettings = appSettings;
            _profileRepository = profileRepository;

            RuleFor(x => x.ProfileType).IsInEnum();

            RuleFor(x => x.Email)
                .Must(IsValidEmailAddress)
                .WithMessage("Invalid Email Address");

            RuleFor(x => x.DateOfBirth)
                .Must(IsValidDateOfBirth)
                .WithMessage($"Invalid Dependent or Member must be old than {_appSettings.MinAgeInMonths} months");

            //RuleFor(x => x.IdentificationNumber)
            //    .MustAsync(async (IdentificationNumber, cancellation) =>
            //    {
            //        return await isUniqueID(IdentificationNumber);
            //    })
            //    .WithMessage($"Another profile has the same ID number");

            RuleFor(x => x.MobileNumber)
                .Must(IsValidMobileNumber)
                .WithMessage("Invalid Mobile Number");

            RuleFor(x => x.Address).NotNull();

            RuleFor(x => x.CreatedBy).NotNull();


        }

        private async Task<bool> isUniqueID(string idNumber)
        {
            var response = await _profileRepository.ValidateIdNumber(idNumber);

            return response.ResponseData[0];
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

                var ageInmonths = dateOfBirth.Subtract(DateTime.Now).Days / (365.2425 / 12) * -1;

                return (_appSettings.MinAgeInMonths <= ageInmonths);


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
