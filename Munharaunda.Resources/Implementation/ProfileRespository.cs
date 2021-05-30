using Muharaunda.Core.Contracts;
using Muharaunda.Core.Models;
using Munharaunda.Core.Dtos;
using Munharaunda.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munharaunda.Resources.Implementation
{
    public class ProfileRespository : IProfileRespository
    {
        public Task<ResponseModel<Profile>> AuthoriseProfile(int ProfileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Profile>> CreateProfile(CreateProfileRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Profile>> DeleteProfile(int ProfileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<Profile>>> GetListOfActiveProfiles()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<Profile>>> GetListofAuthorisedProfiles()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<Profile>>> GetListOfDependentsByProfile(int profileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Profile>> GetNextOfKindByProfile(int profileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<Profile>>> GetOverAgeDependents()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Profile>> GetProfileDetails(int ProfileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<Profile>>> GetUnauthorisedProfiles()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> ValidateIdNumber(string IdNumber)
        {
            throw new NotImplementedException();
        }
    }
}
