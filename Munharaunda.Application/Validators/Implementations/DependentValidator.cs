using FluentValidation;
using Muharaunda.Core.Models;
using Munharaunda.Application.Validators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Muharaunda.Core.Constants.SystemWideConstants;

namespace Munharaunda.Application.Validators.Implementations
{
    public class DependentValidator : AbstractValidator<Profile>
    {
        public DependentValidator()
        {
            RuleFor(x => x.ProfileType).Equal(ProfileTypes.Dependent);
        }
        
    }
}
