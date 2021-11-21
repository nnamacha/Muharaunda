using FluentValidation;
using Muharaunda.Core.Contracts;
using Muharaunda.Core.Models;
using Munharaunda.Domain.Contracts;
using System;
using static Muharaunda.Core.Constants.SystemWideConstants;

namespace Munharaunda.Application.Validators.Implementations
{
    public class FuneralValidator : AbstractValidator<Funeral>
    {
        private readonly Muharaunda.Core.Contracts.IAppSettings _appSettings;
        private readonly IFuneralRepository _funeralRepository;

        public FuneralValidator(Muharaunda.Core.Contracts.IAppSettings appSettings, IFuneralRepository funeralRepository)
        {
            _appSettings = appSettings;

            _funeralRepository = funeralRepository;

            //RuleFor(x => x.Profile.ProfileStatus)
            //    .Must(isActiveProfile);

            //RuleFor(x => x.Profile.ProfileType)
            //    .Must(isValidProfileType);

            //RuleFor(x => x.Profile.ActivationDate)
            //    .Must(isActivatedProfile);
        }

        private bool isActivatedProfile(DateTime activationDate)
        {
            return DateTime.Now > activationDate;
        }

        private bool isValidProfileType(ProfileTypes profileType)
        {
            return (profileType == ProfileTypes.Admin || profileType == ProfileTypes.Member);
        }

        private bool isActiveProfile(Statuses status)
        {
            return status == Statuses.Active;
        }
    }
}
