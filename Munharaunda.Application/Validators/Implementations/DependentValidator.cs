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
    public class DependentValidator : IValidator<Profile>
    {
        public bool Validate(Profile t)
        {
            return t.ProfileType == ProfileTypes.Dependent ? true : false;
        }
    }
}
