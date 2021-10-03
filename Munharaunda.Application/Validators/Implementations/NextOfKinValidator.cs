using FluentValidation;
using Muharaunda.Core.Contracts;
using Munharaunda.Core.Constants;
using Munharaunda.Core.Models;
using System.Threading.Tasks;

namespace Munharaunda.Application.Validators.Implementations
{
    public class NextOfKinValidator : AbstractValidator<ProfileNextOfKin>
    {
        private readonly IProfile _profileRespository;

        public NextOfKinValidator(IProfile profileRespository)
        {
            RuleFor(x => x.NextOfKinProfileId)
                .MustAsync(async (NextOfKinProfileId, cancellation) =>
                {
                    return await IsValidProfile(NextOfKinProfileId);
                })
                .WithMessage("Next of kin's profile not found");
            _profileRespository = profileRespository;
        }

        private async Task<bool> IsValidProfile(int profileId)
        {
            var response = await _profileRespository.GetProfileDetailsAsync(profileId);

            return response.ResponseCode == ResponseConstants.R00;
        }


    }
}
