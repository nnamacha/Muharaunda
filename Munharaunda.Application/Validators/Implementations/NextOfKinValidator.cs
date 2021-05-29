using FluentValidation;
using Muharaunda.Core.Models;
using Munharaunda.Application.Contracts;
using Munharaunda.Core.Constants;
using Munharaunda.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munharaunda.Application.Validators.Implementations
{
    public class NextOfKinValidator: AbstractValidator<ProfileNextOfKin>
    {
        private readonly IDataRespository _dataRespository;

        public NextOfKinValidator(IDataRespository dataRespository)
        {
            RuleFor(x => x.NextOfKinProfileId)
                .MustAsync(async (NextOfKinProfileId,cancellation) => {
                    return await IsValidProfile(NextOfKinProfileId);
                })
                .WithMessage("Next of kin's profile not found");
            _dataRespository = dataRespository;
        }

        private async Task<bool> IsValidProfile(int profileId)
        {
            var response =  await _dataRespository.GetProfileDetails(profileId);

            return response.ResponseCode == ResponseConstants.R00;
        }


    }
}
