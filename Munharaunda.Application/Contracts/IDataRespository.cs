using Muharaunda.Core.Models;
using Munharaunda.Application.Dtos;
using Munharaunda.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munharaunda.Application.Contracts
{
    public interface IDataRespository
    {
        #region Profile Related
        public Task<ResponseModel<Profile>> CreateProfile(CreateProfileRequest request);
        public Task<ResponseModel<List<Profile>>> GetUnauthorisedProfiles();
        public Task<ResponseModel<Profile>> DeleteProfile(int ProfileId);
        public Task<ResponseModel<Profile>> GetProfileDetails(int ProfileId);

        public Task<ResponseModel<Profile>> AuthoriseProfile(int ProfileId);
        public Task<ResponseModel<List<Profile>>> GetListofAuthorisedProfiles();
        public Task<ResponseModel<List<Profile>>> GetListOfActiveProfiles();

        public Task<ResponseModel<List<Profile>>> GetOverAgeDependents();
        public Task<ResponseModel<List<Profile>>> GetListOfDependentsByProfile(int profileId);
        public Task<ResponseModel<Profile>> GetNextOfKindByProfile(int profileId);


        #endregion

    }
}
