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
        public Task<ResponseModel<ProfileBase>> GetUnauthorisedProfilesAsync();
        public Task<ResponseModel<bool>> DeleteProfileAsync(int ProfileId);
        public Task<ResponseModel<ProfileBase>> GetProfileDetailsAsync(int ProfileId);

        public Task<ResponseModel<ProfileBase>> AuthoriseProfileAsync(int ProfileId);
        public Task<ResponseModel<ProfileBase>> GetListOfActiveProfilesAsync();

        public Task<ResponseModel<Profile>> GetListOfDependentsByProfileAsync(int profileId);
        public Task<ResponseModel<Profile>> GetNextOfKindByProfileAsync(int profileId);

        public Task<ResponseModel<bool>> ValidateIdNumber(string IdNumber);

        #endregion

    }
}
