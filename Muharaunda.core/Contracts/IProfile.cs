using Muharaunda.Core.Models;
using Muharaunda.Domain.Models;
using Munharaunda.Core.Dtos;
using Munharaunda.Core.Models;
using Munharaunda.Domain.Contracts;
using System.Threading.Tasks;
using static Muharaunda.Core.Constants.SystemWideConstants;

namespace Muharaunda.Core.Contracts
{
    public interface IProfile
    {
        #region Profile Related

        public Task<ResponseModel<IProfileBase>> CreateProfileAsync(IProfileBase request, bool checkUnique = false);
        public Task<ResponseModel<IProfileBase>> GetUnauthorisedProfilesAsync();
        public Task<ResponseModel<bool>> DeleteProfileAsync(int ProfileId);
        public Task<ResponseModel<IProfileBase>> GetProfileDetailsAsync(int ProfileId);

        public Task<ResponseModel<bool>> AuthoriseProfileAsync(int ProfileId);
        public Task<ResponseModel<IProfileBase>> GetListOfActiveProfilesAsync();

        public Task<ResponseModel<IProfileBase>> GetListOfDependentsByProfileAsync(int profileId);
        public Task<ResponseModel<IProfileBase>> GetNextOfKindByProfileAsync(int profileId);

        public Task<ResponseModel<bool>> ValidateIdNumber(string IdNumber);

        public Task<ResponseModel<bool>> UpdateProfileAsync(Profile profile);
        public Task<ResponseModel<bool>> UpdateProfileStatusAsync(int profileId, Statuses newStatus);

        #endregion

    }
}
