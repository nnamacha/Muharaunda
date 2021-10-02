using Muharaunda.Core.Models;
using Munharaunda.Core.Dtos;
using Munharaunda.Core.Models;
using System.Threading.Tasks;

namespace Muharaunda.Core.Contracts
{
    public interface IProfileRespository
    {
        #region Profile Related

        public Task<ResponseModel<ProfileBase>> CreateProfileAsync(CreateProfileRequest request);
        public Task<ResponseModel<Profile>> GetUnauthorisedProfilesAsync();
        public Task<ResponseModel<bool>> DeleteProfileAsync(int ProfileId);
        public Task<ResponseModel<Profile>> GetProfileDetailsAsync(int ProfileId);

        public Task<ResponseModel<bool>> AuthoriseProfileAsync(int ProfileId);
        public Task<ResponseModel<Profile>> GetListOfActiveProfilesAsync();

        public Task<ResponseModel<Profile>> GetListOfDependentsByProfileAsync(int profileId);
        public Task<ResponseModel<Profile>> GetNextOfKindByProfileAsync(int profileId);

        public Task<ResponseModel<bool>> ValidateIdNumber(string IdNumber);

        public Task<ResponseModel<bool>> UpdateProfileAsync(Profile profile);

        #endregion

    }
}
