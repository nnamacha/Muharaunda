﻿using Muharaunda.Core.Contracts;
using Muharaunda.Core.Models;
using Munharaunda.Core.Dtos;
using Munharaunda.Core.Models;
using System;
using System.Threading.Tasks;

namespace Munharaunda.Resources.Implementation
{
    public class ProfileRespository : IProfileRespository
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

        public Task<ResponseModel<ProfileBase>> GetListOfActiveProfilesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Profile>> GetListofAuthorisedProfiles()
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

        public Task<ResponseModel<ProfileBase>> GetListOfDependentsByProfileAsync(int profileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<ProfileBase>> GetNextOfKindByProfileAsync(int profileId)
        {
            throw new NotImplementedException();
        }
    }
}
