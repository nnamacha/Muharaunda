using Muharaunda.Core.Contracts;
using Muharaunda.Core.Models;
using Munharaunda.Core.Dtos;
using Munharaunda.Core.Models;
using Munharaunda.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munharaunda.Resources.Implementation
{
    public class ProfileRespository : IProfileRespository
    {
        public Task<ResponseModel<ProfileBase>> AuthoriseProfileAsync(int ProfileId)
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

        public Task<ResponseModel<ProfileBase>> GetListOfActiveProfilesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Profile>> GetListofAuthorisedProfiles()
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


        public Task<ResponseModel<ProfileBase>> GetProfileDetailsAsync(int ProfileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<ProfileBase>> GetUnauthorisedProfilesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> ValidateIdNumber(string IdNumber)
        {
            throw new NotImplementedException();
        }

   
    }
}
