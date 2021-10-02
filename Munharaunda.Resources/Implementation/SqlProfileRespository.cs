using Muharaunda.Core.Contracts;
using Muharaunda.Core.Models;
using Munharaunda.Core.Dtos;
using Munharaunda.Core.Models;
using System;
using System.Threading.Tasks;

namespace Munharaunda.Resources.Implementation
{
    public class SqlProfileRespository : IProfileRespository
    {


        public Task<ResponseModel<bool>> AuthoriseProfileAsync(int ProfileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<ProfileBase>> CreateProfileAsync(CreateProfileRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> DeleteProfileAsync(int ProfileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Profile>> GetListOfActiveProfilesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Profile>> GetListofAuthorisedProfiles()
        {
            throw new NotImplementedException();
        }



        public Task<ResponseModel<Profile>> GetProfileDetailsAsync(int ProfileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Profile>> GetUnauthorisedProfilesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> ValidateIdNumber(string IdNumber)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Profile>> GetListOfDependentsByProfileAsync(int profileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Profile>> GetNextOfKindByProfileAsync(int profileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Profile>> UpdateProfileAsync(Profile profile)
        {
            throw new NotImplementedException();
        }

        Task<ResponseModel<bool>> IProfileRespository.UpdateProfileAsync(Profile profile)
        {
            throw new NotImplementedException();
        }
    }
}
