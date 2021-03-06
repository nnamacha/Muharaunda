using Muharaunda.Core.Models;
using Muharaunda.Domain.Models;
using Munharaunda.Core.Dtos;
using Munharaunda.Core.Models;
using Munharaunda.Domain.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Muharaunda.Core.Constants.SystemWideConstants;

namespace Muharaunda.Core.Contracts
{
    public interface IProfile
    {
        #region Profile Related

        public Task<ResponseModel<ProfileBase>> CreateProfileAsync(ProfileBase request, bool checkUnique = false);
        public Task<ResponseModel<ProfileBase>> GetUnauthorisedProfilesAsync();
        public Task<ResponseModel<bool>> DeleteProfileAsync(int profileId);
        public Task<ResponseModel<ProfileBase>> GetProfileDetailsAsync(int profileId);

        public Task<ResponseModel<bool>> AuthoriseProfileAsync(int profileId);
        public Task<ResponseModel<ProfileBase>> GetListOfActiveProfilesAsync();

        public Task<ResponseModel<ProfileBase>> GetListOfDependentsByProfileAsync(int profileId);
        public Task<ResponseModel<ProfileBase>> GetNextOfKindByProfileAsync(int profileId);

        public Task<ResponseModel<bool>> ValidateIdNumber(string idNumber);

        public Task<ResponseModel<bool>> UpdateProfileAsync(Profile profile);
        public Task<ResponseModel<bool>> UpdateProfileStatusAsync(int profileId, Statuses newStatus);
        public Task CreateBulkProfilesAsync(List<ProfileBase> profiles);
        public Task UpdateBulkProfilesAsync(List<ProfileBase> profiles);



        #endregion

    }
}
