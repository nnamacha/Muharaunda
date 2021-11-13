using FluentValidation;
using Muharaunda.Core.Contracts;
using Muharaunda.Core.Models;
using Muharaunda.Domain.Models;
using System;
using static Muharaunda.Core.Constants.SystemWideConstants;

namespace Munharaunda.Application.Validators.Implementations
{
    public class DependentValidator : AbstractValidator<IProfileBase>
    {

        private readonly IAppSettings _appSettings;

        public DependentValidator(IAppSettings appSettings)
        {

            RuleFor(x => x.ProfileType).Equal(ProfileTypes.Dependent);

            RuleFor(x => x.DateOfBirth).Must(IsAMinor).WithMessage("Dependent Not a minor");

            _appSettings = appSettings;
        }

        private bool IsAMinor(string dateOfBirth)
        {
            int age = new DateTime(DateTime.Now.Subtract(DateTime.Parse(dateOfBirth)).Ticks).Year - 1;

            return _appSettings.MaximumDependentAge > age;
        }

    }
}
