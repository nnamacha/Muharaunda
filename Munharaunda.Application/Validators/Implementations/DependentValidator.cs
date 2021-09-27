using FluentValidation;
using Microsoft.Extensions.Configuration;
using Muharaunda.Core.Contracts;
using Muharaunda.Core.Models;
using Munharaunda.Application.Validators.Interfaces;
using Munharaunda.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Muharaunda.Core.Constants.SystemWideConstants;

namespace Munharaunda.Application.Validators.Implementations
{
    public class DependentValidator : AbstractValidator<ProfileBase>
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
