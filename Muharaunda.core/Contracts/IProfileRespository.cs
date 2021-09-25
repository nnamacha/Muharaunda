using Muharaunda.Core.Models;
using Munharaunda.Application.Dtos;
using Munharaunda.Application.Orchestration.Contracts;
using Munharaunda.Core.Dtos;
using Munharaunda.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muharaunda.Core.Contracts
{
    public interface IProfileRespository
    {
        #region Profile Related

        public Task<ResponseModel<Profile>> CreateProfile(CreateProfileRequest request);
        public Task<ResponseModel<Profile>> GetUnauthorisedProfiles();
        public Task<ResponseModel<bool>> DeleteProfile(int ProfileId);
        public Task<ResponseModel<Profile>> GetProfileDetails(int ProfileId);

        public Task<ResponseModel<Profile>> AuthoriseProfile(int ProfileId);
        public Task<ResponseModel<Profile>> GetListOfActiveProfiles();

        
        public Task<ResponseModel<Profile>> GetListOfDependentsByProfile(int profileId);
        public Task<ResponseModel<Profile>> GetNextOfKindByProfile(int profileId);

        public Task<ResponseModel<bool>> ValidateIdNumber(string IdNumber);

        #endregion

    }
}
